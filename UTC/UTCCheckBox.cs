using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace UTC
{
    public partial class UTCCheckBox : Infragistics.Win.UltraWinEditors.UltraCheckEditor
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
                System.Windows.Forms.ToolTip TT1 = new ToolTip();
                TT1.SetToolTip(this, _ToolTips);
            }
        }
        public UTCCheckBox()
        {
            InitializeComponent();
        }
        public UTCCheckBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }
}
