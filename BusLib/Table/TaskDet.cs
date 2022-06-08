using System;
using System.Collections.Generic;
using System.Text;

namespace BusLib.Table
{
    public class clsTaskDet
    {
        public int CmpCode { get; set; } = 0;
        public int UCODE { get; set; } = 0;
        public int SrNo { get; set; } = 0;
        public int AssToUCODE { get; set; } = 0;
        public int Priority { get; set; } = 0;
        public Boolean WorkingOn { get; set; } = false;
        public Boolean TaskComplate { get; set; } = false;
        public string TaskName { get; set; } = "";
        public string TaskDet { get; set; } = "";
        public string DevRemark { get; set; } = "";
        public string AssDate { get; set; } = null;
        public string TrfDate { get; set; } = null;
        public string IDate { get; set; } = null;
        public string FilePath { get; set; } = null;

    }
}