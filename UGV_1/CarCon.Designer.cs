namespace UGV_1
{
    partial class CarCon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarCon));
            this.DemoCar = new System.Windows.Forms.PictureBox();
            this.LowSpeed = new System.Windows.Forms.PictureBox();
            this.Rotate = new System.Windows.Forms.PictureBox();
            this.HighSpeed = new System.Windows.Forms.PictureBox();
            this.Control = new System.Windows.Forms.PictureBox();
            this.BackGround = new System.Windows.Forms.PictureBox();
            this.CarICON = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DemoCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LowSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Control)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarICON)).BeginInit();
            this.SuspendLayout();
            // 
            // DemoCar
            // 
            this.DemoCar.BackColor = System.Drawing.Color.Transparent;
            this.DemoCar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DemoCar.BackgroundImage")));
            this.DemoCar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DemoCar.Location = new System.Drawing.Point(20, 400);
            this.DemoCar.Name = "DemoCar";
            this.DemoCar.Size = new System.Drawing.Size(110, 90);
            this.DemoCar.TabIndex = 6;
            this.DemoCar.TabStop = false;
            this.DemoCar.Click += new System.EventHandler(this.DemoCar_Click);
            // 
            // LowSpeed
            // 
            this.LowSpeed.BackColor = System.Drawing.Color.Transparent;
            this.LowSpeed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LowSpeed.BackgroundImage")));
            this.LowSpeed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LowSpeed.Location = new System.Drawing.Point(1155, 255);
            this.LowSpeed.Name = "LowSpeed";
            this.LowSpeed.Size = new System.Drawing.Size(90, 90);
            this.LowSpeed.TabIndex = 5;
            this.LowSpeed.TabStop = false;
            this.LowSpeed.Visible = false;
            this.LowSpeed.Click += new System.EventHandler(this.LowSpeed_Click);
            // 
            // Rotate
            // 
            this.Rotate.BackColor = System.Drawing.Color.Transparent;
            this.Rotate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Rotate.BackgroundImage")));
            this.Rotate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rotate.Location = new System.Drawing.Point(995, 210);
            this.Rotate.Name = "Rotate";
            this.Rotate.Size = new System.Drawing.Size(90, 90);
            this.Rotate.TabIndex = 4;
            this.Rotate.TabStop = false;
            this.Rotate.Visible = false;
            this.Rotate.Click += new System.EventHandler(this.Rotate_Click);
            // 
            // HighSpeed
            // 
            this.HighSpeed.BackColor = System.Drawing.Color.Transparent;
            this.HighSpeed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("HighSpeed.BackgroundImage")));
            this.HighSpeed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.HighSpeed.Location = new System.Drawing.Point(835, 255);
            this.HighSpeed.Name = "HighSpeed";
            this.HighSpeed.Size = new System.Drawing.Size(90, 90);
            this.HighSpeed.TabIndex = 3;
            this.HighSpeed.TabStop = false;
            this.HighSpeed.Visible = false;
            this.HighSpeed.Click += new System.EventHandler(this.HighSpeed_Click);
            // 
            // Control
            // 
            this.Control.BackColor = System.Drawing.Color.Transparent;
            this.Control.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Control.BackgroundImage")));
            this.Control.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Control.Location = new System.Drawing.Point(1005, 445);
            this.Control.Name = "Control";
            this.Control.Size = new System.Drawing.Size(70, 70);
            this.Control.TabIndex = 2;
            this.Control.TabStop = false;
            this.Control.Visible = false;
            this.Control.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.Control.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.Control.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // BackGround
            // 
            this.BackGround.BackColor = System.Drawing.Color.Transparent;
            this.BackGround.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BackGround.BackgroundImage")));
            this.BackGround.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackGround.Location = new System.Drawing.Point(860, 300);
            this.BackGround.Name = "BackGround";
            this.BackGround.Size = new System.Drawing.Size(360, 360);
            this.BackGround.TabIndex = 1;
            this.BackGround.TabStop = false;
            this.BackGround.Visible = false;
            // 
            // CarICON
            // 
            this.CarICON.BackColor = System.Drawing.Color.Transparent;
            this.CarICON.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CarICON.BackgroundImage")));
            this.CarICON.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CarICON.Location = new System.Drawing.Point(1240, 10);
            this.CarICON.Name = "CarICON";
            this.CarICON.Size = new System.Drawing.Size(100, 100);
            this.CarICON.TabIndex = 0;
            this.CarICON.TabStop = false;
            this.CarICON.Click += new System.EventHandler(this.CarICON_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(394, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 179);
            this.panel1.TabIndex = 7;
            // 
            // CarCon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DemoCar);
            this.Controls.Add(this.LowSpeed);
            this.Controls.Add(this.Rotate);
            this.Controls.Add(this.HighSpeed);
            this.Controls.Add(this.Control);
            this.Controls.Add(this.BackGround);
            this.Controls.Add(this.CarICON);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CarCon";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CarCon";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            ((System.ComponentModel.ISupportInitialize)(this.DemoCar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LowSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Control)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarICON)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox CarICON;
        private System.Windows.Forms.PictureBox BackGround;
        private System.Windows.Forms.PictureBox Control;
        private System.Windows.Forms.PictureBox HighSpeed;
        private System.Windows.Forms.PictureBox Rotate;
        private System.Windows.Forms.PictureBox LowSpeed;
        private System.Windows.Forms.PictureBox DemoCar;
        private System.Windows.Forms.Panel panel1;


    }
}