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
    public partial class frmCuttingDefect : Form
    {
        #region Variables

        Cutting oCutting;
        Dal oDal;
        int _NewPendingQty = 0;
        int _DTB = 0;
        List<Cutting> _ListCutting;

        #endregion

        #region Form Methods

        public frmCuttingDefect(Cutting cutting, int NewPendingQty, List<Cutting> cuttings, int dtb)
        {
            try
            {
                InitializeComponent();
                oCutting = cutting;
                oDal = new Dal();
                _NewPendingQty = NewPendingQty;
                _ListCutting = cuttings;
                _DTB = dtb;
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
                txtInnverCavity.Focus();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "ERROR: " + ex.Message;
            }
        }

        #endregion

        #region Button Event
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int DefectQty = 0;
                DefectQty += oCutting.InnerCavity = txtInnverCavity.Text.Length > 0 ? Convert.ToInt32(txtInnverCavity.Text) : 0;
                DefectQty += oCutting.OuterCavity = txtOuterCavity.Text.Length > 0 ? Convert.ToInt32(txtOuterCavity.Text) : 0;
                DefectQty += oCutting.PenetrationPorosity = txtPenetration.Text.Length > 0 ? Convert.ToInt32(txtPenetration.Text) : 0;
                DefectQty += oCutting.Slag = txtSlag.Text.Length > 0 ? Convert.ToInt32(txtSlag.Text) : 0;
                DefectQty += oCutting.Dent = txtDent.Text.Length > 0 ? Convert.ToInt32(txtDent.Text) : 0;
                DefectQty += oCutting.Spine = txtSpine.Text.Length > 0 ? Convert.ToInt32(txtSpine.Text) : 0;
                DefectQty += oCutting.ForeignSubstance = txtForeign.Text.Length > 0 ? Convert.ToInt32(txtForeign.Text) : 0;
                DefectQty += oCutting.Crack = txtCrack.Text.Length > 0 ? Convert.ToInt32(txtCrack.Text) : 0;
                DefectQty += oCutting.MaterialLD = txtMaterialLD.Text.Length > 0 ? Convert.ToInt32(txtMaterialLD.Text) : 0;
                DefectQty += oCutting.OD = txtOD.Text.Length > 0 ? Convert.ToInt32(txtOD.Text) : 0;
                DefectQty += oCutting.Cuttings = txtCutting.Text.Length > 0 ? Convert.ToInt32(txtCutting.Text) : 0;
                DefectQty += oCutting.Other = txtOther.Text.Length > 0 ? Convert.ToInt32(txtOther.Text) : 0;
                //DefectQty += oCutting.DTBQty = txtDTB.Text.Length > 0 ? Convert.ToInt32(txtDTB.Text) : 0;
                DefectQty += oCutting.DTBQty = txtDTBQty.Text.Length > 0 ? Convert.ToInt32(txtDTBQty.Text) : 0;
                DefectQty += oCutting.Sample = txtSample.Text.Length > 0 ? Convert.ToInt32(txtSample.Text) : 0;
                DefectQty += oCutting.PowerCut = txtPowerCut.Text.Length > 0 ? Convert.ToInt32(txtPowerCut.Text) : 0;
                DefectQty += oCutting.Length = txtLength.Text.Length > 0 ? Convert.ToInt32(txtLength.Text) : 0;
                DefectQty += oCutting.ExtraParam5 = txtExtraParam5.Text.Length > 0 ? Convert.ToInt32(txtExtraParam5.Text) : 0;
                //Validate Qty With NG Qty
                if (DefectQty == oCutting.NgQty) //if qty same then save
                {
                    if (oCutting.IsMixedTrolley == false)
                    {
                        string ReturnMsg = oDal.SaveCuttingData(oCutting).Rows[0]["Result"].ToString();
                        if (ReturnMsg.ToUpper() == "Y")
                        {
                            string Msg = PrintLabel(oCutting.LotNo, oCutting.OkQty.ToString(), oCutting.ModelNo, oCutting.TrolleyCard);
                            string ShowMsg = "";
                            if (Msg == "OK")
                            {
                                if (_NewPendingQty == 0)
                                    ShowMsg = "Print successfully, data saved successfully, Production complete for this model";
                                else
                                    ShowMsg = "Print successfully, data saved successfully";
                                ClsGlobal.ShowConfirmMessageBox(ShowMsg);
                            }
                            else
                            {
                                if (_NewPendingQty == 0)
                                    ShowMsg = "Data saved successfully, printing error message - " + Msg + " , Production complete for this model";
                                else
                                    ShowMsg = "Data saved successfully, printing error message - " + Msg;
                                ClsGlobal.ShowInfoMessageBox(ShowMsg);
                            }

                            ClsGlobal.CuttingDefectAutoClose = true;
                            this.Close();
                        }
                        else
                            ClsGlobal.SetErrorMessage("Data could not be saved, " + ReturnMsg, lblMessage);
                    }
                    else
                        AddMixedTrolleyData(_NewPendingQty);
                }
                else
                    ClsGlobal.ShowInfoMessageBox("Qty mismatch, Defect Qty is " + DefectQty + " And Ng Qty is " + oCutting.NgQty);
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            ClsGlobal.CuttingDefectAutoClose = false;
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        #endregion

        #region TextBox Events
        private void txtDefect_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtDTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int dtbValue = txtDTB.Text.Trim() != "" ? int.Parse(txtDTB.Text.Trim()) : 0;
                txtDTBQty.Text = (dtbValue * _DTB).ToString();
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
                txtInnverCavity.Text = "0";
                txtOuterCavity.Text = "0";
                txtPenetration.Text = "0";
                txtSlag.Text = "0";
                txtDent.Text = "0";
                txtSpine.Text = "0";
                txtForeign.Text = "0";
                txtCrack.Text = "0";
                txtMaterialLD.Text = "0";
                txtOD.Text = "0";
                txtCutting.Text = "0";
                txtOther.Text = "0";
                txtDTB.Text = "0";
                txtDTBQty.Text = "0";
                txtSample.Text = "0";
                txtPowerCut.Text = "0";
                txtLength.Text = "0";
                txtExtraParam5.Text = "0";
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
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

        private void AddMixedTrolleyData(int NewPendingQty)
        {
            try
            {
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
                    IsMixedTrolley = oCutting.IsMixedTrolley,

                    InnerCavity = oCutting.InnerCavity,
                    OuterCavity = oCutting.OuterCavity,
                    PenetrationPorosity = oCutting.PenetrationPorosity,
                    Slag = oCutting.Slag,
                    Dent = oCutting.Dent,
                    Spine = oCutting.Spine,
                    ForeignSubstance = oCutting.ForeignSubstance,
                    Crack = oCutting.Crack,
                    MaterialLD = oCutting.MaterialLD,
                    OD = oCutting.OD,
                    Cuttings = oCutting.Cuttings,
                    Other = oCutting.Other,
                    DTBQty = oCutting.DTBQty,
                    Sample = oCutting.Sample,
                    PowerCut = oCutting.PowerCut,
                    Length = oCutting.Length,
                    ExtraParam5 = oCutting.ExtraParam5
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

                        ClsGlobal.CuttingDefectAutoClose = true;
                        this.Close();
                    }
                    else
                        ClsGlobal.SetInfoMessage("Data could not be saved, " + ReturnMsg, lblMessage);
                }
                else
                {
                    ClsGlobal.CuttingDefectAutoClose = true;
                    this.Close();
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

        #endregion
    }
}
