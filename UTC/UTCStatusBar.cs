using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Configuration;
using System.Runtime.InteropServices;

namespace UTC
{
    public partial class UTCStatusBar : UserControl
    {
        private string _CompanyName = "";

        public string CompanyName
        {
            get { return _CompanyName; }
            set
            {
                _CompanyName = value;
                lblCompanyName.Text = _CompanyName;
            }
        }

        private string _ErrorMessage = "";

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                _ErrorMessage = value;
                lblErrorMessage.Text = _ErrorMessage;
            }
        }


        //public bool CapsLockState()
        //{
        //    bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        //    return CapsLock;
        //}
        //public bool NumLockState()
        //{
        //    bool NumLock = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
        //    return NumLock;
        //}
        //public bool ScrollLockState()
        //{
        //    bool ScrollLock = (((ushort)GetKeyState(0x91)) & 0xffff) != 0;
        //    return ScrollLock;
        //}


        private System.Windows.Forms.Form _ParentForm;
        [Browsable(true)]
        public System.Windows.Forms.Form ParentForm
        {
            get { return _ParentForm; }
            set
            {
                _ParentForm = value;
                _ParentWidth = this.FindForm().Width;
                SetLayout();
            }
        }

        private int _ParentWidth;
        /// <summary>
        /// Parent Form Width
        /// </summary>
        public int ParentWidth
        {
            get { return _ParentWidth; }
            set { 
                _ParentWidth = value;
                SetLayout();
                }
        }

        public UTCStatusBar()
        {
            InitializeComponent();            
        }

        public void SetLayout()
        {
            if (ParentWidth != 0)
            {
                this.BackColor = System.Drawing.SystemColors.Control;
                this.Height = 20;
                int Width = ParentWidth;
                int ControlWidth = Width / 3;
                int ControlHeight = lblErrorMessage.Height;
                this.Size = new Size(ParentWidth, this.Height);
                
                lblCompanyName.Size = new Size(ControlWidth - 10, ControlHeight);
                lblCompanyName.TextAlign = ContentAlignment.MiddleRight;

                lblErrorMessage.Size = new Size(ControlWidth, ControlHeight);
                lblErrorMessage.TextAlign = ContentAlignment.MiddleLeft;

                lblCapsLock.Size = new Size(ControlWidth, ControlHeight);
                lblCapsLock.TextAlign = ContentAlignment.MiddleLeft;

                lblErrorMessage.Location = new Point(0 * ControlWidth, 0);
                lblCapsLock.Location = new Point(1 * ControlWidth , 0);
                lblCompanyName.Location = new Point(2 * ControlWidth, 0);
            }
        }

        private void TimerCaps_Tick(object sender, EventArgs e)
        {
            string Str = "";
            if (Control.IsKeyLocked(Keys.CapsLock) == true)
            {
                Str = "Caps Lock on";
            }
            else
            {
                Str = "Caps Lock off";
            }
            Str = Str + "   |   ";
            if (Control.IsKeyLocked(Keys.NumLock) == true)
            {
                Str = Str + "Num Lock on";
            }
            else
            {
                Str = Str + "Num Lock off";
            }
            Str = Str + "   |   ";
            Str = Str + DateTime.Now.ToString("dd/MM/yyyy") + "  " + DateTime.Now.ToString("hh:mm:ss tt");
            lblCapsLock.Text = Str;
            lblCompanyName.Text = "";
        }

        private void TimerErrorMessage_Tick(object sender, EventArgs e)
        {            
            lblErrorMessage.Text = _ErrorMessage;
            if (_ErrorMessage != "")
            {
                _ErrorMessage = "";
            }
        }

        private void lblCompanyName_Click(object sender, EventArgs e)
        {
            
        }

    }
}
