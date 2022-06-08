using Infragistics.Win.AppStyling;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskControl.Utility;

namespace TaskControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Login FrmLogin;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                //String StrStyle = Properties.Settings.Default.theme.ToString();
                //StyleManager.Load(Application.StartupPath + "\\Styles\\" + StrStyle);

                // ChkUpdate();

                if (BusLib.Configuration.BOConfiguration.ReadUserSetting() == true)
                {
                    Application.SetCompatibleTextRenderingDefault(true);
                    FrmLogin = new Login();

                   string  UserName = BusLib.Configuration.BOConfiguration.EndUserId;
                   string Password = BusLib.Configuration.BOConfiguration.EndUserPass;

                    FrmLogin.txtUserName.Text = UserName;
                    FrmLogin.txtPass.Text = Password;
                }
                else
                {
                    MessageBox.Show("Give User Id, Password Info. In Config File");
                    return;
                }


                FrmLogin.ShowDialog();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error During Reading Config File...." + ex.ToString());
            }
        }
    }
}
