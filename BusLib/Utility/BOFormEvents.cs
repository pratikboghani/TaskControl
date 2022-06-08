using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Val = BusLib.Validation.BOValidation;

namespace BusLib.Utility
{
    public class BOFormEvents
    {
        private Form _mForm;
        private UserControl _mUsreCtrl;
        private System.Collections.ArrayList _mArrList = new System.Collections.ArrayList();

        public System.Collections.ArrayList ObjToDispose
        {
            get
            
            {
                return _mArrList;
            }
            set
            {
                _mArrList.Add(value);
            }
        }

        public Form CurForm
        {
            get
            {
                return _mForm;
            }
            set
            {
                _mForm = value;
            }
        }

        public UserControl CurFormCtrl
        {
            get
            {
                return _mUsreCtrl;
            }
            set
            {
                _mUsreCtrl = value;
            }
        }

        public bool FormKeyDown
        {
            set
            {
                if (value == true)
                {
                    CurForm.KeyDown += new KeyEventHandler(Form_KeyDown);
                }
            }
        }

        public bool FormKeyDownUSrCtrl
        {
            set
            {
                if (value == true)
                {
                    CurFormCtrl.KeyDown += new KeyEventHandler(Form_KeyDownUserCtrl);
                }
            }
        }

        public bool FormKeyPress
        {
            set
            {
                if (value == true)
                {
                    CurForm.KeyPress += new KeyPressEventHandler(Form_KeyPress);
                }
            }
        }

        public bool FormKeyPressAsItIs
        {
            set
            {
                if (value == true)
                {
                    CurForm.KeyPress += new KeyPressEventHandler(Form_KeyPressAsItIs);
                }
            }
        }

        public bool FormResize
        {
            set
            {
                if (value == true)
                {
                    CurForm.Resize += new EventHandler(Form_Resize);
                }
            }
        }

        public bool FormDisposed
        {
            set
            {
                if (value == true)
                {
                    CurForm.Disposed += new EventHandler(Form_Disposed);
                }
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            Val.FormKeyDownEvent(sender, e);
        }

        private void Form_KeyDownUserCtrl(object sender, KeyEventArgs e)
        {
            Val.FormKeyDownEventUsrControl(sender, e);
        }

        private void Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void Form_KeyPressAsItIs(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar;
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            Val.frmResize(CurForm);
        }
        private void Form_Disposed(object sender, System.EventArgs e)
        {
            for (Int16 inti = 0; inti <= ObjToDispose.Count - 1; inti++)
            {
                ObjToDispose[inti] = null;
            }
        }
    }
}
