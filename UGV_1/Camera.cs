using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Controls;

namespace UGV_1
{
    public partial class Camera : Form
    {
        //委托事件
        public event Timedelegate Dogtimer;
        public event ModeCodeWrtdelegate ModeCodeWrt;
        public event MoveCodeWrtdelegate MoveCodeWrt;
        public event MoveCodeClrdelegate MoveCodeClr;
        //显示无相机标语
        NoCamra noCamra=new NoCamra();
        //进行图标缩放
        int ScreenRefWidth = 1366;
        int ScreenRefHeigh = 768;
        int iActualWidth;//屏幕宽度
        int iActualHeight;//屏幕高度
        public float ScaleFactor;
//        //相机设备
//        public FilterInfoCollection videoDevices;
        //初始化窗体
        public Camera()
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
//            iActualWidth = Screen.PrimaryScreen.Bounds.Width;
//            iActualHeight = Screen.PrimaryScreen.Bounds.Height;//获取当前屏幕大小
//            this.Size = new System.Drawing.Size(iActualWidth, iActualHeight);//改界面大小，以便图标大小自适应
//            //this.Opacity = 0.5;//设置整个窗体为透明色；若要只显示图标有颜色，背景无颜色，则背景设置为某种颜色，然后transparentkey同选该色即可
//            //摄像头界面
//            videPlayer.Size=new System.Drawing.Size(iActualWidth,iActualHeight);
            //摄像头界面
            axVLCPlugin.Size = new Size(iActualWidth, iActualHeight);
        }

        private void Video_Click(object sender, EventArgs e)
        {
            if (Video.BackColor == Color.Transparent)
            {
                Dogtimer();
                ModeCodeWrt(0x40);
                MoveCodeClr();
                Video.BackColor = Color.Chartreuse;
                axVLCPlugin.Visible = true;
                axVLCPlugin.SendToBack();
                this.SendToBack();
                this.BringToFront();
                axVLCPlugin.playlist.add("udp://@192.168.0.104:8888 --network-caching=0");
                axVLCPlugin.playlist.play();
//                try
//                {
//                    //枚举所有视频输入设备
//                    videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
//
//                    if (videoDevices.Count == 0)
//                        throw new ApplicationException();
//                }
//                catch (ApplicationException)
//                {
//                    //显示窗体No Camera
//                    noCamra.ShowDialog();
//                    //视频设备为0
//                    videoDevices = null;
//                }
//                if (videoDevices != null)
//                {
//                    //VideoDisplay();
//                }
            }
            else
            {
                ModeCodeWrt(0x41);
                MoveCodeClr();
                Video.BackColor = Color.Transparent;
                axVLCPlugin.Visible = false;
                this.SendToBack();
                this.BringToFront();
                axVLCPlugin.playlist.stop();
                //VideoClose();
            }
            
        }

//        private void VideoDisplay()
//        {
//            VideoCaptureDevice videoDevice=new VideoCaptureDevice();
//            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
//            videoSource.DesiredFrameSize=new Size(iActualHeight,iActualWidth);
//            videoSource.DesiredFrameRate = 1;
//            //调用控件显示
//            videPlayer.VideoSource = videoSource;
//            videPlayer.Start();
//            videPlayer.Visible = true;
//        }
//
//        public void VideoClose()
//        {
//            videPlayer.Visible = false;
//            videPlayer.SignalToStop();
//            videPlayer.WaitForStop();
//        }

        private void ScreenZoom(float factor)
        {
            this.Scale(factor);
        }
    }
}
