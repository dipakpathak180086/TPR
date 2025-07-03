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
    public partial class frmLogin : Form
    {
        #region Variables

        Dal oDal;
        User oUser;

        #endregion

        #region Form Methods

        public frmLogin()
        {
            try
            {
                InitializeComponent();
                oUser = new User();
                oDal = new Dal();
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void frmModelMaster_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;

                lblMessage.Text = "";
                lblVersion.Text = "App Version : " + Application.ProductVersion;
                GetLine();
                txtUserId.Focus();
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void OFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            txtUserId.Text = "";
            txtPassword.Text = "";
            txtUserId.Focus();
            cmbLineNo.SelectedIndex = 0;
            this.Show();
        }

        #endregion

        #region Button Event

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (string.IsNullOrEmpty(txtUserId.Text))
                {
                    ClsGlobal.SetInfoMessage("Enter User Id", lblMessage);
                    txtUserId.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    ClsGlobal.SetInfoMessage("Enter Password", lblMessage);
                    txtPassword.Focus();
                    return;
                }
                if (cmbLineNo.SelectedIndex <= 0)
                {
                    ClsGlobal.SetInfoMessage("Please select Line No", lblMessage);
                    txtPassword.Focus();
                    return;
                }
                oUser.UserId = txtUserId.Text.Trim();
                oUser.Password = txtPassword.Text.Trim();
                oUser.DbType = EnumDbType.VALIDATEUSER;
                DataTable dt = oDal.ManageUser(oUser);
                if (dt.Rows.Count > 0)
                {
                    //Get Shift
                    ClsGlobal.Shift = oDal.GetShift().Rows[0]["ShiftName"].ToString();
                    //Get TimerTime Setting
                    DataTable dtTimer = oDal.GetTimerTime("Time");
                    int AutoLogOut = Convert.ToInt32(dtTimer.Rows[0]["TimeInMin"]);
                    int ReOiling = Convert.ToInt32(dtTimer.Rows[1]["TimeInMin"]);

                    ClsGlobal.UserId = txtUserId.Text.Trim();
                    ClsGlobal.UserName = dt.Rows[0]["UserName"].ToString();
                    ClsGlobal.UserGroup = dt.Rows[0]["GroupName"].ToString();
                    ClsGlobal.LineNo = cmbLineNo.SelectedItem.ToString().Split('-')[0].Trim();
                    frmMenu oFrm = new frmMenu(AutoLogOut, ReOiling);
                    oFrm.Show();
                    oFrm.FormClosing += OFrm_FormClosing;
                    this.Hide();
                }
                else
                {
                    txtUserId.Text = "";
                    txtPassword.Text = "";
                    ClsGlobal.SetInfoMessage("Wrong UserId/Password", lblMessage);
                    txtUserId.Focus();
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region TextBox Event
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin_Click(sender, e);
        }

        #endregion

        #region Methods

        private void GetLine()
        {
            try
            {
                cmbLineNo.Items.Add("--Select Line--");
                DataTable dt = oDal.ManageLine(new Line { DbType = EnumDbType.SELECT });
                foreach (DataRow row in dt.Rows)
                    cmbLineNo.Items.Add(row["Line_No"].ToString().Trim() + "-" + row["Description"].ToString().Trim());
                cmbLineNo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion
    }
}
