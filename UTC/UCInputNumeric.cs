using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UTC
{
    public partial class UCInputNumeric : UserControl
    {
        private UTC.UTCTextBox _txtInputbox;

        public UTC.UTCTextBox txtInputbox
        {
            get { return _txtInputbox; }
            set { _txtInputbox = value; }
        }
        public UCInputNumeric()
        {
            InitializeComponent();
        }
        public UCInputNumeric(UTC.UTCTextBox pTxtBox)
        {
            InitializeComponent();
            _txtInputbox = pTxtBox;
        }

        private void CmdN1_Click(object sender, EventArgs e)
        {
            UTC.UTCButton Btn = (UTC.UTCButton)sender;
            UTC.UTCTextBox txtBox = _txtInputbox;
            string StrNum = txtBox.Text;
            if (txtBox.SelectedText.Length == txtBox.Text.Length)
            {
                StrNum = "";
                txtBox.Text = "";
            }
            if (Btn.Tag.ToString() == "C")
            {
                StrNum = "";
            }

            else if (Btn.Tag.ToString() == ".")
            {
                if (StrNum.Contains(".") == true)
                {
                    return;
                }
                StrNum = StrNum + ".";
            }
            else if (Microsoft.VisualBasic.Information.IsNumeric(StrNum + Btn.Tag.ToString()) == true)
            {
                StrNum = StrNum + Btn.Tag.ToString();
            }
            if (StrNum.Equals("."))
                txtBox.Text = "0.";
            else
            {
                string[] StrSplit;
                if (StrNum.Contains("."))
                {
                    StrSplit = StrNum.Split('.');
                    StrNum = StrSplit[0] + ".";
                    switch (StrSplit[1].Length)
                    {
                        case 1: StrNum += StrSplit[1]; break;
                        case 2:
                        case 3: StrNum += StrSplit[1].Substring(0, 2); break;
                        default:
                            StrNum += "";
                            break;
                    }
                }
                txtBox.Text = StrNum;
            }
            if (Btn.Tag.ToString() == "BACK")
            {
                if (txtBox.SelectedText.Length != 0)
                {
                    txtBox.Text = txtBox.Text.Remove(txtBox.SelectionStart, txtBox.SelectionLength);
                }
                else if (txtBox.Text != "")
                {
                    txtBox.Text = txtBox.Text.Substring(0, txtBox.Text.Length - 1);
                }
            }
            if (txtBox.Text.StartsWith("."))
            {
                txtBox.Text = "0" + txtBox.Text;
            }
        }

        private void BtnHide_Click(object sender, EventArgs e)
        {
            Done();
            if (_txtInputbox.CanFocus == true) _txtInputbox.Focus();
        }
        public void Done()
        {
            if (_txtInputbox != null)
            {
                this.Hide();
            }
            this.Visible = false;
        }

        private void UCInputNumeric_Leave(object sender, EventArgs e)
        {
            Done();
        }

    }
}
