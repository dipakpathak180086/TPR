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
    public partial class frmRptReOiling : Form
    {
        #region Variables

        Dal oDal;
        bool IsOnlyPending = false;

        #endregion

        #region Form Methods

        public frmRptReOiling(bool OnlyPending)
        {
            try
            {
                InitializeComponent();
                oDal = new Dal();
                IsOnlyPending = OnlyPending;
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

                dgv.AutoGenerateColumns = false;
                lblMessage.Text = "";
                GetModel();
                if (IsOnlyPending) btnGet_Click(sender, e);
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
        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (Convert.ToDateTime(dtpFromDate.Text) > Convert.ToDateTime(dtpToDate.Text))
                {
                    ClsGlobal.SetInfoMessage("To date can no be less than from date!!", lblMessage);
                    return;
                }
                string ModelNo = cmbModelNo.SelectedIndex > 0 ? cmbModelNo.SelectedItem.ToString() : "";
                IsOnlyPending = rdbPending.Checked ? true : false;
                DataTable dt = oDal.GetReOilingReportData(dtpFromDate.Text, dtpToDate.Text, ModelNo, IsOnlyPending);
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
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
                cmbModelNo.SelectedIndex = 0;
                rdbPending.Checked = true;
                dgv.DataSource = null;
                lblCount.Text = "Rows Count : 0";
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

        #endregion

        #region Label Event
        private void lblMessage_DoubleClick(object sender, EventArgs e)
        {
            ClsGlobal.ShowInfoMessageBox(lblMessage.Text);
        }


        #endregion

        #region RadioButton Event
        private void rdbPending_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPending.Checked)
                pnlFilter.Enabled = false;
        }

        private void rdbComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbComplete.Checked)
                pnlFilter.Enabled = true;
        }

        #endregion
    }
}
