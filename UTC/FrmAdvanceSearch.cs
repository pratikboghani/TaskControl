using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace UTC
{
    public partial class FrmAdvanceSearch : Form
    {
        public event DataGridViewCellFormattingEventHandler OnDataGridCellFormatting;

        #region PROPERTY
        private DataTable mDTable;
        private string _mStrRet="";
        private string _mStrRetDisp = "";
        private bool CheckedBoxColumn = false;        
        private System.Windows.Forms.ToolTip TT1 = new ToolTip();            
        private DataGridViewAutoSizeColumnsMode _resizeStyle = DataGridViewAutoSizeColumnsMode.AllCells;

        public DataGridViewAutoSizeColumnsMode AutoResizeStyle
        {
            get { return _resizeStyle; }
            set { _resizeStyle = value; }
        }


        private bool _enableAlternateRowColor = true  ;
        public bool EnableAlternateRowColor
        {
            get { return _enableAlternateRowColor; }
            set { _enableAlternateRowColor = value; }
        }

        private bool _spcCharacter = false;
        public bool FilterSpecialCharacter 
        {
            get { return _spcCharacter; }
            set { _spcCharacter = value; }
        }

        private DataGridViewRow _mGrdRow = null;
        public DataGridViewRow SelectedRow
        {
            get { return _mGrdRow; }
            set { _mGrdRow = value; }
        }

        private DataRow _mDataRow = null;
        public DataRow SelectedDataRow
        {
            get { return _mDataRow; }
            set { _mDataRow = value; }
        }

        private string _retVal="";
        public string SelectedValue
        {
            get { return _retVal; }           
        }

        private string _retDispVal = "";
        public string SelectedDispValue
        {
            get { return _retDispVal; }
        }
        private string _doubleQuotList;
        public string DoubleQuateList
        {
            get { return _doubleQuotList; }
            //set { _SPList = value; }
        }

        private string _singleQuotList;
        public string SingleQuateList
        {
            get { return _singleQuotList; }
            //set { _List = value; }
        }

        private string _plainList;
        public string List
        {
            get { return _plainList; }
            //set { _DPList = value; }
        }

        private string _dispList;
        public string DispList
        {
            get { return _dispList; }           
        }
        #endregion

        #region CTOR
        
        public FrmAdvanceSearch()
        {
            InitializeComponent();            
        }
        public void DataSource(DataTable dt, string ReturnField, bool IsMultiSelect, string ColumnsList, string ColumnsCaptions)
        {
            _mStrRet = ReturnField;
            CheckedBoxColumn = IsMultiSelect;
            mDTable = dt;

            SetDatasource(ColumnsList, ColumnsCaptions);
        }

        public FrmAdvanceSearch(DataTable dt, bool IsMultiSelect)
        {           
            SetConstructor(dt, "", IsMultiSelect, "", "");
        }
        public FrmAdvanceSearch(DataTable dt, bool IsMultiSelect,string ColumnsList,string ColumnsCaptions)
        {                      
            SetConstructor(dt, "", IsMultiSelect, ColumnsList, ColumnsCaptions);
        }
        public FrmAdvanceSearch(DataTable dt, string ReturnField, bool IsMultiSelect)
        {            
            SetConstructor(dt, ReturnField, IsMultiSelect, "", "");
        }
        public FrmAdvanceSearch(DataTable dt, string ReturnField, bool IsMultiSelect, string ColumnsList, string ColumnsCaptions)
        {         
            SetConstructor(dt, ReturnField, IsMultiSelect, ColumnsList, ColumnsCaptions);
        }
        public FrmAdvanceSearch(DataTable dt, string ReturnField, string ReturnDisplayField, bool IsMultiSelect, string ColumnsList, string ColumnsCaptions)
        {
            _mStrRetDisp = ReturnDisplayField;
            SetConstructor(dt, ReturnField, IsMultiSelect, ColumnsList, ColumnsCaptions);
        }
        private void SetConstructor(DataTable dt, string ReturnField, bool IsMultiSelect, string ColumnsList, string ColumnsCaptions)
        {
            InitializeComponent();
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, dgvSearch, new object[] { true });
            
            _mStrRet = ReturnField;
            CheckedBoxColumn = IsMultiSelect;            
            mDTable = dt;

            SetDatasource(ColumnsList, ColumnsCaptions);                
        }
        public void SetDefaultSearchValue(string strFieldName,string FieldValue)
        { 
            var ctrls= this.Controls.Find("txt" + strFieldName,true);
            if(ctrls.Length >0)
                ctrls[0].Text = FieldValue;
        }
        public void SetFocusOnSearchBox(string strFieldName)
        {
            var ctrls = this.Controls.Find("txt" + strFieldName, true);
            if (ctrls.Length > 0)
                ctrls[0].Select();
        }

        public void SetSelectedList(string strList)
        {
            if (!CheckedBoxColumn) return;
            if (mDTable == null || mDTable.Rows.Count == 0) return;
            string strCond = string.Empty;
            DataRow[] dr = null;
            //if(strList.Length ==0)  //IF BLANK LIST IS PASSED THEN CLEAR DATATABLE
            //{
                foreach (DataRow DRow in mDTable.Rows)                             
                    DRow["Sel"] = false;                              
                
            //    return;
            //}
            foreach (string s in strList.Split(','))
            {
                //if (_mStrRet.Length > 0)
                //{
                //    strCond=_mStrRet + "='" + s + "'";
 
                //    dr= mDTable.Select(strCond);
                //    if (dr.Length >0 )
                //        dr[0]["Sel"] = true;                   
                //}
                if (_mStrRetDisp.Length > 0)
                {
                    strCond = _mStrRetDisp + "='" + s + "'";

                    dr = mDTable.Select(strCond);
                    if (dr.Length > 0)
                        dr[0]["Sel"] = true;
                }
                else
                {
                    strCond = mDTable.Columns[1].ColumnName + "='" + s + "'";
                    dr = mDTable.Select(strCond);
                    if (dr.Length > 0)
                        dr[0]["Sel"] = true;

                    //foreach (DataRow dr in mDTable.Rows)
                    //{
                    //    if (dr[1].ToString() == s)
                    //    {
                    //        dr["Sel"] = true;
                    //        break;
                    //    }
                    //}
                }
            }
        }
        #endregion

        #region METHODS        

        private void SetDatasource(string colName,string Colcaps)
        {
            if (mDTable == null || mDTable.Rows.Count == 0)
            {
                MessageBox.Show("Data Source Table Not Set");
                return;
            }

            dgvSearch.Visible = false;
            dgvSearch.Rows.Clear();
            mDTable.DefaultView.RowFilter = "";

            int k = 0;

            if (CheckedBoxColumn == true && !mDTable.Columns.Contains("Sel")) //ADD CHECKBOX COLUMN
            {
                DataColumn Col = mDTable.Columns.Add("Sel", System.Type.GetType("System.Boolean"));
                Col.SetOrdinal(k);// to put the column in position 0;                
                //mDTable.Columns.Add("Sel", System.Type.GetType("System.Boolean"));             
                k++;
            }
            else if (CheckedBoxColumn == true && mDTable.Columns.Contains("Sel")) //IF COLUMN ALREADY EXIST THEN SET ORDINAL TO 0(FIRST COLUMN) [WHEN SECOND TIME DIALOG SHOWN THEN IT WILL WANT SET ORDINAL AGAIN TO ZERO]
            {
                DataColumn Col = mDTable.Columns["Sel"];
                Col.SetOrdinal(k);               
                k++;
            }

            if (colName.Length != 0)
            {
                foreach (string s in colName.Split(','))
                {
                    DataColumn Col1 = mDTable.Columns[s.ToString().Trim()];
                    Col1.SetOrdinal(k);
                    k++;
                }
            }
          
            if (!mDTable.Columns.Contains("Fill")) //ADD FILL COLUMN TO AVOID EXTRA SPACE
                mDTable.Columns.Add("Fill");

            dgvSearch.DataSource = mDTable;

            if (Colcaps.Length > 0 && colName.Length > 0)
            {
                if (CheckedBoxColumn == true)
                {
                    Colcaps += ",Sel,Fill";
                    colName += ",Sel,Fill";
                }
                else
                {
                    Colcaps += ",Fill";
                    colName += ",Fill";
                }
                string[] caps = Colcaps.Split(',');
                string[] cols = colName.Split(',');
                int i = 0;
                if (cols.Length == cols.Length)
                {
                    foreach (DataGridViewColumn clm in dgvSearch.Columns)
                        clm.Visible = false;

                    foreach (string s in cols)
                    {
                        dgvSearch.Columns[s].Visible = true;
                        dgvSearch.Columns[s].HeaderText = caps[i];
                        i++;
                    }
                }
            }
            dgvSearch.Visible = true; 

            //if (AutoResizeStyle != DataGridViewAutoSizeColumnsMode.Fill && AutoResizeStyle != DataGridViewAutoSizeColumnsMode.None)
            //{
            //    // autosize all columns according to their content                                 
            //    dgvSearch.AutoResizeColumns(AutoResizeStyle);
            //}
            //// let the last column fill the empty space when the grid or any column is resized (more natural/expected behaviour) 
           
            //dgvSearch.Columns.GetLaUTColumn(DataGridViewElementStates.None, DataGridViewElementStates.None).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgvSearch.Columns[dgvSearch.Columns.Count - 1].HeaderText = "";
            //dgvSearch.Columns[dgvSearch.Columns.Count - 1].ReadOnly = true;
            
            
           
            ////CREATE CONTROLS AS COLUMN HEAD FOR FILTER 
            //SetControls();
        }

        public void ShowForm()
        {         
            this.Show();
        }
        
        private void SetControls()
        {
            int intColWidth, intTabIndex;

            if(CheckedBoxColumn)
                intColWidth = panel1.Left + 51;
            else
                intColWidth = panel1.Left + 20;
            intTabIndex = 0;

            foreach (DataGridViewColumn GrdCol in dgvSearch.Columns)
            {               
                if (GrdCol.Visible == false) continue; 

                if (GrdCol.ValueType.ToString() != "System.Boolean" && GrdCol.Name != "Fill")
                {
                    GrdCol.ReadOnly = true;
                    UTCTextBox TB = new UTCTextBox();
                    TB.ToolTips = GrdCol.Name;
                    //TT1.SetToolTip(TB,GrdCol.Name);
                    Point P = new Point(intColWidth, (panel1.Top - TB.Height) - 2);
                    TB.Location = P;
                    TB.Width = GrdCol.Width;
                    TB.Name = "txt" + GrdCol.Name;
                    TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    TB.ForeColor = System.Drawing.Color.Navy;
                    TB.TabIndex = intTabIndex;

                    TB.TextChanged += new EventHandler(TextBox_TextChanged);
                    TB.KeyDown += new KeyEventHandler(TextBox_KeyDown);

                    TB.AccessibleName = GrdCol.Name;
                    GrdCol.HeaderCell.Tag = TB;
                    
                    if (intTabIndex == 0)
                        TB.BackColor = Color.Linen;

                    switch (GrdCol.ValueType.ToString())
                    {
                        case "System.String":
                            TB.TextAlign = HorizontalAlignment.Left;
                            break;
                        case "System.Boolean":
                            TB.TextAlign = HorizontalAlignment.Right;
                            break;
                        case "System.DateTime":
                            GrdCol.DefaultCellStyle.Format = "dd/MM/yyyy";
                            TB.Format = "dd/MM/yyyy";
                            break;
                        case "System.Int16":
                            GrdCol.DefaultCellStyle.Format = "##########";
                            TB.TextAlign = HorizontalAlignment.Right;
                            TB.Format = "##########";
                            GrdCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "System.Int32":
                            GrdCol.DefaultCellStyle.Format = "##########";
                            TB.TextAlign = HorizontalAlignment.Right;
                            TB.Format = "##########";
                            GrdCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "System.Int64":
                            GrdCol.DefaultCellStyle.Format = "#####";
                            TB.TextAlign = HorizontalAlignment.Right;
                            TB.Format = "##########";
                            GrdCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "System.Decimal":
                            GrdCol.DefaultCellStyle.Format = "0.000";
                            TB.TextAlign = HorizontalAlignment.Right;
                            TB.Format = "##.###";
                            GrdCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "System.Double":
                            GrdCol.DefaultCellStyle.Format = "0.000";
                            TB.Format = "##.###";
                            GrdCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            TB.TextAlign = HorizontalAlignment.Right;
                            break;
                    }

                    this.Controls.Add(TB);
                    intColWidth = intColWidth + GrdCol.Width;
                    intTabIndex = intTabIndex + 1;
                }
                else if (GrdCol.ValueType.ToString() == "System.Boolean" && GrdCol.Name != "Sel")
                {
                    CheckBox CB = new CheckBox();
                    CB.ThreeState = true;
                    CB.CheckState = CheckState.Indeterminate;
                    Point P = new Point(intColWidth, (panel1.Top - CB.Height));
                    TT1.SetToolTip(CB, GrdCol.Name);
                    CB.Location = P;
                    CB.Text = "";
                    CB.CheckAlign = ContentAlignment.MiddleCenter;
                    //CB.Text = GrdCol.Name;                    
                    CB.Width = GrdCol.Width;
                    CB.Name = "txt" + GrdCol.Name;
                    CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    CB.ForeColor = System.Drawing.Color.Navy;
                    CB.TabIndex = intTabIndex;
                    CB.CheckStateChanged += new EventHandler(OnCheckStateChanged);
                    CB.AccessibleName = GrdCol.Name;
                    GrdCol.HeaderCell.Tag = CB;
                    this.Controls.Add(CB);
                    intColWidth = intColWidth + GrdCol.Width;
                    intTabIndex = intTabIndex + 1;
                }
            }
            if (CheckedBoxColumn) //IF CHECBOX COL REQ THEN ADD SELECTE ALL CHECKBOX
            {
                CheckBox c = new CheckBox();
                Point P = new Point(panel1.Left + 30, (panel1.Top - c.Height));
                c.Location = P;
                c.Name = "chkSelAll";
                c.CheckedChanged += new EventHandler(OnCheckedChanged);
                this.Controls.Add(c);
            }

            this.dgvSearch.Width = intColWidth; //+ 10;
            this.dgvSearch.Height = Screen.PrimaryScreen.WorkingArea.Height - 200;

            dgvSearch.TabIndex = intTabIndex;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height - 110;



            this.Width = intColWidth + 38;

            panel1.Width = this.Width-20;
            panel1.Height = this.Height-90; 
            
            //Point P1 = new Point(7, Screen.PrimaryScreen.WorkingArea.Height - 160);
            //LblRecords.Location = P1;

            LblRecords.Top = cmdClose.Top+5;
            LblRecords.Text = "Total :  " + dgvSearch.Rows.Count.ToString();

            if(EnableAlternateRowColor==false)
                dgvSearch.AlternatingRowsDefaultCellStyle.BackColor = dgvSearch.DefaultCellStyle.BackColor;

            dgvSearch.TabStop = true;
            base.Left = (Screen.PrimaryScreen.WorkingArea.Width - base.Width) / 2;
            base.Top = (Screen.PrimaryScreen.WorkingArea.Height - base.Height) / 2;
        }
        private void SetReturnProperty()
        {
            int retCol = 0;            
                
            if (CheckedBoxColumn)
            {
                retCol = 1;
                String StrDbleQuotList = String.Empty;
                String StrPlainList = String.Empty;
                String StrSingleList = String.Empty;
                String StrDispList = String.Empty;

                foreach (DataRow  DRow in mDTable.Rows)
                {
                    if (!string.IsNullOrEmpty(DRow["Sel"].ToString()) && Convert.ToBoolean(DRow["Sel"]) == true)
                    {
                        if (_mStrRet.Length != 0)
                        {
                            if (StrDbleQuotList.Length != 0) StrDbleQuotList = StrDbleQuotList + ",";
                            StrDbleQuotList = StrDbleQuotList + "''" + DRow[_mStrRet].ToString() + "''";

                            if (StrSingleList.Length != 0) StrSingleList = StrSingleList + ",";
                            StrSingleList = StrSingleList + "'" + DRow[_mStrRet].ToString() + "'";

                            if (StrPlainList.Length != 0) StrPlainList = StrPlainList + ",";
                            StrPlainList = StrPlainList + DRow[_mStrRet].ToString();

                            if(_mStrRetDisp.Length >0)
                            {
                                if (StrDispList.Length != 0) StrDispList = StrDispList + ",";
                                StrDispList = StrDispList + DRow[_mStrRetDisp].ToString();
                            }
                        }
                        else
                        {                            
                            if (StrDbleQuotList.Length != 0) StrDbleQuotList = StrDbleQuotList + ",";
                            StrDbleQuotList = StrDbleQuotList + "''" + DRow[retCol].ToString() + "''";

                            if (StrSingleList.Length != 0) StrSingleList = StrSingleList + ",";
                            StrSingleList = StrSingleList + "'" + DRow[retCol].ToString() + "'";

                            if (StrPlainList.Length != 0) StrPlainList = StrPlainList + ",";
                            StrPlainList = StrPlainList + DRow[retCol].ToString();
                        }
                    }
                }

                _doubleQuotList = "'" + StrDbleQuotList + "'";
                _singleQuotList = StrSingleList;                
                _plainList = StrPlainList;
                _dispList = StrDispList;
            }
            else
            {
                _mGrdRow = null;
                _retVal = "";
                if (dgvSearch.Rows.Count > 0)
                {
                    _mGrdRow = dgvSearch.CurrentRow;
                    _mDataRow = mGrdRow(dgvSearch.CurrentRow);

                    if (_mStrRet.Length > 0)
                    {
                        _retVal = dgvSearch.CurrentRow.Cells[_mStrRet].Value.ToString();
                        if (_mStrRetDisp.Length > 0)
                            _retDispVal = dgvSearch.CurrentRow.Cells[_mStrRetDisp].Value.ToString();
                    }
                    else
                        _retVal = dgvSearch.CurrentRow.Cells[0].Value.ToString();
                }
            }
            this.Close();
        }

        private DataRow mGrdRow(DataGridViewRow row)
        {            
            DataRow DRow = (row.DataBoundItem as DataRowView).Row;
            
            return DRow;
        }
        private bool IsDate(string anyString)
        {
            if (anyString == null || anyString.Length == 0)
            {
                anyString = "";
                return false;
            }
            else if (anyString.Length > 10)
            {
                if (anyString.Substring(0, 10) == "01/01/0001")
                    return false;
            }
            System.DateTime dummyDate;
            try
            {
                dummyDate = DateTime.Parse(anyString);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private bool IsTime(string ptxtTime)
        {
            if (ptxtTime == "" || ptxtTime == "00:00:00")
            {
                return false;
            }
            return true;
        }
        #endregion

        #region CONTROL EVENTS
        private void FrmSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                if (e.KeyCode == Keys.Escape && CheckedBoxColumn == false)
                {
                    _mGrdRow = null;
                    _retVal = "";
                    this.Close();
                    return;
                }
                SetReturnProperty();                               
            }            
        }
        private void dgvSearch_DoubleClick(object sender, EventArgs e)
        {
            //if (CheckedBoxColumn)
            //{
            //    foreach (DataGridViewRow r in dgvSearch.Rows) //reset checkbox
            //        r.Cells["Sel"].Value = false; 
            //    dgvSearch.CurrentRow.Cells["Sel"].Value = true;
            //}
            //SetReturnProperty();               
        }
        private void TRefresh_Tick(object sender, EventArgs e)
        {
            String StrFilter = String.Empty;

            foreach (DataGridViewColumn GrdCol in dgvSearch.Columns)
            {
                if (GrdCol.HeaderCell.Tag == null) continue;
                TextBox TB = GrdCol.HeaderCell.Tag as TextBox;
               // MessageBox.Show(TB.MaxLength.ToString()); 
                if (TB != null)
                {
                    switch (GrdCol.ValueType.ToString())
                    {
                        case "System.String":
                            if (TB.TextLength != 0)
                            {
                                if(FilterSpecialCharacter ==true)
                                    StrFilter = StrFilter + '[' + GrdCol.Name + "] Like '" + EscapeLikeValue(TB.Text)  + "%' And ";
                                else
                                    StrFilter = StrFilter + '[' + GrdCol.Name + "] Like '" + TB.Text.Replace("'", "''") + "%' And ";
                            }
                            break;
                        case "System.Double":
                        case "System.Decimal":
                            if (TB.Text.Length > 0 && Convert.ToDouble(TB.Text) != 0)
                                StrFilter = StrFilter + '[' + GrdCol.Name + "] = " + Convert.ToDouble(TB.Text) + " And ";
                            break;
                        case "System.Int16":
                            if (TB.Text.Length > 0 && Convert.ToInt16(TB.Text) != 0)
                                StrFilter = StrFilter + '[' + GrdCol.Name + "] = " + Convert.ToInt16(TB.Text) + " And ";
                            break;
                        case "System.Int32":
                            if (TB.Text.Length > 0 && Convert.ToInt32(TB.Text) != 0)
                                StrFilter = StrFilter + '[' + GrdCol.Name + "] = " + Convert.ToInt32(TB.Text) + " And ";
                            break;
                        case "System.Int64":
                            if (TB.Text.Length > 0 && Convert.ToInt64(TB.Text) != 0)
                                StrFilter = StrFilter + '[' + GrdCol.Name + "] = " + Convert.ToInt64(TB.Text) + " And ";
                            break;
                        case "System.DateTime":
                            if (TB.Text.Length > 0)
                            {
                                if (IsDate(TB.Text))
                                    StrFilter = StrFilter + '[' + GrdCol.Name + "] = #" + DateTime.Parse(TB.Text).ToString("MM/dd/yyyy") + "# And ";
                            }
                            break;
                    }
                }
                else
                {
                    CheckBox CB = GrdCol.HeaderCell.Tag as CheckBox;                    
                    if (CB.CheckState != CheckState.Indeterminate)
                        StrFilter = StrFilter + '[' + GrdCol.Name + "] = " + CB.Checked + " And ";
                }
            }
            if (StrFilter.Length > 0)
            {
                StrFilter = StrFilter.Substring(0, StrFilter.LastIndexOf("And "));
            }

            mDTable.DefaultView.RowFilter = StrFilter;            
            LblRecords.Text = "Total :  " + mDTable.DefaultView.ToTable().Rows.Count.ToString();
            TRefresh.Enabled = false;
        }
        
        
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TRefresh.Enabled = true;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            { 
                if(CheckedBoxColumn)
                    dgvSearch.CurrentCell = dgvSearch.Rows[0].Cells[dgvSearch.Columns["Sel"].Index];
                
                dgvSearch.Focus();
            }
            //int intFieldLen = 0;
            //TextBox TB = (TextBox)sender;

            //intFieldLen = Val.ToInt(TB.AccessibleName);
            //if (TB.TextLength + 1 > intFieldLen && intFieldLen != 0 && e.KeyCode != Keys.Back  )
            //{
            //    e.SuppressKeyPress = true;
            //    e.Handled = true;
            //}
        }
        private void OnCheckStateChanged(object sender, EventArgs e)
        {
            TRefresh.Enabled = true;
        }
        private void OnCheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dgvSearch.Rows)
                r.Cells["Sel"].Value = (this.Controls["chkSelAll"] as CheckBox).Checked;
        }

        private void dgvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!CheckedBoxColumn) return;
            if (e.ColumnIndex > 0) return;
            string strChk = _mStrRet;
            int i = 0;
            foreach (DataRow dr in mDTable.Rows)
            {                
                if (CheckedBoxColumn == false && strChk.Length == 0)
                    strChk = dgvSearch.Columns[0].Name;
                else if (CheckedBoxColumn == true && strChk.Length == 0)
                    strChk = dgvSearch.Columns[1].Name;

                if (dr[strChk].ToString() == dgvSearch.Rows[e.RowIndex].Cells[strChk].Value.ToString())
                {
                    mDTable.Rows[i]["Sel"] = dgvSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;                    
                    break;
                }
                i++;
            }            
        }
        #endregion        

        private void dgvSearch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!CheckedBoxColumn) return;
            if (e.KeyCode == Keys.Space)
            {
                if (string.IsNullOrEmpty(dgvSearch.CurrentRow.Cells["Sel"].Value.ToString()) || Convert.ToBoolean(dgvSearch.CurrentRow.Cells["Sel"].Value) == false)
                    dgvSearch.CurrentRow.Cells["Sel"].Value = true;
                else
                    dgvSearch.CurrentRow.Cells["Sel"].Value = false;
            }     
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            if (CheckedBoxColumn == false)
            {
                _mGrdRow = null;
                _retVal = "";
                this.Close();
                return;
            }
            SetReturnProperty();                               
            this.Close();
        }

        private void dgvSearch_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (OnDataGridCellFormatting != null)
                OnDataGridCellFormatting.Invoke(sender, e);
        }

        private void dgvSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CheckedBoxColumn && e.ColumnIndex >0)
            {
                foreach (DataGridViewRow r in dgvSearch.Rows) //reset checkbox
                    r.Cells["Sel"].Value = false;
                dgvSearch.CurrentRow.Cells["Sel"].Value = true;
            }
            SetReturnProperty();   
        }
        public static string EscapeLikeValue(string valueWithoutWildcards)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < valueWithoutWildcards.Length; i++)
            {
                char c = valueWithoutWildcards[i];
                if (c == '*' || c == '%' || c == '[' || c == ']')
                    sb.Append("[").Append(c).Append("]");
                else if (c == '\'')
                    sb.Append("''");
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }

        private void FrmAdvanceSearch_Shown(object sender, EventArgs e)
        {
            if (AutoResizeStyle != DataGridViewAutoSizeColumnsMode.Fill && AutoResizeStyle != DataGridViewAutoSizeColumnsMode.None)
            {
                dgvSearch.AutoResizeColumns(AutoResizeStyle);
            }
            dgvSearch.Columns.GetLastColumn(DataGridViewElementStates.None, DataGridViewElementStates.None).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSearch.Columns[dgvSearch.Columns.Count - 1].HeaderText = "";
            dgvSearch.Columns[dgvSearch.Columns.Count - 1].ReadOnly = true;
            SetControls();
        }
    }
}