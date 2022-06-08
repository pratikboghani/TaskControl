namespace UTC
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using System.Data;

    public class UIDialogEditor : UITypeEditor
	{        			        	
        FrmAdvanceSearch f ;				
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
            if (context != null && context.Instance != null)
            {
                if (!context.PropertyDescriptor.IsReadOnly)
                {
                    return UITypeEditorEditStyle.Modal;
                } 
            }
            return UITypeEditorEditStyle.None;
		}				
		
		[RefreshProperties(RefreshProperties.All)]public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
            
			if (context == null || provider == null || context.Instance == null)
			{
				return base.EditValue(provider, value);
			}
    
            if (context == null || provider == null || context.Instance == null)
            {
                return base.EditValue(provider, value);
            }
           
            CustomPropertyCollection cp = context.Instance as CustomPropertyCollection;
            CustomProperty p = cp[context.PropertyDescriptor.Name];
            if (p != null)
            {                                                                
                f = new FrmAdvanceSearch(p.Datasource as DataTable, p.ValueMember,p.DisplayMember , p.IsMultiSelect ,p.ColumnHeaders ,p.ColumnCaps);
                if (p.Value.ToString().Length > 0)
                    f.SetSelectedList(p.Value.ToString());
                f.ShowDialog();
            }
            if (p.IsMultiSelect)
            {
                if (p.ValueMember.Length > 0)
                {
                    p.SelectedValue = f.List;
                    value = f.DispList;
                }
                else
                    value = f.List;
            }
            else
            {
                if (p.ValueMember.Length > 0)
                {
                    p.SelectedValue = f.SelectedValue;
                    value = f.SelectedDispValue;
                }
                else
                    value = f.SelectedValue;
            }
            p.SingleQuoted = f.SingleQuateList;
            p.DoubleQuoted = f.DoubleQuateList;
            return value;			
		}

        [AttributeUsage(AttributeTargets.Property)]
		public class UIDialogEditorDatasource : Attribute
		{
			
			private object oDataSource;
			public UIDialogEditorDatasource(ref object Datasource)
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
	}	
}
