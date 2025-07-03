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
    public partial class frmQASampling : Form
    {
        #region Variables

        Dal oDal;
        QA oQA;

        #endregion

        #region Form Methods

        public frmQASampling()
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
                // Clear();
                if (string.IsNullOrEmpty(txtTrolleyCard.Text))
                {
                    ClsGlobal.SetInfoMessage("Scan/Enter Trolley Card", lblMessage);
                    txtTrolleyCard.Focus();
                    return;
                }
                bool Status = ValidateTrolley(txtTrolleyCard.Text.Trim());
                if (Status)
                {
                    if (ValidateField())
                    {
                        oQA.DbType = EnumDbType.UPDATE;
                        oQA.TrolleyCard = txtTrolleyCard.Text.Trim();
                        oQA.PickedQty = Convert.ToInt32(txtPickedQty.Text);
                        oQA.Status = Convert.ToInt32(EnumCuttingStatus.QC_Sample);
                        oQA.CreatedBy = ClsGlobal.UserId;

                        oDal.ManageQASample(oQA);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Saved Successfully!!", lblMessage);
                    }
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
                txtTrolleyCard.Text = "";
                Clear();
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
                    Clear();
                    ValidateTrolley(txtTrolleyCard.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void txtPickedQty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Clear()
        {
            try
            {
                lblMessage.Text = "";
                txtTotalQty.Text = "";
                txtPickedQty.Text = "";
            }
            catch (Exception ex) { throw ex; }
        }
        private bool ValidateField()
        {
            try
            {
                if (string.IsNullOrEmpty(txtTrolleyCard.Text))
                {
                    ClsGlobal.SetInfoMessage("Scan/Enter Trolley Card", lblMessage);
                    txtTrolleyCard.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtTotalQty.Text) || txtTotalQty.Text.Trim() == "0")
                {
                    ClsGlobal.SetInfoMessage("Total qty not found", lblMessage);
                    return false;
                }
                if (string.IsNullOrEmpty(txtPickedQty.Text) || txtTotalQty.Text.Trim() == "0")
                {
                    ClsGlobal.SetInfoMessage("Input picked qty", lblMessage);
                    txtPickedQty.Focus();
                    return false;
                }
                if (Convert.ToInt32(txtPickedQty.Text) > Convert.ToInt32(txtTotalQty.Text))
                {
                    ClsGlobal.SetInfoMessage("Picked qty can not be greater than total qty", lblMessage);
                    txtPickedQty.Text = "";
                    txtPickedQty.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        private bool ValidateTrolley(string TrolleyBarcode)
        {
            try
            {
                oQA.DbType = EnumDbType.SELECT;
                oQA.TrolleyCard = TrolleyBarcode;
                DataTable dt = oDal.ManageQASample(oQA);
                if (dt.Rows[0]["Result"].ToString().ToUpper() != "Y")
                {
                    ClsGlobal.SetInfoMessage(dt.Rows[0]["Result"].ToString(), lblMessage);
                    txtTrolleyCard.Text = "";
                    txtTrolleyCard.Focus();
                    return false;
                }
                else
                {
                    txtTotalQty.Text = dt.Rows[0]["Qty"].ToString();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

       
    }
}
