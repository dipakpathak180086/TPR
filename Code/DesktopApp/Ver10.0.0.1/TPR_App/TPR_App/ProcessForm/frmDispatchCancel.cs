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
    public partial class frmDispatchCancel : Form
    {
        #region Variables

        Dal oDal;

        #endregion

        #region Form Methods

        public frmDispatchCancel()
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
                string ModelNo = cmbModelNo.SelectedIndex > 0 ? cmbModelNo.SelectedItem.ToString() : "";
                string CustomerId = cmbCustomer.SelectedIndex > 0 ? cmbCustomer.SelectedValue.ToString() : "0";
                DataTable dt = oDal.GetDispatchDataForCancel(dtpDispatchDate.Text, ModelNo, CustomerId);
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
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
                if (txtReason.Text.Trim() == "")
                {
                    ClsGlobal.SetInfoMessage("Enter Reason", lblMessage);
                    txtReason.Focus();
                    return;
                }

                if (dgv.Rows.Count > 0)
                {
                    //Check any item is selected or not
                    bool IsSelected = false;
                    foreach (DataGridViewRow Row in dgv.Rows)
                    {
                        if (Convert.ToBoolean(Row.Cells["Select"].Value) == true)
                        {
                            IsSelected = true;
                            break;
                        }
                    }
                    if (IsSelected == false)
                        ClsGlobal.ShowInfoMessageBox("Please select at least one record to delete");
                    else
                    {
                        if (MessageBox.Show("Do you really want to delete ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            oDal.CancelDispatch(dgv, txtReason.Text.Trim());
                            btnReset_Click(sender, e);
                            ClsGlobal.SetConfirmMessage("Deleted successfully!!", lblMessage);
                        }
                    }
                }
                else
                {
                    ClsGlobal.ShowInfoMessageBox("No data found");
                }
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
                txtReason.Text = "";
                dgv.DataSource = null;
                chkAll.Checked = false;
                lblCount.Text = "Rows Count : 0";
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
                    cmbModelNo.Items.Add(row["ModelNo"].ToString());
                cmbCustomer.SelectedIndex = 0;
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

        #region CheckBox Event
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.Cells["Select"].Value = chkAll.Checked;
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
