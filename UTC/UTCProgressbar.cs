using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace UTC
{
    public partial class UTCProgressbar : Infragistics.Win.UltraWinProgressBar.UltraProgressBar  
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
        public UTCProgressbar()
        {
            this.ForeColor = Color.FromArgb(10, 36, 106);
        }
        public UTCProgressbar(IContainer container)
        {
            container.Add(this);

            //InitializeComponent();
        }
    }
}
