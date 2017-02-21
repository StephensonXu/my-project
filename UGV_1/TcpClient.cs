using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UGV_1
{
    class TcpClient
    {
        //标识位表示连接上，因为连接速度慢较启动会差个2-3秒
        public bool Transmit = false;

        //写日志
        log myLog = new log("D://log", "/Transmit.log");

        //线程
        private Thread _mReceiveThread;
        private Thread _mConnectThread;

        //缓冲数据
        public byte[] buffer = new byte[1024];

        //IP
        private string _ip;
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }

        //端口号
        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        //网络端点
        private IPEndPoint _ipEndPoint;

        //客户端socket
        private Socket mClientSocket;

        //是否连接到服务器
        private bool isConnected = false;
        public bool IsConnected
        {
            get { return isConnected; }
        }

        //客户端构造函数
        public TcpClient(string ip, int port)
        {
            this._ip = ip;
            this._port = port;
            //初始化网络端点
            this._ipEndPoint=new IPEndPoint(IPAddress.Parse(_ip),_port );
            //初始化客户端socket
            mClientSocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            //清空日志
            myLog.ClearLog();
        }

        //客户端启动
        public void Start()
        {
            //创建线程
            _mConnectThread=new Thread(this.ConnectToServer);//var可代替任意类型，类似object，比object效率高点，同强类型效率一致。用var其必须初始化，初始化之后不可再赋值，是局部变量
            //启动线程
            _mConnectThread.Start();

        }

//        //定义个close方法---此方法舍弃，后面暴力关闭即可
//        public void Close()
//        {
//            //关闭线程
//            try
//            {
//                _mReceiveThread.Abort();
//                myLog.WriteLog("关闭_mReceiveThread", "");
//            }
//            catch (Exception e)
//            {
//                myLog.WriteLog("关闭线程问题",e.ToString());
//            }
//            try
//            {
//                _mConnectThread.Abort();
//                myLog.WriteLog("关闭_mConnectThread", "");
//            }
//            catch (Exception e)
//            {
//                myLog.WriteLog("关闭线程问题", e.ToString());
//            }
//            try
//            {
//                _restartThread.Abort();
//                myLog.WriteLog("关闭_restartThread", "");
//            }
//            catch (Exception e)
//            {  
//                myLog.WriteLog("关闭线程问题",e.ToString());
//            }
//            //关闭套接字
//            try
//            {
//                mClientSocket.Close();
//            }
//            catch (Exception e)
//            {
//                myLog.WriteLog("关闭套接字问题", e.ToString());
//            }
//            
//        }

        //连接到服务器
        private void ConnectToServer()
        {
            //当没有连接到服务器时就开始连接
            while (!isConnected)
            {
                try
                {
                    //开始连接
                    mClientSocket.Connect(this._ipEndPoint);
                    this.isConnected = true;
                }
                catch (Exception e)
                {
                    //写日志
                    myLog.WriteLog("连接失败",e.ToString());
                    //Change("连接失败");
                    this.isConnected = false;
                }
                Thread.Sleep(1000);//休息1S再连接
                //输出提示信息
                myLog.WriteLog("尝试连接","");
            }
            //输出连接成功信息
            Transmit = true;
            myLog.WriteLog("连接成功","");
            //创建一个线程以监听数据接收
            _mReceiveThread = new Thread(this.ReceiveMsg);
            //开启线程
            _mReceiveThread.Start();
        }

        //接收服务器数据
        private void ReceiveMsg()
        {
            //设置循环标识位
            bool flag = true;
            while (flag)
            {
                try
                {
                    this.mClientSocket.Receive(buffer);                    
                }
                catch (Exception e)
                {
                    //停止数据接收
                    flag = false;
                    //写日志
                    myLog.WriteLog("接收数据失败",e.ToString());
                    //尝试重新连接
                    this.isConnected = false;
                    ConnectToServer();
                }
            }
        }

        //发送消息
        public void SendMessage(string msg)
        {
            if (msg == string.Empty || this.mClientSocket == null) return;
            try
            {
                mClientSocket.Send(Encoding.UTF8.GetBytes(msg));                
            }
            catch(Exception e)
            {
               myLog.WriteLog("发送string数据失败",e.ToString());
            }
            
        }
        public void SendMessage(byte[] msg)
        {
            if (msg == null || this.mClientSocket == null) return;
            try
            {
                mClientSocket.Send(msg);
            }
            catch (Exception e)
            {
                myLog.WriteLog("发送byte数据失败",e.ToString());
            }
            
        }
    }
}
