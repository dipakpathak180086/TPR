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
    public partial class frmMachineMaster : Form
    {
        #region Variables

        Dal oDal;
        Machine oMachine;
        bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmMachineMaster()
        {
            try
            {
                InitializeComponent();
                oMachine = new Machine();
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
                this.WindowState = FormWindowState.Maximized;
                btnDelete.Enabled = false;
                btnPrint.Enabled = false;
                lblMessage.Text = "";
                txtMachineNo.Focus();
                BindGrid();
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
                if (ValidateInput())
                {
                    oMachine.MachineNo = txtMachineNo.Text.Trim();
                    oMachine.Description = txtMachineDesc.Text.Trim();
                    oMachine.CreatedBy = ClsGlobal.UserId;
                    //If saving data
                    if (_IsUpdate == false)
                    {
                        oMachine.DbType = EnumDbType.INSERT;
                        oDal.ManageMachine(oMachine);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                    }
                    else // if updating data
                    {
                        oMachine.DbType = EnumDbType.UPDATE;
                        oDal.ManageMachine(oMachine);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Updated successfully!!", lblMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    ClsGlobal.SetErrorMessage("Machine No already exist!!", lblMessage);
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
                if (string.IsNullOrEmpty(txtMachineNo.Text))
                {
                    ClsGlobal.SetInfoMessage("Please select machine", lblMessage);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("Äre you sure to delete the record !!", ClsGlobal.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    oMachine.MachineNo = txtMachineNo.Text.Trim();
                    oMachine.DbType = EnumDbType.DELETE;
                    oDal.ManageMachine(oMachine);

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
            { }
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
                txtMachineDesc.Text = "";
                txtMachineNo.Text = "";
                txtSearch.Text = "";
                lblMessage.Text = "";
                btnDelete.Enabled = false;
                btnPrint.Enabled = false;
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
                oMachine.DbType = EnumDbType.SELECT;
                DataTable dt = oDal.ManageMachine(oMachine);
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
                if (txtMachineNo.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Machine No can't be blank!!", lblMessage);
                    txtMachineNo.Focus();
                    return false;
                }
                if (txtMachineDesc.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Machine Description can't be blank!!", lblMessage);
                    txtMachineDesc.Focus();
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

                StreamReader sr = new StreamReader(Application.StartupPath + "\\" + ClsGlobal.MachiningPrnName);
                string PrnFileTemp = sr.ReadToEnd();
                sr.Close();
                PrnFileTemp = PrnFileTemp.Replace("{VARLEN}", BarCode.Length.ToString());
                PrnFileTemp = PrnFileTemp.Replace("{VAR1}", BarCode);
                return PrintBarcode.PrintCommand(PrnFileTemp, PrinterName);
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
                txtMachineNo.Text = dgv.Rows[e.RowIndex].Cells["MachineNo"].Value.ToString();
                txtMachineDesc.Text = dgv.Rows[e.RowIndex].Cells["Description"].Value.ToString();

                btnDelete.Enabled = true;
                btnPrint.Enabled = true;
                txtMachineNo.Enabled = false;
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
                oMachine.DbType = EnumDbType.SEARCH;
                oMachine.MachineNo = txtSearch.Text.Trim();
                DataTable dt = oDal.ManageMachine(oMachine);
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

      
    }
}
