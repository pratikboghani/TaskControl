using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.SupportDialogs.FilterUIProvider;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using System.Linq;

namespace UTC
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UTCGrid : Infragistics.Win.UltraWinGrid.UltraGrid
    {

        #region Color

        public struct Colors
        {
            public static System.Drawing.Color Red = System.Drawing.Color.Red;
            public static System.Drawing.Color Brown = System.Drawing.Color.Brown;
            public static System.Drawing.Color Olive = System.Drawing.Color.Olive;
            public static System.Drawing.Color DarkOliveGreen = System.Drawing.Color.DarkOliveGreen;
            public static System.Drawing.Color DarkCyan = System.Drawing.Color.DarkCyan;
            public static System.Drawing.Color SlateGray = System.Drawing.Color.SlateGray;
            public static System.Drawing.Color Navy = System.Drawing.Color.Navy;
            public static System.Drawing.Color Blue = System.Drawing.Color.Blue;
            public static System.Drawing.Color Indigo = System.Drawing.Color.Indigo;
            public static System.Drawing.Color Purple = System.Drawing.Color.Purple;
            public static System.Drawing.Color Crimson = System.Drawing.Color.Crimson;
            public static System.Drawing.Color ForestGreen = System.Drawing.Color.ForestGreen;
            public static System.Drawing.Color Linen = System.Drawing.Color.Linen;
        }

        #endregion

        #region Variables
        public Colors ColorsF;
        private int mIntDigit = 0, mIntLaUTCellIndForExit = -1;
        private EnumBounUnbound EnumBoUnBMode;
        private EnumMultiSelect MultiSelect;
        private EnumSelectedFieldsUpdation EnumSelFiUpd;
        public string mStrExceptField = String.Empty;
        private Boolean _BlnIsCellActivated;        
        private string _ActiveCellName = "";
        private DataTable dtSortColumn = null;
        private DataTable dtGridSource = null;

        private string CopyCellName = "";
        private string CopyCellValue = "";


        [Description("Occurs When Right Click On Grid's Extra Menu Click")]
        public event EventHandler ShowColumns;

        Infragistics.Win.SupportDialogs.FilterUIProvider.UltraGridFilterUIProvider ultGrdFilter = new Infragistics.Win.SupportDialogs.FilterUIProvider.UltraGridFilterUIProvider();

        #endregion

        #region Enum
        public enum EnumBounUnbound
        {
            Default = 0,
            SPBoundMode = 1,
            BoundMode = 2,
            UnBoundMode = 3            
        }
        public enum EnumSelectedFieldsUpdation
        {
            Default = 0,
            SelectedFields = 1
        }
        public enum EnumCellActivation
        {
            /// <summary>
            /// Enum Active Only
            /// </summary>
            ActiveOnly = 1,

            /// <summary>
            /// Enum Allow Edit
            /// </summary>
            AllowEdit = 0,
            /// <summary>
            /// Enum Disable
            /// </summary>
            Disable = 2,
            /// <summary>
            /// Enum NoEdit
            /// </summary>
            NoEdit = 3
        }
        public enum EnumMultiSelect
        {
            No = 0,
            Yes = 1
        }
        #endregion

        #region Property
        bool _ByPassDownArrayKey = false;
        [Browsable(false)]
        public bool ByPassDownArrayKey
        {
            get { return _ByPassDownArrayKey; }
            set { _ByPassDownArrayKey = value; }
        }

        [Browsable(true)]
        public EnumBounUnbound DataBoundMode
        {
            get { return EnumBoUnBMode; }
            set { EnumBoUnBMode = value; }
        }

          [Browsable(true)]
       // IsGroupHeaderExported
//
          private bool  _IsGroupHeaderExported=true ;
        public Boolean IsGroupHeaderExported
        {
            get { return _IsGroupHeaderExported; }
            set { _IsGroupHeaderExported = value; }
        }
        [Browsable(true)]
        public string ActiveCellName
        {
            get { return _ActiveCellName; }
            set { _ActiveCellName = value; }
        }

        public EnumSelectedFieldsUpdation SelectedUpdate
        {
            get { return EnumSelFiUpd; }
            set { EnumSelFiUpd = value; }
        }

        [Browsable(true)]        
        public Boolean IsCellActivated
        {
            get { return _BlnIsCellActivated; }
            set { _BlnIsCellActivated = value; }
        }

        [Browsable(true)]        
        private bool  _BlnPersistBackColor=false ;
        public Boolean PersistBackColor
        {
            get { return _BlnPersistBackColor; }
            set { _BlnPersistBackColor = value; }
        }


        [Browsable(true)]
        public EnumMultiSelect RowMultiSelectWithMouse
        {
            get { return MultiSelect; }
            set { MultiSelect = value; }
        }

        private bool _BlnIsCapital = true;
        [Browsable(true)]
        public Boolean IsCapital
        {
            get { return _BlnIsCapital; }
            set { _BlnIsCapital = value; }
        }

        private bool _IsRightClickEnabled = true;
        [Browsable(true)]
        public Boolean IsRightClickEnabled
        {
            get { return _IsRightClickEnabled; }
            set { _IsRightClickEnabled = value; }
        }
        private bool _IsRightClickVisible = true;
        [Browsable(true)]
        public Boolean IsRightClickVisible
        {
            get { return _IsRightClickVisible; }
            set { _IsRightClickVisible = value; }
        }
        private bool _IsSetColumnEnabled = false ;
        [Browsable(true)]
        public Boolean IsSetColumnEnabled
        {
            get { return _IsSetColumnEnabled; }
            set { _IsSetColumnEnabled = value; }
        }

        private bool _IsHideAdvanceOption;
        [Browsable(true)]
        public bool IsHideAdvanceOption
        {
            get
            {
                return _IsHideAdvanceOption;
            }
            set
            {
                _IsHideAdvanceOption = value;
            }
        }


        private string _ToolTips = "";
        /// <summary>
        /// Tool Tips
        /// </summary>
        public string ToolTips
        {
            get { return _ToolTips; }
            set
            {
                _ToolTips = value;
                System.Windows.Forms.ToolTip TT1 = new System.Windows.Forms.ToolTip();
                TT1.SetToolTip(this, _ToolTips);
            }
        }

        [Browsable(true)]
        private bool _BlnExcelStyle = false;
        public Boolean EnableExcelStyleEnter
        {
            get { return _BlnExcelStyle; }
            set { _BlnExcelStyle = value; }
        }

        private bool _allowNegative = false;
        [Browsable(true)]
        public bool AllowNegativeValue
        {
            get { return _allowNegative; }
            set { _allowNegative = value; }
        }

        private bool _promptSaveDialog = false;
        [Browsable(true)]
        public bool PromptSaveDialog
        {
            get { return _promptSaveDialog; }
            set { _promptSaveDialog = value; }
        }
        private Boolean _IsCopyPaste = false;
        [Browsable(true)]
        public bool IsCopyPaste
        {
            get { return _IsCopyPaste; }
            set { _IsCopyPaste = value; }
        }
        private bool _IsEditMode = false;
        [Browsable(true)]
        public bool IsEditMode
        {
            get { return _IsEditMode; }
            set { _IsEditMode = value; }
        }

        private void EditModetoolStripMenuItem_Click_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            if (menuItem.CheckState == CheckState.Checked)
                _IsEditMode = true;
            else
                _IsEditMode = false;
        }
        #endregion

        #region Context Menu Events

        private void freezeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.DisplayLayout.UseFixedHeaders == true)
            {
                this.DisplayLayout.UseFixedHeaders = false;
            }
            else
            {
                this.DisplayLayout.UseFixedHeaders = true;
            }
        }
        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

            foreach (Infragistics.Win.UltraWinGrid.UltraGridBand band in this.DisplayLayout.Bands)
            {
                //will clear the filters
                band.ColumnFilters.ClearAllFilters();
            }            
            this.DisplayLayout.Override.AllowRowFiltering =Infragistics.Win.DefaultableBoolean.False;
            if (menuItem.CheckState == CheckState.Checked)
            {
                this.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            }
        }
       
        private void FilterToptoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            foreach (Infragistics.Win.UltraWinGrid.UltraGridBand band in this.DisplayLayout.Bands)
            {
                //will clear the filters
                band.ColumnFilters.ClearAllFilters();
            }
            this.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            this.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.Default ;

            if (menuItem.CheckState == CheckState.Checked)
            {
                //Enable row filtering to demonstrate this sample
                this.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;

                //this.DisplayLayout.Override.FilterClearButtonLocation = FilterClearButtonLocation.Row;

                //Row filters by way of column header icons
                this.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;      
            }
        }
        private void FilterExceltoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            
            foreach (Infragistics.Win.UltraWinGrid.UltraGridBand band in this.DisplayLayout.Bands)
            {
                //will clear the filters
                band.ColumnFilters.ClearAllFilters();
            }            
            this.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            this.DisplayLayout.Override.FilterUIProvider =null ;
            if (menuItem.CheckState == CheckState.Unchecked)            
                ultGrdFilter.AfterMenuPopulate -= new Infragistics.Win.SupportDialogs.FilterUIProvider.AfterMenuPopulateEventHandler(OnAfterMenuPopulate);
            

            if (menuItem.CheckState ==CheckState.Checked)
            {             
                //Assign the FilterUIProvider to use the new Excel style filtering from UltraGridFilterUIProvider           
                //Infragistics.Win.SupportDialogs.FilterUIProvider.UltraGridFilterUIProvider ultGrdFilter = new Infragistics.Win.SupportDialogs.FilterUIProvider.UltraGridFilterUIProvider();
                ultGrdFilter.AfterMenuPopulate += new Infragistics.Win.SupportDialogs.FilterUIProvider.AfterMenuPopulateEventHandler(OnAfterMenuPopulate);

                this.DisplayLayout.Override.FilterUIProvider = ultGrdFilter;

                //Enable row filtering to demonstrate this sample
                this.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;

                //Row filters by way of column header icons
                this.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.HeaderIcons;               
            }
        }
       private void OnAfterMenuPopulate(object sender, AfterMenuPopulateEventArgs e)
       {
           FilterUIProviderTreeSettings treeSettings = ultGrdFilter.TreeSettings;
           treeSettings.Appearance.FontData.Name = e.ColumnFilter.Column.CellAppearance.FontData.Name; 
           treeSettings.Appearance.FontData.SizeInPoints = e.ColumnFilter.Column.CellAppearance.FontData.SizeInPoints;
       }
        
        private void ExporttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "";
                Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter();
                ultraGridExcelExporter.BeginExport += new Infragistics.Win.UltraWinGrid.ExcelExport.BeginExportEventHandler(OnBeginExport);
                ultraGridExcelExporter.RowExporting += new Infragistics.Win.UltraWinGrid.ExcelExport.RowExportingEventHandler(OnRowExporting);
                ultraGridExcelExporter.EndExport += new Infragistics.Win.UltraWinGrid.ExcelExport.EndExportEventHandler(OnEndExport);
                ultraGridExcelExporter.CellExported += new Infragistics.Win.UltraWinGrid.ExcelExport.CellExportedEventHandler(OnCellExported);
                
                fileName = this.Name + ".xls";
                if (_promptSaveDialog)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Filter = "Excel File (.xls)|*.xls";
                    //dlg.DefaultExt = "xls";
                    dlg.ShowDialog();
                    fileName = dlg.FileName;
                }
                ultraGridExcelExporter.Export(this, fileName);
                System.Diagnostics.Process.Start(fileName);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }
        private void toolStripGrpBoxMenuItem_Click(object sender, EventArgs e)
        {
            if (this.DisplayLayout.ViewStyleBand == ViewStyleBand.OutlookGroupBy)
            {
                this.DisplayLayout.ViewStyleBand = ViewStyleBand.Vertical;
                this.DisplayLayout.GroupByBox.Hidden = true ;
            }
            else
            {
                this.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
                this.DisplayLayout.GroupByBox.Hidden = false;
            }
        }
        private static void OnBeginExport(object sender, Infragistics.Win.UltraWinGrid.ExcelExport.BeginExportEventArgs e)
        {
            ((UltraGridExcelExporter)sender).BandSpacing = BandSpacing.None;
            BandEnumerator enumerator = e.Layout.Bands.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    UltraGridBand current = enumerator.Current;
                    current.Indentation = 0;
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            ((UltraGridExcelExporter)sender).OutliningStyle = OutliningStyle.None;
        }
        private void OnRowExporting(object sender, Infragistics.Win.UltraWinGrid.ExcelExport.RowExportingEventArgs e)
        {
            if (!IsGroupHeaderExported && e.GridRow.IsGroupByRow)
            {
                e.Cancel = true;
            }
        }
        private static void OnCellExported(object sender, Infragistics.Win.UltraWinGrid.ExcelExport.CellExportedEventArgs e)
        {            
            // Get the cell being exporter 
            UltraGridCell cell = e.GridRow.Cells[e.GridColumn];

            // Get the Merged cells. 
            UltraGridCell[] mergedCells = cell.GetMergedCells();

            // Check to see if there are any merged cells. If not, we don't need to do anything. 
            if (mergedCells != null)
            {
                // There are merged cells. Check to see if the cell being exported is the last one. 
                bool isLaUTCell = true;
                foreach (UltraGridCell mergedCell in mergedCells)
                {
                    if (cell.Row.Index < mergedCell.Row.Index)
                    {
                        // If the cell being exported has a lower index that any of the cells 
                        // it is merged with, then it's not the last cell. 
                        isLaUTCell = false;
                        break;
                    }
                }

                // If it is the last cell, merge this cell with the cells above it based on the 
                // count of the merged cells. 
                if (isLaUTCell)
                {
                    int rowIndex = e.CurrentRowIndex;
                    int colIndex = e.CurrentColumnIndex;
                    e.CurrentWorksheet.MergedCellsRegions.Add(
                        rowIndex - (mergedCells.Length - 1),
                        colIndex,
                        rowIndex,
                        colIndex).CellFormat.VerticalAlignment = Infragistics.Documents.Excel.VerticalCellAlignment.Center;
                    
                }
            }
        }
        private static void OnEndExport(object sender, Infragistics.Win.UltraWinGrid.ExcelExport.EndExportEventArgs e)
        {                        
            MessageBox.Show("File Successfully Exported");
        }

        private void SortingtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            if(menuItem.CheckState ==CheckState.Checked)
                this.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortSingle;
            else
                this.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.Select ;
        }

        private void SortMultitoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dtGridSource == null)
            {
                dtGridSource = new DataTable("tblSort");
                dtGridSource.Columns.Add("COLUMN_NAME", typeof(System.String));                               
                dtGridSource.Columns.Add("ORDER", typeof(System.String));
            }

            if (dtSortColumn == null)
                dtSortColumn = CreateColsTable();

            FrmSort f = new FrmSort(dtSortColumn,dtGridSource);           
            f.ShowForm();

            this.DisplayLayout.Bands[0].SortedColumns.Clear();
            if (f.SortFields == null) return;
            dtGridSource = f.SortFields;
            
            foreach (DataRow dr in f.SortFields.Rows)
            {
                if (dr[1].ToString() == "A to Z")
                    this.DisplayLayout.Bands[0].Columns[dr[0].ToString()].SortIndicator = SortIndicator.Ascending;
                else if (dr[1].ToString() == "Z to A")
                    this.DisplayLayout.Bands[0].Columns[dr[0].ToString()].SortIndicator = SortIndicator.Descending;
            }
        }

        private DataTable CreateColsTable()
        {             
            DataTable dt = new DataTable("tblSort");
            dt.Columns.Add("COLUMN_NAME", typeof(System.String));
            dt.Columns.Add("COLUMN_KEY", typeof(System.String));
            dt.Columns.Add("ORDER", typeof(System.String));
            foreach (UltraGridColumn dc in this.DisplayLayout.Bands[0].Columns)
                dt.Rows.Add(dc.Header.Caption, dc.Key);
            return dt;
        }

        

        private void ColumntoolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowColumnChooser(this.Text, true);
        }
        private void CopyPastetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            if (menuItem.CheckState == CheckState.Checked)
            {
                this.DisplayLayout.Override.AllowMultiCellOperations = AllowMultiCellOperation.All;
                _IsCopyPaste = true;

            }

            else
            {
            this.DisplayLayout.Override.AllowMultiCellOperations = AllowMultiCellOperation.None;
                _IsCopyPaste = false;

            }

            mnuCon.Items["toolCopyMenuItem"].Enabled = _IsCopyPaste;
            mnuCon.Items["toolPasteMenuitem"].Enabled = _IsCopyPaste;
        }
        private void UltraGrid_BeforeSelectChange(object sender, BeforeSelectChangeEventArgs e)
        {
            if (_IsCopyPaste)
            {
                if (e.NewSelections.Cells.OfType<UltraGridCell>().Count() > 0)
                {
                    if (e.NewSelections.Cells.OfType<UltraGridCell>().First().Column.Key != e.NewSelections.Cells.OfType<UltraGridCell>().Last().Column.Key)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
        private void toolPasteMenuitem_Click(object sender, EventArgs e)
        {

            if (_IsCopyPaste && CopyCellName != "" && this.Selected.Cells.IsReadOnly == false && this.Selected.Cells.Count > 0)
            {
                List<int> RowIndex = new List<int>();

                for (int i = 0; i < this.Selected.Cells.Count; i++)
                {
                    if (this.Selected.Cells[i].Column.CellActivation == Activation.AllowEdit && this.Selected.Cells[i].Column.Key.ToString() == CopyCellName)
                        RowIndex.Add(this.Selected.Cells[i].Row.Index);
                }

                foreach (var num in RowIndex)
                {
                    this.ActiveCell = this.Rows[num].Cells[CopyCellName];
                    this.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    this.ActiveCell.SelText = CopyCellValue;
                    this.Rows[num].Cells[CopyCellName].Value = CopyCellValue;
                    this.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    this.Rows[num].Update();
                }
            }
        }
        private void toolCopyMenuItem_Click(object sender, EventArgs e)
        {
            if (_IsCopyPaste)
            {
                if (this.Selected.Cells.Count == 1)
                {
                    CopyCellName = this.Selected.Cells[0].Column.Key.ToString();
                    CopyCellValue = this.Selected.Cells[0].Value.ToString();
                }
                else
                {
                    CopyCellName = "";
                    CopyCellValue = "";
                }
            }
        }
        private void RowSizetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            if (menuItem.CheckState == CheckState.Checked)
            {                
                this.DisplayLayout.Bands[0].Override.RowSizing = RowSizing.AutoFree;                
            }
            else
            {
                this.DisplayLayout.Bands[0].Override.RowSizing = RowSizing.Default;
            }
        }
        private void CellSumtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal douTot = 0;
            int cnt = 0;
            string msg = "";
            if (this.Selected.Cells.Count > 0)
            {
                foreach (UltraGridCell oCell in this.Selected.Cells)
                {
                    if (IsNumeric(oCell.Value.ToString()))
                    {
                        douTot += Convert.ToDecimal(oCell.Value);
                        cnt++;
                    }
                }
            }
            msg = "Sum =: [ " + douTot.ToString("0.00") + " ]";
            msg += "\nCount =: [ " + cnt.ToString() + " ]";
            if(cnt>0)
                msg += "\nAverage =: [ " + (douTot/cnt).ToString("0.00") + " ]";

            MessageBox.Show(msg,"Selected Cell Summary",MessageBoxButtons.OK ,MessageBoxIcon.Information);  
        }
        private  static bool IsNumeric(String pStr)
        {
            decimal output;
            return decimal.TryParse(pStr, out output);
        }
        #endregion

        #region Events
        private void UltraGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            _BlnIsCellActivated = false;
            if (this.UseOsThemes == Infragistics.Win.DefaultableBoolean.False)
            {
                //Infragistics.Win.AppearanceBase ActCelApp = this.DisplayLayout.Override.ActiveCellAppearance;
                //Infragistics.Win.AppearanceBase DispApp = this.DisplayLayout.Appearance;
                //ActCelApp.BackColor = Color.LightCyan;
                //ActCelApp.ForeColor = Color.DarkBlue;
                //ActCelApp.BorderColor = Color.Green;
                //ActCelApp.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
                //DispApp.BackColor = Color.LavenderBlush;
                //DispApp.BorderColor = Color.Gray;
                //mIntLaUTCellIndForExit = -1;
                //DispApp.ForeColor = Color.Navy;
            }            
            foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn UC in this.DisplayLayout.Bands[0].Columns)
            {
                if (UC.DataType.FullName == "System.DateTime")
                {
                    UC.MinValue = System.DateTime.Parse("01/01/1900");
                    UC.MaxValue = System.DateTime.Parse("06/06/2079");
                }
                if (UC.DataType.FullName == "System.Boolean")
                {
                    UC.DefaultCellValue = false;
                }
                //if (UC.Index == this.DisplayLayout.Bands[0].Columns.Count - 1)
                //{
                //    UC.Tag = "Exit";
                //    mIntLaUTCellIndForExit = UC.Index;
                //}
            }

            ExitColumnLocked();
        }

        private Infragistics.Win.UltraWinGrid.UltraGridCell ActivateCell()
        {
            int inti;            
            
            if (ActiveCellName == null || ActiveCellName != "")
            {
                return this.ActiveRow.Cells[ActiveCellName];
            }
            for (inti = 0; inti <= this.DisplayLayout.Bands[0].Columns.BoundColumnsCount + this.DisplayLayout.Bands[0].Columns.UnboundColumnsCount - 1; inti++)
            {
                if (this.DisplayLayout.Bands[0].Columns[inti].TabStop == true)
                {
                    if(this.ActiveRow.Cells !=null)
                        return this.ActiveRow.Cells[inti];
                }
            }
            return null;
        }

        private void cUltraGrid_Error(object sender, Infragistics.Win.UltraWinGrid.ErrorEventArgs e)
        {
            this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ActivateCell);
        }

        #endregion

        #region Ovveride Property

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        public override ContextMenu ContextMenu
        {
            get
            {
                return base.ContextMenu;
            }
            set
            {
                base.ContextMenu = value;
            }
        }
        #endregion

        #region Ovveride Method

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //this.SupportThemes = false;
            //this.DisplayLayout.CaptionAppearance.ResetBackColor();
            //this.DisplayLayout.Override.HeaderAppearance.ResetBackColor();
            this.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
            this.DisplayLayout.Override.HeaderAppearance.FontData.Bold =  Infragistics.Win.DefaultableBoolean.True;
            this.DisplayLayout.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            
            mnuCon.Items["toolShowColumnDetail"].Visible = IsSetColumnEnabled;
        }
        protected override void OnInitializeLayout(Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            base.OnInitializeLayout(e);
            if (MultiSelect == EnumMultiSelect.Yes)
            {
                Infragistics.Win.SelectionStrategyNone selectionStrategyNone = new Infragistics.Win.SelectionStrategyNone(this);
                this.SelectionStrategyFilter = new SelectionStrategyFilter_NoRowSelection(selectionStrategyNone);
            }
            this.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
            
        }
        protected override void OnEnter(EventArgs e)
        {

            base.OnEnter(e);
            if (this.ActiveRow == null)
            {
                if (this.Rows != null)
                {
                    this.ActiveRow = this.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.First);
                }
            }
            if (this.ActiveCell == null)
            {
                if (this.Rows != null)
                {
                    if (this.Rows.Count > 0)
                    {
                        this.ActiveCell = ActivateCell();
                    }
                }
            }
        }
        protected override void OnLeave(EventArgs e)
        {
            if (ActiveCell != null)
            {
                if (ActiveCell.IsInEditMode == true)
                {
                    PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);
                }
            }
            base.OnLeave(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            
            if (_BlnIsCellActivated == false)
            {
                if (this.Rows == null) return; 
                base.OnKeyDown(e);
                if (this.Rows.Count == 0)
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                    return;
                }
                if ((e.KeyData.ToString() == Keys.Enter.ToString()) || (e.KeyData.ToString() == Keys.Return.ToString()))
                {

                    if (this.ActiveRow == null)
                    {
                        this.ActiveRow = this.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.First);
                    }
                    if (this.ActiveCell == null)
                    {
                        this.ActiveCell = ActivateCell();
                    }
                    if (this.ActiveCell != null)
                    {
                        if (this.ActiveRow.HasNextSibling() == false && this.ActiveCell.Column.Tag + "" == "Exit")
                        {
                            if (this.ActiveCell == null)
                            {
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.DeactivateCell);
                                e.Handled = true;
                                SendKeys.Send("{TAB}");
                            }
                        }
                        else
                        {
                            if(this.EnableExcelStyleEnter)
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.BelowCell, false, false);
                            else
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.NextCellByTab, false, false);

                            e.Handled = true;
                            this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                        }
                    }
                }
            }
            else
            {
                try
                {
                    if (this.ActiveRow!=null)
                    if (this.ActiveRow.IsUnmodifiedTemplateAddRow == true)
                    {
                        if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
                        {
                            e.Handled = true;
                            this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.PrevRow, false, false);
                            return;
                        }
                        else if (!(e.KeyValue == 13))
                        {
                            try
                            {
                                if (this.ActiveCell.Column.EditorComponent != null)
                                {
                                    if (this.ActiveCell.Column.DataType.Name.ToLower() == "string")
                                    {
                                        if (this.ActiveCell.Column.EditorControl.Text != "")
                                        {
                                            this.ActiveCell.Value = this.ActiveCell.Text;
                                        }
                                    }
                                    else
                                    {
                                        if (this.ActiveCell.Column.EditorControl.Text != "")
                                        {
                                            this.ActiveCell.Value = Convert.ToInt32("0" + this.ActiveCell.Text);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                            if (e.KeyCode == Keys.F1 || e.KeyCode ==Keys.F4)
                            {
                                //base.OnKeyDown(e);
                            }
                            else
                                return;
                        }
                        else
                        {
                            if (this.ActiveRow.HasNextSibling() == false && this.ActiveCell.Column.Tag + "" == "Exit")
                            {
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.DeactivateCell);
                                e.Handled = true;
                                SendKeys.Send("{TAB}");
                                return;
                            }
                            else
                            {
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.NextCellByTab, false, false);
                                e.Handled = true;
                                return;
                            }
                        }
                    }
                    else if ((e.KeyData.ToString() == Keys.Enter.ToString()) || (e.KeyData.ToString() == Keys.Return.ToString()))
                    {
                        if (this.ActiveRow != null)
                        {
                            if (this.ActiveRow.Activation == Infragistics.Win.UltraWinGrid.Activation.NoEdit)
                            {
                                if (this.ActiveRow.HasNextSibling() == false && this.ActiveCell.Column.Tag + "" == "Exit")
                                {
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.DeactivateCell);
                                    e.Handled = true;
                                    SendKeys.Send("{TAB}");
                                    return;
                                }
                                else
                                {
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.NextCellByTab, false, false);
                                    e.Handled = true;
                                    return;
                                }
                            }
                            if (this.ActiveRow.IsActiveRow)
                            {
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);
                                if(EnableExcelStyleEnter)
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.BelowCell, false, false);
                                else
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.NextCellByTab, false, false);
                                e.Handled = true;
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                                return;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                   this.ActiveRow = this.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.Last);
                }


                if (e.KeyCode == Keys.F2) e.Handled = true;

                base.OnKeyDown(e);

                if (this.ActiveRow != null)
                {
                    if (this.ActiveCell == null)
                    {
                        if ((e.KeyCode.ToString() == Keys.Enter.ToString()))
                        {
                            if (this.ActiveRow == null)
                            {
                                this.ActiveRow = this.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.First);
                            }
                            if (this.ActiveCell == null)
                            {
                                this.ActiveCell = ActivateCell();
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                            }
                        }
                        return;
                    }
                    if (this.ActiveCell != null)
                    {
                        if (e.KeyCode.ToString() == Keys.Up.ToString())
                        {
                            if (this.ActiveCell.DroppedDown == true 
                                || this.ActiveCell.Column.Style==Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown
                                || this.ActiveCell.Column.Style == Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList 
                                )
                            {
                            }
                            else if (this.ActiveCell.Column.DataType.FullName == "System.Boolean")
                            {
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                            }
                            else
                            {
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);                                
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.AboveCell, false, false);
                                e.Handled = true;
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                            }
                        }
                        else if (e.KeyData.ToString() == Keys.Down.ToString())
                        {

                            if (this.ActiveCell.DroppedDown == true 
                                || this.ActiveCell.Column.Style == Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown
                                || this.ActiveCell.Column.Style == Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList 
                                )
                            {
                            }
                            else if (this.ActiveCell.Column.DataType.FullName == "System.Boolean")
                            {
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                            }
                            else
                            {
                                if (_ByPassDownArrayKey==false) //MOdify By Satish
                                { 
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);                                
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.BelowCell, false, false);                                                                    
                                    e.Handled = true;
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                                }
                            }
                        }
                        else if (e.KeyData.ToString() == Keys.Right.ToString())
                        {
                            if (this.ActiveCell.Column.DataType.FullName == "System.Boolean")
                            {
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                            }
                            if (this.ActiveCell.IsInEditMode == true)
                            {
                                if (!(this.ActiveCell.Band.Columns[this.ActiveCell.Column.Key].Style == Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox))
                                {
                                    if (this.ActiveCell.SelText.Length == this.ActiveCell.Text.Length)
                                    {
                                        this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);
                                        this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.NextCellByTab, false, false);
                                        e.Handled = true;
                                        this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                                    }
                                }
                            }
                        }
                        else if (e.KeyData.ToString() == Keys.Left.ToString())
                        {
                            if (this.ActiveCell.Column.DataType.FullName == "System.Boolean")
                            {
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                            }
                            if (this.ActiveCell.IsInEditMode == true)
                            {
                                if (!(this.ActiveCell.Band.Columns[this.ActiveCell.Column.Key].Style == Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox))
                                {
                                    if (this.ActiveCell.SelText.Length == this.ActiveCell.Text.Length)
                                    {
                                        this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);
                                        this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.PrevCellByTab, false, false);
                                        e.Handled = true;
                                        this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                                    }
                                }
                            }
                        }
                        else if (e.Alt == true && e.KeyCode == Keys.Enter)
                        {
                            //Sart Commet
                            if (this.DisplayLayout.Bands[0].Override.AllowAddNew == Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom)
                            {
                                e.Handled = true;

                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.FirstCellInRow, false, false);
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                            }
                            //Sart Commet
                        }
                        else    
                        if ((e.KeyData.ToString() == Keys.Enter.ToString()) || (e.KeyData.ToString() == Keys.Return.ToString()))
                        {
                            if (this.ActiveRow.IsUnmodifiedTemplateAddRow == true)
                            {
                                if (this.ActiveCell.Column.Tag + "" == "Exit")
                                {
                                    e.Handled = true;
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.UndoRow, false, false);
                                    SendKeys.Send("{TAB}");
                                }
                                else
                                {
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.BelowCell, false, false);
                                    e.Handled = true;
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);

                                    //SendKeys.Send("{TAB}");
                                }
                            }
                            else
                            {
                                if (this.ActiveCell.IsInEditMode == false)
                                {
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                                }
                                else if (this.ActiveRow.HasNextSibling() == false && this.ActiveCell.Column.Tag + "" == "Exit")
                                {
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.DeactivateCell);
                                    e.Handled = true;
                                    SendKeys.Send("{TAB}");
                                }
                                else
                                {
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.NextCellByTab, false, false);
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                                    e.Handled = true;
                                }
                            }
                        }
                        else if (e.KeyCode == Keys.Escape)
                        {
                            if (this.ActiveRow.IsUnmodifiedTemplateAddRow == true)
                            {
                                e.Handled = true;
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);
                                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.UndoRow, false, false);
                                SendKeys.Send("{TAB}");
                            }
                        }
                        else if (e.KeyCode == Keys.F2)
                        {
                            if (this.ActiveCell != null)
                            {
                                if (this.ActiveCell.IsInEditMode == true & this.ActiveCell.Column.DataType.FullName != "System.Boolean")
                                {
                                    if (this.ActiveCell.SelText.Length == this.ActiveCell.Text.Length)
                                    {
                                        if (this.ActiveCell.Column.DataType.FullName == "System.DateTime")
                                        {
                                            if (this.ActiveCell.Text.ToString() != "")
                                            {
                                                this.ActiveCell.Value = this.ActiveCell.Value.ToString().Substring(0, 10);
                                            }
                                        }
                                        this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);                                        
                                        this.ActiveCell.SelStart = this.ActiveCell.Text.Length;
                                        this.ActiveCell.SelLength = this.ActiveCell.Text.Length;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Enter && this.Rows.Count == 0)
                    {
                        this.Rows.Band.AddNew();
                        if (this.ActiveCell != null)
                        {
                            if (this.ActiveCell.IsTabStop == false)
                            {
                                if (ActiveCellName != string.Empty)
                                {
                                    this.ActiveCell = ActivateCell();
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ActivateCell, false, false);
                                    this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
                                    SendKeys.Send("{TAB}");
                                }
                                else
                                {
                                    SendKeys.Send("{TAB}");
                                }
                            }
                        }
                    }
                    else if (e.KeyValue == 27)
                    {
                        if (this.ActiveRow == null)
                        {
                            e.Handled = true;
                            this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.LastRowInGrid, false, false);
                            this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.LastCellInGrid, false, false);
                            this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ActivateCell, false, false);
                            return;
                        }
                    }
                }
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Insert && e.Control == true)
            {
                if (this.Selected.Rows.Count == 0) return;

                int ind = this.Selected.Rows[0].VisibleIndex;
                UltraGridRow row = this.DisplayLayout.Bands[0].AddNew();
                row.ParentCollection.Move(row, ind);
                this.ActiveRowScrollRegion.ScrollRowIntoView(row);
            }

            if (e.KeyCode == Keys.Enter && e.Shift == true)
            {
                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);
                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.PrevCellByTab, false, false);
                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Length != 0)
            {
                if (e.KeyChar == (Char)4)
                {
                    //PTControls.cConMenu.cConMenu Con = new PTControls.cConMenu.cConMenu();
                    //Con.STRPROGRAM = this.FindForm().ProductName;
                    //Con.STRFORMNAME = this.FindForm().Name;
                    //Con.STRCONTNAME = this.Name;
                    //Con.Show(this, new Point(this.Bottom, this.Left));
                    e.Handled = true;
                    return;
                }
            }
            base.OnKeyPress(e);
            if (IsCapital == true) e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
            if (this.ActiveCell != null )
            {
                if (ActiveCell.IsInEditMode == false) return;
                if (this.ActiveCell.Column.Format != null && e.KeyChar != 22 && e.KeyChar !=3)  //3 - ctl+c and 22 - ctl + v (copy paste)
                {
                    if (this.ActiveCell.Column.Format.ToString().StartsWith("#"))
                    {
                        mIntDigit = 0;
                        if (this.ActiveCell.Column.Format.Contains("."))
                        {
                            string[] digiFormat = this.ActiveCell.Column.Format.Split('.');
                            mIntDigit = digiFormat[1].Length;  
                        }
                        //for (int i = (this.ActiveCell.Column.Format.ToString().Length) - 1; i >= 0 && this.ActiveCell.Column.Format.ToString()[i] != '.'; i--)
                        //    mIntDigit++;
                        if (ValNum(e, e.KeyChar, mIntDigit) == false)
                        {                                         
                            e.Handled = true;
                        }
                    }
                }
            }
        }
        protected override void OnAfterRowActivate()
        {
            if (ActiveCell != null)
            {
                if (ActiveCell.IsInEditMode == false)
                {
                    PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);
                }
            }
            base.OnAfterRowActivate();
        }
        protected override void OnAfterCellActivate()
        {
            if (this.ActiveRow != null && this.ActiveCell != null && this.ActiveCell.Column.EditorComponent != null)
            {
                if (this.ActiveCell.Column.EditorComponent.GetType().Name.ToLower() == "cultracombo")
                {
                    ((UTCCombo)this.ActiveCell.Column.EditorComponent).SelectedRow = null;
                }
            }
            base.OnAfterCellActivate();
        }
        protected override void OnBeforeRowDeactivate(CancelEventArgs e)
        {
            if (_BlnIsCellActivated == true)
            {
                if (this.ActiveRow.IsUnmodifiedTemplateAddRow == true)
                {
                    e.Cancel = false;
                    return;
                }
                else
                {
                    base.OnBeforeRowDeactivate(e);
                    if (this.ActiveRow != null)
                    {
                        if (PersistBackColor == false)
                        {
                            this.ActiveRow.CellAppearance.BackColor = System.Drawing.Color.White;
                        }
                    }
                }
            }
        }
        protected override void OnBeforeCellDeactivate(CancelEventArgs e)
        {
            if (_BlnIsCellActivated == true)
            {
                if (this.ActiveCell.Column.DataType.FullName == "System.DateTime")
                {
                    if (this.ActiveCell.Value.ToString() == "01/01/0001 00:00:00")
                    {
                        this.ActiveCell.Value = Convert.DBNull;
                    }
                }
                base.OnBeforeCellDeactivate(e);
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (MultiSelect == EnumMultiSelect.Yes)
            {
                System.Drawing.Point Point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement elementFromPoint = this.DisplayLayout.UIElement.ElementFromPoint(Point);
                if (elementFromPoint is Infragistics.Win.UltraWinGrid.RowSelectorUIElement)
                {
                    Infragistics.Win.UltraWinGrid.RowSelectorUIElement RowSelectorUIElement = (Infragistics.Win.UltraWinGrid.RowSelectorUIElement)elementFromPoint;
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)RowSelectorUIElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));
                    if (row != null)
                    {
                        row.Selected = !row.Selected;
                    }
                }
            }
            base.OnMouseClick(e);
        }
        protected override void OnBeforeRowUpdate(Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e)
        {
            base.OnBeforeRowUpdate(e);
        }
        protected override void OnAfterRowUpdate(Infragistics.Win.UltraWinGrid.RowEventArgs e)
        {
            base.OnAfterRowUpdate(e);
            if (EnumBoUnBMode == EnumBounUnbound.BoundMode || EnumBoUnBMode == EnumBounUnbound.SPBoundMode)
            {
                bool BlnExe;
                if (EnumBoUnBMode == EnumBounUnbound.SPBoundMode)
                    BlnExe = false;
                else
                    BlnExe = true;
               
                DataTable DT = ((DataSet)this.DataSource).Tables[this.DataMember];
                String StrConType = ((DataSet)this.DataSource).Tables[this.DataMember].ExtendedProperties["ConType"].ToString().ToLower() + "";
                DataSet DS = ((DataSet)DataSource);
                if (!(DT.GetChanges() == null))
                {
                    //if (StrConType.CompareTo("Sql".ToLower()) == 0)
                    //    //DataLib.OperationSql.UpdRec(DT.GetChanges(), DS, DT.TableName, mStrExceptField, BlnExe);
                    //else if (StrConType.CompareTo("Ora".ToLower()) == 0)
                    //    //DataLib.OperationOra.UpdRecGrid(DS, DT.GetChanges(), DT.TableName, mStrExceptField);
                    //else if (StrConType.CompareTo("Ole".ToLower()) == 0)
                    //    //DataLib.OperationOle.UpdRec(DT.GetChanges(), DS, DT.TableName, mStrExceptField, BlnExe);
                }
            }
        }
        protected override void OnAfterRowsDeleted()
        {
            base.OnAfterRowsDeleted();
            if (EnumBoUnBMode == EnumBounUnbound.BoundMode || EnumBoUnBMode == EnumBounUnbound.SPBoundMode)
            {

                bool BlnExe;
                if (EnumBoUnBMode == EnumBounUnbound.SPBoundMode)
                    BlnExe = false;
                else
                    BlnExe = true;

                DataTable DT = ((DataSet)this.DataSource).Tables[this.DataMember];
                String StrConType = ((DataSet)this.DataSource).Tables[this.DataMember].ExtendedProperties["ConType"].ToString().ToLower();
                DataSet DS = ((DataSet)DataSource);
                if (!(DT.GetChanges() == null))
                {
                    if (StrConType.CompareTo("Sql".ToLower()) == 0)
                    {
                        //DataLib.OperationSql.DelRec(DT, DS, DT.TableName, BlnExe);
                    }
                    else if (StrConType.CompareTo("Ora".ToLower()) == 0)
                    {
                        //DataLib.OperationOra.DelRec(DT, DS, DT.TableName);
                    }
                    else if (StrConType.CompareTo("Ole".ToLower()) == 0)
                    {
                        //DataLib.OperationOle.DelRec(DT, DS, DT.TableName, BlnExe);
                    }
                }
            }
        }
        protected override void OnBeforeRowsDeleted(Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs e)
        {
            base.OnBeforeRowsDeleted(e);

            //if (EnumBoUnBMode == EnumBounUnbound.BoundMode)
            //{
            //    e.DisplayPromptMsg = false;
            //    if (e.Rows.Length != 1)
            //    {
            //        Val.Message("Multiple Rows Selected", DataLib.GlobalDec.gStrMsgHeading, MessageBoxIcon.Information);
            //        e.Cancel = true;
            //    }
            //    else if (Val.Conf("Want Do Delete Current Row ? ") == DialogResult.No)
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right) 
                this.mnuCon.Enabled   = IsRightClickEnabled;
             
        }
        #endregion
        
        #region User Define function

        public void SetOperation(bool pBlnOnlyAddNew, bool pBlnOnlyDelete, bool pBlnOnlyUpdate, EnumCellActivation pBlnCellActivation)
        {
            if (DisplayLayout == null) return;
            SetOperation(pBlnOnlyAddNew, pBlnOnlyDelete, pBlnOnlyUpdate);
            for (int IntI = 0; IntI <= DisplayLayout.Bands[0].Columns.All.Length - 1; IntI++)
            {
                DisplayLayout.Bands[0].Columns[IntI].CellActivation = (Infragistics.Win.UltraWinGrid.Activation)pBlnCellActivation;
            }
            if ((Infragistics.Win.UltraWinGrid.Activation)pBlnCellActivation == Infragistics.Win.UltraWinGrid.Activation.AllowEdit)
            {
                _BlnIsCellActivated = true;
            }
            else
            {
                _BlnIsCellActivated = false;
            }
        }
        public void SetOperation(bool pBlnOnlyAddNew, bool pBlnOnlyDelete, bool pBlnOnlyUpdate)
        {
            if (DisplayLayout == null) return;
            if (pBlnOnlyAddNew == true)
            {
                DisplayLayout.Bands[0].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
            }
            else
            {
                DisplayLayout.Bands[0].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            }
            DisplayLayout.Bands[0].Override.AllowDelete = (Infragistics.Win.DefaultableBoolean)Microsoft.VisualBasic.Interaction.IIf(pBlnOnlyDelete == true, Infragistics.Win.DefaultableBoolean.True, Infragistics.Win.DefaultableBoolean.False);
            DisplayLayout.Bands[0].Override.AllowUpdate = (Infragistics.Win.DefaultableBoolean)Microsoft.VisualBasic.Interaction.IIf(pBlnOnlyUpdate == true, Infragistics.Win.DefaultableBoolean.True, Infragistics.Win.DefaultableBoolean.False);

            for (int IntI = 0; IntI <= DisplayLayout.Bands[0].Columns.All.Length - 1; IntI++)
            {
                DisplayLayout.Bands[0].Columns[IntI].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            }
            _BlnIsCellActivated = false;
        }

        public void SetColumnLock(bool pBlnLock)
        {
            Infragistics.Win.UltraWinGrid.ColumnsCollection UltGrdCols;
            if (this.DataMember.ToString().Length == 0)
                UltGrdCols = DisplayLayout.BandsSerializer[0].Columns;
            else
                UltGrdCols = this.DisplayLayout.Bands[this.DataMember.ToString()].Columns;

            for(int i=0;i<UltGrdCols.All.Length-1;i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridColumn UltGrdCol=UltGrdCols[i];
                string StrColumnName = UltGrdCol .ToString();
                if (DisplayLayout == null) return;
                if (UltGrdCol.Hidden  == false)
                {
                    if (pBlnLock)
                    {
                        UltGrdCol.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                        UltGrdCol.TabStop = false;
                    }
                    else
                    {
                        UltGrdCol.CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                        UltGrdCol.TabStop = true;
                    }
                    if (this.InInitializeLayout == false)
                    {
                        ExitColumnLocked();
                    }
                }
            }
        }
        public void SetColumnLock(String pStrColumnName, bool pBlnLock)
        {
            if (DisplayLayout == null) return;
            if (this.DisplayLayout.Bands[0].Columns.Count == 0) return;
            if (this.DisplayLayout.Bands[0].Columns[pStrColumnName].Hidden == true) return;
            Infragistics.Win.UltraWinGrid.UltraGridColumn UltGrdCol;
            
            if (this.DataMember.ToString().Length == 0)
                UltGrdCol = DisplayLayout.Bands[0].Columns[pStrColumnName];
            else
                UltGrdCol = this.DisplayLayout.Bands[this.DataMember.ToString()].Columns[pStrColumnName];
            if (pBlnLock)
            {
                UltGrdCol.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                UltGrdCol.TabStop = false;
            }
            else
            {
                UltGrdCol.CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                UltGrdCol.TabStop = true;
            }
            if (this.InInitializeLayout == false)
            {
                ExitColumnLocked();
            }
        }
        public void SetFocus(string ColumnName)
        {
            if (this.Rows == null) return;
            try
            {
                this.Rows[this.Rows.Count - 1].Cells[ColumnName].Activate();
                this.Focus();
                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
            }
            catch (Exception e)
            {
            }
        }
        public void SetFocus(int RowNo, string ColumnName)
        {
            if (RowNo > (this.Rows.Count - 1))
                RowNo = this.Rows.Count - 1;

            if (this.Rows == null) return;
            try
            {
                this.Rows[RowNo].Cells[ColumnName].Activate();
                this.Focus();
                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode, false, false);
            }
            catch (Exception e)
            {
            }
        }
        public void LockColumn(String pStrColumnName, bool pBlnLock)
        {
            if (DisplayLayout == null) return;
            if (this.DisplayLayout.Bands[0].Columns[pStrColumnName].Hidden == true) return;
            Infragistics.Win.UltraWinGrid.UltraGridColumn UltGrdCol;
            if (this.DataMember.ToString().Length == 0)
                UltGrdCol = DisplayLayout.BandsSerializer[0].Columns[pStrColumnName];
            else
                UltGrdCol = this.DisplayLayout.Bands[this.DataMember.ToString()].Columns[pStrColumnName];
            if (pBlnLock)
            {
                UltGrdCol.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            }
            else
            {
                UltGrdCol.CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            }
            if (this.InInitializeLayout == false)
            {
                ExitColumnLocked();
            }
        }
        private void ExitColumnLocked(String pStrColumnName, Boolean pBlnLocked)
        {
            int inti, intj;
            if (this.ActiveRow == null) return;
            if (this.ActiveRow.Cells == null) return;

            if (pBlnLocked == false)
            {
                if (this.ActiveRow.Cells[pStrColumnName].Column.Index > mIntLaUTCellIndForExit)
                {
                    this.DisplayLayout.Bands[0].Columns[mIntLaUTCellIndForExit].Tag = "";
                    this.DisplayLayout.Bands[0].Columns[pStrColumnName].Tag = "Exit";
                    mIntLaUTCellIndForExit = this.ActiveRow.Cells[pStrColumnName].Column.Index;
                    return;
                }
            }

            for (inti = this.DisplayLayout.Bands[0].Columns.Count - 1; inti >= 0; inti--)
            {
                if (pBlnLocked == true)
                {
                    if (pStrColumnName == this.DisplayLayout.Bands[0].Columns[inti].Key)
                    {
                        if (this.DisplayLayout.Bands[0].Columns[inti].Tag + "" == "Exit")
                        {
                            this.DisplayLayout.Bands[0].Columns[inti].Tag = "";
                            for (intj = this.DisplayLayout.Bands[0].Columns.Count - 1; intj >= 0; intj--)
                            {
                                if (this.DisplayLayout.Bands[0].Columns[intj].TabStop == true)
                                {
                                    this.DisplayLayout.Bands[0].Columns[intj].Tag = "Exit";
                                    mIntLaUTCellIndForExit = intj;
                                    return;
                                }
                            }
                            if (intj < 0)
                            {
                                this.DisplayLayout.Bands[0].Columns[pStrColumnName].Tag = "Exit";
                                mIntLaUTCellIndForExit = 0;
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void ExitColumnLocked()
        {            
            if (this.DisplayLayout.Bands[0].Columns.Count == 0) return;
            //CLEAR ALL COLUMNS TAG
            foreach (var col in this.DisplayLayout.Bands[0].Columns)
                col.Tag = "";
            
            // Get the first visible column by passing in VisibleRelation.First.
            UltraGridColumn column = this.DisplayLayout.Bands[0].Columns[0].GetRelatedVisibleColumn(VisibleRelation.Last);

            while (null != column)
            {
                if  (column.TabStop ==true)
                {
                    column.Tag = "Exit";
                    return;
                }               

                // Get the next visible column by passing in VisibleRelation.Next.
                column = column.GetRelatedVisibleColumn(VisibleRelation.Previous);
            }

        }
        public void SetColumnHide(String pStrColumnName, bool pBlnLock)
        {
            
            Infragistics.Win.UltraWinGrid.UltraGridColumn UltGrdCol;
            if (this.DataMember.ToString().Length==0)
                UltGrdCol = DisplayLayout.BandsSerializer[0].Columns[pStrColumnName];
            else
                UltGrdCol = this.DisplayLayout.Bands[this.DataMember.ToString()].Columns[pStrColumnName];

            if (pBlnLock)
            {
                UltGrdCol.Hidden = true;
                UltGrdCol.TabStop = false;
            }
            else
            {
                UltGrdCol.Hidden = false;
                UltGrdCol.TabStop = true;
            }
            if (UltGrdCol.Group != null) SetGroupWidth(UltGrdCol.Group);

           
            ExitColumnLocked();
        }
        //public bool ValNum(KeyPressEventArgs e, int KeyAscii, int NumberofDecimal)
        //{
        //    int intDotPosition = 0;
        //    int DigitInFraction;

        //    if (KeyAscii == 13 || KeyAscii == 8)
        //        return true;
        //    if (this.ActiveCell.SelText.Length == 0)
        //        if (this.ActiveCell.Column.Format.Length == this.ActiveCell.Text.Length) return false;

        //    intDotPosition = Microsoft.VisualBasic.Strings.InStr(1, this.ActiveCell.Text, ".", Microsoft.VisualBasic.CompareMethod.Text);

        //    if (!((KeyAscii > 47 && KeyAscii < 58) || (KeyAscii == 8) || (KeyAscii == 45)))
        //    {
        //        if (KeyAscii == 46 && NumberofDecimal > 0)
        //        {
        //            if ((intDotPosition > 0 && this.ActiveCell.SelText.Length > 0))
        //                return true;
        //            else if (!(intDotPosition > 0))
        //                return true;
        //            else
        //            {
        //                if ((this.ActiveCell.SelText.Length == this.ActiveCell.Text.Length))
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //        return false;
        //    }
        //    else
        //    {
        //        if (NumberofDecimal == 0 || KeyAscii == 8 || KeyAscii == 45)
        //        {
        //            if (KeyAscii != 8 && this.ActiveCell.Text.Length == this.ActiveCell.Column.Format.Length)
        //                return false;
        //            else
        //                return true;
        //        }
        //        else if (ActiveCell.Text.Length == ActiveCell.SelLength)
        //            return true;
        //        if (intDotPosition > 0)
        //        {
        //            DigitInFraction = Microsoft.VisualBasic.Strings.Len(Microsoft.VisualBasic.Strings.Mid(this.ActiveCell.Text, intDotPosition + 1, this.ActiveCell.Text.Length));
        //            if (DigitInFraction >= NumberofDecimal)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //        else
        //            return true;
        //    }
        //}
        public bool ValNum(KeyPressEventArgs e, int KeyAscii, int NumberofDecimal)
        {
            int intDotPosition = 0;
            int DigitInFraction;

            if (KeyAscii == 13 || KeyAscii == 8 || KeyAscii == 32)
                return true;

            if (this.ActiveCell.SelText.Length == 0)
                if (this.ActiveCell.Column.Format.Length == this.ActiveCell.Text.Length) return false;

            if (AllowNegativeValue && KeyAscii == 45)
            {
                if (Microsoft.VisualBasic.Strings.InStr(1, this.ActiveCell.Text, "-", Microsoft.VisualBasic.CompareMethod.Text) > 0)
                    return false;
                else
                    return true;
            }

            intDotPosition = Microsoft.VisualBasic.Strings.InStr(1, this.ActiveCell.Text, ".", Microsoft.VisualBasic.CompareMethod.Text);

            if (!((KeyAscii > 47 && KeyAscii < 58) || KeyAscii == 8))
            {
                if (KeyAscii == 46 && NumberofDecimal > 0)
                {
                    if ((intDotPosition > 0 && this.ActiveCell.SelText.Length > 0))
                        return true;
                    else if (!(intDotPosition > 0))
                        return true;
                    else
                        return false;
                }
                return false;
            }
            else
            {
                if (intDotPosition > 0)
                {
                    DigitInFraction = Microsoft.VisualBasic.Strings.Len(Microsoft.VisualBasic.Strings.Mid(this.ActiveCell.Text, intDotPosition + 1, Microsoft.VisualBasic.Strings.Len(this.ActiveCell.Text)));
                    if (this.ActiveCell.SelStart > intDotPosition && DigitInFraction >= NumberofDecimal)
                        return false;
                }
            }
            return true;
        }

        public void SetColVisPos(String pStrColumnName, int pintOrder)
        {
            if (this.DisplayLayout.Bands[0].Columns.Count == 0) return;

            Infragistics.Win.UltraWinGrid.UltraGridColumn UltGrdCol = this.DisplayLayout.Bands[0].Columns[pStrColumnName];
            UltGrdCol.Header.VisiblePosition = pintOrder;
        }
        public void AddPrcColor(string pStrName, Color pColBack, Color pColFora)
        {
            try
            {
                this.DisplayLayout.Appearances.Add(pStrName);
                this.DisplayLayout.Appearances[pStrName].BackColor = pColBack;
                this.DisplayLayout.Appearances[pStrName].ForeColor = pColFora;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AddPrcColor(string pStrName, Color pColBack)
        {
            try
            {
                this.DisplayLayout.Appearances.Add(pStrName);
                this.DisplayLayout.Appearances[pStrName].BackColor = pColBack;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void ExportColumn()
        {
            DataTable DBCol = new DataTable("DBCOl");
            DBCol.Columns.Add(new DataColumn("ColName"));
            DBCol.Columns.Add(new DataColumn("FieldName"));

            foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn Col in this.DisplayLayout.Bands[0].Columns.All)
            {
                DataRow DRow = DBCol.NewRow();
                DRow["ColName"] = Col.ToString();
                DRow["FieldName"] = Col.Header.Caption;
                DBCol.Rows.Add(DRow);
            }
            DBCol.WriteXml(this.Name.ToString());
        }
        public void OnLeave()
        {

            if (ActiveRow != null)
            {
                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode, false, false);
                this.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.UndoRow, false, false);
            }

        }

        #region Format Column
        /// <summary>
        /// To Set Column at dynmictime
        /// </summary>
        /// <param name="pStrBandKey">Band</param>
        /// <param name="pStrColumnKey">Column Id</param>
        /// <param name="pStrColumnCaption">Caption</param>
        /// <param name="pintColWidth">Width</param>
        /// <param name="pStrFormat">Format</param>
        /// <param name="pBlnHidden">Hidden</param>
        /// <param name="pColumnStyle">Style</param>
        /// <param name="pintHeaderVisiblePosition">Header.VisiblePosition</param>
        /// <param name="pBlnFontBold">Bold</param>
        /// <param name="pBackColor">BackColor</param>
        /// <param name="pForeColor">ForeColor</param>
        /// <param name="pHAlign">TextHAlign</param>
        public void                                          ColumnSet(String pStrBandKey, String pStrColumnKey, String pStrColumnCaption, int pintColWidth, String pStrFormat, Boolean pBlnHidden, Infragistics.Win.UltraWinGrid.ColumnStyle pColumnStyle, int pintHeaderVisiblePosition, Infragistics.Win.DefaultableBoolean pBlnFontBold, System.Drawing.Color pBackColor, System.Drawing.Color pForeColor, Infragistics.Win.HAlign pHAlign)
        {
            Infragistics.Win.UltraWinGrid.UltraGridColumn UltraGridColumn = DisplayLayout.Bands[pStrBandKey].Columns[pStrColumnKey];
            UltraGridColumn.Width = pintColWidth;
            UltraGridColumn.Header.Caption = pStrColumnCaption;
            UltraGridColumn.Header.VisiblePosition = pintHeaderVisiblePosition;
            UltraGridColumn.Hidden = pBlnHidden;
            UltraGridColumn.Format = pStrFormat;
            UltraGridColumn.Style = pColumnStyle;
            UltraGridColumn.TabStop = true;

            Infragistics.Win.Appearance Appearance = new Infragistics.Win.Appearance();
            Appearance.FontData.Bold = pBlnFontBold;
            Appearance.ForeColor = pForeColor;
            Appearance.BackColor = pBackColor;
            Appearance.TextHAlign = pHAlign;
            //UltraGridColumn.CellAppearance = Appearance;
            this.DisplayLayout.Bands[pStrBandKey].Columns[pStrColumnKey].CellAppearance = Appearance;
        }
        public Infragistics.Win.UltraWinGrid.UltraGridColumn ColumnSet(String pStrBandKey, String pStrColumnKey, String pStrColumnCaption, int pintColWidth, String pStrFormat, Boolean pBlnHidden, Infragistics.Win.UltraWinGrid.ColumnStyle pColumnStyle, int pintHeaderVisiblePosition, Infragistics.Win.DefaultableBoolean pBlnFontBold, System.Drawing.Color pBackColor, System.Drawing.Color pForeColor, Infragistics.Win.HAlign pHAlign, int pintFieldLen, Boolean pBlnLock)
        {
            Infragistics.Win.UltraWinGrid.UltraGridColumn UltraGridColumn = DisplayLayout.Bands[pStrBandKey].Columns[pStrColumnKey];
            UltraGridColumn.Width = pintColWidth;
            UltraGridColumn.Header.Caption = pStrColumnCaption;
            UltraGridColumn.Header.VisiblePosition = pintHeaderVisiblePosition;
            UltraGridColumn.Hidden = pBlnHidden;
            UltraGridColumn.Format = pStrFormat;
            UltraGridColumn.Style = pColumnStyle;
            UltraGridColumn.MaxLength = pintFieldLen;

            Infragistics.Win.Appearance Appearance = new Infragistics.Win.Appearance();
            Appearance.FontData.Bold = pBlnFontBold;
            Appearance.ForeColor = pForeColor;
            Appearance.BackColor = pBackColor;
            Appearance.TextHAlign = pHAlign;
            //UltraGridColumn.CellAppearance = Appearance;            
            this.DisplayLayout.Bands[pStrBandKey].Columns[pStrColumnKey].CellAppearance = Appearance;

            if (this.DisplayLayout.Bands[pStrBandKey].Columns.Count == 0) return null;
            if (this.DisplayLayout.Bands[pStrBandKey].Columns[pStrColumnKey].Hidden == true) return null;
            Infragistics.Win.UltraWinGrid.UltraGridColumn UltGrdCol = this.DisplayLayout.Bands[pStrBandKey].Columns[pStrColumnKey];
            if (pBlnLock)
            {
                UltGrdCol.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                UltGrdCol.TabStop = false;
            }
            else
            {
                UltGrdCol.CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                UltGrdCol.TabStop = true;
            }
            if (this.InInitializeLayout == false)
            {
                ExitColumnLocked();
            }
            return UltraGridColumn;
        }
        public void CreateGroup(String pStrKey, int pintId, String pStrCaption, int pintHeaderVisiblePosition)
        {
            if (this.DisplayLayout.Bands[0].Groups.Exists(pStrKey) == false)
            {
                Infragistics.Win.UltraWinGrid.UltraGridGroup UltraGridGroup = this.DisplayLayout.Bands[0].Groups.Insert(pintId, pStrKey);
                UltraGridGroup.Header.Caption = pStrCaption;
                UltraGridGroup.Header.VisiblePosition = pintHeaderVisiblePosition;
                UltraGridGroup.Key = pStrKey;
            }
        }
        public void  ColumnSet(String pStrGroupKey, String pStrBandKey, String pStrColumnKey, String pStrColumnCaption, int pintColWidth, String pStrFormat, Boolean pBlnHidden, Infragistics.Win.UltraWinGrid.ColumnStyle pColumnStyle, int pintHeaderVisiblePosition, Infragistics.Win.DefaultableBoolean pBlnFontBold, System.Drawing.Color pBackColor, System.Drawing.Color pForeColor, Infragistics.Win.HAlign pHAlign, int pintFieldLen, Boolean pBlnLock, Boolean pBlnAddUnBoundColumn)
        {
            this.DisplayLayout.Bands[0].Columns.Insert(pintHeaderVisiblePosition, pStrColumnKey);
            ColumnSet(pStrGroupKey, pStrBandKey, pStrColumnKey, pStrColumnCaption, pintColWidth, pStrFormat, pBlnHidden, pColumnStyle, pintHeaderVisiblePosition, pBlnFontBold, pBackColor, pForeColor, pHAlign, pintFieldLen, pBlnLock);
        }
        public Infragistics.Win.UltraWinGrid.UltraGridColumn ColumnSet(String pStrGroupKey, String pStrBandKey, String pStrColumnKey, String pStrColumnCaption, int pintColWidth, String pStrFormat, Boolean pBlnHidden, Infragistics.Win.UltraWinGrid.ColumnStyle pColumnStyle, int pintHeaderVisiblePosition, Infragistics.Win.DefaultableBoolean pBlnFontBold, System.Drawing.Color pBackColor, System.Drawing.Color pForeColor, Infragistics.Win.HAlign pHAlign, int pintFieldLen, Boolean pBlnLock)
        {
            Infragistics.Win.UltraWinGrid.UltraGridColumn UltraGridColumn = DisplayLayout.Bands[pStrBandKey].Columns[pStrColumnKey];
            UltraGridColumn.Width = pintColWidth;
            UltraGridColumn.Header.Caption = pStrColumnCaption;
            UltraGridColumn.Header.VisiblePosition = pintHeaderVisiblePosition;
            UltraGridColumn.Hidden = pBlnHidden;
            UltraGridColumn.Format = pStrFormat;
            UltraGridColumn.Style = pColumnStyle;
            UltraGridColumn.MaxLength = pintFieldLen;
            if (pStrGroupKey != string.Empty)
            {
                UltraGridColumn.Group = this.DisplayLayout.Bands[pStrBandKey].Groups[pStrGroupKey];
            }

            Infragistics.Win.Appearance Appearance = new Infragistics.Win.Appearance();
            Appearance.FontData.Bold = pBlnFontBold;
            Appearance.ForeColor = pForeColor;
            Appearance.BackColor = pBackColor;
            Appearance.TextHAlign = pHAlign;
            //UltraGridColumn.CellAppearance = Appearance;
            this.DisplayLayout.Bands[pStrBandKey].Columns[pStrColumnKey].CellAppearance = Appearance;

            if (this.DisplayLayout.Bands[pStrBandKey].Columns.Count == 0) return null;
            if (this.DisplayLayout.Bands[pStrBandKey].Columns[pStrColumnKey].Hidden == true) return null;
            Infragistics.Win.UltraWinGrid.UltraGridColumn UltGrdCol = this.DisplayLayout.Bands[pStrBandKey].Columns[pStrColumnKey];
            if (pBlnLock)
            {
                UltGrdCol.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                UltGrdCol.TabStop = false;
            }
            else
            {
                UltGrdCol.CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                UltGrdCol.TabStop = true;
            }
            if (this.InInitializeLayout == false)
            {
                ExitColumnLocked();
            }
            return UltraGridColumn;
        }
        #endregion
        #region Group
        private void SetGroupWidth(Infragistics.Win.UltraWinGrid.UltraGridGroup Grp)
        {
            int Width = 0;
            foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn Col in Grp.Columns)
            {
                if (Col.Hidden == false)
                {
                    Width += Col.Width;
                }
            }
            if (Width == 0)
            {
                Grp.Hidden = true;
            }
            else if (Grp.Hidden == true)
            {
                Grp.Hidden = false;
            }
            Grp.Width = Width;
        }
        #endregion 
        #region Summary Field
        /// <summary>
        /// Set Summary Of Column
        /// if both parameter is blanck then all field with format contain # display 
        /// </summary>
        /// <param name="pStrField">Only Thouse Field Require To Sum</param>
        /// <param name="pStrExceptField">Except Those FIeld</param>                    

        public void SetSummaryColumn(string pStrField, string pStrExceptField, string SummaryCaption, string CaptionDisplayColumn, Infragistics.Win.Appearance SummaryAppearnce, bool WithCount)
        {
            string[] StrField = pStrField.Split(',');
            string[] StrExceptField = pStrExceptField.Split(',');

            //clear summary
            foreach (Infragistics.Win.UltraWinGrid.SummarySettings Sum in this.DisplayLayout.Bands[0].Summaries)
            {
                this.DisplayLayout.Bands[0].Summaries.Remove(Sum);
                this.DisplayLayout.Bands[0].ResetSummaries();
            }
            //add summary caption
            if (SummaryCaption.Length > 0 && CaptionDisplayColumn.Length == 0)
            {
                UltraGridColumn col = this.DisplayLayout.Bands[0].GetFirstVisibleCol(this.ActiveColScrollRegion, true);
                if (null != col && (col.Format == null || !col.Format.ToString().Contains("#"))) //by pass if its summary field -alredy exist in summary
                    CaptionDisplayColumn = col.Key;
            }

            if (CaptionDisplayColumn.Length > 0 && this.DisplayLayout.Bands[0].Columns.Exists(CaptionDisplayColumn)) //set summary footer caption
            {
                Infragistics.Win.UltraWinGrid.SummarySettings ss = this.DisplayLayout.Bands[0].Summaries.Add(this.DisplayLayout.Bands[0].Columns[CaptionDisplayColumn].Key, Infragistics.Win.UltraWinGrid.SummaryType.Count, this.DisplayLayout.Bands[0].Columns[CaptionDisplayColumn], Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn);
                if (WithCount)
                    ss.DisplayFormat = SummaryCaption + "{0:######}";
                else
                    ss.DisplayFormat = SummaryCaption;

                ss.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed | SummaryDisplayAreas.GroupByRowsFooter | SummaryDisplayAreas.InGroupByRows;
                if (SummaryAppearnce == null)
                {
                    ss.Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                    ss.Appearance.TextHAlign = this.DisplayLayout.Bands[0].Columns[CaptionDisplayColumn].CellAppearance.TextHAlign;
                }
                else
                {
                    Infragistics.Win.Appearance app = (Infragistics.Win.Appearance)SummaryAppearnce.Clone();
                    app.TextHAlign = DisplayLayout.Bands[0].Columns[CaptionDisplayColumn].CellAppearance.TextHAlign;
                    ss.Appearance = app;
                }
            }
            else if (SummaryCaption.Length > 0)
            {
                // Set the appearance for the caption on top of the summary area.
                this.DisplayLayout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.True;
                if (SummaryAppearnce != null)
                    this.DisplayLayout.Override.SummaryFooterCaptionAppearance = SummaryAppearnce;
                this.DisplayLayout.Override.SummaryFooterCaptionAppearance.TextHAlign = HAlign.Left;

                // Set the text that shows up in the caption of the summary footer.
                this.DisplayLayout.Bands[0].SummaryFooterCaption = SummaryCaption;
            }



            //add summary of all field
            for (int i = 0; i < this.DisplayLayout.Bands[0].Columns.Count; i++)
            {
                bool isAdd = false;

                if (this.DisplayLayout.Bands[0].Columns[i].Format != null)
                {
                    if (!this.DisplayLayout.Bands[0].Columns[i].Format.ToString().Contains("%"))
                    {
                        if (this.DisplayLayout.Bands[0].Columns[i].Format.ToString().Contains("#"))
                        {
                            if (pStrField.Length != 0)
                            {
                                foreach (string str in StrField)
                                {
                                    if (str.ToUpper() == this.DisplayLayout.Bands[0].Columns[i].ToString().ToUpper())
                                    {
                                        isAdd = true;
                                    }
                                }
                            }
                            else if (pStrExceptField.Length != 0)
                            {
                                isAdd = true;
                                foreach (string str in StrExceptField)
                                {
                                    if (str.ToUpper() == this.DisplayLayout.Bands[0].Columns[i].ToString().ToUpper())
                                    {
                                        isAdd = false;
                                    }
                                }
                            }
                            else
                            {
                                isAdd = true;
                            }

                            if (isAdd)
                            {
                                Infragistics.Win.UltraWinGrid.SummarySettings summary = this.DisplayLayout.Bands[0].Summaries.Add(this.DisplayLayout.Bands[0].Columns[i].Key, Infragistics.Win.UltraWinGrid.SummaryType.Sum, this.DisplayLayout.Bands[0].Columns[i], Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn);
                                summary.DisplayFormat = "{0:" + this.DisplayLayout.Bands[0].Columns[i].Format.ToString() + "}";
                                summary.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed | SummaryDisplayAreas.GroupByRowsFooter | SummaryDisplayAreas.InGroupByRows;

                                if (SummaryAppearnce == null)
                                {
                                    summary.Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
                                    summary.Appearance.TextHAlign = this.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign; //Infragistics.Win.HAlign.Right; 
                                    summary.Appearance.BackColor = Color.Pink;
                                }
                                else
                                {
                                    summary.Appearance = SummaryAppearnce;

                                }
                            }
                        }
                    }
                }
                this.DisplayLayout.Override.SummaryFooterAppearance.TextVAlign = Infragistics.Win.VAlign.Bottom;
            }
        }
        public void SetSummaryColumn(string pStrField, string pStrExceptField, string SummaryCaption, string CaptionDisplayColumn, Infragistics.Win.Appearance SummaryAppearnce)
        {
            SetSummaryColumn(pStrField, pStrExceptField, SummaryCaption, CaptionDisplayColumn, SummaryAppearnce, false);
        }
        public void SetSummaryColumn(string pStrField, string pStrExceptField, string CaptionDisplayColumn, Infragistics.Win.Appearance SummaryAppearnce)
        { 
            SetSummaryColumn(pStrField,pStrExceptField,"",CaptionDisplayColumn,SummaryAppearnce,false);
        }
        public void SetSummaryColumn(string pStrField, string pStrExceptField, string CaptionDisplayColumn, Infragistics.Win.Appearance SummaryAppearnce, bool WithCount)
        {
            SetSummaryColumn(pStrField, pStrExceptField, "", CaptionDisplayColumn, SummaryAppearnce, WithCount);
        }
        public void SetSummaryColumn(string pStrField, string pStrExceptField, string CaptionDisplayColumn, bool WithCount)
        {
            SetSummaryColumn(pStrField, pStrExceptField, "",CaptionDisplayColumn,null, WithCount);
        }
        public void SetSummaryColumn(string pStrField, string pStrExceptField)
        { 
            SetSummaryColumn(pStrField, pStrExceptField,"","", null,false);
        }
        public void SetSummaryColumn(string pStrField, string pStrExceptField, string SummaryCaption, string CaptionDisplayColumn)
        {
            SetSummaryColumn(pStrField, pStrExceptField, SummaryCaption, CaptionDisplayColumn,null,false );
        }
        public void SetSummaryColumn(string pStrField, string pStrExceptField, Infragistics.Win.Appearance SummaryAppearnce)
        {
            SetSummaryColumn(pStrField, pStrExceptField,"","", SummaryAppearnce,false);
        }
 
        public void SetSummaryCount(Infragistics.Win.Appearance SummaryAppearnce)
        {
            if (this.DisplayLayout.Bands[0].Summaries.Count == 0 && this.DisplayLayout.Bands[0].Columns.Count >0 )
            {
                //Infragistics.Win.UltraWinGrid.SummarySettings summary = this.DisplayLayout.Bands[0].Summaries.Add(Infragistics.Win.UltraWinGrid.SummaryType.Count, this.DisplayLayout.Bands[0].Columns[this.DisplayLayout.Bands[0].Columns.Count - 1], Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn);

                Infragistics.Win.UltraWinGrid.SummarySettings summary = this.DisplayLayout.Bands[0].Summaries.Add(Infragistics.Win.UltraWinGrid.SummaryType.Count, this.DisplayLayout.Bands[0].Columns[0].GetRelatedVisibleColumn(VisibleRelation.Last), Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn);
                summary.DisplayFormat = "{0:######}";
                summary.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed | SummaryDisplayAreas.GroupByRowsFooter | SummaryDisplayAreas.InGroupByRows;

                if (SummaryAppearnce == null)
                {
                    summary.Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
                    //summary.Appearance.TextHAlign = this.DisplayLayout.Bands[0].Columns[this.DisplayLayout.Bands[0].Columns.Count - 1].CellAppearance.TextHAlign; // Infragistics.Win.HAlign.Right;                
                    summary.Appearance.TextHAlign = this.DisplayLayout.Bands[0].Columns[this.DisplayLayout.Bands[0].Columns[0].GetRelatedVisibleColumn(VisibleRelation.Last).Key].CellAppearance.TextHAlign; // Infragistics.Win.HAlign.Right;                
                }
                else
                    summary.Appearance = SummaryAppearnce;
            }
        }
        public void SetSummaryCount()
        {
            SetSummaryCount(null); 
        }
        public void RowSummaryCaptionDisplay(int pBand, string pKeyName, string pColumnName, string pDisplayString, AppearanceBase pApperances)
        {
            SummarySettings RowSum;
            if (!this.DisplayLayout.Bands[pBand].Summaries.Exists(pKeyName + pColumnName))
            {
                RowSum = this.DisplayLayout.Bands[pBand].Summaries.Add(pKeyName + pColumnName, SummaryType.Formula, this.DisplayLayout.Bands[pBand].Columns[pColumnName], SummaryPosition.UseSummaryPositionColumn);
            }
            else
            {
                RowSum = this.DisplayLayout.Bands[pBand].Summaries[pKeyName + pColumnName];
            }
            //if (!string.IsNullOrEmpty(pColumnFormat))
            //{
            //    RowSum.DisplayFormat = "{0:" + pColumnFormat + "}";
            //}
            RowSum.DisplayFormat = pDisplayString;
            this.DisplayLayout.Bands[pBand].Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
            SetApperance(RowSum, pApperances);
        }
        public void RowSummaryCaptionDisplay(int pBand, string pColumnName, string pDisplayString)
        {
            SummarySettings pSummary = this.DisplayLayout.Bands[pBand].Summaries.Exists("CP" + pColumnName) ? this.DisplayLayout.Bands[pBand].Summaries["CP" + pColumnName] : this.DisplayLayout.Bands[pBand].Summaries.Add("CP" + pColumnName, SummaryType.Formula, this.DisplayLayout.Bands[pBand].Columns[pColumnName], SummaryPosition.UseSummaryPositionColumn);
            pSummary.DisplayFormat = pDisplayString;
            this.DisplayLayout.Bands[pBand].Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
            pSummary.Appearance.TextHAlign = HAlign.Center;
            this.DisplayLayout.Bands[0].Override.SummaryFooterCaptionAppearance.BackColor = Color.Pink;
            pSummary.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed | SummaryDisplayAreas.GroupByRowsFooter;
         
          //  this.SetApperance(pSummary);
           
        }

        public void RowSummaryCaptionDisplayMulti(int pBand, string pColumnName, string pDisplayString)
        {
            SummarySettings pSummary = this.DisplayLayout.Bands[pBand].Summaries.Exists("CP2" + pColumnName) ? this.DisplayLayout.Bands[pBand].Summaries["CP2" + pColumnName] : this.DisplayLayout.Bands[pBand].Summaries.Add("CP2" + pColumnName, SummaryType.Formula, this.DisplayLayout.Bands[pBand].Columns[pColumnName], SummaryPosition.UseSummaryPositionColumn);
            pSummary.DisplayFormat = pDisplayString;
            this.DisplayLayout.Bands[pBand].Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
            pSummary.Appearance.TextHAlign = HAlign.Center;
            this.DisplayLayout.Bands[0].Override.SummaryFooterCaptionAppearance.BackColor = Color.Pink;
            pSummary.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed | SummaryDisplayAreas.GroupByRowsFooter;

            //  this.SetApperance(pSummary);

        }


        public void RowSummaryCaptionDisplayNew(int pBand, string pColumnName, string pDisplayString)
        {
            SummarySettings pSummary = this.DisplayLayout.Bands[pBand].Summaries.Exists("CP" + pColumnName) ? this.DisplayLayout.Bands[pBand].Summaries["CP" + pColumnName] : this.DisplayLayout.Bands[pBand].Summaries.Add("CP" + pColumnName, SummaryType.Formula, this.DisplayLayout.Bands[pBand].Columns[pColumnName], SummaryPosition.UseSummaryPositionColumn);
            pSummary.DisplayFormat = pDisplayString;
            this.DisplayLayout.Bands[pBand].Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
            pSummary.Appearance.TextHAlign = HAlign.Center;
            this.DisplayLayout.Bands[0].Override.SummaryFooterCaptionAppearance.BackColor = Color.Pink;
            pSummary.SummaryDisplayArea = SummaryDisplayAreas.GroupByRowsFooter;

            //  this.SetApperance(pSummary);

        }

        private void SetApperance(SummarySettings pSummary)
        {
            pSummary.Appearance.BackColor = Color.LightSteelBlue;
            this.DisplayLayout.Bands[0].Override.SummaryFooterCaptionAppearance.BackColor = Color.LightSkyBlue;
            pSummary.Appearance.TextHAlign = HAlign.Right;
            pSummary.Appearance.TextVAlign = VAlign.Middle;
        }
        private void SetApperance(SummarySettings pSummary, AppearanceBase pApperances)
        {
            this.SetApperance(pSummary);
            pSummary.Appearance = pApperances;
        }
        #endregion
        #endregion      
       
        #region Selection Strategy Class
        public class SelectionStrategyFilter_NoRowSelection : Infragistics.Win.ISelectionStrategyFilter
        {
            private Infragistics.Win.SelectionStrategyBase selectionStrategy = null;

            public SelectionStrategyFilter_NoRowSelection(Infragistics.Win.SelectionStrategyBase selectionStrategy)
            {
                this.selectionStrategy = selectionStrategy;
            }

            public Infragistics.Win.ISelectionStrategy GetSelectionStrategy(Infragistics.Shared.ISelectableItem item)
            {
                if (item is Infragistics.Win.UltraWinGrid.UltraGridRow)
                    return this.selectionStrategy;
                return null;
            }
        }
        #endregion

        //private void toolPasteMenuitem_Click(object sender, EventArgs e)
        //{            
        //    this.PerformAction(UltraGridAction.Paste, true, true);
        //    this.DisplayLayout.Override.AllowMultiCellOperations = AllowMultiCellOperation.Paste;
        //}

        //private void toolCopyMenuItem_Click(object sender, EventArgs e)
        //{
        //    SendKeys.Send("^c");
        //    //this.PerformAction(UltraGridAction.Copy, true, true);
        //    //this.DisplayLayout.Override.AllowMultiCellOperations=
        //}

        private void toolShowColumns_Click(object sender, EventArgs e)
        {
            DataTable dtCols = CreateColsTable().Copy();
           
            FrmSort f = new FrmSort(dtCols);
            f.ShowForm();                                                
        }

        private void toolResizeCol_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            if (menuItem.CheckState == CheckState.Checked)
                this.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand,true); 
            else
                this.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.None); 
        }

        private void toolShowColumnDetail_Click(object sender, EventArgs e)
        {
            EventHandler eh = this.ShowColumns;
            if (eh != null)
            {                
                eh.Invoke(this, EventArgs.Empty);
            }
        }          
    }
}