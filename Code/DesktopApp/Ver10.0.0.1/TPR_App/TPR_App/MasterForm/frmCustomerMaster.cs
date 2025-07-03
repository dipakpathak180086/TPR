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
    public partial class frmCustomerMaster : Form
    {
        #region Variables

        Dal oDal;
        Customer oCustomer;
        bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmCustomerMaster()
        {
            try
            {
                InitializeComponent();
                oCustomer = new Customer();
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
                txtName.Focus();
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
                    oCustomer.Name = txtName.Text.Trim();
                    oCustomer.Address = txtAddress.Text.Trim();
                    oCustomer.Location = txtLocation.Text.Trim();
                    oCustomer.CreatedBy = ClsGlobal.UserId;
                    //If saving data
                    if (_IsUpdate == false)
                    {
                        oCustomer.DbType = EnumDbType.INSERT;
                        oDal.ManageCustomer(oCustomer);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                    }
                    else // if updating data
                    {
                        oCustomer.DbType = EnumDbType.UPDATE;
                        oDal.ManageCustomer(oCustomer);
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
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    ClsGlobal.SetInfoMessage("Please select Customer", lblMessage);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("Äre you sure to delete the record !!", ClsGlobal.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    oCustomer.Name = txtName.Text.Trim();
                    oCustomer.Location = txtLocation.Text.Trim();
                    oCustomer.DbType = EnumDbType.DELETE;
                    oDal.ManageCustomer(oCustomer);

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
                txtName.Text = "";
                txtAddress.Text = "";
                txtLocation.Text = "";
                lblMessage.Text = "";
                txtName.Enabled = txtLocation.Enabled= true;
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
                oCustomer.DbType = EnumDbType.SELECT;
                DataTable dt = oDal.ManageCustomer(oCustomer);
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
                if (txtName.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Customer Name can't be blank!!", lblMessage);
                    txtName.Focus();
                    return false;
                }
                if (txtAddress.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Address can't be blank!!", lblMessage);
                    txtAddress.Focus();
                    return false;
                }
                if (txtLocation.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Location can't be blank!!", lblMessage);
                    txtAddress.Focus();
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
                txtName.Text = dgv.Rows[e.RowIndex].Cells["CustomerName"].Value.ToString();
                txtAddress.Text = dgv.Rows[e.RowIndex].Cells["Address"].Value.ToString();
                txtLocation.Text = dgv.Rows[e.RowIndex].Cells["Location"].Value.ToString();

                btnDelete.Enabled = true;
                txtName.Enabled = false;
                txtLocation.Enabled = false;
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
                lblMessage.Text = "";
                oCustomer.DbType = EnumDbType.SEARCH;
                oCustomer.Name = txtSearch.Text.Trim();
                DataTable dt = oDal.ManageCustomer(oCustomer);
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
