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

namespace TPR_App
{
    public partial class frmRptCutting : Form
    {
        #region Variables

        Dal oDal;

        #endregion

        #region Form Methods

        public frmRptCutting()
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

                lblMessage.Text = "";
                GetModel();
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
                DataTable dt = oDal.GetCuttingReportData(dtpFromDate.Text, dtpToDate.Text, ModelNo, txtLotNo.Text.Trim(), "0", "1");
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
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
                cmbModelNo.SelectedIndex = 0;
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
                    //saveFileDialog1.Filter = "CSV Files|*.csv|Excel Files|*.xlsx|1997-2003 Excel Files|*.xls";
                    saveFileDialog1.Filter = "CSV Files|*.csv";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string ModelNo = cmbModelNo.SelectedIndex > 0 ? cmbModelNo.SelectedItem.ToString() : "";
                        DataTable dt = oDal.GetCuttingReportData(dtpFromDate.Text, dtpToDate.Text, ModelNo, txtLotNo.Text.Trim(), "0", "3");
                        if (saveFileDialog1.FilterIndex == 1)
                        {
                            ClsGlobal.ExportCsv(dt, saveFileDialog1.FileName);
                        }
                        else
                        {

                        }
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

        #endregion

        #region Label Event
        private void lblMessage_DoubleClick(object sender, EventArgs e)
        {
            ClsGlobal.ShowInfoMessageBox(lblMessage.Text);
        }


        #endregion

        #region DataGridView Events
        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                if (dgv.Rows[e.RowIndex].Cells["QAPending"].Value.ToString().ToUpper() == "YES")
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                else
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(dgv.CurrentRow.Cells["NGQty"].Value.ToString()) > 0)
                {
                    DataTable dt = oDal.GetCuttingReportData("", "", "", txtLotNo.Text.Trim(), dgv.CurrentRow.Cells["Id"].Value.ToString(), "2");
                    frmRptNgView oFrm = new frmRptNgView(dt);
                    oFrm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        #endregion
    }
}
