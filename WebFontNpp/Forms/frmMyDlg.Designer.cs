namespace WebFontNpp
{
    partial class frmMyDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.fontsListBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.loadingProgressBar = new System.Windows.Forms.ProgressBar();
            this.loadFontsWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // filterTextBox
            // 
            this.filterTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterTextBox.Enabled = false;
            this.filterTextBox.Location = new System.Drawing.Point(0, 0);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(366, 20);
            this.filterTextBox.TabIndex = 0;
            this.filterTextBox.TextChanged += new System.EventHandler(this.filterTextBox_TextChanged);
            // 
            // fontsListBox
            // 
            this.fontsListBox.DisplayMember = "name";
            this.fontsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fontsListBox.Enabled = false;
            this.fontsListBox.FormattingEnabled = true;
            this.fontsListBox.Location = new System.Drawing.Point(0, 0);
            this.fontsListBox.Name = "fontsListBox";
            this.fontsListBox.Size = new System.Drawing.Size(366, 362);
            this.fontsListBox.TabIndex = 1;
            this.fontsListBox.DoubleClick += new System.EventHandler(this.fontsListBox_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.loadingProgressBar);
            this.panel1.Controls.Add(this.fontsListBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 362);
            this.panel1.TabIndex = 2;
            // 
            // loadingProgressBar
            // 
            this.loadingProgressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.loadingProgressBar.Location = new System.Drawing.Point(0, 0);
            this.loadingProgressBar.Name = "loadingProgressBar";
            this.loadingProgressBar.Size = new System.Drawing.Size(366, 25);
            this.loadingProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.loadingProgressBar.TabIndex = 2;
            // 
            // loadFontsWorker
            // 
            this.loadFontsWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loadFontsWorker_DoWork);
            this.loadFontsWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loadFontsWorker_RunWorkerCompleted);
            // 
            // frmMyDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 382);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.filterTextBox);
            this.Name = "frmMyDlg";
            this.Text = "frmMyDlg";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.ListBox fontsListBox;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker loadFontsWorker;
        private System.Windows.Forms.ProgressBar loadingProgressBar;
    }
}