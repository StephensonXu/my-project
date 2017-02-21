namespace UGV_1
{
    partial class DemoArm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoArm));
            this.Stop = new System.Windows.Forms.PictureBox();
            this.Demo = new System.Windows.Forms.Label();
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
            // Demo
            // 
            resources.ApplyResources(this.Demo, "Demo");
            this.Demo.ForeColor = System.Drawing.Color.Red;
            this.Demo.Name = "Demo";
            // 
            // DemoArm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.Demo);
            this.Controls.Add(this.Stop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DemoArm";
            this.Opacity = 0.6D;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.Stop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Stop;
        private System.Windows.Forms.Label Demo;
    }
}