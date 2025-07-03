using System;
using System.Data;
using System.Windows.Forms;

namespace TPR_App
{
    public partial class frmColorMaster : Form
    {


        #region Variables

        Dal oDal;
        Colors oColor;
        bool _IsUpdate = false;
        string sRowId = string.Empty;
        #endregion

        #region Form Methods

        public frmColorMaster()
        {
            try
            {
                InitializeComponent();
                oColor = new Colors();
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
                lblMessage.Text = "";
                txtColorName.Focus();
                BindGrid();
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
                lblMessage.Text = "";
                if (ValidateInput())
                {
                    oColor.RowId = sRowId == "" ? 0 : Convert.ToInt64(sRowId);
                    oColor.ColorName = txtColorName.Text.Trim();
                    oColor.CreatedBy = ClsGlobal.UserId;
                    //If saving data
                    if (_IsUpdate == false)
                    {
                        oColor.DbType = EnumDbType.INSERT;
                        oDal.ManageColor(oColor);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                    }
                    else // if updating data
                    {
                        oColor.DbType = EnumDbType.UPDATE;
                        oDal.ManageColor(oColor);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Updated successfully!!", lblMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    ClsGlobal.SetErrorMessage("Color name already exist!!", lblMessage);
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
                if (string.IsNullOrEmpty(txtColorName.Text))
                {
                    ClsGlobal.SetInfoMessage("Please Enter Color", lblMessage);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("Äre you sure to delete the record !!", ClsGlobal.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    oColor.ColorName = txtColorName.Text.Trim();
                    oColor.DbType = EnumDbType.DELETE;
                    oDal.ManageColor(oColor);

                    btnReset_Click(sender, e);
                    ClsGlobal.SetConfirmMessage("Deleted successfully!!", lblMessage);
                }
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

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaxiMize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        #endregion

        #region Methods

        private void Clear()
        {
            try
            {
                txtColorName.Text = "";
                lblMessage.Text = "";
                txtColorName.Enabled = true;
                btnDelete.Enabled = false;
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
                oColor.DbType = EnumDbType.SELECT;
                DataTable dt = oDal.ManageColor(oColor);
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
                if (txtColorName.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Color Name can't be blank!!", lblMessage);
                    txtColorName.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex) { throw ex; }
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
                sRowId = string.Empty;
                sRowId = dgv.Rows[e.RowIndex].Cells["RowId"].Value.ToString();
                txtColorName.Text = dgv.Rows[e.RowIndex].Cells["ColorName"].Value.ToString();
                btnDelete.Enabled = true;
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
                oColor.DbType = EnumDbType.SEARCH;
                oColor.ColorName = txtSearch.Text.Trim();
                DataTable dt = oDal.ManageColor(oColor);
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
