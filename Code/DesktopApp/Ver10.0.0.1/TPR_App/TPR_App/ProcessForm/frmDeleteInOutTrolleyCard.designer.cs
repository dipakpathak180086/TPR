namespace TPR_App
{
    partial class frmDeleteInOutTrolleyCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeleteInOutTrolleyCard));
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOkQty = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.txtSearchFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbProcess = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
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
            this.lblHeader.Size = new System.Drawing.Size(583, 27);
            this.lblHeader.TabIndex = 6;
            this.lblHeader.Text = "DELETE IN-OUT TROLLEY CARD";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbProcess);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtOkQty);
            this.panel1.Controls.Add(this.btnGo);
            this.panel1.Controls.Add(this.txtSearchFilter);
            this.panel1.Controls.Add(this.lblFilter);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Location = new System.Drawing.Point(6, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(571, 263);
            this.panel1.TabIndex = 8;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.Location = new System.Drawing.Point(344, 137);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 56);
            this.btnDelete.TabIndex = 217;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F);
            this.label3.Location = new System.Drawing.Point(103, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 19);
            this.label3.TabIndex = 213;
            this.label3.Text = "Qty";
            // 
            // txtOkQty
            // 
            this.txtOkQty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtOkQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtOkQty.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtOkQty.Location = new System.Drawing.Point(141, 88);
            this.txtOkQty.MaxLength = 9;
            this.txtOkQty.Name = "txtOkQty";
            this.txtOkQty.ReadOnly = true;
            this.txtOkQty.Size = new System.Drawing.Size(304, 27);
            this.txtOkQty.TabIndex = 212;
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.BackColor = System.Drawing.Color.LightCoral;
            this.btnGo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGo.FlatAppearance.BorderSize = 0;
            this.btnGo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.ForeColor = System.Drawing.Color.White;
            this.btnGo.Location = new System.Drawing.Point(446, 50);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(46, 27);
            this.btnGo.TabIndex = 183;
            this.btnGo.Text = "Go";
            this.btnGo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtSearchFilter
            // 
            this.txtSearchFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSearchFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchFilter.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtSearchFilter.Location = new System.Drawing.Point(141, 47);
            this.txtSearchFilter.Name = "txtSearchFilter";
            this.txtSearchFilter.Size = new System.Drawing.Size(304, 27);
            this.txtSearchFilter.TabIndex = 0;
            this.txtSearchFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTrolleyCard_KeyPress);
            // 
            // lblFilter
            // 
            this.lblFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Calibri", 12F);
            this.lblFilter.Location = new System.Drawing.Point(11, 50);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(124, 19);
            this.lblFilter.TabIndex = 180;
            this.lblFilter.Text = "Enter Trolley Card";
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
            this.btnClose.Location = new System.Drawing.Point(484, 137);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 55);
            this.btnClose.TabIndex = 174;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.btnReset.Location = new System.Drawing.Point(412, 137);
            this.btnReset.Margin = new System.Windows.Forms.Padding(5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(66, 55);
            this.btnReset.TabIndex = 173;
            this.btnReset.Text = "&Reset";
            this.btnReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(0, 243);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(569, 18);
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
            this.btnMinimize.Location = new System.Drawing.Point(547, 2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(28, 32);
            this.btnMinimize.TabIndex = 211;
            this.btnMinimize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F);
            this.label2.Location = new System.Drawing.Point(33, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 19);
            this.label2.TabIndex = 221;
            this.label2.Text = "Select Process";
            // 
            // cmbProcess
            // 
            this.cmbProcess.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbProcess.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbProcess.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProcess.Font = new System.Drawing.Font("Calibri", 12F);
            this.cmbProcess.FormattingEnabled = true;
            this.cmbProcess.Items.AddRange(new object[] {
            "--Select---",
            "CUTTING",
            "MACHINING",
            "PACKING"});
            this.cmbProcess.Location = new System.Drawing.Point(141, 6);
            this.cmbProcess.Name = "cmbProcess";
            this.cmbProcess.Size = new System.Drawing.Size(304, 27);
            this.cmbProcess.TabIndex = 220;
            // 
            // frmDeleteInOutTrolleyCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(583, 312);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDeleteInOutTrolleyCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CUTTING CARD UPDATE";
            this.Load += new System.EventHandler(this.frmModelMaster_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.TextBox txtSearchFilter;
        private System.Windows.Forms.Label lblFilter;
        public System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOkQty;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbProcess;
    }
}