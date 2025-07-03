using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TPR_App
{
    public enum EnumProcess { CUTTING, MACHINING, FINAL_PACKING }
    public enum EnumDbType { SELECT, INSERT, UPDATE, DELETE, SELECTBYID, SEARCH, VALIDATEUSER, UPDATEPASSWORD };
    public enum EnumCuttingStatus { Cutting = 1, QC_Ok = 2, QC_Ng = 3, QC_Hold = 4, QC_Sample = 5, Picking = 6, After_Machining = 7 };
    public enum EnumTrolleyStatus { Receive = 0, Dispatch = 1 };
    public enum EnumAppType { DESKTOPAPP, COMMSERVER, ANDROIDAPP };
    public enum EnumMachiningStatus { Machining = 1, FinalPacking = 2 };
    public enum EnumFinalPackingStatus { FinalPacking = 1, Dispatch = 2 };
    public enum EnumDashBoardName { TROLLEYCOUNT, PRODUCTION, INVENTORY, RECEIVING_PENDING }

    public class ClsGlobal
    {
        #region Static Variables

        public static string AppName { get; set; } = "TPR";
        public static string UserId { get; set; }
        public static string UserName { get; set; }
        public static string Shift { get; set; }
        public static string LineNo { get; set; }
        public static string UserGroup { get; set; }
        public static string MachiningPrnName = "MACHINING.prn";
        public static string TrolleyPrnName = "TROLLEY.prn";
        public static string CuttingPrnName = "CUTTING.prn";
        public static bool CuttingDefectAutoClose = false; //this flag will be true when from cutting defect will close auto
        public static bool IsCuttingManualEnable = false;
        public static string mMainSqlConString = "";
        #endregion

        #region Static Methods
        public static void SetErrorMessage(string Message, Label label)
        {
            label.Text = "ERROR : " + Message;
            label.ForeColor = System.Drawing.Color.Red;
        }
        public static void SetInfoMessage(string Message, Label label)
        {
            label.Text = "INFO : " + Message;
            label.ForeColor = System.Drawing.Color.Crimson;
        }
        public static void SetConfirmMessage(string Message, Label label)
        {
            label.Text = "SUCCESS : " + Message;
            label.ForeColor = System.Drawing.Color.Green;
        }
        public static void ShowErrorMessageBox(string Message)
        {
            MessageBox.Show(Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ShowInfoMessageBox(string Message)
        {
            MessageBox.Show(Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void ShowConfirmMessageBox(string Message)
        {
            MessageBox.Show(Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ExportCsv(DataTable dt, string FileName)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(FileName);
                string StrColumns = "";
                //Add Columns
                foreach (DataColumn column in dt.Columns)
                {
                    StrColumns += column.ColumnName + ",";
                }
                StrColumns = StrColumns.TrimEnd(',');
                sw.WriteLine(StrColumns);
                //Add Row
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strRowData = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string Data = dt.Rows[i][j].ToString().Replace(',', '@');
                        //if (dt.Columns[j].ColumnName == "LotNo")
                        //    Data = "'" + Data;
                        strRowData += Data + ",";
                    }
                    strRowData = strRowData.TrimEnd(',');
                    sw.WriteLine(strRowData);
                }
                sw.Flush();
                sw.Close();
                sw = null;

                ClsGlobal.ShowConfirmMessageBox("File export successfully");
            }
            catch (Exception fex)
            { ClsGlobal.ShowErrorMessageBox(fex.Message); }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                    sw = null;
                }
            }
        }

        public static void ExportExcel(DataTable dt, string FileName)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application obj = new Microsoft.Office.Interop.Excel.Application();
                obj.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                Microsoft.Office.Interop.Excel.Range FormatRange;
                object misValue = System.Reflection.Missing.Value;
                FormatRange = obj.Worksheets[1].Cells;
                FormatRange.NumberFormat = "@";
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                xlWorkBook = obj.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                //AddColumn
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    obj.Cells[1, j + 1] = dt.Columns[j].ColumnName;
                }
                //Add Row
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (int.TryParse(dt.Rows[i][j].ToString(), out _) && !dt.Columns[j].ToString().Contains("LotNo"))
                        {
                            obj.Cells[i + 2, j + 1] = Convert.ToInt64(dt.Rows[i][j].ToString());
                        }
                        else if (DateTime.TryParse(dt.Rows[i][j].ToString(), out _) && !dt.Columns[j].ToString().Contains("LotNo"))
                        {
                            obj.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();
                            xlWorkSheet.Rows.Cells.Range["B2"].NumberFormat = "dd-MM-yyyy";
                        }

                        else if (dt.Columns[j].ToString().Contains("LotNo"))
                        {
                            obj.Cells[i + 2, j + 1] = "'" + dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            obj.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();
                        }


                    }
                }
                ////xlWorkBook.FileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel3
                ////xlWorkBook.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                ////xlWorkBook.Close(true, misValue, misValue);
                ////obj.Quit();
                obj.ActiveWorkbook.SaveAs(FileName);
                obj.ActiveWorkbook.Close(true);
                obj.ActiveWorkbook.Saved = true;

                obj.Quit();
                releaseObject(obj);
                releaseObject(xlWorkBook);
                releaseObject(xlWorkSheet);
                ClsGlobal.ShowConfirmMessageBox("File export successfully");
            }
            catch (Exception fex)
            { ClsGlobal.ShowErrorMessageBox(fex.Message); }
        }
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion
    }
}
