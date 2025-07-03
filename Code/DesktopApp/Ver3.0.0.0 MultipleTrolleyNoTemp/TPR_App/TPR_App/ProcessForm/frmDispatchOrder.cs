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
    public partial class frmDispatchOrder : Form
    {
        #region Variables

        Dal oDal;
        DispatchOrder oDispatchOrder;
        bool _IsUpdate = false;
        int _OldQty = 0;

        #endregion

        #region Form Methods

        public frmDispatchOrder()
        {
            try
            {
                InitializeComponent();
                oDispatchOrder = new DispatchOrder();
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
                dgv.AutoGenerateColumns = false;
                lblMessage.Text = "";
                BindCombo();
                BindGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "ERROR: " + ex.Message;
            }
        }

        #endregion

        #region Button Event
        private void btnShowTrolleyStock_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAvailQty.Text.Trim() != "" && Convert.ToInt32(txtAvailQty.Text.Trim()) > 0)
                {
                    frmTrolleyStock ofrm = new frmTrolleyStock(cmbModel.SelectedItem.ToString());
                    ofrm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
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
                    oDispatchOrder.Shift = oDal.GetShift().Rows[0]["ShiftName"].ToString();
                    oDispatchOrder.DispatchDate = dtpDispatchDate.Text;
                    oDispatchOrder.ModelNo = cmbModel.SelectedItem.ToString();
                    oDispatchOrder.CustomerId = cmbCustomer.SelectedValue.ToString();
                    oDispatchOrder.Qty = Convert.ToInt32(txtQty.Text.Trim());
                    oDispatchOrder.OldQty = _OldQty;
                    oDispatchOrder.CreatedBy = ClsGlobal.UserId;

                    //If saving data
                    if (_IsUpdate == false)
                    {
                        oDispatchOrder.DbType = EnumDbType.INSERT;
                        oDal.ManageDispatchOrder(oDispatchOrder);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                    }
                    else // if updating data
                    {
                        oDispatchOrder.DbType = EnumDbType.UPDATE;
                        oDal.ManageDispatchOrder(oDispatchOrder);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Updated successfully!!", lblMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    ClsGlobal.SetErrorMessage("Same date,same model,same customer details already exist!!", lblMessage);
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
                if (cmbModel.SelectedIndex<=0 || cmbCustomer.SelectedIndex<=0)
                {
                    ClsGlobal.SetInfoMessage("Please select dispatch order", lblMessage);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("Äre you sure to delete the record !!", ClsGlobal.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    oDispatchOrder.DispatchDate = dtpDispatchDate.Text;
                    oDispatchOrder.ModelNo = cmbModel.SelectedItem.ToString();
                    oDispatchOrder.CustomerName = cmbCustomer.SelectedItem.ToString();
                    oDispatchOrder.DbType = EnumDbType.DELETE;
                    oDal.ManageDispatchOrder(oDispatchOrder);

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
        private void BindGrid()
        {
            try
            {
                lblMessage.Text = "";
                oDispatchOrder.DbType = EnumDbType.SELECT;
                DataTable dt = oDal.ManageDispatchOrder(oDispatchOrder);
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void Clear()
        {
            try
            {
                dtpDispatchDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                cmbModel.SelectedIndex = 0;
                cmbCustomer.SelectedIndex = 0;
                txtQty.Text = "";
                txtAvailQty.Text = "";
                
                lblMessage.Text = "";
                dtpDispatchDate.Enabled = true;
                cmbCustomer.Enabled = true;
                cmbModel.Enabled = true;
                btnDelete.Enabled = false;
                _IsUpdate = false;
                _OldQty = 0;
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
                if (Convert.ToDateTime(dtpDispatchDate.Text) < DateTime.Parse(DateTime.Now.ToString("dd-MMM-yyyy")))
                {
                    ClsGlobal.SetInfoMessage("Date can not be past date!!", lblMessage);
                    dtpDispatchDate.Focus();
                    return false;
                }
                if (cmbModel.SelectedIndex <= 0)
                {
                    ClsGlobal.SetInfoMessage("Please select model!!", lblMessage);
                    cmbModel.Focus();
                    return false;
                }
                if (cmbCustomer.SelectedIndex <= 0)
                {
                    ClsGlobal.SetInfoMessage("Please select customer!!", lblMessage);
                    cmbCustomer.Focus();
                    return false;
                }
                if (txtAvailQty.Text.Trim() == "" || Convert.ToInt32(txtAvailQty.Text.Trim()) <= 0)
                {
                    ClsGlobal.SetInfoMessage("Stock is not available", lblMessage);
                    return false;
                }
                if (txtQty.Text.Trim() == "" || Convert.ToInt32(txtQty.Text.Trim()) <= 0)
                {
                    ClsGlobal.SetInfoMessage("Please input qty!!", lblMessage);
                    txtQty.Focus();
                    return false;
                }
                if (Convert.ToInt32(txtQty.Text.Trim()) > Convert.ToInt32(txtAvailQty.Text.Trim()))
                {
                    ClsGlobal.SetInfoMessage("Qty can not be greater than available qty!!", lblMessage);
                    txtQty.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
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
                cmbModel.Items.Add("--Select--");
                foreach (DataRow row in dt.Rows)
                    cmbModel.Items.Add(row["ModelNo"].ToString());
                cmbCustomer.SelectedIndex = 0;
                cmbModel.SelectedIndex = 0;
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
                Clear();
                dtpDispatchDate.Text= dgv.Rows[e.RowIndex].Cells["DispatchDate"].Value.ToString();
                cmbModel.SelectedItem = dgv.Rows[e.RowIndex].Cells["ModelNo"].Value.ToString();
                cmbModel_SelectionChangeCommitted(sender, e);
                cmbCustomer.SelectedValue = dgv.Rows[e.RowIndex].Cells["CustomerId"].Value.ToString();
                txtQty.Text = dgv.Rows[e.RowIndex].Cells["Qty"].Value.ToString();
                _OldQty = Convert.ToInt32(txtQty.Text);
                
                btnDelete.Enabled = true;
                dtpDispatchDate.Enabled = false;
                cmbModel.Enabled = false;
                cmbCustomer.Enabled = false;
                _IsUpdate = true;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

        #region TextBox Event
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
        #endregion

        #region ComboBoxEvent
        private void cmbModel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                txtQty.Text = "";
                txtAvailQty.Text = "";
                if (cmbModel.SelectedIndex > 0)
                {
                    DataTable dt = oDal.GetAvailableStockForDispatch("1", cmbModel.SelectedItem.ToString());
                    txtAvailQty.Text = dt.Rows[0]["Qty"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion
        
    }
}
