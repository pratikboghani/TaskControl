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
using TaskControl;
using Events = BusLib.Utility;
using OBJ = BusLib.Utility;
using Val = BusLib.Validation.BOValidation;
using Glb = DataLib.GlobalSql;


namespace TaskControl
{
    public partial class ViewPara : Form  
    {
        private BOViewParaMast mboViewPara = new BOViewParaMast();
        
        public ViewPara()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            //Val.frmGenSet(this);

            FillGrd();
            txtPass_TextChanged(null, null);

            this.Width = Global.setFormWidth(UltGrdViewPara);

            this.Show();
        }

        private void FillGrd()
        {
            //mboViewPara.Fill("");
            //UltGrdViewPara.SetDataBinding(mboViewPara.DS, mboViewPara.TableName, true);
            //UltGrdViewPara.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
            //Global.SetGridColumns(UltGrdViewPara, mboViewPara.GetViewColumns("VIEWPARA"));
            //this.UltGrdViewPara.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            //this.UltGrdViewPara.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Edit;
           
        }
        

        private void UltGrdViewPara_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Override.HeaderClickAction = HeaderClickAction.SortSingle;            
        }

        private void UltGrdViewPara_AfterRowUpdate(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e)
        {           
          //  mboViewPara.Update();           
        }


        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (txtPass.Text=="1")
            {
                UltGrdViewPara.SetOperation(true,true,true, UTC.UTCGrid.EnumCellActivation.AllowEdit);
            }
            else
            {
                UltGrdViewPara.SetOperation(false, false, false, UTC.UTCGrid.EnumCellActivation.NoEdit);
            }

        }

        private void UltGrdViewPara_AfterRowsDeleted(object sender, EventArgs e)
        {            
            //mboViewPara.Delete();
        }

        private void ViewPara_Load(object sender, EventArgs e)
        {
            ShowForm();
        }
    }
}
