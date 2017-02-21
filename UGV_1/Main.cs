using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

/*****************************************************************************************************************************
 * C#学习之show与showdiag区别：
 * show方法转化为showdialog方法，顾名思义，showdialog是一个进行路经绑定的show方法，他是不可以自由切换的，换言之，就是，当你没有关闭
 * 你当前页的前提下，你是无法关闭该页面后面的任一页面的，它是当前唯一（路经打开）为true的。
 * 而show方法由于未进行绑定，它所显示的各个窗口、对话框是可以相互切换，而不需要关闭当前窗口、对话框。
 * 但是他由于未进行绑定，所以每个由show方法打开的窗口的顺序是非固定的，如果遇到什么问题，由show方法打开的窗口的顺序可能会有很大的改变！
 * ***************************************************************************************************************************/

namespace UGV_1
{
    //dogtimer
    public delegate void Timedelegate();
    //moveflag
    public delegate void SetMoveflagdelegate();
    public delegate void ResetMoveflagdelegate();
    public delegate bool GetMoveflagdelegate();
    //emergencyflag
    public delegate void SetEmergencyflagdelegate();
    public delegate void ResetEmergencyflagdelegate();
    //控件颜色
    public delegate Color GetDemoCarColordelegate();
    public delegate Color GetDemoArmColordelegate();
    public delegate Color GetArmResetColordelegate();
    public delegate Color GetCarConColordelegate();
    public delegate Color GetArmConColordelegate();
    public delegate void ChangeCarConColordelegate();
    public delegate void ChangeArmConColordelegate();
    //写数据
    public delegate void ControlCodeClrdelegate();
    public delegate void MoveCodeClrdelegate();
    public delegate void ModeCodeWrtdelegate(byte mode);
    public delegate void MoveCodeWrtdelegate(byte[] move);

    public partial class Main : Form
    {
        //定义标识位
        private bool moveflag=false;
        private bool EmergeStopFlag = false;
        //写日志
        log myLog = new log("D://log", "/event.log");
/******************************************************************************* 
 * //定义传输数据格式，第1位写模式，第2位写方向，第3位写速度，第4位写角度0-90度，第5位写心跳0或1，第6位写校验位
 *      //先用string来写，后用byte来写
        private string ModeWrtStr = "X";
        private string MoveWrtStr = "XXXXX";
        private string HeartWrtStr = "X";
* ****************************************************************************/
        public static byte[] _inputBytes = {0xff,0xff,0xff,0xff,0xff,0x06};
        private byte[] _readBytes = new byte[6];
       
        //心跳标识位及丢失次数
        private int flagDog = 0;
        private int ConLostCnt = 0;
        //显示其他窗体及初始化通信客户端类
        Camera camera=new Camera();
        CarCon carControl=new CarCon();
        ArmCon armControl=new ArmCon();
        EmergencyStop emergencyStop=new EmergencyStop();
        LostConnect lost = new LostConnect();
        //TcpClient tcpClient = new TcpClient("192.168.0.108", 9966);
        TcpClient tcpClient = new TcpClient("192.168.2.10", 9966);
        //TcpClient tcpClient=new TcpClient("192.168.1.160",9966);
        //进行图标缩放
        int ScreenRefWidth = 1366;
        int ScreenRefHeigh = 768;
        int iActualWidth;//屏幕宽度
        int iActualHeight;//屏幕高度
        public float ScaleFactor;

        public Main()
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
                             
