using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;

namespace TPR_App
{
    public partial class frmRptQA : Form
    {
        #region Variables

        Dal oDal;

        #endregion

        #region Form Methods

        public frmRptQA()
        {
            try
            {
                InitializeComponent();
                oDal = new Dal();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "ERROR: " + ex.Message;
            }
        }

        private void frmModelMaster_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

                dgv.AutoGenerateColumns = false;
                lblMessage.Text = "";
                GetModel();
                GetLine();
                GetStatus();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "ERROR: " + ex.Message;
            }
        }

        #endregion

        #region Button Event
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (Convert.ToDateTime(dtpFromDate.Text) > Convert.ToDateTime(dtpToDate.Text))
                {
                    ClsGlobal.SetInfoMessage("To date can no be less than from date!!", lblMessage);
                    return;
                }
                string ModelNo = cmbModelNo.SelectedIndex > 0 ? cmbModelNo.SelectedItem.ToString() : "";
                string LineNo = cmbLineNo.SelectedIndex > 0 ? cmbLineNo.SelectedItem.ToString().Split('-')[0].Trim() : "";
                string status = cmbStatus.SelectedIndex > 0 ? cmbStatus.SelectedItem.ToString().Trim() : "";

                DataTable dt = oDal.GetQAReportData(dtpFromDate.Text, dtpToDate.Text, ModelNo, txtLotNo.Text.Trim(),LineNo, status);
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;

                ChangeGridColor();
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                cmbLineNo.SelectedIndex = 0;
                cmbModelNo.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
                txtLotNo.Text = "";
                dgv.DataSource = null;
                lblCount.Text = "Rows Count : 0";
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    saveFileDialog1.Filter = "Excel Files|*.xlsx|1997-2003 Excel Files|*.xls|CSV Files|*.csv";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dt = dgv.DataSource as DataTable;
                        if (saveFileDialog1.FilterIndex == 1)
                        {
                            ClsGlobal.ExportExcel(dt, saveFileDialog1.FileName);
                        }
                        else if (saveFileDialog1.FilterIndex == 2)
                        {
                            ClsGlobal.ExportExcel(dt, saveFileDialog1.FileName);
                        }
                        else if (saveFileDialog1.FilterIndex == 3)
                            ClsGlobal.ExportCsv(dt, saveFileDialog1.FileName);
                    }
                }
                else
                    ClsGlobal.ShowInfoMessageBox("There is no data to export");
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

        private void GetStatus()
        {
            try
            {
                cmbStatus.Items.Add("--Select--");
                DataTable dt = oDal.ReprintHistory("GET_STATUS_TYPE", "", "", 0, "", ClsGlobal.UserId, "", "");
                foreach (DataRow row in dt.Rows)
                    cmbStatus.Items.Add(row["Status"].ToString().Trim());
                cmbStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void GetLine()
        {
            try
            {
                cmbLineNo.Items.Add("--Select Line--");
                DataTable dt = oDal.ManageLine(new Line { DbType = EnumDbType.SELECT });
                foreach (DataRow row in dt.Rows)
                    cmbLineNo.Items.Add(row["Line_No"].ToString().Trim() + "-" + row["Description"].ToString().Trim());
                cmbLineNo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void GetModel()
        {
            try
            {
                DataTable dt = oDal.GetModel();
                cmbModelNo.Items.Add("--Select--");
                foreach (DataRow row in dt.Rows)
                {
                    cmbModelNo.Items.Add(row["ModelNo"].ToString());
                }
                cmbModelNo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void ChangeGridColor()
        {
            try
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    if (dgv.Rows[i].Cells["Status"].Value.ToString().ToUpper() == "OK")
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    else if (dgv.Rows[i].Cells["Status"].Value.ToString().ToUpper() == "NG")
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    else
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

        #region Label Event
        private void lblMessage_DoubleClick(object sender, EventArgs e)
        {
            ClsGlobal.ShowInfoMessageBox(lblMessage.Text);
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
