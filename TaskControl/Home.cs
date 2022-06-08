using BusLib;
using BusLib.Master;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Glb = DataLib.GlobalSql;
using Val = BusLib.Validation.BOValidation;
using BusLib.Table;
using BusLib.Utility;
using Infragistics.Win;
using Ope = DataLib.OperationSql;
using Events = BusLib.Utility;
using System.IO;
using Microsoft.Win32;

namespace TaskControl
{
    public partial class Home : Form
    {
        BOFrmMain BOFrmMain = new BOFrmMain();
        private Events.BOFormEvents ObjFormEvents = new Events.BOFormEvents();
        private BOViewParaMast mboViewPara = new BOViewParaMast();
        //BOFrmMain.ClsCmpMast clsCmpMast = new BOFrmMain.ClsCmpMast();
        public string strCmpName = "";
        public string strTargetPath = "";
        public string FilesPathSave;
        public int CmpCode = 0;
        BOTaskDet mBoTaskDet = new BOTaskDet();
        BOFillCombo _BOPriority = new BOFillCombo();
        BOFillCombo _BOUserMast = new BOFillCombo();
        BOFillCombo _BOAssUserMast = new BOFillCombo();
        BOFillCombo _BOCmpMast = new BOFillCombo();
        BOFillCombo _BODOWNLOADFILE = new BOFillCombo();
        clsTaskDet _clsTaskDet = new clsTaskDet();
        bool IsEntryExists = false;
        bool IsDownloadExists = false;
        public Home()
        {
            InitializeComponent();
            ObjFormEvents.CurForm = this;
            ObjFormEvents.FormKeyPress = true;
            ObjFormEvents.FormKeyDown = true;
            //ObjFormEvents.FormResize = true;
            ObjFormEvents.FormDisposed = true;
        }


        private void FillCmp()
        {//192, 192, 255 Grp color
            BOFrmMain.FillCmp();
            UltGrdCmp.SetDataBinding(BOFrmMain.DS, BOFrmMain.TableCmp, true);
            UltGrdCmp.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
            Global.SetGridColumns(UltGrdCmp, mboViewPara.GetViewColumns("CMPMAST"));
            this.UltGrdCmp.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            this.UltGrdCmp.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Edit;
            UltGrdCmp.SetOperation(false, false, false, UTC.UTCGrid.EnumCellActivation.NoEdit);
        }

        private void FillCmpTask(int CmpCode, string OptType)
        {
            mBoTaskDet.FillTaskDet(CmpCode, OptType);
            UltGrdCmpTask.SetDataBinding(mBoTaskDet.DS, mBoTaskDet.TableName, true);
            UltGrdCmpTask.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
            Global.SetGridColumns(UltGrdCmpTask, mboViewPara.GetViewColumns("TASKDET"));
            this.UltGrdCmpTask.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            this.UltGrdCmpTask.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Edit;
            UltGrdCmpTask.SetOperation(false, true, false, UTC.UTCGrid.EnumCellActivation.NoEdit);
        }
        private void FillWorkDet()
        {
            mBoTaskDet.FillWorkDet();
            UltGrdCmpTask.SetDataBinding(mBoTaskDet.DS, mBoTaskDet.WorkDet, true);
            UltGrdCmpTask.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
            Global.SetGridColumns(UltGrdCmpTask, mboViewPara.GetViewColumns("WORKDET"));
            this.UltGrdCmpTask.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            this.UltGrdCmpTask.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Edit;
            UltGrdCmpTask.SetOperation(false, true, false, UTC.UTCGrid.EnumCellActivation.AllowEdit);
        }

        private void Home_Load(object sender, EventArgs e)
        {
            ultraStatusBar1.Panels[2].Text = Glb.GStrEndUserId;
            SetControl();
            FillCmp();

            OptType_ValueChanged(null, null);
            if (Glb.GStrEndUserCat == "S" || Glb.GStrEndUserCat == "A")
            {
                UltTabCtr.Tabs["A"].Visible = true;
                UltcmbAssF.ReadOnly = false;
                txtAssDate.ReadOnly = false;
                txtTrfDate.ReadOnly = false;
            }
            else
            {
                UltTabCtr.Tabs["A"].Visible = false;
                UltcmbAssF.ReadOnly = true;
                txtAssDate.ReadOnly = true;
                txtTrfDate.ReadOnly = true;
            }

            //SetPermition();
        }

