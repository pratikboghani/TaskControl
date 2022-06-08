namespace UTC
{
    partial class frmSelect
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GrpDetail = new System.Windows.Forms.GroupBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lstVw = new System.Windows.Forms.ListView();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnAll = new System.Windows.Forms.Button();
            this.GrpDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(3, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 7);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // GrpDetail
            // 
            this.GrpDetail.BackColor = System.Drawing.SystemColors.Control;
            this.GrpDetail.Controls.Add(this.BtnAll);
            this.GrpDetail.Controls.Add(this.BtnClose);
            this.GrpDetail.Controls.Add(this.groupBox1);
            this.GrpDetail.Controls.Add(this.lstVw);
            this.GrpDetail.Controls.Add(this.btnClear);
            this.GrpDetail.Controls.Add(this.txtSearch);
            this.GrpDetail.Controls.Add(this.lblSearch);
            this.GrpDetail.Location = new System.Drawing.Point(3, -3);
            this.GrpDetail.Margin = new System.Windows.Forms.Padding(0);
            this.GrpDetail.Name = "GrpDetail";
            this.GrpDetail.Padding = new System.Windows.Forms.Padding(0);
            this.GrpDetail.Size = new System.Drawing.Size(463, 535);
            this.GrpDetail.TabIndex = 0;
            this.GrpDetail.TabStop = false;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.SystemColors.Control;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.Black;
            this.lblSearch.Location = new System.Drawing.Point(6, 18);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(55, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search :";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Indigo;
            this.txtSearch.Location = new System.Drawing.Point(65, 14);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(222, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.MistyRose;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnClear.FlatAppearance.BorderSize = 2;
            this.btnClear.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnClear.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(348, 15);
            this.btnClear.Margin = new System.Windows.Forms.Padding(0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(50, 21);
            this.btnClear.TabIndex = 6;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lstVw
            // 
            this.lstVw.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstVw.BackColor = System.Drawing.Color.LavenderBlush;
            this.lstVw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstVw.CheckBoxes = true;
            this.lstVw.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstVw.FullRowSelect = true;
            this.lstVw.GridLines = true;
            this.lstVw.HideSelection = false;
            this.lstVw.HoverSelection = true;
            this.lstVw.Location = new System.Drawing.Point(9, 55);
            this.lstVw.MultiSelect = false;
            this.lstVw.Name = "lstVw";
            this.lstVw.Size = new System.Drawing.Size(446, 455);
            this.lstVw.TabIndex = 9;
            this.lstVw.UseCompatibleStateImageBehavior = false;
            this.lstVw.View = System.Windows.Forms.View.Details;
            this.lstVw.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstVwLot_ItemCheck);
            
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.MistyRose;
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.BtnClose.FlatAppearance.BorderSize = 2;
            this.BtnClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnClose.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.Location = new System.Drawing.Point(399, 15);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(50, 21);
            this.BtnClose.TabIndex = 7;
            this.BtnClose.TabStop = false;
            this.BtnClose.Text = "&Close";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnAll
            // 
            this.BtnAll.BackColor = System.Drawing.Color.MistyRose;
            this.BtnAll.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.BtnAll.FlatAppearance.BorderSize = 2;
            this.BtnAll.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnAll.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAll.Location = new System.Drawing.Point(296, 15);
            this.BtnAll.Margin = new System.Windows.Forms.Padding(0);
            this.BtnAll.Name = "BtnAll";
            this.BtnAll.Size = new System.Drawing.Size(50, 21);
            this.BtnAll.TabIndex = 11;
            this.BtnAll.TabStop = false;
            this.BtnAll.Text = "&All";
            this.BtnAll.UseVisualStyleBackColor = false;
            this.BtnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // frmSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnClose;
            this.ClientSize = new System.Drawing.Size(470, 541);
            this.ControlBox = false;
            this.Controls.Add(this.GrpDetail);
            this.KeyPreview = true;
            this.Name = "frmSelect";
            this.Text = "-";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSelect_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmSelect_KeyPress);
            this.GrpDetail.ResumeLayout(false);
            this.GrpDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.GroupBox GrpDetail;
        internal System.Windows.Forms.Button BtnAll;
        internal System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.ListView lstVw;
        internal System.Windows.Forms.Button btnClear;
        internal System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.Label lblSearch;

    }
}