            camera.Show();            
            carControl.Show();
            armControl.Show();
            emergencyStop.Show();
            camera.TopMost = true;
            //事件注册
            carControl.Dogtimer+=new Timedelegate(StartTimer);
            carControl.SetMoveflag+=new SetMoveflagdelegate(SetMoveflag);
            carControl.ResetMoveflag+=new ResetMoveflagdelegate(ResetMoveflag);;
            carControl.GetMoveflag+=new GetMoveflagdelegate(GetMoveflag);
            carControl.GetDemoCarColor+=new GetDemoCarColordelegate(GetDemoCarColor);
            carControl.GetArmResetColor+=new GetArmResetColordelegate(GetArmResetColor);
            carControl.GetDemoArmColor+=new GetDemoArmColordelegate(GetDemoArmColor);
            carControl.GetArmConColor+=new GetArmConColordelegate(GetArmControlColor);
            carControl.ChangeArmConColor+=new ChangeArmConColordelegate(ResetArmCon);
            carControl.ControlCodeClr+=new ControlCodeClrdelegate(ControlCodeClr);
            carControl.MoveCodeClr+=new MoveCodeClrdelegate(MoveCodeClr);
            carControl.ModeCodeWrt+=new ModeCodeWrtdelegate(ModeCodeWrt);
            carControl.MoveCodeWrt+=new MoveCodeWrtdelegate(MoveCodeWrt);
            emergencyStop.Dogtimer+=new Timedelegate(StartTimer);
            emergencyStop.ResetEmergencyflag+=new ResetEmergencyflagdelegate(ResetEmegencyStopflag);
            emergencyStop.SetEmergencyflag+=new SetEmergencyflagdelegate(SetEmegencyStopflag);
            emergencyStop.EmergencyStopMode += new ModeCodeWrtdelegate(ModeCodeWrt);
            emergencyStop.EmergencyStopMove +=new MoveCodeClrdelegate(MoveCodeClr);
            armControl.Dogtimer+=new Timedelegate(StartTimer);
            armControl.SetMoveflag += new SetMoveflagdelegate(SetMoveflag);
            armControl.ResetMoveflag += new ResetMoveflagdelegate(ResetMoveflag); ;
            armControl.GetMoveflag += new GetMoveflagdelegate(GetMoveflag);
            armControl.GetDemoCarColor += new GetDemoCarColordelegate(GetDemoCarColor);
            armControl.GetArmResetColor += new GetArmResetColordelegate(GetArmResetColor);
            armControl.GetDemoArmColor += new GetDemoArmColordelegate(GetDemoArmColor);
            armControl.GetCarConColor+=new GetCarConColordelegate(GetCarControlColor);
            armControl.ChangeCarConColor+=new ChangeCarConColordelegate(ResetCarCon);
            armControl.ControlCodeClr += new ControlCodeClrdelegate(ControlCodeClr);
            armControl.MoveCodeClr += new MoveCodeClrdelegate(MoveCodeClr);
            armControl.ModeCodeWrt += new ModeCodeWrtdelegate(ModeCodeWrt);
            armControl.MoveCodeWrt += new MoveCodeWrtdelegate(MoveCodeWrt);
            camera.Dogtimer += new Timedelegate(StartTimer);
            camera.MoveCodeClr += new MoveCodeClrdelegate(MoveCodeClr);
            camera.ModeCodeWrt += new ModeCodeWrtdelegate(ModeCodeWrt);
            camera.MoveCodeWrt += new MoveCodeWrtdelegate(MoveCodeWrt);
            //开启通信
            tcpClient.Start();
            //不清空并写日志
            myLog.WriteLog("初始化启动成功","");
        }

/*******************************************************************************************************************
 * 图标按钮事件
 * ****************************************************************************************************************/

//        //关闭按钮
//        private void CloseICON_Click(object sender, EventArgs e)
//        {
//            //先写入结束数据，再关闭服务器
//            ResetInputBytes();
//            if (tcpClient.IsConnected)
//            {
//                tcpClient.SendMessage(_inputBytes);
//            }                        
//            //关闭窗体
//            this.Close();
//            //如果lost窗体未关闭，则关闭lost窗体
//            if (lost.IsDisposed==false)
//            {
//                lost.Close();
//            }
//            //如果相机未关闭，关闭相机
//            //camera.VideoClose();
//            //写关闭
//            myLog.WriteLog("关闭系统", "");
//            //暴力全关闭
//            Environment.Exit(0);
//            
//        }

/*******************************************************************************************************************
 * 看门狗使能及通信
 * ****************************************************************************************************************/

