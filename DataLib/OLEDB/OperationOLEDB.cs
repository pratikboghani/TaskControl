using System;
using Microsoft.VisualBasic;
using System.Data.OleDb;
using System.Collections;
using ADODB;
using GADO = DataLib.GlobalADO;
using GOpeADO = DataLib.OperationADO;
using System.Data;

namespace DataLib
{
    /// <summary>
    /// Class For DataBase Operation [OLEDB]
    /// </summary>
    public class OperationOLEDB : DataLib.GlobalOLEDB
    {
        public static int ExNonQueryOleDb(string pStr, OleDbConnection pConn)
        {
            if (pConn == null)
            {
                GlobalOLEDB.GCommOleDb.CommandType = CommandType.Text;
                GlobalOLEDB.GCommOleDb.CommandText = pStr;
                GlobalOLEDB.GCommOleDb.Connection = GlobalOLEDB.GConn;
                return GlobalOLEDB.GCommOleDb.ExecuteNonQuery();
            }
            else
            {
                GlobalOLEDB.GCommOleDb.CommandType = CommandType.Text;
                GlobalOLEDB.GCommOleDb.CommandText = pStr;
                GlobalOLEDB.GCommOleDb.Connection = pConn;
                return GlobalOLEDB.GCommOleDb.ExecuteNonQuery();
            }
        }

        public static void CConOleDb(ref OleDbConnection pConn)
        {
            if (pConn != null)
            {
                if (pConn.State == System.Data.ConnectionState.Open)
                {
                    pConn.Close();
                }
            }
        }
 
    }
}