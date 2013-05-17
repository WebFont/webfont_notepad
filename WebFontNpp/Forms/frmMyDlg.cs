using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WebFontNpp.Util;

namespace WebFontNpp
{
    public partial class frmMyDlg : Form
    {
        public frmMyDlg()
        {
            InitializeComponent();

            textBox1.Text = "hello!";

            try
            {
                var fonts = FontLoader.LoadFonts();

                textBox1.Text = fonts.Count.ToString();

            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }
        }

        
    }
}
