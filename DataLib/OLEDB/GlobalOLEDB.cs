using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace DataLib
{
    /// <summary>
    /// Class For Global Variable Declatation [OLEDB]
    /// </summary>

    public class GlobalOLEDB
    {
        public enum EnumOLEDBLockType : int
        {
            /// <summary>
            /// Enum Read Only Lock
            /// </summary>
            adLockReadOnly = 1,

            /// <summary>
            /// Enum Optimistic Lock
            /// </summary>
            adLockOptimistic = 3
        }

        public static string GConnString = String.Empty;

        public static System.Data.OleDb.OleDbConnection GConn;
        public static OleDbCommand GCommOleDb = new OleDbCommand() ;
        public static OleDbDataAdapter GDataAdapterOleDb = new OleDbDataAdapter();
    }            
}