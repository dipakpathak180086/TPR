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
    public partial class frmTrolleyReceiving : Form
    {
        #region Variables

        Dal oDal;
        string _SelectedColor, _TrayQty, _Id;
        #endregion

        #region Form Methods

        public frmTrolleyReceiving(string TrayQty, string ColorName, string Id)
        {
            try
            {
                InitializeComponent();
                oDal = new Dal();
                _TrayQty = TrayQty;
                _SelectedColor = ColorName;
                _Id = Id;
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
                GetColor();
                txtTrayQty.Focus();
                txtTrayQty.Text = _TrayQty;
                cmbColor.SelectedItem = _SelectedColor;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

        #region Button Event

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                if (string.IsNullOrEmpty(txtTrayQty.Text))
                {
                    ClsGlobal.SetInfoMessage("Input tray Qty", lblMessage);
                    txtTrayQty.Focus();
                    return;
                }
                if (Convert.ToInt32(txtTrayQty.Text) <= 0)
                {
                    ClsGlobal.SetInfoMessage("Input tray Qty", lblMessage);
                    txtTrayQty.Text = "";
                    txtTrayQty.Focus();
                    return;
                }
                if (cmbColor.SelectedIndex <= 0)
                {
                    ClsGlobal.SetInfoMessage("Select Color", lblMessage);
                    cmbColor.Focus();
                    return;
                }

                oDal.GetTrolleyReceivingReportData("3", "", "", "", "0", Convert.ToInt32(txtTrayQty.Text), cmbColor.SelectedItem.ToString(), _Id);
                this.Close();
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
        private void GetColor()
        {
            try
            {
                DataTable dt = oDal.ManageColor(new Colors { DbType = EnumDbType.SELECT });
                cmbColor.Items.Add("--Select--");
                for (int i = 0; i < dt.Rows.Count; i++)
                    cmbColor.Items.Add(dt.Rows[i]["ColorName"].ToString());
                cmbColor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
