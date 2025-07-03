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
    public partial class frmShiftMaster : Form
    {
        #region Variables

        Dal oDal;
        Shift oShift;
        bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmShiftMaster()
        {
            try
            {
                InitializeComponent();
                oShift = new Shift();
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
                txtShift.Focus();
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
                    oShift.ShiftName = txtShift.Text.Trim();
                    oShift.StartTime = txtStartTime.Text.Trim();
                    oShift.EndTime = txtEndTime.Text.Trim();
                    oShift.CreatedBy = ClsGlobal.UserId;
                    //If saving data
                    if (_IsUpdate == false)
                    {
                        oShift.DbType = EnumDbType.INSERT;
                        string Msg=oDal.ManageShift(oShift).Rows[0]["RESULT"].ToString().ToUpper();
                        if (Msg == "Y")
                        {
                            btnReset_Click(sender, e);
                            ClsGlobal.SetConfirmMessage("Saved successfully!!", lblMessage);
                        }
                        else
                            ClsGlobal.SetInfoMessage(Msg, lblMessage);
                    }
                    else // if updating data
                    {
                        oShift.DbType = EnumDbType.UPDATE;
                        oDal.ManageShift(oShift);
                        btnReset_Click(sender, e);
                        ClsGlobal.SetConfirmMessage("Updated successfully!!", lblMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    ClsGlobal.SetErrorMessage("Shift Name already exist!!", lblMessage);
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
                if (string.IsNullOrEmpty(txtShift.Text))
                {
                    ClsGlobal.SetInfoMessage("Please select shift", lblMessage);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("Äre you sure to delete the record !!", ClsGlobal.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    oShift.ShiftName = txtShift.Text.Trim();
                    oShift.DbType = EnumDbType.DELETE;
                    oDal.ManageShift(oShift);

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

        #endregion

        #region Methods

        private void Clear()
        {
            try
            {
                txtShift.Text = "";
                txtStartTime.Text = DateTime.Now.ToString("HH:mm");
                txtEndTime.Text = DateTime.Now.ToString("HH:mm");
                lblMessage.Text = "";
                txtShift.Enabled = true;
                btnSave.Enabled = true;
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
                oShift.DbType = EnumDbType.SELECT;
                DataTable dt = oDal.ManageShift(oShift);
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
                if (txtShift.Text.Trim().Length == 0)
                {
                    ClsGlobal.SetInfoMessage("Shift name can't be blank!!", lblMessage);
                    txtShift.Focus();
                    return false;
                }
                //if (Convert.ToDateTime(txtStartTime.Value.ToString("HH:mm")) > Convert.ToDateTime(txtEndTime.Value.ToString("HH:mm")))
                //{
                //    ClsGlobal.SetInfoMessage("End time should be greater than start time!!", lblMessage);
                //    return false;
                //}
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
                txtShift.Text = dgv.Rows[e.RowIndex].Cells["ShiftName"].Value.ToString();

                btnDelete.Enabled = true;
                txtShift.Enabled = false;
                btnSave.Enabled = false;
                _IsUpdate = true;
            }
            catch (Exception ex)
            {
                ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }
        }

        #endregion

       
    }
}
