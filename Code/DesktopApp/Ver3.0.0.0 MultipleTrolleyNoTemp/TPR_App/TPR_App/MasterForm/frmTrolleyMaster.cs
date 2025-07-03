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
                    oTrolley.CreatedBy = ClsGlobal.UserId;
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
                //if (txtTolleyNo.Text.Trim() != "" && _IsUpdate == true)
                //{
                string PrintMessage = PrintLabel(txtTolleyNo.Text.Trim());
                //if (PrintMessage.ToUpper() == "OK")
                //{
                btnReset_Click(sender, e);
                ClsGlobal.SetConfirmMessage("Print Sucessfully!!", lblMessage);
                //}
                //else
                //    ClsGlobal.SetConfirmMessage(PrintMessage, lblMessage);
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

        private string PrintLabel(string BarCode)
        {
            try
            {
                string PrinterName = Properties.Settings.Default.PrinterName;

                StreamReader sr = new StreamReader(Application.StartupPath + "\\Trolley4.prn");
                string PrnFileTemp = sr.ReadToEnd();
                sr.Close();
                int counter = 0;
                string PrnData = PrnFileTemp;
                for (int i = 0; i < dgv.SelectedRows.Count; i++)
                {
                    PrnData = PrnData.Replace("{VAR" + (counter + 1) + "LEN}", dgv.SelectedRows[i].Cells["TrolleyNo"].Value.ToString().Length.ToString());
                    PrnData = PrnData.Replace("{VAR" + (counter + 1) + "}", dgv.SelectedRows[i].Cells["TrolleyNo"].Value.ToString());
                    if ((counter+1) % 4 == 0 && counter != 0)
                    {
                        PrintBarcode.PrintCommand(PrnData, PrinterName);
                        counter = 0;
                        PrnData = PrnFileTemp;
                    }
                    else
                        counter++;
                }
                return "OK";
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

        #region DataGridView Events
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Clear();
                txtTolleyNo.Text = dgv.Rows[e.RowIndex].Cells["TrolleyNo"].Value.ToString();
                txtPackSize.Text = dgv.Rows[e.RowIndex].Cells["PackSize"].Value.ToString();
                txtDesc.Text = dgv.Rows[e.RowIndex].Cells["Description"].Value.ToString();

                btnDelete.Enabled = true;
                //btnPrint.Enabled = true;
                txtTolleyNo.Enabled = false;
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