        public void SetControl()
        {
            _BOCmpMast.TableName = "CmpMast";
            _BOCmpMast.Fill();
            UltCmbAddWorkCmp.SetColumn(_BOCmpMast.TableName, "CmpName,CmpCode", "" + (Val.ToInt(UltCmbAddWorkCmp.Width) - 40).ToString() + ",40");
            UltCmbAddWorkCmp._SetDataBinding(_BOCmpMast.DS, _BOCmpMast.TableName, true);
            UltCmbAddWorkCmp.SetObject(lblAddWorkCmpCode, "CmpCode");

            _BOUserMast.TableName = "UserMast";
            _BOUserMast.Fill();
            UltcmbAssF.SetColumn(_BOUserMast.TableName, "U_NAME,UCODE", "100,40");
            UltcmbAssF._SetDataBinding(_BOUserMast.DS, _BOUserMast.TableName, true);
            UltcmbAssF.SetObject(lblAssF, "UCODE");

            UltcmbAssT.SetColumn(_BOUserMast.TableName, "U_NAME,UCODE", "100,40");
            UltcmbAssT._SetDataBinding(_BOUserMast.DS, _BOUserMast.TableName, true);
            UltcmbAssT.SetObject(lblAssT, "UCODE");

            _BOPriority.TableName = "Priority";
            _BOPriority.Fill();
            UltCmbPriority.SetColumn(_BOPriority.TableName, "Name,Code", "100,40");
            UltCmbPriority._SetDataBinding(_BOPriority.DS, _BOPriority.TableName, true);
            UltCmbPriority.SetObject(lblPriorityCode, "Code");

            UltCmbPriority1.SetColumn(_BOPriority.TableName, "Name,Code", "100,40");
            UltCmbPriority1._SetDataBinding(_BOPriority.DS, _BOPriority.TableName, true);
            UltCmbPriority1.SetObject(lblPriorityCode1, "Code");
            UltCmbPriority1.Text = UltCmbPriority1.Rows[1].Cells["Name"].Text;


        }
        public void SetDownload(int UCode, int CmpCode, int SrNo)
        {
    
            if (_BODOWNLOADFILE.DS.Tables.Count > 0)
            {
                _BODOWNLOADFILE.DS.Tables.Remove(_BODOWNLOADFILE.TableName + "1");
                _BODOWNLOADFILE.DS.Tables.Remove(_BODOWNLOADFILE.TableName);
            }
            _BODOWNLOADFILE.TableName = "DOWNLOADFILE";
            _BODOWNLOADFILE.Code = UCode;
            _BODOWNLOADFILE.CmpCode = CmpCode;
            _BODOWNLOADFILE.SrNo = SrNo;
            _BODOWNLOADFILE.Fill();
            DataTable dt = new DataTable();
            dt.Columns.Add("FilePath");
            dt.Columns.Add("FileName");
            DataRow dr = _BODOWNLOADFILE.DS.Tables[_BODOWNLOADFILE.TableName].Rows[0];  
            string[] FilePath = dr["FilePath"].ToString().Split(',');
            string[] filename = dr["FilePath"].ToString().Split(',');
            int i = 0;
            foreach (string st in filename)
            {

                int length = st.Split('\\').Length - 1;
                filename[i] = st.Split('\\')[length];
                dt.Rows.Add(FilePath[i], filename[i]);
                i++;
            }
            dt.Rows[dt.Rows.Count - 1].Delete();
            dt.TableName = _BODOWNLOADFILE.TableName + "1";
            _BODOWNLOADFILE.DS.Tables.Add(dt);
            UltCmbDownload.SetColumn(_BODOWNLOADFILE.TableName + "1", "FileName", (Val.ToInt(UltCmbDownload.Width)).ToString());
            UltCmbDownload._SetDataBinding(_BODOWNLOADFILE.DS, _BODOWNLOADFILE.TableName + "1", true);
            UltCmbDownload.SetObject(lblDownloadFPath, "FilePath");
            UltCmbDownload.UpdateData();
            UltCmbDownload.Refresh();

            //UltCmbDownload.Text = UltCmbDownload.Rows[0].Cells["FileName"].Text;
        }
        public void SetPermition()
        {
            if (Glb.GIntEndUserCode == Val.ToInt(lblTaskUCode.Text) || Glb.GStrEndUserCat == "S" || Glb.GStrEndUserCat == "A")
            {
                txtTaskName.ReadOnly = false;
                UltcmbAssF.ReadOnly = true;
                UltcmbAssT.ReadOnly = false;
                //txtAssDate.Enabled = false;
                txtTrfDate.Enabled = false;
                txtTaskDetails.ReadOnly = false;
                UltCmbPriority.ReadOnly = false;
            }
            else
            {
                txtTaskName.ReadOnly = true;
                UltcmbAssT.ReadOnly = false;
                txtTaskDetails.ReadOnly = true;
                UltCmbPriority.ReadOnly = true;
            }

        }

