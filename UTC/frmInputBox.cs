using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UTC
{
    public partial class frmInputBox : Form
    {

        #region Public Properties

        string formCaption = string.Empty;
        public string FormCaption
        {
            get { return formCaption; }
            set { formCaption = value; }
        }

        string formPrompt = string.Empty;
        public string FormPrompt
        {
            get { return formPrompt; }
            set { formPrompt = value; }
        }

        string inputResponse = string.Empty;
        public string InputResponse
        {
            get { return inputResponse; }
            set { inputResponse = value; }
        }

        string defaultValue = string.Empty;
        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        #endregion

        #region Form and Control Events
        private void InputBox_Load(object sender, System.EventArgs e)
        {
            this.txtInput.Text = defaultValue;
            this.lblPrompt.Text = formPrompt;
            this.Text = formCaption;
            this.txtInput.SelectionStart = 0;
            this.txtInput.SelectionLength = this.txtInput.Text.Length;
            this.txtInput.Focus();
        }
        public string ShowForm()
        {
            this.Show();
            return (inputResponse);
        }
        private void BtnOK_Click(object sender, System.EventArgs e)
        {
            InputResponse = this.txtInput.Text;
            this.Close();
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void frmInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            //Val.FormKeyDownEvent(sender,e);
        }

        private void frmInputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }
    }
}