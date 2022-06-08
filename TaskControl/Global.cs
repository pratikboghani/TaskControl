using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTC;
using Val = BusLib.Validation.BOValidation;
using Glb = DataLib.OperationSql;
using Infragistics.Win;
using System.Drawing;
using System.Windows.Forms;
using TaskControl;

namespace TaskControl
{
   public static class Global
    {
        public static int setFormWidth(UTCGrid _StGrid)
        {
            int intSize = 0;
            foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn UCol in _StGrid.DisplayLayout.Bands[0].Columns)
            {
                if (UCol.Hidden == false)
                {
                    intSize += UCol.Width;
                }
            }
            return intSize + 70;
        }

        public static void SetGridColumns(UTCGrid ultGrid, DataTable dt, int IntBand = 0)
        {
            string StrGroupKey, StrOldGroupKey = string.Empty;
            int intHeaderVisiblePosition = 0;
            string StrFirUTCol = "", StrSecCol = "", StrThirdCol = "";
            Boolean IsHide = false;
            Boolean IsLock = false;

            for (int IntI = 0; IntI <= ultGrid.DisplayLayout.Bands[IntBand].Columns.Count - 1; IntI++)
            {
                ultGrid.DisplayLayout.Bands[IntBand].Columns[IntI].Hidden = true;
            }

            ultGrid.DisplayLayout.Bands[IntBand].Groups.Clear();
            ultGrid.DisplayLayout.Bands[0].Summaries.Clear();
            ultGrid.DisplayLayout.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False;

            foreach (DataRow DRow in dt.Rows)
            {
                //Val.Message(DRow["NAME"].ToString());
                if (ultGrid.DisplayLayout.Bands[IntBand].Columns.IndexOf(DRow["NAME"].ToString()) >= 0)
                {
                    Infragistics.Win.HAlign H = (Infragistics.Win.HAlign)(Val.ToInt(DRow["HALIGN"]));
                    System.Drawing.Color c;
                    Infragistics.Win.UltraWinGrid.ColumnStyle K;
                    if (DRow["FORECOLOR"].ToString().Length != 0)
                    {
                        c = System.Drawing.Color.FromName((DRow["FORECOLOR"]).ToString());
                    }
                    else
                    {
                        c = System.Drawing.Color.Empty;
                    }
                    if (DRow["COLUMNSTYLE"].ToString().Length != 0)
                    {
                        K = (Infragistics.Win.UltraWinGrid.ColumnStyle)(Enum.Parse(typeof(Infragistics.Win.UltraWinGrid.ColumnStyle), DRow["COLUMNSTYLE"].ToString()));
                    }
                    else
                    {
                        K = Infragistics.Win.UltraWinGrid.ColumnStyle.Default;
                    }

                    if (DRow["GROUPKEY"].ToString().Length != 0)
                    {
                        StrGroupKey = DRow["GROUPKEY"].ToString();
                    }
                    else
                    {
                        StrGroupKey = " ";
                    }
                    if (Convert.ToBoolean(DRow["ISHIDDEN"].ToString()) == true)
                    {
                        IsHide = true;
                    }
                    else
                    {
                        IsHide = false;
                    }
                    if (Convert.ToBoolean(DRow["LOCK"].ToString()) == true)
                    {
                        IsLock = true;
                    }

                    else
                    {
                        IsLock = false;
                    }
                    if (StrGroupKey != StrOldGroupKey && IntBand == 0)
                    {
                        if (!ultGrid.DisplayLayout.Bands[IntBand].Groups.Exists(StrGroupKey))
                        {
                            ultGrid.CreateGroup(StrGroupKey, intHeaderVisiblePosition, StrGroupKey, intHeaderVisiblePosition);
                            ultGrid.DisplayLayout.Bands[IntBand].Groups[StrGroupKey].Header.Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                            ultGrid.DisplayLayout.Bands[IntBand].Groups[StrGroupKey].Header.Appearance.ForeColor = c;
                            intHeaderVisiblePosition++;
                        }
                        StrOldGroupKey = StrGroupKey;
                    }

                    if (IntBand != 0) StrGroupKey = "";
                    if ((DRow["NAME"].ToString() == "C_NAME") && IntBand == 1)
                    {
                        //Val.Message(ultGrid.DisplayLayout.Bands[IntBand].Index.ToString());
                    }
                    if (IntBand != 0)
                    {
                        ultGrid.ColumnSet(ultGrid.DisplayLayout.Bands[IntBand].Key, DRow["NAME"].ToString(), DRow["DISPNAME"].ToString(), Val.ToInt(DRow["SIZE"]), DRow["FORMAT"].ToString(), IsHide, K,
                        Val.ToInt(DRow["ORD"]), Infragistics.Win.DefaultableBoolean.False, System.Drawing.Color.Empty, c, H, 0, IsLock);
                    }
                    else
                    {
                        ultGrid.ColumnSet(StrGroupKey, ultGrid.DisplayLayout.Bands[IntBand].Key, DRow["NAME"].ToString(), DRow["DISPNAME"].ToString(), Val.ToInt(DRow["SIZE"]), DRow["FORMAT"].ToString(), IsHide, K,
                        Val.ToInt(DRow["ORD"]), Infragistics.Win.DefaultableBoolean.False, System.Drawing.Color.Empty, c, H, 0, IsLock);
                    }

                    #region Summary

                    if (DRow["Formula"].ToString().ToUpper() != "")
                    {
                        foreach (UltraGridColumn UltCol in ultGrid.DisplayLayout.Bands[0].Columns)
                        {
                            if (UltCol.Key.ToString().ToUpper() == DRow["NAME"].ToString().ToUpper())
                            {
                                if (DRow["Formula"].ToString().ToUpper() == "SUM")
                                {
                                    Global.SetGrdSummaryAppearance(ultGrid, UltCol.Format, SummaryType.Sum, UltCol);
                                }
                                else if (DRow["Formula"].ToString().ToUpper() == "COUNT")
                                {
                                    Global.SetGrdSummaryAppearance(ultGrid, UltCol.Format, SummaryType.Count, UltCol);
                                }
                                else if (DRow["Formula"].ToString().ToUpper() == "MINIMUM")
                                {
                                    Global.SetGrdSummaryAppearance(ultGrid, UltCol.Format, SummaryType.Minimum, UltCol);
                                }
                                else if (DRow["Formula"].ToString().ToUpper() == "MAXIMUM")
                                {
                                    Global.SetGrdSummaryAppearance(ultGrid, UltCol.Format, SummaryType.Maximum, UltCol);
                                }
                                else if (DRow["Formula"].ToString().ToUpper() == "AVERAGE")
                                {
                                    Global.SetGrdSummaryAppearance(ultGrid, UltCol.Format, SummaryType.Average, UltCol);
                                }
                                else if (DRow["Formula"].ToString().Substring(0, 7).ToUpper() == "AVGRATE")
                                {
                                    //I_CARAT,RATE
                                    try
                                    {
                                        StrFirUTCol = DRow["Formula"].ToString().Split(',')[1].Split('#')[0].ToString();
                                        StrSecCol = DRow["Formula"].ToString().Split(',')[1].Split('#')[1].ToString();
                                        if (ultGrid.DisplayLayout.Bands[0].Columns.Exists(StrFirUTCol) == true && ultGrid.DisplayLayout.Bands[0].Columns.Exists(StrSecCol) == true)
                                        {
                                            Global.SetGrdSummaryAppearance(ultGrid, UltCol.Format, SummaryType.Custom, UltCol, new CustSummaryCalcAvgRate(StrFirUTCol, StrSecCol));
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }

                                }
                                else if (DRow["Formula"].ToString().Substring(0, 6).ToUpper() == "AVGPER")
                                {
                                    //I_CARAT,O_RATE
                                    try
                                    {
                                        StrFirUTCol = DRow["Formula"].ToString().Split(',')[1].Split('#')[0].ToString();
                                        StrSecCol = DRow["Formula"].ToString().Split(',')[1].Split('#')[1].ToString();
                                        if (ultGrid.DisplayLayout.Bands[0].Columns.Exists(StrFirUTCol) == true && ultGrid.DisplayLayout.Bands[0].Columns.Exists(StrSecCol) == true)
                                        {
                                            Global.SetGrdSummaryAppearance(ultGrid, UltCol.Format.ToString(), SummaryType.Custom, UltCol, new CustSummaryCalcAvgRate(StrFirUTCol, StrSecCol, CustSummaryCalcAvgRate.CalcTyp.AvgPer));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Val.Message(ex.Message.ToString(), Glb.GStrMsgCaption, MessageBoxIcon.Error);
                                    }
                                }
                                else if (DRow["Formula"].ToString().Substring(0, 6).ToUpper() == "AVGDIS")
                                {
                                    //I_CARAT,O_RATE,RATE
                                    try
                                    {
                                        StrFirUTCol = DRow["Formula"].ToString().Split(',')[1].Split('#')[0].ToString();
                                        StrSecCol = DRow["Formula"].ToString().Split(',')[1].Split('#')[1].ToString();
                                        StrThirdCol = DRow["Formula"].ToString().Split(',')[1].Split('#')[2].ToString();
                                        if (ultGrid.DisplayLayout.Bands[0].Columns.Exists(StrFirUTCol) == true && ultGrid.DisplayLayout.Bands[0].Columns.Exists(StrSecCol) == true && ultGrid.DisplayLayout.Bands[0].Columns.Exists(StrThirdCol) == true)
                                        {
                                            Global.SetGrdSummaryAppearance(ultGrid, UltCol.Format.ToString(), SummaryType.Custom, UltCol, new CustSummaryCalcAvgRate(StrFirUTCol, StrSecCol, StrThirdCol, CustSummaryCalcAvgRate.CalcTyp.Disc));
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }

                            }
                        }
                    }


                    #endregion

                }
            }
        }

        public static void SetGrdSummaryAppearance(UTCGrid ultGrid, string pStrFormat, SummaryType summaryType, UltraGridColumn UltCol)
        {
            SetGrdSummaryAppearance(ultGrid, pStrFormat, summaryType, UltCol, null);
        }
        public static void SetGrdSummaryAppearance(UTCGrid ultGrid, string pStrFormat, SummaryType summaryType, UltraGridColumn UltCol, ICustomSummaryCalculator iCustomSumCalc)
        {
            SummarySettings summary = new SummarySettings();

            ultGrid.DisplayLayout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;

            if (summaryType == SummaryType.Custom)
            {
                //summary = ultGrid.DisplayLayout.Bands[0].Summaries.Add(UltCol.Key.ToString(), summaryType, iCustomSumCalc, UltCol, SummaryPosition.Left, UltCol);
                summary = ultGrid.DisplayLayout.Bands[0].Summaries.Add(UltCol.Key.ToString(), summaryType, iCustomSumCalc, UltCol, SummaryPosition.UseSummaryPositionColumn, UltCol);
            }
            else
            {
                summary = ultGrid.DisplayLayout.Bands[0].Summaries.Add(UltCol.Key.ToString(), summaryType, UltCol);
            }
            summary.DisplayFormat = "{0:" + Convert.ToString(pStrFormat) + "}";
            summary.Appearance.ForeColor = Color.Maroon;
            summary.Appearance.BackColor = SystemColors.Control;
            summary.Appearance.FontData.SizeInPoints = 8;
            summary.Appearance.FontData.Bold = DefaultableBoolean.True;
            summary.Appearance.TextVAlign = VAlign.Middle;
            summary.Appearance.TextHAlign = HAlign.Right;
            summary.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
        }
    }
}
