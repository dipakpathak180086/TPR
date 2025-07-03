using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace TPR_App
{
    public partial class frmRptDispatch : Form
    {
        #region Variables

        private Dal oDal;

        #endregion

        #region Form Methods

        public frmRptDispatch()
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
                dgv.AutoGenerateColumns = false;
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
                lblMessage.Text = "";
                //if (dtpFromDate.Value> dtpToDate.Value)
                //{
                //    ClsGlobal.SetInfoMessage("To date can no be less than from date!!", lblMessage);
                //    return;
                //}
                string customerName = "''";
                string ModelNo = cmbModelNo.SelectedIndex > 0 ? cmbModelNo.SelectedItem.ToString() : "";
                string CustomerId = cmbCustomer.SelectedIndex > 0 ? cmbCustomer.SelectedValue.ToString() : "0";
                string TrolleyNo = cmbTrolleyNo.SelectedIndex > 0 ? cmbTrolleyNo.SelectedItem.ToString() : "";
                string isTrolleyReceived = cmbIsTrolleyRec.SelectedIndex > 0 ? cmbIsTrolleyRec.SelectedItem.ToString() : "";
                DataTable dt = null;
                DataTable dtColumns = new DataTable();
                if (lstItem.Items.Count > 0)
                {

                    for (int i = 0; i < lstItem.Items.Count; i++)
                    {
                        dt = oDal.GetDispatchReportData(dtpFromDate.Value.ToString("yyyy-MM-dd"), dtpToDate.Value.ToString("yyyy-MM-dd"), ModelNo, CustomerId, TrolleyNo, isTrolleyReceived, lstItem.Items[i].ToString());
                        if (dtColumns.Columns.Count == 0)
                        {
                            dtColumns = dt.Clone();
                        }

                        foreach (DataRow item in dt.Rows)
                        {
                            dtColumns.ImportRow(item);
                        }

                    }
                    if (dtColumns.Rows.Count >0)
                    {
                        int iTotalDispatchQty = dtColumns.AsEnumerable().Sum(x => x.Field<int>("DispatchQty"));
                        lblTotalDispatchQty.Text = "Total Dispatch Qty: " + iTotalDispatchQty;
                    }
                    dgv.DataSource = dtColumns;
                }
                else
                {
                    dt = oDal.GetDispatchReportData(dtpFromDate.Value.ToString("yyyy-MM-dd"), dtpToDate.Value.ToString("yyyy-MM-dd"), ModelNo, CustomerId, TrolleyNo, isTrolleyReceived, "");
                    if (dt.Rows.Count >0)
                    {
                        int iTotalDispatchQty = dt.AsEnumerable().Sum(x => x.Field<int>("DispatchQty"));
                        lblTotalDispatchQty.Text = "Total Dispatch Qty: " + iTotalDispatchQty;
                    }
                    dgv.DataSource = dt;
                }



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
                cmbModelNo.SelectedIndex = 0;
                cmbCustomer.SelectedIndex = 0;
                cmbIsTrolleyRec.SelectedIndex = 0;
                if (cmbTrolleyNo.SelectedIndex > 0)
                {
                    cmbTrolleyNo.SelectedIndex = 0;
                }

                dgv.DataSource = null;
                lstItem.Items.Clear();
                lblCount.Text = "Rows Count : 0";
                lblTotalDispatchQty.Text = "Total Dispatch Qty: 0";
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
        private void ChangeGridColor()
        {
            try
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Cells["IsTrolleyReceived"].Value.ToString().ToUpper() == "NO")
                    {
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void BindCombo()
        {
            try
            {
                lblMessage.Text = "";
                //Bind Customer
                DataTable dt = oDal.GetCustomer("1");
                DataRow dataRow = dt.NewRow();
                dataRow["Customer"] = "--Select--";
                dataRow["Id"] = 0;
                dt.Rows.InsertAt(dataRow, 0);
                cmbCustomer.DataSource = dt;
                cmbCustomer.DisplayMember = "Customer";
                cmbCustomer.ValueMember = "Id";
                //Bind Mondel
                dt = oDal.ManageModel(new Model { DbType = EnumDbType.SELECT });
                cmbModelNo.Items.Add("--Select--");
                foreach (DataRow row in dt.Rows)
                {
                    cmbModelNo.Items.Add(row["ModelNo"].ToString());
                }

                cmbCustomer.SelectedIndex = 0;
                cmbModelNo.SelectedIndex = 0;
                //Bind Trolley no
                dt = oDal.GetTrolleyReceivingReportData("1");
                cmbTrolleyNo.Items.Add("--Select--");
                foreach (DataRow row in dt.Rows)
                {
                    cmbTrolleyNo.Items.Add(row["TrolleyNo"].ToString());
                }
                cmbTrolleyNo.SelectedIndex = cmbIsTrolleyRec.SelectedIndex = 0;
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

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (Convert.ToInt32(dgv.CurrentRow.Cells["NGQty"].Value.ToString()) > 0)
                //{
                //    DataTable dt = oDal.GetFinalPackingReportData("", "", "", cmbCustomer.SelectedValue.ToString(), dgv.CurrentRow.Cells["Id"].Value.ToString(), "2");
                //    frmRptNgView oFrm = new frmRptNgView(dt);
                //    oFrm.ShowDialog();
                //}
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
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
