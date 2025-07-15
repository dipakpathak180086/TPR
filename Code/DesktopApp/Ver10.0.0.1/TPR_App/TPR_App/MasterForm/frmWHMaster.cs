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
    public partial class frmWHMaster : Form
    {
        #region Variables

        Dal oDal;
        PL_WH_MASTER objpl;
        bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmWHMaster()
        {
            try
            {
                InitializeComponent();
                objpl = new PL_WH_MASTER();
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

                btnDelete.Enabled = false;
                lblMessage.Text = "";
                txtWHCode.Focus();
                BindGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "ERROR: " + ex.Message;
            }
        }

        #endregion

        #region Button Event

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (ValidateInput())
                {
                    objpl = new PL_WH_MASTER();
                    objpl.WHCode = txtWHCode.Text.Trim();
                    objpl.WHName = txtWHName.Text.Trim();
                    objpl.WHLocation = txtWHLocation.Text.Trim();
                    objpl.Status = chkSelect.Checked;
                    objpl.CreatedBy = ClsGlobal.UserId;
                    //If saving data
                    if (_IsUpdate == false)
                    {
                        objpl.DbType = EnumDbType.INSERT;
                        oDal.WarehouseMasterExecuteTask(objpl);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                    }
                    else // if updating data
                    {
                        objpl.DbType = EnumDbType.UPDATE;
                        oDal.WarehouseMasterExecuteTask(objpl);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Updated successfully!!", lblMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    ClsGlobal.SetErrorMessage("Customer name with same location already exist!!", lblMessage);
                }
                else
                {
                    ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
                }
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                Clear();
                BindGrid();
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (string.IsNullOrEmpty(txtWHCode.Text))
                {
                    ClsGlobal.SetInfoMessage("Please select Customer", lblMessage);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("Äre you sure to delete the record !!", ClsGlobal.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    objpl = new PL_WH_MASTER();
                    objpl.WHCode = txtWHCode.Text.Trim();
                    objpl.DbType = EnumDbType.DELETE;
                    oDal.WarehouseMasterExecuteTask(objpl);

                    btnReset_Click(sender, e);
                    ClsGlobal.SetConfirmMessage("Deleted successfully!!", lblMessage);
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaxiMize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        #endregion

        #region Methods

        private void Clear()
        {
            try
            {
                txtWHName.Text = "";
                txtWHCode.Text = "";
                txtWHLocation.Text = "";
                txtWHLocation.Text = "";
                lblMessage.Text = "";
                txtWHCode.Enabled = txtWHLocation.Enabled = true;
                btnDelete.Enabled = false;
                _IsUpdate = false;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void BindGrid()
        {
            try
            {
                lblMessage.Text = "";
                objpl.DbType = EnumDbType.SELECT;
                DataTable dt = oDal.WarehouseMasterExecuteTask(objpl);
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private bool ValidateInput()
        {
            try
            {
                if (txtWHCode.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Warehouse code can't be blank!!", lblMessage);
                    txtWHCode.Focus();
                    return false;
                }
                if (txtWHName.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Warehouse Name can't be blank!!", lblMessage);
                    txtWHName.Focus();
                    return false;
                }
                if (txtWHLocation.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Warehouse Location can't be blank!!", lblMessage);
                    txtWHLocation.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
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
                Clear();
                txtWHCode.Text = dgv.Rows[e.RowIndex].Cells["WHCode"].Value.ToString();
                txtWHName.Text = dgv.Rows[e.RowIndex].Cells["WHName"].Value.ToString();
                txtWHLocation.Text = dgv.Rows[e.RowIndex].Cells["WHLocation"].Value.ToString();
                object cellValue = dgv.Rows[e.RowIndex].Cells["Status"].Value;
                chkSelect.Checked = cellValue == null || cellValue == DBNull.Value ? false : (bool)cellValue;

                btnDelete.Enabled = true;
                txtWHCode.Enabled = false;
               // txtWHLocation.Enabled = false;
                _IsUpdate = true;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

        #region TextBox Event

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dgv.DataSource as DataTable).DefaultView.RowFilter = string.Format("WHCode LIKE '%{0}%' or WHName LIKE '%{0}%'", txtSearch.Text);
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

    }
}
