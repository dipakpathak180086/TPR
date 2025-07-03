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
    public partial class frmProductionPlan : Form
    {
        #region Variables

        Dal oDal;
        ProductionPlan oProdPlan;
        bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmProductionPlan()
        {
            try
            {
                InitializeComponent();
                oProdPlan = new ProductionPlan();
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
                cmbMonth.Focus();
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
                    oProdPlan.Month = cmbMonth.SelectedItem.ToString();
                    oProdPlan.MonthNo = DateTime.Now.Month + (cmbMonth.SelectedIndex - 1);
                    oProdPlan.Year = DateTime.Now.Year;
                    oProdPlan.OrderNo = Convert.ToInt32(txtOrderNo.Text.Trim());
                    oProdPlan.ModelNo = cmbModel.SelectedItem.ToString();
                    oProdPlan.Qty = Convert.ToInt32(txtQty.Text.Trim());
                    oProdPlan.CreatedBy = ClsGlobal.UserId;
                    //If saving data
                    if (_IsUpdate == false)
                    {
                        oProdPlan.DbType = EnumDbType.INSERT;
                        string ReturnMsg = oDal.ManageProductionPlan(oProdPlan).Rows[0]["RESULT"].ToString();
                        if (ReturnMsg.ToUpper() == "Y")
                        {
                            btnReset_Click(sender, e);
                            ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                        }
                        else
                            ClsGlobal.SetInfoMessage(ReturnMsg, lblMessage);
                    }
                    else // if updating data
                    {
                        oProdPlan.DbType = EnumDbType.UPDATE;
                        string ReturnMsg = oDal.ManageProductionPlan(oProdPlan).Rows[0]["RESULT"].ToString();
                        if (ReturnMsg.ToUpper() == "Y")
                        {
                            btnReset_Click(sender, e);
                            ClsGlobal.SetConfirmMessage("Updated successfully!!", lblMessage);
                        }
                        else
                            ClsGlobal.SetInfoMessage(ReturnMsg, lblMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    ClsGlobal.SetErrorMessage("Model No already exist in the selected month!!", lblMessage);
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
                cmbSearch.SelectedIndex = 0;
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
                if (cmbMonth.SelectedIndex <= 0 || cmbModel.SelectedIndex <= 0)
                {
                    ClsGlobal.SetInfoMessage("Please select month", lblMessage);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("Äre you sure to delete the record !!", ClsGlobal.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    oProdPlan.Month = cmbMonth.SelectedItem.ToString();
                    oProdPlan.ModelNo = cmbModel.SelectedItem.ToString();
                    oProdPlan.DbType = EnumDbType.DELETE;
                    string ReturnMsg = oDal.ManageProductionPlan(oProdPlan).Rows[0]["RESULT"].ToString();
                    if (ReturnMsg.ToUpper() == "Y")
                    {
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Deleted successfully!!", lblMessage);
                    }
                    else
                        ClsGlobal.SetInfoMessage(ReturnMsg, lblMessage);
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
                cmbMonth.SelectedIndex = 0;
                cmbModel.SelectedIndex = 0;
                txtOrderNo.Text = "";
                txtQty.Text = "";
                lblMessage.Text = "";
                cmbModel.Enabled = true;
                cmbMonth.Enabled = true;
                btnDelete.Enabled = false;
                _IsUpdate = false;
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
                //Bind Month
                cmbMonth.Items.Add("--Select--");
                cmbMonth.Items.Add(DateTime.Now.ToString("MMM-yyyy"));
                for (int i = 1; i <= 12 - DateTime.Now.Month; i++)
                {
                    string Month = DateTime.Now.AddMonths(i).ToString("MMM-yyyy");
                    cmbMonth.Items.Add(Month);
                }

                //Bind Search Month
                cmbSearch.Items.Add("--Search By Month--");
                string Year = DateTime.Now.Year.ToString();
                cmbSearch.Items.Add("Jan-" + Year);
                cmbSearch.Items.Add("Feb-" + Year);
                cmbSearch.Items.Add("Mar-" + Year);
                cmbSearch.Items.Add("Apr-" + Year);
                cmbSearch.Items.Add("May-" + Year);
                cmbSearch.Items.Add("Jun-" + Year);
                cmbSearch.Items.Add("Jul-" + Year);
                cmbSearch.Items.Add("Aug-" + Year);
                cmbSearch.Items.Add("Sep-" + Year);
                cmbSearch.Items.Add("Oct-" + Year);
                cmbSearch.Items.Add("Nov-" + Year);
                cmbSearch.Items.Add("Dec-" + Year);

                //Bind Mondel
                DataTable dt = oDal.ManageModel(new Model { DbType = EnumDbType.SELECT });
                cmbModel.Items.Add("--Select--");
                foreach (DataRow row in dt.Rows)
                    cmbModel.Items.Add(row["ModelNo"].ToString());

                cmbMonth.SelectedIndex = 0;
                cmbSearch.SelectedIndex = 0;
                cmbModel.SelectedIndex = 0;
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
                oProdPlan.DbType = EnumDbType.SELECT;
                DataTable dt = oDal.ManageProductionPlan(oProdPlan);
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
                if (cmbMonth.SelectedIndex <= 0)
                {
                    ClsGlobal.SetInfoMessage("Please select month!!", lblMessage);
                    cmbMonth.Focus();
                    return false;
                }
                if (cmbModel.SelectedIndex <= 0)
                {
                    ClsGlobal.SetInfoMessage("Please select model!!", lblMessage);
                    cmbModel.Focus();
                    return false;
                }
                if (txtOrderNo.Text.Trim().Length == 0 || int.Parse(txtOrderNo.Text.Trim()) == 0)
                {
                    ClsGlobal.SetInfoMessage("Please input production order no!!", lblMessage);
                    txtOrderNo.Focus();
                    return false;
                }
                if (txtQty.Text.Trim().Length == 0 || int.Parse(txtQty.Text.Trim()) == 0)
                {
                    ClsGlobal.SetInfoMessage("Please input production qty!!", lblMessage);
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
                cmbMonth.SelectedItem = dgv.Rows[e.RowIndex].Cells["Month"].Value.ToString();
                cmbModel.SelectedItem = dgv.Rows[e.RowIndex].Cells["ModelNo"].Value.ToString();
                txtOrderNo.Text = dgv.Rows[e.RowIndex].Cells["OrderNo"].Value.ToString();
                txtQty.Text = dgv.Rows[e.RowIndex].Cells["Qty"].Value.ToString();

                btnDelete.Enabled = true;
                cmbMonth.Enabled = false;
                cmbModel.Enabled = false;
                _IsUpdate = true;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

        #region TextBox Event

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion

        #region ComboBox Event
        private void cmbSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                oProdPlan.DbType = EnumDbType.SEARCH;
                oProdPlan.Month = cmbSearch.SelectedIndex > 0 ? cmbSearch.SelectedItem.ToString() : "";
                DataTable dt = oDal.ManageProductionPlan(oProdPlan);
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
