using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace TaskControl
{
    class CustSummaryCalcAvgRate : ICustomSummaryCalculator
    {
        #region Var

        private readonly object _ObjColumnCarat = "";
        private readonly object _ObjColumnRate = "";
        private object _ObjColumnORate = "";
        private bool _BlnCalOnAmt = false;
        private readonly CalcTyp _CalcTyp = CalcTyp.Rate;
        double dblTAmt = 0, dblTOAmt = 0, dblTCrt = 0, dblTORate = 0, dblTRate = 0;

        /// <summary <MB>>
        /// Default Calculate On Rate,
        /// </summary <MB>>
        public enum CalcTyp
        {
            Rate,
            Amount,
            Disc,
            Avg,
            AvgPer
        }

        #endregion

        public CustSummaryCalcAvgRate(object pStrColumnCarat, object pStrColumnRateAmt, CalcTyp pCalcTyp)
        {
            _ObjColumnCarat = pStrColumnCarat;
            _ObjColumnRate = pStrColumnRateAmt;
            _CalcTyp = pCalcTyp;
        }

        public CustSummaryCalcAvgRate(object pStrColumnCarat, object pStrColumnORate, object pStrColumnRate, CalcTyp pCalcTyp)
        {
            _ObjColumnCarat = pStrColumnCarat;
            _ObjColumnORate = pStrColumnORate;
            _ObjColumnRate = pStrColumnRate;
            _CalcTyp = pCalcTyp;
        }

        public CustSummaryCalcAvgRate(object pStrColumnCarat, object pStrColumnRate)
        {
            _ObjColumnCarat = pStrColumnCarat;
            _ObjColumnRate = pStrColumnRate;
            _CalcTyp = CalcTyp.Rate;
        }

        public void AggregateCustomSummary(SummarySettings summarySettings, UltraGridRow row)
        {
            object dblCarat = 0, dblRate = 0, dblORate = 0;


            //if (_CalcTyp == CalcTyp.Avg || _CalcTyp == CalcTyp.AvgPer)
            if (_CalcTyp == CalcTyp.Avg)
            {
                // Here is where we process each row that gets passed in.
                dblCarat = _ObjColumnCarat;
                dblRate = _ObjColumnRate;
            }
            else if (_CalcTyp == CalcTyp.AvgPer)
            {
                dblCarat = row.GetCellValue(summarySettings.SourceColumn.Band.Columns[Convert.ToString(_ObjColumnCarat)]);
                dblRate = row.GetCellValue(summarySettings.SourceColumn.Band.Columns[Convert.ToString(_ObjColumnRate)]);
            }

            else if (_CalcTyp == CalcTyp.Disc)
            {
                dblCarat = row.GetCellValue(summarySettings.SourceColumn.Band.Columns[Convert.ToString(_ObjColumnCarat)]);
                dblRate = row.GetCellValue(summarySettings.SourceColumn.Band.Columns[Convert.ToString(_ObjColumnRate)]);
                dblORate = row.GetCellValue(summarySettings.SourceColumn.Band.Columns[Convert.ToString(_ObjColumnORate)]);
            }
            else
            {
                dblCarat = row.GetCellValue(summarySettings.SourceColumn.Band.Columns[Convert.ToString(_ObjColumnCarat)]);
                dblRate = row.GetCellValue(summarySettings.SourceColumn.Band.Columns[Convert.ToString(_ObjColumnRate)]);
            }


            // Handle null values
            if (dblCarat is DBNull || dblRate is DBNull)
            {
                return;
            }

            try
            {
                switch (_CalcTyp)
                {
                    case CalcTyp.Rate:
                        dblTAmt += Convert.ToDouble(dblCarat) * Convert.ToDouble(dblRate);
                        dblTCrt += Convert.ToDouble(dblCarat);
                        break;
                    case CalcTyp.Amount:
                    case CalcTyp.Avg:
                    case CalcTyp.AvgPer:
                        dblTAmt += Convert.ToDouble(dblRate);
                        dblTCrt += Convert.ToDouble(dblCarat);
                        break;
                    //case CalcTyp.Amount:
                    //    this.dblTAmt += Convert.ToDouble(dblRate);
                    //    this.dblTCrt += Convert.ToDouble(dblCarat);
                    //    break;
                    //case CalcTyp.Avg:
                    //    this.dblTAmt += Convert.ToDouble(dblRate);
                    //    this.dblTCrt += Convert.ToDouble(dblCarat);
                    //    break;
                    //case CalcTyp.AvgPer:
                    //    this.dblTAmt += Convert.ToDouble(dblRate);
                    //    this.dblTCrt += Convert.ToDouble(dblCarat);
                    //    break;
                    case CalcTyp.Disc:
                        dblTCrt += Convert.ToDouble(dblCarat);
                        dblTAmt += Convert.ToDouble(dblCarat) * Convert.ToDouble(dblRate);
                        dblTOAmt += Convert.ToDouble(dblCarat) * Convert.ToDouble(dblORate);
                        break;
                }

            }
            catch (Exception)
            {
                Debug.Assert(false, "Exception thrown while trying to convert cell's value to double!");
            }
        }

        public void BeginCustomSummary(SummarySettings summarySettings, RowsCollection rows)
        {
            dblTAmt = 0;
            dblTOAmt = 0;
            dblTCrt = 0;
        }

        public object EndCustomSummary(SummarySettings summarySettings, RowsCollection rows)
        {
            double dblRetVal = 0;
            switch (_CalcTyp)
            {
                case CalcTyp.Rate:
                case CalcTyp.Amount:
                    if (dblTAmt != 0 && dblTCrt != 0)
                    {
                        dblRetVal = Math.Round((dblTAmt / dblTCrt), 2);
                    }
                    break;
                case CalcTyp.Avg:
                    if (dblTAmt != 0 && dblTCrt != 0)
                    {
                        dblRetVal = Math.Round((dblTCrt / dblTAmt), 2);
                    }
                    break;
                case CalcTyp.AvgPer:
                    if (dblTAmt != 0 && dblTCrt != 0)
                    {
                        dblRetVal = Math.Round(((dblTCrt / dblTAmt) * 100), 2);
                    }
                    break;
                case CalcTyp.Disc:

                    double dblORate = (dblTOAmt / dblTCrt);
                    double dblRate = (dblTAmt / dblTCrt);

                    if (dblORate != 0 && dblRate != 0)
                    {
                        dblRetVal = Math.Round(((dblORate - dblRate) / dblORate) * 100, 2);
                    }
                    break;
            }
            return dblRetVal;
        }
    }
}
