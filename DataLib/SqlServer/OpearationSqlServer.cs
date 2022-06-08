using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;

using GSql = DataLib.GlobalSql;
using GOpeSql = DataLib.OperationSql;
using GAdo = DataLib.GlobalADO;
using System.Security.Cryptography;
//using Val =  BusLib.Validation.BOValidation;


namespace DataLib
{
    /// <summary>
    /// Class For DataBase Operation [ Sql Server ]
    /// </summary>
    public class OperationSql : DataLib.GlobalSql
    {
        public static int GIntPointer = 0;

        public static DateTime? gDtServerExeModifyDate = null;


        public static string GStrMsgCaption = "Genesis Textile ERP System";

        public static string GStrInvalidDateMsg = "Entered date must be between the current financial period.";

        public static string GStrMsgDeleteDeny = "Sorry, Delete Permission Denied";

        public static string GStrMsgInsertDeny = "Sorry, Insert Permission Denied";

        public static string GStrMsgUpdateDeny = "You have not Permission For Update";

        public static string GStrMsgViewDeny = "Sorry, View Permission Denied";

        public static string GStrMsgPerNotSet = "Sorry, Permission Not Set. Contact To System Administrator";

        public static string GStrMsgInsUpdDeny = "Sorry, Insert Or Update Permission Denied";

        public static string GstrMsgDataNotFound = "Data Not Found !!";

        public static string GstrMsgInwardSuccess = "Slip Inward SuccessFull !!";

        public static string GStrMsgHeading = "Genesis Textile ERP System";
        public static string gStrCompName = "-";


        /// <summary>
        /// Set Current Culture Info
        /// </summary>
        public static System.Globalization.CultureInfo CultureInfoUS = new System.Globalization.CultureInfo("en-US", false);

        #region Enum Declaration...
        /// <summary>
        /// Enum For Setting Cell Activation State in UltraWinGrid
        /// </summary>
        public enum EnumCellActivation
        {
            /// <summary>
            /// Enum Active Only
            /// </summary>
            ActiveOnly = 1,
            /// <summary>
            /// Enum Allow Edit
            /// </summary>
            AllowEdit = 0,
            /// <summary>
            /// Enum Disable
            /// </summary>
            Disable = 2,
            /// <summary>
            /// Enum NoEdit
            /// </summary>
            NoEdit = 3
        }
        /// <summary>
        /// Enum For Setting Database Operation 
        /// </summary>
        public enum EnumOpeState
        {
            /// <summary>
            /// Enum For Select Record [Value = 0]
            /// </summary>
            Select = 0,
            /// <summary>
            /// Enum For Insert Record [Value = 1]
            /// </summary>
            Insert = 1,
            /// <summary>
            /// Enum For Update Record [Value = 2]
            /// </summary>
            Update = 2,
            /// <summary>
            /// Enum For Delete Record [Value = 3]
            /// </summary>
            Delete = 3
        }

        /// <summary>
        /// Enum : List Of Servers
        /// </summary>
        public enum EnumServer
        {
            /// <summary>
            /// Enum For Stock Server [Value = 0]
            /// </summary>
            ACC = 0,
        }

        public enum EnumSqlTran
        {
            Start = 0,
            Continue = 1,
            Stop = 2,
            None = 4
        }

        #endregion Enum Declaration...

        #region Utilities Like Password Encription,Reading Configuration File ....

        public static String GetDatabaseSetting()
        {
            String StrConnString = string.Empty;
            try
            {
                StrConnString = System.Configuration.ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;
            }
            catch
            {
                StrConnString = string.Empty;
            }
            return StrConnString;
        }

