namespace TPR_App
{
    partial class frmCuttingDefect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCuttingDefect));
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.txtSample = new System.Windows.Forms.TextBox();
            this.txtExtraParam5 = new System.Windows.Forms.TextBox();
            this.txtPowerCut = new System.Windows.Forms.TextBox();
            this.txtDTB = new System.Windows.Forms.TextBox();
            this.txtOther = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSlag = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPenetration = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtOuterCavity = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtSpine = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtInnverCavity = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtCutting = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtCrack = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtOD = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtMaterialLD = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtDent = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtForeign = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDTBQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Navy;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(889, 39);
            this.lblHeader.TabIndex = 6;
            this.lblHeader.Text = "CUTTING - MATERIAL DEFECT";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Location = new System.Drawing.Point(6, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 334);
            this.panel1.TabIndex = 8;
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
            this.btnClose.Location = new System.Drawing.Point(804, 247);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 55);
            this.btnClose.TabIndex = 174;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Controls.Add(this.txtExtraParam5);
            this.groupBox1.Controls.Add(this.txtDTBQty);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSample);
            this.groupBox1.Controls.Add(this.txtPowerCut);
            this.groupBox1.Controls.Add(this.txtDTB);
            this.groupBox1.Controls.Add(this.txtOther);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSlag);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtPenetration);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.txtOuterCavity);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.txtSpine);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txtInnverCavity);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txtCutting);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.txtCrack);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtOD);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtMaterialLD);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtDent);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtForeign);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F);
            this.groupBox1.Location = new System.Drawing.Point(19, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(841, 237);
            this.groupBox1.TabIndex = 163;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Details";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(141, 189);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(125, 27);
            this.txtLength.TabIndex = 15;
            this.txtLength.Text = "0";
            this.txtLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // txtSample
            // 
            this.txtSample.Location = new System.Drawing.Point(409, 157);
            this.txtSample.Name = "txtSample";
            this.txtSample.Size = new System.Drawing.Size(125, 27);
            this.txtSample.TabIndex = 13;
            this.txtSample.Text = "0";
            this.txtSample.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // txtExtraParam5
            // 
            this.txtExtraParam5.Location = new System.Drawing.Point(409, 189);
            this.txtExtraParam5.Name = "txtExtraParam5";
            this.txtExtraParam5.Size = new System.Drawing.Size(125, 27);
            this.txtExtraParam5.TabIndex = 16;
            this.txtExtraParam5.Text = "0";
            this.txtExtraParam5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // txtPowerCut
            // 
            this.txtPowerCut.Location = new System.Drawing.Point(705, 157);
            this.txtPowerCut.Name = "txtPowerCut";
            this.txtPowerCut.Size = new System.Drawing.Size(125, 27);
            this.txtPowerCut.TabIndex = 14;
            this.txtPowerCut.Text = "0";
            this.txtPowerCut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // txtDTB
            // 
            this.txtDTB.Location = new System.Drawing.Point(141, 157);
            this.txtDTB.Name = "txtDTB";
            this.txtDTB.Size = new System.Drawing.Size(49, 27);
            this.txtDTB.TabIndex = 12;
            this.txtDTB.Text = "0";
            this.txtDTB.TextChanged += new System.EventHandler(this.txtDTB_TextChanged);
            this.txtDTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // txtOther
            // 
            this.txtOther.Location = new System.Drawing.Point(705, 125);
            this.txtOther.Name = "txtOther";
            this.txtOther.Size = new System.Drawing.Size(125, 27);
            this.txtOther.TabIndex = 11;
            this.txtOther.Text = "0";
            this.txtOther.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(656, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 19);
            this.label1.TabIndex = 33;
            this.label1.Text = "Other";
            // 
            // txtSlag
            // 
            this.txtSlag.Location = new System.Drawing.Point(141, 61);
            this.txtSlag.Name = "txtSlag";
            this.txtSlag.Size = new System.Drawing.Size(125, 27);
            this.txtSlag.TabIndex = 3;
            this.txtSlag.Text = "0";
            this.txtSlag.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(95, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 19);
            this.label11.TabIndex = 31;
            this.label11.Text = "Slag";
            // 
            // txtPenetration
            // 
            this.txtPenetration.Location = new System.Drawing.Point(705, 29);
            this.txtPenetration.Name = "txtPenetration";
            this.txtPenetration.Size = new System.Drawing.Size(125, 27);
            this.txtPenetration.TabIndex = 2;
            this.txtPenetration.Text = "0";
            this.txtPenetration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(562, 29);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(140, 19);
            this.label23.TabIndex = 29;
            this.label23.Text = "Penetration Porosity";
            // 
            // txtOuterCavity
            // 
            this.txtOuterCavity.Location = new System.Drawing.Point(409, 29);
            this.txtOuterCavity.Name = "txtOuterCavity";
            this.txtOuterCavity.Size = new System.Drawing.Size(125, 27);
            this.txtOuterCavity.TabIndex = 1;
            this.txtOuterCavity.Text = "0";
            this.txtOuterCavity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(309, 29);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(90, 19);
            this.label22.TabIndex = 27;
            this.label22.Text = "Outer Cavity";
            // 
            // txtSpine
            // 
            this.txtSpine.Location = new System.Drawing.Point(705, 61);
            this.txtSpine.Name = "txtSpine";
            this.txtSpine.Size = new System.Drawing.Size(125, 27);
            this.txtSpine.TabIndex = 5;
            this.txtSpine.Text = "0";
            this.txtSpine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(658, 63);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(44, 19);
            this.label21.TabIndex = 25;
            this.label21.Text = "Spine";
            // 
            // txtInnverCavity
            // 
            this.txtInnverCavity.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtInnverCavity.Location = new System.Drawing.Point(141, 29);
            this.txtInnverCavity.Name = "txtInnverCavity";
            this.txtInnverCavity.Size = new System.Drawing.Size(125, 27);
            this.txtInnverCavity.TabIndex = 0;
            this.txtInnverCavity.Text = "0";
            this.txtInnverCavity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label20.Location = new System.Drawing.Point(54, 29);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 16);
            this.label20.TabIndex = 23;
            this.label20.Text = "Inner Cavity";
            // 
            // txtCutting
            // 
            this.txtCutting.Location = new System.Drawing.Point(409, 125);
            this.txtCutting.Name = "txtCutting";
            this.txtCutting.Size = new System.Drawing.Size(125, 27);
            this.txtCutting.TabIndex = 10;
            this.txtCutting.Text = "0";
            this.txtCutting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(354, 95);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(45, 19);
            this.label24.TabIndex = 21;
            this.label24.Text = "Crack";
            // 
            // txtCrack
            // 
            this.txtCrack.Location = new System.Drawing.Point(409, 93);
            this.txtCrack.Name = "txtCrack";
            this.txtCrack.Size = new System.Drawing.Size(125, 27);
            this.txtCrack.TabIndex = 7;
            this.txtCrack.Text = "0";
            this.txtCrack.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(343, 128);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 19);
            this.label19.TabIndex = 21;
            this.label19.Text = "Cutting";
            // 
            // txtOD
            // 
            this.txtOD.Location = new System.Drawing.Point(141, 125);
            this.txtOD.Name = "txtOD";
            this.txtOD.Size = new System.Drawing.Size(125, 27);
            this.txtOD.TabIndex = 9;
            this.txtOD.Text = "0";
            this.txtOD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(101, 128);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(30, 19);
            this.label18.TabIndex = 19;
            this.label18.Text = "OD";
            // 
            // txtMaterialLD
            // 
            this.txtMaterialLD.Location = new System.Drawing.Point(705, 93);
            this.txtMaterialLD.Name = "txtMaterialLD";
            this.txtMaterialLD.Size = new System.Drawing.Size(125, 27);
            this.txtMaterialLD.TabIndex = 8;
            this.txtMaterialLD.Text = "0";
            this.txtMaterialLD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(617, 97);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(85, 19);
            this.label17.TabIndex = 17;
            this.label17.Text = "Material LD";
            // 
            // txtDent
            // 
            this.txtDent.Location = new System.Drawing.Point(409, 61);
            this.txtDent.Name = "txtDent";
            this.txtDent.Size = new System.Drawing.Size(125, 27);
            this.txtDent.TabIndex = 4;
            this.txtDent.Text = "0";
            this.txtDent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(359, 62);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 19);
            this.label14.TabIndex = 11;
            this.label14.Text = "Dent";
            // 
            // txtForeign
            // 
            this.txtForeign.Location = new System.Drawing.Point(141, 93);
            this.txtForeign.Name = "txtForeign";
            this.txtForeign.Size = new System.Drawing.Size(125, 27);
            this.txtForeign.TabIndex = 6;
            this.txtForeign.Text = "0";
            this.txtForeign.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefect_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 94);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(127, 19);
            this.label13.TabIndex = 9;
            this.label13.Text = "Foreign Substance";
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
            this.btnSave.Location = new System.Drawing.Point(661, 247);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 56);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "&Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(0, 314);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(875, 18);
            this.lblMessage.TabIndex = 139;
            this.lblMessage.Text = "test";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
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
            this.btnReset.Location = new System.Drawing.Point(734, 247);
            this.btnReset.Margin = new System.Windows.Forms.Padding(5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(66, 55);
            this.btnReset.TabIndex = 173;
            this.btnReset.Text = "&Reset";
            this.btnReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 19);
            this.label2.TabIndex = 34;
            this.label2.Text = "DTB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 19);
            this.label3.TabIndex = 36;
            this.label3.Text = "Qty";
            // 
            // txtDTBQty
            // 
            this.txtDTBQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtDTBQty.Location = new System.Drawing.Point(229, 157);
            this.txtDTBQty.Name = "txtDTBQty";
            this.txtDTBQty.ReadOnly = true;
            this.txtDTBQty.Size = new System.Drawing.Size(37, 27);
            this.txtDTBQty.TabIndex = 35;
            this.txtDTBQty.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(343, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 19);
            this.label4.TabIndex = 37;
            this.label4.Text = "Sample";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(630, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 38;
            this.label5.Text = "Powercut";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 19);
            this.label6.TabIndex = 39;
            this.label6.Text = "Length";
            // 
            // frmCuttingDefect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(889, 383);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCuttingDefect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CUTTING DEFECT";
            this.Load += new System.EventHandler(this.frmModelMaster_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSlag;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPenetration;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtOuterCavity;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtSpine;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtInnverCavity;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtCutting;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtCrack;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtOD;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtMaterialLD;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDent;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtForeign;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.TextBox txtSample;
        private System.Windows.Forms.TextBox txtExtraParam5;
        private System.Windows.Forms.TextBox txtPowerCut;
        private System.Windows.Forms.TextBox txtDTB;
        private System.Windows.Forms.TextBox txtOther;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDTBQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}