        private void txtPass_ValueChanged(object sender, EventArgs e)
        {
            if (txtPass.Text == "1")
            {
                UltGrd.SetOperation(true, true, true, UTC.UTCGrid.EnumCellActivation.AllowEdit);
            }
            else
            {
                UltGrd.SetOperation(false, false, false, UTC.UTCGrid.EnumCellActivation.NoEdit);
            }
        }
        public void SetVisible(bool i)
        {
            UltcmbAssT.Visible = i;
            txtTrfDate.Visible = i;
            txtAssDays.Visible = i;
            txtTrfDays.Visible = i;
            txtDevRemark.Visible = i;
            UltCmbPriority.Visible = i;
            ultraLabel2.Visible = i;
            ultraLabel1.Visible = i;
            ultraLabel10.Visible = i;
            ultraLabel5.Visible = i;
            ultraLabel6.Visible = i;
            lblCmbPriority.Visible = i;
            CmbWorkingOn.Visible = i;
            CmbTaskComplete.Visible = i;
            UltcmbAssF.Visible = i;
            txtTaskName.ReadOnly = i;
            txtTaskDetails.ReadOnly = i;
            txtAssDate.ReadOnly = i;
        }
        private void OptType_ValueChanged(object sender, EventArgs e)
        {
            if (OptType.StrPrcCode.ToUpper() == "WORK")
            {
                UltCmbDownload.Visible = false;
                lblfiles.Visible = false;
                SetVisible(false);
                FillWorkDet();
            }
            else
            {
                UltCmbDownload.Visible = false;
                lblfiles.Visible = false;
                UltGrpAddWork.Visible = false;
                SetVisible(true);
                FillCmpTask(0, OptType.StrPrcCode);
            }
            ClearTaskDet();
        }

        private void UltGrdCmp_ClickCellButton(object sender, CellEventArgs e)
        {

            UltGrpTaskSave.Size = UltGrdCmpTask.Size;
            UltGrpTaskSave.Width = UltGrdCmpTask.Width;
            UltGrpTaskSave.Location = UltGrdCmpTask.Location;
            UltGrpTaskSave.Dock = UltGrdCmpTask.Dock;
            UltGrpTaskSave.Anchor = UltGrdCmpTask.Anchor;

            //Rectangle rectBounding = new Rectangle();
            //rectBounding = UltGrdCmp.ActiveCell.GetUIElement().ClipRect;
            //UltGrpTaskSave.Location = new Point(rectBounding.X, rectBounding.Y);

            lblCmpName1.Text = UltGrdCmp.ActiveRow.Cells["CmpName"].Text;
            lblCmpCode1.Text = UltGrdCmp.ActiveRow.Cells["CmpCode"].Text;

            UltGrpTaskSave.Visible = true;
            txtAssDate1.Focus();
        }
        public void AssignToVar()
        {
            try
            {
                _clsTaskDet.CmpCode = Val.ToInt(lblCmpCode.Text);
                _clsTaskDet.UCODE = Val.ToInt(lblAssF.Text);
                _clsTaskDet.SrNo = Val.ToInt(lblSrNo.Text);
                _clsTaskDet.TaskName = txtTaskName.Text;
                _clsTaskDet.TaskDet = txtTaskDetails.Text;
                _clsTaskDet.DevRemark = txtDevRemark.Text;
                _clsTaskDet.AssDate = txtAssDate.Text;
                _clsTaskDet.TrfDate = txtTrfDate.Text;
                _clsTaskDet.AssToUCODE = Val.ToInt(lblAssT.Text);
                _clsTaskDet.Priority = Val.ToInt(lblPriorityCode.Text);
                _clsTaskDet.WorkingOn = Val.ToBool(lblWorkingOn.Text);
                _clsTaskDet.TaskComplate = Val.ToBool(lblTaskComplate.Text);

            }
            catch (Exception ex)
            {
            }
        }
        private void CmbWorkingOn_Click(object sender, EventArgs e)
        {
            if (CmbWorkingOn.Appearance.BackColor == System.Drawing.Color.Transparent)
            {
                lblWorkingOn.Text = "true";
                lblTaskComplate.Text = "false";
                AssignToVar();
                mBoTaskDet.NewTaskSave(_clsTaskDet);
                CmbTaskComplete.Appearance.BackColor = System.Drawing.Color.Transparent;
                CmbWorkingOn.Appearance.BackColor = System.Drawing.Color.FromArgb(179, 217, 255);
            }
            else
            {
                lblWorkingOn.Text = "false";
                AssignToVar();
                mBoTaskDet.NewTaskSave(_clsTaskDet);
                CmbWorkingOn.Appearance.BackColor = System.Drawing.Color.Transparent;
            }
            OptType_ValueChanged(null, null);

        }
        private void CmbTaskComplete_Click(object sender, EventArgs e)
        {
            if (CmbTaskComplete.Appearance.BackColor == System.Drawing.Color.Transparent)
            {
                lblWorkingOn.Text = "false";
                lblTaskComplate.Text = "true";
                AssignToVar();
                mBoTaskDet.NewTaskSave(_clsTaskDet);
                CmbWorkingOn.Appearance.BackColor = System.Drawing.Color.Transparent;
                CmbTaskComplete.Appearance.BackColor = System.Drawing.Color.FromArgb(173, 235, 173);
            }
            else
            {
                lblTaskComplate.Text = "false";
                AssignToVar();
                mBoTaskDet.NewTaskSave(_clsTaskDet);
                CmbTaskComplete.Appearance.BackColor = System.Drawing.Color.Transparent;
            }
            OptType_ValueChanged(null, null);
        }

