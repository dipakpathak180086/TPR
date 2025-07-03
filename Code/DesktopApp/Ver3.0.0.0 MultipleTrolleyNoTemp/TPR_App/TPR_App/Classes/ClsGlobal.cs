using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TPR_App
{
    public enum EnumDbType { SELECT, INSERT, UPDATE, DELETE, SELECTBYID, SEARCH, VALIDATEUSER, UPDATEPASSWORD };
    public enum EnumCuttingStatus { Cutting = 1, QC_Ok = 2, QC_Ng = 3, QC_Hold = 4, QC_Sample = 5, After_Machining = 6 };
    public enum EnumTrolleyStatus { Cutting = 1, Machining = 2, FinalPacking = 3 };
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

        public static void ExportCsv(DataTable dt,string FileName)
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

        public static void ExportExcel()
        {
            try
            {
                //Microsoft.Office.Interop.Excel.Application obj = new Microsoft.Office.Interop.Excel.Application();
                //obj.Workbooks.Add(Type.Missing);

                //Microsoft.Office.Interop.Excel.Range FormatRange;
                //FormatRange = obj.Worksheets[1].Cells;
                //FormatRange.NumberFormat = "@";

                //obj.Cells[1, 1] = "IP Number";
                //obj.Cells[1, 2] = "Product GTIN";
                //obj.Cells[1, 3] = "Case SSCC No";
                ////AddColumn
                //for (int j = 0; j < dgView.Columns.Count; j++)
                //{
                //    obj.Cells[1, j + 1] = dgView.Columns[j].HeaderText;
                //}
                ////Add Row
                //for (int i = 0; i < dgView.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dgView.Columns.Count; j++)
                //    {
                //        // i+2 means row index 2 we want to store data from 2nd row
                //        // obj.Cells[i + 2, j + 1] = dt.Rows[i][j];
                //        obj.Cells[i + 2, j + 1] = dgView[j, i].EditedFormattedValue.ToString();
                //    }
                //}

                //obj.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName);

                //obj.ActiveWorkbook.Saved = true;

                //obj.Quit();

                //ClsGlobal.ShowConfirmMessageBox("File export successfully");
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}
