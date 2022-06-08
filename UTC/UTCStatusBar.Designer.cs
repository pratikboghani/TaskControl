namespace UTC
{
    partial class UTCStatusBar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.lblCapsLock = new System.Windows.Forms.Label();
            this.TimerCaps = new System.Windows.Forms.Timer(this.components);
            this.TimerErrorMessage = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblErrorMessage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblErrorMessage.Location = new System.Drawing.Point(3, 1);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(200, 20);
            this.lblErrorMessage.TabIndex = 0;
            this.lblErrorMessage.Text = "lblErrorMessage";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCompanyName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCompanyName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.Blue;
            this.lblCompanyName.Location = new System.Drawing.Point(544, 1);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(387, 20);
            this.lblCompanyName.TabIndex = 0;
            this.lblCompanyName.Text = "lblCompanyName";
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCompanyName.Click += new System.EventHandler(this.lblCompanyName_Click);
            // 
            // lblCapsLock
            // 
            this.lblCapsLock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCapsLock.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCapsLock.Location = new System.Drawing.Point(220, 1);
            this.lblCapsLock.Name = "lblCapsLock";
            this.lblCapsLock.Size = new System.Drawing.Size(318, 20);
            this.lblCapsLock.TabIndex = 0;
            this.lblCapsLock.Text = "lblCapsLock";
            // 
            // TimerCaps
            // 
            this.TimerCaps.Enabled = true;
            this.TimerCaps.Interval = 300;
            this.TimerCaps.Tick += new System.EventHandler(this.TimerCaps_Tick);
            // 
            // TimerErrorMessage
            // 
            this.TimerErrorMessage.Enabled = true;
            this.TimerErrorMessage.Interval = 3000;
            this.TimerErrorMessage.Tick += new System.EventHandler(this.TimerErrorMessage_Tick);
            // 
            // PTStatusBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.lblCapsLock);
            this.Controls.Add(this.lblErrorMessage);
            this.Name = "PTStatusBar";
            this.Size = new System.Drawing.Size(930, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer TimerCaps;
        private System.Windows.Forms.Timer TimerErrorMessage;
        public System.Windows.Forms.Label lblErrorMessage;
        public System.Windows.Forms.Label lblCompanyName;
        public System.Windows.Forms.Label lblCapsLock;

    }
}
