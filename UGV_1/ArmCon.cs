using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//假定ArmReset必须先按下ArmReset，再按臂号，否则回待机状态
namespace UGV_1
{
    public partial class ArmCon : Form
    {
        //委托事件
        public event Timedelegate Dogtimer;
        public event SetMoveflagdelegate SetMoveflag;
        public event ResetMoveflagdelegate ResetMoveflag;
        public event GetMoveflagdelegate GetMoveflag;
        public event GetDemoCarColordelegate GetDemoCarColor;
        public event GetDemoArmColordelegate GetDemoArmColor;
        public event GetArmResetColordelegate GetArmResetColor;
        public event GetCarConColordelegate GetCarConColor;
        public event ChangeCarConColordelegate ChangeCarConColor;
        public event ControlCodeClrdelegate ControlCodeClr;
        public event MoveCodeClrdelegate MoveCodeClr;
        public event ModeCodeWrtdelegate ModeCodeWrt;
        public event MoveCodeWrtdelegate MoveCodeWrt;
        //定义标识位，臂控为0时表示臂为臂停止，为9时表示待控，为8时表示大臂联动/小臂联动，为1-7表示臂1-7臂单轴动
        private int Armcontrolflag = 0;
        //移动时圆坐标
        private int xPos, yPos;
        private int xHomePos, yHomePos;
        private int xFPos, yFPos;
        private int CircleRad = 140;
        //计算出的角度及速度百分比
        private int angle;
        private int velocity;
        //发送数据
        private byte tempMode = 0xff;
        private byte[] tempMove = { 0xff, 0xff, 0xff };
        private byte tempHeart = 0xff;
        private byte tempCRC = 0xff;
        //定义屏幕变化后分辨率变化
        int ScreenRefWidth = 1366;
        int ScreenRefHeigh = 768;
        private int iActualWidth;
        private int iActualHeight;
        public float ScaleFactor;
        //窗体
        ArmReset RST=new ArmReset();
        DemoArm demoArm = new DemoArm();

        public ArmCon()
        {
            InitializeComponent();

            xHomePos = control.Location.X;//控制球边角坐标
            yHomePos = control.Location.Y;
            xFPos = xHomePos + control.Size.Width / 2;//控制球圆心坐标
            yFPos = yHomePos + control.Size.Height / 2;

            //屏幕缩放变化
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


        }

        //获取速度及角度
        private void GetFactor(int tempX, int tempY, float factor)
        {
            float tan;
            double tempR = (double)(tempX * tempX + tempY * tempY);
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
            if (angle < 0) angle = 0;
            if (angle > 90) angle = 90;

        }

        //写入参数
        void WriteFactor(int tempX, int tempY, float factor)
        {
            if (tempX > 0 && tempY >= 0)//第4象限
            {
                GetFactor(Math.Abs(tempX), Math.Abs(tempY), factor);
                if (tempY == 0 && Armcontrolflag != 0)
                {                    
                    tempMove[0] = 4;
                    tempMove[1] = (byte)velocity;
                    tempMove[2] = 0xff;
                }
                else
                {
                    tempMove[0] = 4;
                    tempMove[1] = (byte)velocity;
                    tempMove[2] = (byte)angle;
                }
                MoveCodeWrt(tempMove);
                return;
            }
            if (tempX <= 0 && tempY > 0)//第3象限
            {
                GetFactor(Math.Abs(tempX), Math.Abs(tempY), factor);
                if (tempX == 0 && Armcontrolflag != 0)
                {
                    tempMove[0] = 3;
                    tempMove[1] = (byte)velocity;
                    tempMove[2] = 0xff;
                }
                else
                {
                    tempMove[0] = 3;
                    tempMove[1] = (byte)velocity;
                    tempMove[2] = (byte)angle;
                }
                MoveCodeWrt(tempMove);
                return;
            }
            if (tempX < 0 && tempY <= 0)//第2象限
            {
                GetFactor(Math.Abs(tempX), Math.Abs(tempY), factor);
                if (tempY == 0 && Armcontrolflag != 0)
                {
                    tempMove[0] = 2;
                    tempMove[1] = (byte)velocity;
                    tempMove[2] = 0xff;
                }
                else
                {
                    tempMove[0] = 2;
                    tempMove[1] = (byte)velocity;
                    tempMove[2] = (byte)angle;
                }
                MoveCodeWrt(tempMove);
                return;
            }
            if (tempX >= 0 && tempY < 0)//第1象限
            {
                GetFactor(Math.Abs(tempX), Math.Abs(tempY), factor);
                if (tempX == 0 && Armcontrolflag != 0)
                {
                    tempMove[0] = 1;
                    tempMove[1] = (byte)velocity;
                    tempMove[2] = 0xff;
                }
                else
                {
                    tempMove[0] = 2;
                    tempMove[1] = (byte)velocity;
                    tempMove[2] = (byte)angle;
                }
                MoveCodeWrt(tempMove);
                return;
            }
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)//鼠标放下
        {
            SetMoveflag();
            xPos = e.X;
            yPos = e.Y;
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)//鼠标松开
        {
            ResetMoveflag();
            control.Location = new Point(xHomePos, yHomePos);
            xFPos = xHomePos + control.Size.Width / 2;
            yFPos = yHomePos + control.Size.Height / 2;

            //清除速度和角度残余值
            velocity = 0;
            angle = 0;

            //传输写0
            byte[] tempMove0 = { 0x00, 0x00, 0x00 };
            MoveCodeWrt(tempMove0);
        }

