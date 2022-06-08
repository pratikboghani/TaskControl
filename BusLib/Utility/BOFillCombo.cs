using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Glb = DataLib.GlobalSql;
using Ope = DataLib.OperationSql;
using Val = BusLib.Validation.BOValidation;

namespace BusLib.Utility
{
    public class BOFillCombo
    {
        private DataSet _DS = new DataSet();
        /// <summary>
        /// Retrieve Dataset
        /// </summary>
        public DataSet DS
        {
            get { return _DS; }
            set { _DS = value; }
        }

        private string _TableName = "";

        /// <summary>
        /// Dynamic Table Name Is Given
        /// </summary>
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }


        public int Code { get; set; } = 0;
        public int CmpCode { get; set; } = 0;
        public int SrNo{ get; set; } = 0;



        public Boolean Fill()
        {
            Ope.Clear();
            if ((string.IsNullOrEmpty(_TableName) == true))
            {
                Val.Message("Error During Load. [Invalid Arguments]");
                return false;
            }
            Ope.AddParams("Type", _TableName);
            Ope.AddParams("Code", Code);
            Ope.AddParams("CmpCode", CmpCode);
            Ope.AddParams("SrNo", SrNo);
            Ope.FillDataSet(DataLib.OperationSql.EnumServer.ACC, DS, _TableName, "Usp_Mast_CmbFill", Ope.GetParams());
            return true;
        }

    }
}
