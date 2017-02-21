namespace UGV_1
{
    partial class LostConnect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LostConnect));
            this.Stop = new System.Windows.Forms.PictureBox();
            this.Breakdown = new System.Windows.Forms.Label();
            this.Restart = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Stop)).BeginInit();
            this.SuspendLayout();
            // 
            // Stop
            // 
            this.Stop.BackColor = System.Drawing.Color.Transparent;
            this.Stop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Stop.BackgroundImage")));
            this.Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Stop.Location = new System.Drawing.Point(12, 5);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(70, 70);
            this.Stop.TabIndex = 0;
            this.Stop.TabStop = false;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Breakdown
            // 
            this.Breakdown.AutoSize = true;
            this.Breakdown.BackColor = System.Drawing.Color.Transparent;
            this.Breakdown.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Breakdown.ForeColor = System.Drawing.Color.Red;
            this.Breakdown.Location = new System.Drawing.Point(103, 9);
            this.Breakdown.Name = "Breakdown";
            this.Breakdown.Size = new System.Drawing.Size(298, 21);
            this.Breakdown.TabIndex = 1;
            this.Breakdown.Text = "Communication Breakdown!";
            // 
            // Restart
            // 
            this.Restart.AutoSize = true;
            this.Restart.BackColor = System.Drawing.Color.Transparent;
            this.Restart.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Restart.ForeColor = System.Drawing.Color.Red;
            this.Restart.Location = new System.Drawing.Point(103, 39);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(226, 21);
            this.Restart.TabIndex = 2;
            this.Restart.Text = "Cheak and Restart!";
            // 
            // LostConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(413, 78);
            this.Controls.Add(this.Restart);
            this.Controls.Add(this.Breakdown);
            this.Controls.Add(this.Stop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LostConnect";
            this.Opacity = 0.7D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LostConnect";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.Stop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Stop;
        private System.Windows.Forms.Label Breakdown;
        private System.Windows.Forms.Label Restart;
    }
}