namespace UGV_1
{
    partial class NoCamra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoCamra));
            this.NoCamera = new System.Windows.Forms.Label();
            this.NoCameraBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.NoCameraBox)).BeginInit();
            this.SuspendLayout();
            // 
            // NoCamera
            // 
            this.NoCamera.AutoSize = true;
            this.NoCamera.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NoCamera.ForeColor = System.Drawing.Color.Red;
            this.NoCamera.Location = new System.Drawing.Point(81, 31);
            this.NoCamera.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.NoCamera.Name = "NoCamera";
            this.NoCamera.Size = new System.Drawing.Size(286, 21);
            this.NoCamera.TabIndex = 0;
            this.NoCamera.Text = "Error: No Local Camera!";
            // 
            // NoCameraBox
            // 
            this.NoCameraBox.BackColor = System.Drawing.Color.Transparent;
            this.NoCameraBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("NoCameraBox.BackgroundImage")));
            this.NoCameraBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NoCameraBox.Location = new System.Drawing.Point(12, 12);
            this.NoCameraBox.Name = "NoCameraBox";
            this.NoCameraBox.Size = new System.Drawing.Size(60, 60);
            this.NoCameraBox.TabIndex = 1;
            this.NoCameraBox.TabStop = false;
            this.NoCameraBox.Click += new System.EventHandler(this.NoCameraBox_Click);
            // 
            // NoCamra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(380, 80);
            this.Controls.Add(this.NoCameraBox);
            this.Controls.Add(this.NoCamera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NoCamra";
            this.Opacity = 0.6D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NoCamra";
            ((System.ComponentModel.ISupportInitialize)(this.NoCameraBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NoCamera;
        private System.Windows.Forms.PictureBox NoCameraBox;
    }
}