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
    public partial class frmQA : Form
    {
        #region Variables

        Dal oDal;
        QA oQA;

        #endregion

        #region Form Methods

        public frmQA()
        {
            try
            {
                InitializeComponent();
                oQA = new QA();
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
                this.WindowState = FormWindowState.Normal;

                lblMessage.Text = "";
                txtTrolleyCard.Focus();
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
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
                if (string.IsNullOrEmpty(txtTrolleyCard.Text))
                {
                    ClsGlobal.SetInfoMessage("Scan/Enter Trolley Card", lblMessage);
                    txtTrolleyCard.Focus();
                    return;
                }
                if (rdbOk.Checked == false && rdbNg.Checked == false && rdbHold.Checked == false && rdbPartialNG.Checked == false)
                {
                    ClsGlobal.SetInfoMessage("Please select QA option", lblMessage);
                    return;
                }
                if (txtOkQty.Text.Trim() == "" || txtOkQty.Text.Trim() == "0")
                {
                    ClsGlobal.SetInfoMessage("Qty details not found,please check", lblMessage);
                    return;
                }
                if (rdbPartialNG.Checked)
                {
                    if (cmbLotNo.SelectedIndex < 0)
                    {
                        ClsGlobal.SetInfoMessage("Select Lot No", lblMessage);
                        cmbLotNo.Focus();
                        return;
                    }
                    if (txtOkQty.Text.Trim() == "" || txtOkQty.Text.Trim() == "0")
                    {
                        ClsGlobal.SetInfoMessage("Qty details not found,please check", lblMessage);
                        return;
                    }
                    if (txtNgQty.Text.Trim() == "" || txtNgQty.Text.Trim() == "0")
                    {
                        ClsGlobal.SetInfoMessage("Input ng qty", lblMessage);
                        return;
                    }
                    if (Convert.ToInt32(txtNgQty.Text.Trim()) > Convert.ToInt32(txtOkQty.Text))
                    {
                        ClsGlobal.SetInfoMessage("Ng qty can not be greater than qty", lblMessage);
                        txtNgQty.Text = "";
                        txtNgQty.Focus();
                        return;
                    }
                    if (txtNgReason.Text.Trim() == "")
                    {
                        ClsGlobal.SetInfoMessage("Input ng reason", lblMessage);
                        txtNgReason.Focus();
                        return;
                    }
                }
                //bool Status = ValidateTrolley(txtTrolleyCard.Text.Trim());
                //if (Status)
                //{
                //In case of partial ng,trolley will be in ok status except partial ng qty
                oQA.DbType = EnumDbType.UPDATE;
                oQA.TrolleyCard = txtTrolleyCard.Text.Trim();
                oQA.Shift = oDal.GetShift().Rows[0]["ShiftName"].ToString();
                oQA.Status = Convert.ToInt32(EnumCuttingStatus.QC_Ok); //in case of parial ng then status will be ok
                oQA.CreatedBy = ClsGlobal.UserId;
                oQA.PartialNgQty = rdbPartialNG.Checked ? int.Parse(txtNgQty.Text.Trim()) : 0;
                oQA.PartialNgReason = txtNgReason.Text.Trim();
                oQA.LotNo = cmbLotNo.SelectedIndex >= 0 ? cmbLotNo.SelectedValue.ToString().Split('#')[0].Trim() : "";
                if (rdbOk.Checked)
                    oQA.Status = Convert.ToInt32(EnumCuttingStatus.QC_Ok);
                else if (rdbNg.Checked)
                    oQA.Status = Convert.ToInt32(EnumCuttingStatus.QC_Ng);
                else if (rdbHold.Checked)
                    oQA.Status = Convert.ToInt32(EnumCuttingStatus.QC_Hold);

                oDal.ManageQA(oQA);
                btnReset_Click(sender, e);
                ClsGlobal.SetConfirmMessage("Saved Successfully!!", lblMessage);
                // }
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
                lblMessage.Text = "";
                txtTrolleyCard.Text = "";
                rdbOk.Checked = false;
                rdbNg.Checked = false;
                rdbHold.Checked = false;
                rdbPartialNG.Checked = false;
                txtNgReason.Text = "";
                txtNgQty.Text = "";
                txtOkQty.Text = "";
                cmbLotNo.DataSource = null;
                txtTrolleyCard.Focus();
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

        #region TextBox Events
        private void txtTrolleyCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (e.KeyChar == 13)
                {
                    if (string.IsNullOrEmpty(txtTrolleyCard.Text))
                    {
                        ClsGlobal.SetInfoMessage("Scan/Enter Trolley Card", lblMessage);
                        txtTrolleyCard.Focus();
                        return;
                    }
                    ValidateTrolley(txtTrolleyCard.Text.Trim());
                }
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

        #region Methods

        private bool ValidateTrolley(string TrolleyBarcode)
        {
            try
            {
                cmbLotNo.DataSource = null;
                txtNgQty.Text = "";
                txtNgReason.Text = "";
                txtOkQty.Text = "";
                oQA.DbType = EnumDbType.SELECT;
                oQA.TrolleyCard = TrolleyBarcode;
                DataTable dt = oDal.ManageQA(oQA);
                if (dt.Rows.Count > 0)
                {
                    int Status = Convert.ToInt32(dt.Rows[0]["Status"]);
                    if (Status <= 5)
                    {
                        cmbLotNo.DataSource = dt;
                        cmbLotNo.DisplayMember = "LotNo";
                        cmbLotNo.ValueMember = "LotNo";
                        cmbLotNo_SelectionChangeCommitted(null, null);
                        return true;
                    }
                    else
                    {
                        ClsGlobal.SetInfoMessage("Invalid Status", lblMessage);
                        txtTrolleyCard.Text = "";
                        txtTrolleyCard.Focus();
                        return false;
                    }
                }
                else
                {
                    ClsGlobal.SetInfoMessage("Invalid trolley card", lblMessage);
                    txtTrolleyCard.Text = "";
                    txtTrolleyCard.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Label Event
        private void lblMessage_DoubleClick(object sender, EventArgs e)
        {
            ClsGlobal.ShowInfoMessageBox(lblMessage.Text);
        }

        #endregion

        #region Radio Button Events
        private void QACheckChange(object sender, EventArgs e)
        {
            try
            {
                txtNgReason.Text = "";
                txtNgQty.Text = "";
                if (rdbPartialNG.Checked)
                {
                    pnlNg.Visible = true;
                }
                else
                    pnlNg.Visible = false;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

        #region ComboBox Event
        private void cmbLotNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                txtOkQty.Text = cmbLotNo.SelectedValue.ToString().Split('#')[1].Trim();
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion
    }
}
