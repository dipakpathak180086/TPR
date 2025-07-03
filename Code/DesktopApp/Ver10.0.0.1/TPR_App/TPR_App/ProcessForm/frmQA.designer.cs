namespace TPR_App
{
    partial class frmQA
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQA));
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbNg = new System.Windows.Forms.RadioButton();
            this.rdbHold = new System.Windows.Forms.RadioButton();
            this.rdbOk = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtTrolleyCard = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.rdbPartialNG = new System.Windows.Forms.RadioButton();
            this.txtNgQty = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNgReason = new System.Windows.Forms.TextBox();
            this.pnlNg = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOkQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbLotNo = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlNg.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Navy;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(451, 27);
            this.lblHeader.TabIndex = 6;
            this.lblHeader.Text = "QA";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.txtTrolleyCard);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Location = new System.Drawing.Point(6, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 398);
            this.panel1.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlNg);
            this.groupBox1.Controls.Add(this.rdbPartialNG);
            this.groupBox1.Controls.Add(this.rdbNg);
            this.groupBox1.Controls.Add(this.rdbHold);
            this.groupBox1.Controls.Add(this.rdbOk);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F);
            this.groupBox1.Location = new System.Drawing.Point(9, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 205);
            this.groupBox1.TabIndex = 175;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "QA Option";
            // 
            // rdbNg
            // 
            this.rdbNg.AutoSize = true;
            this.rdbNg.Location = new System.Drawing.Point(100, 34);
            this.rdbNg.Name = "rdbNg";
            this.rdbNg.Size = new System.Drawing.Size(47, 23);
            this.rdbNg.TabIndex = 2;
            this.rdbNg.TabStop = true;
            this.rdbNg.Text = "NG";
            this.rdbNg.UseVisualStyleBackColor = true;
            this.rdbNg.CheckedChanged += new System.EventHandler(this.QACheckChange);
            // 
            // rdbHold
            // 
            this.rdbHold.AutoSize = true;
            this.rdbHold.Location = new System.Drawing.Point(169, 34);
            this.rdbHold.Name = "rdbHold";
            this.rdbHold.Size = new System.Drawing.Size(65, 23);
            this.rdbHold.TabIndex = 1;
            this.rdbHold.TabStop = true;
            this.rdbHold.Text = "HOLD";
            this.rdbHold.UseVisualStyleBackColor = true;
            this.rdbHold.CheckedChanged += new System.EventHandler(this.QACheckChange);
            // 
            // rdbOk
            // 
            this.rdbOk.AutoSize = true;
            this.rdbOk.Location = new System.Drawing.Point(32, 34);
            this.rdbOk.Name = "rdbOk";
            this.rdbOk.Size = new System.Drawing.Size(46, 23);
            this.rdbOk.TabIndex = 0;
            this.rdbOk.TabStop = true;
            this.rdbOk.Text = "OK";
            this.rdbOk.UseVisualStyleBackColor = true;
            this.rdbOk.CheckedChanged += new System.EventHandler(this.QACheckChange);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnClose.Image = global::TPR_App.Properties.Resources.Delete;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(364, 302);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 55);
            this.btnClose.TabIndex = 174;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnSave.Image = global::TPR_App.Properties.Resources.iconfinder_Save_1493294;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(221, 301);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 56);
            this.btnSave.TabIndex = 172;
            this.btnSave.Text = "&Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnReset.Image = global::TPR_App.Properties.Resources._1336028501_001_39;
            this.btnReset.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReset.Location = new System.Drawing.Point(294, 302);
            this.btnReset.Margin = new System.Windows.Forms.Padding(5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(66, 55);
            this.btnReset.TabIndex = 173;
            this.btnReset.Text = "&Reset";
            this.btnReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtTrolleyCard
            // 
            this.txtTrolleyCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtTrolleyCard.Location = new System.Drawing.Point(9, 40);
            this.txtTrolleyCard.Name = "txtTrolleyCard";
            this.txtTrolleyCard.Size = new System.Drawing.Size(423, 22);
            this.txtTrolleyCard.TabIndex = 0;
            this.txtTrolleyCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTrolleyCard_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 19);
            this.label6.TabIndex = 159;
            this.label6.Text = "Scan/Enter Trolley Card";
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(0, 378);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(437, 18);
            this.lblMessage.TabIndex = 139;
            this.lblMessage.Text = "test";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblMessage.DoubleClick += new System.EventHandler(this.lblMessage_DoubleClick);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.BackgroundImage = global::TPR_App.Properties.Resources.iconfinder_minus_remove_delete_minimize_2931142;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnMinimize.Location = new System.Drawing.Point(415, 2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(28, 32);
            this.btnMinimize.TabIndex = 211;
            this.btnMinimize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // rdbPartialNG
            // 
            this.rdbPartialNG.AutoSize = true;
            this.rdbPartialNG.Location = new System.Drawing.Point(256, 34);
            this.rdbPartialNG.Name = "rdbPartialNG";
            this.rdbPartialNG.Size = new System.Drawing.Size(93, 23);
            this.rdbPartialNG.TabIndex = 3;
            this.rdbPartialNG.TabStop = true;
            this.rdbPartialNG.Text = "Partial NG";
            this.rdbPartialNG.UseVisualStyleBackColor = true;
            this.rdbPartialNG.CheckedChanged += new System.EventHandler(this.QACheckChange);
            // 
            // txtNgQty
            // 
            this.txtNgQty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNgQty.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtNgQty.Location = new System.Drawing.Point(283, 46);
            this.txtNgQty.MaxLength = 9;
            this.txtNgQty.Name = "txtNgQty";
            this.txtNgQty.Size = new System.Drawing.Size(112, 27);
            this.txtNgQty.TabIndex = 206;
            this.txtNgQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F);
            this.label2.Location = new System.Drawing.Point(221, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 19);
            this.label2.TabIndex = 207;
            this.label2.Text = "NG Qty";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F);
            this.label1.Location = new System.Drawing.Point(3, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 19);
            this.label1.TabIndex = 208;
            this.label1.Text = "NG Reason";
            // 
            // txtNgReason
            // 
            this.txtNgReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtNgReason.Location = new System.Drawing.Point(104, 76);
            this.txtNgReason.MaxLength = 500;
            this.txtNgReason.Multiline = true;
            this.txtNgReason.Name = "txtNgReason";
            this.txtNgReason.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNgReason.Size = new System.Drawing.Size(291, 45);
            this.txtNgReason.TabIndex = 209;
            // 
            // pnlNg
            // 
            this.pnlNg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNg.Controls.Add(this.label4);
            this.pnlNg.Controls.Add(this.cmbLotNo);
            this.pnlNg.Controls.Add(this.label3);
            this.pnlNg.Controls.Add(this.txtOkQty);
            this.pnlNg.Controls.Add(this.txtNgReason);
            this.pnlNg.Controls.Add(this.label2);
            this.pnlNg.Controls.Add(this.txtNgQty);
            this.pnlNg.Controls.Add(this.label1);
            this.pnlNg.Location = new System.Drawing.Point(6, 63);
            this.pnlNg.Name = "pnlNg";
            this.pnlNg.Size = new System.Drawing.Size(411, 136);
            this.pnlNg.TabIndex = 176;
            this.pnlNg.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F);
            this.label3.Location = new System.Drawing.Point(52, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 19);
            this.label3.TabIndex = 211;
            this.label3.Text = "Qty";
            // 
            // txtOkQty
            // 
            this.txtOkQty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtOkQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtOkQty.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtOkQty.Location = new System.Drawing.Point(104, 46);
            this.txtOkQty.MaxLength = 9;
            this.txtOkQty.Name = "txtOkQty";
            this.txtOkQty.ReadOnly = true;
            this.txtOkQty.Size = new System.Drawing.Size(112, 27);
            this.txtOkQty.TabIndex = 210;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F);
            this.label4.Location = new System.Drawing.Point(33, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 19);
            this.label4.TabIndex = 213;
            this.label4.Text = "Lot No";
            // 
            // cmbLotNo
            // 
            this.cmbLotNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbLotNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLotNo.Font = new System.Drawing.Font("Calibri", 12F);
            this.cmbLotNo.FormattingEnabled = true;
            this.cmbLotNo.Location = new System.Drawing.Point(104, 13);
            this.cmbLotNo.Name = "cmbLotNo";
            this.cmbLotNo.Size = new System.Drawing.Size(291, 27);
            this.cmbLotNo.TabIndex = 212;
            this.cmbLotNo.SelectionChangeCommitted += new System.EventHandler(this.cmbLotNo_SelectionChangeCommitted);
            // 
            // frmQA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(451, 447);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmQA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QA";
            this.Load += new System.EventHandler(this.frmModelMaster_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlNg.ResumeLayout(false);
            this.pnlNg.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtTrolleyCard;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbNg;
        private System.Windows.Forms.RadioButton rdbHold;
        private System.Windows.Forms.RadioButton rdbOk;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.RadioButton rdbPartialNG;
        private System.Windows.Forms.TextBox txtNgReason;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNgQty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlNg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOkQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbLotNo;
    }
}