        private void btnTaskSave_Click(object sender, EventArgs e)
        {
            try
            {
                clsTaskDet cls = new clsTaskDet();
                cls.CmpCode = Val.ToInt(lblCmpCode1.Text);
                cls.UCODE = Glb.GIntEndUserCode;
                cls.SrNo = mBoTaskDet.GetNewSrNo(cls.CmpCode);
                cls.TaskName = txtTaskName1.Text;
                cls.TaskDet = txtTaskDetails1.Text;
                cls.DevRemark = "";
                cls.AssDate = txtAssDate1.Text;
                cls.TrfDate = "";
                cls.AssToUCODE = 0;
                cls.Priority = Val.ToInt(lblPriorityCode1.Text);
                cls.WorkingOn = false;
                cls.TaskComplate = false;
                cls.FilePath = FilesPathSave;

                mBoTaskDet.NewTaskSave(cls);
                OptType_ValueChanged(null, null);

            }
            catch (Exception ex)
            {

            }

            finally
            {
                ClearTaskSave();
            }
        }
        private void ClearTaskSave()
        {
            txtTaskDetails1.Text = "";
            txtTaskName1.Text = "";
            lblCmpName1.Text = "";
            lblCmpCode1.Text = "";
            txtSourcePath.Text = "";
            UltGrpTaskSave.Size = new Size(463, 45);
            UltGrpTaskSave.Visible = false;
        }
        private void ClearTaskDet()
        {
            lblCmpName.Text = "";
            lblSrNo.Text = "";
            lblCmpCode.Text = "";
            lblTaskUCode.Text = "";
            txtTaskName.Text = "";
            txtTaskDetails.Text = "";
            UltcmbAssF.Text = "";
            UltcmbAssT.Text = "";
            txtAssDate.Text = "";
            txtTrfDate.Text = "";
            txtTrfDays.Text = "";
            txtAssDays.Text = "";
            txtDevRemark.Text = "";
            UltCmbPriority.Text = "";
            lblWorkingOn.Text = "";
            lblTaskComplate.Text = "";
            CmbWorkingOn.Appearance.BackColor = Color.Transparent;
            CmbTaskComplete.Appearance.BackColor = Color.Transparent;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Home_Load(null, null);
            ClearTaskDet();
        }

        private void UltGrdCmpTask_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            if (OptType.StrPrcCode.ToUpper() != "WORK")
            {
                e.DisplayPromptMsg = false;
                if (Glb.GStrEndUserCat == "S" || Glb.GStrEndUserCat == "A" ||  Glb.GStrEndUserId== UltGrdCmpTask.ActiveRow.Cells["UNAME"].Text)
                {

                    DialogResult DlgmsgCnf = Val.Conf("Are You Sure Delete  ? ");
                    if (DlgmsgCnf == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (UltraGridRow uRow in UltGrdCmpTask.Selected.Rows)
                        {
                            _clsTaskDet.CmpCode = Val.ToInt(uRow.Cells["CmpCode"].Text);
                            _clsTaskDet.UCODE = Val.ToInt(uRow.Cells["UCODE"].Text);
                            _clsTaskDet.SrNo = Val.ToInt(uRow.Cells["SrNo"].Text);
                            mBoTaskDet.TaskDelete(_clsTaskDet);
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }

                }
                else
                {
                    e.Cancel = true;
                }
            }
        }


