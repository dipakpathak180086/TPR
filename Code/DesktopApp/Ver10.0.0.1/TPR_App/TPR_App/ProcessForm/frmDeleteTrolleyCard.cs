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
    public partial class frmDeleteTrolleyCard : Form
    {
        #region Variables

        Dal oDal;
        #endregion

        #region Form Methods

        public frmDeleteTrolleyCard()
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
                txtSearchFilter.Focus();
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
        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (txtSearchFilter.Text.Trim() == "")
                {
                    ClsGlobal.SetInfoMessage("Enter Trolley", lblMessage);
                    txtSearchFilter.Focus();
                    return;
                }

                EnumProcess enumProcess = EnumProcess.CUTTING;
                string TrolleyNo = "";
                if (rdbMaching.Checked)
                {
                    enumProcess = EnumProcess.MACHINING;
                }
                else if (rdbFinalPacking.Checked)
                {
                    enumProcess = EnumProcess.FINAL_PACKING;
                    TrolleyNo = txtSearchFilter.Text.Trim();
                }

                DataTable dt = oDal.DeleteTrolleyCard(EnumDbType.SELECT, enumProcess, txtSearchFilter.Text.Trim(), TrolleyNo);
                if (dt.Rows.Count > 0)
                {
                    string Message = dt.Rows[0]["Result"].ToString();
                    if (Message == "Y")
                        txtOkQty.Text = dt.Rows[0]["Qty"].ToString();
                    else
                        ClsGlobal.ShowInfoMessageBox(Message);
                }
                else
                {
                    ClsGlobal.SetInfoMessage("No reponse from db", lblMessage);
                    txtSearchFilter.Text = "";
                    txtSearchFilter.Focus();
                }
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
                if (txtSearchFilter.Text.Trim() == "")
                {
                    ClsGlobal.SetInfoMessage("Enter Trolley", lblMessage);
                    txtSearchFilter.Focus();
                    return;
                }
                if (txtReason.Text.Trim() == "")
                {
                    ClsGlobal.SetInfoMessage("Enter Reason", lblMessage);
                    txtSearchFilter.Focus();
                    return;
                }
                EnumProcess enumProcess = EnumProcess.CUTTING;
                string TrolleyNo = "";
                if (rdbMaching.Checked)
                {
                    enumProcess = EnumProcess.MACHINING;
                }
                else if (rdbFinalPacking.Checked)
                {
                    enumProcess = EnumProcess.FINAL_PACKING;
                    TrolleyNo = txtSearchFilter.Text.Trim();
                }
                if (MessageBox.Show("Do you really want to delete ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataTable dt = oDal.DeleteTrolleyCard(EnumDbType.DELETE, enumProcess, txtSearchFilter.Text.Trim(), TrolleyNo, txtReason.Text.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        string Message = dt.Rows[0]["Result"].ToString();
                        if (Message == "Y")
                        {
                            btnReset_Click(sender, e);
                            ClsGlobal.SetConfirmMessage("Deleted sucessfully",lblMessage);
                        }
                        else
                            ClsGlobal.SetInfoMessage(Message, lblMessage);
                    }
                    else
                    {
                        ClsGlobal.SetInfoMessage("No reponse from db", lblMessage);
                        txtSearchFilter.Text = "";
                        txtSearchFilter.Focus();
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
                txtSearchFilter.Text = "";
                lblFilter.Text = "Enter Trolley Card";
                rdbCutting.Checked = true;
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

        #region Label Event
        private void lblMessage_DoubleClick(object sender, EventArgs e)
        {
            ClsGlobal.ShowInfoMessageBox(lblMessage.Text);
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
                    btnGo_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }


        private void txtLot1_KeyPress(object sender, KeyPressEventArgs e)
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

        #region RadioButton Event
        private void rdbFinalPacking_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbFinalPacking.Checked)
                lblFilter.Text = "Enter Trolley No.";
            else
                lblFilter.Text = "Enter Trolley Card";
        }

        #endregion

        #region Methods

        private void Clear()
        {
            try
            {
                lblMessage.Text = "";
                txtReason.Text = "";
                txtOkQty.Text = "";
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion
    }
}
