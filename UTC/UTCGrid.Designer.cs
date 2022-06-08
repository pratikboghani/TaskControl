using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace UTC
{
    public partial class UTCGrid : Infragistics.Win.UltraWinGrid.UltraGrid
    {
        [System.Diagnostics.DebuggerNonUserCode()]

        public UTCGrid(System.ComponentModel.IContainer Container)
            : this()
        {   

            //Required for Windows.Forms Class Composition Designer support
           // Container.Add(this);

        }

        [System.Diagnostics.DebuggerNonUserCode()]
        public UTCGrid()
            : base()
        {

            //This call is required by the Component Designer.
            InitializeComponent();

        }

        //Component overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {                                
                dtSortColumn = null;
                dtGridSource = null;
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        //Required by the Component Designer
        private System.ComponentModel.IContainer components;

        //NOTE: The following procedure is required by the Component Designer
        //It can be modified using the Component Designer.
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mnuCon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterExceltoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterToptoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FreezeTollStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExporttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripGrpBoxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SortingtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SortMultitoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ColumntoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyPastetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RowSizetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CellSumtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolCopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolPasteMenuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolShowColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.toolResizeCol = new System.Windows.Forms.ToolStripMenuItem();
            this.toolShowColumnDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuCon
            // 
            this.mnuCon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterToolStripMenuItem,
            this.FilterExceltoolStripMenuItem,
            this.FilterToptoolStripMenuItem,
            this.FreezeTollStripMenuItem,
            this.ExporttoolStripMenuItem,
            this.toolStripGrpBoxMenuItem,
            this.SortingtoolStripMenuItem,
            this.SortMultitoolStripMenuItem,
            this.ColumntoolStripMenuItem,
            this.CopyPastetoolStripMenuItem,
            this.RowSizetoolStripMenuItem,
            this.CellSumtoolStripMenuItem,
            this.toolCopyMenuItem,
            this.toolPasteMenuitem,
            this.toolShowColumns,
            this.toolResizeCol,
            this.toolShowColumnDetail});
            this.mnuCon.Name = "mnuCon";
            this.mnuCon.Size = new System.Drawing.Size(198, 400);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.BackColor = System.Drawing.Color.LavenderBlush;
            this.filterToolStripMenuItem.CheckOnClick = true;
            this.filterToolStripMenuItem.DoubleClickEnabled = true;
            this.filterToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.filterToolStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.ShowShortcutKeys = false;
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // FilterExceltoolStripMenuItem
            // 
            this.FilterExceltoolStripMenuItem.CheckOnClick = true;
            this.FilterExceltoolStripMenuItem.DoubleClickEnabled = true;
            this.FilterExceltoolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilterExceltoolStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.FilterExceltoolStripMenuItem.Name = "FilterExceltoolStripMenuItem";
            this.FilterExceltoolStripMenuItem.ShowShortcutKeys = false;
            this.FilterExceltoolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.FilterExceltoolStripMenuItem.Text = "Filter Multiple";
            this.FilterExceltoolStripMenuItem.Click += new System.EventHandler(this.FilterExceltoolStripMenuItem_Click);
            // 
            // FilterToptoolStripMenuItem
            // 
            this.FilterToptoolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.FilterToptoolStripMenuItem.CheckOnClick = true;
            this.FilterToptoolStripMenuItem.DoubleClickEnabled = true;
            this.FilterToptoolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.FilterToptoolStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.FilterToptoolStripMenuItem.Name = "FilterToptoolStripMenuItem";
            this.FilterToptoolStripMenuItem.ShowShortcutKeys = false;
            this.FilterToptoolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.FilterToptoolStripMenuItem.Text = "Filter Row - Top";
            this.FilterToptoolStripMenuItem.Click += new System.EventHandler(this.FilterToptoolStripMenuItem_Click);
            // 
            // FreezeTollStripMenuItem
            // 
            this.FreezeTollStripMenuItem.BackColor = System.Drawing.Color.LavenderBlush;
            this.FreezeTollStripMenuItem.CheckOnClick = true;
            this.FreezeTollStripMenuItem.DoubleClickEnabled = true;
            this.FreezeTollStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.FreezeTollStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.FreezeTollStripMenuItem.Name = "FreezeTollStripMenuItem";
            this.FreezeTollStripMenuItem.ShowShortcutKeys = false;
            this.FreezeTollStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.FreezeTollStripMenuItem.Text = "Freeze";
            this.FreezeTollStripMenuItem.Click += new System.EventHandler(this.freezeToolStripMenuItem_Click);
            // 
            // ExporttoolStripMenuItem
            // 
            this.ExporttoolStripMenuItem.BackColor = System.Drawing.Color.LavenderBlush;
            this.ExporttoolStripMenuItem.DoubleClickEnabled = true;
            this.ExporttoolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ExporttoolStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.ExporttoolStripMenuItem.Name = "ExporttoolStripMenuItem";
            this.ExporttoolStripMenuItem.ShowShortcutKeys = false;
            this.ExporttoolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.ExporttoolStripMenuItem.Text = "Export To Excel";
            this.ExporttoolStripMenuItem.Click += new System.EventHandler(this.ExporttoolStripMenuItem_Click);
            // 
            // toolStripGrpBoxMenuItem
            // 
            this.toolStripGrpBoxMenuItem.CheckOnClick = true;
            this.toolStripGrpBoxMenuItem.DoubleClickEnabled = true;
            this.toolStripGrpBoxMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripGrpBoxMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.toolStripGrpBoxMenuItem.Name = "toolStripGrpBoxMenuItem";
            this.toolStripGrpBoxMenuItem.ShowShortcutKeys = false;
            this.toolStripGrpBoxMenuItem.Size = new System.Drawing.Size(197, 22);
            this.toolStripGrpBoxMenuItem.Text = "Show Group By Box";
            this.toolStripGrpBoxMenuItem.Click += new System.EventHandler(this.toolStripGrpBoxMenuItem_Click);
            // 
            // SortingtoolStripMenuItem
            // 
            this.SortingtoolStripMenuItem.CheckOnClick = true;
            this.SortingtoolStripMenuItem.DoubleClickEnabled = true;
            this.SortingtoolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SortingtoolStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.SortingtoolStripMenuItem.Name = "SortingtoolStripMenuItem";
            this.SortingtoolStripMenuItem.ShowShortcutKeys = false;
            this.SortingtoolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.SortingtoolStripMenuItem.Text = "Header Click Sorting";
            this.SortingtoolStripMenuItem.Click += new System.EventHandler(this.SortingtoolStripMenuItem_Click);
            // 
            // SortMultitoolStripMenuItem
            // 
            this.SortMultitoolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.SortMultitoolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.SortMultitoolStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.SortMultitoolStripMenuItem.Name = "SortMultitoolStripMenuItem";
            this.SortMultitoolStripMenuItem.ShowShortcutKeys = false;
            this.SortMultitoolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.SortMultitoolStripMenuItem.Text = "Sort Multi Columns ";
            this.SortMultitoolStripMenuItem.Click += new System.EventHandler(this.SortMultitoolStripMenuItem_Click);
            // 
            // ColumntoolStripMenuItem
            // 
            this.ColumntoolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ColumntoolStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.ColumntoolStripMenuItem.Name = "ColumntoolStripMenuItem";
            this.ColumntoolStripMenuItem.ShowShortcutKeys = false;
            this.ColumntoolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.ColumntoolStripMenuItem.Text = "Column Chooser";
            this.ColumntoolStripMenuItem.Click += new System.EventHandler(this.ColumntoolStripMenuItem_Click);
            // 
            // CopyPastetoolStripMenuItem
            // 
            this.CopyPastetoolStripMenuItem.CheckOnClick = true;
            this.CopyPastetoolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.CopyPastetoolStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.CopyPastetoolStripMenuItem.Name = "CopyPastetoolStripMenuItem";
            this.CopyPastetoolStripMenuItem.ShowShortcutKeys = false;
            this.CopyPastetoolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.CopyPastetoolStripMenuItem.Text = "Enable Copy Paste";
            this.CopyPastetoolStripMenuItem.Click += new System.EventHandler(this.CopyPastetoolStripMenuItem_Click);
            // 
            // RowSizetoolStripMenuItem
            // 
            this.RowSizetoolStripMenuItem.CheckOnClick = true;
            this.RowSizetoolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.RowSizetoolStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.RowSizetoolStripMenuItem.Name = "RowSizetoolStripMenuItem";
            this.RowSizetoolStripMenuItem.ShowShortcutKeys = false;
            this.RowSizetoolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.RowSizetoolStripMenuItem.Text = "Row Resize Free";
            this.RowSizetoolStripMenuItem.Click += new System.EventHandler(this.RowSizetoolStripMenuItem_Click);
            // 
            // CellSumtoolStripMenuItem
            // 
            this.CellSumtoolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.CellSumtoolStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.CellSumtoolStripMenuItem.Name = "CellSumtoolStripMenuItem";
            this.CellSumtoolStripMenuItem.ShowShortcutKeys = false;
            this.CellSumtoolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.CellSumtoolStripMenuItem.Text = "Selected Cell Summary";
            this.CellSumtoolStripMenuItem.Click += new System.EventHandler(this.CellSumtoolStripMenuItem_Click);
            // 
            // toolCopyMenuItem
            // 
            this.toolCopyMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolCopyMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.toolCopyMenuItem.Name = "toolCopyMenuItem";
            this.toolCopyMenuItem.ShowShortcutKeys = false;
            this.toolCopyMenuItem.Size = new System.Drawing.Size(197, 22);
            this.toolCopyMenuItem.Text = "Copy";
            this.toolCopyMenuItem.Click += new System.EventHandler(this.toolCopyMenuItem_Click);
            // 
            // toolPasteMenuitem
            // 
            this.toolPasteMenuitem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolPasteMenuitem.ForeColor = System.Drawing.Color.Indigo;
            this.toolPasteMenuitem.Name = "toolPasteMenuitem";
            this.toolPasteMenuitem.ShowShortcutKeys = false;
            this.toolPasteMenuitem.Size = new System.Drawing.Size(197, 22);
            this.toolPasteMenuitem.Text = "Paste";
            this.toolPasteMenuitem.Click += new System.EventHandler(this.toolPasteMenuitem_Click);
            // 
            // toolShowColumns
            // 
            this.toolShowColumns.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolShowColumns.ForeColor = System.Drawing.Color.Indigo;
            this.toolShowColumns.Name = "toolShowColumns";
            this.toolShowColumns.ShowShortcutKeys = false;
            this.toolShowColumns.Size = new System.Drawing.Size(197, 22);
            this.toolShowColumns.Text = "Columns Detail";
            this.toolShowColumns.Click += new System.EventHandler(this.toolShowColumns_Click);
            // 
            // toolResizeCol
            // 
            this.toolResizeCol.CheckOnClick = true;
            this.toolResizeCol.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolResizeCol.ForeColor = System.Drawing.Color.Indigo;
            this.toolResizeCol.Name = "toolResizeCol";
            this.toolResizeCol.ShowShortcutKeys = false;
            this.toolResizeCol.Size = new System.Drawing.Size(197, 22);
            this.toolResizeCol.Text = "Auto Resize Columns";
            this.toolResizeCol.Click += new System.EventHandler(this.toolResizeCol_Click);
            // 
            // toolShowColumnDetail
            // 
            this.toolShowColumnDetail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolShowColumnDetail.ForeColor = System.Drawing.Color.Indigo;
            this.toolShowColumnDetail.Name = "toolShowColumnDetail";
            this.toolShowColumnDetail.ShowShortcutKeys = false;
            this.toolShowColumnDetail.Size = new System.Drawing.Size(197, 22);
            this.toolShowColumnDetail.Text = "Set Columns";
            this.toolShowColumnDetail.Click += new System.EventHandler(this.toolShowColumnDetail_Click);
            // 
            // PTGrid
            // 
            this.ContextMenuStrip = this.mnuCon;
            this.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.UltraGrid_InitializeLayout);
            this.mnuCon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private ContextMenuStrip mnuCon;
        private ToolStripMenuItem filterToolStripMenuItem;
        private ToolStripMenuItem FreezeTollStripMenuItem;
        private ToolStripMenuItem ExporttoolStripMenuItem;
        private ToolStripMenuItem toolStripGrpBoxMenuItem;
        private ToolStripMenuItem FilterExceltoolStripMenuItem;
        private ToolStripMenuItem SortingtoolStripMenuItem;
        private ToolStripMenuItem FilterToptoolStripMenuItem;
        private ToolStripMenuItem SortMultitoolStripMenuItem;
        private ToolStripMenuItem ColumntoolStripMenuItem;
        private ToolStripMenuItem CopyPastetoolStripMenuItem;
        private ToolStripMenuItem RowSizetoolStripMenuItem;
        private ToolStripMenuItem CellSumtoolStripMenuItem;
        private ToolStripMenuItem toolPasteMenuitem;
        private ToolStripMenuItem toolCopyMenuItem;
        private ToolStripMenuItem toolShowColumns;
        private ToolStripMenuItem toolResizeCol;
        private ToolStripMenuItem toolShowColumnDetail;

    }

} //end of root namespace