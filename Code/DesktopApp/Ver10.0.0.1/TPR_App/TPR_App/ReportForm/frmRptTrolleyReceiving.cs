using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace TPR_App
{
    public partial class frmRptTrolleyReceiving : Form
    {
        #region Variables

        private Dal oDal;

        #endregion

        #region Form Methods

        public frmRptTrolleyReceiving()
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
                BindCombo();
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
                lblTrayQty.Text = "Tray Total Qty : 0";
                lblMessage.Text = "";
                if (Convert.ToDateTime(dtpFromDate.Text) > Convert.ToDateTime(dtpToDate.Text))
                {
                    ClsGlobal.SetInfoMessage("To date can no be less than from date!!", lblMessage);
                    return;
                }
                string customerName = "''";
                string CustomerId = cmbCustomer.SelectedIndex > 0 ? cmbCustomer.SelectedValue.ToString() : "0";
                string TrolleyNo = cmbTrolleyNo.SelectedIndex > 0 ? cmbTrolleyNo.SelectedItem.ToString() : "";
                DataTable dt = null;
                DataTable dtColumns = new DataTable();
                if (lstItem.Items.Count > 0)
                {
                    for (int i = 0; i < lstItem.Items.Count; i++)
                    {
                        dt = oDal.GetTrolleyReceivingReportData("2", dtpFromDate.Value.ToString("yyyy-MM-dd"), dtpToDate.Value.ToString("yyyy-MM-dd"), TrolleyNo, CustomerId, 0, "", "0", lstItem.Items[i].ToString());
                    if (dtColumns.Columns.Count == 0)
                    {
                        dtColumns = dt.Clone();
                    }

                    foreach (DataRow item in dt.Rows)
                    {
                        dtColumns.ImportRow(item);
                    }
                    }
                    dgv.DataSource = dtColumns;
                }
                else
                {
                    dt = oDal.GetTrolleyReceivingReportData("2", dtpFromDate.Value.ToString("yyyy-MM-dd"), dtpToDate.Value.ToString("yyyy-MM-dd"), TrolleyNo, CustomerId, 0, "", "0", "");
                    dgv.DataSource = dt;
                }
             
                if (dt.Rows.Count > 0)
                {
                   
                    lblTrayQty.Text = "Tray Total Qty : " + dt.Compute("SUM(TrayQty)", string.Empty);
                }
                if (dtColumns.Rows.Count > 0)
                {

                    lblTrayQty.Text = "Tray Total Qty : " + dtColumns.Compute("SUM(TrayQty)", string.Empty);
                }
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
                lstItem.Items.Clear();
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
                cmbTrolleyNo.SelectedIndex = 0;
                cmbCustomer.SelectedIndex = 0;
                dgv.DataSource = null;
                lstItem.Items.Clear();
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
                        {
                            ClsGlobal.ExportCsv(dt, saveFileDialog1.FileName);
                        }
                    }
                }
                else
                {
                    ClsGlobal.ShowInfoMessageBox("There is no data to export");
                }
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

        private void BindCombo()
        {
            try
            {
                //Bind Customer
                DataTable dt = oDal.GetCustomer("1");
                DataRow dataRow = dt.NewRow();
                dataRow["Customer"] = "--Select--";
                dataRow["Id"] = 0;
                dt.Rows.InsertAt(dataRow, 0);
                cmbCustomer.DataSource = dt;
                cmbCustomer.DisplayMember = "Customer";
                cmbCustomer.ValueMember = "Id";
                //Bind Trolley No
                dt = oDal.GetTrolleyReceivingReportData("1");
                cmbTrolleyNo.Items.Add("--Select--");
                foreach (DataRow row in dt.Rows)
                {
                    cmbTrolleyNo.Items.Add(row["TrolleyNo"].ToString());
                }
                cmbTrolleyNo.SelectedIndex = 0;
                cmbCustomer.SelectedIndex = 0;
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

        #region DataGridView Event
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows.Count > 0)
                {
                    frmTrolleyReceiving oFrm = new frmTrolleyReceiving(dgv.CurrentRow.Cells["TrayQty"].Value.ToString(), dgv.CurrentRow.Cells["ColorName"].Value.ToString(), dgv.CurrentRow.Cells["Id"].Value.ToString());
                    oFrm.FormClosed += OFrm_FormClosed;
                    oFrm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        private void OFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            { btnGet_Click(sender, e); }
            catch (Exception ex) { throw ex; }
        }


        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex > 0)
            {
                lstItem.Items.Add(cmbCustomer.Text.Trim());
                cmbCustomer.SelectedIndex = 0;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstItem.Items.Count > 0)
            {
                for (int i = lstItem.Items.Count - 1; i >= 0; i--)
                {
                    lstItem.Items.RemoveAt(i);
                    break;
                }
                
            }
        }
    }
}
