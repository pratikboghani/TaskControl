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
	public partial class UTCTextBox : TextBox
	{
		private string _ToolTips = "";
		String _StrCurrentCulture = UTC.ControlSetting._StrCurrentCulture; 

		public enum InputStype
		{
			None = 0,
			Char = 1,
			Number = 2,
			CharNumber = 3
		}

		//private Font oldFont = null;
		//private Boolean waterMarkTextEnabled = false;

		#region Attributes
		private Color _waterMarkColor = Color.Gray;
		public Color WaterMarkColor
		{
			get { return _waterMarkColor; }
			set { _waterMarkColor = value; Invalidate(); }
		}

		private string _waterMarkText = "";
		public string WaterMarkText
		{
			get { return _waterMarkText; }
			set { _waterMarkText = value; Invalidate(); }
		}
		#endregion

		//Default constructor
		public UTCTextBox()
		{
			InitializeComponent();

			if (this.PasswordChar != '\0')
				this.UseSystemPasswordChar = true;
		   
		}
		
		protected override void OnCreateControl()
		{
			base.OnCreateControl();           
		}

		//Override OnPaint
		protected override void OnPaint(PaintEventArgs args)
		{           
			base.OnPaint(args);
		}
			   
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

		private Color c1;
		private Int32 mIntDigit=0;
		private Boolean mBlnChangeByKeyBoard = false;
		public Boolean mBlnIsNumeric = false;
		public DateTime mDatTim;
		public string SqlText="";

		public UTC.UCInputNumeric UCInputNumeric
		{
			get { return _UCInputNumeric; }
			set { _UCInputNumeric = value; }
		}
		public UTC.UCInputChar UCInputChar
		{
			get { return _UCInputChar; }
			set { _UCInputChar = value; }
		}
		public UTC.UCInputCharNumeric UCInputCharNumeric
		{
			get { return _UCInputCharNumeric; }
			set { _UCInputCharNumeric = value; }
		}


		private UTC.UCInputNumeric _UCInputNumeric;
		private UTC.UCInputChar _UCInputChar;
		private UTC.UCInputCharNumeric _UCInputCharNumeric;		

		#region Fields/Properties
				

		public override string Text
		{
			get
			{
				if (mBlnIsNumeric)
				{
					if (base.Text.Trim().Length == 0)
					{
						return "0";
					}
					else
						return base.Text;
				}
				else if (_Format.Contains("%"))
				{ 
					return (Convert.ToDouble(base.Text.Replace('%',' ').Trim())).ToString();
				}
				else
					return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}
		public string Value
		{
			get
			{
				if (_Format.Contains("%"))
				{
					if (Text == "" || Text == "0")
					{
						return Text;
					}
					else
					{
						return (Convert.ToDouble(base.Text.Replace('%', ' ').Trim()) / 100).ToString();
					}
				}
				else
				{
					return Text;
				}
			}
		}

		protected override void OnReadOnlyChanged(EventArgs e)
		{
			if (this.ReadOnly == true)
			{
				this.TabStop = false;
			}
			else
			{
				this.TabStop = true;
			}
			base.OnReadOnlyChanged(e);
		}

		private InputStype _MouseEntryStyle = InputStype.None;
		[Browsable(true)]
		public InputStype MouseEntryStyle
		{
			get { return _MouseEntryStyle; }
			set { _MouseEntryStyle = value; }
		}

		private void ShowMouseEntry()
		{
			if (_MouseEntryStyle == InputStype.Number)
			{
				if (_UCInputNumeric == null)
				{
					_UCInputNumeric = new UCInputNumeric();
				}
				_UCInputNumeric.txtInputbox = this;
				if (this.Parent.GetType().ToString() == "System.Windows.Forms.GroupBox")
				{
					_UCInputNumeric.Top = this.Parent.Top + this.Top + this.Height;
					_UCInputNumeric.Left = this.Parent.Left + this.Left + (this.Width / 2) - (_UCInputNumeric.Width / 2);

					this.FindForm().Controls.Add(_UCInputNumeric);
					_UCInputNumeric.BringToFront();
				}
				else
				{
					_UCInputNumeric.Top = this.Top + this.Height;
					_UCInputNumeric.Left = this.Left + (this.Width / 2) - (_UCInputNumeric.Width / 2);

					this.FindForm().Controls.Add(_UCInputNumeric);
					_UCInputNumeric.BringToFront();
				}
				_UCInputNumeric.Show();
				_UCInputNumeric.Focus();
			}
			else if (_MouseEntryStyle == InputStype.CharNumber)
			{
				if (_UCInputCharNumeric == null)
				{
					_UCInputCharNumeric = new UCInputCharNumeric();
				}
				_UCInputCharNumeric.txtInputBox = this;
				if (this.Parent.GetType().ToString() == "System.Windows.Forms.GroupBox")
				{
					_UCInputCharNumeric.Top = this.Parent.Top + this.Top + this.Height;
					_UCInputCharNumeric.Left = this.Parent.Left + this.Left + (this.Width / 2) - (_UCInputCharNumeric.Width / 2);

					this.FindForm().Controls.Add(_UCInputCharNumeric);
					_UCInputCharNumeric.BringToFront();
				}
				else
				{
					_UCInputCharNumeric.Top = this.Top + this.Height;
					_UCInputCharNumeric.Left = this.Left + (this.Width / 2) - (_UCInputCharNumeric.Width / 2);

					this.FindForm().Controls.Add(_UCInputCharNumeric);
					_UCInputCharNumeric.BringToFront();
				}
				_UCInputCharNumeric.Show();
				_UCInputCharNumeric.Focus();
			}
			else
			{
				if (_UCInputChar == null)
				{
					_UCInputChar = new UCInputChar();
				}
				_UCInputChar.txtInputBox = this;
				if (this.Parent.GetType().ToString() == "System.Windows.Forms.GroupBox")
				{
					_UCInputChar.Top = this.Parent.Top + this.Top + this.Height;
					_UCInputChar.Left = this.Parent.Left + this.Left + (this.Width / 2) - (_UCInputChar.Width / 2);

					this.FindForm().Controls.Add(_UCInputChar);
					_UCInputChar.BringToFront();
				}
				else
				{
					_UCInputChar.Top = this.Top + this.Height;
					_UCInputChar.Left = this.Left + (this.Width / 2) - (_UCInputChar.Width / 2);

					this.FindForm().Controls.Add(_UCInputChar);
					_UCInputChar.BringToFront();
				}
				_UCInputChar.Show();
				_UCInputChar.Focus();

			}

		}
		private string _requiredChars = string.Empty;
		public string RequiredChars
		{
			get { return _requiredChars; }
			set { _requiredChars = value; }
		}

		/// <summary>
		/// Take Format String  For Validation
		/// </summary>
		private string _Format = string.Empty;
		[Browsable(true)]
		public string Format
		{
			get { return _Format; }
			set
			{
				_Format = value;
				if (_Format.Length != 0)
				{
					//if (_Format.Substring(0, 1).CompareTo("#") == 0)
					if (_Format.Contains("#") == true)
					{
						mIntDigit = 0;
						for (int i = _Format.Length - 1; i >= 0 && _Format[i] != '.'; i--)
							mIntDigit++;
						if (mIntDigit == _Format.Length) mIntDigit = 0;
						_requiredChars = "0123456789.";
						TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
						mBlnIsNumeric = true;
					}
					else
					{
						_requiredChars = "";
						mBlnIsNumeric = false;
					}
					if (_Format.ToUpper() == "DD/MM/YY" && _StrCurrentCulture == "en-US") _Format = "MM/dd/yy";
					if (_Format.ToUpper() == "DD/MM/YYYY" && _StrCurrentCulture == "en-US") _Format = "MM/dd/yyyy";
				}
				else
				{
					_requiredChars = "";
					mBlnIsNumeric = false;
				}
			}
		}
		/// <summary>
		/// Take Boolean Flag For Color Change WIth Goto Focus Is Diaplay Or Not 
		/// </summary>
		private Boolean _ActivationColor;
		[Browsable(true)]
		public Boolean ActivationColor
		{
			get { return _ActivationColor; }
			set { _ActivationColor = value; }
		}

		private bool _AutoDate=false;
		[Browsable(true)]
		public bool AutoDate
		{
			get { return _AutoDate; }
			set { _AutoDate = value; }
		}

		private bool _SelectOnClick = false;
		[Browsable(true)]
		public bool SelectOnClick
		{
			get { return _SelectOnClick; }
			set { _SelectOnClick = value; }
		}

		private bool _allowNegative = false;
		[Browsable(true)]
		public bool AllowNegativeValue
		{
			get { return _allowNegative; }
			set { _allowNegative = value; }
		}
		#endregion

		#region Overridden Methods
		
		protected override void OnKeyPress(KeyPressEventArgs e)
		{           
			if ((_requiredChars != string.Empty ))
			{
				if (ValNum(e, e.KeyChar, mIntDigit) == false)
				{
					e.Handled = true;
				}
			}
			if ((_requiredChars != string.Empty && TextLength == 0))
			{
				if (ValNum(e, e.KeyChar, mIntDigit) == false)
				{
					e.Handled = true;
				}
			}
			
			//if (e.KeyChar.ToString().Length != 0)
			//{
			//    if (e.KeyChar == (Char)4)
			//    {
			//        //PTControls.cConMenu.cConMenu Con = new PTControls.cConMenu.cConMenu();
			//        //Con.STRPROGRAM = this.FindForm().ProductName;
			//        //Con.STRFORMNAME = this.FindForm().Name;
			//        //Con.STRCONTNAME = this.Name;
			//        //Con.Show(this, new Point(this.Bottom, this.Left));
			//    }
			//}
			
			if (e.Handled == false)
			{
				base.OnKeyPress(e);
			}
		}
		protected override void OnClick(EventArgs e)
		{
			if(_SelectOnClick)
			{
				SelectionStart = 0;
				SelectionLength = Text.Length;
			}
			base.OnClick(e);
		}
		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (_MouseEntryStyle != InputStype.None)
			{
				ShowMouseEntry();
			}
			base.OnMouseClick(e);
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			KeyEventArgs K = new KeyEventArgs(Keys.F1);
			base.OnKeyDown(K);
		}

		protected override void OnGotFocus(EventArgs e)
		{
			SelectionStart = 0;
			SelectionLength = Text.Length;
			if (_ActivationColor)
			{
				c1 = BackColor;
				BackColor = Color.GreenYellow;
			}
			mBlnChangeByKeyBoard = true;
			ChkValidation();
			if (_AutoDate == true && TextLength==0)
			{
				if (_Format.ToUpper().CompareTo("DD/MM/YY") == 0 || _Format.ToUpper().CompareTo("DD/MM/YYYY") == 0 || _Format.ToUpper().CompareTo("HH:MM TT") == 0)
				{
					Text = DateTime.Today.ToString(_Format);                    
				}
			}
			if (_Format.Contains("%"))
			{ 
				this.Text=Text.Replace('%',' ').Trim();
			}
			base.OnGotFocus(e);
		}
		protected override void OnLostFocus(EventArgs e)
		{
			if (this.Parent == null) return;
			mBlnChangeByKeyBoard = false;
			base.OnLostFocus(e);
		}
		protected override void OnTextChanged(EventArgs e)
		{
			if (!mBlnChangeByKeyBoard)
			{
				ChkValidation();
			}
			base.OnTextChanged(e);
		}
	   
		protected override void OnValidating(CancelEventArgs e)
		{
			if (this.Parent == null) return;
			//if (this.FindForm().Disposing == true) return;
			//if (Disposing == true) return;
			ChkValidation();
			base.OnValidating(e);
		}
		protected override void OnValidated(EventArgs e)
		{
			if (this.FindForm().Disposing == true) return;
			if (Disposing == true) return;
			base.OnValidated(e);
		}
		protected override void OnLeave(EventArgs e)
		{
			if (FindForm() == null) return;
			if (FindForm().Disposing == true) return;
			if (Disposing == true) return;
			base.OnLeave(e);
		}
		#endregion

		#region User Define function

		public bool ValNum(KeyPressEventArgs e, int KeyAscii, int NumberofDecimal)
		{
			int intDotPosition = 0;
			int DigitInFraction;

			if (ModifierKeys == Keys.Control) return true;

			if (KeyAscii == 13 || KeyAscii==8) return true;
			
			if (SelectedText.Length ==0)
				if (_Format.Length == this.Text.Length) return false;

			if(AllowNegativeValue && KeyAscii ==45)
			{
				if (Microsoft.VisualBasic.Strings.InStr(1, this.Text, "-", Microsoft.VisualBasic.CompareMethod.Text) > 0)
					return false;
				else
					return true;
			}

			intDotPosition = Microsoft.VisualBasic.Strings.InStr(1, this.Text, ".", Microsoft.VisualBasic.CompareMethod.Text);

			if (!((KeyAscii > 47 && KeyAscii < 58) || KeyAscii == 8))
			{
				if (KeyAscii == 46 && NumberofDecimal > 0 )
				{
					if ((intDotPosition > 0 && SelectedText.Length > 0))
						return true;
					else if (!(intDotPosition > 0))
						return true;
					else
						return false;
				}
				return false  ;
			}
			else
			{                
				if (intDotPosition > 0)
				{
					DigitInFraction = Microsoft.VisualBasic.Strings.Len(Microsoft.VisualBasic.Strings.Mid(this.Text, intDotPosition + 1, Microsoft.VisualBasic.Strings.Len(this.Text)));
					if (this.SelectionStart > intDotPosition && DigitInFraction >= NumberofDecimal)                    
						return false;                    
				}                                                                            
			}
			return true;
		}
		private void ChkValidation()
		{
			if (_ActivationColor)
				BackColor = c1;
			if (_Format.Length != 0)
			{
				if (_Format.ToUpper().CompareTo("DD/MM/YY") == 0 || _Format.ToUpper().CompareTo("DD/MM/YYYY") == 0 || _Format.ToUpper().CompareTo("HH:MM TT") == 0)                
				{
					if (DateTime.TryParse(Text, out mDatTim) == true)
					{
						if (_Format.ToUpper().CompareTo("HH:MM TT") == 0)
						{
							Text = mDatTim.ToString("hh:mm tt");
							SqlText = mDatTim.ToString("hh:mm tt");
						}
						else
						{
							if (_Format.ToUpper().CompareTo("DD/MM/YY") == 0)
							{
								Text = mDatTim.ToString("dd/MM/yy");
								SqlText = mDatTim.ToString("MM/dd/yy");
							}
							else
							{
								Text = mDatTim.ToString("dd/MM/yyyy");
								SqlText = mDatTim.ToString("MM/dd/yyyy");
							}
						}
					}
					else
					{
						Text = "";
						SqlText = "";
					}
				}
				else if ((_Format.ToUpper().CompareTo("MM/DD/YY") == 0 || _Format.ToUpper().CompareTo("MM/DD/YYYY") == 0) && _StrCurrentCulture == "en-US")
				{
					if (DateTime.TryParse(Text, out mDatTim) == true)
					{
						if (_Format.ToUpper().CompareTo("MM/DD/YY") == 0)
						{
							Text = mDatTim.ToString("MM/dd/yy");
							SqlText = mDatTim.ToString("MM/dd/yy");
						}
						else
						{
							Text = mDatTim.ToString("MM/dd/yyyy");
							SqlText = mDatTim.ToString("MM/dd/yyyy");
						}
					}
					else
					{
						Text = "";
						SqlText = "";
					}
				}
				else if (_Format.Contains("%") == true)
				{
					string StrText = this.Text;
					if (StrText.Contains("%"))
					{
						StrText = (Microsoft.VisualBasic.Conversion.Val(StrText.Replace('%', ' ').Trim()) / 100).ToString();
					}
					else if (Microsoft.VisualBasic.Conversion.Val(StrText.Replace('%', ' ').Trim())>1)
					{
						StrText = (Microsoft.VisualBasic.Conversion.Val(StrText) / 100).ToString();
					}
					int intDotPosition = Microsoft.VisualBasic.Strings.InStr(1, _Format, ".", Microsoft.VisualBasic.CompareMethod.Text);
					int DigitInFraction = Microsoft.VisualBasic.Strings.Len(Microsoft.VisualBasic.Strings.Mid(_Format, intDotPosition + 1, Microsoft.VisualBasic.Strings.Len(_Format)))-1;
					Text = Microsoft.VisualBasic.Strings.FormatPercent(StrText, DigitInFraction, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault);
				}
				else if (_Format.Contains("#") == true)
				{
					Text = Microsoft.VisualBasic.Strings.Format(Microsoft.VisualBasic.Conversion.Val(Text), _Format);
				}
			}
		}
		/// <summary>
		/// Get Method To Get Direct Int
		/// </summary>
		/// <returns></returns>
		public int ToInt()
		{
			return Convert.ToInt16(Text);
		}
		/// <summary>
		/// Get Method To Get Direct Double 
		/// </summary>
		/// <returns></returns>
		public double ToDouble()
		{
			return Convert.ToDouble(Text);
		}
		public DateTime ToDate()
		{
			DateTime Temp=new DateTime();
			if (DateTime.TryParse(Text,out Temp) == true)

			{
				return Convert.ToDateTime(Text);
			}
			else
			{
				return Convert.ToDateTime(null);
			}
		}
		public Boolean IsDate()
		{
			DateTime Temp = new DateTime();
			if (DateTime.TryParse(Text, out Temp) == true)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public void SetAutoComplete(DataTable DTab,string StrColumn)
		{
			System.Windows.Forms.AutoCompleteStringCollection AutoCompleteStringCollection = new System.Windows.Forms.AutoCompleteStringCollection();
			foreach (DataRow DRow in DTab.Rows)
			{
				AutoCompleteStringCollection.Add(DRow[StrColumn].ToString());
			}
			this.AutoCompleteCustomSource = AutoCompleteStringCollection;
			this.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
		}
		#endregion
	}
}
