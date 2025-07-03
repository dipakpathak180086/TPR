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
    public partial class frmMachiningHold : Form
    {
        #region Variables

        Dal oDal;

        #endregion

        #region Form Methods

        public frmMachiningHold()
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
                
                bool Status = ValidateTrolley(txtTrolleyCard.Text.Trim());
                if (Status)
                {
                    oDal.ManageMachiningHoldUnHold(EnumDbType.UPDATE, txtTrolleyCard.Text.Trim(), rdbHold.Checked, ClsGlobal.UserId);
                    btnReset_Click(sender, e);
                    ClsGlobal.SetConfirmMessage("Saved Successfully!!", lblMessage);
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
                lblMessage.Text = "";
                txtTrolleyCard.Text = "";
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

        #endregion

        #region Methods

        private bool ValidateTrolley(string TrolleyBarcode)
        {
            try
            {
                string ReturnMsg = oDal.ManageMachiningHoldUnHold(EnumDbType.SELECT,TrolleyBarcode,false,"").Rows[0]["Result"].ToString();
                if (ReturnMsg.ToUpper() != "Y")
                {
                    ClsGlobal.SetInfoMessage(ReturnMsg, lblMessage);
                    txtTrolleyCard.Text = "";
                    txtTrolleyCard.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
