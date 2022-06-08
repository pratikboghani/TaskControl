using System;
using Microsoft.VisualBasic;
using System.Data.OleDb;
using System.Collections;
using ADODB;
using GADO = DataLib.GlobalADO;
using GOpeADO = DataLib.OperationADO;
using System.Windows.Forms;

namespace DataLib
{
    /// <summary>
    /// Class For DataBase Operation [ADODB]
    /// </summary>
    public class OperationADO : DataLib.GlobalADO
    {

        public static Hashtable HTParam = new Hashtable();
        public static Int32 IntParamCount = 0;

        /// <summary>
        /// Enum : List Of Servers
        /// </summary>
        public enum EnumServer
        {
            /// <summary>
            /// Enum For Stock Server [Value = 0]
            /// </summary>
            Diamond = 0,
        }

        #region Connetion Manipulation

        public static bool IsCon(ADODB.Connection pConn)
        {
            if (pConn.State == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region "Open Connetion"

        public static void OpenConnection()
        {
            if (GADO.GConn != null)
            {
                if (GADO.GConn.State == 1) CloseConnection(GADO.GConn);
            }

            try
            {
                GADO.GConn = new ADODB.Connection();
                GADO.GConn.CursorLocation = ADODB.CursorLocationEnum.adUseClient;
                GADO.GConn.Open(GADO.GConnString, "", "", -1);
                 
            }
            catch (Exception Ex)
            {
                System.Windows.Forms.MessageBox.Show("Connection Failed Retry    " + Ex.Message );
                return;
            }
            if (GlobalADO.DiamondDBName == "")
            {                
                DiamondServer = GADO.GConn.Properties["Data Source"].Value.ToString();                
                DiamondDBUser = GADO.GConn.Properties["User Id"].Value.ToString();
                DiamondDBName = GADO.GConn.Properties["Initial Catalog"].Value.ToString();                 
            }
        }

        #endregion

        #region Close Connection

        /// <summary>
        /// Close Connetion Of SqlServer
        /// </summary>
        /// <param name="pConn">Name of Connection</param>
        public static void CloseConnection(ADODB.Connection pConn)
        {
            if (pConn != null)
            {
                if (pConn.State == 1)
                {
                    pConn.Close();
                    pConn = null;
                }
            }
            else
            {
                pConn = null;
            }
        }

        #endregion

        #region Record Manupulation

        public static void OpenRecSet(ADODB.Recordset pRs, string pStr, EnumADODBLockType ADODBLock)
        {
            pRs.Open(pStr, GADO.GConn, ADODB.CursorTypeEnum.adOpenForwardOnly, (ADODB.LockTypeEnum)ADODBLock, -1);
        }

        public static ADODB.Recordset OpenRecSet(string pStr, EnumADODBLockType ADODBLock)
        {
            ADODB.Recordset RsTemp = new ADODB.Recordset();
            try
            {
                RsTemp.Open(pStr, GADO.GConn, ADODB.CursorTypeEnum.adOpenForwardOnly, (ADODB.LockTypeEnum)ADODBLock, -1);
            }
            catch (Exception e1)
            {

                MessageBox.Show(e1.Message);

            }
            return RsTemp;
        }

        public static void ClsRecSet(ADODB.Recordset pRs)
        {
            if (pRs == null) return;
            if (pRs.State == 1) pRs.Close();
        }

        #endregion

        public static void AddParams(String pStrKey, Object pStrVal)
        {
            HTParam.Add(pStrKey, pStrVal);
            IntParamCount++;
        }

        public static bool HasRows(ADODB.Recordset pRs)
        {
            if (pRs != null)
            {
                if (pRs.RecordCount != 0)
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


        public static void GetParams(Command pCom)
        {
            try
            {
                foreach (DictionaryEntry DE in HTParam)
                {
                    try
                    {
                        foreach (Parameter Par in pCom.Parameters)
                        {
                            if (Par.Name == DE.Key.ToString())
                            {                             
                                if (DE.Value.GetType().Name == "DBNull")
                                {
                                    Par.Value = System.DBNull.Value;
                                }
                                else
                                {
                                    Par.Value = DE.Value.ToString(); 
                                }
                                break;
                            }
                        }                        
                    }
                    catch (Exception x)
                    {
                        MessageBox.Show(x.Message);
                    }                     
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            Clear();             
        }

        public static void Clear()
        {
            HTParam.Clear();
        }

        public static ADODB.Recordset OpenRecSet(EnumServer ServerConn, String pProcedureName)
        {
            ADODB.Recordset RsTemp = new ADODB.Recordset();
            ADODB.Command Com = new Command();
            object objRecAff;

            OpenConnection();

            Com.CommandType = CommandTypeEnum.adCmdStoredProc;
            Com.ActiveConnection = GADO.GConn;
            Com.CommandTimeout = 0;
            Com.CommandText = pProcedureName;
            Com.Parameters.Refresh();
            GetParams(Com);
            Object objParameters = Com.Parameters;

            RsTemp = Com.Execute(out objRecAff, ref objParameters, (int)ADODB.CommandTypeEnum.adCmdStoredProc);
            return RsTemp;
        }


        public static ADODB.Recordset OpenRecSet(EnumServer ServerConn, String pProcedureName, Boolean pBlnSubReport)
        {
            ADODB.Recordset RsTemp = new ADODB.Recordset();
            ADODB.Command Com = new Command();
            object objRecAff;
            if (pBlnSubReport == false)
            {
                OpenConnection();
            }

            Com.CommandType = CommandTypeEnum.adCmdStoredProc;
            Com.ActiveConnection = GADO.GConn;
            Com.CommandTimeout = 0;
            Com.CommandText = pProcedureName;
            Com.Parameters.Refresh();
            GetParams(Com);
            Object objParameters = Com.Parameters;

            RsTemp = Com.Execute(out objRecAff, ref objParameters, (int)ADODB.CommandTypeEnum.adCmdStoredProc);
            return RsTemp;
        }
    }
}