        public static Boolean OpenConnection()
        {
            try
            {
                ReadDatabaseSettingLocal();
                CCon(GSql.GConn);
                GSql.GConn = new SqlConnection(GSql.GConnString);
                if (OCon(GSql.GConn) == false) return false;
                GSql.GComm.Connection = GSql.GConn;
                if (GSql.DiamondDBName == "")
                {
                    System.Data.SqlClient.SqlConnectionStringBuilder SConStrBuilder = new SqlConnectionStringBuilder(GSql.GConnString);
                    // GSql.DiamondSalDBName = SConStrBuilder.InitialCatalog;
                    //GSql.DiamondSalServer = SConStrBuilder.DataSource;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Connect To [  ] Server");
                CCon(GSql.GConn);
                return false;
            }
            return true;
        }

        public static void SetConnectionStringADODB()
        {
            //GAdo.GConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DiamondAdodbConnectionString"].ConnectionString;
            string DatasrcEncrypt = "";
            if (GSql.islocal == true)
            {
                DatasrcEncrypt = Decrypt(System.Configuration.ConfigurationManager.AppSettings["LConn"]);
            }
            else
            {
                DatasrcEncrypt = Decrypt(GSql.RConn);
            }
            GAdo.GConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DiamondAdodbConnectionString"].ConnectionString + ";Data source=" + DatasrcEncrypt;
        }

        //public static bool ReadDatabaseSetting()
        //{
        //    string StrConnString = string.Empty;
        //    string str = "";
        //    str = GSql.RConn;


        //    StrConnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //    GSql.GConnString = StrConnString;
        //    return true;

        //}

        public static bool ReadDatabaseSettingLocal()
        {
            string StrConnString = string.Empty;
           
            StrConnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
             
            if (StrConnString.Length == 0)
            {
                GSql.GConnString = "";
                return false;
            }
            else
            {
                GSql.GConnString = StrConnString;
            }
            GAdo.GConnString = StrConnString;

            return true;
        }

        public static bool ReadDatabaseSettingAcYear(int pAccYear)
        {
            string empty = string.Empty;
            empty = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(empty);
            string empty2 = string.Empty;
            object obj = empty2;
            empty2 = obj + "Data Source=" + sqlConnectionStringBuilder.DataSource + ";Initial Catalog=" + sqlConnectionStringBuilder.InitialCatalog + pAccYear + ";User ID=" + sqlConnectionStringBuilder.UserID + " ;password=" + sqlConnectionStringBuilder.Password;
            if (empty.Length == 0)
            {
                GlobalSql.GConnString = "";
                return false;
            }
            GlobalSql.GConnString = empty2;
            string empty3 = string.Empty;
            obj = empty3;
            empty3 = obj + "Provider=SQLOLEDB.1;Persist Security Info = False;User ID=" + sqlConnectionStringBuilder.UserID + ";password=" + sqlConnectionStringBuilder.Password + ";Initial Catalog=" + sqlConnectionStringBuilder.InitialCatalog + pAccYear + ";Data Source=" + sqlConnectionStringBuilder.DataSource + ";";
            return true;
        }
    
   
        public static Boolean OpenSGConnection()
        {
            try
            {
                ReadSGDatabaseSetting();
                CCon(GSql.GSGConn);
                GSql.GSGConn = new SqlConnection(GSql.GSGConnString);
                if (OCon(GSql.GSGConn) == false) return false;
                GSql.GSGComm.Connection = GSql.GSGConn;
                if (GSql.DiamondDBName == "")
                {
                    System.Data.SqlClient.SqlConnectionStringBuilder SConStrBuilder = new SqlConnectionStringBuilder(GSql.GSGConnString);
                    // GSql.DiamondSalDBName = SConStrBuilder.InitialCatalog;
                    //GSql.DiamondSalServer = SConStrBuilder.DataSource;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Connect Server");
                CCon(GSql.GSGConn);
                return false;
            }
            return true;
        }
        public static bool ReadSGDatabaseSetting()
        {
            string StrConnString = string.Empty;

            StrConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DiamondAvgConnectionString"].ConnectionString;
            if (StrConnString.Length == 0)
            {
                GSql.GConnString = "";
                return false;
            }
            else
            {
                GSql.GSGConnString = StrConnString;
            }
            return true;


         
        }
        public static Boolean OpenRGConnection(string Str)
        {
            try
            {
                ReadRGDatabaseSetting(Str);
                CCon(GSql.GRGConn);
                GSql.GRGConn = new SqlConnection(GSql.GRGConnString);
                if (OCon(GSql.GRGConn) == false) return false;
                GSql.GRGComm.Connection = GSql.GRGConn;
                if (GSql.DiamondDBName == "")
                {
                    System.Data.SqlClient.SqlConnectionStringBuilder SConStrBuilder = new SqlConnectionStringBuilder(GSql.GRGConnString);
                    // GSql.DiamondSalDBName = SConStrBuilder.InitialCatalog;
                    //GSql.DiamondSalServer = SConStrBuilder.DataSource;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Connect Server");
                CCon(GSql.GRGConn);
                return false;
            }
            return true;
        }
        public static bool ReadRGDatabaseSetting(string Str)
        {
            string StrConnString = string.Empty;
            string StrConn = string.Empty;
            StrConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionRGString"].ConnectionString;
            StrConnString = "Initial Catalog= " + Str + ";" + StrConn;
            if (StrConnString.Length == 0)
            {
                GSql.GConnString = "";
                return false;
            }
            else
            {
                GSql.GRGConnString = StrConnString;
            }
            return true;
        }
   
        public static String ExNonQueryRetStr(EnumServer ServerConn, String pProcedureName, SqlParameter[] pParamList, String pStrOutParam)
        {
            OpenConnection(ServerConn);
            Int32 IntParams = pParamList.Length;
            GSql.GComm.Parameters.Clear();

            for (Int32 IntI = 0; IntI < pParamList.Length; IntI++)
            {
                if ((pParamList[IntI] != null)) GSql.GComm.Parameters.Add(pParamList[IntI]);
            }

            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                GSql.GComm.ExecuteNonQuery();
                return GSql.GComm.Parameters[pStrOutParam].Value == null ? "" : GSql.GComm.Parameters[pStrOutParam].Value.ToString();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }
            return "";
        }
      
        public static void FillDataSet(SqlConnection pConn, DataSet pDataSet, string pTableName, string pProcedureName, int IntType)
        {
            DataColumn[] DataColumnPrimaryKey;
            //OpenRemoteConnection();
            GSql.GComm.CommandText = pProcedureName;
            if (IntType == 0)
                GSql.GComm.CommandType = CommandType.StoredProcedure;
            else
                GSql.GComm.CommandType = CommandType.Text;
            GSql.GComm.Connection = pConn;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                    if (IntType == 1)
                        pDataSet.Tables.Remove(pTableName);
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
       
        public static string Decrypt(string cypherString)
        {
            try
            {
                string _strIp = string.Empty;
                string _strOneChar = string.Empty;
                for (int i = 0; i < cypherString.Length; i++)
                {

                    _strOneChar = cypherString.Substring(i, 1);
                    switch (_strOneChar)
                    {
                        case "!":
                            _strIp = _strIp + "0";
                            break;
                        case "@":
                            _strIp = _strIp + "9";
                            break;
                        case "#":
                            _strIp = _strIp + "8";
                            break;
                        case "$":
                            _strIp = _strIp + "7";
                            break;
                        case "%":
                            _strIp = _strIp + "6";
                            break;
                        case "^":
                            _strIp = _strIp + "5";
                            break;
                        case "~":
                            _strIp = _strIp + "4";
                            break;
                        case "*":
                            _strIp = _strIp + "3";
                            break;
                        case "(":
                            _strIp = _strIp + "2";
                            break;
                        case ")":
                            _strIp = _strIp + "1";
                            break;
                        case ">":
                            _strIp = _strIp + ".";
                            break;
                        default:
                            _strIp = _strIp + _strOneChar;
                            break;
                    }

                }

                return _strIp;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return string.Empty;
        }

        public static string Encrypt(string cypherString)
        {
            try
            {
                string _strIp = string.Empty;
                string _strOneChar = string.Empty;
                for (int i = 0; i < cypherString.Length; i++)
                {

                    _strOneChar = cypherString.Substring(i, 1);
                    switch (_strOneChar)
                    {
                        case "0":
                            _strIp = _strIp + "!";
                            break;
                        case "9":
                            _strIp = _strIp + "@";
                            break;
                        case "8":
                            _strIp = _strIp + "#";
                            break;
                        case "7":
                            _strIp = _strIp + "$";
                            break;
                        case "6":
                            _strIp = _strIp + "%";
                            break;
                        case "5":
                            _strIp = _strIp + "^";
                            break;
                        case "4":
                            _strIp = _strIp + "~";
                            break;
                        case "3":
                            _strIp = _strIp + "*";
                            break;
                        case "2":
                            _strIp = _strIp + "(";
                            break;
                        case "1":
                            _strIp = _strIp + ")";
                            break;
                        case ".":
                            _strIp = _strIp + ">";
                            break;
                        default:
                            _strIp = _strIp + _strOneChar;
                            break;

                    }

                }

                return _strIp;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return string.Empty;
        }
      
    
   
        public static bool ReadUserSetting()
        {
            String StrUId = string.Empty, StrPass = string.Empty;

            StrUId = System.Configuration.ConfigurationManager.AppSettings["UId"];
            if (StrUId.Length == 0)
            {
                GSql.GStrEndUserId = "";
                return false;
            }
            else
            {
                GSql.GStrEndUserId = StrUId;
            }

            StrPass = System.Configuration.ConfigurationManager.AppSettings["Pass"];
            if (StrPass.Length == 0)
            {
                GSql.GStrEndUserPass = "";
                return false;
            }
            else
            {
                GSql.GStrEndUserPass = StrPass;
            }

            return true;
        }

        
    

        /// <summary>
        /// Method for Encoding Or Decoding Given Password
        /// </summary>
        /// <param name="pStr">PassWord String </param>
        /// <param name="pStrToEncodeOrDecode"> String For Encode Or Decode [E-Encode,D-Decode]</param>
        /// <returns>String</returns>
        public static string ENCODE_DECODE(string pStr, string pStrToEncodeOrDecode)
        {
            int IntPos;
            string StrPass;
            string StrECode;
            string StrDCode;
            char ChrSingle;

            StrECode = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StrDCode = ")(*&^%$#@!";

            for (int IntLen = 1; IntLen <= 52; IntLen++)
            {
                StrDCode = StrDCode + (Char)(IntLen + 160);
            }

            StrPass = "";
            for (int IntCnt = 0; IntCnt <= pStr.Trim().Length - 1; IntCnt++)
            {
                ChrSingle = char.Parse(pStr.Substring(IntCnt, 1));
                if (pStrToEncodeOrDecode == "E")
                {
                    IntPos = StrECode.IndexOf(ChrSingle, 0);
                }
                else
                {
                    IntPos = StrDCode.IndexOf(ChrSingle, 0);
                }
                if (pStrToEncodeOrDecode == "E")
                {
                    StrPass = StrPass + StrDCode.Substring(IntPos, 1);
                }
                else
                {
                    StrPass = StrPass + StrECode.Substring(IntPos, 1);
                }
            }
            return StrPass;
        }


        #endregion

        #region ConnetionManupulation

        /// <summary>
        /// Connection Create As Well As Open It. 
        /// </summary>
        /// <param name="ServerCon">Database Name</param>
        /// <returns>True If Successful Else False</returns>
        public static Boolean OpenConnection(EnumServer ServerCon)
        {
            try
            {
                CCon(GSql.GConn);
                GSql.GConn = new SqlConnection(GSql.GConnString);
                if (OCon(GSql.GConn) == false) return false;
                GSql.GComm.Connection = GSql.GConn;
                if (GSql.DiamondDBName == string.Empty)
                {
                    System.Data.SqlClient.SqlConnectionStringBuilder SConStrBuilder = new SqlConnectionStringBuilder(GSql.GConnString);
                    GSql.DiamondDBName = SConStrBuilder.InitialCatalog;
                    GSql.DiamondDBPass = SConStrBuilder.Password;
                    GSql.DiamondDBUser = SConStrBuilder.UserID;
                    GSql.DiamondServer = SConStrBuilder.DataSource;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Connect To Server");
                CCon(GSql.GConn);
                return false;
            }
            return true;
        }

        public static Boolean OpenWebConnection(EnumServer ServerCon)
        {
            try
            {
                CCon(GSql.GWebConn);
                GSql.GWebConn = new SqlConnection(GSql.GWebConnString);
                if (OCon(GSql.GWebConn) == false) return false;
                GSql.GWebComm.Connection = GSql.GWebConn;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Connect To  Server");
                CCon(GSql.GWebConn);
                return false;
            }
            return true;
        }

        public static bool IsConnectionCheck(EnumServer ServerCon)
        {
            bool Result = false;
            switch (ServerCon)
            {
                case EnumServer.ACC:
                    try
                    {
                        CCon(GSql.GConn);
                        GSql.GConn = new SqlConnection(GSql.GConnString);
                        OCon(GSql.GConn);
                        Result = true;
                        CCon(GSql.GConn);
                    }
                    catch (Exception)
                    {
                        CCon(GSql.GConn);
                        Result = false;
                    }
                    break;
            }
            return Result;
        }

        /// <summary>
        /// Hash Table For Assigning Parameter Names And Values
        /// </summary>
        public static Hashtable HTParam = new Hashtable();
        /// <summary>
        ///Variable Use For Hash Table Parameter Count
        /// </summary>
        public static Int32 IntParamCount = 0;
        /// <summary>
        /// Method For Checking Connection State And Return True Or False
        /// </summary>
        /// <param name="pConn">SqlConnection</param>
        /// <returns>True Or False</returns>
        public static bool IsCon(SqlConnection pConn)
        {
            if (pConn.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region "Open Connection"
        /// <summary>
        /// Open A Connection Of Sql Server With given Connection
        /// </summary>
        /// <param name="pConn">SqlConnection</param>
        public static Boolean OCon(SqlConnection pConn)
        {
            if (pConn.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    pConn.Open();
                }
                catch (Exception Ex1)
                {

                    MessageBox.Show("Can't Connect To [ " + pConn.DataSource.ToUpper() + " ] Server  " + Ex1.Message);
                    CCon(pConn);
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Open A Connection Of Sql Server With given ConnectionString
        /// </summary>
        /// <param name="pConn">SqlConnection</param>
        /// <param name="pStrConn">ConenctionString</param>
        /// <param name="BlnOpenCon">Opening Conenction Flag</param>
        public static void OCon(SqlConnection pConn, string pStrConn, bool BlnOpenCon)
        {
            CCon(pConn);
            if (BlnOpenCon == true)
            {
                pConn.Open();
            }
            if (pConn.State == System.Data.ConnectionState.Closed)
            {
                pConn.Open();
            }
        }
        /// <summary>
        /// Open A Connection Of Sql Server With Given Criteria
        /// </summary>
        /// <param name="pConn">SqlConnection</param>
        /// <param name="pStrServer">Server Name</param>
        /// <param name="pStrDBName">DataBase Name</param>
        /// <param name="pStrDBUser">DataBase User Name</param>
        /// <param name="pStrDBPass">DataBase Password</param>
        public static void OCon(SqlConnection pConn, string pStrServer, string pStrDBName, string pStrDBUser, string pStrDBPass)
        {
            CCon(pConn);
            pConn.ConnectionString = "Data Source = " + pStrServer + "; Initial Catalog = " + pStrDBName + "; User Id = " + pStrDBUser + "; Password = " + pStrDBPass + "; Persist Security Info = True;";
            pConn.Open();
        }

        #endregion

        #region Close Connection

        /// <summary>
        /// Close Connetion Of SqlServer
        /// </summary>
        /// <param name="pConn">Name of Connection</param>
        public static void CCon(SqlConnection pConn)
        {
            if (pConn != null)
            {
                if (pConn.State == System.Data.ConnectionState.Open)
                {
                    pConn.Close();
                    pConn.Dispose();
                    pConn = null;
                }
            }
            else
            {
                pConn = null;
            }
        }
        public static void CCon(EnumServer ServerCon)
        {
            if (ServerCon == EnumServer.ACC)
            {

                if (GSql.GConn != null)
                {
                    if (GSql.GConn.State == System.Data.ConnectionState.Open)
                    {
                        GSql.GConn.Close();
                        GSql.GConn.Dispose();
                        GSql.GConn = null;
                    }
                }
                else
                {
                    GSql.GConn = null;
                }
            }
        }
        public static void CloseConnection(EnumServer ServerCon)
        {
            if (ServerCon == EnumServer.ACC)
            {

                if (GSql.GConn != null)
                {
                    if (GSql.GConn.State == System.Data.ConnectionState.Open)
                    {
                        GSql.GConn.Close();
                        GSql.GConn.Dispose();
                        GSql.GConn = null;
                    }
                }
                else
                {
                    GSql.GConn = null;
                }
            }
        }


        #endregion Close Connection
        #endregion

        #region Record Manupulation

        /// <summary> Update Current Row Using Stored  Proc (DataTable,DataSet,TableName,ExceptFields)
        /// Business DataAdapter for Dataset With Primary Key
        /// </summary>
        /// <param name="pTab">Data Table</param>
        /// <param name="pDataSet">Name Of DataSet</param>
        /// <param name="pStrTableName">DatBase Table Name</param>
        /// <param name="pStrExceptField">Fields To Skip Update</param>
        public static void UpdateGrid(EnumServer ServerConn, DataTable pTab, DataSet pDataSet, String pStrTableName, String pStrExceptField)
        {
            String StrAssign = "", StrCondition = "", StrCastStr = "", StrTable = "", StrCurCellValue = "";
            StringBuilder StrBuFields, StrBuValues;
            DataColumn DCCurCol;
            Hashtable HTTmp = new Hashtable();
            Object ObjCRowCol;
            if (pStrExceptField.Trim().Length != 0)
            {
                pStrExceptField = "," + pStrExceptField + ",";
            }

            if (pStrTableName + "" != "") StrTable = pStrTableName;
            else StrTable = pTab.TableName;

            if (!System.Convert.ToBoolean(pTab.GetChanges(DataRowState.Modified) == null))
            {
                StrAssign = "";
                for (Int32 IntCol = 0; IntCol < pTab.Columns.Count; IntCol++)
                {
                    DCCurCol = pTab.Columns[IntCol];
                    ObjCRowCol = pTab.Rows[0][IntCol];

                    //if (((DCCurCol.ColumnName.ToLower().Trim().IndexOf(pStrExceptField.ToLower().Trim(), 0)) == 0 && pStrExceptField.Length != 0) || (pTab.Rows[0][IntCol, DataRowVersion.Original].ToString() == pTab.Rows[0][IntCol, DataRowVersion.Current].ToString()))
                    if (((pStrExceptField.ToLower().Trim().Contains("," + DCCurCol.ColumnName.ToLower().Trim() + ",")) == true && pStrExceptField.Length != 0) || (pTab.Rows[0][IntCol, DataRowVersion.Original].ToString() == pTab.Rows[0][IntCol, DataRowVersion.Current].ToString()))
                        continue;

                    //if (!((DCCurCol.ColumnName.ToLower().Trim().IndexOf(pStrExceptField.ToLower().Trim(), 0)) == 0 && pStrExceptField.Length != 0))
                    if (((pStrExceptField.ToLower().Trim().Contains("," + DCCurCol.ColumnName.ToLower().Trim() + ",")) == false))
                    {
                        StrCurCellValue = ((String)(ObjCRowCol.ToString() + "")).Trim();

                        switch (DCCurCol.DataType.Name.ToLower())
                        {
                            case "string":
                                if (ObjCRowCol.GetType().ToString() == "System.DBNull")
                                {
                                    StrAssign += " [" + DCCurCol.ColumnName + "] = '' ,";
                                }
                                else
                                {
                                    StrCastStr = "";
                                    for (int IntI = 1; IntI <= StrCurCellValue.Length; IntI++)
                                    {
                                        StrCastStr = StrCastStr + (StrCurCellValue.Substring(IntI - 1, 1) == "'" ? "''" : StrCurCellValue.Substring(IntI - 1, 1));
                                    }
                                    StrAssign += " [" + DCCurCol.ColumnName + "] = '" + StrCastStr + "',";
                                }
                                break;

                            case "double":
                            case "decimal":
                            case "integer":
                            case "int64":
                            case "int32":
                            case "int16":
                            case "byte":
                                StrAssign += " [" + DCCurCol.ColumnName + "] = ";
                                if (StrCurCellValue == "")
                                {
                                    StrAssign += "0,";
                                }
                                else
                                {
                                    StrAssign += Convert.ToDouble(StrCurCellValue) + ",";
                                }
                                break;

                            case "datetime":
                                if (pTab.Rows[0][IntCol].GetType().ToString() == "System.DBNull" || StrCurCellValue.Length == 0)
                                {
                                    StrAssign += " [" + DCCurCol.ColumnName + "] = Null ,";
                                }
                                else
                                {
                                    if (Strings.Right(DCCurCol.ColumnName, 4).ToString() == "TIME")
                                    {
                                        StrAssign += " [" + DCCurCol.ColumnName + "] = '" + Strings.Format(DateTime.Parse(StrCurCellValue.ToString()), "MM/dd/yyyy hh:mm tt") + "',";
                                    }
                                    else
                                    {
                                        StrAssign += " [" + DCCurCol.ColumnName + "] = '" + SqlDate(StrCurCellValue.ToString()) + "',";
                                    }
                                }
                                break;

                            case "boolean":
                                if (pTab.Rows[0][IntCol].GetType().ToString() == "System.DBNull")
                                {
                                    StrAssign += " [" + DCCurCol.ColumnName + "] = Null ,";
                                }
                                else
                                {
                                    StrAssign += " [" + DCCurCol.ColumnName + "] = " + Interaction.IIf(StrCurCellValue.ToString().ToLower() == "true", 1, 0) + ",";
                                }
                                break;
                        }
                    }
                }
                if (StrAssign == "") return;
                GOpeSql.AddParams("TABLENAME", StrTable);

                StrAssign = StrAssign.Substring(0, StrAssign.Length - 1);
                GOpeSql.AddParams("FIELDS", StrAssign);

                StrCondition = PKGen(pTab);
                GOpeSql.AddParams("CRITERIA", StrCondition);

                try
                {
                    GOpeSql.ExNonQuery(ServerConn, "Usp_UPDATE", GOpeSql.GetParams());
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            else if (!System.Convert.ToBoolean(pTab.GetChanges(DataRowState.Added) == null))
            {
                StrBuFields = new StringBuilder();
                StrBuValues = new StringBuilder();
                HTTmp.Clear();

                for (Int32 IntCol = 0; IntCol <= pTab.Columns.Count - 1; IntCol++)
                {
                    //if ((pTab.Columns[IntCol].ColumnName.ToLower().Trim().IndexOf(pStrExceptField.ToLower().Trim(), 0)) == 0 && pStrExceptField.Length != 0)
                    if (((pStrExceptField.ToLower().Trim().Contains("," + pTab.Columns[IntCol].ColumnName.ToLower().Trim() + ",")) == true && pStrExceptField.Length != 0))
                    {
                    }
                    else
                    {
                        DCCurCol = pTab.Columns[IntCol];
                        ObjCRowCol = pTab.Rows[0][IntCol];

                        StrCurCellValue = (ObjCRowCol.ToString() + "");
                        StrCurCellValue = StrCurCellValue.Trim();

                        switch (DCCurCol.DataType.Name.ToLower())
                        {
                            case "string":
                                if (!(ObjCRowCol.GetType().ToString() == "System.DBNull" || ObjCRowCol.GetType().ToString() == ""))
                                {
                                    StrCastStr = "";
                                    for (Int32 IntI = 1; IntI <= StrCurCellValue.Length; IntI++)
                                    {
                                        StrCastStr = StrCastStr + (StrCurCellValue.Substring(IntI - 1, 1) == "'" ? "''" : StrCurCellValue.Substring(IntI - 1, 1));
                                    }
                                    HTTmp.Add("[" + DCCurCol.ColumnName + "]", " '" + StrCastStr + "' ");
                                }
                                break;

                            case "double":
                            case "decimal":
                            case "integer":
                            case "int32":
                            case "int64":
                            case "int16":
                            case "byte":
                                if (!(StrCurCellValue == ""))
                                {
                                    HTTmp.Add("[" + DCCurCol.ColumnName + "]", Convert.ToDouble(StrCurCellValue));
                                }
                                break;
                            case "datetime":
                                if (!(ObjCRowCol.GetType().ToString() == "System.DBNull" || StrCurCellValue.Length == 0))
                                {
                                    HTTmp.Add("[" + DCCurCol.ColumnName + "]", " '" + Strings.Format(DateTime.Parse(StrCurCellValue.ToString()), "MM/dd/yyyy") + "' ");
                                }
                                break;
                            case "boolean":
                                if (!(ObjCRowCol.GetType().ToString() == "System.DBNull"))
                                {
                                    HTTmp.Add(DCCurCol.ColumnName, Interaction.IIf(StrCurCellValue.ToString().ToLower() == "true", 1, 0));
                                }
                                break;
                        }
                    }
                }
                foreach (DictionaryEntry DE in HTTmp)
                {
                    StrBuFields.Append(DE.Key.ToString() + ",");
                    StrBuValues.Append(DE.Value.ToString() + ",");
                }

                GOpeSql.AddParams("TABLENAME", StrTable);
                GOpeSql.AddParams("FIELDS", StrBuFields.ToString().Substring(0, StrBuFields.Length - 1));
                GOpeSql.AddParams("VALUES", StrBuValues.ToString().Substring(0, StrBuValues.Length - 1));

                try
                {
                    GOpeSql.ExNonQuery(ServerConn, "Usp_INSERT", GOpeSql.GetParams());
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            pDataSet.AcceptChanges();
        }


        private static int UpdateGrid(EnumServer ServerConn, DataTable pTab, DataSet pDataSet, string pStrTableName, string pStrExceptField, bool pBlnDataSetCommit)
        {
            string text = "";
            string text2 = "";
            string text3 = "";
            string text4 = "";
            string text5 = "";
            string text6 = "";
            string text7 = "";
            Hashtable hashtable = new Hashtable();
            int num = 0;
            string[] array = (!string.IsNullOrEmpty(pStrExceptField)) ? pStrExceptField.ToLower().Split(',') : new string[0];
            text5 = ((!((pStrTableName ?? "") != "")) ? pTab.TableName : pStrTableName);
            if (!Convert.ToBoolean(pTab.GetChanges(DataRowState.Modified) == null))
            {
                text = "";
                foreach (DataRow row in pTab.GetChanges(DataRowState.Modified).Rows)
                {
                    hashtable.Clear();
                    for (int i = 0; i < pTab.Columns.Count; i++)
                    {
                        DataColumn dataColumn = pTab.Columns[i];
                        object obj = row[i];
                        if (Array.IndexOf(array, dataColumn.ColumnName.ToLower().Trim()) <= -1 && !(row[i, DataRowVersion.Original].ToString() == row[i, DataRowVersion.Current].ToString()) && (dataColumn.ColumnName.ToLower().Trim().IndexOf(pStrExceptField.ToLower().Trim(), 0) != 0 || pStrExceptField.Length == 0))
                        {
                            text6 = (obj.ToString() ?? "").Trim();
                            switch (dataColumn.DataType.Name.ToLower())
                            {
                                case "string":
                                    if (obj.GetType().ToString() == "System.DBNull")
                                    {
                                        text = text + " [" + dataColumn.ColumnName + "] = '' ,";
                                    }
                                    else
                                    {
                                        text4 = "";
                                        for (int j = 1; j <= text6.Length; j++)
                                        {
                                            text4 += ((text6.Substring(j - 1, 1) == "'") ? "''" : text6.Substring(j - 1, 1));
                                        }
                                        string text8 = text;
                                        text = text8 + " [" + dataColumn.ColumnName + "] = '" + text4 + "',";
                                    }
                                    break;
                                case "double":
                                case "decimal":
                                case "integer":
                                case "int64":
                                case "int32":
                                case "int16":
                                case "byte":
                                    text = text + " [" + dataColumn.ColumnName + "] = ";
                                    text = ((!(text6 == "")) ? (text + Convert.ToDouble(text6) + ",") : (text + "0,"));
                                    break;
                                case "datetime":
                                    if (pTab.Rows[0][i].GetType().ToString() == "System.DBNull" || text6.Length == 0)
                                    {
                                        text = text + " [" + dataColumn.ColumnName + "] = Null ,";
                                    }
                                    else if (Strings.Right(dataColumn.ColumnName, 4).ToString() == "TIME")
                                    {
                                        string text8 = text;
                                        text = text8 + " [" + dataColumn.ColumnName + "] = '" + Strings.Format(DateTime.Parse(text6.ToString()), "MM/dd/yyyy hh:mm tt") + "',";
                                    }
                                    else
                                    {
                                        string text8 = text;
                                        text = text8 + " [" + dataColumn.ColumnName + "] = '" + SqlDate(text6.ToString()) + "',";
                                    }
                                    break;
                                case "boolean":
                                    if (pTab.Rows[0][i].GetType().ToString() == "System.DBNull")
                                    {
                                        text = text + " [" + dataColumn.ColumnName + "] = Null ,";
                                    }
                                    else
                                    {
                                        object obj2 = text;
                                        text = obj2 + " [" + dataColumn.ColumnName + "] = " + Interaction.IIf(text6.ToString().ToLower() == "true", 1, 0) + ",";
                                    }
                                    break;
                            }
                        }
                    }
                    if (!(text == ""))
                    {
                        AddParams("TABLENAME", text5);
                        text = text.Substring(0, text.Length - 1);
                        AddParams("FIELDS", text);
                        text2 = PKGen(pTab, row);
                        AddParams("CRITERIA", text2);
                        text = "";
                        text2 = "";
                        try
                        {
                            num = ExNonQuery(ServerConn, "SP_Update ", GetParams());
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Class == 18 && ex.State == 1)
                            {
                                throw ex;
                            }
                            MessageBox.Show(ex.Message);
                            num = -1;
                        }
                    }
                }
            }
            else if (!Convert.ToBoolean(pTab.GetChanges(DataRowState.Added) == null))
            {
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();
                hashtable.Clear();
                foreach (DataRow row2 in pTab.GetChanges(DataRowState.Added).Rows)
                {
                    hashtable.Clear();
                    for (int i = 0; i <= pTab.Columns.Count - 1; i++)
                    {
                        if (Array.IndexOf(array, pTab.Columns[i].ColumnName.ToLower().Trim()) <= -1)
                        {
                            DataColumn dataColumn = pTab.Columns[i];
                            object obj = row2[i];
                            text6 = (obj.ToString() ?? "");
                            text6 = text6.Trim();
                            switch (dataColumn.DataType.Name.ToLower())
                            {
                                case "string":
                                    if (!(obj.GetType().ToString() == "System.DBNull") && !(obj.GetType().ToString() == ""))
                                    {
                                        text4 = "";
                                        for (int j = 1; j <= text6.Length; j++)
                                        {
                                            text4 += ((text6.Substring(j - 1, 1) == "'") ? "''" : text6.Substring(j - 1, 1));
                                        }
                                        hashtable.Add("[" + dataColumn.ColumnName + "]", " '" + text4 + "' ");
                                    }
                                    break;
                                case "double":
                                case "decimal":
                                case "integer":
                                case "int32":
                                case "int64":
                                case "int16":
                                case "byte":
                                    if (!(text6 == ""))
                                    {
                                        hashtable.Add("[" + dataColumn.ColumnName + "]", Convert.ToDouble(text6));
                                    }
                                    break;
                                case "datetime":
                                    if (!(obj.GetType().ToString() == "System.DBNull") && text6.Length != 0)
                                    {
                                        hashtable.Add("[" + dataColumn.ColumnName + "]", " '" + Strings.Format(DateTime.Parse(text6.ToString()), "MM/dd/yyyy") + "' ");
                                    }
                                    break;
                                case "boolean":
                                    if (!(obj.GetType().ToString() == "System.DBNull"))
                                    {
                                        hashtable.Add(dataColumn.ColumnName, Interaction.IIf(text6.ToString().ToLower() == "true", 1, 0));
                                    }
                                    break;
                            }
                        }
                    }
                    foreach (DictionaryEntry item in hashtable)
                    {
                        stringBuilder.Append(item.Key.ToString() + ",");
                        stringBuilder2.Append(item.Value.ToString() + ",");
                    }
                    AddParams("TABLENAME", text5);
                    AddParams("FIELDS", stringBuilder.ToString().Substring(0, stringBuilder.Length - 1));
                    AddParams("VALUES", stringBuilder2.ToString().Substring(0, stringBuilder2.Length - 1));
                    stringBuilder.Clear();
                    stringBuilder2.Clear();
                    try
                    {
                        num = ExNonQuery(ServerConn, "SP_Insert", GetParams());
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Class == 18 && ex.State == 1)
                        {
                            throw ex;
                        }
                        MessageBox.Show(ex.Message);
                        num = -1;
                    }
                }
            }
            if (pBlnDataSetCommit)
            {
                if (num == -1)
                {
                    pDataSet.RejectChanges();
                }
                else
                {
                    pDataSet.AcceptChanges();
                }
            }
            return num;
        }

        private static int UpdateGrid(EnumServer ServerConn, DataTable pTab, DataSet pDataSet, string pStrTableName, string pStrExceptField, bool pBlnDataSetCommit, EnumSqlTran pSQLTransaction)
        {
            string text = "";
            string text2 = "";
            string text3 = "";
            string text4 = "";
            string text5 = "";
            string text6 = "";
            string text7 = "";
            Hashtable hashtable = new Hashtable();
            int num = 0;
            string[] array = (!string.IsNullOrEmpty(pStrExceptField)) ? pStrExceptField.ToLower().Split(',') : new string[0];
            text5 = ((!((pStrTableName ?? "") != "")) ? pTab.TableName : pStrTableName);
            if (!Convert.ToBoolean(pTab.GetChanges(DataRowState.Modified) == null))
            {
                text = "";
                foreach (DataRow row in pTab.GetChanges(DataRowState.Modified).Rows)
                {
                    hashtable.Clear();
                    for (int i = 0; i < pTab.Columns.Count; i++)
                    {
                        DataColumn dataColumn = pTab.Columns[i];
                        object obj = row[i];
                        if (Array.IndexOf(array, dataColumn.ColumnName.ToLower().Trim()) <= -1 && !(row[i, DataRowVersion.Original].ToString() == row[i, DataRowVersion.Current].ToString()) && (dataColumn.ColumnName.ToLower().Trim().IndexOf(pStrExceptField.ToLower().Trim(), 0) != 0 || pStrExceptField.Length == 0))
                        {
                            text6 = (obj.ToString() ?? "").Trim();
                            switch (dataColumn.DataType.Name.ToLower())
                            {
                                case "string":
                                    if (obj.GetType().ToString() == "System.DBNull")
                                    {
                                        text = text + " [" + dataColumn.ColumnName + "] = '' ,";
                                    }
                                    else
                                    {
                                        text4 = "";
                                        for (int j = 1; j <= text6.Length; j++)
                                        {
                                            text4 += ((text6.Substring(j - 1, 1) == "'") ? "''" : text6.Substring(j - 1, 1));
                                        }
                                        string text8 = text;
                                        text = text8 + " [" + dataColumn.ColumnName + "] = '" + text4 + "',";
                                    }
                                    break;
                                case "double":
                                case "decimal":
                                case "integer":
                                case "int64":
                                case "int32":
                                case "int16":
                                case "byte":
                                    text = text + " [" + dataColumn.ColumnName + "] = ";
                                    text = ((!(text6 == "")) ? (text + Convert.ToDouble(text6) + ",") : (text + "0,"));
                                    break;
                                case "datetime":
                                    if (pTab.Rows[0][i].GetType().ToString() == "System.DBNull" || text6.Length == 0)
                                    {
                                        text = text + " [" + dataColumn.ColumnName + "] = Null ,";
                                    }
                                    else if (Strings.Right(dataColumn.ColumnName, 4).ToString() == "TIME")
                                    {
                                        string text8 = text;
                                        text = text8 + " [" + dataColumn.ColumnName + "] = '" + Strings.Format(DateTime.Parse(text6.ToString()), "MM/dd/yyyy hh:mm tt") + "',";
                                    }
                                    else
                                    {
                                        string text8 = text;
                                        text = text8 + " [" + dataColumn.ColumnName + "] = '" + SqlDate(text6.ToString()) + "',";
                                    }
                                    break;
                                case "boolean":
                                    if (pTab.Rows[0][i].GetType().ToString() == "System.DBNull")
                                    {
                                        text = text + " [" + dataColumn.ColumnName + "] = Null ,";
                                    }
                                    else
                                    {
                                        object obj2 = text;
                                        text = obj2 + " [" + dataColumn.ColumnName + "] = " + Interaction.IIf(text6.ToString().ToLower() == "true", 1, 0) + ",";
                                    }
                                    break;
                            }
                        }
                    }
                    if (!(text == ""))
                    {
                        AddParams("TABLENAME", text5);
                        text = text.Substring(0, text.Length - 1);
                        AddParams("FIELDS", text);
                        text2 = PKGen(pTab, row);
                        AddParams("CRITERIA", text2);
                        text = "";
                        text2 = "";
                        try
                        {
                            num = ExNonQuery(ServerConn, "SP_Update ", GetParams(), pSQLTransaction);
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Class == 18 && ex.State == 1)
                            {
                                throw ex;
                            }
                            MessageBox.Show(ex.Message);
                            num = -1;
                        }
                    }
                }
            }
            else if (!Convert.ToBoolean(pTab.GetChanges(DataRowState.Added) == null))
            {
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();
                hashtable.Clear();
                foreach (DataRow row2 in pTab.GetChanges(DataRowState.Added).Rows)
                {
                    hashtable.Clear();
                    for (int i = 0; i <= pTab.Columns.Count - 1; i++)
                    {
                        if (Array.IndexOf(array, pTab.Columns[i].ColumnName.ToLower().Trim()) <= -1)
                        {
                            DataColumn dataColumn = pTab.Columns[i];
                            object obj = row2[i];
                            text6 = (obj.ToString() ?? "");
                            text6 = text6.Trim();
                            switch (dataColumn.DataType.Name.ToLower())
                            {
                                case "string":
                                    if (!(obj.GetType().ToString() == "System.DBNull") && !(obj.GetType().ToString() == ""))
                                    {
                                        text4 = "";
                                        for (int j = 1; j <= text6.Length; j++)
                                        {
                                            text4 += ((text6.Substring(j - 1, 1) == "'") ? "''" : text6.Substring(j - 1, 1));
                                        }
                                        hashtable.Add("[" + dataColumn.ColumnName + "]", " '" + text4 + "' ");
                                    }
                                    break;
                                case "double":
                                case "decimal":
                                case "integer":
                                case "int32":
                                case "int64":
                                case "int16":
                                case "byte":
                                    if (!(text6 == ""))
                                    {
                                        hashtable.Add("[" + dataColumn.ColumnName + "]", Convert.ToDouble(text6));
                                    }
                                    break;
                                case "datetime":
                                    if (!(obj.GetType().ToString() == "System.DBNull") && text6.Length != 0)
                                    {
                                        if (obj.ToString().Substring(0, 10) == "01/01/0001" || obj.ToString().Substring(0, 10) == "01/01/1900")
                                        {
                                            hashtable.Add("[" + dataColumn.ColumnName + "]", " '" + DateTime.Parse(text6.ToString()).ToString("01/01/1900 hh:mm tt") + "' ");
                                        }
                                        else
                                        {
                                            hashtable.Add("[" + dataColumn.ColumnName + "]", " '" + Strings.Format(DateTime.Parse(text6.ToString()), "MM/dd/yyyy") + "' ");
                                        }
                                    }
                                    break;
                                case "boolean":
                                    if (!(obj.GetType().ToString() == "System.DBNull"))
                                    {
                                        hashtable.Add(dataColumn.ColumnName, Interaction.IIf(text6.ToString().ToLower() == "true", 1, 0));
                                    }
                                    break;
                            }
                        }
                    }
                    foreach (DictionaryEntry item in hashtable)
                    {
                        stringBuilder.Append(item.Key.ToString() + ",");
                        stringBuilder2.Append(item.Value.ToString() + ",");
                    }
                    AddParams("TABLENAME", text5);
                    AddParams("FIELDS", stringBuilder.ToString().Substring(0, stringBuilder.Length - 1));
                    AddParams("VALUES", stringBuilder2.ToString().Substring(0, stringBuilder2.Length - 1));
                    stringBuilder.Clear();
                    stringBuilder2.Clear();
                    try
                    {
                        num = ExNonQuery(ServerConn, "SP_Insert", GetParams(), pSQLTransaction);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Class == 18 && ex.State == 1)
                        {
                            throw ex;
                        }
                        MessageBox.Show(ex.Message);
                        num = -1;
                    }
                }
            }
            if (pBlnDataSetCommit)
            {
                if (num == -1)
                {
                    pDataSet.RejectChanges();
                }
                else
                {
                    pDataSet.AcceptChanges();
                }
            }
            return num;
        }

        /// <summary> Delete Current Row Using Stored Procedure (DataTable,DataSet,TableName)
        /// Business DataAdapter for Dataset Without Parameter Connetion With Primary Key
        /// </summary>
        /// <param name="pTab">Data Table </param>
        /// <param name="pDataSet">Name Of DataSet</param>
        /// <param name="pStrTableName">DataBase Table Name</param>
        public static void DeleteGrid(EnumServer ServerConn, DataTable pTab, DataSet pDataSet, string pStrTableName)
        {
            String StrTable = ""; ;
            DataTable TabDeletedRows = pTab.GetChanges(DataRowState.Deleted);

            if (pStrTableName + "" != "") StrTable = pStrTableName;
            else StrTable = pTab.TableName;

            GOpeSql.AddParams("TABLENAME", StrTable);
            GOpeSql.AddParams("CRITERIA", PKGen(TabDeletedRows));

            try
            {
                GOpeSql.ExNonQuery(ServerConn, "Usp_Delete", GOpeSql.GetParams());
                pTab.AcceptChanges();
                pDataSet.AcceptChanges();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        public static void DeleteGrid(EnumServer ServerConn, DataTable pTab, DataSet pDataSet, string pStrTableName, EnumSqlTran pSQLTransaction)
        {
            string text = "";
            DataTable changes = pTab.GetChanges(DataRowState.Deleted);
            if (changes != null)
            {
                text = ((!((pStrTableName ?? "") != "")) ? pTab.TableName : pStrTableName);
                foreach (DataRow row in changes.Rows)
                {
                    AddParams("TABLENAME", text);
                    AddParams("CRITERIA", PKGen(changes, row));
                    try
                    {
                        ExNonQuery(ServerConn, "SP_Delete", GetParams(), pSQLTransaction);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Class == 18 && ex.State == 1)
                        {
                            throw ex;
                        }
                        MessageBox.Show(ex.ToString());
                    }
                }
                changes.AcceptChanges();
            }
        }

        public static void InsUpdGrid(EnumServer ServerConn, DataTable pTab, DataSet pDataSet, String pStrTableName, String pStrExceptField)
        {
            String StrAssign = "", StrCondition = "", StrCastStr = "", StrTable = "", StrCurCellValue = "";
            StringBuilder StrBuFields, StrBuValues;
            DataColumn DCCurCol;
            Hashtable HTTmp = new Hashtable();
            Object ObjCRowCol;
            StrBuFields = new StringBuilder();
            StrBuValues = new StringBuilder();
            if (pStrTableName + "" != "") StrTable = pStrTableName;
            else StrTable = pTab.TableName;
            if (pStrExceptField.Trim().Length != 0)
            {
                pStrExceptField = "," + pStrExceptField + ",";
            }
            if (!System.Convert.ToBoolean(pTab.GetChanges(DataRowState.Modified) == null))
            {
                StrAssign = "";
                for (Int32 IntCol = 0; IntCol < pTab.Columns.Count; IntCol++)
                {
                    DCCurCol = pTab.Columns[IntCol];
                    ObjCRowCol = pTab.Rows[0][IntCol];

                    //if (((DCCurCol.ColumnName.ToLower().Trim().IndexOf(pStrExceptField.ToLower().Trim(), 0)) == 0 && pStrExceptField.Length != 0))
                    if (((pStrExceptField.ToLower().Trim().Contains("," + DCCurCol.ColumnName.ToLower().Trim() + ",")) == true && pStrExceptField.Length != 0))
                        //if (((DCCurCol.ColumnName.ToLower().Trim().IndexOf(pStrExceptField.ToLower().Trim(), 0)) == 0 && pStrExceptField.Length != 0) || (pTab.Rows[0][IntCol, DataRowVersion.Original].ToString() == pTab.Rows[0][IntCol, DataRowVersion.Current].ToString()))
                        continue;

                    //if (!((DCCurCol.ColumnName.ToLower().Trim().IndexOf(pStrExceptField.ToLower().Trim(), 0)) == 0 && pStrExceptField.Length != 0))
                    //{
                    if ((pTab.Columns[IntCol].ColumnName.ToLower().Trim().IndexOf(pStrExceptField.ToLower().Trim(), 0)) == 0 && pStrExceptField.Length != 0)
                    {
                    }
                    else
                    {
                        StrCurCellValue = ((String)(ObjCRowCol.ToString() + "")).Trim();

                        switch (DCCurCol.DataType.Name.ToLower())
                        {
                            case "string":
                                if (ObjCRowCol.GetType().ToString() == "System.DBNull")
                                {
                                    StrAssign += " [" + DCCurCol.ColumnName + "] = '' ,";
                                }
                                else
                                {
                                    StrCastStr = "";
                                    for (int IntI = 1; IntI <= StrCurCellValue.Length; IntI++)
                                    {
                                        StrCastStr = StrCastStr + (StrCurCellValue.Substring(IntI - 1, 1) == "'" ? "''" : StrCurCellValue.Substring(IntI - 1, 1));
                                    }
                                    StrAssign += " [" + DCCurCol.ColumnName + "] = '" + StrCastStr + "',";
                                    HTTmp.Add("[" + DCCurCol.ColumnName + "]", " '" + StrCastStr + "' ");
                                }
                                break;

                            case "double":
                            case "decimal":
                            case "integer":
                            case "int64":
                            case "int32":
                            case "int16":
                            case "byte":
                                StrAssign += " [" + DCCurCol.ColumnName + "] = ";
                                if (StrCurCellValue == "")
                                {
                                    StrAssign += "0,";
                                }
                                else
                                {
                                    StrAssign += Convert.ToDouble(StrCurCellValue) + ",";
                                    HTTmp.Add("[" + DCCurCol.ColumnName + "]", Convert.ToDouble(StrCurCellValue));
                                }
                                break;

                            case "datetime":
                                if (pTab.Rows[0][IntCol].GetType().ToString() == "System.DBNull" || StrCurCellValue.Length == 0)
                                {
                                    StrAssign += " [" + DCCurCol.ColumnName + "] = Null ,";
                                }
                                else
                                {
                                    if (Strings.Right(DCCurCol.ColumnName, 4).ToString() == "TIME")
                                    {
                                        StrAssign += " [" + DCCurCol.ColumnName + "] = '" + Strings.Format(DateTime.Parse(StrCurCellValue.ToString()), "MM/dd/yyyy hh:mm tt") + "',";
                                        //HTTmp.Add("[" + DCCurCol.ColumnName + "]", " '" + Strings.Format(DateTime.Parse(StrCurCellValue.ToString()), "MM/dd/yyyy") + "' ");
                                    }
                                    else
                                    {
                                        StrAssign += " [" + DCCurCol.ColumnName + "] = '" + SqlDate(StrCurCellValue.ToString()) + "',";
                                        HTTmp.Add("[" + DCCurCol.ColumnName + "]", " '" + Strings.Format(DateTime.Parse(StrCurCellValue.ToString()), "MM/dd/yyyy") + "' ");
                                    }
                                }
                                break;

                            case "boolean":
                                if (pTab.Rows[0][IntCol].GetType().ToString() == "System.DBNull")
                                {
                                    StrAssign += " [" + DCCurCol.ColumnName + "] = Null ,";
                                }
                                else
                                {
                                    StrAssign += " [" + DCCurCol.ColumnName + "] = " + Interaction.IIf(StrCurCellValue.ToString().ToLower() == "true", 1, 0) + ",";
                                    HTTmp.Add(DCCurCol.ColumnName, Interaction.IIf(StrCurCellValue.ToString().ToLower() == "true", 1, 0));
                                }
                                break;
                        }
                    }
                }

                foreach (DictionaryEntry DE in HTTmp)
                {
                    StrBuFields.Append(DE.Key.ToString() + ",");
                    StrBuValues.Append(DE.Value.ToString() + ",");
                }

                if (StrAssign == "") return;
                GOpeSql.AddParams("TABLENAME", StrTable);

                StrAssign = StrAssign.Substring(0, StrAssign.Length - 1);
                GOpeSql.AddParams("FIELDS", StrAssign);

                StrCondition = PKGen(pTab);
                GOpeSql.AddParams("CRITERIA", StrCondition);

                GOpeSql.AddParams("INSFIELDS", StrBuFields.ToString().Substring(0, StrBuFields.Length - 1));
                GOpeSql.AddParams("VALUES", StrBuValues.ToString().Substring(0, StrBuValues.Length - 1));


                try
                {
                    GOpeSql.ExNonQuery(ServerConn, "SP_INSUPD", GOpeSql.GetParams());
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            pDataSet.AcceptChanges();
        }
        /// <summary> Generate Primary Keys(DataTable)
        /// Business DataAdapter for Dataset Without Parameter Connetion With Primary Key
        /// </summary>
        /// <param name="pTab">Data Table </param>
        private static string PKGen(DataTable pTab)
        {
            string Str = "";
            foreach (DataColumn DataColumnPrimaryKey in pTab.PrimaryKey)
            {
                switch (DataColumnPrimaryKey.DataType.Name.ToLower())
                {
                    case "string":
                        Str = Str + " And [" + DataColumnPrimaryKey.ColumnName + "] = '" + pTab.Rows[0][DataColumnPrimaryKey.ColumnName, DataRowVersion.Original] + "' ";
                        break;
                    case "double":
                    case "decimal":
                    case "integer":
                    case "int32":
                    case "int64":
                    case "int16":
                        Str = Str + " And [" + DataColumnPrimaryKey.ColumnName + "] = " + pTab.Rows[0][DataColumnPrimaryKey.ColumnName, DataRowVersion.Original];
                        break;
                    case "datetime":
                        if (pTab.Rows[0][DataColumnPrimaryKey.ColumnName, DataRowVersion.Original].GetType().ToString() == "System.DBNull")
                        {
                            Str = Str + " And [" + DataColumnPrimaryKey.ColumnName + "] = Null ";
                        }
                        else
                        {
                            Str = Str + " And [" + DataColumnPrimaryKey.ColumnName + "] = '" + SqlDate(pTab.Rows[0][DataColumnPrimaryKey.ColumnName, DataRowVersion.Original].ToString()) + "'";
                        }
                        break;
                    case "boolean":

                        if (pTab.Rows[0][DataColumnPrimaryKey.ColumnName, DataRowVersion.Original].GetType().ToString() == "System.DBNull")
                        {
                            Str = Str + " And [" + DataColumnPrimaryKey.ColumnName + "] = Null ";
                        }
                        else
                        {
                            Str = Str + " And [" + DataColumnPrimaryKey.ColumnName + "] = " + System.Convert.ToInt32(pTab.Rows[0][DataColumnPrimaryKey.ColumnName, DataRowVersion.Original]);
                        }
                        break;
                }
            }
           return Str;
        }

        private static string PKGen(DataTable pTab, DataRow pDRow)
        {
            string text = "";
            DataColumn[] primaryKey = pTab.PrimaryKey;
            foreach (DataColumn dataColumn in primaryKey)
            {
                switch (dataColumn.DataType.Name.ToLower())
                {
                    case "string":
                        text = text + " And [" + dataColumn.ColumnName + "] = '" + pDRow[dataColumn.ColumnName, DataRowVersion.Original] + "' ";
                        break;
                    case "double":
                    case "decimal":
                    case "integer":
                    case "int32":
                    case "int64":
                    case "int16":
                        text = text + " And [" + dataColumn.ColumnName + "] = " + pDRow[dataColumn.ColumnName, DataRowVersion.Original];
                        break;
                    case "datetime":
                        text = ((!(pDRow[dataColumn.ColumnName, DataRowVersion.Original].GetType().ToString() == "System.DBNull")) ? (text + " And [" + dataColumn.ColumnName + "] = '" + SqlDate(pDRow[dataColumn.ColumnName, DataRowVersion.Original].ToString()) + "'") : (text + " And [" + dataColumn.ColumnName + "] = Null "));
                        break;
                    case "boolean":
                        text = ((!(pDRow[dataColumn.ColumnName, DataRowVersion.Original].GetType().ToString() == "System.DBNull")) ? (text + " And [" + dataColumn.ColumnName + "] = " + Convert.ToInt32(pDRow[dataColumn.ColumnName, DataRowVersion.Original])) : (text + " And [" + dataColumn.ColumnName + "] = Null "));
                        break;
                }
            }
            return text;
        }
        public static void AddParams(Hashtable Ht)
        {
            HTParam = Ht;
            IntParamCount = Ht.Count;
        }
        public static void AddParams(String pStrKey, String pStrVal)
        {
            HTParam.Add(pStrKey, pStrVal);
            IntParamCount++;
        }
        public static void AddParams(string pStrKey, bool? pBlnVal)
        {
            HTParam.Add(pStrKey, pBlnVal);
            IntParamCount++;
        }
        public static void AddParams(String pStrKey, Int16 pIntVal)
        {
            HTParam.Add(pStrKey, pIntVal);
            IntParamCount++;
        }


        public static void AddParams(String pStrKey, Int32 pIntVal)
        {
            HTParam.Add(pStrKey, pIntVal);
            IntParamCount++;
        }

        public static void AddParams(String pStrKey, Int32? pIntVal)
        {
            HTParam.Add(pStrKey, pIntVal);
            IntParamCount++;
        }



        /// <summary> Add ParaMeters To HashTable (Key,Value)
        /// Method For Adding Parameters To HashTable With Key And Vaue
        /// </summary>
        /// <param name="pStrKey">Parameter Name</param>
        /// <param name="pIntVal">Parameter Value</param>
        public static void AddParams(String pStrKey, Int64 pIntVal)
        {
            HTParam.Add(pStrKey, pIntVal);
            IntParamCount++;
        }

        /// <summary> Add ParaMeters To HashTable (Key,Value)
        /// Method For Adding Parameters To HashTable With Key And Vaue
        /// </summary>
        /// <param name="pStrKey">Parameter Name</param>
        /// <param name="pDblVal">Parameter Value</param>
        public static void AddParams(String pStrKey, double pDblVal)
        {
            HTParam.Add(pStrKey, pDblVal);
            IntParamCount++;
        }

        public static void AddParams(String pStrKey, decimal pDecVal)
        {
            HTParam.Add(pStrKey, pDecVal);
            IntParamCount++;
        }

        public static void AddParams(String pStrKey, decimal? pDecVal)
        {
            HTParam.Add(pStrKey, pDecVal);
            IntParamCount++;
        }

        /// <summary> Add ParaMeters To HashTable (Key,Value)
        /// Method For Adding Parameters To HashTable With Key And Vaue
        /// </summary>
        /// <param name="pStrKey">Parameter Name</param>
        /// <param name="pImage">Parameter Image</param>
        public static void AddParams(String pStrKey, byte[] pImage)
        {
            HTParam.Add(pStrKey, pImage);
            IntParamCount++;
        }

        /// <summary> Add ParaMeters To HashTable (Key,Value)
        /// Method For Adding Parameters To HashTable With Key And Vaue
        /// </summary>
        /// <param name="pStrKey"></param>
        /// <param name="pDatTime"></param>
        public static void AddParams(String pStrKey, DateTime? pDatTime)
        {
            if (pDatTime == null || pDatTime.ToString() == "")
            {
                HTParam.Add(pStrKey, System.DBNull.Value);
            }
            else
            {
                if (pDatTime.Value.ToShortDateString() == "01/01/0001" || pDatTime.Value.ToShortDateString() == "01/01/01")
                {
                    HTParam.Add(pStrKey, DTDBTime(pDatTime).Value.ToString("hh:mm tt"));
                }
                else
                {
                    HTParam.Add(pStrKey, DTDBDate(pDatTime).Value.ToString("MM/dd/yyyy"));
                }
            }
            IntParamCount++;
        }

        /// <summary> Add ParaMeters To HashTable (Key,Value)
        /// Method For Adding Parameters To HashTable With Key And Vaue
        /// </summary>
        /// <param name="pStrKey">Parameter Name</param>
        /// <param name="pIntVal">Parameter Value</param>
        public static void AddParams(String pStrKey, Boolean pBlnVal)
        {
            HTParam.Add(pStrKey, pBlnVal);
            IntParamCount++;
        }


        public static void AddParams(String pStrKey, DateTime? pDatTime, Boolean pBlnDateAndTime)
        {
            if (pDatTime == null || pDatTime.ToString() == "")
            {
                HTParam.Add(pStrKey, System.DBNull.Value);
            }
            else
            {
                HTParam.Add(pStrKey, DTDBDate(pDatTime).Value.ToString("MM/dd/yyyy hh:mm tt"));
            }
            IntParamCount++;
        }
        /// <summary> Get ParaMeters From HashTable
        /// Method For Get Parameters From HashTable With Key And Vaue
        /// </summary>
        /// <returns>SqlParameters Colection</returns>
        public static SqlParameter[] GetParams()
        {
            Int16 IntI = 0;

            SqlParameter[] GetPara = new SqlParameter[HTParam.Count];
            try
            {
                foreach (DictionaryEntry DE in HTParam)
                {
                    try
                    {
                        //if (DE.Key.ToString().Substring(0, 2) == "@@")
                        if (DE.Key.ToString().Length >= 3 && DE.Key.ToString().Substring(0, 2) == "@@" && DE.Key.ToString().Substring(2, 1) != "@")
                        {
                            GetPara[IntI] = new SqlParameter();
                            GetPara[IntI].Direction = ParameterDirection.Output;
                            GetPara[IntI].Size = 500;
                            GetPara[IntI].ParameterName = DE.Key.ToString().Substring(1);
                        }
                        else if (DE.Key.ToString().Length >= 4 && DE.Key.ToString().Substring(0, 3) == "@@@")
                        {
                            GetPara[IntI] = new SqlParameter();
                            GetPara[IntI].Direction = ParameterDirection.Output;
                            GetPara[IntI].Size = 2000;
                            GetPara[IntI].ParameterName = DE.Key.ToString().Substring(2);
                        }
                        else
                        {
                            if (DE.Value == null)
                            {
                                GetPara[IntI] = new SqlParameter("@" + DE.Key.ToString(), System.DBNull.Value);
                            }
                            else if (DE.Value.GetType().Name.ToUpper() == "BYTE[]")
                            {
                                GetPara[IntI] = new SqlParameter("@" + DE.Key.ToString(), SqlDbType.Image);
                                GetPara[IntI].Value = DE.Value;
                            }
                            else if (DE.Value.GetType().Name == "DBNull")
                            {
                                GetPara[IntI] = new SqlParameter("@" + DE.Key.ToString(), SqlDbType.DateTime);
                                GetPara[IntI].Value = System.DBNull.Value;
                            }
                            else
                            {
                                GetPara[IntI] = new SqlParameter("@" + DE.Key.ToString(), DE.Value.ToString());
                            }

                        }
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message);
                    }

                    IntI++;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            Clear();
            return GetPara;
        }

        /// <summary> Clear All The Parameters From HashTable 
        /// 
        /// </summary>
        public static void Clear()
        {
            HTParam.Clear();
        }

        #endregion

        #region Filling Of Data

        #region With Server Connection
        /// <summary> Fill Of DataSet with Stored Procedure(Dataset ,TableName,Procedure Name,PrimaryKey)
        /// Business DataAdapter for Dataset With Primary Key using Procedure Name
        /// </summary>
        /// <param name="ServerConn">Server Connection Enum </param>
        /// <param name="pDataSet">DataSet</param>
        /// <param name="pTableName">Name of Table Name</param>
        /// <param name="pProcedureName">Name of StoreProcedure</param>
        /// <param name="pParamList">SqlParameter Collection</param>
        /// <param name="pStrPrimaryKey">Primary Key</param>
        public static void FillDataSet(EnumServer ServerConn, DataSet pDataSet, string pTableName, string pProcedureName, SqlParameter[] pParamList, string pStrPrimaryKey)
        {
            DataColumn[] DataColumnPrimaryKey;
            int CountParam = pParamList.Length;
            OpenConnection(ServerConn);
            for (int i = 0; i < CountParam; i++)
            {
                if (pParamList[i] != null)
                    GSql.GComm.Parameters.Add(pParamList[i]);
            }
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
            }
            if (pStrPrimaryKey != "")
            {
                string[] StrArray;
                StrArray = pStrPrimaryKey.Split(',');
                DataColumnPrimaryKey = new DataColumn[StrArray.GetUpperBound(0) + 1];
                for (int IntCount = 0; IntCount <= StrArray.GetUpperBound(0); IntCount++)
                {
                    DataColumnPrimaryKey[IntCount] = pDataSet.Tables[pTableName].Columns[StrArray[IntCount]];
                }
                pDataSet.Tables[pTableName].PrimaryKey = DataColumnPrimaryKey;
            }
            CCon(ServerConn);
        }
        /// <summary> Fill Of DataSet with Stored Procedure(Dataset ,TableName,Procedure Name)
        /// Business DataAdapter for Dataset Without Primary Key using Procedure Name
        /// </summary>
        /// <param name="ServerConn">Server Connection Enum </param>
        /// <param name="pDataSet">DataSet</param>
        /// <param name="pTableName">Name of Table Name</param>
        /// <param name="pProcedureName">Name of StoreProcedure</param>
        /// <param name="pParamList">SqlParameter Collection</param>
        public static void FillDataSet(EnumServer ServerConn, DataSet pDataSet, string pTableName, string pProcedureName, SqlParameter[] pParamList)
        {
            // DataColumn[] DataColumnPrimaryKey;
            int CountParam = pParamList.Length;
            OpenConnection(ServerConn);

            for (int i = 0; i < CountParam; i++)
            {
                if (pParamList[i] != null)
                    GSql.GComm.Parameters.Add(pParamList[i]);
            }
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.CommandTimeout = 500000;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }

        }
        public static void FillData(EnumServer ServerConn, DataSet pDataSet, string pTableName, string pProcedureName, SqlParameter[] pParamList)
        {
            // DataColumn[] DataColumnPrimaryKey;
            int CountParam = pParamList.Length;
            OpenConnection(ServerConn);

            for (int i = 0; i < CountParam; i++)
            {
                if (pParamList[i] != null)
                    GSql.GComm.Parameters.Add(pParamList[i]);
            }
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.CommandTimeout = 500000;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }

        }

        /// <summary> Fill Of DataSet with Stored Procesure(PrimaryKey)
        /// Business DataAdapter for Dataset With General Connetion
        /// </summary>
        /// <param name="ServerConn">Server Connection Enum </param>
        /// <param name="pDataSet">DataSet</param>
        /// <param name="pTableName">Name of Table Name</param>
        /// <param name="pProcedureName">Sql Store Procedure Name</param>
        /// <param name="pStrPrimaryKey">Name of Primary Key</param>
        public static Boolean FillDataSet(EnumServer ServerConn, DataSet pDataSet, string pTableName, string pProcedureName, string pStrPrimaryKey)
        {
            DataColumn[] DataColumnPrimaryKey;
            Boolean BlnProperPrimaryKey = true;

            if (OpenConnection(ServerConn) == false) return false;

            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                GBlnFilTErr = true;
                return false;
            }
            if (pStrPrimaryKey != "")
            {
                string[] StrArray;
                StrArray = pStrPrimaryKey.Split(',');
                DataColumnPrimaryKey = new DataColumn[StrArray.GetUpperBound(0) + 1];
                for (int IntCount = 0; IntCount <= StrArray.GetUpperBound(0); IntCount++)
                {
                    if (pDataSet.Tables[pTableName].Columns[StrArray[IntCount]] == null)
                    {
                        BlnProperPrimaryKey = false;
                        break;
                    }
                    DataColumnPrimaryKey[IntCount] = pDataSet.Tables[pTableName].Columns[StrArray[IntCount]];
                }
                if (BlnProperPrimaryKey == false)
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                    return false;
                }
                pDataSet.Tables[pTableName].PrimaryKey = DataColumnPrimaryKey;
            }
            return true;
        }

        /// <summary> Fill Of DataSet with Stored Procesure(Without Primary Key )
        /// Business DataAdapter for Dataset With General Connetion
        /// </summary>
        /// <param name="pDataSet">DataSet</param>
        /// <param name="pTableName">Name of DataTable</param>
        /// <param name="pProcedureName">Sql Store Procedure Name</param>
        public static void FillDataSet(EnumServer ServerConn, DataSet pDataSet, string pTableName, string pProcedureName)
        {
            //  DataColumn[] DataColumnPrimaryKey;
            OpenConnection(ServerConn);

            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }
        }

        #endregion


        #region Without Server Connection
        /// <summary> Fill Of DataSet with Stored Procedure(Dataset ,TableName,Procedure Name,PrimaryKey)
        /// Business DataAdapter for Dataset With Primary Key using Procedure Name
        /// </summary>
        /// <param name="pDataSet">DataSet</param>
        /// <param name="pTableName">Name of Table Name</param>
        /// <param name="pProcedureName">Name of StoreProcedure</param>
        /// <param name="pParamList">SqlParameter Collection</param>
        /// <param name="pStrPrimaryKey">Primary Key</param>
        public static void FillDataSet(DataSet pDataSet, string pTableName, string pProcedureName, SqlParameter[] pParamList, string pStrPrimaryKey)
        {
            DataColumn[] DataColumnPrimaryKey;
            int CountParam = pParamList.Length;
            for (int i = 0; i < CountParam; i++)
            {
                if (pParamList[i] != null)
                    GSql.GComm.Parameters.Add(pParamList[i]);
            }
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.Connection = GSql.GConn;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
            }
            if (pStrPrimaryKey != "")
            {
                string[] StrArray;
                StrArray = pStrPrimaryKey.Split(',');
                DataColumnPrimaryKey = new DataColumn[StrArray.GetUpperBound(0) + 1];
                for (int IntCount = 0; IntCount <= StrArray.GetUpperBound(0); IntCount++)
                {
                    DataColumnPrimaryKey[IntCount] = pDataSet.Tables[pTableName].Columns[IntCount];
                }
                pDataSet.Tables[pTableName].PrimaryKey = DataColumnPrimaryKey;
            }




        }
        /// <summary> Fill Of DataSet with Stored Procedure(Dataset ,TableName,Procedure Name)
        /// Business DataAdapter for Dataset Without Primary Key using Procedure Name
        /// </summary>
        /// <param name="pDataSet">DataSet</param>
        /// <param name="pTableName">Name of Table Name</param>
        /// <param name="pProcedureName">Name of StoreProcedure</param>
        /// <param name="pParamList">SqlParameter Collection</param>
        public static void FillDataSet(DataSet pDataSet, string pTableName, string pProcedureName, SqlParameter[] pParamList)
        {
            DataColumn[] DataColumnPrimaryKey;
            int CountParam = pParamList.Length;

            OpenConnection(EnumServer.ACC);
            for (int i = 0; i < CountParam; i++)
            {
                if (pParamList[i] != null)
                    GSql.GComm.Parameters.Add(pParamList[i]);
            }
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.Connection = GSql.GConn;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(EnumServer.ACC);
            }

        }


        /// <summary> Fill Of DataSet with Stored Procesure(PrimaryKey)
        /// Business DataAdapter for Dataset With General Connetion
        /// </summary>
        /// <param name="pDataSet">DataSet</param>
        /// <param name="pTableName">Name of Table Name</param>
        /// <param name="pProcedureName">Sql Store Procedure Name</param>
        /// <param name="pStrPrimaryKey">Name of Primary Key</param>
        public static void FillDataSet(DataSet pDataSet, string pTableName, string pProcedureName, string pStrPrimaryKey)
        {
            DataColumn[] DataColumnPrimaryKey;

            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.Connection = GSql.GConn;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (pStrPrimaryKey != "")
            {
                string[] StrArray;
                StrArray = pStrPrimaryKey.Split(',');
                DataColumnPrimaryKey = new DataColumn[StrArray.GetUpperBound(0) + 1];
                for (int IntCount = 0; IntCount <= StrArray.GetUpperBound(0); IntCount++)
                {
                    DataColumnPrimaryKey[IntCount] = pDataSet.Tables[pTableName].Columns[IntCount];
                }
                pDataSet.Tables[pTableName].PrimaryKey = DataColumnPrimaryKey;
            }
        }
        /// <summary> Fill Of DataSet with Stored Procesure(Without Primary Key )
        /// Business DataAdapter for Dataset With General Connetion
        /// </summary>
        /// <param name="pDataSet">DataSet</param>
        /// <param name="pTableName">Name of DataTable</param>
        /// <param name="pProcedureName">Sql Store Procedure Name</param>
        public static void FillDataSet(DataSet pDataSet, string pTableName, string pProcedureName)
        {
            DataColumn[] DataColumnPrimaryKey;


            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.Connection = GSql.GConn;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        public static void FillDataSet(DataSet pDataSet, string pTableName, string pProcedureName, int IntType)
        {
            DataColumn[] DataColumnPrimaryKey;
            OpenConnection(EnumServer.ACC);
            GSql.GComm.CommandText = pProcedureName;
            if (IntType == 0)
                GSql.GComm.CommandType = CommandType.StoredProcedure;
            else
                GSql.GComm.CommandType = CommandType.Text;
            GSql.GComm.Connection = GSql.GConn;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                    if (IntType == 1)
                        pDataSet.Tables.Remove(pTableName);
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #endregion


        /// <summary> Fill Of DataTable With(StoreProcedure)
        /// Business DataAdapter for DataTable With Parameter Connetion
        /// </summary>
        /// <param name="pDataTable">DataTable</param>
        /// <param name="pProcedureName">Store Procedure Name </param>
        /// <param name="pParamList">Parameter List Array</param>
        public static void FillDataTable(EnumServer ServerConn, DataTable pDataTable, String pProcedureName, SqlParameter[] pParamList)
        {
            int CountParam = pParamList.Length;
            OpenConnection(ServerConn);

            for (int i = 0; i < CountParam; i++)
            {
                if (pParamList[i] != null)
                    GSql.GComm.Parameters.Add(pParamList[i]);
            }
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataTable == null))
                {
                    pDataTable.Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }
        }
        /// <summary> Fill Of DataTable With Stored Procedure With Primary Key(DataTable,ProcedureName,ParaList,PrimaryKey)
        /// Business DataAdapter for DataTable With Primary Keys
        /// </summary>
        /// <param name="pConn"></param>
        /// <param name="pDataTable"></param>
        /// <param name="pProcedureName"></param>
        /// <param name="pParamList"></param>
        /// <param name="pStrPrimaryKey">Primary Keys</param>
        public static void FillDataTable(EnumServer ServerConn, DataTable pDataTable, String pProcedureName, SqlParameter[] pParamList, string pStrPrimaryKeys)
        {
            int CountParam = pParamList.Length;
            DataColumn[] DataColumnPrimaryKey;
            OpenConnection(ServerConn);

            for (int i = 0; i < CountParam; i++)
            {
                if (pParamList[i] != null)
                    GSql.GComm.Parameters.Add(pParamList[i]);
            }
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataTable == null))
                {
                    pDataTable.Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataTable);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }

            if (pStrPrimaryKeys != "")
            {
                string[] StrArray;
                StrArray = pStrPrimaryKeys.Split(',');
                DataColumnPrimaryKey = new DataColumn[StrArray.GetUpperBound(0) + 1];
                for (int IntCount = 0; IntCount <= StrArray.GetUpperBound(0); IntCount++)
                {
                    DataColumnPrimaryKey[IntCount] = pDataTable.Columns[IntCount];
                }
                pDataTable.PrimaryKey = DataColumnPrimaryKey;
            }
        }
        /// <summary> Fill Of DataTable With General Query(DataTable,Query)
        /// Business DataAdapter for DataTable With Parameter Connetion
        /// </summary>
        /// <param name="pDataTable">Name of Table Name</param>
        /// <param name="pProcedureName">Stored Procedure Name</param>
        public static void FillDataTable(EnumServer ServerConn, DataTable pDataTable, string pProcedureName)
        {
            DataColumn[] DataColumnPrimaryKey;
            OpenConnection(ServerConn);

            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataTable == null))
                {
                    pDataTable.Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataTable);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                CCon(ServerConn);
            }

        }
        /// <summary> Fill Of DataTable With General Query(DataTable,Query)
        /// Business DataAdapter for DataTable With Parameter Connetion
        /// </summary>
        /// <param name="pDataTable">Name of Table Name</param>
        /// <param name="pProcedureName">Stored Procedure Name</param>
        /// <param name="pStrPrimaryKey">Primary Keys</param>
        public static void FillDataTable(EnumServer ServerConn, DataTable pDataTable, string pProcedureName, string pStrPrimaryKeys)
        {
            DataColumn[] DataColumnPrimaryKey;
            OpenConnection(ServerConn);

            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.Connection = GSql.GConn;
            GSql.GDataAdapter.SelectCommand = GSql.GComm;
            try
            {
                if (!(pDataTable == null))
                {
                    pDataTable.Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataTable);

                if (pStrPrimaryKeys != "")
                {
                    string[] StrArray;
                    StrArray = pStrPrimaryKeys.Split(',');
                    DataColumnPrimaryKey = new DataColumn[StrArray.GetUpperBound(0) + 1];
                    for (int IntCount = 0; IntCount <= StrArray.GetUpperBound(0); IntCount++)
                    {
                        DataColumnPrimaryKey[IntCount] = pDataTable.Columns[IntCount];
                    }
                    pDataTable.PrimaryKey = DataColumnPrimaryKey;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                CCon(ServerConn);
            }
        }


        /// <summary> Give DataReader With(Store Procedure)
        /// Use To Executer Store Procedure With Parameter List With General Connetion
        /// <param name="pProcedureName">Name of Store Procedure</param>
        /// <param name="pParamList">Parameter List Arraty</param>
        /// <returns>SqlDataReader With Record</returns>
        public static SqlDataReader ExeRed(String pProcedureName, SqlParameter[] pParamList)
        {
            int CountParam = pParamList.Length;

            for (int i = 0; i < CountParam; i++)
            {
                GSql.GComm.Parameters.Add(pParamList[i]);
            }
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.Connection = GSql.GConn;
            try
            {
                return GSql.GComm.ExecuteReader();
            }
            catch (SqlException e)
            {
                return null;
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
            }
        }


        public static SqlDataReader ExeRed(EnumServer ServerConn, string pStr)
        {
            OpenConnection(ServerConn);
            GSql.GComm.CommandType = CommandType.Text;
            GSql.GComm.CommandText = pStr;
            GSql.GComm.Connection = GSql.GConn;
            return GSql.GComm.ExecuteReader();
        }

        /// <summary> Give DataReader With(,ServerConn,Store Procedure)
        /// Use To Executer Store Procedure With Parameter List With General Connetion
        /// <param name="pProcedureName">Name of Store Procedure</param>
        /// <param name="pParamList">Parameter List Arraty</param>
        /// <returns>SqlDataReader With Record</returns>
        public static SqlDataReader ExeRed(EnumServer ServerConn, String pProcedureName, SqlParameter[] pParamList)
        {
            OpenConnection(ServerConn);
            int CountParam = pParamList.Length;

            for (int i = 0; i < CountParam; i++)
            {
                GSql.GComm.Parameters.Add(pParamList[i]);
            }
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                return GSql.GComm.ExecuteReader();
            }
            catch (SqlException)
            {
                return null;
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
            }
        }


