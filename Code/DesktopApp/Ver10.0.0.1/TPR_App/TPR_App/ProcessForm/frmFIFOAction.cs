using System;
using System.Data;
using System.Windows.Forms;

namespace TPR_App
{
    public partial class frmFIFOAction : Form
    {
        #region Variables

        private Dal oDal;
        private FIFOAction oFifoAction;
        bool _IsUpdate = false;
        #endregion

        #region Form Methods

        public frmFIFOAction()
        {
            try
            {
                InitializeComponent();

                oFifoAction = new FIFOAction();
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
                GetFIFOActionData();
                GetModel();
                GetProcess();
                dgv.Columns["FifoAction"].ReadOnly = false;
                lblMessage.Text = "";

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
                if (ValidateField())
                {
                    if (rbtnSKU.Checked)
                    {
                        bool Action = false;
                        if (chkAction.Checked)
                        {
                            Action = true;
                        }

                        //If saving data
                        if (_IsUpdate == false)
                        {
                            DataTable dt = oDal.SaveFifosku("INSERT_SKU_WISE_FIFO", cmbProcess.SelectedItem.ToString(), Action, cmbModelNo.SelectedItem.ToString());

                            if (dt.Rows.Count > 0)
                            {
                                btnReset_Click(sender, e);
                                ClsGlobal.SetConfirmMessage("Save Successfully!!", lblMessage);
                            }
                            else
                            {
                                ClsGlobal.SetConfirmMessage("Data Not Saved!!", lblMessage);
                            }
                        }
                        else // if updating data
                        {
                            DataTable dt = oDal.SaveFifosku("UPDATE_SKU_WISE_FIFO", cmbProcess.SelectedItem.ToString(), Action, cmbModelNo.SelectedItem.ToString());

                            if (dt.Rows.Count > 0)
                            {
                                btnReset_Click(sender, e);
                                ClsGlobal.SetConfirmMessage("Updated Successfully!!", lblMessage);
                            }
                            else
                            {
                                ClsGlobal.SetConfirmMessage("Data Not Saved!!", lblMessage);
                            }
                        }
                    }
                    else
                    {
                        DataTable dt = Save();
                        if (dt.Rows.Count > 0)
                        {
                            btnReset_Click(sender, e);
                            ClsGlobal.SetConfirmMessage("Updated Successfully!!", lblMessage);
                        }
                        else
                        {
                            ClsGlobal.SetConfirmMessage("Data Not Saved!!", lblMessage);
                        }
                    }
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

                GetFIFOActionData();
                lblMessage.Text = "";
                if (cmbProcess.SelectedIndex > 0)
                {
                    cmbModelNo.SelectedIndex = 0;
                }
                if (cmbProcess.SelectedIndex > 0)
                {
                    cmbProcess.SelectedIndex = 0;
                }

                cmbModelNo.Enabled = false;
                cmbProcess.Enabled = false;
                _IsUpdate = false;
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

        #region TextBox Events

        #endregion

        #region Methods

        private void Clear()
        {
            try
            {
                lblMessage.Text = "";

                if (cmbProcess.SelectedIndex > 0)
                {
                    cmbModelNo.SelectedIndex = 0;
                }
                if (cmbProcess.SelectedIndex > 0)
                {
                    cmbProcess.SelectedIndex = 0;
                }
                cmbModelNo.Enabled = false;
                cmbProcess.Enabled = false;
                _IsUpdate = false;
                lblMessage.Text = "";

            }
            catch (Exception ex) { throw ex; }
        }
        private bool ValidateField()
        {
            try
            {
                if (rbtnSKU.Checked)
                {
                    if (cmbProcess.SelectedIndex <= 0)
                    {
                        ClsGlobal.SetInfoMessage("Please select Process!!", lblMessage);
                        cmbProcess.Focus();
                        return false;
                    }
                    if (cmbModelNo.SelectedIndex <= 0)
                    {
                        ClsGlobal.SetInfoMessage("Please select model!!", lblMessage);
                        cmbModelNo.Focus();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        private void GetFIFOActionData()
        {
            try
            {
                if (rbtnSKU.Checked)
                {

                    DataTable dt = oDal.SaveFifoDisableEnable("GETFIFOSKU");

                    dgv.DataSource = dt;
                }
                else
                {
                    DataTable dt = oDal.SaveFifoDisableEnable("SELECT");
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable Save()
        {
            try
            {
                DataTable dt = null;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dt = oDal.SaveFifoDisableEnable("UPDATE", dgv.Rows[i].Cells["Code"].Value.ToString(), Convert.ToBoolean(dgv.Rows[i].Cells["FifoAction"].Value));

                }
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        private void rbtnProcess_CheckedChanged(object sender, EventArgs e)
        {
            GetProcess();


        }

        private void GetModel()
        {
            try
            {
                DataTable dt = oDal.GetModel();
                cmbModelNo.Items.Add("--Select--");
                foreach (DataRow row in dt.Rows)
                {
                    cmbModelNo.Items.Add(row["ModelNo"].ToString());
                }
                cmbModelNo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void GetProcess()
        {
            try
            {
                cmbProcess.Items.Clear();
                DataTable dt = oDal.SaveFifoDisableEnable("SELECT_PROCESS");
                cmbProcess.Items.Add("--Select--");
                foreach (DataRow row in dt.Rows)
                {
                    cmbProcess.Items.Add(row["Process"].ToString());
                }
                cmbProcess.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
        private void rbtnSKU_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbProcess.Items.Count > 0)
            {
                cmbProcess.Items.Remove("MachiningPicking");
            }



        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                Clear();
                cmbProcess.SelectedItem = dgv.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                cmbModelNo.SelectedItem = dgv.Rows[e.RowIndex].Cells["Process"].Value.ToString();
                if (dgv.Rows[e.RowIndex].Cells["FifoAction"].Value.ToString() == "True")
                {
                    chkAction.Checked = true;
                }
                cmbProcess.Enabled = false;
                _IsUpdate = true;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        private void rbtnProcess_Click(object sender, EventArgs e)
        {
            rbtnSKU.Checked = false;
            rbtnProcess.Checked = true;
            cmbProcess.Enabled = false;
            cmbModelNo.Enabled = false;
            btnDelete.Enabled = true;
            GetFIFOActionData();

        }

        private void rbtnSKU_Click(object sender, EventArgs e)
        {
            rbtnSKU.Checked = true;
            rbtnProcess.Checked = false;
            cmbProcess.Enabled = true;
            cmbModelNo.Enabled = true;
            chkAction.Checked = true;
            btnDelete.Enabled = true;
            GetFIFOActionData();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (ValidateField())
                {
                    if (rbtnSKU.Checked)
                    {
                        bool Action = false;
                        if (chkAction.Checked)
                        {
                            Action = true;
                        }

                        //If saving data
                        if (_IsUpdate == true)
                        {
                            DataTable dt = oDal.SaveFifosku("DELETE_SKU_WISE_FIFO", cmbProcess.SelectedItem.ToString(), Action, cmbModelNo.SelectedItem.ToString());

                            if (dt.Rows.Count > 0)
                            {
                                btnReset_Click(sender, e);
                                ClsGlobal.SetConfirmMessage("Delete Successfully!!", lblMessage);
                            }
                            else
                            {
                                ClsGlobal.SetConfirmMessage("Data Not Saved!!", lblMessage);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }
    }
}
