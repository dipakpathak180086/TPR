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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace TPR_App
{
    public partial class frmTrolleyMaster : Form
    {
        #region Variables

        Dal oDal;
        Trolley oTrolley;
        bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmTrolleyMaster()
        {
            try
            {
                InitializeComponent();
                oTrolley = new Trolley();
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

                btnDelete.Enabled = false;
                // btnPrint.Enabled = false;
                lblMessage.Text = "";
                txtTolleyNo.Focus();
                BindGrid();
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (ValidateInput())
                {
                    oTrolley.TrolleyNo = txtTolleyNo.Text.Trim();
                    oTrolley.Description = txtDesc.Text.Trim();
                    oTrolley.PackSize = int.Parse(txtPackSize.Text.Trim());
                    oTrolley.IsReturnAble = chkReturnable.Checked;
                    oTrolley.CreatedBy = ClsGlobal.UserId;

                    if (chkNG.Checked)
                    {
                        oTrolley.IsNG = true;
                    }
                    else {
                        oTrolley.IsNG = false;
                    }
                    //If saving data
                    if (_IsUpdate == false)
                    {
                        oTrolley.DbType = EnumDbType.INSERT;
                        oDal.ManageTrolley(oTrolley);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                    }
                    else // if updating data
                    {
                        oTrolley.DbType = EnumDbType.UPDATE;
                        oDal.ManageTrolley(oTrolley);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Updated successfully!!", lblMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    ClsGlobal.SetErrorMessage("Trolley No already exist!!", lblMessage);
                }
                else
                {
                    ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
                }
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                Clear();
                BindGrid();
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
                lblMessage.Text = "";
                if (string.IsNullOrEmpty(txtTolleyNo.Text))
                {
                    ClsGlobal.SetInfoMessage("Please select trolley", lblMessage);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("Äre you sure to delete the record !!", ClsGlobal.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    oTrolley.TrolleyNo = txtTolleyNo.Text.Trim();
                    oTrolley.DbType = EnumDbType.DELETE;
                    oDal.ManageTrolley(oTrolley);

                    btnReset_Click(sender, e);
                    ClsGlobal.SetConfirmMessage("Deleted successfully!!", lblMessage);
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void bntPrint_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                List<string> _ListSelectedTrolley = new List<string>();
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["SelectTrolley"].Value) == true)
                    {
                        _ListSelectedTrolley.Add(row.Cells["TrolleyNo"].Value.ToString());
                        //if (_ListSelectedTrolley.Count == 2)//Commmented by dipak 01-12-21 user can print multiple or single
                        //    break;
                    }
                }
                if (_ListSelectedTrolley.Count > 0)//added by dipak 01-12-21 user can print multiple or single
                {
                    string PrintMessage = Print2Label(_ListSelectedTrolley);
                    if (PrintMessage.ToUpper() == "OK")
                    {
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Print Sucessfully!!", lblMessage);
                    }
                    else
                        ClsGlobal.SetConfirmMessage(PrintMessage, lblMessage);
                }
                else
                    ClsGlobal.SetInfoMessage("Select Trolley", lblMessage);

                //if (txtTolleyNo.Text.Trim() != "" && _IsUpdate == true)
                //{
                //    string PrintMessage = PrintLabel(txtTolleyNo.Text.Trim());
                //    if (PrintMessage.ToUpper() == "OK")
                //    {
                //        btnReset_Click(sender, e);
                //        ClsGlobal.SetConfirmMessage("Print Sucessfully!!", lblMessage);
                //    }
                //    else
                //        ClsGlobal.SetConfirmMessage(PrintMessage, lblMessage);
                //}
                //else
                //    ClsGlobal.SetInfoMessage("Select Trolley", lblMessage);
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

        private void Clear()
        {
            try
            {
                txtTolleyNo.Text = "";
                txtDesc.Text = "";
                txtPackSize.Text = "";
                lblMessage.Text = "";
                txtTolleyNo.Enabled = true;
                btnDelete.Enabled = false;
                chkNG.Checked= false;
                chkNG.Enabled=false;
                //btnPrint.Enabled = false;
                _IsUpdate = false;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void BindGrid()
        {
            try
            {
                lblMessage.Text = "";
                oTrolley.DbType = EnumDbType.SELECT;
                DataTable dt = oDal.ManageTrolley(oTrolley);
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
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
                if (txtTolleyNo.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Trolley No can't be blank!!", lblMessage);
                    txtTolleyNo.Focus();
                    return false;
                }
                if (txtDesc.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Trolley Description can't be blank!!", lblMessage);
                    txtDesc.Focus();
                    return false;
                }
                if (txtPackSize.Text.Trim().Length == 0 || int.Parse(txtPackSize.Text.Trim()) == 0)
                {
                    ClsGlobal.SetInfoMessage("Please enter pack size!!", lblMessage);
                    txtPackSize.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        private string Print2Label(List<string> ListBarcode)
        {
            try
            {
                string PrinterName = Properties.Settings.Default.PrinterName;

                StreamReader sr = new StreamReader(Application.StartupPath + "\\" + ClsGlobal.TrolleyPrnName);
                string PrnFileTemp = sr.ReadToEnd();
                sr.Close();
                PrnFileTemp = PrnFileTemp.Replace("{VAR1LEN}", ListBarcode[0].Length.ToString());
                PrnFileTemp = PrnFileTemp.Replace("{VAR1}", ListBarcode[0]);
                if (ListBarcode.Count > 1)
                {
                    PrnFileTemp = PrnFileTemp.Replace("{VAR2LEN}", ListBarcode[1].Length.ToString());
                    PrnFileTemp = PrnFileTemp.Replace("{VAR2}", ListBarcode[1]);
                }
                else
                {
                    PrnFileTemp = PrnFileTemp.Replace("{VAR2LEN}", "");
                    PrnFileTemp = PrnFileTemp.Replace("{VAR2}", "");
                }
                return PrintBarcode.PrintCommand(PrnFileTemp, PrinterName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //private string PrintLabel(string BarCode)
        //{
        //    try
        //    {
        //        string PrinterName = Properties.Settings.Default.PrinterName;

        //        StreamReader sr = new StreamReader(Application.StartupPath + "\\" + ClsGlobal.TrolleyPrnName);
        //        string PrnFileTemp = sr.ReadToEnd();
        //        sr.Close();
        //        PrnFileTemp = PrnFileTemp.Replace("{VARLEN}", BarCode.Length.ToString());
        //        PrnFileTemp = PrnFileTemp.Replace("{VAR1}", BarCode);
        //        return PrintBarcode.PrintCommand(PrnFileTemp, PrinterName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        #endregion

        #region Label Event
        private void lblMessage_DoubleClick(object sender, EventArgs e)
        {
            ClsGlobal.ShowInfoMessageBox(lblMessage.Text);
        }

        #endregion

        #region DataGridView Events
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Clear();

                txtTolleyNo.Text = dgv.Rows[e.RowIndex].Cells["TrolleyNo"].Value.ToString();
                txtPackSize.Text = dgv.Rows[e.RowIndex].Cells["PackSize"].Value.ToString();
                txtDesc.Text = dgv.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                chkReturnable.Checked = Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells["IsReturnAble"].Value);
                if(Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells["IsNG"].Value)==true)
                {
                    chkNG.Checked = true;
                }
                btnDelete.Enabled = true;
                //btnPrint.Enabled = true;
                txtTolleyNo.Enabled = false;
                chkNG.Enabled = true;
                _IsUpdate = true;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

        #region TextBox Event

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                oTrolley.DbType = EnumDbType.SEARCH;
                oTrolley.TrolleyNo = txtSearch.Text.Trim();
                DataTable dt = oDal.ManageTrolley(oTrolley);
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void txtPackSize_KeyPress(object sender, KeyPressEventArgs e)
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


    }
}