        /// <summary>
        /// Acccount Coonection 
        /// </summary>
        /// <param name="pConn"></param>
        /// <param name="pDataSet"></param>
        /// <param name="pTableName"></param>
        /// <param name="pProcedureName"></param>
        /// <param name="pParamList"></param>

        public static void FillDataSet(SqlConnection pConn, DataSet pDataSet, string pTableName, string pProcedureName, SqlParameter[] pParamList)
        {
            int CountParam = pParamList.Length;


            for (int i = 0; i < CountParam; i++)
            {
                if (pParamList[i] != null)
                    GSql.GAccComm.Parameters.Add(pParamList[i]);
            }

            GSql.GAccComm.CommandType = CommandType.StoredProcedure;
            GSql.GAccComm.CommandText = pProcedureName;
            GSql.GAccComm.Connection = pConn;
            GSql.GDataAdapter.SelectCommand = GSql.GAccComm;

            try
            {
                if (!(pDataSet.Tables[pTableName] == null))
                {
                    pDataSet.Tables[pTableName].Rows.Clear();
                }
                GSql.GDataAdapter.Fill(pDataSet, pTableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                GSql.GAccComm.Parameters.Clear();

            }

        }


        /// <summary> Give String With (Store Procedure)
        /// Use To Execute Store Procedure With Parameter List and With Connection As Perameter
        /// </summary>
        /// <param name="pConn">Name of Connetion</param>
        /// <param name="pProcedureName">Name of Store Procedure</param>
        /// <param name="pParamList">Parameter List Arraty</param>
        /// <returns>Number of Affected Record Or If error raise then return -1</returns>
        public static string ExeScal(EnumServer ServerConn, String pProcedureName, SqlParameter[] pParamList)
        {
            String Str = "";
            int CountParam = pParamList.Length;
            OpenConnection(ServerConn);
            for (int i = 0; i < CountParam; i++)
            {
                if (pParamList[i] != null)
                    GSql.GComm.Parameters.Add(pParamList[i]);
            }
            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                Str = GSql.GComm.ExecuteScalar().ToString();
                return Str;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                return Str;
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }
        }