        //看门狗,100ms执行一次,进行通信
        private void StartTimer()
        {
            if (Dog_timer.Enabled == false)
            {
                Dog_timer.Enabled = true;
                Dog_timer.Start();
                myLog.WriteLog("start timer", "success");
            }            
        }
        private void Dog_timer_tick(object sender, EventArgs e)
        {
            if (tcpClient.Transmit)
            {
                if (flagDog == 0) //仅执行一次
                {
                    flagDog = 1;
                    _inputBytes[4] = 0;
                    tcpClient.SendMessage(_inputBytes);
                    myLog.WriteLog("第一次发送", _inputBytes[0] + " " + _inputBytes[1] + " " + _inputBytes[2] + " " + _inputBytes[3] + " " + _inputBytes[4] + " " + _inputBytes[5]);
                }
                else
                {
                    //读取下位机传输数据
                    _readBytes = tcpClient.buffer;
                    //如果心跳位未改写为1
                    if (_readBytes[4] == 0)
                    {
                        ConLostCnt++;
                        myLog.WriteLog("丢失数", ConLostCnt.ToString());
                        if (ConLostCnt >= 20)
                        {
                            myLog.WriteLog("丢失数大于20，通信中断", ConLostCnt.ToString());
                            Dog_timer.Enabled = false; //关闭关门狗,并显示尝试重新连接标语
                            lost.ShowDialog();
                        }
                    }
                    else
                    {
                        ConLostCnt = 0;//丢失数清零
                    }
                    _inputBytes[4] = 0;
                    tcpClient.SendMessage(_inputBytes);
                    myLog.WriteLog("发送数据", _inputBytes[0] + " " + _inputBytes[1] + " " + _inputBytes[2] + " " + _inputBytes[3] + " " + _inputBytes[4] + " " + _inputBytes[5]);
                }
            }            
        }

/*******************************************************************************************************************
 * 委托函数
 * ****************************************************************************************************************/

        //标识位Moveflag
        private void SetMoveflag()
        {
            if (moveflag == false)
            {
                moveflag = true;
            }
        }
        private void ResetMoveflag()
        {
            if (moveflag)
            {
                moveflag = false;
            }
        }
        private bool GetMoveflag()
        {
            return moveflag;
        }
        //标识位emergencystopflag
        private void SetEmegencyStopflag()
        {
            if (EmergeStopFlag == false)
            {
                EmergeStopFlag = true;
            }
        }
        private void ResetEmegencyStopflag()
        {
            if (EmergeStopFlag)
            {
                EmergeStopFlag = false;
            }
        }
        //传递数据
/********************************************************************************************************************
 * string 格式
 * ******************************************************************************************************************
        private void ControlCodeClr() //清空模式位数据
        {
            ModeWrtStr = "X";
        }
        private void MoveCodeClr()//清空移动位数据
        {
            MoveWrtStr = "XXXXX";
        }

        private void HeartCodeClr()//清空心跳置初始态
        {
            HeartWrtStr = "X";
        }
        private void ModeCodeWrt(string mode) //写入控制部位模式
        {
            if (!EmergeStopFlag)
            {
                ModeWrtStr = mode;
            }
        }
        private void MoveCodeWrt(string move)//写入移动部位模式
        {
            if (!EmergeStopFlag)
            {
                MoveWrtStr = move;
            }
        } 
 * *******************************************************************************************************************
 * byte 格式
 * ******************************************************************************************************************/

        private void ResetInputBytes()
        {
            for (int i = 0; i < 5; i++)
            {
                _inputBytes[i] = 0xff;
            }
            _inputBytes[5] = 0x06;
        }
        private void ControlCodeClr()
        {
            _inputBytes[0] = 0xff;
        }

        private void MoveCodeClr()
        {
            for (int i = 1; i < 4; i++)
            {
                _inputBytes[i] = 0xff;
            }
        }
        private void HeartCodeClr()
        {
            _inputBytes[5] = 0xff;
        }
        private void ModeCodeWrt(byte mode)
        {
            if (!EmergeStopFlag)
            {
                _inputBytes[0] = mode;
            }            
        }
        private void MoveCodeWrt(byte[] move)
        {
            if (!EmergeStopFlag)
            {
                for (int i = 1; i < 4; i++)
                {
                    _inputBytes[i] = move[i - 1];
                }
            }            
        }


        //获取窗体颜色及设置窗体颜色
        private Color GetDemoArmColor()
        {
            return armControl.GetDemoArmBoxColor();
        }
        private Color GetArmResetColor()
        {
            return armControl.GetArmResetBoxColor();
        }
        private Color GetDemoCarColor()
        {
            return carControl.GetDemoCarBoxColor();
        }
        private Color GetCarControlColor()
        {
            return carControl.GetCarControlBoxColor();
        }

        private Color GetArmControlColor()
        {
            return armControl.GetArmControlBoxColor();
        }
        private void ResetArmCon()
        {
            armControl.ArmReset();
            armControl.ArmNumDisapper();
            armControl.ResetArmIcon();
        }
        private void ResetCarCon()
        {
            carControl.CarReset();
            carControl.CarConDisapper();
            carControl.ResetCarIcon();
        }


        private void ScreenZoom(float factor)
        {
            this.Scale(factor);
        }
    }
}
