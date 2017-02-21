using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace UGV_1
{    
    
    public partial class CarCon : Form
    {
        //委托事件
        public event Timedelegate Dogtimer;
        public event SetMoveflagdelegate SetMoveflag;
        public event ResetMoveflagdelegate ResetMoveflag;
        public event GetMoveflagdelegate GetMoveflag;
        public event GetDemoCarColordelegate GetDemoCarColor;
        public event GetDemoArmColordelegate GetDemoArmColor;
        public event GetArmResetColordelegate GetArmResetColor;
        public event GetArmConColordelegate GetArmConColor;
        public event ChangeArmConColordelegate ChangeArmConColor;
        public event ControlCodeClrdelegate ControlCodeClr;
        public event MoveCodeClrdelegate MoveCodeClr;
        public event ModeCodeWrtdelegate ModeCodeWrt;
        public event MoveCodeWrtdelegate MoveCodeWrt;
        //定义标识位，车控为0时表示车停，为4表示待控，为1表示车低速/高速动，为2表示车旋转
        private int Carcontrolflag = 0;
        //再设个改变车最大速度按钮及显示速度表盘

        //定义屏幕变化后分辨率变化
        int ScreenRefWidth = 1366;
        int ScreenRefHeigh = 768;
        private int iActualWidth;
        private int iActualHeight;
        public float ScaleFactor;
        //移动时圆坐标
        private int xPos,yPos;
        private int xHomePos, yHomePos;
        private int xFPos, yFPos;
        private int CircleRad = 140;
        //计算出的角度，及两轮差速
        private int angle;
        private int velocity;
        //发送数据
        private string TempMove = "XXXXX";
        private string TempCode = "X";
        private string TempHeart = "X";
        private byte tempMode = 0xff;
        private byte[] tempMove = {0xff, 0xff, 0xff};
        private byte tempHeart = 0xff;
        private byte tempCRC = 0x06;
        //窗体
        DemoCar demoCar = new DemoCar();

        //
        [DllImport("User32.dll ", EntryPoint = "SetParent")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll ", EntryPoint = "ShowWindow")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

            


        public CarCon()
        {
            InitializeComponent();

            xHomePos = Control.Location.X;//控制球边角坐标
            yHomePos = Control.Location.Y;
            xFPos = xHomePos + Control.Size.Width / 2;//控制球圆心坐标
            yFPos = yHomePos + Control.Size.Height / 2;

            //屏幕缩小变化
            iActualWidth = Screen.PrimaryScreen.Bounds.Width;
            iActualHeight = Screen.PrimaryScreen.Bounds.Height;
            if (((float)iActualWidth / ScreenRefWidth) - ((float)iActualHeight / ScreenRefHeigh) < 0)
            {
                ScaleFactor = (float)iActualWidth / ScreenRefWidth;//新分辨率下需要缩小的倍数
            }
            else
            {
                ScaleFactor = (float)iActualHeight / ScreenRefHeigh;
            }
            ScreenZoom(ScaleFactor);
            //启动unity.exe
            Process process;
            //this.appContainer1.AppFilename =
            //"C:\\Users\\Frank\\Desktop\\test.exe";
            //this.appContainer1.Start();
            process = new Process();
            process.StartInfo.FileName = Application.StartupPath + @"\test8.exe";
            process.Start();
            //
            Process p = new Process();
            p.StartInfo.FileName = Application.StartupPath + @"\test8.exe";
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized; p.Start();
            System.Threading.Thread.Sleep(100);
            SetParent(p.MainWindowHandle, this.Handle);
            ShowWindow(p.MainWindowHandle, 3);

        }

        //获取速度及角度
        private void GetFactor(int tempX,int tempY,float factor)
        {
            float tan;
            double tempR = (double) (tempX*tempX + tempY*tempY);
            int distance = (int)Math.Sqrt(tempR);
            //计算速度百分比
            velocity = (int)(distance * 100 / CircleRad / factor);
            if (velocity < 0) velocity = 0;
            if (velocity > 99) velocity = 99;
            //计算角度
            if (tempY == 0)
            {
                angle = 0;
            }
            else if (tempX == 0 && tempY != 0)
            {
                angle = 90;
            }
            else
            {
                tan = ((float)tempY) / ((float)tempX);
                angle = (int)(Math.Atan(tan) / Math.PI * 180);//弧度转换
            }
            //进行差速
            angle = 90 - angle;
            if (Carcontrolflag == 1)//车控时才分段
            {
                if (angle < 85)
                {
                    angle = 10 * (int)Math.Floor((double)(angle / 10.0));
                }
                else
                {
                    angle = 90;
                }
            }
            if (angle < 0) angle = 0;
            if (angle > 90) angle = 90;

        }

        //写入参数
        void WriteFactor(int tempX, int tempY, float factor)
        {
            if (tempX > 0 && tempY >= 0)//第4象限
            {
                GetFactor(Math.Abs(tempX), Math.Abs(tempY), factor);
                if (tempY == 0 && Carcontrolflag != 0)
                {
                    TempMove = "4" + velocity.ToString("00") + "XX";
                    Main._inputBytes[1] = 4;
                    Main._inputBytes[2] = (byte)velocity;
                    Main._inputBytes[3] = 0x5a;
                }
                else
                {
                    TempMove = "4" + velocity.ToString("00") + angle.ToString("00");
                    Main._inputBytes[1] = 4;
                    Main._inputBytes[2] = (byte)velocity;
                    Main._inputBytes[3] = (byte)angle;
                }
                return;
            }
            if (tempX <= 0 && tempY > 0)//第3象限
            {
                GetFactor(Math.Abs(tempX), Math.Abs(tempY), factor);
                if (tempX == 0 && Carcontrolflag != 0)
                {
                    TempMove = "3" + velocity.ToString("00") + "XX";
                    Main._inputBytes[1] = 3;
                    Main._inputBytes[2] = (byte)velocity;
                    Main._inputBytes[3] = 0x5a;
                }
                else
                {
                    TempMove = "3" + velocity.ToString("00") + angle.ToString("00");
                    Main._inputBytes[1] = 3;
                    Main._inputBytes[2] = (byte)velocity;
                    Main._inputBytes[3] = (byte)angle;
                }
                MoveCodeWrt(tempMove);
                return;
            }
            if (tempX < 0 && tempY <= 0)//第2象限
            {
                GetFactor(Math.Abs(tempX), Math.Abs(tempY), factor);
                if (tempY == 0 && Carcontrolflag != 0)
                {
                    TempMove = "2" + velocity.ToString("00") + "XX";
                    Main._inputBytes[1] = 2;
                    Main._inputBytes[2] = (byte)velocity;
                    Main._inputBytes[3] = 0x5a;
                }
                else
                {
                    TempMove = "2" + velocity.ToString("00") + angle.ToString("00");
                    Main._inputBytes[1] = 2;
                    Main._inputBytes[2] = (byte)velocity;
                    Main._inputBytes[3] = (byte)angle;
                }
                //MoveCodeWrt(tempMove);
                return;
            }
            if (tempX >= 0 && tempY < 0)//第1象限
            {
                GetFactor(Math.Abs(tempX), Math.Abs(tempY), factor);
                if (tempX == 0 && Carcontrolflag != 0)
                {
                    TempMove = "1" + velocity.ToString("00") + "XX";
                    Main._inputBytes[1] = 1;
                    Main._inputBytes[2] = (byte)velocity;
                    Main._inputBytes[3] = 0x5a;
                }
                else
                {
                    TempMove = "1" + velocity.ToString("00") + angle.ToString("00");
                    Main._inputBytes[1] = 1;
                    Main._inputBytes[2] = (byte)velocity;
                    Main._inputBytes[3] = (byte)angle;
                }
                //MoveCodeWrt(tempMove);
                return;
            }
        }



        private void Control_MouseDown(object sender, MouseEventArgs e)//鼠标放下
        {
            SetMoveflag();
            xPos = e.X;
            yPos = e.Y;
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)//鼠标移动
        {
            int Temp;
            if (GetMoveflag())
            {               
                switch (Carcontrolflag)
                {
                    case 1://普通模式
                        Temp = (xFPos - xHomePos - Control.Size.Width / 2) * (xFPos - xHomePos - Control.Size.Width / 2) + (yFPos - yHomePos - Control.Size.Height / 2) * (yFPos - yHomePos - Control.Size.Height / 2);
                        if (Temp < CircleRad * CircleRad * ScaleFactor * ScaleFactor)
                        {
                            xFPos += e.X - xPos;
                            yFPos += e.Y - yPos;
                            Control.Left += Convert.ToInt16(e.X - xPos);
                            Control.Top += Convert.ToInt16(e.Y - yPos);
                            //获得速度和角度值
                            WriteFactor((xFPos - xHomePos - Control.Size.Width / 2), (yFPos - yHomePos - Control.Size.Height / 2), ScaleFactor);
                        }
                        break;                                                                                                      
                    case 2://转弯模式
                        if (Math.Abs(xFPos - xHomePos - Control.Size.Width / 2) < CircleRad * ScaleFactor)
                        {
                            xFPos += e.X - xPos;
                            Control.Left += Convert.ToInt16(e.X - xPos);
                            //获得速度和角度值
                            WriteFactor((xFPos - xHomePos - Control.Size.Width / 2), 0, ScaleFactor);
                        }
                        break;                  
                }
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)//鼠标松开
        {
            ResetMoveflag();
            Control.Location = new Point(xHomePos, yHomePos);
            xFPos = xHomePos + Control.Size.Width / 2;
            yFPos = yHomePos + Control.Size.Height / 2;

            //清除速度和角度残余值
            velocity = 0;
            angle = 0;

            //传输写0
            byte[] tempMove0 = {0x00, 0x00, 0x00};
            MoveCodeWrt(tempMove0);
        }

        public Color GetDemoCarBoxColor()
        {
            return DemoCar.BackColor;
        }
        public Color GetCarControlBoxColor()
        {
            return CarICON.BackColor;
        }

        public void ResetCarIcon()
        {
            CarICON.BackColor = Color.Transparent;
        }
        public void CarReset()
        {
            Rotate.BackColor = Color.Transparent;
            HighSpeed.BackColor = Color.Transparent;
            LowSpeed.BackColor = Color.Transparent;
            Carcontrolflag = 0;
        }
        public void CarConDisapper()
        {
            BackGround.Visible = false;
            Control.Visible = false;
            Rotate.Visible = false;
            HighSpeed.Visible = false;
            LowSpeed.Visible = false;
        }



        private void CarICON_Click(object sender, EventArgs e)
        {
            Dogtimer();
            ChangeArmConColor();
            if (GetDemoCarColor() == Color.Transparent && GetDemoArmColor() == Color.Transparent &&
                GetArmResetColor() == Color.Transparent)
            {
                if (CarICON.BackColor == Color.Transparent)
                {
                    //写入模式位
                    ModeCodeWrt(0);
                    //显示转盘界面，隐藏臂界面
                    CarICON.BackColor = Color.Chartreuse;
                    //显示高速选项及原地转选项
                    HighSpeed.Visible = true;
                    LowSpeed.Visible = true;
                    Rotate.Visible = true;
                    Control.Visible = true;
                    BackGround.Visible = true;                                   
                    //车标识位写待控
                    Carcontrolflag = 4; 
                }
                else
                {
                    //清空数据
                    ControlCodeClr();
                    MoveCodeClr();
                    //图标颜色恢复
                    CarICON.BackColor = Color.Transparent;                    
                    BackGround.Visible = false;
                    Control.Visible = false;
                    //隐藏高速选项及原地转弯选项,并恢复成透明色
                    HighSpeed.BackColor = Color.Transparent;
                    HighSpeed.Visible = false;
                    LowSpeed.BackColor = Color.Transparent;
                    LowSpeed.Visible = false;
                    Rotate.BackColor = Color.Transparent;
                    Rotate.Visible = false;
                    //车标识位写0
                    Carcontrolflag = 0;
                }
           }
        }

        private void Rotate_Click(object sender, EventArgs e)
        {
            if (Rotate.BackColor == Color.Transparent)
            {
                if (HighSpeed.BackColor == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(4);
                }
                else//默认低速旋转，即使不按低速选项
                {
                    //写入模式位
                    ModeCodeWrt(3);
                }               
                Rotate.BackColor = Color.Chartreuse;
                Carcontrolflag = 2;
            }
            else
            {
                if (HighSpeed.BackColor == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(2);
                    Carcontrolflag = 1;
                }
                else if (LowSpeed.BackColor == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(1);
                    Carcontrolflag = 1;
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0);
                    MoveCodeClr();
                    Carcontrolflag = 4;
                }               
                Rotate.BackColor = Color.Transparent;
            }
        }
        private void HighSpeed_Click(object sender, EventArgs e)
        {
            if (HighSpeed.BackColor == Color.Transparent)
            {
                HighSpeed.BackColor = Color.Chartreuse;
                LowSpeed.BackColor = Color.Transparent;
                if (Rotate.BackColor == Color.Transparent)
                {
                    //写入模式位
                    ModeCodeWrt(2);
                    Carcontrolflag = 1;
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(4);
                    Carcontrolflag = 2;
                }
            }
            else
            {
                HighSpeed.BackColor = Color.Transparent;
                if (Rotate.BackColor == Color.Transparent)
                {
                    //写入模式位
                    ModeCodeWrt(0);
                    MoveCodeClr();
                    Carcontrolflag = 4;
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(3);
                    Carcontrolflag = 2;
                }
            }
        }

        private void LowSpeed_Click(object sender, EventArgs e)
        {
            if (LowSpeed.BackColor == Color.Transparent)
            {
                LowSpeed.BackColor = Color.Chartreuse;
                HighSpeed.BackColor = Color.Transparent;
                if (Rotate.BackColor == Color.Transparent)
                {
                    //写入模式位
                    ModeCodeWrt(1);
                    Carcontrolflag = 1;
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(3);
                    Carcontrolflag = 2;
                }

            }
            else
            {
                LowSpeed.BackColor = Color.Transparent;
                if (Rotate.BackColor == Color.Transparent)
                {
                    //写入模式位
                    ModeCodeWrt(0);
                    MoveCodeClr();
                    Carcontrolflag = 4;
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(3);
                    Carcontrolflag = 2;
                }
            }
        }

        private void DemoCar_Click(object sender, EventArgs e)
        {
            Dogtimer();            
            if (GetDemoArmColor() == Color.Transparent && GetDemoCarColor() == Color.Transparent &&
                GetArmResetColor() == Color.Transparent && GetCarControlBoxColor() == Color.Transparent &&
                GetArmConColor() == Color.Transparent)
            {
                DemoCar.BackColor = Color.Chartreuse;
                ModeCodeWrt(0x1a);                
                demoCar.Show();
                return;
            }
            if (GetDemoArmColor() == Color.Transparent && GetDemoCarColor() == Color.Chartreuse &&
                GetArmResetColor() == Color.Transparent && GetCarControlBoxColor() == Color.Transparent &&
                GetArmConColor() == Color.Transparent)
            {
                DemoCar.BackColor = Color.Transparent;
                ModeCodeWrt(0xff);
                demoCar.Hide();
                return;
            }
        }

        private void ScreenZoom(float factor)
        {
            this.Scale(factor);
        }

    }

}
