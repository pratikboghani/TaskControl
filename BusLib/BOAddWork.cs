using System;
using System.Data;
using Ope = DataLib.OperationSql;
using Val = BusLib.Validation.BOValidation;
using Glb = DataLib.GlobalSql;
using BusLib.Table;

namespace BusLib
{
    /// <summary>
    /// Class Implementation For Main Form
    /// </summary>
    public class BOAddWork
    {
        private DataSet _DS = new DataSet();
        DataTable _Dt = new DataTable();

        public string TableName = "WorkDet";
        public DataSet DS { get { return _DS; } }
        /// <summary>
        /// Sets Form Menu
        /// </summary>
        /// <param name="UserName">Pass UserName</param>
        /// <returns>Returns DataTable Object</returns>         
        /// 


        public int GetNewSrNo(int CmpCode)
        {
            return Ope.FindNewId(DataLib.OperationSql.EnumServer.ACC, TableName, "MAX(SrNo)", "CmpCode="+CmpCode+"and UCODE="+Glb.GIntEndUserCode);
        }
        public void AddWorkDetSave(clsTaskDet Cls)
        {
            Ope.Clear();
            Ope.AddParams("CmpCode", Cls.CmpCode);
            Ope.AddParams("UCODE", Cls.UCODE);
            Ope.AddParams("SrNo", Cls.SrNo);
            Ope.AddParams("TaskName", Cls.TaskName);
            Ope.AddParams("TaskDet", Cls.TaskDet);
            Ope.AddParams("IDate", Val.DTDBDate(Cls.AssDate));
            Ope.ExNonQuery(DataLib.OperationSql.EnumServer.ACC, "Usp_WorkDetSave", Ope.GetParams());
        }
        public void WorkDelete(clsTaskDet Cls)
        {
            Ope.Clear();
            Ope.AddParams("CmpCode", Cls.CmpCode);
            Ope.AddParams("UCODE", Cls.UCODE);
            Ope.AddParams("SrNo", Cls.SrNo);
            Ope.ExNonQuery(DataLib.OperationSql.EnumServer.ACC, "Usp_WorkDelete", Ope.GetParams());
        }
        public void FillTaskDet(int pIntCmpCode,int pIntUCODE)
        {
            Ope.Clear();
            Ope.AddParams("CmpCode", pIntCmpCode);
            Ope.AddParams("UCODE", pIntUCODE);
            Ope.FillDataSet(DataLib.OperationSql.EnumServer.ACC, DS, TableName, "dbo.Usp_WorkDetFill", Ope.GetParams(), "CmpCode,UCODE,SrNo");
        }

    }
}