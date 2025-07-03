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
    public partial class frmModelMaster : Form
    {
        #region Variables

        Dal oDal;
        Model oModel;
        bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmModelMaster()
        {
            try
            {
                InitializeComponent();
                oModel = new Model();
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
                txtModelNo.Focus();
                BindGrid();
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (ValidateInput())
                {
                    oModel.ModelNo = txtModelNo.Text.Trim();
                    oModel.Description = txtDesc.Text.Trim();
                    oModel.DTB = int.Parse(txtDTB.Text.Trim());
                    oModel.Qty = int.Parse(txtQty.Text.Trim());
                    oModel.CreatedBy = ClsGlobal.UserId;
                    //If saving data
                    if (_IsUpdate == false)
                    {
                        oModel.DbType = EnumDbType.INSERT;
                        oDal.ManageModel(oModel);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                    }
                    else // if updating data
                    {
                        oModel.DbType = EnumDbType.UPDATE;
                        oDal.ManageModel(oModel);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Updated successfully!!", lblMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    ClsGlobal.SetErrorMessage("Model No already exist!!", lblMessage);
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
                if (string.IsNullOrEmpty(txtModelNo.Text))
                {
                    ClsGlobal.SetInfoMessage("Please select Model", lblMessage);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("Äre you sure to delete the record !!", ClsGlobal.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    oModel.ModelNo = txtModelNo.Text.Trim();
                    oModel.DbType = EnumDbType.DELETE;
                    oDal.ManageModel(oModel);

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

        #endregion

        #region Methods

        private void Clear()
        {
            try
            {
                txtModelNo.Text = "";
                txtDesc.Text = "";
                lblMessage.Text = "";
                txtQty.Text = "";
                txtDTB.Text = "";
                txtModelNo.Enabled = true;
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
                oModel.DbType = EnumDbType.SELECT;
                DataTable dt = oDal.ManageModel(oModel);
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
                if (txtModelNo.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Model No can't be blank!!", lblMessage);
                    txtModelNo.Focus();
                    return false;
                }
                if (txtDesc.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Description can't be blank!!", lblMessage);
                    txtDesc.Focus();
                    return false;
                }
                if (txtDTB.Text.Trim() == "" || txtDTB.Text.Trim() == "0")
                {
                    ClsGlobal.SetInfoMessage("Please input DTB!!", lblMessage);
                    txtDTB.Focus();
                    return false;
                }
                if (txtQty.Text.Trim() == "" || txtQty.Text.Trim() == "0")
                {
                    ClsGlobal.SetInfoMessage("Please input cutting max qty!!", lblMessage);
                    txtQty.Focus();
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
                txtModelNo.Text = dgv.Rows[e.RowIndex].Cells["ModelNo"].Value.ToString();
                txtDesc.Text = dgv.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                txtDTB.Text = dgv.Rows[e.RowIndex].Cells["DTB"].Value.ToString();
                txtQty.Text = dgv.Rows[e.RowIndex].Cells["CuttingMaxQty"].Value.ToString();

                btnDelete.Enabled = true;
                txtModelNo.Enabled = false;
                _IsUpdate = true;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

        #region TextBox Event

        private void txtDTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (e.KeyChar != 8 && !char.IsNumber(e.KeyChar))
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (e.KeyChar != 8 && !char.IsNumber(e.KeyChar))
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                oModel.DbType = EnumDbType.SEARCH;
                oModel.ModelNo = txtSearch.Text.Trim();
                DataTable dt = oDal.ManageModel(oModel);
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion



    }
}
