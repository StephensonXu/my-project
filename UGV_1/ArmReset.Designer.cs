namespace UGV_1
{
    partial class ArmReset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArmReset));
            this.Stop = new System.Windows.Forms.PictureBox();
            this.Reseting = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Stop)).BeginInit();
            this.SuspendLayout();
            // 
            // Stop
            // 
            this.Stop.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.Stop, "Stop");
            this.Stop.Name = "Stop";
            this.Stop.TabStop = false;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Reseting
            // 
            resources.ApplyResources(this.Reseting, "Reseting");
            this.Reseting.ForeColor = System.Drawing.Color.Red;
            this.Reseting.Name = "Reseting";
            // 
            // ArmReset
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.Reseting);
            this.Controls.Add(this.Stop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ArmReset";
            this.Opacity = 0.6D;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.Stop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Stop;
        private System.Windows.Forms.Label Reseting;
    }
}