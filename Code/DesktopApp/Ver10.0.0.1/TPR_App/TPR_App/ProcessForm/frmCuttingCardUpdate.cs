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
        int iCounterUpdateLoop = 0;
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
                BindModel();
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
                /*When lot no is changed*/
                if (rdbLotNo.Checked)
                {
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
                    for (int i = 0; i < cuttingTrolleyUpdates.Count; i++)
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
                else if (rdbModel.Checked) //When model is updated
                {
                    if (cmbModel.SelectedIndex <= 0)
                    {
                        ClsGlobal.SetInfoMessage("Select new model", lblMessage);
                        cmbModel.Focus();
                        return;
                    }
                    string StrModelNo = txtTrolleyCard.Text.Trim().Split('-')[0];
                    if (cmbModel.SelectedItem.ToString() == StrModelNo)
                    {
                        ClsGlobal.SetInfoMessage("You can not use the same model", lblMessage);
                        cmbModel.Focus();
                        return;
                    }
                    string NewModelNo = cmbModel.SelectedItem.ToString();
                    string[] ArrTrollCardData = txtTrolleyCard.Text.Trim().Split('-');
                    string NewTrolleyCard = NewModelNo + "-";
                    for (int i = 1; i < ArrTrollCardData.Length; i++)
                        NewTrolleyCard += ArrTrollCardData[i] + "-";

                    NewTrolleyCard = NewTrolleyCard.TrimEnd('-');
                    string Message = oDal.UpdateCuttingTrolleyCardModel(NewTrolleyCard, txtTrolleyCard.Text.Trim(), NewModelNo, 2);
                    if (Message == "Y")
                    {
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                    }
                    else
                        ClsGlobal.SetInfoMessage(Message, lblMessage);
                }
                else //whe qty updated
                {
                    //if (txtTrolleyCard.Text.Trim().Split('-').Length > 2)
                    //{
                    //    ClsGlobal.SetInfoMessage("You can not change qty for mix trolley", lblMessage);
                    //    return;
                    //}
                    if (lblLot1TotalQty.Visible == true)
                    {
                        if (txtLot1TotalQty.Text.Trim() == "" || txtLot1TotalQty.Text.Trim() == "0")
                        {
                            ClsGlobal.SetInfoMessage("Please input total qty", lblMessage);
                            txtLot1TotalQty.Focus();
                            return;
                        }
                        if (txtLot1OkQty.Text.Trim() == "" || txtLot1OkQty.Text.Trim() == "0")
                        {
                            ClsGlobal.SetInfoMessage("Please input ok qty", lblMessage);
                            txtLot1OkQty.Focus();
                            return;
                        }
                        if (txtLot1NgQty.Text.Trim() == "")
                        {
                            ClsGlobal.SetInfoMessage("Please input ng qty", lblMessage);
                            txtLot1NgQty.Focus();
                            return;
                        }
                    }
                    if (lblLot2TotalQty.Visible == true)
                    {
                        if (txtLot2TotalQty.Text.Trim() == "" || txtLot2TotalQty.Text.Trim() == "0")
                        {
                            ClsGlobal.SetInfoMessage("Please input total qty", lblMessage);
                            txtLot2TotalQty.Focus();
                            return;
                        }
                        if (txtLot2OkQty.Text.Trim() == "" || txtLot2OkQty.Text.Trim() == "0")
                        {
                            ClsGlobal.SetInfoMessage("Please input ok qty", lblMessage);
                            txtLot2OkQty.Focus();
                            return;
                        }
                        if (txtLot2NgQty.Text.Trim() == "")
                        {
                            ClsGlobal.SetInfoMessage("Please input ng qty", lblMessage);
                            txtLot2NgQty.Focus();
                            return;
                        }
                    }
                    if (lblLot3TotalQty.Visible == true)
                    {
                        if (txtLot3TotalQty.Text.Trim() == "" || txtLot3TotalQty.Text.Trim() == "0")
                        {
                            ClsGlobal.SetInfoMessage("Please input total qty", lblMessage);
                            txtLot3TotalQty.Focus();
                            return;
                        }
                        if (txtLot3OkQty.Text.Trim() == "" || txtLot3OkQty.Text.Trim() == "0")
                        {
                            ClsGlobal.SetInfoMessage("Please input ok qty", lblMessage);
                            txtLot3OkQty.Focus();
                            return;
                        }
                        if (txtLot3NgQty.Text.Trim() == "")
                        {
                            ClsGlobal.SetInfoMessage("Please input ng qty", lblMessage);
                            txtLot3NgQty.Focus();
                            return;
                        }
                    }
                    if (lblLot4TotalQty.Visible == true)
                    {
                        if (txtLot4TotalQty.Text.Trim() == "" || txtLot4TotalQty.Text.Trim() == "0")
                        {
                            ClsGlobal.SetInfoMessage("Please input total qty", lblMessage);
                            txtLot4TotalQty.Focus();
                            return;
                        }
                        if (txtLot4OkQty.Text.Trim() == "" || txtLot4OkQty.Text.Trim() == "0")
                        {
                            ClsGlobal.SetInfoMessage("Please input ok qty", lblMessage);
                            txtLot4OkQty.Focus();
                            return;
                        }
                        if (txtLot4NgQty.Text.Trim() == "")
                        {
                            ClsGlobal.SetInfoMessage("Please input ng qty", lblMessage);
                            txtLot4NgQty.Focus();
                            return;
                        }
                    }
                    /*sum of ok+ng should not be greater than total qty*/
                    if (lblLot1TotalQty.Visible == true)
                    {
                        int TotalQty = int.Parse(txtLot1TotalQty.Text.Trim());
                        int OkQty = int.Parse(txtLot1OkQty.Text.Trim());
                        int NgQty = int.Parse(txtLot1NgQty.Text.Trim());
                        if (TotalQty != OkQty + NgQty)
                        {
                            ClsGlobal.SetInfoMessage("Ng+OK qty should be equal to Total Qty, Please check", lblMessage);
                            txtLot1NgQty.Focus();
                            return;
                        }
                    }
                    if (lblLot2TotalQty.Visible == true)
                    {
                        int TotalQty = int.Parse(txtLot2TotalQty.Text.Trim());
                        int OkQty = int.Parse(txtLot2OkQty.Text.Trim());
                        int NgQty = int.Parse(txtLot2NgQty.Text.Trim());
                        if (TotalQty != OkQty + NgQty)
                        {
                            ClsGlobal.SetInfoMessage("Ng+OK qty should be equal to Total Qty, Please check", lblMessage);
                            txtLot2NgQty.Focus();
                            return;
                        }
                    }
                    if (lblLot3TotalQty.Visible == true)
                    {
                        int TotalQty = int.Parse(txtLot3TotalQty.Text.Trim());
                        int OkQty = int.Parse(txtLot3OkQty.Text.Trim());
                        int NgQty = int.Parse(txtLot3NgQty.Text.Trim());
                        if (TotalQty != OkQty + NgQty)
                        {
                            ClsGlobal.SetInfoMessage("Ng+OK qty should be equal to Total Qty, Please check", lblMessage);
                            txtLot3NgQty.Focus();
                            return;
                        }
                    }
                    if (lblLot4TotalQty.Visible == true)
                    {
                        int TotalQty = int.Parse(txtLot4TotalQty.Text.Trim());
                        int OkQty = int.Parse(txtLot4OkQty.Text.Trim());
                        int NgQty = int.Parse(txtLot4NgQty.Text.Trim());
                        if (TotalQty != OkQty + NgQty)
                        {
                            ClsGlobal.SetInfoMessage("Ng+OK qty should be equal to Total Qty, Please check", lblMessage);
                            txtLot4NgQty.Focus();
                            return;
                        }
                    }
                    string Message = string.Empty;
                    for (int i = 0; i < iCounterUpdateLoop; i++)
                    {
                        if (i == 0)
                        {
                            Message = oDal.UpdateCuttingTrolleyCardModel("", txtTrolleyCard.Text.Trim(), "", 3
                                ,Convert.ToInt32( txtLot1TotalQty.Text),Convert.ToInt32( txtLot1OkQty.Text),Convert.ToInt32( txtLot1NgQty.Text),txtLot1.Text);
                        }
                        else if (i == 1)
                        {
                            Message = oDal.UpdateCuttingTrolleyCardModel("", txtTrolleyCard.Text.Trim(), "", 3
                                , Convert.ToInt32(txtLot2TotalQty.Text), Convert.ToInt32(txtLot2OkQty.Text), Convert.ToInt32(txtLot2NgQty.Text), txtLot2.Text);
                        }
                        else if (i == 2)
                        {
                            Message = oDal.UpdateCuttingTrolleyCardModel("", txtTrolleyCard.Text.Trim(), "", 3
                                , Convert.ToInt32(txtLot3TotalQty.Text), Convert.ToInt32(txtLot3OkQty.Text), Convert.ToInt32(txtLot3NgQty.Text), txtLot3.Text);
                        }
                        else if (i == 3)
                        {
                            Message = oDal.UpdateCuttingTrolleyCardModel("", txtTrolleyCard.Text.Trim(), "", 3
                                , Convert.ToInt32(txtLot4TotalQty.Text), Convert.ToInt32(txtLot4OkQty.Text), Convert.ToInt32(txtLot4NgQty.Text), txtLot4.Text);
                        }
                    }
                    
                   
                    if (Message == "Y")
                    {
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                    }
                    else
                        ClsGlobal.SetInfoMessage(Message, lblMessage);
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
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
                iCounterUpdateLoop = dt.Rows.Count;
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
                                txtLot1TotalQty.Text = row["TotalQty"].ToString();
                                txtLot1OkQty.Text = row["OkQty"].ToString();
                                txtLot1NgQty.Text = row["NgQty"].ToString();
                            }
                            else if (i == 2)
                            {
                                lblLot2.Visible = true;
                                txtLot2.Visible = true;
                                lblLot2TotalQty.Visible = true;
                                txtLot2TotalQty.Visible = true;
                                lblLot2OkQty.Visible = true;
                                txtLot2OkQty.Visible = true;
                                lblLot2NgQty.Visible = true;
                                txtLot2NgQty.Visible = true;
                                txtLot2TotalQty.Text = row["TotalQty"].ToString();
                                txtLot2OkQty.Text = row["OkQty"].ToString();
                                txtLot2NgQty.Text = row["NgQty"].ToString();
                                txtLot2.Text = Lot2OldValue = row["ChargePallet"].ToString();
                                txtLot2.Tag = row["Id"].ToString();
                               
                            }
                            else if (i == 3)
                            {
                                lblLot3.Visible = true;
                                txtLot3.Visible = true;
                                lblLot3TotalQty.Visible = true;
                                txtLot3TotalQty.Visible = true;
                                lblLot3OkQty.Visible = true;
                                txtLot3OkQty.Visible = true;
                                lblLot3NgQty.Visible = true;
                                txtLot3NgQty.Visible = true;
                                txtLot3TotalQty.Text = row["TotalQty"].ToString();
                                txtLot3OkQty.Text = row["OkQty"].ToString();
                                txtLot3NgQty.Text = row["NgQty"].ToString();
                                txtLot3.Text = Lot3OldValue = row["ChargePallet"].ToString();
                                txtLot3.Tag = row["Id"].ToString();
                            }
                            else if (i == 4)
                            {
                                lblLot4.Visible = true;
                                txtLot4.Visible = true;
                                lblLot4TotalQty.Visible = true;
                                txtLot4TotalQty.Visible = true;
                                lblLot4OkQty.Visible = true;
                                txtLot4OkQty.Visible = true;
                                lblLot4NgQty.Visible = true;
                                txtLot4NgQty.Visible = true;
                                txtLot4TotalQty.Text = row["TotalQty"].ToString();
                                txtLot4OkQty.Text = row["OkQty"].ToString();
                                txtLot4NgQty.Text = row["NgQty"].ToString();
                                txtLot4.Text = Lot4OldValue = row["ChargePallet"].ToString();
                                txtLot4.Tag = row["Id"].ToString();
                            }
                            i++;
                        }
                        /*Show qty*/
                        txtTotalQty.Text = dt.Rows[0]["TotalQty"].ToString();
                        txtOKQty.Text = dt.Rows[0]["OkQty"].ToString();
                        txtNGQty.Text = dt.Rows[0]["NgQty"].ToString();
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
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtTrolleyCard.Text = "";
                cmbModel.SelectedIndex = 0;
                rdbLotNo.Checked = true;
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

        private void txtLot1Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !char.IsNumber(e.KeyChar))
                e.Handled = true;
        }

        private void txtLot2Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !char.IsNumber(e.KeyChar))
                e.Handled = true;
        }

        private void txtLot3Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !char.IsNumber(e.KeyChar))
                e.Handled = true;
        }

        private void txtLot4Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !char.IsNumber(e.KeyChar))
                e.Handled = true;
        }

        #endregion

        #region Radio Button Event
        private void rdbLotNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLotNo.Checked)
                cmbModel.Enabled = false;
            else if(rdbModel.Checked)
                cmbModel.Enabled = true;
            else
                cmbModel.Enabled = false;
        }

        #endregion

        #region Methods
        private void BindModel()
        {
            try
            {
                lblMessage.Text = "";

                //Bind Mondel
                DataTable dt = oDal.ManageModel(new Model { DbType = EnumDbType.SELECT });
                cmbModel.Items.Add("--Select--");

                foreach (DataRow row in dt.Rows)
                {
                    cmbModel.Items.Add(row["ModelNo"].ToString());
                }
                cmbModel.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void Clear()
        {
            try
            {
                lblMessage.Text = "";

                txtLot1.Text = "";
                
                lblLot2.Visible = false;
                txtLot2.Visible = false;
                lblLot2TotalQty.Visible = false;
                txtLot2TotalQty.Visible = false;
                lblLot2OkQty.Visible = false;
                txtLot2OkQty.Visible = false;
                lblLot2NgQty.Visible = false;
                txtLot2NgQty.Visible = false;

                lblLot3.Visible = false;
                txtLot3.Visible = false;
                lblLot3TotalQty.Visible = false;
                txtLot3TotalQty.Visible = false;
                lblLot3OkQty.Visible = false;
                txtLot3OkQty.Visible = false;
                lblLot3NgQty.Visible = false;
                txtLot3NgQty.Visible = false;
                
                lblLot4.Visible = false;
                txtLot4.Visible = false;
                lblLot4TotalQty.Visible = false;
                txtLot4TotalQty.Visible = false;
                lblLot4OkQty.Visible = false;
                txtLot4OkQty.Visible = false;
                lblLot4NgQty.Visible = false;
                txtLot4NgQty.Visible = false;

                txtLot2.Text = "";
                txtLot3.Text = "";
                txtLot4.Text = "";

                Lot1OldValue = ""; Lot2OldValue = ""; Lot3OldValue = ""; Lot4OldValue = "";
                txtLot1TotalQty.Text = txtLot1OkQty.Text = txtLot1NgQty.Text = "";
                txtLot2TotalQty.Text = txtLot2OkQty.Text = txtLot2NgQty.Text = "";
                txtLot3TotalQty.Text = txtLot3OkQty.Text = txtLot3NgQty.Text = "";
                txtLot4TotalQty.Text = txtLot4OkQty.Text = txtLot4NgQty.Text = "";
                txtTotalQty.Text = "";
                txtOKQty.Text = "";
                txtNGQty.Text = "";
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion
    }
}
