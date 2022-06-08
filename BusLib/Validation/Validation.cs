using System;
using System.Collections.Generic;
using System.Text; 
using System.Collections; 
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using GSql = DataLib.GlobalSql;
using System.Drawing;

namespace BusLib.Validation
{
    /// <summary>
    /// Class To Perform Common Validation
    /// </summary>
    public class BOValidation
    {

        public static int gIntmsgMod = 0;
        public static string gstrMessage = "";
        public static string gStrUserName = "";
        public static string gStrHostName = "";
        public static string gStrIP = "";
        public static System.Drawing.Color gMsgColor = System.Drawing.Color.Red;
        public static string gStrMempCode = "2";
        public static Label lblMessage;
        /// <summary>
        /// Enum Font Name [0 - Regular,1 - Gujarati]
        /// </summary>
        public enum EnumFont
        {
            /// <summary>
            /// Enum Font Regular
            /// </summary>
            Regular = 0,

            /// <summary>
            /// Enum Font Gujarati
            /// </summary>
            Gujarati = 1
        }

        /// <summary>
        /// Enum For Setting Excel Cell Alignment [Left, Center, Right]
        /// </summary>
        public enum EnumExcelCellAlignment
        {
            /// <summary>
            /// Enum Excel Cell Left Alignment
            /// </summary>
            Left = -4131,

            /// <summary>
            /// Enum Excel Cell Center Alignment
            /// </summary>
            Center = -4108,

            /// <summary>
            /// Enum Excel Cell Right Alignment
            /// </summary>
            Right = -4152
        }

        /// <summary>
        /// Enum For Setting Cell Activation State in UltraWinGrid
        /// </summary>
        public enum EnumCellActivation
        {
            /// <summary>
            /// Enum Active Only
            /// </summary>
            ActiveOnly = 1,

            /// <summary>
            /// Enum Allow Edit
            /// </summary>
            AllowEdit = 0,

            /// <summary>
            /// Enum Disable
            /// </summary>
            Disable = 2,

            /// <summary>
            /// Enum NoEdit
            /// </summary>
            NoEdit = 3
        }