        private void UltGrdCmpTask_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (OptType.StrPrcCode.ToUpper() == "WORK")
            {
                lblTaskUCode.Text = UltGrdCmpTask.ActiveRow.Cells["UCODE"].Value.ToString();
                UltcmbAssF.Text = UltGrdCmpTask.ActiveRow.Cells["UNAME"].Value.ToString();

                lblCmpName.Text = UltGrdCmpTask.ActiveRow.Cells["CmpName"].Value.ToString();
                lblCmpCode.Text = UltGrdCmpTask.ActiveRow.Cells["CmpCode"].Value.ToString();
                lblSrNo.Text = UltGrdCmpTask.ActiveRow.Cells["SrNo"].Value.ToString();
                txtAssDate.Text = UltGrdCmpTask.ActiveRow.Cells["IDate"].Value.ToString();
                txtTaskName.Text = UltGrdCmpTask.ActiveRow.Cells["TaskName"].Value.ToString();
                txtTaskDetails.Text = UltGrdCmpTask.ActiveRow.Cells["TaskDet"].Value.ToString();

            }
            else
            {
                IsEntryExists = true;
                
                lblTaskUCode.Text = UltGrdCmpTask.ActiveRow.Cells["UCODE"].Value.ToString();

                lblCmpName.Text = UltGrdCmpTask.ActiveRow.Cells["CmpName"].Value.ToString();
                lblCmpCode.Text = UltGrdCmpTask.ActiveRow.Cells["CmpCode"].Value.ToString();
                lblSrNo.Text = UltGrdCmpTask.ActiveRow.Cells["SrNo"].Value.ToString();
                txtAssDate.Text = UltGrdCmpTask.ActiveRow.Cells["AssDate"].Value.ToString();
                txtTrfDate.Text = UltGrdCmpTask.ActiveRow.Cells["TrfDate"].Value.ToString();
                UltcmbAssF.Text = UltGrdCmpTask.ActiveRow.Cells["UNAME"].Value.ToString();

                txtTaskName.Text = UltGrdCmpTask.ActiveRow.Cells["TaskName"].Value.ToString();
                UltcmbAssT.Text = UltGrdCmpTask.ActiveRow.Cells["AssToUNAME"].Value.ToString();
                txtTaskDetails.Text = UltGrdCmpTask.ActiveRow.Cells["TaskDet"].Value.ToString();
                txtDevRemark.Text = UltGrdCmpTask.ActiveRow.Cells["DevRemark"].Value.ToString();
                UltCmbPriority.Text = UltGrdCmpTask.ActiveRow.Cells["PriorityName"].Value.ToString();
                txtAssDays.Text = UltGrdCmpTask.ActiveRow.Cells["AssDays"].Value.ToString();
                txtTrfDays.Text = UltGrdCmpTask.ActiveRow.Cells["TrfDays"].Value.ToString();
                lblWorkingOn.Text = UltGrdCmpTask.ActiveRow.Cells["WorkingOn"].Text;
                lblTaskComplate.Text = UltGrdCmpTask.ActiveRow.Cells["TaskComplate"].Text;
                if (Val.ToBoolToInt(UltGrdCmpTask.ActiveRow.Cells["WorkingOn"].Value) == 1)
                {
                    CmbWorkingOn.Appearance.BackColor = System.Drawing.Color.FromArgb(179, 217, 255);
                }
                else
                {
                    CmbWorkingOn.Appearance.BackColor = System.Drawing.Color.Transparent;
                }
                if (Val.ToBoolToInt(UltGrdCmpTask.ActiveRow.Cells["TaskComplate"].Value) == 1)
                {
                    CmbTaskComplete.Appearance.BackColor = System.Drawing.Color.FromArgb(173, 235, 173);
                }
                else
                {
                    CmbTaskComplete.Appearance.BackColor = System.Drawing.Color.Transparent;
                }
                if (UltGrdCmpTask.ActiveRow.Cells["TargetPath"].Text != "")
                {
                    UltCmbDownload.Visible = true;
                    lblfiles.Visible = true;

                    SetDownload(Val.ToInt(lblTaskUCode.Text), Val.ToInt(lblCmpCode.Text), Val.ToInt(lblSrNo.Text));
                }
                else
                {
                    UltCmbDownload.Visible = false;
                    lblfiles.Visible = false;

                }
                SetPermition();
                IsEntryExists = false;
            }
            if (Glb.GStrEndUserCat != "S" && Glb.GStrEndUserCat != "A")
            {
                if (((lblAssF.Text != lblAssT.Text && lblAssT.Text != "" && lblAssT.Text != Glb.GIntEndUserCode.ToString()) || (lblAssT.Text != "" && lblAssT.Text != Glb.GIntEndUserCode.ToString())) && (Glb.GStrEndUserCat != "S" || Glb.GStrEndUserCat != "A"))
                {
                    CmbWorkingOn.Enabled = false;
                    CmbTaskComplete.Enabled = false;
                }
                else
                {
                    CmbWorkingOn.Enabled = true;
                    CmbTaskComplete.Enabled = true;
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearTaskSave();
            UltGrpTaskSave.Visible = false;
        }

        private void UltGrdCmp_ClickCell(object sender, ClickCellEventArgs e)
        {
            FillCmpTask(Val.ToInt(UltGrdCmp.ActiveRow.Cells["CmpCode"].Text), OptType.StrPrcCode);

        }

        private void UltGrdCmpTask_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (OptType.StrPrcCode.ToUpper() != "WORK")
            {
                if (Val.ToInt(e.Row.Cells["AssDays"].Value.ToString().Replace(" Days", "")) >= 5)
                {
                    e.Row.Cells["AssDays"].Appearance.ForeColor = Color.Red;
                }
                if (Val.ToInt(e.Row.Cells["TrfDays"].Value.ToString().Replace(" Days", "")) >= 5)
                {
                    e.Row.Cells["TrfDays"].Appearance.ForeColor = Color.Red;
                }

                if (Val.ToBoolToInt(e.Row.Cells["TaskComplate"].Value.ToString()) == 1)
                {
                    e.Row.Cells["TaskComplateImg"].Appearance.Image = global::TaskControl.Properties.Resources.checkmark;
                    e.Row.Cells["TaskComplateImg"].Appearance.ForeColor = Color.Green;
                    e.Row.Cells["AssDays"].Appearance.ForeColor = Color.Green;
                    e.Row.Cells["TrfDays"].Appearance.ForeColor = Color.Green;
                }

                if (Val.ToBoolToInt(e.Row.Cells["WorkingOn"].Value.ToString()) == 1)
                {
                    e.Row.Appearance.BackColor = Color.FromArgb(179, 217, 255);
                }
            }
        }

