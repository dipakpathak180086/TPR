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
        int _ReOilingTimerMin = 0, _TrolleyCountTimerMin = 0;

        #endregion

        #region Form Methods

        public frmMenu(int AutoLogOutTime, int ReOilingTime, int TrolleyCount)
        {
            try
            {
                InitializeComponent();
                oUser = new User();
                oDal = new Dal();

                _AutoLogoutTimerMin = AutoLogOutTime;
                _ReOilingTimerMin = ReOilingTime;
                _TrolleyCountTimerMin = TrolleyCount;
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

                GetTrolleyCount();
                BindModelNo();

                //AutoLogOut timer
                //timerAutoLogOut.Interval = _AutoLogoutTimerMin * 60 * 1000;
                //timerAutoLogOut.Enabled = true;

                //Reoiling Counter Timer
                timerReOiling.Interval = _ReOilingTimerMin * 60 * 1000;
                timerReOiling.Enabled = true;

                //TrolleyCount Counter Timer
                timerTrolleyCount.Interval = _TrolleyCountTimerMin * 60 * 1000;
                //timerTrolleyCount.Enabled = true;
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
        private void pictureBoxDispatchCancel_Click(object sender, EventArgs e)
        {
            frmDispatchCancel frm = new frmDispatchCancel();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void pictureBoxTrolleyReceivingHistory_Click(object sender, EventArgs e)
        {
            frmRptTrolleyReceiving frm = new frmRptTrolleyReceiving();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void pictureBoxMachiningHold_Click(object sender, EventArgs e)
        {
            frmMachiningHold frm = new frmMachiningHold();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void pictureBoxNgPer_Click(object sender, EventArgs e)
        {
            frmNgPercentage frm = new frmNgPercentage();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picAfterMachiningNg_Click(object sender, EventArgs e)
        {
            frmAfterMachiningNg frm = new frmAfterMachiningNg();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void pictureBoxPicking_Click(object sender, EventArgs e)
        {
            frmRptPicking frm = new frmRptPicking();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picCustomerReturn_Click(object sender, EventArgs e)
        {
            frmCustomerReturn frm = new frmCustomerReturn();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picDeleteTrolleyCard_Click(object sender, EventArgs e)
        {
            frmDeleteTrolleyCard frm = new frmDeleteTrolleyCard();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picFifoAction_Click(object sender, EventArgs e)
        {
            frmFIFOAction frm = new frmFIFOAction();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picHoldPacking_Click(object sender, EventArgs e)
        {
            frmAfterPackingHold frm = new frmAfterPackingHold();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();

        }

        private void picExchangeFinalPackingTrolley_Click(object sender, EventArgs e)
        {
            frmTrolleyExchange frm = new frmTrolleyExchange();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picTrolleyDeleteHistory_Click(object sender, EventArgs e)
        {
            frmRptTrolleyDeleteHis frm = new frmRptTrolleyDeleteHis();
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
                ClsGlobal.IsCuttingManualEnable = false;
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
                        case "110":
                            picWHMater.Enabled = true;
                            lblWHMaster.Enabled = true;
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
                        case "214":
                            pictureBoxDispatchCancel.Enabled = true;
                            lblDispatchCancel.Enabled = true;
                            break;
                        case "215":
                            pictureBoxMachiningHold.Enabled = true;
                            lblMachiningHold.Enabled = true;
                            break;
                        case "216":
                            pictureBoxNgPer.Enabled = true;
                            lblNGPerc.Enabled = true;
                            break;
                        case "217":
                            picAfterMachiningNg.Enabled = true;
                            lblAfterMachiningNg.Enabled = true;
                            break;
                        case "218":
                            picCustomerReturn.Enabled = true;
                            lblCustomerReturn.Enabled = true;
                            break;
                        case "219":
                            picDeleteTrolleyCard.Enabled = true;
                            lblDeleteTrolleyCard.Enabled = true;
                            break;
                        case "220":
                            picFifoAction.Enabled = true;
                            lblFifoAction.Enabled = true;
                            break;
                        case "221":
                            picHoldPacking.Enabled = true;
                            lblHoldPacking.Enabled = true;
                            break;
                        case "222":
                            picExchangeFinalPackingTrolley.Enabled = true;
                            lblExchangeFinalPackingTrolley.Enabled = true;
                            break;
                        case "223":
                            picInOutDeleteTrolley.Enabled = true;
                            lblInOutDeleteTrolley.Enabled = true;
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
                        case "308":
                            pictureBoxTrolleyReceivingHistory.Enabled = true;
                            lblTrolleyRecevingHistory.Enabled = true;
                            break;
                        case "309":
                            pictureBoxPicking.Enabled = true;
                            lblPicking.Enabled = true;
                            break;
                        case "310":
                            picTrolleyDeleteHistory.Enabled = true;
                            lblTrolleyDeletedHis.Enabled = true;
                            break;
                        case "401":
                            ClsGlobal.IsCuttingManualEnable = true;
                            break;

                        case "311":
                            picReprintHistory.Enabled = true;
                            lblReprintHistory.Enabled = true;
                            break;
                        case "312":
                            picTrolleyExchangeHistory.Enabled = true;
                            lblTrolleyExchangeHistory.Enabled = true;
                            break;
                        case "313":
                            picPendingMachiningPackingHistory.Enabled = true;
                            lblMachiningPackingHistory.Enabled = true;
                            break;
                        case "314":
                            picInOutReport.Enabled = true;
                            lblInOutReport.Enabled = true;
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

        private void BindModelNo()
        {
            try
            {

                //Bind Mondel
                DataTable dt = oDal.ManageModel(new Model { DbType = EnumDbType.SELECT });
                cmbModelNo.Items.Add("--Select--");
                foreach (DataRow row in dt.Rows)
                    cmbModelNo.Items.Add(row["ModelNo"].ToString());

                cmbModelNo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        private void GetTrolleyCount()
        {
            try
            {
                lblCuttingCount.Text = "0";
                lblMachiningCount.Text = "0";
                lblFinalPackCount.Text = "0";
                lblDispatchTrolleyCount.Text = "0";
                bool IsPreviousDay = rdbPreviousDay.Checked ? true : false;
                DataTable dtCount = oDal.GetDashBoardData(EnumDashBoardName.TROLLEYCOUNT, IsPreviousDay);
                if (dtCount.Rows.Count > 0)
                {
                    lblCuttingCount.Text = dtCount.Rows[0]["CuttingCount"].ToString();
                    lblMachiningCount.Text = dtCount.Rows[0]["MachiningCount"].ToString();
                    lblFinalPackCount.Text = dtCount.Rows[0]["FinalPackCount"].ToString();
                    lblDispatchTrolleyCount.Text = dtCount.Rows[0]["DispatchCount"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        private void GetProductionCount()
        {
            try
            {
                lblPPQty.Text = "0";
                lblPendingQty.Text = "0";
                lblOKQty.Text = "0";
                lblNgQty.Text = "0";
                lblNGPerc.Text = "0";
                lblNGPerc.BackColor = Color.Black;
                int ProdFlag = 1;//show all production data for current month
                if (rdbAll.Checked)
                    ProdFlag = 1;
                else if (rdbProdToday.Checked)
                    ProdFlag = 2;
                else
                    ProdFlag = 3;

                DataTable dtCount = oDal.GetDashBoardData(EnumDashBoardName.PRODUCTION, false, ProdFlag);
                if (dtCount.Rows.Count > 0)
                {
                    lblPPQty.Text = dtCount.Rows[0]["PPQty"].ToString();
                    lblPendingQty.Text = dtCount.Rows[0]["PendingQty"].ToString();
                    lblOKQty.Text = dtCount.Rows[0]["OkQty"].ToString();
                    lblNgQty.Text = dtCount.Rows[0]["NgQty"].ToString();
                    decimal TotalQty = Convert.ToDecimal(dtCount.Rows[0]["TotalQty"]);
                    decimal NgQty = Convert.ToDecimal(lblNgQty.Text);
                    decimal NgPer = (NgQty * 100) / (TotalQty == 0 ? 1 : TotalQty);
                    lblNGPerc.Text = Math.Round(NgPer, 2).ToString() + "%";
                    decimal NgPerColorValue = Convert.ToDecimal(dtCount.Rows[0]["NgPer"]);
                    if (NgPer > NgPerColorValue)
                        lblNGPerc.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        private void GetInventory()
        {
            try
            {
                lblInvCutting.Text = "0";
                lblInvMachining.Text = "0";
                lblInvFinalPacking.Text = "0";
                string ModelNo = cmbModelNo.SelectedIndex > 0 ? cmbModelNo.SelectedItem.ToString() : "";
                DataTable dtCount = oDal.GetDashBoardData(EnumDashBoardName.INVENTORY, false, 0, ModelNo);
                if (dtCount.Rows.Count > 0)
                {
                    lblInvCutting.Text = dtCount.Rows[0]["CuttingInventory"].ToString();
                    lblInvMachining.Text = dtCount.Rows[0]["MachiningInventory"].ToString();
                    lblInvFinalPacking.Text = dtCount.Rows[0]["FinalPackInventory"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        private void GetReceivingPending()
        {
            try
            {
                lblReceivingPendingCount.Text = "0";
                DataTable dtCount = oDal.GetDashBoardData(EnumDashBoardName.RECEIVING_PENDING, false, 0, "", chkNotUsedTrolley.Checked);
                if (dtCount.Rows.Count > 0)
                {
                    if (chkNotUsedTrolley.Checked == false)
                        lblReceivingPendingCount.Text = dtCount.AsEnumerable().Sum(x => x.Field<int>("TrolleyCount")).ToString();
                    else
                        lblReceivingPendingCount.Text = dtCount.Rows.Count.ToString();
                }
                dgv.DataSource = dtCount;
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

        bool IsProcessStart = false;
        private void timerTrolleyCount_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsProcessStart == false)
                {
                    IsProcessStart = true;

                    GetTrolleyCount();

                    IsProcessStart = false;
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        #endregion

        #region RadioButton Events
        private void rdbToday_CheckedChanged(object sender, EventArgs e)
        {
            try
            { GetTrolleyCount(); }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        private void rdbAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            { GetProductionCount(); }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        private void rdbProdToday_CheckedChanged(object sender, EventArgs e)
        {
            try
            { GetProductionCount(); }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }
        #endregion

        #region DashBoard Page Event
        private void tabControlDashBoard_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                if (e.TabPage.Name == "tabPageTrolleyCount")
                    GetTrolleyCount();
                else if (e.TabPage.Name == "tabPageProduction")
                    GetProductionCount();
                else if (e.TabPage.Name == "tabPageInventory")
                    GetInventory();
                else if (e.TabPage.Name == "tabPageReceivingPending")
                    GetReceivingPending();
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }


        #endregion

        #region ComboBoxEvent
        private void cmbModelNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                GetInventory();
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        #endregion

        #region CheckBox Event
        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoRefresh.Checked)
                    timerTrolleyCount.Enabled = true;
                else
                    timerTrolleyCount.Enabled = false;
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    saveFileDialog1.Filter = "Excel Files|*.xlsx|1997-2003 Excel Files|*.xls|CSV Files|*.csv";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dt = dgv.DataSource as DataTable;
                        if (saveFileDialog1.FilterIndex == 1)
                        {
                            ClsGlobal.ExportExcel(dt, saveFileDialog1.FileName);
                        }
                        else if (saveFileDialog1.FilterIndex == 2)
                        {
                            ClsGlobal.ExportExcel(dt, saveFileDialog1.FileName);
                        }
                        else if (saveFileDialog1.FilterIndex == 3)
                        {
                            ClsGlobal.ExportCsv(dt, saveFileDialog1.FileName);
                        }
                    }
                }
                else
                {
                    ClsGlobal.ShowInfoMessageBox("There is no data to export");
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void picReprintHistory_Click(object sender, EventArgs e)
        {
            frmRptReprintHistory frm = new frmRptReprintHistory();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void pictureBoxTrolleyExchangeHistory_Click(object sender, EventArgs e)
        {
            frmRptTrolleyExchangeHistory frm = new frmRptTrolleyExchangeHistory();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picMachiningPackingHistory_Click(object sender, EventArgs e)
        {
            frmRptPendingMachiningPacking frm = new frmRptPendingMachiningPacking();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picWHMater_Click(object sender, EventArgs e)
        {
            frmWHMaster frm = new frmWHMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void cmbModelNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void picInOutReport_Click(object sender, EventArgs e)
        {
            frmRptInOutWarehouse frm = new frmRptInOutWarehouse();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picInOutDeleteTrolley_Click(object sender, EventArgs e)
        {
            frmDeleteInOutTrolleyCard frm = new frmDeleteInOutTrolleyCard();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void chkNotUsedTrolley_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetReceivingPending();
            }
            catch (Exception ex)
            {
                ClsGlobal.ShowErrorMessageBox(ex.Message);
            }
        }

        #endregion
    }
}
