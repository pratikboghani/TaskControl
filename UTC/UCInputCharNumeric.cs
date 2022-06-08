using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UTC
{
    public partial class UCInputCharNumeric : UserControl
    {
        private UTC.UTCTextBox _txtInputbox;
        
        public UTC.UTCTextBox txtInputBox
        {
            get { return _txtInputbox; }
            set { _txtInputbox = value; }
        }

        public UCInputCharNumeric()
        {
            InitializeComponent();
        }
        public UCInputCharNumeric(UTC.UTCTextBox pTxtBox)
        {
            InitializeComponent();
            _txtInputbox = pTxtBox;
        }
        private void CmdCharClick(UTC.UTCButton Btn)
        {
            string StrChar = _txtInputbox.Text;
            switch (Btn.Tag.ToString().ToUpper())
            {
                case "HIDE":
                    if (_txtInputbox.CanFocus==true) _txtInputbox.Focus();
                    this.Hide();
                    break;
                case "BACK": StrChar = (StrChar.Length != 0 ? StrChar.Substring(0, StrChar.Length - 1) : ""); break;
                default: StrChar += Btn.Tag.ToString(); break;
            }
            _txtInputbox.Text = StrChar;
        }

        private void Char_Click(object sender, EventArgs e)
        {
            CmdCharClick((UTC.UTCButton)sender);
            //CmdCharClick((ModCont.cButton)sender);
            //Done();
            //if (_txtInputbox.CanFocus == true) _txtInputbox.Focus();
            //this.Visible = false;
        }
        public void Done()
        {
            if (_txtInputbox != null)
            {
                string StrChar = _txtInputbox.Text;
                this.Hide();
                _txtInputbox.Text = StrChar;
            }
        }

        private void UCInputCharNumeric_Leave(object sender, EventArgs e)
        {
            Done();
        }
    }
}