        private void UltGrdCmp_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            e.Row.Cells["New"].Appearance.Image = global::TaskControl.Properties.Resources.Plus;
        }



        private void UltGrd_AfterRowsDeleted(object sender, EventArgs e)
        {

            switch (OptAdminType.StrPrcCode.ToString().ToUpper())
            {
                //nTakaNo = Val.ToString(uRow.Cells["TakaNo"].Value);
                case "VIEWPARA":
                    BOFrmMain.Delete(BOFrmMain.TableViewPara);
                    break;
                case "CMPMAST":
                    BOFrmMain.Delete(BOFrmMain.TableCmp);
                    break;
                case "PRIORITYMAST":
                    BOFrmMain.Delete(BOFrmMain.TablePriority);
                    break;
                case "USERMAST":
                    BOFrmMain.Delete(BOFrmMain.TablePass);
                    break;


            }

        }

        private void UltGrd_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
        {

            switch (OptAdminType.StrPrcCode.ToString().ToUpper())
            {

                case "CMPMAST":
                    if (UltGrd.ActiveRow.Cells["CmpCode"].Value == "")
                    {
                        UltGrd.ActiveRow.Cells["CmpCode"].Value = BOFrmMain.GetMaxId(BOFrmMain.TableCmp, "CmpCode");
                    }
                    break;
                case "PRIORITYMAST":
                    if (UltGrd.ActiveRow.Cells["PriorityCode"].Value == "")
                    {
                        UltGrd.ActiveRow.Cells["PriorityCode"].Value = BOFrmMain.GetMaxId(BOFrmMain.TablePriority, "PriorityCode");
                    }
                    break;
                case "USERMAST":
                    if (UltGrd.ActiveRow.Cells["UCODE"].Value == "")
                    {
                        UltGrd.ActiveRow.Cells["UCODE"].Value = BOFrmMain.GetMaxId(BOFrmMain.TablePass, "UCODE");
                    }
                    break;
            }
        }

        private void OptAdminType_ValueChanged(object sender, EventArgs e)
        {
            switch (OptAdminType.StrPrcCode.ToString().ToUpper())
            {
                //nTakaNo = Val.ToString(uRow.Cells["TakaNo"].Value);
                case "VIEWPARA":
                    BOFrmMain.FillViewPara("");
                    UltGrd.SetDataBinding(BOFrmMain.DS, BOFrmMain.TableViewPara, true);

                    UltGrd.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
                    this.UltGrd.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                    this.UltGrd.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Edit;
                    txtPass_ValueChanged(null, null);
                    Global.SetGridColumns(UltGrd, mboViewPara.GetViewColumns("VIEWPARA"));


                    break;
                case "CMPMAST":
                    BOFrmMain.FillCmp();
                    UltGrd.SetDataBinding(BOFrmMain.DS, BOFrmMain.TableCmp, true);
                    UltGrd.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;

                    this.UltGrd.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                    this.UltGrd.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Edit;
                    txtPass_ValueChanged(null, null);
                    Global.SetGridColumns(UltGrd, mboViewPara.GetViewColumns("CMPMAST"));

                    break;
                case "PRIORITYMAST":
                    BOFrmMain.FillPriority();
                    UltGrd.SetDataBinding(BOFrmMain.DS, BOFrmMain.TablePriority, true);
                    UltGrd.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
                    this.UltGrd.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                    this.UltGrd.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Edit;
                    txtPass_ValueChanged(null, null);
                    Global.SetGridColumns(UltGrd, mboViewPara.GetViewColumns("PRIORITYMAST"));

                    break;
                case "USERMAST":
                    BOFrmMain.FillUser();
                    foreach (DataRow dr in BOFrmMain.DS.Tables[BOFrmMain.TablePass].Rows)
                    {
                        dr["U_PASS"] = Ope.ENCODE_DECODE(dr["U_PASS"].ToString(), "D");
                    }
                    UltGrd.SetDataBinding(BOFrmMain.DS, BOFrmMain.TablePass, true);
                    UltGrd.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
                    this.UltGrd.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                    this.UltGrd.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Edit;
                    txtPass_ValueChanged(null, null);
                    Global.SetGridColumns(UltGrd, mboViewPara.GetViewColumns("USERMAST"));
                    break;

            }
        }

