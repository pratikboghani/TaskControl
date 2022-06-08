using System;
using System.Data;
using Ope = DataLib.OperationSql;
using Val = BusLib.Validation.BOValidation;
using Glb = DataLib.GlobalSql;
using System.Data.SqlClient;
using BusLib.Table;

namespace BusLib
{
    /// <summary>
    /// Class Implementation For Main Form
    /// </summary>
    public class BOFrmMain
    {
        private DataSet _DS = new DataSet();
        DataTable _Dt = new DataTable();

        public string TableCmp = "CmpMast";
        //public string TableTask = "TaskDet";
        public string TablePriority = "PriorityMast";
        public string TablePass = "PASS";
        public string TableViewPara = "VIEWPARA";

        public DataSet DS { get { return _DS; } }
        /// <summary>
        /// Sets Form Menu
        /// </summary>
        /// <param name="UserName">Pass UserName</param>
        /// <returns>Returns DataTable Object</returns>         
        /// 

        public void FillCmp()
        {
            Ope.Clear();
            Ope.FillDataSet(DataLib.OperationSql.EnumServer.ACC, DS, TableCmp, "dbo.Usp_CmpMastFill", Ope.GetParams(), "CmpCode");
        }

        public void FillPriority()
        {
            Ope.Clear();
            Ope.FillDataSet(DataLib.OperationSql.EnumServer.ACC, DS, TablePriority, "dbo.Usp_PriorityMastFill", Ope.GetParams(), "PriorityCode");
        }
        public void FillUser()
        {
            Ope.Clear();
            Ope.FillDataSet(DataLib.OperationSql.EnumServer.ACC, DS, TablePass, "dbo.Usp_UserMastFill", Ope.GetParams(), "UCODE");
        }
        public void FillViewPara(string Str)
        {
            Ope.Clear();
            Ope.AddParams("ViewName", Str);
            Ope.FillDataSet(DataLib.OperationSql.EnumServer.ACC, DS, TableViewPara, "USP_ViewParaFill", Ope.GetParams(), "VIEWNAME,NAME");
        }     
        public int GetMaxId(string StrTableName , string StrColumn)
        {
            return Ope.FindNewId(DataLib.OperationSql.EnumServer.ACC, StrTableName, "MAX("+ StrColumn +")", "");
        }
        //public string GetCmpName(int Cmp_Code)
        //{
        //    return Ope.FindName(DataLib.OperationSql.EnumServer.ACC, TableName, "CmpName", "CmpCode = " + Cmp_Code);
        //}

        //public int GetMaxSrNo(int CmpCode,int UCODE)
        //{
        //    return Ope.FindNewId(DataLib.OperationSql.EnumServer.ACC, TableName1, "MAX(SrNo)", "CmpCode = "+ CmpCode+ "AND UCODE="+UCODE);

        //}

        public void Update()
        {
            if (!(DS.GetChanges() == null))
            {
                Ope.UpdateGrid(DataLib.OperationSql.EnumServer.ACC, DS.GetChanges().Tables[TableCmp], DS, TableCmp, "");
            }
        }
        public void Delete()
        {
            if (!(DS.GetChanges() == null))
            {
                Ope.DeleteGrid(DataLib.OperationSql.EnumServer.ACC, DS.GetChanges().Tables[TableCmp], DS, TableCmp);
            }
        }

        public void Update(string StrTableName)
        {
            if (!(DS.GetChanges() == null))
            {

                Ope.UpdateGrid(DataLib.OperationSql.EnumServer.ACC, DS.GetChanges().Tables[StrTableName], DS, StrTableName, "");
            }
        }
        public void Delete(string StrTableName)
        {
            if (!(DS.GetChanges() == null))
            {
                Ope.DeleteGrid(DataLib.OperationSql.EnumServer.ACC, DS.GetChanges().Tables[StrTableName], DS, StrTableName);
            }
        }

    }
}