        /// <summary>
        /// Method For Checking Numeric Or Decimal Value In A Given Control
        /// </summary>
        /// <param name="ctrl">Control Name</param>
        /// <param name="KeyAscii">KeyAscii Value</param>
        /// <param name="NumberofDecimal">Decimal Places </param>
        /// <returns>True If Given KeyAscii Value Is Numeric Or Decimal One</returns>
        public static bool ValNum(object ctrl, int KeyAscii, int NumberofDecimal)
        {
            TextBox txtBox = null;
            object ctlActiveControl;
            int intDotPosition = 0;
            int DigitInFraction;

            ctlActiveControl = ctrl;

            if (KeyAscii == 13)
            {
                return true;
            }

            if (ctlActiveControl.GetType().Name.ToString() == "TextBox")
            {
                txtBox = (TextBox)ctlActiveControl;
                txtBox.SelectedText = "";
                intDotPosition = Microsoft.VisualBasic.Strings.InStr(1, txtBox.Text.ToString(), ".", Microsoft.VisualBasic.CompareMethod.Text);
            }
            if (!((KeyAscii > 47 && KeyAscii < 58) || KeyAscii == 8))
            {
                if (KeyAscii == 46 && NumberofDecimal > 0)
                {
                    if (intDotPosition > 0)
                    {
                        txtBox.SelectionStart = intDotPosition;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                if (NumberofDecimal == 0 || KeyAscii == 8)
                {
                    return true;
                }
                else
                {
                    if (intDotPosition > 0)
                    {
                        DigitInFraction = Microsoft.VisualBasic.Strings.Len(Microsoft.VisualBasic.Strings.Mid(txtBox.Text, intDotPosition + 1, Microsoft.VisualBasic.Strings.Len(txtBox.Text)));
                        if (DigitInFraction >= NumberofDecimal)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }


        /// <summary>
        /// Method For Text Selection
        /// </summary>
        /// <param name="pObj">Ojbect Name</param>
        public static void SelText(object pObj)
        {
            TextBox txtBox = null;
            txtBox = (TextBox)pObj;
            txtBox.SelectionStart = 0;
            txtBox.SelectionLength = txtBox.TextLength;
        }
        /// <summary>
        /// Method For Validate Proper Date
        /// </summary>
        /// <param name="ptxtDate">Date TextBox Control</param>
        /// <returns>True Or False</returns>
        public static bool ValDate(TextBox ptxtDate)
        {
            if (ptxtDate.Text == "")
            {
                return false;
            }

            if (IsDate(ptxtDate.Text) == true)
            {
                ptxtDate.Text = System.Convert.ToString(DateTime.Parse(ptxtDate.Text).ToShortDateString());
            }
            else
            {
                ptxtDate.Text = "";
            }
            return false;
        }


        /// <summary>
        /// Method For Checking Valid Date
        /// </summary>
        /// <param name="anyString">Date String </param>
        /// <returns>True Or False</returns>
        public static bool IsDate(string anyString)
        {
            if (anyString == null)
            {
                anyString = "";
            }
            if (anyString.Length > 0)
            {
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
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method For Checking Valid Time
        /// </summary>
        /// <param name="ptxtTime">Time String</param>
        /// <returns>True Or False</returns>
        public static bool ValTime(TextBox ptxtTime)
        {
            if (ptxtTime.Text == "")
            {
                return false;
            }
            if (IsDate(ptxtTime.Text) == true)
            {
                ptxtTime.Text = String.Format(ptxtTime.Text, "hh:mm tt");
            }
            else
            {
                ptxtTime.Text = "";
            }
            return false;
        }

        /// <summary>
        /// Method For Display Date In [DD/MM/YYYY] Format
        /// </summary>
        /// <param name="pStrDate">Date String</param>
        /// <returns>True Or False</returns>
        public static string DispDate(string pStrDate)
        {
            if (pStrDate == "")
            {
                return "";
            }
            else
            {
                return Convert.ToDateTime(pStrDate).ToString("dd/MM/yyyy");
            }
        }
        public static bool ToBool(String pStr)
        {
            bool boolVal;

            if (pStr == null || pStr.Length == 0)
            {
                return false;
            }
            bool.TryParse(pStr, out boolVal);
            return boolVal;
        }

        public static string DispDate(DateTime? pDateTime)
        {
            if (pDateTime.HasValue == false)
            {
                return "";
            }
            else
            {
                return pDateTime.Value.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Method For Display Date In [HH:MM AM/PM] Format
        /// </summary>
        /// <param name="pStrTime">Time String</param>
        /// <returns>String</returns>
        public static string DispTime(string pStrTime)
        {
            if (pStrTime == "")
            {
                return "";
            }
            else
            {
                return Convert.ToDateTime(pStrTime).ToString("hh:mm tt");
            }
        }
        /// <summary>
        /// Display Time In (HH:MM AM/PM) format
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns></returns>
        public static string DispTime(DateTime? pDateTime)
        {
            if (pDateTime.HasValue == false)
            {
                return "";
            }
            else
            {
                return pDateTime.Value.ToString("hh:mm tt");
            }
        }
        /// <summary>
        /// Convert Date In (dd/MM/yyyy) format
        /// </summary>
        /// <param name="pStrDateTime"></param>
        /// <returns></returns>
        public static DateTime? DTDBDate(String pStrDateTime)
        {
            CultureInfo CI1 = new CultureInfo("en-GB");
            if (pStrDateTime == null || pStrDateTime == "")
            {
                DateTime? DT = null;
                return DT;
            }
            else
            {
                return DateTime.Parse(pStrDateTime, CI1);
            }
        }
        /// <summary>
        /// Convert Date In (dd/MM/yyyy) format
        /// </summary>
        /// <param name="pObjDateTime"></param>
        /// <returns></returns>
        public static DateTime? DTDBDate(Object pObjDateTime)
        {
            if (pObjDateTime == null || pObjDateTime.ToString() == "")
            {
                DateTime? DT = null;
                return DT;
            }
            else
            {
                return DateTime.Parse(pObjDateTime.ToString());
            }
        }
        /// <summary>
        /// Date Checking
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns></returns>
        public static DateTime? DTDBDate(DateTime? pDateTime)
        {
            if (pDateTime == null || pDateTime.ToString() == "")
            {
                DateTime? DT = null;
                return DT;
            }
            else
            {
                return pDateTime;
            }
        }
        /// <summary>
        /// Time Checking
        /// </summary>
        /// <param name="pStrDateTime"></param>
        /// <returns></returns>
        public static DateTime? DTDBTime(String pStrDateTime)
        {
            CultureInfo CI1 = new CultureInfo("en-GB");
            if (pStrDateTime == null || pStrDateTime == "")
            {
                DateTime? DT = null;
                return DT;
            }
            else
            {
                DateTime? DT = DateTime.Parse(pStrDateTime, CI1);
                DateTime? DTRet = new DateTime(1, 1, 1, DT.Value.Hour, DT.Value.Minute, DT.Value.Second);
                return DTRet;
            }
        }
        /// <summary>
        /// Data Access To Business Layer For Time
        /// </summary>
        /// <param name="pObjDateTime"></param>
        /// <returns></returns>
        public static DateTime? DTDBTime(Object pObjDateTime)
        {
            if (pObjDateTime == null || pObjDateTime.ToString() == "")
            {
                DateTime? DT = null;
                return DT;
            }
            else
            {
                DateTime? DT = DateTime.Parse(pObjDateTime.ToString());
                DateTime? DTRet = new DateTime(1, 1, 1, DT.Value.Hour, DT.Value.Minute, DT.Value.Second);
                return DTRet;
            }
        }
        /// <summary>
        /// Data Access To Business Layer For Time
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns></returns>
        public static DateTime? DTDBTime(DateTime? pDateTime)
        {
            if (pDateTime == null || pDateTime.ToString() == "")
            {
                DateTime? DT = null; // new DateTime(1, 1, 1); 
                return DT;
            }
            else
            {
                DateTime? DTRet = new DateTime(1, 1, 1, pDateTime.Value.Hour, pDateTime.Value.Minute, pDateTime.Value.Second);
                return DTRet;
                //return DateTime.Parse(pDateTime.ToString("hh:mm tt"));
            }
        }

        /// <summary>
        /// Method For Display Date In Sql [MM/DD/YYYY] Format
        /// </summary>
        /// <param name="pStrDate">Date String</param>
        /// <returns>String</returns>
        public static string SqlDate(string pStrDate)
        {
            if (pStrDate.Length == 0)
            {
                return "null";
            }
            else
            {
                //return "'" + DateTime.Parse(pStrDate).ToString(new System.Globalization.CultureInfo("en-US", false)).ToString() + "'";
                return "" + DateTime.Parse(pStrDate).ToString("MM/dd/yy") + "";
            }
        } 


        /// <summary>
        /// Method For Display Time In Sql [HH:MM AM/PM] Format
        /// </summary>
        /// <param name="pStrTime">Time String</param>
        /// <returns>String</returns>
        public static string SqlTime(string pStrTime)
        {
            if (pStrTime.Length == 0)
            {
                return "null";
            }
            else
            {
                return Convert.ToDateTime(pStrTime).ToString("hh:mm tt");
            }
        }
        /// <summary>
        /// Method For Display Date In Sql Short [MM/DD/YY] Format
        /// </summary>
        /// <param name="pStrDate">Date String</param>
        /// <returns>String</returns>
        public static string SqlShortDate(string pStrDate)
        {
            if (pStrDate.Length == 0)
            {
                return "null";
            }
            else
            {
                return DateTime.Parse(pStrDate).ToString("MM/dd/yy");
            }
        }
        /// <summary>
        /// Method For Display Time In Sql Short [HH:MM AM/PM] Format
        /// </summary>
        /// <param name="pStrTime">Time String</param>
        /// <returns>String</returns>
        public static string SqlShortTime(string pStrTime)
        {
            if (pStrTime.Length == 0)
            {
                return "null";
            }
            else
            {
                return DateTime.Parse(pStrTime).ToString("hh:mm tt");
            }
        }

        /// <summary>
        /// Method For Display Date In Short [DD/MM/YY] Format
        /// </summary>
        /// <param name="pStrDate">Date String</param>
        /// <returns>String</returns>
        public static string DispShortDate(string pStrDate)
        {
            if (pStrDate.Length == 0)
            {
                return "null";
            }
            else
            {
                return DateTime.Parse(pStrDate).ToString("dd/MM/yy");
            }
        }
        /// <summary>
        /// Method For Display Time In Short [HH:MM AM/PM] Format
        /// </summary>
        /// <param name="pStrTime"></param>
        /// <returns></returns>
        public static string DispShortTime(string pStrTime)
        {
            if (pStrTime.Length == 0)
            {
                return "null";
            }
            else
            {
                return DateTime.Parse(pStrTime).ToString("hh:mm tt");
            }
        }

        public static short ToSmallInt(String pStr)
        {
            return Convert.ToInt16(Microsoft.VisualBasic.Conversion.Val(pStr + ""));
        }
        /// <summary>
        /// Method For Setting Form Key Down Events
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Events E</param>
        public static void FormKeyDownEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (((Form)sender).ActiveControl.GetType().FullName.ToString().IndexOf("UltraGrid") != -1 | ((Form)sender).ActiveControl.Parent.GetType().FullName.IndexOf("UltraGrid") != -1)
                {
                }
                else
                {
                    SendKeys.Send("{TAB}");
                }
            }
        }
        /// <summary>
        /// Method for Searching DataRow From A Given Table
        /// </summary>
        /// <param name="pTab">DataTable </param>
        /// <param name="pStrSearch">Search Field Value</param>
        /// <returns>DataRow</returns>
        public static DataRow SearchText(DataTable pTab, string pStrSearch)
        {
            DataRow DRow;
            object[] Key = new object[pTab.PrimaryKey.GetUpperBound(0) + 1];
            string[] StrSeparatedWord = new string[pTab.PrimaryKey.GetUpperBound(0) + 1];

            if (pStrSearch != "")
            {
                StrSeparatedWord = pStrSearch.Split(',');
            }
            for (int IntI = 0; IntI <= pTab.PrimaryKey.GetUpperBound(0); IntI++)
            {
                Key[IntI] = StrSeparatedWord[IntI];
            }
            DRow = pTab.Rows.Find(Key);
            if (DRow != null)
            {
                return DRow;
            }
            return null;
        }
        /// <summary>
        /// Method for Searching DataRow From A Given Table
        /// </summary>
        /// <param name="pTab">DataTable </param>
        /// <param name="pStrSearch">Search Field Name</param>
        /// <param name="pStrFieldValue">Search Field Value</param>
        /// <returns>DataRow</returns>
        public static DataRow SearchText(DataTable pTab, string pStrSearch, string pStrFieldValue)
        {
            DataRow DRow;
            if (pStrSearch.Length == 0 || pStrFieldValue.Length == 0)
                return null;

            pTab.DefaultView.RowFilter = "";
            pTab.DefaultView.RowFilter = " 1=1 And " + pStrSearch + "=" + pStrFieldValue.ToString();
            if (pTab.DefaultView.Count != 0)
            {
                DRow = pTab.DefaultView[0].Row; 
                pTab.DefaultView.RowFilter = "";
                return DRow;
            }
            pTab.DefaultView.RowFilter = "";
            return null;
        }
        /// <summary>
        /// Method for Searching Column Value From A Given Table
        /// </summary>
        /// <param name="pTab">DataTable</param>
        /// <param name="pStrSearch">Search Field Name</param>
        /// <param name="pStrFieldValue">Search Fields Value</param>
        /// <param name="pStrWhichFieldToReturn">Return Field NAme</param>
        /// <returns>String</returns>
        public static string SearchText(DataTable pTab, string pStrSearch, string pStrFieldValue, string pStrWhichFieldToReturn)
        {
            
            string RetField = "";
            if (pStrSearch.Length == 0 || pStrFieldValue.Length == 0 || pStrWhichFieldToReturn.Length == 0)
                return "";

            string[] StrWord = pStrSearch.Split(','); ;
            string[] StrValue = pStrFieldValue.Split(','); ;
            string StrFilter;
            StrFilter = "1 = 1";
            for (int IntI = 0; IntI < Conversion.Val(StrWord.GetLength(0)); IntI++)
            {
                StrFilter = StrFilter + " And " + StrWord[IntI] + "=" + StrValue[IntI] + "";
            }
            pTab.DefaultView.RowFilter = StrFilter;
            if (pTab.DefaultView.Count != 0)
            {
                RetField = pTab.DefaultView[0][pStrWhichFieldToReturn].ToString().Trim().ToUpper();
                pTab.DefaultView.RowFilter = "";
            }
            pTab.DefaultView.RowFilter = "";
            return RetField;
        }


        public static DataRowView  SearchTextDataRow(DataTable pTab, string pStrSearch, string pStrFieldValue)
        {
            string RetField = string.Empty;
            if (pStrSearch.Length == 0 || pStrFieldValue.Length == 0 )
                return null;

            string[] StrWord = pStrSearch.Split(','); ;
            string[] StrValue = pStrFieldValue.Split(','); ;
            string StrFilter;
            StrFilter = "1 = 1";
            pTab.DefaultView.RowFilter = string.Empty;
            for (int IntI = 0; IntI < Conversion.Val(StrWord.GetLength(0)); IntI++)
            {
                StrFilter = StrFilter + " And " + StrWord[IntI] + "=" + StrValue[IntI] + "";
            }
            pTab.DefaultView.RowFilter = StrFilter;
            if (pTab.DefaultView.Count != 0)
            {
                return pTab.DefaultView[0];                 
            }
            pTab.DefaultView.RowFilter = string.Empty;
            return null;
        }


        /// <summary>
        /// Method for Searching Column And Return Bool Value From A Given Table
        /// </summary>
        /// <param name="pTab">DataTable</param>
        /// <param name="pStrFieldName">Search Field Name</param>
        /// <param name="pStrFieldValue">Search Fields Value</param>
        /// <param name="pRetBoolean">Boolean Return</param>
        /// <returns>Boolean</returns>
        public static Boolean SearchText(DataTable pTab, string pStrFieldName, string pStrFieldValue, Boolean pRetBoolean)
        {
            Boolean BlnRetVal = false;
            string[] StrWord = pStrFieldName.Split(','); ;
            string[] StrValue = pStrFieldValue.Split(','); ;
            string StrFilter;
            StrFilter = "1=1";
            for (int IntI = 0; IntI < Conversion.Val(StrWord.GetLength(0)); IntI++)
            {
                StrFilter = StrFilter + " And " + StrWord[IntI] + "=" + StrValue[IntI] + "";
            }
            //  pTab.DefaultView.RowFilter = StrFilter;
            if (pTab.DefaultView.Count == 0)
            {
                BlnRetVal = false;
            }
            else
            {
                BlnRetVal = true;
            }
            pTab.DefaultView.RowFilter = "";
            return BlnRetVal;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTab">Data Table Name</param>
        /// <param name="pStrCondition">Condition To Apply On View To Search Record</param>
        /// <param name="pStrFiledToReturn">Which Field To Return After Filtering</param>
        /// <param name="pNoRec">No. Of Records Found</param>
        /// <returns></returns>
        public static String SearchText(DataTable pTab, String pStrCondition, String pStrFiledToReturn,int pNoRec)
        {            
            pTab.DefaultView.RowFilter = pStrCondition;
            if (pTab.DefaultView.Count == 0)
            {
                return "";
            }
            else
            {
                return pTab.DefaultView[0][pStrFiledToReturn].ToString().Trim(); 
            }             
        }
        public static string SearchTextLike(DataTable pTab, string pStrSearch, string pStrFieldValue, string pStrWhichFieldToReturn)
        {
            string RetField = "";
            if (pStrSearch.Length == 0 || pStrFieldValue.Length == 0 || pStrWhichFieldToReturn.Length == 0)
                return "";
            if (pTab == null || pTab.Rows.Count == 0)
            {
                return "";
            }
            //string[] StrWord = pStrSearch.Split(','); ;
            //string[] StrValue = pStrFieldValue.Split(','); ;
            string StrFilter;
            StrFilter = "1=1";
            //for (int IntI = 0; IntI < Conversion.Val(StrWord.GetLength(0)); IntI++)
            //{
            //    StrFilter = StrFilter + " And " + StrWord[IntI] + "=" + StrValue[IntI] + "";
            //}
            StrFilter = StrFilter + " AND ','+" + pStrSearch + "+',' LIKE '%," + pStrFieldValue + ",%'";

            pTab.DefaultView.RowFilter = StrFilter;
            if (pTab.DefaultView.Count != 0)
            {
                RetField = pTab.DefaultView[0][pStrWhichFieldToReturn].ToString().Trim().ToUpper();
                pTab.DefaultView.RowFilter = "";
            }
            pTab.DefaultView.RowFilter = "";
            return RetField;
        }

        /// <summary>
        /// Find Return Value
        /// </summary>
        /// <param name="pDTab"></param>
        /// <param name="pStrSearch"></param>
        /// <param name="pStrFieldToRet"></param>
        /// <returns></returns>
        public static string FindRow(DataTable pDTab, String pStrSearch, String pStrFieldToRet)
        {
            DataRow DRow;
            Object[] ObjKey = new Object[pDTab.PrimaryKey.GetUpperBound(0) + 1];
            String[] StrSepWord = new String[pDTab.PrimaryKey.GetUpperBound(0) + 1];

            if (pStrSearch != "")
            {
                StrSepWord = pStrSearch.Split(',');
            }
            for (int intI = 0; intI <= pDTab.PrimaryKey.GetUpperBound(0); intI++)
            {
                ObjKey[intI] = StrSepWord[intI];
            }
            DRow = pDTab.Rows.Find(ObjKey);
            if (DRow != null)
            {
                return DRow[pStrFieldToRet].ToString();
            }
            return "";
        }

        /// <summary>
        /// Method For Setting Form Properties [Style,KeyPreview,StartUp Position]
        /// </summary>
        /// <param name="pFrm">Form Name For Setting ParaMeter </param>
        public static void frmGenSet(Form pFrm)
        {
            Form Frm = pFrm;
            Frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            Frm.KeyPreview = true;
            Frm.StartPosition = FormStartPosition.CenterScreen;
            Frm.WindowState = FormWindowState.Normal;

            Frm.MaximizeBox = false;
            Frm.MinimizeBox = false;
            Frm.ControlBox = false;
        }
        public static void FullView(Form pFrm)
        {
            pFrm.Height = pFrm.Parent.Height - 100;
            pFrm.Width = pFrm.Parent.Width - 100;
        }
        /// <summary>
        /// Method For Resize The Form
        /// </summary>
        /// <param name="pFrm">Form Name For Resizing </param>
        public static void frmResize(Form pFrm)
        {
            pFrm.WindowState = FormWindowState.Normal;
        }
        /// <summary>
        /// Method for Validating String Value
        /// </summary>
        /// <param name="Str">String Value</param>
        /// <returns>Double</returns>
        public static double Val(String Str)
        {
            return Convert.ToDouble(Microsoft.VisualBasic.Conversion.Val(Str));
        }
        /// <summary>
        /// Method for Validating Object Value
        /// </summary>
        /// <param name="Str">Object Str</param>
        /// <returns>Double </returns>
        public static double Val(Object Str)
        {
            return Convert.ToDouble(Microsoft.VisualBasic.Conversion.Val(Str));
        }
        /// <summary>
        /// Method For Remove Leading & Trailing Blank Space From String
        /// </summary>
        /// <param name="Str">String Str</param>
        /// <returns>String</returns>
        public static string Trim(String Str)
        {
            return Microsoft.VisualBasic.Strings.Trim((Str));
        }

        public static int ToBoolToInt(Object pObj)
        {             
            if (pObj == null) return 0;
            if (pObj.ToString().Length == 0) return 0;
            if (((Boolean)pObj) == true) return 1;
            else return 0;             
        }

        public static int ToBoolToInt(String pStr)
        {
            if (pStr == null) return 0;
            if ((Convert.ToBoolean(pStr) == true)) return 1;
            else return 0;
        }
         

        public static int ToInt(String pStr)
        {
            return Convert.ToInt32(Microsoft.VisualBasic.Conversion.Val(pStr + ""));
        }

        public static int? ToIntNullable(String pStr)
        {
            return Convert.ToInt32(Microsoft.VisualBasic.Conversion.Val(pStr + ""));
        }

        public static string ToInt(String pStr, Boolean BlnIntThenStr)
        {
            return ToInt(pStr).ToString(); 
        }

        public static int ToInt(Object pObj)
        {
            return Convert.ToInt32(Microsoft.VisualBasic.Conversion.Val(pObj  + ""));
        }


        public static string ToInt(Object pObj, Boolean BlnIntThenStr)
        {
            return ToInt(pObj).ToString(); 
        }
                
        public static Int64 ToInt64(String pStr)
        {
            return Convert.ToInt64(Microsoft.VisualBasic.Conversion.Val(pStr));
        }

        public static Int64 ToInt64(Object pObj) 
        {
            return Convert.ToInt64(Microsoft.VisualBasic.Conversion.Val("0" + pObj.ToString())); 
        }
        public static Decimal ToDec(String pStr)
        {
            decimal decVal;

            if (pStr == null || pStr.Length == 0)
            {
                return 0;
            }
            decimal.TryParse(pStr, out decVal);
            return decVal;
        }
        public static Decimal ToDecimal(Object pObj)
        {
            decimal decVal; 

            if (pObj == null || pObj.ToString().Length == 0)
            {
                return 0;
            }
            decimal.TryParse(pObj.ToString(), out decVal);
            return decVal; 
        }

        public static string ToDecimal(Object pObj, Boolean BlnDblThenStr)
        {
            return ToDecimal(pObj).ToString(); 
        }

        public static Decimal ToDecimal(String pStr)
        {
            //return Convert.ToDecimal("0" + pStr);

            decimal  decVal;

            if (pStr == null || pStr.Length == 0)
            {
                return 0;
            }
            decimal.TryParse(pStr, out decVal);
            return decVal; 
        }

        public static string ToDecimal(String pStr, Boolean BlnDblThenStr)
        {
            return ToDecimal(pStr).ToString(); 
        }

        public static double ToDouble(String pStr)
        {
            double dblVal;

            if (pStr == null || pStr.Length == 0)
            {
                return 0;
            }
            double.TryParse(pStr, out dblVal);
            return dblVal; 
        }
        public static DateTime? DTDBDateTime(String pStrDateTime)
        {
            if (pStrDateTime == null || pStrDateTime == "")
            {
                DateTime? DT = null;
                return DT;
            }
            else
            {
                DateTime? DT = DateTime.Parse(pStrDateTime);
                return DT;
            }
        }
        public static string ToDouble(String pStr, Boolean BlnDblThenStr)
        {
            return ToDouble(pStr).ToString();
        }
        
        public static double ToDouble(Object pObj)
        {
            double dblVal;

            if (pObj == null || pObj.ToString().Length  == 0)
            {
                return 0;
            }
            double.TryParse(pObj.ToString(), out dblVal);
            return dblVal; 
        }

        public static string  ToDouble(Object pObj, Boolean BlnDblThenStr)
        {
            return ToDouble(pObj).ToString(); 
        }

        public static string ToString(Object pObj)
        {
            if (pObj == null) return "";
            return string.IsNullOrEmpty(pObj.ToString()) ? "" : pObj.ToString();
        }

        public static char ToChar(Object pObj)
        {
            return string.IsNullOrEmpty(pObj.ToString()) ? Convert.ToChar(""): Convert.ToChar(pObj.ToString());
        }

        public static Int16? ToInt16Nullable(Object pObj)
        {
            return Convert.ToInt16(Microsoft.VisualBasic.Conversion.Val("0" + pObj.ToString()));
        }

        public static Int16 ToInt16(Object pObj)
        {
            return Convert.ToInt16(Microsoft.VisualBasic.Conversion.Val("0" + pObj.ToString()));
        }

        public static string ToInt16(Object pObj, Boolean BlnDblThenStr)
        {
            return ToInt16(pObj).ToString(); 
        }

        public static Int16? ToInt16Nullable(String pStr)
        {
            return Convert.ToInt16(Microsoft.VisualBasic.Conversion.Val(pStr + string.Empty));
        }

        public static Int16 ToInt16(String pStr)
        {
            return Convert.ToInt16(Microsoft.VisualBasic.Conversion.Val(pStr));
        }

        public static string ToInt16(String pStr, Boolean BlnDblThenStr)
        {
            return ToInt16(pStr).ToString(); 
        }
        public static bool ToBoolean(String pStr)
        {
            if (pStr.ToUpper() == "TRUE") return true;
            if (pStr.ToUpper() == "FALSE") return false;
            return Convert.ToBoolean(Microsoft.VisualBasic.Conversion.Val(pStr + string.Empty));
        }
        
        public static string Devide(string pStr1, string pStr2)
        {
            if (pStr2 != "" && pStr1 != "" && Convert.ToDouble(pStr2) > 0)
            {
                return Convert.ToString((Convert.ToDouble(pStr1) / Convert.ToDouble(pStr2)));
            }
            else
            {
                return "";
            }
        }
        public static double Devide(double pStr1, double pStr2)
        {
            if (pStr1 > 0 && pStr2 > 0)
            {
                return Convert.ToDouble(pStr1 / pStr2);
            }
            else
            {
                return 0;
            }
        }

        public static string Percent(string pStr1, string pStr2)
        {
            if (pStr2 != "" && pStr1 != "" && Convert.ToDouble(pStr2) > 0)
            {
                return Convert.ToString((Convert.ToDouble(pStr1) / Convert.ToDouble(pStr2)) * 100);
            }
            else
            {
                return "";
            }
        }

        public static Double Percent(Double pDouValue1, Double pDouValue2)
        {
            if (pDouValue2 > 0)
            {
                return Convert.ToDouble((pDouValue1 / pDouValue2) * 100);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// Method For Printing Any Message
        /// </summary>
        /// <param name="Str">String Message</param>
        public static void PrnMsg(string Str)
        {
           // Message(Str);
            GSql.GIntMesgMode = 1;
            GSql.GStrMsgCaption = Str;
        }
       
        /// <summary>
        /// Method For Confirmation Warning
        /// </summary>
        /// <returns>DialogBox Results </returns>
        public static DialogResult Conf()
        {
            return MessageBox.Show("Are You Sure ? ", GSql.GStrMsgHeading, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
        /// <summary>
        /// Method For Confirmation Warning
        /// </summary>
        /// <param name="pStrDispMsg">Message To Display</param>
        /// <returns>DialogBox Results</returns>
        public static DialogResult Conf(string pStrDispMsg)
        {
            return MessageBox.Show(pStrDispMsg, GSql.GStrMsgHeading, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
        /// <summary>
        /// Method For Confirmation Warning
        /// </summary>
        /// <param name="pStrDispMsg">Message To Display</param>
        /// <param name="pStrDispValue">Display Value</param>
        /// <returns>DialogBox Results</returns>
        public static DialogResult Conf(string pStrDispMsg, string pStrDispValue)
        {
            return MessageBox.Show(pStrDispMsg + pStrDispValue, GSql.GStrMsgHeading, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

       
         

        /// <summary>
        /// Method For Display Message 
        /// </summary>
        /// <param name="pStrMessage">Message To Display</param>
        public static void Message(string pStrMessage)
        {
            //MessageBox.Show(pStrMessage.ToString(), GSql.GStrMsgHeading);
            //GSql.GIntMesgMode = 1;
            //GSql.GStrMsgCaption = pStrMessage;
            MessageBox.Show(pStrMessage.ToString(), GSql.GStrMsgHeading);
        }
        /// <summary>
        /// Method For Display Message 
        /// </summary>
        /// <param name="pStrMessage">Message To Display</param>
        /// <param name="pStrMessageCaption">Message Caption</param>
        public static void Message(string pStrMessage, string pStrMessageCaption)
        {
            MessageBox.Show(pStrMessage.ToString(), pStrMessageCaption);
        }
        /// <summary>
        /// Method For Display Message 
        /// </summary>
        /// <param name="pStrMessage">Message To Display</param>
        /// <param name="pStrMessageCaption">Message Caption</param>
        /// <param name="pMsgBoxIcon">Message Icon</param>
        public static void Message(string pStrMessage, string pStrMessageCaption, MessageBoxIcon pMsgBoxIcon)
        {
            MessageBox.Show(pStrMessage.ToString(), pStrMessageCaption, MessageBoxButtons.OK, pMsgBoxIcon);
        }
        /// <summary>
        /// Method For Display Message 
        /// </summary>
        /// <param name="pStrMessage">Message To Display</param>
        /// <param name="pStrMessageCaption">Message Caption</param>
        /// <param name="pMsgBoxButton">Message Button</param>
        /// <param name="pMsgBoxIcon">Message Icon</param>
        public static void Message(string pStrMessage, string pStrMessageCaption, MessageBoxButtons pMsgBoxButton, MessageBoxIcon pMsgBoxIcon)
        {
            MessageBox.Show(pStrMessage.ToString(), pStrMessageCaption, pMsgBoxButton, pMsgBoxIcon);
        }
        /// <summary>Method For Extracting Left Side Part Of String
        /// Get Left Side String Of A Given Length
        /// </summary>
        /// <param name="pStr">Original String</param>
        /// <param name="pLength">Number Of Characters To Extract</param>
        /// <returns>String</returns>
        public static string Left(String pStr, int pLength)
        {
            return Microsoft.VisualBasic.Strings.Left(pStr, pLength);
        }
        /// <summary>Method For Extracting Right Side Part Of String
        /// Get Right Side String Of A Given Length
        /// </summary>
        /// <param name="pStr">Original String</param>
        /// <param name="pLength">Number Of Characters To Extract</param>
        /// <returns>String</returns>

        public static string Right(String pStr, int pLength)
        {
            return Microsoft.VisualBasic.Strings.Right(pStr, pLength);
        }

        public static char Chr(int pIntChar)
        {
            return Microsoft.VisualBasic.Strings.Chr(pIntChar);
        }
        /// <summary>
        /// To Format String With Object Expression
        /// </summary>
        /// <param name="Expression"></param>
        /// <param name="Style"></param>
        /// <returns></returns>
        public static string Format(object Expression, string Style)
        {
            if (Style.IndexOf('#') != -1)
            {
                if (Style.IndexOf('.') != -1)
                {
                    double douval = ToDouble(Expression);
                    return Microsoft.VisualBasic.Strings.Format(douval, Style);
                }
                else
                {
                    Int64 int64val = ToInt64(Convert.ToString(Expression));
                    return Microsoft.VisualBasic.Strings.Format(int64val, Style);
                }
            }
            else
            {
                return Microsoft.VisualBasic.Strings.Format(Expression, Style);
            }
        }

        public static  double FormatToDbl(object Expression, string Style)
        {
            return ToDouble(Format(Expression, Style));
        }

        public static string AdjStr(string pStrStringToAdjust, int pIntWidth, string pStrType, bool pBlnPrintzero)
        {
            int IntLength;
            IntLength = pStrStringToAdjust.Trim().Length;
            switch (pStrType)
            {
                case "C":
                case "D":
                    if ((IntLength > pIntWidth))
                    {
                        return pStrStringToAdjust.Trim().Substring(0, pIntWidth);
                    }
                    else
                    {
                        return pStrStringToAdjust.Trim() + new string(' ', pIntWidth - IntLength);
                    }
                case "N":
                    if ((Microsoft.VisualBasic.Conversion.Val(pStrStringToAdjust) == 0) && (pBlnPrintzero == true))
                    {
                        return new string(' ', pIntWidth);
                    }
                    else if (IntLength > pIntWidth)
                    {
                        return pStrStringToAdjust.Trim().Substring(0, pIntWidth);
                    }
                    else
                    {
                        return new string(' ', pIntWidth - IntLength) + pStrStringToAdjust.Trim();
                    }
            }
            return "";
        }
        public static void PrintMsgE(string strMessage)
        {
            gIntmsgMod = 1;
            gstrMessage = strMessage;
            gMsgColor = System.Drawing.Color.Red;
        }
        public static bool IsAlphaNumeric(string pStr)
        {
            bool BlnValid = true ;

            String StrAN = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i <  pStr.Length; i++)
            {
                if (StrAN.IndexOf(pStr[i]) < 0)
                {
                    BlnValid = false;
                    break;
                }
            }
            return BlnValid;
        }
        public static void FormKeyDownEventUsrControl(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (((UserControl)sender).ActiveControl.GetType().FullName.ToString().IndexOf("UTC.UTCGrid") != -1 | ((UserControl)sender).ActiveControl.Parent.GetType().FullName.IndexOf("UTC.UTCGrid") != -1)
                {
                }
                else
                {
                    SendKeys.Send("{TAB}");
                }
            }
        }
        public static string InputBox(string pStrPrompt)
        {
            return Microsoft.VisualBasic.Interaction.InputBox(pStrPrompt, GSql.GStrMsgCaption, "", 300, 300);
        }
        public static string GetQuateList(String pStr)
        {
            if (pStr.Length == 0)
            {
                return "";
            }

            string Lot = "'" + pStr.Replace(",", "','") + "'";
            return Lot;
        }

    }
}
    