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
    public partial class UTCOptionSet : Infragistics.Win.UltraWinEditors.UltraOptionSet        
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
        public UTCOptionSet()
        {
            InitializeComponent();
            //this.CreationFilter = objFilter;
        }
        public UTCOptionSet(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            this.ValueChanged += new System.EventHandler(this.cOptionSet_OnValueChanged);
            
            MyUltraOptionSetCreationFilter objFilter = new MyUltraOptionSetCreationFilter();
            this.CreationFilter = objFilter;
            
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }
        private string _StrPrcCode;
        public string StrPrcCode
        {
            get
            {
                if (CheckedIndex >= 0)
                {
                    if (this.ValueMember !=null && this.ValueMember.ToString().Length > 0)
                    {
                        return string.IsNullOrEmpty(Items[CheckedIndex].DataValue.ToString()) ? "" : Items[CheckedIndex].DataValue.ToString();
                    }
                    else {
                        return string.IsNullOrEmpty(Items[CheckedIndex].Tag.ToString()) ? "" : Items[CheckedIndex].Tag.ToString();
                    }
                }
                else
                {
                    return "";
                }
            }
            set
            {
                for (int IntI = 0; IntI < Items.Count ; IntI++)
                {
                    if (Items[IntI].Tag != null) return;
                    if (value.ToUpper() == Items[IntI].Tag.ToString())
                    {
                        CheckedItem=Items[IntI];
                        this.FocusedIndex = IntI;
                    }
                }
            }
        }
        
        #region Field/Property

       
        /// <summary>
        /// <catarag>Personal Setting</catarag>
        /// <remarks>Set For Selected Color </remarks>
        /// </summary>
        private Color _SelectColor = Color.Red;
        [Browsable(true)]
        public Color SelectColor
        {
            get { return _SelectColor; }
            set
            {
                _SelectColor = value;
                SetColor();
            }
        }
        private Color _DeSelectColor = Color.Navy;
        [Browsable(true)]
        public Color DeSelectColor
        {
            get { return _DeSelectColor; }
            set
            {
                _DeSelectColor = value;
                SetColor();
            }
        }
      
        #endregion

        #region Override Method
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (CheckedIndex == 0)
                {
                    this.CheckedIndex = Items.Count - 1;
                }
                else
                {
                    this.CheckedIndex--;
                }
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (CheckedIndex == Items.Count - 1)
                {
                    CheckedIndex = 0;
                }
                else
                {
                    CheckedIndex++;
                }
            }
            SetColor();
            base.OnKeyDown(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            if (this.FindForm() == null) return;
            if (this.FindForm().Disposing == true) return;
            if (Disposing == true) return;

            base.OnLeave(e);
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

        #region Method
        private void cOptionSet_OnValueChanged(object sender, EventArgs e)
        {
            SetColor();
        }
        private void cOptionSet_ControlAdded(object sender, ControlEventArgs e)
        {
            _SelectColor = Items[0].Appearance.ForeColor;
            if (Items.Count > 1)
            {
                _DeSelectColor = Items[1].Appearance.ForeColor;
            }
            else
            {
                _DeSelectColor = Color.Gray;
            }
        }
        private void cOptionSet_ControlRemoved(object sender, ControlEventArgs e)
        {
            _SelectColor = Items[0].Appearance.ForeColor;
            if (Items.Count > 1)
            {
                _DeSelectColor = Items[1].Appearance.ForeColor;
            }
            else
            {
                _DeSelectColor = Color.Gray;
            }
        }
        private void SetColor()
        {
            if (this.FindForm() == null) return;
            if (Conversion.Val(CheckedIndex) != -1)
            {
                if (Items.Count >= 2)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        try
                        {
                            Items[i].Appearance.ForeColor = _DeSelectColor;
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }
                Items[CheckedIndex].Appearance.ForeColor = _SelectColor;
            }
        }
        #endregion
    }

    public class MyDrawFilter : Infragistics.Win.IUIElementDrawFilter
    {
        bool Infragistics.Win.IUIElementDrawFilter.DrawElement(Infragistics.Win.DrawPhase drawPhase, ref Infragistics.Win.UIElementDrawParams drawParams)
        {
            // Return true here to cancel the default drawing. 
            return true;
        }

        Infragistics.Win.DrawPhase Infragistics.Win.IUIElementDrawFilter.GetPhasesToFilter(ref Infragistics.Win.UIElementDrawParams drawParams)
        {
            Infragistics.Win.UltraWinGrid.UltraGridUIElement ultraGridUIElement = drawParams.Element as Infragistics.Win.UltraWinGrid.UltraGridUIElement;
            if (null != ultraGridUIElement)
            {
                if (ultraGridUIElement.Layout.IsPrintLayout)
                {
                    return Infragistics.Win.DrawPhase.BeforeDrawBackColor;
                    
                }
            }

            return Infragistics.Win.DrawPhase.None;
        }
    }
    public class MyUltraOptionSetCreationFilter : Infragistics.Win.IUIElementCreationFilter
    {
        #region IUIElementCreationFilter Members
        
        void Infragistics.Win.IUIElementCreationFilter.AfterCreateChildElements(Infragistics.Win.UIElement parent)
        {
            if (parent is Infragistics.Win.OptionSetEmbeddableUIElement)
            {
                int? left = null;
                int? right = null;

                foreach (Infragistics.Win.UIElement element in parent.ChildElements)
                {
                    if (element is Infragistics.Win.OptionSetOptionButtonUIElement)
                    {
                        Rectangle rect = element.Rect;
                        if (left == null)
                            left = rect.Left;
                        else
                            left = Math.Min(left.Value, rect.Left);

                        if (right == null)
                            right = rect.Right;
                        else
                            right = Math.Max(right.Value, rect.Right);
                    }
                }
                
                if (left.HasValue && right.HasValue)
                {
                    int requiredWidth = right.Value - left.Value;
                    int availableWidth = parent.RectInsideBorders.Width;

                    int newLeft = (availableWidth - requiredWidth) / 2;
                    int offset = newLeft - left.Value;

                    if (offset != 0)
                    {
                        foreach (Infragistics.Win.UIElement element in parent.ChildElements)
                        {
                            element.Rect = new Rectangle(
                                element.Rect.X + offset,
                                element.Rect.Y,
                                element.Rect.Width,
                                element.Rect.Height);
                        }
                    }
                }
            }

        }

        bool Infragistics.Win.IUIElementCreationFilter.BeforeCreateChildElements(Infragistics.Win.UIElement parent)
        {            
            return false;
        }

        #endregion
    }
}
