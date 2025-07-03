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
    public partial class frmNgPercentage : Form
    {
        #region Variables

        Dal oDal;

        #endregion

        #region Form Methods

        public frmNgPercentage()
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
                GetNgData();
                txtNgPercentage.Focus();
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
                if (string.IsNullOrEmpty(txtNgPercentage.Text))
                {
                    ClsGlobal.SetInfoMessage("Input % Value", lblMessage);
                    txtNgPercentage.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtNgPercentage.Text) <= 0)
                {
                    ClsGlobal.SetInfoMessage("Input % Value", lblMessage);
                    txtNgPercentage.Text = "";
                    txtNgPercentage.Focus();
                    return;
                }

                oDal.ManageNgPerentage(EnumDbType.UPDATE, Convert.ToDecimal(txtNgPercentage.Text.Trim()));
                btnReset_Click(sender, e);
                ClsGlobal.SetConfirmMessage("Saved Successfully!!", lblMessage);

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
                txtNgPercentage.Text = "";
                txtNgPercentage.Focus();
                GetNgData();
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

        #region Label Event
        private void lblMessage_DoubleClick(object sender, EventArgs e)
        {
            ClsGlobal.ShowInfoMessageBox(lblMessage.Text);
        }

        #endregion

        #region TextBox Events

        private void txtNgPercentage_KeyPress(object sender, KeyPressEventArgs e)
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
        private void GetNgData()
        {
            try
            {
                txtNgPercentage.Text = oDal.ManageNgPerentage(EnumDbType.SELECT).Rows[0]["NgPerValue"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
