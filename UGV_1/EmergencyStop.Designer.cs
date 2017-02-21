namespace UGV_1
{
    partial class EmergencyStop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmergencyStop));
            this.EmergencyStopBox = new System.Windows.Forms.PictureBox();
            this.Back = new System.Windows.Forms.PictureBox();
            this.Stop = new System.Windows.Forms.PictureBox();
            this.Emergency_Stop = new System.Windows.Forms.Label();
            this.CloseICON = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.EmergencyStopBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseICON)).BeginInit();
            this.SuspendLayout();
            // 
            // EmergencyStopBox
            // 
            this.EmergencyStopBox.BackColor = System.Drawing.Color.Transparent;
            this.EmergencyStopBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("EmergencyStopBox.BackgroundImage")));
            this.EmergencyStopBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EmergencyStopBox.Location = new System.Drawing.Point(1260, 658);
            this.EmergencyStopBox.Name = "EmergencyStopBox";
            this.EmergencyStopBox.Size = new System.Drawing.Size(90, 90);
            this.EmergencyStopBox.TabIndex = 0;
            this.EmergencyStopBox.TabStop = false;
            this.EmergencyStopBox.Click += new System.EventHandler(this.EmergencyStopBox_Click);
            // 
            // Back
            // 
            this.Back.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Back.Location = new System.Drawing.Point(493, 349);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(380, 70);
            this.Back.TabIndex = 1;
            this.Back.TabStop = false;
            this.Back.Visible = false;
            // 
            // Stop
            // 
            this.Stop.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Stop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Stop.BackgroundImage")));
            this.Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Stop.Location = new System.Drawing.Point(505, 356);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(60, 60);
            this.Stop.TabIndex = 2;
            this.Stop.TabStop = false;
            this.Stop.Visible = false;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Emergency_Stop
            // 
            this.Emergency_Stop.AutoSize = true;
            this.Emergency_Stop.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Emergency_Stop.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Emergency_Stop.ForeColor = System.Drawing.Color.Red;
            this.Emergency_Stop.Location = new System.Drawing.Point(597, 377);
            this.Emergency_Stop.Name = "Emergency_Stop";
            this.Emergency_Stop.Size = new System.Drawing.Size(202, 21);
            this.Emergency_Stop.TabIndex = 1;
            this.Emergency_Stop.Text = "Emergency Stop!!";
            this.Emergency_Stop.Visible = false;
            // 
            // CloseICON
            // 
            this.CloseICON.BackColor = System.Drawing.Color.Transparent;
            this.CloseICON.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseICON.BackgroundImage")));
            this.CloseICON.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseICON.Location = new System.Drawing.Point(10, 658);
            this.CloseICON.Name = "CloseICON";
            this.CloseICON.Size = new System.Drawing.Size(90, 90);
            this.CloseICON.TabIndex = 3;
            this.CloseICON.TabStop = false;
            this.CloseICON.Click += new System.EventHandler(this.CloseICON_Click);
            // 
            // EmergencyStop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.CloseICON);
            this.Controls.Add(this.Emergency_Stop);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.EmergencyStopBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EmergencyStop";
            this.ShowInTaskbar = false;
            this.Text = "EmergencyStop";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            ((System.ComponentModel.ISupportInitialize)(this.EmergencyStopBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseICON)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox EmergencyStopBox;
        private System.Windows.Forms.PictureBox Back;
        private System.Windows.Forms.PictureBox Stop;
        private System.Windows.Forms.Label Emergency_Stop;
        private System.Windows.Forms.PictureBox CloseICON;
    }
}