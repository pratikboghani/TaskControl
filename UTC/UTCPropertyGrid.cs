namespace UTC
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Drawing;
	using System.Reflection;
	using System.Runtime.CompilerServices;
	using System.Runtime.InteropServices;
	using System.Windows.Forms;
	using System.Collections.Generic;
	using System.Data;
	using System.Text;

	public partial class UTCPropertyGrid : PropertyGrid
	{			
		#region "Protected variables and objects"
		// CustomPropertyCollection assigned to MyBase.SelectedObject
		protected CustomPropertyCollection oCustomPropertyCollection;
		protected bool bShowCustomProperties;
	  
		// CustomPropertyCollectionSet assigned to MyBase.SelectedObjects
		protected CustomPropertyCollectionSet oCustomPropertyCollectionSet;
		protected bool bShowCustomPropertiesSet;

		// Internal PropertyGrid Controls
		protected object oPropertyGridView;
		protected object oHotCommands;
		protected object oDocComment;
		protected ToolStrip oToolStrip;
		
		// Internal PropertyGrid Fields
		protected Label oDocCommentTitle;
		protected Label oDocCommentDescription;
		protected FieldInfo oPropertyGridEntries;

		// Properties variables
		protected bool bAutoSizeProperties;
		protected bool bDrawFlatToolbar;
		DataTable dtSpParams = null;

		[Description("Occurs When Last Property Of Propertygrid Leaves The Focus")]
		public event EventHandler PropertyOver;

		#endregion
		
		#region "Public Functions"
		public UTCPropertyGrid()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		   
			// Add any initialization after the InitializeComponent() call.
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);

			// Initialize collections
			oCustomPropertyCollection = new CustomPropertyCollection();
			oCustomPropertyCollectionSet = new CustomPropertyCollectionSet();
			
			// Attach internal controls
			oPropertyGridView = base.GetType().BaseType.InvokeMember("gridView", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, this, null);
			oHotCommands = base.GetType().BaseType.InvokeMember("hotcommands", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, this, null);
			oToolStrip = (ToolStrip) base.GetType().BaseType.InvokeMember("toolStrip", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, this, null);
			oDocComment = base.GetType().BaseType.InvokeMember("doccomment", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, this, null);
			
			// Attach DocComment internal fields
			if (oDocComment != null)
			{
				oDocCommentTitle = (Label)oDocComment.GetType().InvokeMember("m_labelTitle", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, oDocComment, null);
				oDocCommentDescription = (Label)oDocComment.GetType().InvokeMember("m_labelDesc", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, oDocComment, null);
			}
			
			// Attach PropertyGridView internal fields
			if (oPropertyGridView != null)
			{
				oPropertyGridEntries = oPropertyGridView.GetType().GetField("allGridEntries", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			}
			
			// Apply Toolstrip style
			if (oToolStrip != null)
			{
				ApplyToolStripRenderMode(bDrawFlatToolbar);
			}			
		}

		private string _para = "";
		[Browsable(false)] 
		public string ParamList 
		{
			get { return _para; }
			set { _para = value; }
		}
		
		public void MoveSplitterTo(int x)
		{
			oPropertyGridView.GetType().InvokeMember("MoveSplitterTo", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, oPropertyGridView, new object[] { x });
		}
		
		public override void Refresh()
		{
			if (bShowCustomPropertiesSet)
			{
				base.SelectedObjects =  (object[]) oCustomPropertyCollectionSet.ToArray();
			}
			base.Refresh();
			if (bAutoSizeProperties)
			{
				AutoSizeSplitter(32);
			}
		}
		
		public void SetComment(string title, string description)
		{
			MethodInfo method = oDocComment.GetType().GetMethod("SetComment");
			method.Invoke(oDocComment, new object[] { title, description });
			//oDocComment.SetComment(title, description);
		}

		/// <summary>
		/// set focus on grid item based on grid lable name passed on to it.
		/// </summary>
		/// <param name="LableName"></param>
		public void SetFocusToGridItem(string LableName)
		{
			this.SelectedGridItem = FindPropertyGridItem(LableName);
		}
		public bool IsPropertyExists(string PropertyName)
		{
			return this.Items[PropertyName] != null;             
		}
		public GridItem FindPropertyGridItem(string item_label)
		{
			//' Find the GridItem root.
			GridItem root = this.SelectedGridItem;

			while (root.Parent != null)
			{
				root = root.Parent;
			}

			//' Search the tree.
			List<GridItem> nodes = new List<GridItem>();
			nodes.Add(root);
			GridItem test_node = null;
			while (nodes.Count != 0)
			{
				test_node = nodes[0];
				nodes.RemoveAt(0);
				if (test_node.Label == item_label)
					return test_node;

				foreach (GridItem obj in test_node.GridItems)
					nodes.Add(obj);

			}

			return test_node;
		}
		/// <summary>
		/// Returns the Table Structure of Datasource
		/// </summary>
		/// <returns></returns>
		public string GetDataSourceTableStructure()
		{
			string script = @"SET ANSI_NULLS ON
							GO
							SET QUOTED_IDENTIFIER ON
							GO
							SET ANSI_PADDING ON
							GO
							CREATE TABLE [CLV].[REPPARA](
								[NAME] [varchar](50) NOT NULL,
								[DISPLAYNAME] [varchar](50) NULL,
								[CATE_NAME] [varchar](50) NULL,
								[DATATYPE] [varchar](50) NULL,
								[DESCRIPTION] [varchar](100) NULL,
								[LOOKUP_STRING] [varchar](1000) NULL,
								[DATATABLE] [varchar](50) NULL,
								[DISPLAYMEMBER] [varchar](50) NULL,
								[VALUEMEMBER] [varchar](50) NULL,
								[RESIZABLE_DROPDOWN] [bit] NULL,
							 CONSTRAINT [PK_REPPARA] PRIMARY KEY CLUSTERED 
							(
								[NAME] ASC
							)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
							) ON [PRIMARY]

							GO

							SET ANSI_PADDING OFF
							GO

							ALTER TABLE [CLV].[REPPARA] ADD  CONSTRAINT [DF_REPPARA_P_NAME]  DEFAULT ('') FOR [NAME]
							GO

							ALTER TABLE [CLV].[REPPARA] ADD  CONSTRAINT [DF_REPPARA_RESIZABLE_CONTROL]  DEFAULT ((0)) FOR [RESIZABLE_DROPDOWN]
							GO";
			return script;
		}
		/// <summary>
		/// Set Property from Datatable of Property Field Created as per Predefined structure
		/// </summary>
		/// <param name="PropertyTable">Represents Property Datatable </param>
		/// <param name="LookUpTableSet">Represents Property Look up table Set i.e to bind datatable to property with table name specified </param>
		public void SetDataSource(DataTable PropertyTable, DataSet LookUpTableSet)
		{
			object propDataType = 0;
			string DispName = "";
			string lookUp = "";
			bool IsPassword = false;
			bool IsColunmExist = false;
			this.ShowCustomProperties = true;
			this.PropertySort = PropertySort.Categorized;
			this.Items.Clear();
			dtSpParams = PropertyTable;

			IsColunmExist = PropertyTable.Columns.Contains("ISMULTI");

			try
			{
				foreach (DataRow dr in PropertyTable.Rows)
				{
					IsPassword = false;
					propDataType = "";

					if (dr["DATATYPE"].ToString().ToUpper() == "INT")
						propDataType = 0;
					else if (dr["DATATYPE"].ToString().ToUpper() == "DECIMAL")
						propDataType = 0.0m;
					else if (dr["DATATYPE"].ToString().ToUpper() == "DOUBLE")
						propDataType = 0.0d;
					else if (dr["DATATYPE"].ToString().ToUpper() == "BOOL")
						propDataType = false;
					else if (dr["DATATYPE"].ToString().ToUpper() == "DATE")
						propDataType = DateTime.MinValue;
					else if (dr["DATATYPE"].ToString().ToUpper() == "TIME")
						propDataType = DateTime.Today.TimeOfDay;
					else if (dr["DATATYPE"].ToString().ToUpper() == "PASSWORD")                    
						IsPassword = true;                    

					if (dr["DEFVAL"].ToString().Length == 0) //if DEFAULT VALUE NOT SET THEN COMPLSORY
						DispName = dr["DISPLAYNAME"].ToString() + " *";
					else
						DispName = dr["DISPLAYNAME"].ToString();
					
					lookUp = dr["LOOKUP_STRING"].ToString();
					this.Items.Add(dr["NAME"].ToString(), DispName, propDataType, false, dr["CATE_NAME"].ToString(), dr["DESCRIPTION"].ToString(), true);
					
					if(IsPassword)                    
						this.Items[dr["NAME"].ToString()].IsPassword = IsPassword;                    
					else if (lookUp.Length > 0)
						this.Items[dr["NAME"].ToString()].Datasource = lookUp.Split(',');
					else if (dr["DATATABLE"].ToString().Length > 0)
					{
						if (dr["DATATYPE"].ToString() == "STRING" && dr["DATATABLE"].ToString().Length > 0 && (Convert.ToInt64(dr["STRLEN"].ToString()) > 50 || Convert.ToInt16(dr["STRLEN"].ToString()) == -1))
						{
							//MessageBox.Show(dr["NAME"].ToString()); 
							if (IsColunmExist)                            
								this.Items.AddDialogForm(dr["NAME"].ToString(), LookUpTableSet.Tables[dr["DATATABLE"].ToString()], dr["DISPLAYMEMBER"].ToString(), dr["VALUEMEMBER"].ToString(),Convert.ToBoolean(dr["ISMULTI"].ToString()));
							else
								this.Items.AddDialogForm(dr["NAME"].ToString(), LookUpTableSet.Tables[dr["DATATABLE"].ToString()], dr["DISPLAYMEMBER"].ToString(), dr["VALUEMEMBER"].ToString());

							this.Items[dr["NAME"].ToString()].ColumnHeaders = dr["COLUMNS"].ToString();
							this.Items[dr["NAME"].ToString()].ColumnCaps = dr["CAPTIONS"].ToString();
							//if (dr["VALUEMEMBER"].ToString().Length > 0)                           
							//    this.Items[dr["NAME"].ToString()].ValueMember  = dr["VALUEMEMBER"].ToString();                                
							
							//this.Items[dr["NAME"].ToString()].DisplayMember = dr["DISPLAYMEMBER"].ToString();

							if (Convert.ToInt16(dr["STRLEN"].ToString().Substring(dr["STRLEN"].ToString().Length - 1))==1)                            
								this.Items[dr["NAME"].ToString()].SingleOrDouble = "SINGLE";
							else if (Convert.ToInt16(dr["STRLEN"].ToString().Substring(dr["STRLEN"].ToString().Length - 1)) == 2)
								this.Items[dr["NAME"].ToString()].SingleOrDouble = "DOUBLE";
						}
						else
							this.Items.AddListBox(dr["NAME"].ToString(), LookUpTableSet.Tables[dr["DATATABLE"].ToString()], dr["DISPLAYMEMBER"].ToString(), dr["VALUEMEMBER"].ToString(), Convert.ToBoolean(dr["RESIZABLE_DROPDOWN"].ToString()));
					}
				}
				this.Refresh();
				//SetFocusToGridItem(firstLable); 
				SelectFirstItem();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		
		private void SelectFirstItem()
		{
			GridItem gi = this.SelectedGridItem;
			if (this.PropertySort == PropertySort.Alphabetical)
			{
				GridItem p_gi = gi.Parent;
				foreach (GridItem egi in p_gi.GridItems) { }
				p_gi.GridItems[0].Select();
			}
			else
			{
				GridItem p_gi = gi.Parent.Parent;
				foreach (GridItem egi in p_gi.GridItems)
				{
					foreach (GridItem eegi in egi.GridItems) { }
				}
				p_gi.GridItems[0].GridItems[0].Select();
			}
		}
		/// <summary>
		/// Return Stored Procedure Parameter with it value Collection
		/// </summary>
		/// <returns></returns>
		public Hashtable GetParams()
		{
			if (!ValidateProperty())
				return null;

			string strValues = "";
			Hashtable HTParam = new Hashtable();
			try
			{
				foreach (CustomProperty p in this.Items)
				{                    
					if (p.Visible && p.Value != null && !string.IsNullOrEmpty(p.Value.ToString()))
					{
						if (p.Type.ToString() == "System.String" && p.Value.ToString().Length > 0 && p.Value.ToString() != "ALL")
						{
							strValues = "";
							p.SelectedValue = "";  //reset property                            
							
							if (!string.IsNullOrEmpty(p.ValueMember))
							{                                
								if (p.Value.ToString().Length > 0)
								{
									DataTable dt = p.Datasource as DataTable;
									
									int colInd = dt.Columns.IndexOf(p.ValueMember);
									DataRow[] dr = null;
									strValues = "";
									foreach (string strFld in p.Value.ToString().Split(','))
									{
										dr = dt.Select(p.DisplayMember + " ='" + strFld + "'");
										if (dr.Length > 0)
										{
											if (strValues.Length == 0)
												strValues = dr[0].ItemArray[colInd].ToString();
											else
												strValues = strValues + "," + dr[0].ItemArray[colInd].ToString();
										}
									}
									
									if(p.SingleOrDouble.Length>0)
										strValues = GetStrList(strValues, p.SingleOrDouble);
								}
								else
									strValues = p.SelectedValue.ToString();
							}
							else if (p.SingleOrDouble.Length > 0) 
								strValues = GetStrList(p.Value.ToString(), p.SingleOrDouble);
							else
								strValues = p.Value.ToString();

							HTParam.Add(p.Name, strValues);                            
						}
						else if (p.Type.ToString() == "System.Boolean")
						{                            
								HTParam.Add(p.Name,Convert.ToBoolean(p.Value));
						}

						else if ((p.Type.ToString() == "System.Decimal") && Convert.ToDecimal(p.Value.ToString()) > 0)
						{
							if (!string.IsNullOrEmpty(p.ValueMember))
								HTParam.Add(p.Name, Convert.ToDecimal(p.SelectedValue));
							else
								HTParam.Add(p.Name, Convert.ToDecimal(p.Value));
						}
						else if ((p.Type.ToString() == "System.Double") && Convert.ToDouble(p.Value.ToString()) > 0)
						{
							if (!string.IsNullOrEmpty(p.ValueMember))
								HTParam.Add(p.Name, Convert.ToDouble(p.SelectedValue));
							else
								HTParam.Add(p.Name, Convert.ToDouble(p.Value));
						}
							 
						else if ((p.Type.ToString()=="System.Int16") && Convert.ToInt16(p.Value.ToString()) > 0)
						{
							if (!string.IsNullOrEmpty(p.ValueMember))
								HTParam.Add(p.Name, Convert.ToInt16(p.SelectedValue));
							else
								HTParam.Add(p.Name, Convert.ToInt16(p.Value));
						}

						else if ((p.Type.ToString() == "System.Int32") && Convert.ToInt32(p.Value.ToString()) > 0)
						{
							if (!string.IsNullOrEmpty(p.ValueMember))
								HTParam.Add(p.Name, Convert.ToInt32(p.SelectedValue));
							else
								HTParam.Add(p.Name, Convert.ToInt32(p.Value));
						}

						else if ((p.Type.ToString() == "System.Int64") && Convert.ToInt64(p.Value.ToString()) > 0)
						{
							if (!string.IsNullOrEmpty(p.ValueMember))
								HTParam.Add(p.Name, Convert.ToInt64(p.SelectedValue));
							else
								HTParam.Add(p.Name, Convert.ToInt64(p.Value));
						}

						else if (p.Type.ToString() == "System.DateTime" && this.IsDate(p.Value.ToString()))
						{
							HTParam.Add(p.Name, DateTime.Parse(p.Value.ToString()).ToString("MM/dd/yyyy"));
						}

						else if (p.Type.ToString() == "System.TimeSpan" && this.IsTime(p.Value.ToString()))
						{
							HTParam.Add(p.Name, DateTime.Parse(p.Value.ToString()).ToShortTimeString());
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
		   
			return HTParam;
		}
		private string  GetStrList(string stringValues,string strType)
		{
			string StrSingleList="";
			string StrDbleQuotList = "";
			
			foreach (string strVal in stringValues.Split(','))
			{
				if (strType == "DOUBLE")
				{
					if (StrDbleQuotList.Length != 0) StrDbleQuotList = StrDbleQuotList + ",";
					StrDbleQuotList = StrDbleQuotList + "''" + strVal + "''";                    
				}
				else
				{
					if (StrSingleList.Length != 0) StrSingleList = StrSingleList + ",";
					StrSingleList = StrSingleList + "'" + strVal + "'";
				}
					
			}
			if (strType == "DOUBLE")
				return "'" + StrDbleQuotList + "'";
			else
				return StrSingleList;            
		}

		public string GetSpSql()
		{
			StringBuilder sd = new StringBuilder();
			
			object selValue = null;
			string returnStr = "";
			try
			{
				foreach (DataRow dr in dtSpParams.Rows)
				{
					CustomProperty p = this.Items[dr["NAME"].ToString()];
					if (p.SelectedValue == null)
					{
						if (dr["DATATYPE"].ToString().ToUpper() == "STRING")
							selValue = string.Empty;
						else
							selValue = 0;
					}
					if (p != null)
					{
						if (dr["DATATYPE"].ToString().ToUpper() == "STRING")
						{
							if (p.Value.ToString() == "ALL")
								sd.Append("''");
							else if (!string.IsNullOrEmpty(p.ValueMember))
							{
								if (p.SingleOrDouble == "SINGLE")
									sd.Append("'" + p.SingleQuoted.ToString() + "'");
								else if (p.SingleOrDouble == "DOUBLE")
									sd.Append(p.DoubleQuoted.ToString());
								else
									sd.Append("'" + selValue.ToString() + "'");                                
							}
							else
							{
								if (p.SingleOrDouble == "SINGLE")
									sd.Append("'" + p.SingleQuoted.ToString() + "'");
								else if (p.SingleOrDouble == "DOUBLE")
									sd.Append(p.DoubleQuoted.ToString());
								else
									sd.Append("'" + p.Value.ToString() + "'");
							}
						}

						else if (dr["DATATYPE"].ToString().ToUpper() == "BOOL")
							sd.Append(Convert.ToBoolean(p.Value));

						else if (dr["DATATYPE"].ToString().ToUpper() == "DECIMAL")
						{
							if (!string.IsNullOrEmpty(p.ValueMember))
								sd.Append(Convert.ToDecimal(selValue));
							else
								sd.Append(Convert.ToDecimal(p.Value));
						}
						else if (dr["DATATYPE"].ToString().ToUpper() == "DOUBLE")
						{
							if (!string.IsNullOrEmpty(p.ValueMember))
								sd.Append(Convert.ToDouble(selValue));
							else
								sd.Append(Convert.ToDouble(p.Value));
						}

						else if (dr["DATATYPE"].ToString().ToUpper() == "INT")
						{
							if (!string.IsNullOrEmpty(p.ValueMember))
								sd.Append(Convert.ToInt16(selValue));
							else
								sd.Append(Convert.ToInt16(p.Value));
						}

						else if (dr["DATATYPE"].ToString().ToUpper() == "DATE")
						{
							if (this.IsDate(p.Value.ToString()))
								sd.Append("'" + DateTime.Parse(p.Value.ToString()).ToString("MM/dd/yyyy") + "'");
							else
								sd.Append("''");
						}

						else if (dr["DATATYPE"].ToString().ToUpper() == "TIME")
						{
							if (this.IsTime(p.Value.ToString()))
								sd.Append("'" + DateTime.Parse(p.Value.ToString()).ToShortTimeString() + "'");
							else
								sd.Append("''");
						}
						sd.Append(",");
					}
				}
			}
			catch (Exception e1)
			{
				MessageBox.Show(e1.Message);
			}
			returnStr =  sd.ToString();
			returnStr = returnStr.Substring(0, returnStr.Length - 1);
			return returnStr;
		}
		/// <summary>
		/// Validates All Required(Marked as *) Property Of PropertyGrid. i.e Parameter of Stored Procedure Without Default Value.
		/// </summary>
		/// <returns></returns>
		public bool ValidateProperty()
		{
			StringBuilder sb = new StringBuilder();   
			try
			{
				foreach (CustomProperty p in this.Items)
				{
					if (p.DisplayName.Contains("*"))
					{
						if (p.Type.ToString() == "System.String" && p.Value.ToString().Length == 0)
						{
							MessageBox.Show(p.DisplayName + " Is  Required");
							this.SetFocusToGridItem(p.DisplayName);
							return false;
						}

						else if ((p.Type.ToString() == "System.Decimal") && Convert.ToDecimal(p.Value.ToString()) <= 0)
						{
							MessageBox.Show(p.DisplayName + " Is  Required");
							this.SetFocusToGridItem(p.DisplayName);
							return false;
						}
						else if ((p.Type.ToString() == "System.Double") && Convert.ToDouble(p.Value.ToString()) <= 0)
						{
							MessageBox.Show(p.DisplayName + " Is  Required");
							this.SetFocusToGridItem(p.DisplayName);
							return false;
						}
						else if ((p.Type.ToString().Contains("Int")) && Convert.ToInt64(p.Value.ToString()) <= 0)
						{
							MessageBox.Show(p.DisplayName + " Is  Required");
							this.SetFocusToGridItem(p.DisplayName);
							return false;
						}

						else if (p.Type.ToString() == "System.DateTime" && this.IsDate(p.Value.ToString()) == false)
						{
							MessageBox.Show(p.DisplayName + " Is  Required");
							this.SetFocusToGridItem(p.DisplayName);
							return false;
						}

						else if (p.Type.ToString() == "System.TimeSpan" && this.IsTime(p.Value.ToString()) == false)
						{
							MessageBox.Show(p.DisplayName + " Is  Required");
							this.SetFocusToGridItem(p.DisplayName);
							return false;
						}
					}
					string sVal = Convert.ToString(p.Value);
					if (p.Category.ToUpper() != "SYSTEM"  && !string.IsNullOrEmpty(sVal) && sVal != "0" && sVal != "0.0"  && sVal != "ALL" && sVal != "00:00:00")
					{
						string dispName = p.DisplayName.Replace('*', ' ').Trim();
						if (p.Type.ToString() == "System.DateTime")
						{
							if(this.IsDate(sVal))
								sb.Append(dispName + " " + DateTime.Parse(p.Value.ToString()).ToShortDateString() + "\n");
						}
						else
							sb.Append(dispName + " " + p.Value.ToString() + "\n");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
			ParamList = sb.ToString();
			return true;
		}
		private bool IsDate(string anyString)
		{
			if (anyString == null || anyString.Length ==0 )
			{
				anyString = "";
				return false;
			}
			if (anyString.Length >= 10 && anyString.Substring(0, 10) == "01/01/0001")
				return false;
			
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
			
			return true ;
		}
		#endregion
		
		#region "Protected Functions"
		protected override void OnPropertyValueChanged(PropertyValueChangedEventArgs e)
		{
			base.OnPropertyValueChanged(e);
		}

		// Do special processing for Tab key.
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{            
			if ((keyData == Keys.Enter) || (keyData == Keys.Tab) || (keyData == (Keys.Tab | Keys.Shift)))
			{
				GridItem selectedItem = SelectedGridItem;
				
				GridItem root = selectedItem;
				while (root.Parent != null)
				{
					root = root.Parent;
				}
				// Find all expanded items and put them in a list.
				ArrayList items = new ArrayList();
				AddExpandedItems(root, items);
				if (selectedItem != null)
				{
					// Find selectedItem.
					int foundIndex = items.IndexOf(selectedItem);
					if ((keyData & Keys.Shift) == Keys.Shift)
					{
						foundIndex--;
						if (foundIndex < 0)
						{
							foundIndex = items.Count - 1;
						}
						if (items.Count > 0)
						{
							SelectedGridItem = (GridItem)items[foundIndex];
							if (expandOnTab && (SelectedGridItem.GridItems.Count > 0))
							{
								SelectedGridItem.Expanded = false;
							}
						}
					}
					else
					{
						foundIndex++;
						if (foundIndex >= items.Count)
						{
							if (MoveToFirst)
								foundIndex = 0;
							else
							{
								foundIndex--;
								EventHandler eh = this.PropertyOver;
								if (eh != null)
								{
									SelectedGridItem = (GridItem)items[foundIndex];  //FIRES PROPERTY VALUE CHANGE EVENT BEFORE LEAVE TO APPLY CHANGED PROPERTY
									eh.Invoke(this, EventArgs.Empty);
								}
								return true;
							}
						}
						if (items.Count > 0)
						{
							SelectedGridItem = (GridItem)items[foundIndex];
							if (expandOnTab && (SelectedGridItem.GridItems.Count > 0))
							{
								SelectedGridItem.Expanded = true;
							}
						}
					}

					return true;
				}
			}
			else if ((keyData == Keys.F4))            //WHEN AT MULTI SELECT PROPERTY AFTER TYPEING DATA USER PRESS F4 THNE IT WILL NOT COMMIT,SO COMMIT MANUALY
				this.Refresh(); 
			
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void AddExpandedItems(GridItem parent, IList items)
		{
			if (parent.PropertyDescriptor != null)
			{
				items.Add(parent);
			}
			if (parent.Expanded)
			{
				foreach (GridItem child in parent.GridItems)
				{
					AddExpandedItems(child, items);
				}
			}
		}
		protected override void OnResize(System.EventArgs e)
		{
			base.OnResize(e);
			if (bAutoSizeProperties)
			{
				AutoSizeSplitter(32);
			}
		}
		
		protected void AutoSizeSplitter(int RightMargin)
		{
			
			GridItemCollection oItemCollection =  (System.Windows.Forms.GridItemCollection) oPropertyGridEntries.GetValue(oPropertyGridView);
			if (oItemCollection == null)
			{
				return;
			}
			System.Drawing.Graphics oGraphics = System.Drawing.Graphics.FromHwnd(this.Handle);
			int CurWidth = 0;
			int MaxWidth = 0;
			
			foreach (GridItem oItem in oItemCollection)
			{
				if (oItem.GridItemType == GridItemType.Property)
				{
					CurWidth =  (int) oGraphics.MeasureString(oItem.Label, this.Font).Width + RightMargin;
					if (CurWidth > MaxWidth)
					{
						MaxWidth = CurWidth;
					}
				}
			}
			
			MoveSplitterTo(MaxWidth);
		}
		protected void ApplyToolStripRenderMode(bool value)
		{
			if (value)
			{
				oToolStrip.Renderer = new ToolStripSystemRenderer();
			}
			else
			{
				ToolStripProfessionalRenderer renderer = new ToolStripProfessionalRenderer(new CustomColorScheme());
				renderer.RoundedEdges = false;
				oToolStrip.Renderer = renderer;
			}
		}
		#endregion
		
		#region "Properties"
		private bool expandOnTab = false;
		[Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DescriptionAttribute("Set the collection of the CustomProperty. Set ShowCustomProperties to True to enable it."), RefreshProperties(RefreshProperties.Repaint)]public CustomPropertyCollection Items
		{
			get
			{
				return oCustomPropertyCollection;
			}
		}

		[Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DescriptionAttribute("Set the CustomPropertyCollectionSet. Set ShowCustomPropertiesSet to True to enable it."), RefreshProperties(RefreshProperties.Repaint)]public CustomPropertyCollectionSet ItemSet
		{
			get
			{
				return oCustomPropertyCollectionSet;
			}
		}

		[Category("Behavior"), DefaultValue(false), DescriptionAttribute("Move automatically the splitter to better fit all the properties shown.")]public bool AutoSizeProperties
		{
			get
			{
				return bAutoSizeProperties;
			}
			set
			{
				bAutoSizeProperties = value;
				if (value)
				{
					AutoSizeSplitter(32);
				}
			}
		}

		[Category("Behavior"), DefaultValue(false), DescriptionAttribute("Use the custom properties collection as SelectedObject."), RefreshProperties(RefreshProperties.All)]public bool ShowCustomProperties
		{
			get
			{
				return bShowCustomProperties;
			}
			set
			{
				if (value == true)
				{
					bShowCustomPropertiesSet = false;
					base.SelectedObject = oCustomPropertyCollection;
				}
				bShowCustomProperties = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), DescriptionAttribute("Use the custom properties collections as SelectedObjects."), RefreshProperties(RefreshProperties.All)]public bool ShowCustomPropertiesSet
		{
			get
			{
				return bShowCustomPropertiesSet;
			}
			set
			{
				if (value == true)
				{
					bShowCustomProperties = false;
					base.SelectedObjects =  (object[]) oCustomPropertyCollectionSet.ToArray();
				}
				bShowCustomPropertiesSet = value;
			}
		}
		[Category("Behavior"), DefaultValue(false), DescriptionAttribute("Tab Navigation Enabled"), RefreshProperties(RefreshProperties.All)]
		public bool ExpandOnTab
		{
			get
			{
				return expandOnTab;
			}
			set
			{
				expandOnTab = value;
			}
		}
		[Category("Behavior"), DefaultValue(false), DescriptionAttribute("Reset Cursor on First item when reached at last item"), RefreshProperties(RefreshProperties.All)]
		private bool _First = false;
		public bool MoveToFirst
		{
			get
			{
				return _First;
			}
			set
			{
				_First = value;
			}
		}
		[Category("Appearance"), DefaultValue(false), DescriptionAttribute("Draw a flat toolbar")]public new bool DrawFlatToolbar
		{
			get
			{
				return bDrawFlatToolbar;
			}
			set
			{
				bDrawFlatToolbar = value;
				ApplyToolStripRenderMode(bDrawFlatToolbar);
			}
		}

		[Category("Appearance"), DisplayName("Toolstrip"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DescriptionAttribute("Toolbar object"), Browsable(true)]public ToolStrip ToolStrip
		{
			get
			{
				return oToolStrip;
			}
		}
		
		[Category("Appearance"), DisplayName("Help"), DescriptionAttribute("DocComment object. Represent the comments area of the PropertyGrid."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]public Control DocComment
		{
			get
			{
				return  (System.Windows.Forms.Control) oDocComment;
			}
		}
		
		[Category("Appearance"), DisplayName("HelpTitle"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DescriptionAttribute("Help Title Label."), Browsable(true)]public Label DocCommentTitle
		{
			get
			{
				return oDocCommentTitle;
			}
		}
		
		[Category("Appearance"), DisplayName("HelpDescription"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DescriptionAttribute("Help Description Label."), Browsable(true)]public Label DocCommentDescription
		{
			get
			{
				return oDocCommentDescription;
			}
		}
		
		[Category("Appearance"), DisplayName("HelpImageBackground"), DescriptionAttribute("Help Image Background.")]public Image DocCommentImage
		{
			get
			{
				return ((Control)oDocComment).BackgroundImage;
			}
			set
			{
				((Control)oDocComment).BackgroundImage = value;
			}
		}
		
		#endregion
		
	}
	
}

