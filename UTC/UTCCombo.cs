using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace UTC
{
    public partial class UTCCombo : Infragistics.Win.UltraWinGrid.UltraCombo
    {
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

        private object[] mObj;
        private string[] mStrFields;
        string[] _StrColumns;

        public UTCCombo()
        {
            InitializeComponent();
            this.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
        }
        public UTCCombo(IContainer container)
        {
            //	container.Add(this);
            InitializeComponent();
            this.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
        }

        #region Fields/Properties

        private Boolean _mBlnByPassValidation;

        [Browsable(true)]
        public Boolean ByPassValidation
        {
            get { return _mBlnByPassValidation; }
            set { _mBlnByPassValidation = value; }
        }

        private Boolean _mBlnDropdownOnFocus;

        [Browsable(true)]
        public Boolean DropdownOnFocus
        {
            get { return _mBlnDropdownOnFocus; }
            set { _mBlnDropdownOnFocus = value; }
        }

        public override string Text
        {
            get
            {
                if (base.IsDisposed == true)
                    return "";
                else
                    return base.Text;
            }
            set
            {
                if (base.IsDisposed == true)
                    return;
                else
                    base.Text = value;
            }
        }

        public string FieldsToDisplay
        {
            get
            {
                if (base.IsDisposed == true)
                    return "0";
                else
                {
                    return base.Text;
                }
            }
            set
            {
                base.Text = Convert.ToString(value);
            }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
        private Boolean _ActivationColor;
        [Browsable(true)]
        public Boolean ActivationColor
        {
            get { return _ActivationColor; }
            set { _ActivationColor = value; }
        }
        private bool _ChkVal = false;
        [Browsable(true)]
        public bool ChkVal
        {
            get { return _ChkVal; }
            set { _ChkVal = value; }
        }

        private bool _ChkOnLeave = false;
        [Browsable(true)]
        public bool ChkOnLeave
        {
            get { return _ChkOnLeave; }
            set { _ChkOnLeave = value; }
        }
        private Infragistics.Win.UltraWinGrid.UltraGridRow _m_dropdownRow;
        public Infragistics.Win.UltraWinGrid.UltraGridRow MySelectedRow
        {
            get { return _m_dropdownRow; }
            set { _m_dropdownRow = value; }
        }

        #endregion

        #region Overridden Methods
        protected override void OnEnter(EventArgs e)
        {
            if (FindForm() == null) return;
            if (this.FindForm().Disposing == true) return;
            if (Disposing == true) return;
            try
            {
                base.OnEnter(e);
                if (this._mBlnDropdownOnFocus == true)
                {
                    this.PerformAction(UltraComboAction.Dropdown);
                    //this.ToggleDropdown();
                    //SendKeys.Send("{F4}");
                }

            }
            catch (Exception)
            {

            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (this.FindForm().Disposing == true) return;
            if (Disposing == true) return;
            if (_ActivationColor)
            {
                Appearance.BackColor = Color.GreenYellow;
            }
            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            if (this.FindForm().Disposing == true) return;
            if (Disposing == true) return;
            base.OnLostFocus(e);
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            if (this.FindForm().Disposing == true) return;
            if (_ChkOnLeave == true) return;
            if (Disposing == true) return;
            if (_ActivationColor) Appearance.BackColor = Color.White;
            if (_mBlnByPassValidation == true)
            {
                ChkValidation(true);
                base.OnValidating(e);
                return;
            }
            if (ActiveRow != null)
            {
                if (this.Text.ToUpper() == ActiveRow.Cells[0].Text.ToUpper())
                {
                    if (mStrFields != null)
                    {
                        ChkValidation(true);
                    }
                }
            }
            else
            {
                if (_ChkVal == false)
                    this.Text = "";
                ChkValidation(false);
            }
            base.OnValidating(e);
        }
        protected override void OnValidated(EventArgs e)
        {
            if (this.FindForm().Disposing == true) return;
            if (_ChkOnLeave == true) return;
            if (Disposing == true) return;
            base.OnValidated(e);
        }
        protected override void OnValueChanged(EventArgs e)
        {
            if (this.FindForm() == null) return;
            if (this.FindForm().Disposing == true) return;
            if (Disposing == true) return;
            ChkValidation(false);
            //CHANGED FOR 2013 GRID 05/05/2014

            if (ActiveRow == null) return;
            MySelectedRow = ActiveRow;
            if (ActiveRow != null)
            {
                ChkValidation(true);
            }
            else
            {
                if (_mBlnByPassValidation == true)
                {
                    ChkValidation(true);
                }
                else
                {
                    this.Text = "";
                    //ChkValidation(false);
                }
            }
            base.OnValueChanged(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            if (this.FindForm() == null) return;
            if (this.FindForm().Disposing == true) return;
            if (Disposing == true) return;
            if (_ChkOnLeave == true)
            {
                Text = Text.Trim();
            }
            if (_mBlnByPassValidation == true)
            {
                ChkValidation(true);
                base.OnLeave(e);
                return;
            }
            if (ActiveRow != null)
            {
                if (this.Text.ToUpper() == ActiveRow.Cells[0].Text.ToUpper())
                {
                    ChkValidation(true);
                }
            }
            else
            {
                if (_ChkVal == false)
                    this.Text = "";
                ChkValidation(false);
            }
            if (this.Parent.Disposing == false)
            {
                base.OnLeave(e);
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
                }
            }
            base.OnKeyPress(e);
        }
        #endregion

        #region User Define Method
        public void SetObject(params Object[] pObj)
        {
            int intCounter;
            intCounter = 0;

            mObj = new object[pObj.Length / 2];
            mStrFields = new String[pObj.Length / 2];

            foreach (Object Obj in pObj)
            {
                if (Obj is TextBox || Obj is Label || Obj is UTCCombo)
                {
                    mObj[intCounter] = Obj;
                    intCounter = intCounter + 1;
                }
            }

            intCounter = 0;
            foreach (Object Obj in pObj)
            {
                if (!(Obj is TextBox || Obj is Label || Obj is UTCCombo))
                {
                    mStrFields[intCounter] = Obj.ToString();
                    intCounter = intCounter + 1;
                }
            }
        }
        public void SetColAlign(string pStrColKey, Infragistics.Win.HAlign pHAlign)
        {
            this.DisplayLayout.Bands[0].Columns[pStrColKey].CellAppearance.TextHAlign = pHAlign;
        }
        public void SetColumn(string pStrBandName, string pStrColumns, string pStrColWidths)
        {
            Infragistics.Win.UltraWinGrid.UltraGridBand UltGrdBand = new Infragistics.Win.UltraWinGrid.UltraGridBand(pStrBandName, -1);
            string[] StrColumns, StrColWidths;
            this.DisplayLayout.Bands[0].Override.Reset();
            StrColumns = pStrColumns.Split(',');
            _StrColumns = pStrColumns.Split(',');
            StrColWidths = pStrColWidths.Split(',');

            Infragistics.Win.UltraWinGrid.UltraGridColumn[] UltGrdCols = new Infragistics.Win.UltraWinGrid.UltraGridColumn[StrColumns.GetUpperBound(0) + 1];
            Infragistics.Win.Appearance[] Appearances = new Infragistics.Win.Appearance[StrColumns.GetUpperBound(0) + 1];
            for (int IntI = 0; IntI <= StrColumns.GetUpperBound(0); IntI++)
            {
                UltGrdCols[IntI] = new Infragistics.Win.UltraWinGrid.UltraGridColumn(StrColumns[IntI]);
                UltGrdCols[IntI].Width = int.Parse(StrColWidths[IntI]);
                Appearances[IntI] = new Infragistics.Win.Appearance();
                if (IntI == 0)
                {
                    this.DisplayMember = StrColumns[IntI];
                    // Appearances[IntI].BackColor = Color.Linen;
                    //Appearances[IntI].ForeColor = Color.DarkBlue;
                    Appearances[IntI].FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                }
                else
                {
                    //Appearances[IntI].ForeColor = Color.Indigo;
                    Appearances[IntI].FontData.Bold = DefaultableBoolean.False;
                }
                UltGrdCols[IntI].CellAppearance = Appearances[IntI];
                UltGrdCols[IntI].CellAppearance.Tag = IntI;
            }

            UltGrdBand.ColHeadersVisible = false;
            for (int IntI = 0; IntI <= StrColumns.GetUpperBound(0); IntI++)
            {
                UltGrdBand.Columns.Add(UltGrdCols[IntI]);
            }
            this.Appearance.ForeColor = System.Drawing.Color.FromArgb(System.Convert.ToInt32((byte)(0)), System.Convert.ToInt32((byte)(0)), System.Convert.ToInt32((byte)(64)));
            this.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.Text = "";

            UltGrdBand.Indentation = 0;
            this.DisplayLayout.BandsSerializer.Add(UltGrdBand);
            this.DisplayLayout.UseFixedHeaders = true;
        }
        public void SetColumn(string pStrBandName, string pStrColumns, string pStrColWidths, bool withOldColor)
        {
            Infragistics.Win.UltraWinGrid.UltraGridBand UltGrdBand = new Infragistics.Win.UltraWinGrid.UltraGridBand(pStrBandName, -1);
            string[] StrColumns, StrColWidths;
            this.DisplayLayout.Bands[0].Override.Reset();
            StrColumns = pStrColumns.Split(',');
            _StrColumns = pStrColumns.Split(',');
            StrColWidths = pStrColWidths.Split(',');

            Infragistics.Win.UltraWinGrid.UltraGridColumn[] UltGrdCols = new Infragistics.Win.UltraWinGrid.UltraGridColumn[StrColumns.GetUpperBound(0) + 1];
            Infragistics.Win.Appearance[] Appearances = new Infragistics.Win.Appearance[StrColumns.GetUpperBound(0) + 1];
            for (int IntI = 0; IntI <= StrColumns.GetUpperBound(0); IntI++)
            {
                UltGrdCols[IntI] = new Infragistics.Win.UltraWinGrid.UltraGridColumn(StrColumns[IntI]);
                UltGrdCols[IntI].Width = int.Parse(StrColWidths[IntI]);
                Appearances[IntI] = new Infragistics.Win.Appearance();
                if (IntI == 0)
                {
                    this.DisplayMember = StrColumns[IntI];
                    if (withOldColor)
                    {
                        Appearances[IntI].BackColor = Color.Linen;
                        Appearances[IntI].ForeColor = Color.DarkBlue;
                    }
                    Appearances[IntI].FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                }
                else
                {
                    if (withOldColor)
                        Appearances[IntI].ForeColor = Color.Indigo;
                    Appearances[IntI].FontData.Bold = DefaultableBoolean.False;
                }
                UltGrdCols[IntI].CellAppearance = Appearances[IntI];
                UltGrdCols[IntI].CellAppearance.Tag = IntI;
            }

            UltGrdBand.ColHeadersVisible = false;
            for (int IntI = 0; IntI <= StrColumns.GetUpperBound(0); IntI++)
            {
                UltGrdBand.Columns.Add(UltGrdCols[IntI]);
            }
            this.Appearance.ForeColor = System.Drawing.Color.FromArgb(System.Convert.ToInt32((byte)(0)), System.Convert.ToInt32((byte)(0)), System.Convert.ToInt32((byte)(64)));
            this.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.Text = "";

            UltGrdBand.Indentation = 0;
            this.DisplayLayout.BandsSerializer.Add(UltGrdBand);
            this.DisplayLayout.UseFixedHeaders = true;
        }
        private void ChkValidation(Boolean pBlnAvailValue)
        {
            if (_ChkVal == true) return;
            if (mStrFields == null) return;
            if (this.FindForm().Disposing == true) return;
            if (pBlnAvailValue == true)
            {
                for (int IntI = 0; IntI <= mStrFields.Length - 1; IntI++)
                {
                    if (mObj[IntI] is TextBox)
                    {
                        if (this.ActiveRow != null)
                        {
                            ((TextBox)mObj[IntI]).Text = this.ActiveRow.Cells[mStrFields[IntI]].Text;
                        }
                        else
                        {
                            ((TextBox)mObj[IntI]).Text = "";
                        }
                    }
                    else if (mObj[IntI] is Label)
                    {
                        if (this.ActiveRow != null)
                        {
                            ((Label)mObj[IntI]).Text = this.ActiveRow.Cells[mStrFields[IntI]].Text;
                        }
                        else
                        {
                            ((Label)mObj[IntI]).Text = "";
                        }
                    }
                    else if (mObj[IntI] is UTCCombo)
                    {
                        if (this.ActiveRow != null)
                        {
                            ((UTCCombo)mObj[IntI]).Text = this.ActiveRow.Cells[mStrFields[IntI]].Text;
                        }
                        else
                        {
                            ((UTCCombo)mObj[IntI]).Text = "";
                        }
                    }

                }
            }
            else
            {
                for (int IntI = 0; IntI <= mStrFields.Length - 1; IntI++)
                {
                    if (mObj[IntI] is TextBox)
                    {
                        ((TextBox)mObj[IntI]).Text = "";
                    }
                    else if (mObj[IntI] is Label)
                    {
                        ((Label)mObj[IntI]).Text = "";
                    }
                    else if (mObj[IntI] is UTCCombo)
                    {
                        ((UTCCombo)mObj[IntI]).Text = "";
                    }
                }
            }
        }
        public void UpdCmb(object pObj)
        {
            DataTable DTB;
            if (DataSource == null) return;
            if ((((DataSet)DataSource).Tables[DataMember]) == null) return;
            DTB = ((DataSet)DataSource).Tables[DataMember];
            String StrField;
            for (int IntI = 0; IntI <= mStrFields.Length - 1; IntI++)
            {
                if (mObj[IntI] == pObj)
                {
                    StrField = mStrFields[IntI];
                    if (pObj is TextBox)
                    {
                        DTB.DefaultView.RowFilter = mStrFields[IntI] + "='" + ((TextBox)pObj).Text + "'";
                    }
                    else if (pObj is Label)
                    {
                        DTB.DefaultView.RowFilter = mStrFields[IntI] + "='" + ((Label)pObj).Text + "'";
                    }
                    else if (pObj is UTCCombo)
                    {
                        DTB.DefaultView.RowFilter = mStrFields[IntI] + "='" + ((UTCCombo)pObj).Text + "'";
                    }
                    if (DTB.DefaultView.Count != 0)
                    {
                        this.Text = DTB.DefaultView[0][0].ToString();
                    }
                    DTB.DefaultView.RowFilter = "";
                }
            }
        }
        public void _SetDataBinding(DataSet dataSource, String dataMember, Boolean hideNewColumns)
        {
            System.Data.DataSet DS = dataSource;
            System.Data.DataTable DT = DS.Tables[dataMember];
            int intCounter = 0;
            foreach (String Str in _StrColumns)
            {
                DT.Columns[Str].SetOrdinal(intCounter);
                intCounter++;
            }

            DT.AcceptChanges();
            DS.Tables[dataMember].AcceptChanges();
            this.SetDataBinding(dataSource, dataMember, hideNewColumns);
            if (hideNewColumns == true)
            {
                HideOtherColumn();
            }

        }
        //create by umesh on26/02/2016
        public void SetDataBindingPT(DataSet dataSource, String dataMember, Boolean hideNewColumns)
        {
            if (hideNewColumns)
                this.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.SetDataBinding(dataSource, dataMember, hideNewColumns);

        }

        public void HideOtherColumn()
        {
            foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn Col in this.DisplayLayout.Bands[0].Columns)
            {
                bool BlnHide = true;

                foreach (String Str in _StrColumns)
                {
                    if (Str.ToUpper() == Col.ToString().ToUpper())
                    {
                        BlnHide = false;
                    }
                }
                if (BlnHide == true)
                {
                    Col.Hidden = true;
                }
            }
        }

        public void ReSetBackColor()
        {

            Appearance.BackColor = Color.White;

        }

        public System.Drawing.Color SetBackColor
        {
            set
            {
                Appearance.BackColor = value;
            }
        }
        //Implement IDisposable.
        //public void Dispose()
        //{           
        //    mObj=null;
        //    mStrFields=null;
        //    _StrColumns=null;
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        #endregion
    }
}