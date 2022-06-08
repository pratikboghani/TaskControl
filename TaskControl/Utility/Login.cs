using BusLib.Table;
using BusLib.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Val = BusLib.Validation.BOValidation;
using Ope = DataLib.OperationSql;
using Events = BusLib.Utility;
using GSql = DataLib.GlobalSql;

namespace TaskControl.Utility
{
    public partial class Login : Form
    {
        private clsLogin objclsLogin = new clsLogin();
        private Events.BOFormEvents ObjFormEvents = new Events.BOFormEvents();
        private BOLogin _BOLogin = new BOLogin();
        public Login()
        {
            InitializeComponent();
            ObjFormEvents.CurForm = this;
            ObjFormEvents.FormKeyPress = true;
            ObjFormEvents.FormKeyDown = true;
            //ObjFormEvents.FormResize = true;
            ObjFormEvents.FormDisposed = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Int16 Result = 0;
            if ((txtUserName.Text == ""))
            {
                //Val.PrnMsg("Enter User Name");
                lblError.Text = "Enter User Name";
                lblError.Visible = true;
                txtUserName.Focus();
                return;
            }
            if ((txtPass.Text == ""))
            {
                //Val.PrnMsg("Enter Password");
                lblError.Text = "Enter Password";
                lblError.Visible = true;
                txtPass.Focus();
                return;
            }


            if (DataLib.OperationSql.ReadDatabaseSettingLocal() == true)
            {
                if (_BOLogin.ServerOn() == false)
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }





            //if (txtAllowOneTime.Text.ToLower() != StrOneTimePass)

            
            objclsLogin.UserId = txtUserName.Text;
            objclsLogin.Password = txtPass.Text;

            Result = _BOLogin.CheckLogin(objclsLogin);
            if (Result == 0)
            {
                lblError.Text = "UserName Not Found";
                lblError.Visible = true;
                //MessageBox.Show("UserName Not Found", "User Login Authentication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Focus();
                return;
            }
            else if (Result == 2)
            {
                lblError.Text = "Invalid Password Try Again !!!";
                lblError.Visible = true;
                //MessageBox.Show("Invalid Password Try Again !!!", "User Login Authentication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPass.Focus();
                return;
            }
            else
            {
                lblError.Visible = false;
                
                this.Close();
                this.Hide();
                this.Dispose();
       
                Home MainPoint = new Home();
             
                MainPoint.ShowDialog();


            }
        }
    
        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();

        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
