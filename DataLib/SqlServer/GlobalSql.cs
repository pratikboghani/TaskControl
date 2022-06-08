using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataLib
{
    public class GlobalSql
    {
        #region

        public static string DiamondServer = String.Empty;

        public static string DiamondDBName = String.Empty;

        public static string DiamondDBUser = String.Empty;

        public static string DiamondDBPass = String.Empty;

        public static string GConnString = String.Empty;

        public static string GSalString = String.Empty;

        public static string GWebConnString = String.Empty;

        public static string GAccConnString = String.Empty;
        public static string GRGConnString = String.Empty;
        public static string GSGConnString = String.Empty;
        public static string ConnectionStringAutoUpd = String.Empty;
        public static string GStrUserCategory = String.Empty;
        public static string UserShift = "";
        #endregion

        #region

        public static string GStrEndUserId = String.Empty;
        public static int GIntEndUserCode = 0;
        public static int GDeptCode = 0;

        public static string GStrEndUserPass = String.Empty;

        public static string GstrConnetion = String.Empty;

        public static string GStrEndUserCat = String.Empty;

        public static string gStrDateFormat = "MM/dd/yy";

        public static string gStrTimeFormat = "HH:MM tt";
        public static string Theme = "";

        #endregion

        #region

        public static DataSet GDSMast = new DataSet();

        public static DataTable GDataTable = new DataTable();

        public static SqlConnection GConn;

        public static SqlDataAdapter GDataAdapter = new SqlDataAdapter();

        public static SqlCommand GComm = new SqlCommand();

        public static SqlDataReader GDataReader;

        public static SqlTransaction GSqlTran;

        public static string GStrComputerName = System.Environment.MachineName.ToString();


        public static SqlConnection GWebConn;
        public static SqlDataAdapter GWebDataAdapter = new SqlDataAdapter();
        public static SqlCommand GWebComm = new SqlCommand();
        public static SqlDataReader GWebDataReader;


        public static SqlConnection GAccConn;
        public static SqlDataAdapter GAccDataAdapter = new SqlDataAdapter();
        public static SqlCommand GAccComm = new SqlCommand();
        public static SqlDataReader GAccDataReader;


        public static SqlConnection GRGConn;
        public static SqlDataAdapter GRGDataAdapter = new SqlDataAdapter();
        public static SqlCommand GRGComm = new SqlCommand();
        public static SqlDataReader GRGDataReader;

        public static SqlConnection GSGConn;
        public static SqlDataAdapter GSGDataAdapter = new SqlDataAdapter();
        public static SqlCommand GSGComm = new SqlCommand();
        public static SqlDataReader GSGDataReader;

        public static string GStrRPTPath = String.Empty;

        #endregion

        #region Global Message

        public static Int32 GIntMesgMode;

        public static string GStrMsgCaption = "Account";

        public static string GStrMsgDeleteDeny = "Sorry, Delete Permission Denied";

        public static string GStrMsgInsertDeny = "Sorry, Insert Permission Denied";

        public static string GStrMsgUpdateDeny = "Sorry, Update Permission Denied";

        public static string GStrMsgViewDeny = "Sorry, View Permission Denied";

        public static string GStrMsgPerNotSet = "Sorry, Permission Not Set. Contact To System Administrator";

        public static string GStrMsgInsUpdDeny = "Sorry, Insert Or Update Permission Denied";

        public static string GStrMsgHeading = "";

        public static string GStrLatestExePath = "";

        public static bool GBlnFilTErr = false;
        public static bool islocal = true;
        public static string RConn = "";
        public static string RConnPass = "";


        #endregion
        public static int gIntMfgUnit = 0;
        public static int GIntPointer = 0;
        public static int pIntGatePassNo = 0;
        public static string gStrMfgUnitName = "";

        public static int gIntCCode = 0;
        public static string gIntCName = "";
        public static int gIntTrfCCode = 0;
        public static int gIntYCode = 0;
        public static int gIntCompStateCode = 24;
        public static string gStrAccEntDate = "";
        public static string gStrAccStartDate = "";
        public static string gStrAccEndDate = "";

        public static String gStrCmpNameYear = "";
        public static string gStrBillFilePath = "";
        public static DateTime? GStrServerDate = null;

        public static string GstrEmail_Default = "";
        public static string GStrEmail_SendList = "";
        public static string GStrEndUserGroup = String.Empty;


        public static string GstrStartDate = "";
        public static bool gBlnAutoAccSave = false;

        public static string MessageCaption = "Diamond Trading System";
        public static string GstrMsgDataNotFound = "Data Not Found !!";
        public static string GstrReportOpenPath = ""; 
        public static string GstrWPTemplateOpenPath = "";
        public static string GstrExportFilePath = "";





        public static string gStrDebuggerFound = "Close Your Debugger. Next Time Your System May be Crash.";
        public static string gStrCompcode = String.Empty;
    }
}

































