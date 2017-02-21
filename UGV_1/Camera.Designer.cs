namespace UGV_1
{
    partial class Camera
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Camera));
            this.videPlayer = new AForge.Controls.VideoSourcePlayer();
            this.Video = new System.Windows.Forms.PictureBox();
            this.axVLCPlugin = new AxAXVLC.AxVLCPlugin2();
            ((System.ComponentModel.ISupportInitialize)(this.Video)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin)).BeginInit();
            this.SuspendLayout();
            // 
            // videPlayer
            // 
            this.videPlayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.videPlayer.Location = new System.Drawing.Point(0, 0);
            this.videPlayer.Name = "videPlayer";
            this.videPlayer.Size = new System.Drawing.Size(1366, 768);
            this.videPlayer.TabIndex = 20;
            this.videPlayer.Text = "videPlayer";
            this.videPlayer.VideoSource = null;
            this.videPlayer.Visible = false;
            // 
            // Video
            // 
            this.Video.BackColor = System.Drawing.Color.Transparent;
            this.Video.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Video.BackgroundImage")));
            this.Video.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Video.Location = new System.Drawing.Point(20, 0);
            this.Video.Name = "Video";
            this.Video.Size = new System.Drawing.Size(110, 90);
            this.Video.TabIndex = 21;
            this.Video.TabStop = false;
            this.Video.Click += new System.EventHandler(this.Video_Click);
            // 
            // axVLCPlugin
            // 
            this.axVLCPlugin.Enabled = true;
            this.axVLCPlugin.Location = new System.Drawing.Point(0, 0);
            this.axVLCPlugin.Name = "axVLCPlugin";
            this.axVLCPlugin.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVLCPlugin.OcxState")));
            this.axVLCPlugin.Size = new System.Drawing.Size(1365, 768);
            this.axVLCPlugin.TabIndex = 22;
            this.axVLCPlugin.Visible = false;
            // 
            // Camera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.axVLCPlugin);
            this.Controls.Add(this.Video);
            this.Controls.Add(this.videPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Camera";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            ((System.ComponentModel.ISupportInitialize)(this.Video)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AForge.Controls.VideoSourcePlayer videPlayer;
        private System.Windows.Forms.PictureBox Video;
        private AxAXVLC.AxVLCPlugin2 axVLCPlugin;

    }
}