        private void Circle_MouseMove(object sender, MouseEventArgs e)
        {
            int Temp;
            if (GetMoveflag())
            {
                switch (Armcontrolflag)
                {                                             
                    case 1:
                        if (Math.Abs(xFPos - xHomePos - control.Size.Width / 2) < CircleRad * ScaleFactor)
                        {
                            xFPos += e.X - xPos;
                            control.Left += Convert.ToInt16(e.X - xPos);
                            //获得速度和角度值
                            WriteFactor((xFPos - xHomePos - control.Size.Width / 2), 0, ScaleFactor);
                        }
                        break;
                    case 2:
                        if (Math.Abs(xFPos - xHomePos - control.Size.Width / 2) < CircleRad * ScaleFactor)
                        {
                            xFPos += e.X - xPos;
                            control.Left += Convert.ToInt16(e.X - xPos);
                            //获得速度和角度值
                            WriteFactor((xFPos - xHomePos - control.Size.Width / 2), 0, ScaleFactor);
                        }
                        break;
                    case 3:
                        if (Math.Abs(yFPos - yHomePos - control.Size.Height / 2) < CircleRad * ScaleFactor)
                        {
                            yFPos += e.Y - yPos;
                            control.Top += Convert.ToInt16(e.Y - yPos);
                            //获得速度和角度值
                            WriteFactor(0, (yFPos - yHomePos - control.Size.Height / 2), ScaleFactor);
                        }
                        break;
                    case 4:
                        if (Math.Abs(yFPos - yHomePos - control.Size.Height / 2) < CircleRad * ScaleFactor)
                        {
                            yFPos += e.Y - yPos;
                            control.Top += Convert.ToInt16(e.Y - yPos);
                            //获得速度和角度值
                            WriteFactor(0, (yFPos - yHomePos - control.Size.Height / 2), ScaleFactor);
                        }
                        break;
                    case 5:
                        if (Math.Abs(yFPos - yHomePos - control.Size.Height / 2) < CircleRad * ScaleFactor)
                        {
                            yFPos += e.Y - yPos;
                            control.Top += Convert.ToInt16(e.Y - yPos);
                            //获得速度和角度值
                            WriteFactor(0, (yFPos - yHomePos - control.Size.Height / 2), ScaleFactor);
                        }
                        break;
                    case 6:
                        if (Math.Abs(yFPos - yHomePos - control.Size.Height / 2) < CircleRad * ScaleFactor)
                        {
                            yFPos += e.Y - yPos;
                            control.Top += Convert.ToInt16(e.Y - yPos);
                            //获得速度和角度值
                            WriteFactor(0, (yFPos - yHomePos - control.Size.Height / 2), ScaleFactor);
                        }
                        break;
                    case 7:
                        if (Math.Abs(xFPos - xHomePos - control.Size.Width / 2) < CircleRad * ScaleFactor)
                        {
                            xFPos += e.X - xPos;
                            control.Left += Convert.ToInt16(e.X - xPos);
                            //获得速度和角度值
                            WriteFactor((xFPos - xHomePos - control.Size.Width / 2), 0, ScaleFactor);
                        }
                        break;
                    case 8://联动模式
                        Temp = (xFPos - xHomePos - control.Size.Width / 2) * (xFPos - xHomePos - control.Size.Width / 2) + (yFPos - yHomePos - control.Size.Height / 2) * (yFPos - yHomePos - control.Size.Height / 2);
                        if (Temp < CircleRad * CircleRad * ScaleFactor * ScaleFactor)
                        {
                            xFPos += e.X - xPos;
                            yFPos += e.Y - yPos;
                            control.Left += Convert.ToInt16(e.X - xPos);
                            control.Top += Convert.ToInt16(e.Y - yPos);
                            //获得速度和角度值
                            WriteFactor((xFPos - xHomePos - control.Size.Width / 2), (yFPos - yHomePos - control.Size.Height / 2), ScaleFactor);
                        }
                        break;        

                }
            }
        }


