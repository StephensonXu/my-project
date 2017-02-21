using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UGV_1
{
    public partial class EmergencyStop : Form
    {
        //事件
        public event Timedelegate Dogtimer;
        public event ModeCodeWrtdelegate EmergencyStopMode;
        public event MoveCodeClrdelegate EmergencyStopMove;
        public event ResetEmergencyflagdelegate ResetEmergencyflag;
        public event SetEmergencyflagdelegate SetEmergencyflag;
        //进行图标缩放
        int ScreenRefWidth = 1366;
        int ScreenRefHeigh = 768;
        int iActualWidth;//屏幕宽度
        int iActualHeight;//屏幕高度
        public float ScaleFactor;

        public EmergencyStop()
        {
            InitializeComponent();
            //屏幕缩放
            iActualWidth = Screen.PrimaryScreen.Bounds.Width;
            iActualHeight = Screen.PrimaryScreen.Bounds.Height;
            if (((float)iActualWidth / ScreenRefWidth) - ((float)iActualHeight / ScreenRefHeigh) < 0)
            {
                ScaleFactor = (float)iActualWidth / ScreenRefWidth;//新分辨率下需要缩放的倍数
            }
            else
            {
                ScaleFactor = (float)iActualHeight / ScreenRefHeigh;
            }
            ScreenZoom(ScaleFactor);
        }
        
        //急停控制
        private void EmergencyStopBox_Click(object sender, EventArgs e)
        {
            Dogtimer();
            if (EmergencyStopBox.BackColor == Color.Transparent)//急停动作
            {
                EmergencyStopMode(0x0e);
                SetEmergencyflag();//写入急停                
                EmergencyStopMove();                
                EmergencyStopBox.BackColor = Color.Red;
                Stop.Visible = true;
                Emergency_Stop.Visible = true;
                Back.Visible = true;                   
            }
            else if (EmergencyStopBox.BackColor == Color.Red)//急停后的复位
            {
                ResetEmergencyflag();//清楚急停标志位
                EmergencyStopMode(0xff);
                EmergencyStopMove();                
                EmergencyStopBox.BackColor = Color.Transparent;
                Back.Visible = false;
                Stop.Visible = false;
                Emergency_Stop.Visible = false;
            }
            else return;
                
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Back.Visible = false;
            Stop.Visible = false;
            Emergency_Stop.Visible = false;
        }

        private void ScreenZoom(float factor)
        {
            this.Scale(factor);
        }

        private void CloseICON_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
