namespace UTC
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using System.Data;

    public class UICheckedListboxEditor : UITypeEditor
	{				
		private bool bIsDropDownResizable = false;
		private CheckedListBox oList = new CheckedListBox();
        string strChkRet = ""; string strChkValRet = "";
		private object oSelectedValue = null;
		private IWindowsFormsEditorService oEditorService;
        UICheckedListboxValueMember valuemember;
        UICheckedListboxDisplayMember displaymember;
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context != null&& context.Instance != null)
			{
				UICheckedListboxIsDropDownResizable attribute =  (UICheckedListboxIsDropDownResizable) context.PropertyDescriptor.Attributes[typeof(UICheckedListboxIsDropDownResizable)];
				if (attribute != null)
				{
					bIsDropDownResizable = true;
				}
				return UITypeEditorEditStyle.DropDown;
			}
			return UITypeEditorEditStyle.None;
		}
		
		public override bool IsDropDownResizable
		{
			get
			{
				return bIsDropDownResizable;
			}
		}
		
		[RefreshProperties(RefreshProperties.All)]public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
            
			if (context == null || provider == null || context.Instance == null)
			{
				return base.EditValue(provider, value);
			}
			
			oEditorService =  (System.Windows.Forms.Design.IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
			if (oEditorService != null)
			{
				
				// Get the Back reference to the Custom Property
				CustomProperty.CustomPropertyDescriptor oDescriptor =  (CustomProperty.CustomPropertyDescriptor) context.PropertyDescriptor;
				CustomProperty cp =  (CustomProperty) oDescriptor.CustomProperty;
				
				// Declare attributes
				UICheckedListboxDatasource datasource;
                //UICheckedListboxValueMember valuemember;
                //UICheckedListboxDisplayMember displaymember;
				
				// Get attributes
				datasource =  (UICheckedListboxDatasource) context.PropertyDescriptor.Attributes[typeof(UICheckedListboxDatasource)];
				valuemember =  (UICheckedListboxValueMember) context.PropertyDescriptor.Attributes[typeof(UICheckedListboxValueMember)];
				displaymember =  (UICheckedListboxDisplayMember) context.PropertyDescriptor.Attributes[typeof(UICheckedListboxDisplayMember)];
				
			    oList.BorderStyle = BorderStyle.None;                
				oList.IntegralHeight = true;
                oList.Sorted = true;
				if (datasource != null)
				{
					oList.DataSource = datasource.Value;
				}
				
				if (displaymember != null)
				{
					oList.DisplayMember = displaymember.Value;
				}
                
				if (valuemember != null)
				{
					oList.ValueMember = valuemember.Value;
				}
				
				if (value != null)
				{
					if (value.GetType().Name == "String")
					{
						oList.Text =  (string) value;
					}
					else
					{
						oList.SelectedItem = value;
					}
				}

                oList.CheckOnClick = true;
                this.oList.Leave += new System.EventHandler(this.OnLeave);
				               
				oEditorService.DropDownControl(oList);
				if (oList.SelectedIndices.Count == 1)                
				{
					cp.SelectedItem = oList.SelectedItem;
                    cp.SelectedValue = oSelectedValue;
					value = oList.Text;
				}
				oEditorService.CloseDropDown();
                //RETURN SELECTED VALUE MEMBER LIST OF CHECKBOX
                cp.SeletedValueMembers = strChkValRet;
			}
			else
			{
				return base.EditValue(provider, value);
			}
			 
			return strChkRet ;
			
		}
        private void OnLeave(object sender, EventArgs e)
        {
            strChkRet = "";
            strChkValRet = "";
            foreach (var item in oList.CheckedItems)
            {
                if (displaymember.Value != null)  //CHECK IF BOUND CHECKLISTBOX
                {
                    var row = (item as DataRowView).Row;
                    if (!string.IsNullOrEmpty(valuemember.Value))  //IF VALUE MEMBER SPECIFIED THEN RETURN VALUES  LIST ALSO
                    {
                        if (strChkValRet.Length == 0)
                            strChkValRet = row[valuemember.Value].ToString();
                        else
                            strChkValRet = strChkValRet + "," + row[valuemember.Value].ToString();
                    }
                 
                    if (strChkRet.Length == 0)
                        strChkRet = row[displaymember.Value].ToString();
                    else
                        strChkRet = strChkRet + "," + row[displaymember.Value].ToString();                 
                }
                else
                {
                    if (strChkRet.Length == 0)  //NO BOUND CHECKED LIST , ONLY LIST OF STRING IS BOUND 
                        strChkRet = item.ToString();
                    else
                        strChkRet = strChkRet + "," + item.ToString();
                }
            }
        }
		private void SelectedItem(object sender, EventArgs e)
		{
			if (oEditorService != null)
			{
				if (oList.SelectedValue != null)
				{
					oSelectedValue = oList.SelectedValue;
				}
				oEditorService.CloseDropDown();
			}
		}

        [AttributeUsage(AttributeTargets.Property)]
		public class UICheckedListboxDatasource : Attribute
		{
			
			private object oDataSource;
			public UICheckedListboxDatasource(ref object Datasource)
			{
				oDataSource = Datasource;
			}
			public object Value
			{
				get
				{
					return oDataSource;
				}
			}
		}

        [AttributeUsage(AttributeTargets.Property)]
		public class UICheckedListboxValueMember : Attribute
		{
			
			private string sValueMember;
			public UICheckedListboxValueMember(string ValueMember)
			{
				sValueMember = ValueMember;
			}
			public string Value
			{
				get
				{
					return sValueMember;
				}
				set
				{
					sValueMember = value;
				}
			}
		}

        [AttributeUsage(AttributeTargets.Property)]
		public class UICheckedListboxDisplayMember : Attribute
		{
			
			private string sDisplayMember;
			public UICheckedListboxDisplayMember(string DisplayMember)
			{
				sDisplayMember = DisplayMember;
			}
			public string Value
			{
				get
				{
					return sDisplayMember;
				}
				set
				{
					sDisplayMember = value;
				}
			}
			
		}

        [AttributeUsage(AttributeTargets.Property)]
		public class UICheckedListboxIsDropDownResizable : Attribute
		{
			
		}	
	}	
}
