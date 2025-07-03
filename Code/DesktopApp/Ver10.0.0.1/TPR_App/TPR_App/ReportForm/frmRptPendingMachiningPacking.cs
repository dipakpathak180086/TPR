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
    public partial class frmRptPendingMachiningPacking : Form
    {
        #region Variables

        Dal oDal;
        private string _reportProcessType = "1";
        #endregion

        #region Form Methods

        public frmRptPendingMachiningPacking()
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
                cbProcess.SelectedIndex = 0;
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
                lblTotalQty.Text = "0";
                lblOKQty.Text = "0";
                lblNgQty.Text = "0";
                lblNGPerc.Text = "0";

                lblMessage.Text = "";
                if (Convert.ToDateTime(dtpFromDate.Text) > Convert.ToDateTime(dtpToDate.Text))
                {
                    ClsGlobal.SetInfoMessage("To date can no be less than from date!!", lblMessage);
                    return;
                }
                string ModelNo = cmbModelNo.SelectedIndex > 0 ? cmbModelNo.SelectedItem.ToString() : "";
                
                DataTable dt = oDal.GetPendingMachiningPacking(dtpFromDate.Text, dtpToDate.Text, ModelNo, txtLotNo.Text.Trim(),"0",_reportProcessType,"");
                dgv.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    //int OkQty = dt.AsEnumerable().Sum(x => x.Field<int>("OkQty"));
                    //int NgQty = dt.AsEnumerable().Sum(x => x.Field<int>("NGQty"));
                    //int TotalQty = OkQty + NgQty;
                    //decimal NgPer = (Convert.ToDecimal(NgQty) * 100) / (TotalQty > 0 ? Convert.ToDecimal(TotalQty) : 1);

                    //lblTotalQty.Text = TotalQty.ToString();
                    //lblOKQty.Text = OkQty.ToString();
                    //lblNgQty.Text = NgQty.ToString();
                    //lblNGPerc.Text = Math.Round(NgPer, 2).ToString() + "%";
                }
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
                lblTotalQty.Text = "0";
                lblOKQty.Text = "0";
                lblNgQty.Text = "0";
                lblNGPerc.Text = "0";
                _reportProcessType = "1";
                cbProcess.SelectedIndex = 0;
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
                    saveFileDialog1.Filter = "Excel Files|*.xlsx|1997-2003 Excel Files|*.xls|CSV Files|*.csv";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string ModelNo = cmbModelNo.SelectedIndex > 0 ? cmbModelNo.SelectedItem.ToString() : "";
                        DataTable dt = oDal.GetPendingMachiningPacking(dtpFromDate.Text, dtpToDate.Text, ModelNo, txtLotNo.Text.Trim(), "0", _reportProcessType, "");
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

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(dgv.CurrentRow.Cells["NGQty"].Value.ToString()) > 0)
                {
                    DataTable dt = oDal.GetMachiningReportData("", "", "", txtLotNo.Text.Trim(), dgv.CurrentRow.Cells["Id"].Value.ToString(), "2");
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

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProcess.SelectedItem.ToString() == "Machining")
            {
                dgv.DataSource = null;
                dgv.Columns["CuttingTotalQty"].Visible = true;
                dgv.Columns["CuttingOkQty"].Visible = true;
                dgv.Columns["CuttingNGQty"].Visible = true;
                dgv.Columns["MachiningTotalQty"].Visible = true;
                dgv.Columns["MachiningOkQty"].Visible = true;
                dgv.Columns["MachiningNGQty"].Visible = true;
                dgv.Columns["PackingTotalQty"].Visible = false;
                dgv.Columns["PackingOkQty"].Visible = false;
                dgv.Columns["PackingNGQty"].Visible = false;
                _reportProcessType = "1";
            }
            else
            {
                dgv.DataSource = null;
                dgv.Columns["CuttingTotalQty"].Visible = false;
                dgv.Columns["CuttingOkQty"].Visible = false;
                dgv.Columns["CuttingNGQty"].Visible = false;
                dgv.Columns["MachiningTotalQty"].Visible = true;
                dgv.Columns["MachiningOkQty"].Visible = true;
                dgv.Columns["MachiningNGQty"].Visible = false;
                dgv.Columns["PackingTotalQty"].Visible = true;
                dgv.Columns["PackingOkQty"].Visible = true;
                dgv.Columns["PackingNGQty"].Visible = true;
                _reportProcessType = "2";
            }
        }
    }
}
