using DataLib;
using System;
using System.Data;
using Ope = DataLib.OperationSql;

namespace BusLib.Master
{
    public class BOViewParaMast
    {
        private DataSet _DS = new DataSet();

        public string TableName = "VIEWPARA";

        public DataSet DS
        {
            get
            {
                return _DS;
            }
        }     

        #region Funcation

        public DataTable GetViewColumns(string StrViewName)
        {
            OperationSql.Clear();
            DataTable Dt = new DataTable();
            Dt.TableName = StrViewName;

            OperationSql.AddParams("ViewName", StrViewName);
            OperationSql.FillDataTable(DataLib.OperationSql.EnumServer.ACC, Dt, "uSp_ViewFillViewPara", OperationSql.GetParams());
            return Dt;
        }

        #endregion
    }
}