        private void ArmICON_Click(object sender, EventArgs e)
        {
            Dogtimer();
            ChangeCarConColor();
            if (GetDemoCarColor() == Color.Transparent && GetDemoArmColor() == Color.Transparent)
            {
                if (ArmICON.BackColor == Color.Transparent)
                {
                    if (ArmResetBox.BackColor == Color.Transparent)
                    {
                        //写入模式位
                        ModeCodeWrt(0x10);
                        MoveCodeClr();
                        //显示转盘界面，隐藏臂界面
                        ArmICON.BackColor = Color.Chartreuse;
                        //显示臂选项                    
                        ArmNumShow();
                        //臂标识位写9表示待控
                        Armcontrolflag = 9;
                    }
                    else
                    {
                        ArmICON.BackColor = Color.Chartreuse;
                        ModeCodeWrt(0x20);
                        MoveCodeClr();
                    }
                }
                else
                {
                    //清空数据
                    ControlCodeClr();
                    MoveCodeClr();
                    //图标颜色恢复
                    ArmICON.BackColor = Color.Transparent;                    
                    //臂初始化，并隐藏臂选项
                    ArmReset();
                    ArmNumDisapper();
                }
            }
        }

        private void Arm1_Click(object sender, EventArgs e)
        {
            if (Arm1.BackColor == Color.Transparent)
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0x21);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x11);
                }
                Arm1.BackColor = Color.Chartreuse;
                Arm2.BackColor = Color.Transparent;
                Arm3.BackColor = Color.Transparent;
                Arm4.BackColor = Color.Transparent;
                Arm5.BackColor = Color.Transparent;
                Arm6.BackColor = Color.Transparent;
                Arm7.BackColor = Color.Transparent;
                Linkage1.BackColor = Color.Transparent;
                Linkage2.BackColor = Color.Transparent;
                Armcontrolflag = 1;
            }
            else
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0xff);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x10);
                }
                ArmReset();
            }
        }

        private void Arm2_Click(object sender, EventArgs e)
        {
            if (Arm2.BackColor == Color.Transparent)
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0x22);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x12);
                }
                Arm1.BackColor = Color.Transparent;
                Arm2.BackColor = Color.Chartreuse;
                Arm3.BackColor = Color.Transparent;
                Arm4.BackColor = Color.Transparent;
                Arm5.BackColor = Color.Transparent;
                Arm6.BackColor = Color.Transparent;
                Arm7.BackColor = Color.Transparent;
                Linkage1.BackColor = Color.Transparent;
                Linkage2.BackColor = Color.Transparent;
                Armcontrolflag = 2;
            }
            else
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0xff);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x10);
                }
                ArmReset();
            }
        }

        private void Arm3_Click(object sender, EventArgs e)
        {
            if (Arm3.BackColor == Color.Transparent)
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0x23);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x13);
                }
                Arm1.BackColor = Color.Transparent;
                Arm2.BackColor = Color.Transparent;
                Arm3.BackColor = Color.Chartreuse;
                Arm4.BackColor = Color.Transparent;
                Arm5.BackColor = Color.Transparent;
                Arm6.BackColor = Color.Transparent;
                Arm7.BackColor = Color.Transparent;
                Linkage1.BackColor = Color.Transparent;
                Linkage2.BackColor = Color.Transparent;
                Armcontrolflag = 3;
            }
            else
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0xff);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x10);
                }
                ArmReset();
            }
        }

        private void Arm4_Click(object sender, EventArgs e)
        {
            if (Arm4.BackColor == Color.Transparent)
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0x24);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x14);
                }
                Arm1.BackColor = Color.Transparent;
                Arm2.BackColor = Color.Transparent;
                Arm3.BackColor = Color.Transparent;
                Arm4.BackColor = Color.Chartreuse;
                Arm5.BackColor = Color.Transparent;
                Arm6.BackColor = Color.Transparent;
                Arm7.BackColor = Color.Transparent;
                Linkage1.BackColor = Color.Transparent;
                Linkage2.BackColor = Color.Transparent;
                Armcontrolflag = 4;
            }
            else
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0xff);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x10);
                }
                ArmReset();
            }
        }

        private void Arm5_Click(object sender, EventArgs e)
        {
            if (Arm5.BackColor == Color.Transparent)
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0x25);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x15);
                }
                Arm1.BackColor = Color.Transparent;
                Arm2.BackColor = Color.Transparent;
                Arm3.BackColor = Color.Transparent;
                Arm4.BackColor = Color.Transparent;
                Arm5.BackColor = Color.Chartreuse;
                Arm6.BackColor = Color.Transparent;
                Arm7.BackColor = Color.Transparent;
                Linkage1.BackColor = Color.Transparent;
                Linkage2.BackColor = Color.Transparent;
                Armcontrolflag = 5;
            }
            else
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0xff);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x10);
                }
                ArmReset();
            }
        }

        private void Arm6_Click(object sender, EventArgs e)
        {
            if (Arm6.BackColor == Color.Transparent)
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0x26);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x16);
                }
                Arm1.BackColor = Color.Transparent;
                Arm2.BackColor = Color.Transparent;
                Arm3.BackColor = Color.Transparent;
                Arm4.BackColor = Color.Transparent;
                Arm5.BackColor = Color.Transparent;
                Arm6.BackColor = Color.Chartreuse;
                Arm7.BackColor = Color.Transparent;
                Linkage1.BackColor = Color.Transparent;
                Linkage2.BackColor = Color.Transparent;
                Armcontrolflag = 6;
            }
            else
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0xff);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x10);
                }
                ArmReset();
            }
        }

        private void Arm7_Click(object sender, EventArgs e)
        {
            if (Arm7.BackColor == Color.Transparent)
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0x27);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x17);
                }
                Arm1.BackColor = Color.Transparent;
                Arm2.BackColor = Color.Transparent;
                Arm3.BackColor = Color.Transparent;
                Arm4.BackColor = Color.Transparent;
                Arm5.BackColor = Color.Transparent;
                Arm6.BackColor = Color.Transparent;
                Arm7.BackColor = Color.Chartreuse;
                Linkage1.BackColor = Color.Transparent;
                Linkage2.BackColor = Color.Transparent;
                Armcontrolflag = 7;
            }
            else
            {
                if (GetArmResetColor() == Color.Chartreuse)
                {
                    //写入模式位
                    ModeCodeWrt(0xff);
                }
                else
                {
                    //写入模式位
                    ModeCodeWrt(0x10);
                }
                ArmReset();
            }
        }

        private void Linkage1_Click(object sender, EventArgs e)
        {
            if (Linkage1.BackColor == Color.Transparent)
            {
                //写入模式位
                ModeCodeWrt(0x18);
                ArmReset();
                Armcontrolflag = 8;
                Linkage1.BackColor = Color.Chartreuse;
            }
            else
            {
                Linkage1.BackColor = Color.Transparent;
                //写入模式位
                ModeCodeWrt(0x10);
                ArmReset();
            }
        }

        private void Linkage2_Click(object sender, EventArgs e)
        {
            if (Linkage2.BackColor == Color.Transparent)
            {
                //写入模式位
                ModeCodeWrt(0x19);
                ArmReset();
                Armcontrolflag = 8;
                Linkage2.BackColor = Color.Chartreuse;
            }
            else
            {
                Linkage2.BackColor = Color.Transparent;
                //写入模式位
                ModeCodeWrt(0x10);
                ArmReset();
            }
        }

        public void ArmReset()
        {
            Arm1.BackColor = Color.Transparent;
            Arm2.BackColor = Color.Transparent;
            Arm3.BackColor = Color.Transparent;
            Arm4.BackColor = Color.Transparent;
            Arm5.BackColor = Color.Transparent;
            Arm6.BackColor = Color.Transparent;
            Arm7.BackColor = Color.Transparent;
            Linkage1.BackColor = Color.Transparent;
            Linkage2.BackColor = Color.Transparent;
            Armcontrolflag = 9;
        }

        private void ArmNumShow()
        {
            Arm1.Visible = true;
            Arm2.Visible = true;
            Arm3.Visible = true;
            Arm4.Visible = true;
            Arm5.Visible = true;
            Arm6.Visible = true;
            Arm7.Visible = true;
            Linkage1.Visible = true;
            Linkage2.Visible = true;
            control.Visible = true;
            BackGround.Visible = true;
        }

        public void ArmNumDisapper()
        {
            Arm1.Visible = false;
            Arm2.Visible = false;
            Arm3.Visible = false;
            Arm4.Visible = false;
            Arm5.Visible = false;
            Arm6.Visible = false;
            Arm7.Visible = false;
            Linkage1.Visible = false;
            Linkage2.Visible = false;
            control.Visible = false;
            BackGround.Visible = false;
        }
        public void ResetArmIcon()
        {
            ArmICON.BackColor = Color.Transparent;
        }
        public Color GetArmControlBoxColor()
        {
            return ArmICON.BackColor;
        }
        public Color GetArmResetBoxColor()
        {
            return ArmResetBox.BackColor;
        }

        public Color GetDemoArmBoxColor()
        {
            return DemoArm.BackColor;
        }

        private void ArmResetBox_Click(object sender, EventArgs e)
        {
            Dogtimer();
            Arm3.BackColor = Color.Transparent;
            Arm4.BackColor = Color.Transparent;
            Arm5.BackColor = Color.Transparent;
            Arm6.BackColor = Color.Transparent;
            if (GetDemoArmColor() == Color.Transparent && GetDemoCarColor()== Color.Transparent  && GetArmResetColor() == Color.Transparent && GetCarConColor() == Color.Transparent && ArmICON.BackColor == Color.Transparent)
            {
                ArmResetBox.BackColor = Color.Chartreuse;
                //写入模式位
                ModeCodeWrt(0xff);
                Arm3.Visible = true;
                Arm4.Visible = true;
                Arm5.Visible = true;
                Arm6.Visible = true;
                RST.Show();                
                return;
            }
            if (GetArmResetColor() == Color.Chartreuse && GetDemoCarColor() == Color.Transparent && DemoArm.BackColor == Color.Transparent)
            {
                ArmResetBox.BackColor = Color.Transparent;
                ArmICON.BackColor = Color.Transparent;
                //写入模式位
                ModeCodeWrt(0xff);
                RST.Hide();
                Arm3.BackColor = Color.Transparent;
                Arm4.BackColor = Color.Transparent;
                Arm5.BackColor = Color.Transparent;
                Arm6.BackColor = Color.Transparent;

                ArmNumDisapper();
                return;
            }
        }

        private void DemoArm_Click(object sender, EventArgs e)
        {
            Dogtimer();
            if (GetDemoArmColor() == Color.Transparent && GetDemoCarColor() == Color.Transparent &&
                GetArmResetColor() == Color.Transparent && GetCarConColor() == Color.Transparent &&
                ArmICON.BackColor == Color.Transparent)
            {
                DemoArm.BackColor = Color.Chartreuse;
                ModeCodeWrt(0x1a);
                demoArm.Show();
                return;
            }
            if (GetDemoArmColor() == Color.Chartreuse && GetDemoCarColor() == Color.Transparent &&
                GetArmResetColor() == Color.Transparent && GetCarConColor() == Color.Transparent &&
                ArmICON.BackColor == Color.Transparent)
            {
                DemoArm.BackColor = Color.Transparent;
                ModeCodeWrt(0xff);
                demoArm.Hide();
                return;
            }
        }

        private void ScreenZoom(float factor)
        {
            this.Scale(factor);
        }
        

    }
}
