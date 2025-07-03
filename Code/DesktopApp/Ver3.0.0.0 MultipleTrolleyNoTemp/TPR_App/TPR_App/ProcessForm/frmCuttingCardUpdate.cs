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
    public partial class frmCuttingCardUpdate : Form
    {
        #region Variables

        Dal oDal;
        string Lot1OldValue = "", Lot2OldValue = "", Lot3OldValue = "", Lot4OldValue = "";
        #endregion

        #region Form Methods

        public frmCuttingCardUpdate()
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
                txtTrolleyCard.Focus();
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
                if (txtTrolleyCard.Text.Trim() == "")
                {
                    ClsGlobal.SetInfoMessage("Enter Trolleycard", lblMessage);
                    txtTrolleyCard.Focus();
                    return;
                }
                if (txtLot1.Text.Trim() == "" || txtLot1.Text.Trim().Length != 11)
                {
                    ClsGlobal.SetInfoMessage("Lot1 details not found", lblMessage);
                    txtLot1.Focus();
                    return;
                }
                if ((txtLot2.Text.Trim() == "" || txtLot2.Text.Trim().Length != 11) && txtLot2.Visible == true)
                {
                    ClsGlobal.SetInfoMessage("Lot2 details not found", lblMessage);
                    txtLot2.Focus();
                    return;
                }
                if ((txtLot3.Text.Trim() == "" || txtLot3.Text.Trim().Length != 11) && txtLot3.Visible == true)
                {
                    ClsGlobal.SetInfoMessage("Lot3 details not found", lblMessage);
                    txtLot3.Focus();
                    return;
                }
                if ((txtLot4.Text.Trim() == "" || txtLot4.Text.Trim().Length != 11) && txtLot4.Visible == true)
                {
                    ClsGlobal.SetInfoMessage("Lot4 details not found", lblMessage);
                    txtLot4.Focus();
                    return;
                }
                string StrModelNo = txtTrolleyCard.Text.Trim().Split('-')[0];
                string TrolleyCard = StrModelNo + "-" + txtLot1.Text.Trim();
                List<CuttingTrolleyUpdate> cuttingTrolleyUpdates = new List<CuttingTrolleyUpdate>();
                //If lot value change then only perform database side operation
                //Add Lot1
                cuttingTrolleyUpdates.Add(new CuttingTrolleyUpdate
                {
                    CreatedBy = ClsGlobal.UserId,
                    DbType = EnumDbType.UPDATE,
                    Id = txtLot1.Tag.ToString(),
                    LotNo = txtLot1.Text.Trim(),
                    LotNoDate = new DateTime(DateTime.Now.Year, int.Parse(txtLot1.Text.Trim().Substring(2, 2)), int.Parse(txtLot1.Text.Trim().Substring(0, 2))).ToString("dd-MMM-yyyy"),
                    IsValueChange = txtLot1.Text.Trim() != Lot1OldValue ? true : false
                });
                //Add Lot2
                if (txtLot2.Visible == true)
                {
                    cuttingTrolleyUpdates.Add(new CuttingTrolleyUpdate
                    {
                        CreatedBy = ClsGlobal.UserId,
                        DbType = EnumDbType.UPDATE,
                        Id = txtLot2.Tag.ToString(),
                        LotNo = txtLot2.Text.Trim(),
                        LotNoDate = new DateTime(DateTime.Now.Year, int.Parse(txtLot2.Text.Trim().Substring(2, 2)), int.Parse(txtLot2.Text.Trim().Substring(0, 2))).ToString("dd-MMM-yyyy"),
                        IsValueChange = txtLot2.Text.Trim() != Lot2OldValue ? true : false
                    });

                    TrolleyCard += "-" + txtLot2.Text.Trim().Substring(txtLot2.Text.Trim().Length - 4, 4);
                }
                //Add Lot3
                if (txtLot3.Visible == true)
                {
                    cuttingTrolleyUpdates.Add(new CuttingTrolleyUpdate
                    {
                        CreatedBy = ClsGlobal.UserId,
                        DbType = EnumDbType.UPDATE,
                        Id = txtLot3.Tag.ToString(),
                        LotNo = txtLot3.Text.Trim(),
                        LotNoDate = new DateTime(DateTime.Now.Year, int.Parse(txtLot3.Text.Trim().Substring(2, 2)), int.Parse(txtLot3.Text.Trim().Substring(0, 2))).ToString("dd-MMM-yyyy"),
                        IsValueChange = txtLot3.Text.Trim() != Lot3OldValue ? true : false
                    });

                    TrolleyCard += "-" + txtLot3.Text.Trim().Substring(txtLot3.Text.Trim().Length - 4, 4);
                }
                //Add Lot4
                if (txtLot4.Visible == true)
                {
                    cuttingTrolleyUpdates.Add(new CuttingTrolleyUpdate
                    {
                        CreatedBy = ClsGlobal.UserId,
                        DbType = EnumDbType.UPDATE,
                        Id = txtLot4.Tag.ToString(),
                        LotNo = txtLot4.Text.Trim(),
                        LotNoDate = new DateTime(DateTime.Now.Year, int.Parse(txtLot4.Text.Trim().Substring(2, 2)), int.Parse(txtLot4.Text.Trim().Substring(0, 2))).ToString("dd-MMM-yyyy"),
                        IsValueChange = txtLot4.Text.Trim() != Lot4OldValue ? true : false
                    });

                    TrolleyCard += "-" + txtLot4.Text.Trim().Substring(txtLot4.Text.Trim().Length - 4, 4); ;
                }
                //In case of mix trolley last trolley no lot date will be lot date for trolley card
                string LastLotNoDate = cuttingTrolleyUpdates[cuttingTrolleyUpdates.Count - 1].LotNoDate;
                //update lotno date for all lotno
                for (int i=0;i< cuttingTrolleyUpdates.Count;i++)
                {
                    cuttingTrolleyUpdates[i].LotNoDate = LastLotNoDate;
                }
                string Message = oDal.UpdateCuttingTrolleyCard(cuttingTrolleyUpdates, TrolleyCard, txtTrolleyCard.Text.Trim());
                if (Message == "Y")
                {
                    btnReset_Click(sender, e);
                    ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                }
                else
                    ClsGlobal.SetInfoMessage(Message, lblMessage);
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
                txtTrolleyCard.Text = "";
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

        #region Methods

        private void Clear()
        {
            try
            {
                lblMessage.Text = "";

                txtLot1.Text = "";

                lblLot2.Visible = false;
                txtLot2.Visible = false;
                lblLot3.Visible = false;
                txtLot3.Visible = false;
                lblLot4.Visible = false;
                txtLot4.Visible = false;

                txtLot2.Text = "";
                txtLot3.Text = "";
                txtLot4.Text = "";

                Lot1OldValue = ""; Lot2OldValue = ""; Lot3OldValue = ""; Lot4OldValue = "";
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (string.IsNullOrEmpty(txtTrolleyCard.Text) || txtTrolleyCard.Text.Trim() == "")
                {
                    ClsGlobal.SetInfoMessage("Enter Trolley Card", lblMessage);
                    txtTrolleyCard.Focus();
                    return;
                }
                DataTable dt = oDal.GetOldCuttingTrolleyData(txtTrolleyCard.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["Status"]) != Convert.ToInt32(EnumCuttingStatus.After_Machining))
                    {
                        int i = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            if (i == 1)
                            {
                                txtLot1.Text = Lot1OldValue = row["ChargePallet"].ToString();
                                txtLot1.Tag = row["Id"].ToString();
                            }
                            else if (i == 2)
                            {
                                lblLot2.Visible = true;
                                txtLot2.Visible = true;

                                txtLot2.Text = Lot2OldValue = row["ChargePallet"].ToString();
                                txtLot2.Tag = row["Id"].ToString();
                            }
                            else if (i == 3)
                            {
                                lblLot3.Visible = true;
                                txtLot3.Visible = true;

                                txtLot3.Text = Lot3OldValue = row["ChargePallet"].ToString();
                                txtLot3.Tag = row["Id"].ToString();
                            }
                            else if (i == 4)
                            {
                                lblLot4.Visible = true;
                                txtLot4.Visible = true;

                                txtLot4.Text = Lot4OldValue = row["ChargePallet"].ToString();
                                txtLot4.Tag = row["Id"].ToString();
                            }
                            i++;
                        }
                    }
                    else
                    {
                        ClsGlobal.SetInfoMessage("Maching is done,now you can not update", lblMessage);
                        txtTrolleyCard.Text = "";
                        txtTrolleyCard.Focus();
                    }
                }
                else
                {
                    ClsGlobal.SetInfoMessage("Wrong Trolley Card", lblMessage);
                    txtTrolleyCard.Text = "";
                    txtTrolleyCard.Focus();
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
    }
}
