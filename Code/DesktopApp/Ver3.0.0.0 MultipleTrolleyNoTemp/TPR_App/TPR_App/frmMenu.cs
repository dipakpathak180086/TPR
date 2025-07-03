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
    public partial class frmMenu : Form
    {
        #region Variables

        Dal oDal;
        User oUser;

        int _AutoLogoutTimerMin = 0;
        int _ReOilingTimerMin = 0;

        #endregion

        #region Form Methods

        public frmMenu(int AutoLogOutTime,int ReOilingTime)
        {
            try
            {
                InitializeComponent();
                oUser = new User();
                oDal = new Dal();

                _AutoLogoutTimerMin = AutoLogOutTime;
                _ReOilingTimerMin = ReOilingTime;
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        private void frmModelMaster_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

                lblWelcome.Text = "Hi! " + ClsGlobal.UserName;
                SetMenuRight();

                //AutoLogOut timer
                //timerAutoLogOut.Interval = _AutoLogoutTimerMin * 60 * 1000;
                //timerAutoLogOut.Enabled = true;
                //Reoiling Counter Timer
                timerReOiling.Interval = _ReOilingTimerMin * 60 * 1000;
                timerReOiling.Enabled = true;
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        private void OFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        #endregion

        #region Button Event

        private void picLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Menu Click Events
        private void picChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword oFrm = new frmChangePassword();
            oFrm.ShowDialog();
        }
        private void picUserMaster_Click(object sender, EventArgs e)
        {
            frmUserMaster frm = new frmUserMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picGroupMaster_Click(object sender, EventArgs e)
        {
            frmGroupMaster frm = new frmGroupMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picMachineMaster_Click(object sender, EventArgs e)
        {
            frmMachineMaster frm = new frmMachineMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picLineMaster_Click(object sender, EventArgs e)
        {
            frmLineMaster frm = new frmLineMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picLocationMaster_Click(object sender, EventArgs e)
        {
            frmLocationMaster frm = new frmLocationMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picModelMaster_Click(object sender, EventArgs e)
        {
            frmModelMaster frm = new frmModelMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picShiftMaster_Click(object sender, EventArgs e)
        {
            frmShiftMaster frm = new frmShiftMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picTrolleryMaster_Click(object sender, EventArgs e)
        {
            frmTrolleyMaster frm = new frmTrolleyMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picProductionPlan_Click(object sender, EventArgs e)
        {
            frmProductionPlan frm = new frmProductionPlan();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picCutting_Click(object sender, EventArgs e)
        {
            frmCutting frm = new frmCutting();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picDispatchOrder_Click(object sender, EventArgs e)
        {
            frmDispatchOrder frm = new frmDispatchOrder();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmCustomerTrolleyReceiving frm = new frmCustomerTrolleyReceiving();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picCustomerMaster_Click(object sender, EventArgs e)
        {
            frmCustomerMaster frm = new frmCustomerMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picQA_Click(object sender, EventArgs e)
        {
            frmQA frm = new frmQA();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picQASampling_Click(object sender, EventArgs e)
        {
            frmQASampling frm = new frmQASampling();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picRptCutting_Click(object sender, EventArgs e)
        {
            frmRptCutting frm = new frmRptCutting();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picReprint_Click(object sender, EventArgs e)
        {
            frmReprint frm = new frmReprint();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picRptQA_Click(object sender, EventArgs e)
        {
            frmRptQA frm = new frmRptQA();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picCuttingCardUpdate_Click(object sender, EventArgs e)
        {
            frmCuttingCardUpdate frm = new frmCuttingCardUpdate();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picReOiling_Click(object sender, EventArgs e)
        {
            frmRptReOiling frm = new frmRptReOiling(false);
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void lblReOilingCount_Click(object sender, EventArgs e)
        {
            frmRptReOiling frm = new frmRptReOiling(true);
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picMachining_Click(object sender, EventArgs e)
        {
            frmRptMachining frm = new frmRptMachining();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picColorMaster_Click(object sender, EventArgs e)
        {
            frmColorMaster frm = new frmColorMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();

        }

        private void picFinalPacking_Click(object sender, EventArgs e)
        {
            frmRptFinalPacking frm = new frmRptFinalPacking();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picDispatchReport_Click(object sender, EventArgs e)
        {
            frmRptDispatch frm = new frmRptDispatch();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picInventoryReport_Click(object sender, EventArgs e)
        {
            frmRptInventory frm = new frmRptInventory();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        #endregion

        #region Method

        private void SetMenuRight()
        {
            try
            {
                DataTable dt = oDal.GetUserRight(ClsGlobal.UserGroup);
                foreach (DataRow row in dt.Rows)
                {
                    switch (row["ModuleId"].ToString())
                    {
                        case "101":
                            picGroupMaster.Enabled = true;
                            lblGroupMaster.Enabled = true;
                            break;
                        case "102":
                            picUserMaster.Enabled = true;
                            lblUserMaster.Enabled = true;
                            break;
                        case "103":
                            picMachineMaster.Enabled = true;
                            lblMachineMaster.Enabled = true;
                            break;
                        case "104":
                            picLineMaster.Enabled = true;
                            lblLineMaster.Enabled = true;
                            break;
                        case "105":
                            picCustomerMaster.Enabled = true;
                            lblCustomerMaster.Enabled = true;
                            break;
                        case "106":
                            picModelMaster.Enabled = true;
                            lblModelMaster.Enabled = true;
                            break;
                        case "107":
                            picShiftMaster.Enabled = true;
                            lblShiftMaster.Enabled = true;
                            break;
                        case "108":
                            picTrolleryMaster.Enabled = true;
                            lblTrolleyMaster.Enabled = true;
                            break;
                        case "109":
                            picColorMaster.Enabled = true;
                            lblColorMaster.Enabled = true;
                            break;
                        case "201":
                            picProductionPlan.Enabled = true;
                            lblProductionPlan.Enabled = true;
                            break;
                        case "202":
                            picCutting.Enabled = true;
                            lblCutting.Enabled = true;
                            break;
                        case "203":
                            picQA.Enabled = true;
                            lblQA.Enabled = true;
                            break;
                        case "204":
                            picQASampling.Enabled = true;
                            lblQASampling.Enabled = true;
                            break;
                        case "205":
                            picDispatchOrder.Enabled = true;
                            lblDispatchOrder.Enabled = true;
                            break;
                        case "211":
                            picReprint.Enabled = true;
                            lblReprint.Enabled = true;
                            break;
                        case "212":
                            picCuttingCardUpdate.Enabled = true;
                            lblCuttingCardUpdat.Enabled = true;
                            break;
                        case "301":
                            picRptCutting.Enabled = true;
                            lblRptCutting.Enabled = true;
                            break;
                        case "302":
                            picRptQA.Enabled = true;
                            lblRptQA.Enabled = true;
                            break;
                        case "303":
                            picReOiling.Enabled = true;
                            lblReOiling.Enabled = true;
                            lblReOilingCount.Enabled = true;
                            break;
                        case "304":
                            picMachining.Enabled = true;
                            lblMachining.Enabled = true;
                            break;
                        case "305":
                            picFinalPacking.Enabled = true;
                            lblFinalPacking.Enabled = true;
                            break;
                        case "306":
                            picDispatchReport.Enabled = true;
                            lblDispatchReport.Enabled = true;
                            break;
                        case "307":
                            picInventoryReport.Enabled = true;
                            lblInventoryReport.Enabled = true;
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        #endregion

        #region Timer Event
        private void timerAutoLogOut_Tick(object sender, EventArgs e)
        {
            try
            {
                //string Shift = oDal.GetShift().Rows[0]["ShiftName"].ToString();
                //if (Shift.Trim().ToUpper() != ClsGlobal.Shift.Trim().ToUpper())
                //{
                //    timerAutoLogOut.Enabled = false;
                //    ClsGlobal.ShowInfoMessageBox("Hi, Shift has changed and you did not logout so appllication will be closed. Please start the application and login again");
                //    Application.Exit();
                //}
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        private void timerReOiling_Tick(object sender, EventArgs e)
        {
            try
            {
                lblReOilingCount.Text = "0";
                DataTable dtTimer = oDal.GetTimerTime();
                if (dtTimer.Rows.Count > 0)
                    lblReOilingCount.Text = dtTimer.Rows[0]["Total"].ToString();
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }



        #endregion
    }
}