        private void UltGrd_AfterRowUpdate(object sender, RowEventArgs e)
        {

            switch (OptAdminType.StrPrcCode.ToString().ToUpper())
            {
                //nTakaNo = Val.ToString(uRow.Cells["TakaNo"].Value);
                case "VIEWPARA":
                    BOFrmMain.Update(BOFrmMain.TableViewPara);
                    break;
                case "CMPMAST":
                    BOFrmMain.Update(BOFrmMain.TableCmp);
                    break;
                case "PRIORITYMAST":
                    BOFrmMain.Update(BOFrmMain.TablePriority);
                    break;
                case "USERMAST":
                    foreach (DataRow dr in BOFrmMain.DS.Tables[BOFrmMain.TablePass].Rows)
                    {
                        dr["U_PASS"] = Ope.ENCODE_DECODE(dr["U_PASS"].ToString(), "E");
                        if (UltGrd.ActiveRow.Index != BOFrmMain.DS.Tables[BOFrmMain.TablePass].Rows.IndexOf(dr))
                        {
                            dr.RejectChanges();
                        }
                    }
                    BOFrmMain.Update(BOFrmMain.TablePass);
                    OptAdminType_ValueChanged(null, null);
                    break;
            }
        }

        private void UltTabCtr_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            OptAdminType_ValueChanged(null, null);
        }

        private void btnAddWork_Click(object sender, EventArgs e)
        {
            OptType.CheckedIndex = 4;

            UltGrpAddWork.Size = UltGrdCmpTask.Size;
            UltGrpAddWork.Width = UltGrdCmpTask.Width;
            UltGrpAddWork.Location = UltGrdCmpTask.Location;
            UltGrpAddWork.Dock = UltGrdCmpTask.Dock;
            UltGrpAddWork.Anchor = UltGrdCmpTask.Anchor;
            UltGrpAddWork.Visible = true;
            txtAddWorkDate.Focus();
            ClearTaskDet();
        }

        private void UltCmbPriority1_ValueChanged(object sender, EventArgs e)
        {
            if (Val.ToInt(lblPriorityCode1.Text) == 1)
            {
                lblCmbPriority1.Appearance.Image = global::TaskControl.Properties.Resources.up_arrow;
            }
            else if (Val.ToInt(lblPriorityCode1.Text) == 2)
            {
                lblCmbPriority1.Appearance.Image = global::TaskControl.Properties.Resources.minus;
            }
            else if (Val.ToInt(lblPriorityCode1.Text) == 3)
            {
                lblCmbPriority1.Appearance.Image = global::TaskControl.Properties.Resources.down_left;
            }
        }

        private void UltcmbAssT_ValueChanged(object sender, EventArgs e)
        {
            if (IsEntryExists == false)
            {
                if (Glb.GStrEndUserCat == "S" || Glb.GStrEndUserCat == "A")
                {
                    txtTrfDate.Enabled = true;
                    txtTrfDate.Text = System.DateTime.Today.Date.ToString();
                    txtTrfDate.Refresh();
                    txtTrfDate.Enabled = false;
                    AssignToVar();

                    mBoTaskDet.NewTaskSave(_clsTaskDet);
                    OptType_ValueChanged(null, null);
                }
                else if (lblAssF.Text != lblAssT.Text && Glb.GIntEndUserCode != Val.ToInt(lblAssF.Text))
                {
                    return;
                }
                else
                {
                    txtTrfDate.Enabled = true;
                    txtTrfDate.Text = System.DateTime.Today.Date.ToString();
                    txtTrfDate.Refresh();
                    txtTrfDate.Enabled = false;
                    AssignToVar();

                    mBoTaskDet.NewTaskSave(_clsTaskDet);
                    OptType_ValueChanged(null, null);
                }
            }
        }

        private void UltCmbPriority_ValueChanged(object sender, EventArgs e)
        {
            if (Val.ToInt(lblPriorityCode.Text) == 1)
            {
                lblCmbPriority.Appearance.Image = global::TaskControl.Properties.Resources.up_arrow;
            }
            else if (Val.ToInt(lblPriorityCode.Text) == 2)
            {
                lblCmbPriority.Appearance.Image = global::TaskControl.Properties.Resources.minus;
            }
            else if (Val.ToInt(lblPriorityCode.Text) == 3)
            {
                lblCmbPriority.Appearance.Image = global::TaskControl.Properties.Resources.down_left;
            }
            else
            {
                lblCmbPriority.Appearance.Image = null;
            }
            //if (IsEntryExists == false)
            //{
            //    AssignToVar();
            //    mBoTaskDet.NewTaskSave(_clsTaskDet);
            //    //OptType_ValueChanged(null, null);
            //}
        }
        public void clearAddWork()
        {
            UltCmbAddWorkCmp.Text = "";
            txtAddWorkTaskDet.Text = "";
            txtAddWorkTaskName.Text = "";
        }
        private void CmdAddWorkSave_Click(object sender, EventArgs e)
        {

            clsTaskDet cls = new clsTaskDet();
            cls.CmpCode = Val.ToInt(lblAddWorkCmpCode.Text);
            cls.UCODE = Glb.GIntEndUserCode;
            cls.SrNo = mBoTaskDet.GetAddWorkSrNo(cls.CmpCode);
            cls.TaskName = txtAddWorkTaskName.Text;
            cls.TaskDet = txtAddWorkTaskDet.Text;
            cls.IDate = txtAddWorkDate.Text;

            mBoTaskDet.AddWorkSave(cls);
            clearAddWork();
            UltGrpAddWork.Visible = false;
            OptType_ValueChanged(null, null);
        }

        private void CmdAddWorkCancel_Click(object sender, EventArgs e)
        {
            clearAddWork();
            UltGrpAddWork.Visible = false;
        }

        private void UltGrdCmpTask_AfterRowsDeleted(object sender, EventArgs e)
        {
            if (OptType.StrPrcCode.ToUpper() == "WORK")
            {
                mBoTaskDet.Delete();
            }
        }

        private void UltGrdCmpTask_AfterRowUpdate(object sender, RowEventArgs e)
        {
            if (OptType.StrPrcCode.ToUpper() == "WORK")
            {
                mBoTaskDet.Update();
            }
        }

        private void CmdUpdate_Click(object sender, EventArgs e)
        {
            if (OptType.StrPrcCode.ToUpper() == "WORK")
            {
                clsTaskDet cls = new clsTaskDet();
                cls.CmpCode = Val.ToInt(lblCmpCode.Text);
                cls.UCODE = Glb.GIntEndUserCode;
                cls.SrNo = Val.ToInt(lblSrNo.Text);
                cls.TaskName = txtTaskName.Text;
                cls.TaskDet = txtTaskDetails.Text;
                cls.IDate = txtAssDate.Text;

                mBoTaskDet.AddWorkSave(cls);
                clearAddWork();
            }
            else
            {
                if (IsEntryExists == false)
                {
                    AssignToVar();
                    mBoTaskDet.NewTaskSave(_clsTaskDet);
                    OptType_ValueChanged(null, null);
                }
            }
            UltCmbPriority_ValueChanged(null, null);
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            txtSourcePath.Text = "";
            FilesPathSave = "";
            try
            {
                strTargetPath = @"\\" + Glb.GStrComputerName + "\\Users\\TaskControl\\Files\\";
                strTargetPath += Glb.GStrEndUserId;
                if (!Directory.Exists(strTargetPath))
                {
                    Directory.CreateDirectory(strTargetPath);
                }

                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Title = "Browse Files";
                //openFileDialog1.DefaultExt = "jpg";
                //openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.Multiselect = true;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    int i = 0;
                    foreach (string st in openFileDialog1.SafeFileNames)
                    {
                        string sourcepath = openFileDialog1.FileNames[i];
                        txtSourcePath.Text += st + " - ";
                        File.Copy(sourcepath, strTargetPath + "\\" + st, true);
                        FilesPathSave += strTargetPath + "\\" + st + ",";
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Val.Message(ex.Message, Glb.GStrMsgCaption, MessageBoxIcon.Information);
            }
        }

        private void UltCmbDownload_ValueChanged(object sender, EventArgs e)
        {
            
                DialogResult DlgmsgCnf = Val.Conf("Are You Sure To Download  ? ");
                if (DlgmsgCnf == System.Windows.Forms.DialogResult.Yes)
                {
                    //string mstrTargetPath = UltGrdView.ActiveRow.Cells["TargetPath"].Value.ToString();
                    string DownloadPath = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString() + "\\TaskControl";
                    if (!Directory.Exists(DownloadPath))
                    {
                        Directory.CreateDirectory(DownloadPath);
                    }
                    File.Copy(lblDownloadFPath.Text, DownloadPath + "\\" + UltCmbDownload.Text, true);
                    File.SetAttributes(DownloadPath + "\\" + UltCmbDownload.Text, FileAttributes.Normal);
                    Val.Message("File Download Succesfull", Glb.GStrMsgCaption, MessageBoxIcon.Exclamation);
                }
            
           
        }

    }
}
