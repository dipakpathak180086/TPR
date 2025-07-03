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
    public partial class frmCutting : Form
    {
        #region Variables

        Dal oDal;
        Cutting oCutting;
        string _ProductionPlanId, _Shift;
        int _DTB;
        //It will store data for 2 lot no
        List<Cutting> _ListCutting = new List<Cutting>();

        #endregion

        #region Form Methods

        public frmCutting()
        {
            try
            {
                InitializeComponent();
                oCutting = new Cutting();
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
                this.WindowState = FormWindowState.Maximized;

                lblMessage.Text = "";
                LoadData();
                txtChargeNo.Focus();
                lblWelcome.Text = "Logged In User : " + ClsGlobal.UserName + ", Line : " + ClsGlobal.LineNo;
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (ValidateInput())
                {
                    SetNgQty();

                    oCutting.ProductionPlanId = _ProductionPlanId;
                    oCutting.ModelNo = txtModelNo.Text.Trim();
                    _DTB = oDal.GetModelDTB(oCutting.ModelNo);
                    oCutting.ShiftName = _Shift;
                    oCutting.LotNo = dtpDate.Value.ToString("ddMMyy") + ClsGlobal.LineNo + txtChargeNo.Text.Trim().PadLeft(2, '0') + txtPalletNo.Text.Trim().PadLeft(2, '0');
                    oCutting.LotNoDate = dtpDate.Text;
                    if (oCutting.LotNo.Length != 11)
                    {
                        ClsGlobal.SetInfoMessage("Lot No " + oCutting.LotNo + " length should be 11, please check", lblMessage);
                        return;
                    }
                    string Operator = "";
                    for (int i = 0; i < chkListOperator.CheckedItems.Count; i++)
                    {
                        Operator += chkListOperator.CheckedItems[i].ToString() + ",";
                    }
                    Operator = Operator.TrimEnd(',');
                    oCutting.Operators = Operator;
                    oCutting.Leaders = ClsGlobal.UserId;
                    oCutting.TotalQty = int.Parse(txtTotalQty.Text.Trim());
                    oCutting.OkQty = int.Parse(txtOkQty.Text.Trim());
                    oCutting.NgQty = int.Parse(lblNgQty.Text.Trim());
                    oCutting.TrolleyCard = oCutting.ModelNo + "-" + oCutting.LotNo;
                    oCutting.Status = Convert.ToInt32(EnumCuttingStatus.Cutting);
                    oCutting.CreatedBy = ClsGlobal.UserId;
                    oCutting.IsMixedTrolley = chkMixedTrolley.Checked;

                    int NewPendingQty = Convert.ToInt32(lblPendingQty.Text) - Convert.ToInt32(txtOkQty.Text);
                    //If Ng Qty then open defect screen
                    if (Convert.ToInt32(lblNgQty.Text) > 0)
                    {
                        ClsGlobal.CuttingDefectAutoClose = false;
                        frmCuttingDefect oFrm = new frmCuttingDefect(oCutting, NewPendingQty, _ListCutting, _DTB);
                        oFrm.Show();
                        oFrm.FormClosing += OFrm_FormClosing; ;
                        this.Hide();
                    }
                    else
                    {
                        //If not mixed trolly then save data
                        if (chkMixedTrolley.Checked == false)
                        {
                            string ReturnMsg = oDal.SaveCuttingData(oCutting).Rows[0]["Result"].ToString();
                            if (ReturnMsg.ToUpper() == "Y")
                            {
                                string Msg = PrintLabel(oCutting.LotNo, oCutting.OkQty.ToString(), oCutting.ModelNo, oCutting.TrolleyCard);

                                string ShowMsg = "";
                                if (Msg == "OK")
                                {
                                    if (NewPendingQty == 0)
                                        ShowMsg = "Print successfully, data saved successfully, Production complete for this model";
                                    else
                                        ShowMsg = "Print successfully, data saved successfully";
                                    ClsGlobal.ShowConfirmMessageBox(ShowMsg);
                                }
                                else
                                {
                                    if (NewPendingQty == 0)
                                        ShowMsg = "Data saved successfully, printing error message - " + Msg + " , Production complete for this model";
                                    else
                                        ShowMsg = "Data saved successfully, printing error message - " + Msg;
                                    ClsGlobal.ShowConfirmMessageBox(ShowMsg);
                                }

                                btnReset_Click(sender, e);
                            }
                            else
                                ClsGlobal.SetInfoMessage("Data could not be saved, " + ReturnMsg, lblMessage);
                        }
                        else //If mixed trolley then no need to save just hold in the list until user does not print trolley card
                            AddMixedTrolleyData(NewPendingQty);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    ClsGlobal.SetErrorMessage("Lot No already exist,please check!!", lblMessage);
                }
                else
                {
                    ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
                }
            }
        }

        private void OFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Show();
            if (ClsGlobal.CuttingDefectAutoClose)
            {
                if (oCutting.IsMixedTrolley)
                {
                    if (_ListCutting.Count == 2)
                        btnReset_Click(sender, e);
                    else
                    {
                        lblMessage.Text = "";
                        lblPendingQty.Text = (Convert.ToInt32(lblPendingQty.Text) - Convert.ToInt32(txtOkQty.Text)).ToString();
                        txtPalletNo.Text = "";
                        txtChargeNo.Text = "";
                        txtOkQty.Text = "";
                        txtTotalQty.Text = "";
                        //for (int i = 0; i < chkListOperator.Items.Count; i++)
                        //    chkListOperator.SetItemChecked(i, false);
                        lblNgQty.Text = "0";
                    }
                }
                else
                    btnReset_Click(sender, e);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                chkMixedTrolley.Enabled = true;
                chkMixedTrolley.Checked = false;
                _ListCutting.Clear();
                Clear();
                LoadData();
                txtChargeNo.Focus();
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

        #region Methods
        private void AddMixedTrolleyData(int NewPendingQty)
        {
            try
            {
                bool IsLotNoExist = oDal.IsLotNoExist(oCutting.LotNo, oCutting.ModelNo);
                if (IsLotNoExist)
                {
                    ClsGlobal.SetErrorMessage("Lot No " + oCutting.LotNo + " already exist,please check!!", lblMessage);
                    return;
                }
                IsLotNoExist = _ListCutting.Exists(x => x.LotNo == oCutting.LotNo && x.ModelNo == oCutting.ModelNo);
                if (IsLotNoExist)
                {
                    ClsGlobal.SetErrorMessage("Lot No " + oCutting.LotNo + " already added,please check!!", lblMessage);
                    return;
                }
                _ListCutting.Add(new Cutting
                {
                    ProductionPlanId = oCutting.ProductionPlanId,
                    ModelNo = oCutting.ModelNo,
                    ShiftName = oCutting.ShiftName,
                    LotNo = oCutting.LotNo,
                    LotNoDate = oCutting.LotNoDate,
                    Operators = oCutting.Operators,
                    Leaders = oCutting.Leaders,
                    TotalQty = oCutting.TotalQty,
                    OkQty = oCutting.OkQty,
                    NgQty = oCutting.NgQty,
                    TrolleyCard = oCutting.TrolleyCard,
                    Status = oCutting.Status,
                    CreatedBy = oCutting.CreatedBy,
                    IsMixedTrolley = oCutting.IsMixedTrolley
                });
                //Only save when user said yes
                if (MessageBox.Show("Do you want to print trolley card ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Fetch all lot no and concate them to create one trolley card
                    string TrolleyCard = _ListCutting[0].TrolleyCard;
                    int OkQty = _ListCutting[0].OkQty;
                    //if mix trolley have different date lot no then last added lot no date will be considered lotno date for trolleycard
                    string LotNoDate = _ListCutting[_ListCutting.Count - 1].LotNoDate;

                    for (int i = 1; i < _ListCutting.Count; i++)
                    {
                        TrolleyCard += "-" + _ListCutting[i].LotNo.Substring(7, 4);
                        OkQty += _ListCutting[i].OkQty;
                    }
                    //Now updat new trolley card in list so that in database new trolley card can be saved
                    //In Mixed trolley card 2nd lot no last 4 digit
                    for (int i = 0; i < _ListCutting.Count; i++)
                    {
                        _ListCutting[i].TrolleyCard = TrolleyCard;
                        _ListCutting[i].LotNoDate = LotNoDate;
                    }

                    string ReturnMsg = oDal.SaveCuttingMixedTrollyData(_ListCutting);
                    if (ReturnMsg.ToUpper() == "Y")
                    {
                        string Msg = PrintLabel(_ListCutting[0].LotNo, OkQty.ToString(), oCutting.ModelNo, TrolleyCard);

                        string ShowMsg = "";
                        if (Msg == "OK")
                        {
                            if (NewPendingQty == 0)
                                ShowMsg = "Print successfully, data saved successfully, Production complete for this model";
                            else
                                ShowMsg = "Print successfully, data saved successfully";
                            ClsGlobal.ShowConfirmMessageBox(ShowMsg);
                        }
                        else
                        {
                            if (NewPendingQty == 0)
                                ShowMsg = "Data saved successfully, printing error message - " + Msg + " , Production complete for this model";
                            else
                                ShowMsg = "Data saved successfully, printing error message - " + Msg;
                            ClsGlobal.ShowConfirmMessageBox(ShowMsg);
                        }

                        btnReset_Click(null, null);
                    }
                    else
                        ClsGlobal.SetInfoMessage("Data could not be saved, " + ReturnMsg, lblMessage);
                }
                else
                {
                    lblMessage.Text = "";
                    lblPendingQty.Text = (Convert.ToInt32(lblPendingQty.Text) - Convert.ToInt32(txtOkQty.Text)).ToString();
                    txtChargeNo.Text = "";
                    txtPalletNo.Text = "";
                    txtOkQty.Text = "";
                    txtTotalQty.Text = "";
                    //for (int i = 0; i < chkListOperator.Items.Count; i++)
                    //    chkListOperator.SetItemChecked(i, false);
                    lblNgQty.Text = "0";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    ClsGlobal.SetErrorMessage("Lot No already exist,please check!!", lblMessage);
                }
                else
                    ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void Clear()
        {
            try
            {
                lblMessage.Text = "";
                txtModelNo.Text = "";
                _ProductionPlanId = "";
                txtPalletNo.Text = "";
                txtChargeNo.Text = "";
                txtTotalQty.Text = "";
                txtOkQty.Text = "";
                chkListOperator.Items.Clear();
                lblPendingQty.Text = "0";
                lblPPQty.Text = "0";
                lblNgQty.Text = "0";
                dtpDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void SetNgQty()
        {
            try
            {
                lblMessage.Text = "";
                int TotalQty = 0;
                if (txtOkQty.Text.Trim() == "")
                {
                    TotalQty = string.IsNullOrEmpty(txtTotalQty.Text) ? 0 : Convert.ToInt32(txtTotalQty.Text);
                    lblNgQty.Text = (TotalQty - 0).ToString();
                }
                if (txtOkQty.Text.Length > 0)
                {
                    TotalQty = string.IsNullOrEmpty(txtTotalQty.Text) ? 0 : Convert.ToInt32(txtTotalQty.Text);
                    if (Convert.ToInt32(txtOkQty.Text) <= TotalQty)
                    {
                        lblNgQty.Text = (Convert.ToInt32(txtTotalQty.Text) - Convert.ToInt32(txtOkQty.Text)).ToString();
                    }
                    else
                        ClsGlobal.SetInfoMessage("OK Qty can not be greater than total qty", lblMessage);
                }
            }
            catch (Exception ex) { throw ex; }
        }
        private void LoadData()
        {
            try
            {
                lblMessage.Text = "";
                DataSet ds = oDal.GetCuttingData();

                //Table For Operator User
                if (ds.Tables[0].Rows.Count == 0)
                    ClsGlobal.ShowInfoMessageBox("User data not found, please check");

                foreach (DataRow row in ds.Tables[0].Rows)
                    chkListOperator.Items.Add(row["UserName"].ToString());

                //Table For Shift
                if (ds.Tables[1].Rows.Count > 0)
                {
                    _Shift = ds.Tables[1].Rows[0]["ShiftName"].ToString();
                    lblShift.Text = "CUTTING - SHIFT " + _Shift;
                }
                else
                    ClsGlobal.ShowInfoMessageBox("Shift data not found, please check");

                //Table For Production Plan
                if (ds.Tables[2].Rows.Count > 0)
                {
                    txtModelNo.Text = ds.Tables[2].Rows[0]["ModelNo"].ToString();
                    lblPPQty.Text = ds.Tables[2].Rows[0]["PPQty"].ToString();
                    lblPendingQty.Text = ds.Tables[2].Rows[0]["PendingQty"].ToString();
                    _ProductionPlanId = ds.Tables[2].Rows[0]["ProductionPlanId"].ToString();
                }
                else
                    ClsGlobal.ShowInfoMessageBox("Model data not found from production plan, please check");

            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private bool ValidateInput()
        {
            try
            {
                if (txtModelNo.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Model no not found!!", lblMessage);
                    txtModelNo.Focus();
                    return false;
                }
                if (Convert.ToDateTime(dtpDate.Text) > DateTime.Parse(DateTime.Now.ToString("dd-MMM-yyyy")))
                {
                    ClsGlobal.SetInfoMessage("Date can not be future date!!", lblMessage);
                    dtpDate.Focus();
                    return false;
                }
                if (txtChargeNo.Text.Trim().Length == 0 || int.Parse(txtChargeNo.Text.Trim()) == 0)
                {
                    ClsGlobal.SetInfoMessage("Please input charge no!!", lblMessage);
                    txtChargeNo.Focus();
                    return false;
                }
                if (txtPalletNo.Text.Trim().Length == 0 || int.Parse(txtPalletNo.Text.Trim()) == 0)
                {
                    ClsGlobal.SetInfoMessage("Please input pallet no!!", lblMessage);
                    txtPalletNo.Focus();
                    return false;
                }
                if (txtTotalQty.Text.Trim().Length == 0 || int.Parse(txtTotalQty.Text.Trim()) == 0)
                {
                    ClsGlobal.SetInfoMessage("Please input total qty!!", lblMessage);
                    txtTotalQty.Focus();
                    return false;
                }
                if (txtOkQty.Text.Trim().Length == 0 || int.Parse(txtOkQty.Text.Trim()) == 0)
                {
                    ClsGlobal.SetInfoMessage("Please input ok qty!!", lblMessage);
                    txtOkQty.Focus();
                    return false;
                }
                if (chkListOperator.CheckedItems.Count == 0)
                {
                    ClsGlobal.SetInfoMessage("Please select operator!!", lblMessage);
                    txtOkQty.Focus();
                    return false;
                }
                if ((int.Parse(txtOkQty.Text.Trim()) + int.Parse(lblNgQty.Text.Trim())) != int.Parse(txtTotalQty.Text.Trim()))
                {
                    ClsGlobal.SetInfoMessage("(Ok+Ng) Qty shoule be equal to total qty ", lblMessage);
                    return false;
                }
                if (int.Parse(txtOkQty.Text.Trim()) > int.Parse(lblPendingQty.Text.Trim()))
                {
                    ClsGlobal.SetInfoMessage("Ok Qty can not be greater than pending qty.", lblMessage);
                    return false;
                }
                string Shift = oDal.GetShift().Rows[0]["ShiftName"].ToString();
                if (Shift.Trim().ToUpper() != _Shift.Trim().ToUpper())
                {
                    ClsGlobal.ShowInfoMessageBox("Hi, Shift has been changed, please logout the application and login again");
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        private string PrintLabel(string LotNo, string OkQty, string ModelNo, string TrolleyCard)
        {
            try
            {
                string PrinterName = Properties.Settings.Default.PrinterName;
                if (File.Exists(Application.StartupPath + "\\" + ClsGlobal.CuttingPrnName))
                {
                    StreamReader sr = new StreamReader(Application.StartupPath + "\\" + ClsGlobal.CuttingPrnName);
                    string PrnFileTemp = sr.ReadToEnd();
                    sr.Close();
                    PrnFileTemp = PrnFileTemp.Replace("{VARLEN}", TrolleyCard.Length.ToString());
                    PrnFileTemp = PrnFileTemp.Replace("{VARMODELNO}", ModelNo);
                    PrnFileTemp = PrnFileTemp.Replace("{VARLOTNO}", LotNo);
                    //PrnFileTemp = PrnFileTemp.Replace("{VARLOTNO}", TrolleyCard);
                    PrnFileTemp = PrnFileTemp.Replace("{VARQTY}", OkQty);
                    PrnFileTemp = PrnFileTemp.Replace("{VARBARCODE}", TrolleyCard);
                    return PrintBarcode.PrintCommand(PrnFileTemp, PrinterName);
                }
                else
                    return "Prn File " + ClsGlobal.CuttingPrnName + " not found";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region TextBox Events
        private void txtTotalQty_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtTotalQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTotalQty.Text.Length > 0)
                    SetNgQty();
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void txtOkQty_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtOkQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetNgQty();
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }



        #endregion

        #region CheckBox Event

        private void chkMixedTrolley_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkMixedTrolley.Checked)
                {
                    chkMixedTrolley.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion
    }
}
