namespace UTC
{
    partial class UTCButton : Infragistics.Win.Misc.UltraButton 
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

            components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance AppHotTrack = new Infragistics.Win.Appearance();
            ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            AppHotTrack.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            AppHotTrack.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            AppHotTrack.BackColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            AppHotTrack.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            AppHotTrack.BackHatchStyle = Infragistics.Win.BackHatchStyle.Horizontal;
            HotTrackAppearance = AppHotTrack;
        }

        #endregion
    }
}
