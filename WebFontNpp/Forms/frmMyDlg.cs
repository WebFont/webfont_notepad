using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        BindingListView<Font> fontsBindingListView;

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
                IntPtr curScintilla = PluginBase.GetCurrentScintilla();

                var insertTextBuilder = new StringBuilder();
                insertTextBuilder.AppendLine(selectedFont.Object.import);
                insertTextBuilder.AppendLine(selectedFont.Object.comments);

                Win32.SendMessage(curScintilla, SciMsg.SCI_REPLACESEL, 0, insertTextBuilder);
            }
        }


    }
}
