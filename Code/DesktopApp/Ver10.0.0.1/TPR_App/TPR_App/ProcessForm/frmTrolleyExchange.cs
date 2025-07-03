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
    public partial class frmTrolleyExchange : Form
    {
        #region Variables

        Dal oDal;
        QA oQA;

        #endregion

        #region Form Methods

        public frmTrolleyExchange()
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
                txtNewTrolleyNo.Enabled = false;
                txtOldTrolleyNo.Focus();
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
                if (string.IsNullOrEmpty(txtOldTrolleyNo.Text))
                {
                    ClsGlobal.SetInfoMessage("Scan/Enter Old Trolley No", lblMessage);
                    txtOldTrolleyNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtNewTrolleyNo.Text))
                {
                    ClsGlobal.SetInfoMessage("Scan/Enter New Trolley No", lblMessage);
                    txtNewTrolleyNo.Focus();
                    return;
                }
                bool Status1 = ValidateOldTrolley(txtOldTrolleyNo.Text.Trim());
                bool Status2 = ValidateNewTrolley(txtNewTrolleyNo.Text.Trim());
                if (Status1 && Status2)
                {
                    if (ValidateField())
                    {
                        oDal.SaveTrolleyExchange("SAVE",txtOldTrolleyNo.Text.Trim(),txtNewTrolleyNo.Text.Trim(), ClsGlobal.UserId);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Trolley Exchange Successfully!!", lblMessage);
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
                txtOldTrolleyNo.Text = "";
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
                    if (string.IsNullOrEmpty(txtOldTrolleyNo.Text))
                    {
                        ClsGlobal.SetInfoMessage("Scan/Enter Old Trolley No", lblMessage);
                        txtOldTrolleyNo.Focus();
                        return;
                    }
                    //Clear();
                    if (ValidateOldTrolley(txtOldTrolleyNo.Text.Trim()))
                    {
                        txtOldTrolleyNo.Enabled = false;
                        txtNewTrolleyNo.Enabled = true;
                        txtNewTrolleyNo.Focus();
                    }
                    
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
                txtOldTrolleyNo.Text = "";
                txtNewTrolleyNo.Text = "";
                txtNewTrolleyNo.Enabled = false;
                txtOldTrolleyNo.Enabled = true;
            }
            catch (Exception ex) { throw ex; }
        }
        private bool ValidateField()
        {
            try
            {
                if (string.IsNullOrEmpty(txtOldTrolleyNo.Text))
                {
                    ClsGlobal.SetInfoMessage("Scan/Enter Old Trolley No", lblMessage);
                    txtOldTrolleyNo.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtNewTrolleyNo.Text))
                {
                    ClsGlobal.SetInfoMessage("Scan/Enter New Trolley No", lblMessage);
                    txtNewTrolleyNo.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        private bool ValidateOldTrolley(string OldTrolleyNo)
        {
            try
            {
                DataTable dt = oDal.SaveTrolleyExchange("VALIDATE_OLD_TROLLEY",OldTrolleyNo);
                if (dt.Rows[0]["Result"].ToString().ToUpper() != "Y")
                {
                    ClsGlobal.SetInfoMessage(dt.Rows[0]["Result"].ToString(), lblMessage);
                    txtOldTrolleyNo.Text = "";
                    txtOldTrolleyNo.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidateNewTrolley(string NewTrolley)
        {
            try
            {
                DataTable dt = oDal.SaveTrolleyExchange("VALIDATE_NEW_TROLLEY","",NewTrolley);
                if (dt.Rows[0]["Result"].ToString().ToUpper() != "Y")
                {
                    ClsGlobal.SetInfoMessage(dt.Rows[0]["Result"].ToString(), lblMessage);
                    txtNewTrolleyNo.Text = "";
                    txtNewTrolleyNo.Focus();
                    return false;
                }
                else
                {
                   
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
