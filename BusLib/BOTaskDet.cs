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
    public class BOTaskDet
    {
        private DataSet _DS = new DataSet();
        private DataSet _DSWork = new DataSet();
        DataTable _Dt = new DataTable();

        public string TableName = "TaskDet";
        public string WorkDet = "WorkDet";
        public DataSet DS { get { return _DS; } }
        public DataSet DSWork { get { return _DSWork; } }
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
        public int GetAddWorkSrNo(int CmpCode)
        {
            return Ope.FindNewId(DataLib.OperationSql.EnumServer.ACC, WorkDet, "MAX(SrNo)", "CmpCode=" + CmpCode + "and UCODE=" + Glb.GIntEndUserCode);
        }
        public void NewTaskSave(clsTaskDet Cls)
        {
            Ope.Clear();
            Ope.AddParams("CmpCode", Cls.CmpCode);
            Ope.AddParams("UCODE", Cls.UCODE);
            Ope.AddParams("SrNo", Cls.SrNo);
            Ope.AddParams("TaskName", Cls.TaskName);
            Ope.AddParams("TaskDet", Cls.TaskDet);
            Ope.AddParams("DevRemark", Cls.DevRemark);
            Ope.AddParams("AssDate", Val.DTDBDate(Cls.AssDate));
            Ope.AddParams("TrfDate",Val.DTDBDate(Cls.TrfDate));
            Ope.AddParams("AssToUCODE", Cls.AssToUCODE);
            Ope.AddParams("Priority", Cls.Priority);
            Ope.AddParams("WorkingOn", Cls.WorkingOn);
            Ope.AddParams("TaskComplate", Cls.TaskComplate);
            Ope.AddParams("TargetPath", Cls.FilePath);

            if (Cls.SrNo!=0 && Cls.UCODE !=0&& Cls.CmpCode!=0)
            {
                Ope.ExNonQuery(DataLib.OperationSql.EnumServer.ACC, "Usp_NewTaskSave", Ope.GetParams());
            }
        }
        public void TaskDelete(clsTaskDet Cls)
        {
            Ope.Clear();
            Ope.AddParams("CmpCode", Cls.CmpCode);
            Ope.AddParams("UCODE", Cls.UCODE);
            Ope.AddParams("SrNo", Cls.SrNo);
            Ope.ExNonQuery(DataLib.OperationSql.EnumServer.ACC, "Usp_TaskDelete", Ope.GetParams());
        }
        public void FillTaskDet(int pIntCmpCode , string OptType)
        {
            Ope.Clear();
            Ope.AddParams("CmpCode", pIntCmpCode);
            Ope.AddParams("Type", OptType);
            Ope.AddParams("UCODE", Glb.GIntEndUserCode);
            Ope.FillDataSet(DataLib.OperationSql.EnumServer.ACC, DS, TableName, "dbo.Usp_CmpTaskDetFill", Ope.GetParams(), "CmpCode,UCODE,SrNo");
        }
        public void AddWorkSave(clsTaskDet Cls)
        {
            Ope.Clear();
            Ope.AddParams("CmpCode", Cls.CmpCode);
            Ope.AddParams("UCODE", Cls.UCODE);
            Ope.AddParams("SrNo", Cls.SrNo);
            Ope.AddParams("TaskName", Cls.TaskName);
            Ope.AddParams("TaskDet", Cls.TaskDet);
            Ope.AddParams("IDate", Val.DTDBDate(Cls.IDate));
            if (Cls.SrNo != 0 && Cls.UCODE != 0 && Cls.CmpCode != 0)
            {
                Ope.ExNonQuery(DataLib.OperationSql.EnumServer.ACC, "Usp_WorkDetSave", Ope.GetParams());
            }
        }
        public void FillWorkDet()
        {
            Ope.Clear();
            Ope.FillDataSet(DataLib.OperationSql.EnumServer.ACC, DS, WorkDet, "dbo.Usp_WorkDetFill", Ope.GetParams(), "CmpCode,UCODE,SrNo");
        }
        public void Update()
        {
            if (!(DS.GetChanges() == null))
            {
                Ope.UpdateGrid(DataLib.OperationSql.EnumServer.ACC, DS.GetChanges().Tables[WorkDet], DS, WorkDet, "");
            }
        }
        public void Delete()
        {
            if (!(DS.GetChanges() == null))
            {
                Ope.DeleteGrid(DataLib.OperationSql.EnumServer.ACC, DS.GetChanges().Tables[WorkDet], DS, WorkDet);
            }
        }
    }
}