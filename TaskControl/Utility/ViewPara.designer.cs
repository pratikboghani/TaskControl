namespace TaskControl
{
    partial class ViewPara
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            this.UltGrdViewPara = new UTC.UTCGrid(this.components);
            this.ViewPara_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.ultraPanel3 = new Infragistics.Win.Misc.UltraPanel();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.txtPass = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            ((System.ComponentModel.ISupportInitialize)(this.UltGrdViewPara)).BeginInit();
            this.ViewPara_Fill_Panel.ClientArea.SuspendLayout();
            this.ViewPara_Fill_Panel.SuspendLayout();
            this.ultraPanel3.ClientArea.SuspendLayout();
            this.ultraPanel3.SuspendLayout();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPass)).BeginInit();
            this.SuspendLayout();
            // 
            // UltGrdViewPara
            // 
            this.UltGrdViewPara.ActiveCellName = "";
            this.UltGrdViewPara.AllowNegativeValue = false;
            this.UltGrdViewPara.ByPassDownArrayKey = false;
            this.UltGrdViewPara.DataBoundMode = UTC.UTCGrid.EnumBounUnbound.Default;
            this.UltGrdViewPara.DisplayLayout.GroupByBox.Style = Infragistics.Win.UltraWinGrid.GroupByBoxStyle.Compact;
            appearance1.FontData.BoldAsString = "True";
            appearance1.TextHAlignAsString = "Center";
            this.UltGrdViewPara.DisplayLayout.Override.HeaderAppearance = appearance1;
            this.UltGrdViewPara.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.UltGrdViewPara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UltGrdViewPara.EnableExcelStyleEnter = false;
            this.UltGrdViewPara.IsCapital = false;
            this.UltGrdViewPara.IsCellActivated = false;
            this.UltGrdViewPara.IsCopyPaste = false;
            this.UltGrdViewPara.IsEditMode = false;
            this.UltGrdViewPara.IsGroupHeaderExported = true;
            this.UltGrdViewPara.IsHideAdvanceOption = false;
            this.UltGrdViewPara.IsRightClickEnabled = true;
            this.UltGrdViewPara.IsRightClickVisible = true;
            this.UltGrdViewPara.IsSetColumnEnabled = false;
            this.UltGrdViewPara.Location = new System.Drawing.Point(0, 0);
            this.UltGrdViewPara.Name = "UltGrdViewPara";
            this.UltGrdViewPara.PersistBackColor = false;
            this.UltGrdViewPara.PromptSaveDialog = false;
            this.UltGrdViewPara.RowMultiSelectWithMouse = UTC.UTCGrid.EnumMultiSelect.No;
            this.UltGrdViewPara.SelectedUpdate = UTC.UTCGrid.EnumSelectedFieldsUpdation.Default;
            this.UltGrdViewPara.Size = new System.Drawing.Size(1081, 462);
            this.UltGrdViewPara.TabIndex = 1;
            this.UltGrdViewPara.ToolTips = "";
            this.UltGrdViewPara.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.UltGrdViewPara_InitializeLayout);
            this.UltGrdViewPara.AfterRowsDeleted += new System.EventHandler(this.UltGrdViewPara_AfterRowsDeleted);
            this.UltGrdViewPara.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.UltGrdViewPara_AfterRowUpdate);
            // 
            // ViewPara_Fill_Panel
            // 
            // 
            // ViewPara_Fill_Panel.ClientArea
            // 
            this.ViewPara_Fill_Panel.ClientArea.Controls.Add(this.ultraPanel3);
            this.ViewPara_Fill_Panel.ClientArea.Controls.Add(this.ultraPanel1);
            this.ViewPara_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.ViewPara_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewPara_Fill_Panel.Location = new System.Drawing.Point(0, 0);
            this.ViewPara_Fill_Panel.Name = "ViewPara_Fill_Panel";
            this.ViewPara_Fill_Panel.Size = new System.Drawing.Size(1081, 503);
            this.ViewPara_Fill_Panel.TabIndex = 0;
            // 
            // ultraPanel3
            // 
            // 
            // ultraPanel3.ClientArea
            // 
            this.ultraPanel3.ClientArea.Controls.Add(this.UltGrdViewPara);
            this.ultraPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraPanel3.Location = new System.Drawing.Point(0, 0);
            this.ultraPanel3.Name = "ultraPanel3";
            this.ultraPanel3.Size = new System.Drawing.Size(1081, 462);
            this.ultraPanel3.TabIndex = 4;
            // 
            // ultraPanel1
            // 
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.txtPass);
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ultraPanel1.Location = new System.Drawing.Point(0, 462);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(1081, 41);
            this.ultraPanel1.TabIndex = 2;
            // 
            // txtPass
            // 
            appearance2.BackColor = System.Drawing.Color.Transparent;
            appearance2.BorderAlpha = Infragistics.Win.Alpha.Transparent;
            appearance2.BorderColor = System.Drawing.Color.Transparent;
            appearance2.BorderColor2 = System.Drawing.Color.Transparent;
            this.txtPass.Appearance = appearance2;
            this.txtPass.BackColor = System.Drawing.Color.Transparent;
            this.txtPass.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.txtPass.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Office2013;
            this.txtPass.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.Location = new System.Drawing.Point(6, 10);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(65, 20);
            this.txtPass.TabIndex = 22;
            this.txtPass.TabStop = false;
            this.txtPass.UseAppStyling = false;
            this.txtPass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.txtPass.ValueChanged += new System.EventHandler(this.txtPass_TextChanged);
            // 
            // ViewPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 503);
            this.Controls.Add(this.ViewPara_Fill_Panel);
            this.Name = "ViewPara";
            this.Text = "ViewPara";
            this.Load += new System.EventHandler(this.ViewPara_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltGrdViewPara)).EndInit();
            this.ViewPara_Fill_Panel.ClientArea.ResumeLayout(false);
            this.ViewPara_Fill_Panel.ResumeLayout(false);
            this.ultraPanel3.ClientArea.ResumeLayout(false);
            this.ultraPanel3.ResumeLayout(false);
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ClientArea.PerformLayout();
            this.ultraPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPass)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UTC.UTCGrid UltGrdViewPara;
        private Infragistics.Win.Misc.UltraPanel ViewPara_Fill_Panel;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPass;
        private Infragistics.Win.Misc.UltraPanel ultraPanel3;
    }
}