        /// <summary> Give String With (Store Procedure)
        /// Use To Execute Store Procedure With Parameter List and With Connection As Perameter
        /// </summary>
        /// <param name="pConn">Name of Connetion</param>
        /// <param name="pProcedureName">Name of Store Procedure</param>
        /// <param name="pParamList">Parameter List Arraty</param>
        /// <returns>Number of Affected Record Or If error raise then return -1</returns>
      
        public static string ExeScal(EnumServer ServerConn, String pProcedureName)
        {
            String Str = "";
            OpenConnection(ServerConn);

            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                Str = GSql.GComm.ExecuteScalar().ToString();
                return Str;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                return Str;
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }
        }
        /// <summary> Execute NonQuery With No of Affected Record With(Store Procedure)
        /// Use To Execute Store Procedure With Parameter List and With General Connetion
        /// </summary>
        /// <param name="pProcedureName">Name of Store Procedure</param>
        /// <param name="pParamList">Parameter List Arraty</param>
        /// <returns>Number of Affected Record Or If error raise then return -1</returns>
        /// 
    
        /// <summary> Execute NonQuery With No of Affected Record With(Store Procedure)
        /// Use To Execute Store Procedure With Parameter List and With General Connetion
        /// </summary>
        /// <param name="pProcedureName">Name of Store Procedure</param>
        /// <param name="pParamList">Parameter List Arraty</param>
        /// <returns>Number of Affected Record Or If error raise then return -1</returns>
        /// 
        public static int ExNonQuery(EnumServer ServerConn, String pProcedureName, SqlParameter[] pParamList)
        {
            OpenConnection(ServerConn);
            Int32 IntParams = pParamList.Length;
            GSql.GComm.Parameters.Clear();

            for (Int32 IntI = 0; IntI < pParamList.Length; IntI++)
            {
                if ((pParamList[IntI] != null)) GSql.GComm.Parameters.Add(pParamList[IntI]);
            }

            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                GSql.GComm.ExecuteNonQuery();
                return 1;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Here Error Is " + e.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }
            return -1;
        }

