using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using NppPluginNET;
using WebFontNpp.Model;
using WebFontNpp.Util;

namespace WebFontNpp
{
    public partial class frmMyDlg : Form
    {
        private const String COMMON_IMPORT_PATH = "http://webfonts.ru/import/";

        private BindingListView<Font> fontsBindingListView;

        private bool downloadFont = false;

        public frmMyDlg()
        {
            InitializeComponent();

            try
            {
                loadingProgressBar.Visible = true;
                loadFontsWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                filterTextBox.Text = ex.Message;
            }
        }

        public frmMyDlg(bool downloadFont)
            : this()
        {
            this.downloadFont = downloadFont;
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            var filter = filterTextBox.Text;

            if (fontsBindingListView != null)
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    fontsBindingListView.ApplyFilter(f => f.name.ToLower().Contains(filter.ToLower()));
                }
                else
                {
                    fontsBindingListView.RemoveFilter();
                }

            }

        }

        private void loadFontsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var fonts = FontLoader.LoadFonts();
            e.Result = fonts;
        }

        private void loadFontsWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var fonts = (List<Font>)e.Result;

            fontsBindingListView = new BindingListView<Font>(fonts);

            //fontsListBox.BeginUpdate();
            fontsListBox.DataSource = fontsBindingListView;
            //fontsListBox.EndUpdate();

            fontsListBox.Enabled = true;
            filterTextBox.Enabled = true;

            loadingProgressBar.Visible = false;
        }

        private void fontsListBox_DoubleClick(object sender, EventArgs e)
        {
            var selectedFont = fontsListBox.SelectedItem as ObjectView<Font>;
            if (selectedFont != null)
            {


                try
                {
                    if (downloadFont)
                    {
                        var folderBrowserDialog = new FolderBrowserDialog();
                        folderBrowserDialog.SelectedPath = GetCurrentPath();
                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            var selectedFolder = folderBrowserDialog.SelectedPath;
                            if (Directory.Exists(selectedFolder))
                            {
                                FontLoader.UnpackFontToFolder(selectedFolder, selectedFont.Object);
                                var fontRelativePath = GetFontRelativePath(GetCurrentPath(), selectedFolder);

//                                MessageBox.Show(string.Format("{0}\n {1}\n {2}", GetCurrentPath(), selectedFolder,
//                                    fontRelativePath));

                                var curScintilla = PluginBase.GetCurrentScintilla();

                                var insertTextBuilder = new StringBuilder();
                                insertTextBuilder.AppendLine(selectedFont.Object.import.Replace(COMMON_IMPORT_PATH, fontRelativePath));
                                insertTextBuilder.AppendLine(selectedFont.Object.comments);

                                Win32.SendMessage(curScintilla, SciMsg.SCI_REPLACESEL, 0, insertTextBuilder);
                            }
                        }

                    }
                    else
                    {
                        var curScintilla = PluginBase.GetCurrentScintilla();

                        var insertTextBuilder = new StringBuilder();
                        insertTextBuilder.AppendLine(selectedFont.Object.import);
                        insertTextBuilder.AppendLine(selectedFont.Object.comments);

                        Win32.SendMessage(curScintilla, SciMsg.SCI_REPLACESEL, 0, insertTextBuilder);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString(), "Exception message", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private string GetFontRelativePath(string documentPath, string folderPath)
        {
            var relativePath = ConvertToRelativePath(documentPath, folderPath);

            if (relativePath != null && !string.IsNullOrEmpty(relativePath))
            {
                relativePath = relativePath + "\\";
            }

            return relativePath;
        }

        private string GetCurrentPath()
        {
            var currentPath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETFULLCURRENTPATH, Win32.MAX_PATH, currentPath);
            return Path.GetDirectoryName(currentPath.ToString()) ?? string.Empty;
        }

        public static String ConvertToRelativePath(String absolutePath, String relativeTo)
        {
            if (string.IsNullOrEmpty(absolutePath))
            {
                return relativeTo;
            }

            StringBuilder relativePath = null;

            // Thanks to:
            // http://mrpmorris.blogspot.com/2007/05/convert-absolute-path-to-relative-path.html
           // absolutePath = absolutePath.Replace("\\", "/");
           // relativeTo = relativeTo.Replace("\\", "/");

            if (string.Equals(absolutePath, relativeTo, StringComparison.InvariantCultureIgnoreCase))
            {

            }
            else
            {
                var absoluteDirectories = absolutePath.Split(new []{'\\'});
                var relativeDirectories = relativeTo.Split(new []{ '\\'});

                //Get the shortest of the two paths
                int length = absoluteDirectories.Length < relativeDirectories.Length ?
                        absoluteDirectories.Length : relativeDirectories.Length;

                //Use to determine where in the loop we exited
                int lastCommonRoot = -1;
                int index;

                //Find common root
                for (index = 0; index < length; index++)
                {
                    if (string.Equals(absoluteDirectories[index],(relativeDirectories[index]), StringComparison.InvariantCultureIgnoreCase))
                    {
                        lastCommonRoot = index;
                    }
                    else
                    {
                        break;
                        //If we didn't find a common prefix then throw
                    }
                }
                if (lastCommonRoot != -1)
                {
                    //Build up the relative path
                    relativePath = new StringBuilder();
                    //Add on the ..
                    for (index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++)
                    {
                        if (absoluteDirectories[index].Length > 0)
                        {
                            relativePath.Append("..\\");
                        }
                    }
                    for (index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++)
                    {
                        relativePath.Append(relativeDirectories[index] + "\\");
                    }
                    relativePath.Append(relativeDirectories[relativeDirectories.Length - 1]);
                }
            }
            return relativePath == null ? "" : relativePath.ToString();
        }
    }
}
