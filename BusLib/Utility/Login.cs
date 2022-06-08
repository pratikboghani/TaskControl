using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Ope = DataLib.OperationSql;
using Val = BusLib.Validation.BOValidation;
using GSql = DataLib.GlobalSql;
using BusLib.Table;
using System.Windows.Forms;
using System.IO;

namespace BusLib.Utility
{
    /// <summary>
    /// User Login Information
    /// </summary>
    public class BOLogin
    {
        private clsLogin insLogin = new clsLogin();
        private SqlDataReader _Reader;
        private String _Category = string.Empty;
        public String mStrUserAuthComp = "";

        public String Category
        {
            get
            {
                return _Category;
            }
        }

        public Boolean ServerOn()
        {
            return Ope.OpenConnection(DataLib.OperationSql.EnumServer.ACC);
        }

        public Int16 CheckLogin(clsLogin pclsLogin)
        {
            SqlDataReader SqlReader;
            string StrDecPass = string.Empty;
            Int16 Result = 0;

            Ope.OpenConnection(DataLib.OperationSql.EnumServer.ACC);
            Ope.AddParams("UserId", pclsLogin.UserId);
            SqlReader = Ope.ExeRed("Usp_UserLoginAuthentication", Ope.GetParams());
            if (Ope.HasRows(SqlReader) == true)
            {
                SqlReader.Read();
                StrDecPass = Ope.ENCODE_DECODE(SqlReader["U_PASS"].ToString(), "D");
                if (StrDecPass == pclsLogin.Password)
                {
                    Result = 1;
                    GSql.GStrEndUserCat = SqlReader["U_CAT"].ToString() + "";
                    GSql.GStrEndUserId = SqlReader["U_NAME"].ToString() + "";
                    GSql.GIntEndUserCode = Val.ToInt(SqlReader["UCODE"].ToString());                    
                    mStrUserAuthComp = SqlReader["AUTCOMP"].ToString();
                    //GSql.gStrMfgUnitName = SqlReader["MfgUnitName"].ToString();
                    GSql.GIntPointer = 1;
                    GSql.GStrEndUserGroup = SqlReader["USRGRP"].ToString();
                    _Category = GSql.GStrEndUserCat;
                }
                else
                {
                    Result = 2;
                }
            }
            Ope.ClsRed(SqlReader);
            Ope.CloseConnection(DataLib.OperationSql.EnumServer.ACC);
            return Result;
        }

    }
}