        public static int ExNonQueryWeb(EnumServer ServerConn, String pProcedureName, SqlParameter[] pParamList)
        {
            OpenWebConnection(ServerConn);
            Int32 IntParams = pParamList.Length;
            GSql.GComm.Parameters.Clear();

            for (Int32 IntI = 0; IntI < pParamList.Length; IntI++)
            {
                if ((pParamList[IntI] != null)) GSql.GWebComm.Parameters.Add(pParamList[IntI]);
            }

            GSql.GWebComm.CommandType = CommandType.StoredProcedure;
            GSql.GWebComm.CommandText = pProcedureName;
            try
            {
                return GSql.GWebComm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                GSql.GWebComm.Parameters.Clear();
                CCon(ServerConn);
            }
            return -1;
        }

        public static int ExNonQuery(EnumServer ServerConn, String pProcedureName, SqlParameter[] pParamList, String pStrOutParam)
        {
            OpenConnection(ServerConn);
            Int32 IntParams = pParamList.Length;
            GSql.GComm.Parameters.Clear();

            for (Int32 IntI = 0; IntI < pParamList.Length; IntI++)
            {
                if ((pParamList[IntI] != null)) GSql.GComm.Parameters.Add(pParamList[IntI]);
            }

            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                GSql.GComm.ExecuteNonQuery();
                return Convert.ToInt32("0" + GSql.GComm.Parameters[pStrOutParam].Value);
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }
            return -1;
        }

      
        public static double ExNonQueryWithOutParam(EnumServer ServerConn, String pProcedureName, SqlParameter[] pParamList, String pStrOutParam)
        {
            OpenConnection(ServerConn);
            Int32 IntParams = pParamList.Length;
            GSql.GComm.Parameters.Clear();

            for (Int32 IntI = 0; IntI < pParamList.Length; IntI++)
            {
                if ((pParamList[IntI] != null)) GSql.GComm.Parameters.Add(pParamList[IntI]);
            }

            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                GSql.GComm.ExecuteNonQuery();
                return Convert.ToDouble(GSql.GComm.Parameters[pStrOutParam].Value);
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }
            return -1;
        }
        public static string ExNonQueryWithOutParamString(EnumServer ServerConn, String pProcedureName, SqlParameter[] pParamList, String pStrOutParam)
        {
            OpenConnection(ServerConn);
            Int32 IntParams = pParamList.Length;
            GSql.GComm.Parameters.Clear();

            for (Int32 IntI = 0; IntI < pParamList.Length; IntI++)
            {
                if ((pParamList[IntI] != null)) GSql.GComm.Parameters.Add(pParamList[IntI]);
            }

            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                GSql.GComm.ExecuteNonQuery();
                return Convert.ToString(GSql.GComm.Parameters[pStrOutParam].Value);
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                CCon(ServerConn);
            }
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServerConn"></param>
        /// <param name="pProcedureName"></param>
        /// <param name="pParamList"></param>
        /// <returns></returns>
        public static int ExNonQuery(EnumServer ServerConn, String pProcedureName, SqlParameter[] pParamList, EnumSqlTran pSqlTransaction)
        {
            Int32 IntParams = pParamList.Length;
            GSql.GComm.Parameters.Clear();

            if (pSqlTransaction == EnumSqlTran.Start)
            {
                OpenConnection(ServerConn);
                GSqlTran = GSql.GComm.Connection.BeginTransaction();
                GSql.GComm.Transaction = GSqlTran;
            }

            for (Int32 IntI = 0; IntI < pParamList.Length; IntI++)
            {
                if ((pParamList[IntI] != null)) GSql.GComm.Parameters.Add(pParamList[IntI]);
            }

            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                GSql.GComm.ExecuteNonQuery();
                return 1;
            }
            catch (SqlException e)
            {
                GSqlTran.Rollback();
                return -1;
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                if (pSqlTransaction == EnumSqlTran.Stop)
                {
                    GSqlTran.Commit();
                    CCon(ServerConn);
                }
            }
            return -1;
        }
        public static string GetStrParamsFull()
        {
            short IntI = 0;
            bool dec = true;
            SqlParameter[] GetPara = new SqlParameter[HTParam.Count];
            string Strret = "";
            foreach (DictionaryEntry DE in HTParam)
            {
                if (DE.Value.ToString().ToUpper() == "DBNULL")
                {
                    Strret += ((Strret.Length != 0) ? "," : "");
                    Strret = Strret + DE.Key + "='null'";
                }
                else
                {
                    switch (DE.Value.GetType().ToString().ToUpper())
                    {
                        case "SYSTEM.STRING":
                            {
                                Strret += ((Strret.Length != 0) ? "," : "");
                                object obj = Strret;
                                Strret = obj + "@" + DE.Key + "='" + DE.Value.ToString() + "'" + Environment.NewLine;
                                break;
                            }
                        case "SYSTEM.DATETIME":
                            if (dec)
                            {
                                object obj = Strret;
                                Strret = obj + "@" + DE.Key + " As DateTime='" + DE.Value.ToString() + "'" + Environment.NewLine;
                            }
                            else
                            {
                                Strret += ((Strret.Length != 0) ? "," : "");
                                object obj = Strret;
                                Strret = obj + "@" + DE.Key + "='" + DE.Value.ToString() + "'" + Environment.NewLine;
                            }
                            break;
                        case "SYSTEM.DOUBLE":
                        case "SYSTEM.DECIMAL":
                        case "SYSTEM.INTEGER":
                        case "SYSTEM.INT64":
                        case "SYSTEM.INT32":
                        case "SYSTEM.INT16":
                            if (dec)
                            {
                                Strret += ((Strret.Length != 0) ? "," : "");
                                object obj = Strret;
                                Strret = obj + "@" + DE.Key + "=" + DE.Value.ToString() + Environment.NewLine;
                            }
                            else
                            {
                                Strret += ((Strret.Length != 0) ? "," : "");
                                object obj = Strret;
                                Strret = obj + "@" + DE.Key + "=" + DE.Value.ToString() + Environment.NewLine;
                            }
                            break;
                    }
                }
                IntI = (short)(IntI + 1);
            }
            return Strret;
        }

        //public static string ExNonQuery(EnumServer ServerConn, String pProcedureName, SqlParameter[] pParamList, String pStrReturnValue)
        //{
        //    Int32 IntParams = pParamList.Length;
        //    GSql.GComm.Parameters.Clear();

        //    if (pSqlTransaction == EnumSqlTran.Start)
        //    {
        //        OpenConnection(ServerConn);
        //        GSqlTran = GSql.GComm.Connection.BeginTransaction();
        //        GSql.GComm.Transaction = GSqlTran;
        //    }

        //    for (Int32 IntI = 0; IntI < pParamList.Length; IntI++)
        //    {
        //        if ((pParamList[IntI] != null)) GSql.GComm.Parameters.Add(pParamList[IntI]);
        //    }

        //    GSql.GComm.CommandType = CommandType.StoredProcedure;
        //    GSql.GComm.CommandText = pProcedureName;
        //    try
        //    {
        //        return GSql.GComm.ExecuteNonQuery();
        //    }
        //    catch (SqlException e)
        //    {
        //        MessageBox.Show(e.ToString());
        //        GSqlTran.Rollback();
        //        return -1;
        //    }
        //    finally
        //    {
        //        GSql.GComm.Parameters.Clear();
        //        if (pSqlTransaction == EnumSqlTran.Stop)
        //        {
        //            GSqlTran.Commit();
        //            CCon(ServerConn);
        //        }
        //    }
        //    return -1;
        //}

        public static int ExNonQuery(SqlConnection pConn, String pProcedureName, SqlParameter[] pParamList)
        {

            Int32 IntParams = pParamList.Length;
            GSql.GComm.Parameters.Clear();

            for (Int32 IntI = 0; IntI < pParamList.Length; IntI++)
            {
                if ((pParamList[IntI] != null)) GSql.GComm.Parameters.Add(pParamList[IntI]);
            }

            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                GSql.GComm.ExecuteNonQuery();
                return 1;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Here Error Is " + e.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();

            }
            return -1;
        }

        public static int ExNonQuery(SqlConnection pConn, String pProcedureName)
        {


            GSql.GComm.Parameters.Clear();


            GSql.GComm.CommandType = CommandType.Text;
            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.Connection = pConn;
            try
            {
                GSql.GComm.ExecuteNonQuery();
                return 1;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                return -1;
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
            }
        }

        public static int ExNonQuery(EnumServer ServerConn, String pProcedureName)
        {
            OpenConnection(ServerConn);

            GSql.GComm.CommandType = CommandType.Text;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                GSql.GComm.ExecuteNonQuery();
                return 1;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                CCon(ServerConn);
            }
            return -1;
        }

        public static int ExNonQuery(EnumServer ServerConn, String pProcedureName, SqlParameter[] pParamList, EnumSqlTran pSqlTransaction, String pStrOutParam)
        {
            Int32 IntParams = pParamList.Length;
            GSql.GComm.Parameters.Clear();

            if (pSqlTransaction == EnumSqlTran.Start || pSqlTransaction == EnumSqlTran.None)
            {
                OpenConnection(ServerConn);
                if (pSqlTransaction == EnumSqlTran.Start)
                {
                    GSqlTran = GSql.GComm.Connection.BeginTransaction();
                    GSql.GComm.Transaction = GSqlTran;
                }
            }

            for (Int32 IntI = 0; IntI < pParamList.Length; IntI++)
            {
                if ((pParamList[IntI] != null)) GSql.GComm.Parameters.Add(pParamList[IntI]);
            }

            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            try
            {
                GSql.GComm.ExecuteNonQuery();
                return Convert.ToInt32("0" + GSql.GComm.Parameters[pStrOutParam].Value);
            }
            catch (SqlException e)
            {
                //MessageBox.Show(e.ToString());
                //GSqlTran.Rollback();
                return -1;
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
                if (pSqlTransaction == EnumSqlTran.Stop)
                {
                    GSqlTran.Commit();
                    CCon(ServerConn);
                }
            }
            return -1;
        }

        public static int ExNonQuery(String pProcedureName, SqlParameter[] pParamList)
        {
            Int32 IntParams = pParamList.Length;
            GSql.GComm.Parameters.Clear();

            for (Int32 IntI = 0; IntI < pParamList.Length; IntI++)
            {
                if ((pParamList[IntI] != null)) GSql.GComm.Parameters.Add(pParamList[IntI]);
            }

            GSql.GComm.CommandType = CommandType.StoredProcedure;
            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.Connection = GSql.GConn;
            try
            {
                return GSql.GComm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                GSql.GComm.Parameters.Clear();
            }
            return -1;
        }

        public static int ExNonQuery(String pProcedureName)
        {
            GSql.GComm.CommandType = CommandType.Text;
            GSql.GComm.CommandText = pProcedureName;
            GSql.GComm.Connection = GSql.GConn;

            try
            {
                return GSql.GComm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
            }
            return -1;
        }

        public static void ExNonQueryCommitTran(EnumServer ServerConn)
        {
            GSql.GComm.Parameters.Clear();
            GSqlTran.Commit();
            CCon(ServerConn);
        }

        public static void ExNonQueryRollbackTran(EnumServer ServerConn)
        {
            GSql.GComm.Parameters.Clear();
            GSqlTran.Rollback();
            CCon(ServerConn);
        }

        public static void ExNonQueryStartTran(EnumServer ServerConn)
        {
            GSql.GComm.Parameters.Clear();
            OpenConnection(ServerConn);
            GSqlTran = GSql.GComm.Connection.BeginTransaction();
            GSql.GComm.Transaction = GSqlTran;
        }

        #endregion

        #region Closing And Utility like Reader,RecordSet,Ulility Like HasRow,FindNewID,FindText

        /// <summary>Close An Open Reader(SqlDataReader) 
        /// Closes Open Data Reader
        /// </summary>
        /// <param name="pReader">SqlDataReader</param>
        public static void ClsRed(SqlDataReader pReader)
        {
            if (pReader != null)
            {
                if (pReader.IsClosed == false)
                {
                    pReader.Close();
                }
            }
        }

        /// <summary>Method For Checking Rows In A given Reader 
        /// And Return True If Reader Has Rows Loaded Otherwise Returns False
        /// </summary>
        /// <param name="pReader">SqlDataReader</param>
        /// <returns></returns>
        public static bool HasRows(SqlDataReader pReader)
        {
            if (pReader != null)
            {
                if (pReader.HasRows == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>Finds New Id From A Given Table With Criteria
        /// And Returns New Integer Number
        /// </summary>
        /// <param name="ServerConn">Server Name Enum</param>
        /// <param name="pStrTableName">Table Name</param>
        /// <param name="pStrIdExp">Expression Like Max(ID)</param>
        /// <param name="pStrCriateria">Search Criteria</param>
        /// <returns>Integer</returns>
        public static int FindNewId(EnumServer ServerConn, string pStrTableName, string pStrIdExp, string pStrCriateria)
        {

            Int32 StrRetVal;
            AddParams("TableName", pStrTableName);
            AddParams("Alias", pStrIdExp);
            if (!string.IsNullOrEmpty(pStrCriateria))
                AddParams("Criteria", " And " + pStrCriateria);
            else
                AddParams("Criteria", " ");
            StrRetVal = Convert.ToInt32(Conversion.Val(ExeScal(ServerConn, "usp_FindNewId", GetParams()) + ""));
            return StrRetVal + 1;
        }
        public static Double FindSum(EnumServer ServerConn, string pStrTableName, string pStrIdExp, string pStrCriateria)
        {

            double StrRetVal;
            AddParams("TableName", pStrTableName);
            AddParams("Alias", pStrIdExp);
            if (!string.IsNullOrEmpty(pStrCriateria))
                AddParams("Criteria", " And " + pStrCriateria);
            else
                AddParams("Criteria", " ");
            StrRetVal = Convert.ToDouble(Conversion.Val(ExeScal(ServerConn, "sp_FindNewId", GetParams()) + ""));
            return StrRetVal;
        }
        public static string FindName(EnumServer ServerConn, string pStrTableName, string pStrIdExp, string pStrCriateria)
        {

            string StrRetVal;
            AddParams("TableName", pStrTableName);
            AddParams("Alias", pStrIdExp);
            if (!string.IsNullOrEmpty(pStrCriateria))
                AddParams("Criteria", " And " + pStrCriateria);
            else
                AddParams("Criteria", " ");
            StrRetVal = Convert.ToString((ExeScal(ServerConn, "uSp_FindNewId", GetParams()) + ""));
            return StrRetVal;
        }
        public static void FindDisp(EnumServer ServerConn, DataSet Ds, string pStrTableName, string pStrIdExp, string pStrCriateria)
        {
            AddParams("TableName", pStrTableName);
            AddParams("Alias", pStrIdExp);
            if (!string.IsNullOrEmpty(pStrCriateria))
                AddParams("Criteria", " And " + pStrCriateria);
            else
                AddParams("Criteria", " ");
            FillData(ServerConn, Ds, pStrTableName, "SP_FindDisp", GetParams());

        }
        public static void Delete(EnumServer ServerConn, string pStrTableName, string pStrCriateria)
        {

            Int32 StrRetVal;
            AddParams("TABLENAME", pStrTableName);
            if (!string.IsNullOrEmpty(pStrCriateria))
                AddParams("Criteria", " And " + pStrCriateria);
            else
                AddParams("Criteria", " ");

            GOpeSql.ExNonQuery(ServerConn, "usp_Delete", GOpeSql.GetParams());
        }
        /// <summary>Finds Text From A Given TableName With Criteria
        /// And Returns Search String
        /// </summary>
        /// <param name="pConn">SqlConnection</param>
        /// <param name="pStrTableName">Table Name</param>
        /// <param name="pStrIdExp">Search Expression</param>
        /// <param name="pStrCriateria">Search Criteria</param>
        /// <returns>String</returns>
        public static string FindText(string pStrTableName, string pStrIdExp, string pStrCriateria)
        {
            OpenConnection(DataLib.OperationSql.EnumServer.ACC);
            string StrRetVal = "";
            SqlDataReader mSqlReader;
            AddParams("TableName", pStrTableName);
            AddParams("Fields", pStrIdExp + " Search");
            if (!string.IsNullOrEmpty(pStrCriateria))
                AddParams("Criteria", " And " + pStrCriateria);
            else
                AddParams("Criteria", " ");
            mSqlReader = GOpeSql.ExeRed("sp_SELECT", GetParams());
            if (GOpeSql.HasRows(mSqlReader) == true)
            {
                mSqlReader.Read();
                StrRetVal = mSqlReader["Search"].ToString();
            }
            mSqlReader.Close();
            return StrRetVal;
        }
        #endregion

        #region Utility
        /// <summary>
        /// Method For Display Date In Sql [MM/DD/YYYY] Format
        /// </summary>
        /// <param name="pStrDate">Date String</param>
        /// <returns>String</returns>
        private static string SqlDate(string pStrDate)
        {
            if (pStrDate.Length == 0)
            {
                return "null";
            }
            else
            {
                //return "'" + DateTime.Parse(pStrDate).ToString(new System.Globalization.CultureInfo("en-US", false)).ToString() + "'";
                return "" + DateTime.Parse(pStrDate).ToString("MM/dd/yy") + "";
            }
        }
        /// <summary>
        /// Method For Display Time In Sql [HH:MM AM/PM] Format
        /// </summary>
        /// <param name="pStrTime">Time String</param>
        /// <returns>String</returns>
        private static string SqlTime(string pStrTime)
        {
            if (pStrTime.Length == 0)
            {
                return "null";
            }
            else
            {
                return Convert.ToDateTime(pStrTime).ToString("hh:mm tt");
            }
        }
        /// <summary>
        /// Data Access To Business Layer For Time
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns></returns>
        private static DateTime? DTDBTime(DateTime? pDateTime)
        {
            if (pDateTime == null || pDateTime.ToString() == "")
            {
                DateTime? DT = null; // new DateTime(1, 1, 1); 
                return DT;
            }
            else
            {
                DateTime? DTRet = new DateTime(1, 1, 1, pDateTime.Value.Hour, pDateTime.Value.Minute, pDateTime.Value.Second);
                return DTRet;
                //return DateTime.Parse(pDateTime.ToString("hh:mm tt"));
            }
        }
        /// <summary>
        /// Date Checking
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns></returns>
        private static DateTime? DTDBDate(DateTime? pDateTime)
        {
            if (pDateTime == null || pDateTime.ToString() == "")
            {
                DateTime? DT = null;
                return DT;
            }
            else
            {
                return pDateTime;
            }
        }
        #endregion

        public static bool WebReadSettings()
        {
            string StrConnString = string.Empty;
            StrConnString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
            if (StrConnString.Length == 0)
            {
                GSql.GConnString = "";
                return false;
            }
            else
            {
                GSql.GConnString = StrConnString;
            }
            //StrConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DiamondAdodbConnectionString"].ConnectionString;
            //GAdo.GConnString = StrConnString;
            return true;
        }

        public static int SaveGrid(EnumServer ServerConn, DataTable pTab, DataSet pDataSet, string pStrTableName, string pStrExceptField)
        {
            if (pTab.PrimaryKey.Length == 0)
            {
                throw new Exception("Primary Key Require");
            }
            int result = 0;
            if (pDataSet.GetChanges(DataRowState.Modified) != null)
            {
                DataTable changes = pTab.GetChanges(DataRowState.Modified);
                if (changes != null)
                {
                    result = UpdateGrid(ServerConn, changes, pDataSet, pStrTableName, pStrExceptField, false);
                }
            }
            if (pDataSet.GetChanges(DataRowState.Added) != null)
            {
                DataTable changes2 = pTab.GetChanges(DataRowState.Added);
                if (changes2 != null)
                {
                    result = UpdateGrid(ServerConn, changes2, pDataSet, pStrTableName, pStrExceptField, false);
                }
            }
            if (pDataSet.GetChanges(DataRowState.Deleted) != null)
            {
                DeleteGrid(ServerConn, pTab, pDataSet, pStrTableName);
            }
            return result;
        }

        public static int SaveGrid(EnumServer ServerConn, DataTable pTab, DataSet pDataSet, string pStrTableName, string pStrExceptField, EnumSqlTran pSQLTransaction)
        {
            int result = 0;
            if (pDataSet.GetChanges(DataRowState.Modified) != null)
            {
                DataTable changes = pTab.GetChanges(DataRowState.Modified);
                if (changes != null)
                {
                    result = UpdateGrid(ServerConn, changes, pDataSet, pStrTableName, pStrExceptField, false, pSQLTransaction);
                }
            }
            if (pDataSet.GetChanges(DataRowState.Added) != null)
            {
                DataTable changes2 = pTab.GetChanges(DataRowState.Added);
                if (changes2 != null)
                {
                    result = UpdateGrid(ServerConn, changes2, pDataSet, pStrTableName, pStrExceptField, false, pSQLTransaction);
                }
            }
            if (pDataSet.GetChanges(DataRowState.Deleted) != null)
            {
                DeleteGrid(ServerConn, pTab, pDataSet, pStrTableName, pSQLTransaction);
            }
            return result;
        }
    }
}