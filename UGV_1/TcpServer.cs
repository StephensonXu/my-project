using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

/*********************************************************************************************************************
 * socket-tcp通信
 * ******************************************************************************************************************/
namespace UGV_1
{
    class TcpServer
    {
        //线程
        private Thread _mServerThread;
        private Thread _mReceiveThread;

        //缓存数据
        private byte[] buffer = new byte[1024];

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

        //IP终端
        private IPEndPoint _ipEndPoint;

        //服务器端socket
        private Socket _mServerSocket;

        //监听数量
        private int _maxClientCount;
        public int MaxClientCount
        {
            get { return _maxClientCount; }
            set { _maxClientCount = value; }
        }

        //客户端列表
        private List<Socket> _mClientSockets;
        public List<Socket> ClientSockets
        {
            get { return _mClientSockets;}
        }

        //当前客户端socket
        private Socket _mClientSocket;
        public Socket ClienSocket
        {
            get { return _mClientSocket;}
            set { _mClientSocket = value; }
        }

        //TCPServer 构造函数
        public TcpServer(int port, int count)
        {
            this._ip = IPAddress.Any.ToString();
            this.Port = port;
            this._maxClientCount = count;
            this._mClientSockets = new List<Socket>();
            //初始化IP终端
            this._ipEndPoint = new IPEndPoint(IPAddress.Any, port);
            //初始化服务器端Socket
            this._mServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Socket绑定端口
            this._mServerSocket.Bind(this._ipEndPoint);
            //设置监听数目
            this._mServerSocket.Listen(_maxClientCount);
        }
        
        //TCPServer 构造函数
        public TcpServer(string ip, int port, int count)
        {
            this._ip = ip;
            this.Port = port;
            this._maxClientCount = count;
            this._mClientSockets = new List<Socket>();
            //初始化IP终端
            this._ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            //初始化服务器端Socket
            this._mServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Socket绑定端口
            this._mServerSocket.Bind(this._ipEndPoint);
            //设置监听数目
            this._mServerSocket.Listen(_maxClientCount);
            
        }
        
        //定义个start方法
        public void Start()
        {
            //创建服务器线程，实现客户端连接请求的循环监听
            _mServerThread = new Thread(this.ListenClientConnect);
            //服务器端线程开启
            _mServerThread.Start();
        }

        //定义个close方法
        public void Close()
        {
            //关闭线程
            if (_mReceiveThread != null)
            {
                _mReceiveThread.Abort();
            }            
            _mServerThread.Abort();            
            //关闭套接字
            _mServerSocket.Close();
        }


        //监听客户端连接
        private void ListenClientConnect()
        {
            bool flag = true;
            while (flag)
            {
                //获取连接到服务器端的客户端
                this._mClientSocket = this._mServerSocket.Accept();
                //将获取到的客户端添加到客户端列表
                this._mClientSockets.Add(this._mClientSocket);
                //向客户端发消息提示连接成功
                this.SendMessage(string.Format("客户端0已经成功连接到服务器", this._mClientSocket.RemoteEndPoint));
                //创建客户端消息线程，实现客户端消息的循环监听
                _mReceiveThread = new Thread(this.ReceiveClient);
                //启动线程
                _mReceiveThread.Start(this.ClienSocket);
            }
        }

        //接收客户端消息的方法
        public void ReceiveClient(object obj)
        {
            var mClientSocket = (Socket)obj;
            //循环标识位
            bool flag = true;
            while (flag)
            {
                try
                {
                    //获取数据长度
                    int receiveLength = mClientSocket.Receive(buffer);
                    //获取客户端消息
                    string clientMessage = Encoding.UTF8.GetString(buffer, 0, receiveLength);
                    //服务器负责分发数据
                    this.SendMessage(String.Format("客户端{0}发来消息:{1}", mClientSocket.RemoteEndPoint, clientMessage));

                }
                catch (Exception e)
                {
                    //从客户端列表中移除该客户端
                    this._mClientSockets.Remove(mClientSocket);
                    //向其他客户端通知其下线
                    this.SendMessage(string.Format("服务器发来消息:客户端{0}从服务器断开,断开原因:{1}", mClientSocket.RemoteEndPoint,
                        e.Message));
                    //断开连接
                    mClientSocket.Shutdown(SocketShutdown.Both);
                    mClientSocket.Close();
                    break;
                }
            }
        }

        //向客户端群发消息
        public void SendMessage(string msg)
        {
            if (msg == string.Empty || this._mClientSockets.Count <= 0) return;
            foreach (Socket s in this._mClientSockets)
            {
                (s as Socket).Send(Encoding.UTF8.GetBytes(msg));
            }
        }

        public void SendMessage(byte[] msg)
        {
            if(msg==null||this._mClientSockets.Count<=0) return;
            foreach (Socket s in this._mClientSockets)
            {
                (s as Socket).Send(msg);
            }
        }

        //向指定客户端发送消息
        public void SendMessage(string ip, int port, string msg)
        {
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ip), port);
            if (msg == string.Empty) return;
            foreach (Socket s in this._mClientSockets)
            {
                if (ipEnd.Equals((IPEndPoint)s.RemoteEndPoint))
                {
                    s.Send(Encoding.UTF8.GetBytes(msg));
                }
            }
        }

        public void SendMessage(string ip, int port, byte[] msg)
        {
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ip), port);
            if (msg == null) return;
            foreach (Socket s in this._mClientSockets)
            {
                if (ipEnd.Equals((IPEndPoint)s.RemoteEndPoint))
                {
                    s.Send(msg);
                }
            }
        }

    }
}
