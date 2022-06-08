using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace UTC
{
    public partial class frmSelect : Form
    {
        #region VARIABLES
        int mintCounter;
        ListViewItem FoundItem;

        public Boolean BlnSpaceAllow = false;
        public String mStrColName="";
        public String mStrColHead = "";
        public String mStrColWidth="";
        public String mStrRetColName = "";
        public bool mBlnShowDialog = false;
        public DataTable mDTable;
        public String mStrCaption;
        string[] mStrArrayName;
        string[] mStrArrayWidth;

        #endregion

        #region PROPERTIES
        private ColumnHeaderAutoResizeStyle _resizeStyle = ColumnHeaderAutoResizeStyle.HeaderSize;
        public ColumnHeaderAutoResizeStyle AutoResizeStyle
        {
            get { return _resizeStyle; }
            set { _resizeStyle = value; }
        }

        private string _SPList;
        public string DoubleQuateList
        {
            get { return _SPList; }
            set { _SPList = value; }
        }

        private string _List;
        public string SingleQuateList
        {
            get { return _List; }
            set { _List = value; }
        }

        private string  _DPList;

        public string  List
        {
            get { return _DPList; }
            set { _DPList = value; }
        }

        #endregion
       
        #region Constructor

        public frmSelect()
        {
            InitializeComponent();
            this.KeyPreview = true;
            lstVw.Columns.Clear();
        }
        #endregion Constructor

        #region PUBLIC METHODS
        public void ShowForm(DataTable dt)
        {            
            if (dt == null) return;            
            SetTable(dt);
            SetDataTableColumns(); 
            lstVw.Items.Clear();
            mintCounter = 0;            
            foreach (DataRow DRow in mDTable.Rows)
            {
                FoundItem = new ListViewItem();

                for (int IntCount = 0; IntCount < mDTable.Columns.Count; IntCount++)
                {
                    FoundItem.SubItems.Add(DRow[IntCount].ToString());
                }

                FoundItem.Checked = string.IsNullOrEmpty(DRow["BlnSel"].ToString()) ? false : Convert.ToBoolean(DRow["BlnSel"]);
                lstVw.Items.Add(FoundItem);
                mintCounter++;
            }
            
            lstVw.AutoResizeColumns(AutoResizeStyle);
            this.ShowDialog();
        }
        
        public void ShowForm()
        {
            mintCounter = 0;            
            FillColumn();
            lstVw.Items.Clear();
            if (mDTable == null || mDTable.Rows.Count == 0)
            {
                MessageBox.Show("Data Not Found"); 
                return;
            }

            if (mStrArrayName == null || mStrArrayName.Length == 0)
            {
                ShowForm(mDTable);
                return;
            }

            foreach (DataRow DRow in mDTable.Rows)
            {
                FoundItem = new ListViewItem();

                for (int IntCount = 0; IntCount <= mStrArrayName.GetUpperBound(0); IntCount++)
                {
                    FoundItem.SubItems.Add(DRow[IntCount].ToString());
                }

                FoundItem.Checked =Convert.ToBoolean(DRow["BlnSel"]);
                lstVw.Items.Add(FoundItem);
                mintCounter++;
            }
            this.Text = "Select " + mStrCaption;            
            lstVw.AutoResizeColumns(AutoResizeStyle);
            if (mBlnShowDialog)
                this.ShowDialog();
            else
                this.Show();
        }

        public void SetColumn(string pStrColumn, string pStrColWidth)
        {
            mStrColName = pStrColumn;
            mStrColWidth = pStrColWidth;
        }

        public void SetTable(DataTable pDTable)
        {
            mDTable = pDTable;
            if (pDTable.Columns.IndexOf("BlnSel") < 0)
            {
                pDTable.Columns.Add("BlnSel", System.Type.GetType("System.Boolean"));
                for (int i = 0; i < pDTable.Rows.Count; i++)
                {
                    pDTable.Rows[i]["BlnSel"] = false;
                }
            }
            PropertyCollection prop;
            prop = pDTable.ExtendedProperties;
            prop.Clear();
            prop.Add("SPList", "");
            prop.Add("DPList", "");
        }
        #endregion

        #region private method
        private void SetDataTableColumns()
        {
            if (lstVw.Columns.Count != 0) return;
            lstVw.Columns.Add("BlnSel");
            foreach (DataColumn d in mDTable.Columns)
            {
                if (d.ColumnName != "BlnSel")
                    lstVw.Columns.Add(d.ColumnName);
            }
        }
        private void FillColumn()
        {
            if (lstVw.Columns.Count != 0) return;
            if (mStrArrayName == null || mStrArrayName.Length == 0)return ;            
            mStrArrayName = mStrColName.Split(',');
            mStrArrayWidth = mStrColWidth.Split(',');
            lstVw.Columns.Add("BlnSel", 50);
            for (int IntCount = 0; IntCount <= mStrArrayName.GetUpperBound(0); IntCount++)
            {
                lstVw.Columns.Add(mStrArrayName[IntCount], Convert.ToInt32(mStrArrayWidth[IntCount]));
            }
            SetColHead();             
        }
        private void SetColHead()
        {
            if (lstVw.Columns.Count == 0) return;
            if (mStrColHead ==null || mStrColHead.Length == 0) return;
            mStrArrayName = mStrColHead.Split(',');
            lstVw.Columns[0].Text = "Sel";
            for (int IntCount = 0; IntCount <= mStrArrayName.GetUpperBound(0); IntCount++)
            {
                lstVw.Columns[IntCount + 1].Text = mStrArrayName[IntCount];
            }            
        }
        private void SelectList()
        {
            PropertyCollection prop;
            prop = mDTable.ExtendedProperties;

            String StrSPList = String.Empty;
            String StrDPList = String.Empty;
            String StrList = String.Empty;

            mintCounter = 0;
            foreach (DataRow DRow in mDTable.Rows)
            {
                if (lstVw.Items[mintCounter].Checked == true)
                {
                    if (mStrRetColName.Length != 0)
                    {
                        if (StrSPList.Length != 0) StrSPList = StrSPList + ",";
                        StrSPList = StrSPList + "''" + DRow[mStrRetColName].ToString() + "''";

                        if (StrList.Length != 0) StrList = StrList + ",";
                        StrList = StrList + "'" + DRow[mStrRetColName].ToString() + "'";

                        if (StrDPList.Length != 0) StrDPList = StrDPList + ",";
                        StrDPList = StrDPList + DRow[mStrRetColName].ToString();
                    }
                    else
                    {
                        if (StrSPList.Length != 0) StrSPList = StrSPList + ",";
                        StrSPList = StrSPList + "''" + DRow[0].ToString() + "''";

                        if (StrList.Length != 0) StrList = StrList + ",";
                        StrList = StrList + "'" + DRow[0].ToString() + "'";

                        if (StrDPList.Length != 0) StrDPList = StrDPList + ",";
                        StrDPList = StrDPList + DRow[0].ToString();
                    }
                }
                mintCounter++;
            }
            prop.Clear();
            prop.Add("SPList", "'" + StrSPList + "'");
            prop.Add("DPList", StrDPList);
            prop.Add("List", StrList);

            DoubleQuateList = "'" + StrSPList + "'";
            SingleQuateList = StrList;
            List = StrDPList;
        }      
        #endregion 

        #region Validation

        private void frmSelect_Resize(object sender, System.EventArgs e)
        {
           // Val.frmResize(this);
        }

        private void frmSelect_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //Val.FormKeyDownEvent(sender, e);
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.Control == true && e.KeyCode == Keys.W)
            {
                this.Close();
            }
        }

        private void frmSelect_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        #endregion Validation

        #region UI OPE
        private void lstVwLot_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            mDTable.Rows[e.Index]["BlnSel"] = !lstVw.Items[e.Index].Checked;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            mintCounter = 0;
            foreach (DataRow DRow in mDTable.Rows)
            {
                DRow["BlnSel"] = false;
                lstVw.Items[mintCounter].Checked = false;
                mintCounter++;
            }
        }
        private void btnAll_Click(object sender, EventArgs e)
        {
            mintCounter = 0;
            foreach (DataRow DRow in mDTable.Rows)
            {
                DRow["BlnSel"] = true;
                lstVw.Items[mintCounter].Checked = true;
                mintCounter++;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            lstVw.HideSelection = false;
            lstVw.SelectedItems.Clear();
            FoundItem = lstVw.FindItemWithText(txtSearch.Text);
            if (FoundItem != null)
            {
                if (FoundItem.Index != -1)
                {
                    lstVw.Items[FoundItem.Index].Selected = true;
                    lstVw.Items[FoundItem.Index].EnsureVisible();
                    FoundItem.Focused = true;
                }
            }
            else
            {
                lstVw.SelectedItems.Clear();
            }
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (lstVw.SelectedItems.Count == 0) return;
                FoundItem = lstVw.SelectedItems[0];
                if (FoundItem == null) return;
                if (FoundItem.Index != -1)
                {
                    lstVw.Items[FoundItem.Index].Selected = true;
                    lstVw.Items[FoundItem.Index].EnsureVisible();
                    lstVw.Items[FoundItem.Index].Checked = !lstVw.Items[FoundItem.Index].Checked;
                   // mDTable.Rows[FoundItem.Index]["BlnSel"] = !lstVw.Items[FoundItem.Index].Checked;
                    if (BlnSpaceAllow == false)
                    {
                        e.SuppressKeyPress = true;
                    }
                }
            }
            else
            {
                if (lstVw.SelectedItems.Count !=0)
                lstVw.SelectedItems[0].Focused =true ;
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            SelectList();
            this.Close();
        }
        #endregion Operations
    }
}