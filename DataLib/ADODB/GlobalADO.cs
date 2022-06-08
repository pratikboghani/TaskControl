using System;
using System.Collections.Generic;
using System.Text;

namespace DataLib
{
    /// <summary>
    /// Class For Global Variable Declatation [ADODB]
    /// </summary>

    public class GlobalADO
    {
        public enum EnumADODBLockType : int
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

        public static string ConnectionStringADODB = String.Empty;
        public static string GConnString = String.Empty;

        public static ADODB.Connection GConn;
        public static ADODB.Recordset GRecSet = new ADODB.Recordset();
        public static ADODB.Recordset GRecSet1 = new ADODB.Recordset();
        public static ADODB.Recordset GRecSet2 = new ADODB.Recordset();
        public static ADODB.Recordset GRecSet3 = new ADODB.Recordset();
        public static ADODB.Recordset GRecSet4 = new ADODB.Recordset();
        public static ADODB.Recordset GRecSet5 = new ADODB.Recordset();
        public static ADODB.Recordset GRecSet6 = new ADODB.Recordset();

        public static string DiamondServer = "";

        public static string DiamondDBName = "";

        public static string DiamondDBUser = "";
        
        public static string DiamondDBPass = "";

    }            
}