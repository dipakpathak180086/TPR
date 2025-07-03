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
    public partial class frmReprint : Form
    {
        #region Variables

        Dal oDal;
        QA oQA;

        #endregion

        #region Form Methods

        public frmReprint()
        {
            try
            {
                InitializeComponent();
                oQA = new QA();
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
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (rdbCutting.Checked == false && rdbMachining.Checked == false)
                {
                    ClsGlobal.SetInfoMessage("Select Reprint Option", lblMessage);
                    return;
                }
                if (txtTrolleyCard.Text.Trim() == "")
                {
                    ClsGlobal.SetInfoMessage("Scan/Enter Trolley Card", lblMessage);
                    txtTrolleyCard.Focus();
                    return;
                }
                if (txtReprintReason.Text.Trim() == "")
                {
                    ClsGlobal.SetInfoMessage("Enter Reprint Reason", lblMessage);
                    txtReprintReason.Focus();
                    return;
                }
                string ReprintType = rdbCutting.Checked ? "CUTTING" : "MACHINING";
                DataTable dt = oDal.GetRePrintData(ReprintType, txtTrolleyCard.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    string Prn = rdbCutting.Checked ? ClsGlobal.CuttingPrnName : ClsGlobal.MachiningPrnName;
                    //Trolleycard is cominatio of Modelno-lotno
                    string Msg = PrintLabel(txtTrolleyCard.Text.Trim().Split('-')[1].Trim(), dt.Rows[0]["OkQty"].ToString(), dt.Rows[0]["ModelNo"].ToString(), txtTrolleyCard.Text.Trim(), Prn);
                    if (Msg == "OK")
                    {
                        DataTable dtReprintHistory = oDal.ReprintHistory("INSERT", ReprintType, txtTrolleyCard.Text.Trim(), Convert.ToInt32( dt.Rows[0]["OkQty"].ToString()), txtReprintReason.Text.Trim(), ClsGlobal.UserId,"","");

                        ClsGlobal.SetConfirmMessage("Print successfully!!", lblMessage);
                        txtTrolleyCard.Text = "";
                        txtReprintReason.Text = "";
                        txtTrolleyCard.Focus();
                    }
                    else
                        ClsGlobal.SetInfoMessage("Error in printing - " + Msg, lblMessage);
                }
                else
                {
                    ClsGlobal.SetInfoMessage("Wrong Trolley Card,Data Not Found", lblMessage);
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                txtTrolleyCard.Text = "";
                rdbCutting.Checked = false;
                rdbMachining.Checked = false;
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

        #region Methods

        private string PrintLabel(string LotNo, string OkQty, string ModelNo, string TrolleyCard, string PrnFileName)
        {
            try
            {
                string PrinterName = Properties.Settings.Default.PrinterName;
                if (File.Exists(Application.StartupPath + "\\" + PrnFileName))
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
                    return "Prn File " + PrnFileName + " not found";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Label Event
        private void lblMessage_DoubleClick(object sender, EventArgs e)
        {
            ClsGlobal.ShowInfoMessageBox(lblMessage.Text);
        }

        #endregion

    }
}
