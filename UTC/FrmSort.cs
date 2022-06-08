using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;

namespace UTC
{
    public partial class FrmSort : Form
    {
        private DataTable mTable = null;
        private const string SortTable = "tblSort";
        public string TableName
        {
            get { return SortTable; }
        }

        private DataSet _DS = new DataSet();
        public DataSet DS
        {
            get { return _DS; }
        }

        private DataTable _dt = null;
        public DataTable SortFields
        {
            get { return _dt; }
            set { _dt=value; }            
        }  
 
        public FrmSort()
        {
           
        }

        public FrmSort(DataTable dtComboSource, DataTable dtGridSource)
        {
            InitializeComponent();
            mTable = dtComboSource;
            SortFields = dtGridSource;
            FillColumns();
        }
        public FrmSort(DataTable dtGridSource)
        {
            InitializeComponent();            
            SortFields = dtGridSource;
            if (!DS.Tables.Contains(SortFields.TableName))
                DS.Tables.Add(SortFields.Copy());

            this.Text = "Column Detail";
            UltGrdCol.SetDataBinding(DS, this.TableName, false);
            UltGrdCol.SetOperation(false , false , false);
            UltGrdCol.DisplayLayout.Bands[0].Columns[0].Header.Caption = "Column Name";
            UltGrdCol.DisplayLayout.Bands[0].Columns[0].Width = 100;
            UltGrdCol.DisplayLayout.Bands[0].Columns[0].CellAppearance.TextHAlign = HAlign.Left;
            UltGrdCol.DisplayLayout.Bands[0].Columns[1].Header.Caption = "Key";            
            UltGrdCol.DisplayLayout.Bands[0].Columns[1].Width = 100;
            UltGrdCol.DisplayLayout.Bands[0].Columns[1].CellAppearance.TextHAlign = HAlign.Left;
            UltGrdCol.SetColumnHide("ORDER", true); 
        }

        public void ShowForm()
        {
            this.ShowDialog();
        }

        private void FillColumns()
        {            
            if (!DS.Tables.Contains(SortFields.TableName))            
                DS.Tables.Add(SortFields.Copy());

            UltGrdCol.SetDataBinding(DS, this.TableName, true);               
            UltGrdCol.SetOperation(true, true, true, UTC.UTCGrid.EnumCellActivation.AllowEdit);
            UltGrdCol.SetFocus("COLUMN_NAME");
        }

        private void UltGrdCol_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            this.UltGrdCol.DisplayLayout.Bands[0].Columns["COLUMN_NAME"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            this.UltGrdCol.DisplayLayout.Bands[0].Columns["ORDER"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;

            ValueList valueList = this.UltGrdCol.DisplayLayout.ValueLists.Add("COLUMN_NAME");
            DataView dv = new DataView();
            if (mTable == null) return;
            dv =mTable.DefaultView;
            //dv = SortFields.DefaultView;
            dv.Sort = "COLUMN_NAME";
            DataTable sDt = dv.ToTable();

            foreach (DataRow dr in sDt.Rows)
                valueList.ValueListItems.Add(dr["COLUMN_KEY"].ToString(),dr["COLUMN_NAME"].ToString());

            this.UltGrdCol.DisplayLayout.Bands[0].Columns["COLUMN_NAME"].ValueList = valueList;
            
            valueList = this.UltGrdCol.DisplayLayout.ValueLists.Add("ORDER");
            valueList.ValueListItems.Add("(none)");
            valueList.ValueListItems.Add("A to Z");
            valueList.ValueListItems.Add("Z to A");
            this.UltGrdCol.DisplayLayout.Bands[0].Columns["ORDER"].ValueList = valueList;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            UltGrdCol.UpdateData();
            DS.AcceptChanges();
            _dt = DS.Tables[this.TableName];
            this.Close();
        }
     

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            this.DS.Tables[this.TableName].Clear();
            UltGrdCol.UpdateData();
            DS.AcceptChanges();
            _dt = DS.Tables[this.TableName];
        }
    }
}
