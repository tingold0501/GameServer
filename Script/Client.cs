using System;
using System.Net.Sockets;

namespace GameServer
{
    class Client
    {
        public int index;
        public string IP;
        public TcpClient Socket;
        public NetworkStream myStream;
        public byte[] readBuff;

        public void Start()
        {
            Socket.SendBufferSize = 4096;
            Socket.ReceiveBufferSize = 4096;
            myStream = Socket.GetStream();
            Array.Resize(ref readBuff, Socket.ReceiveBufferSize);
            myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnReceiceData, null);
        }

        void CloseConnection()
        {
            Socket.Close();
            Socket = null;
            Console.WriteLine("Player disconnected: " + IP);
        }

        void OnReceiceData(IAsyncResult result)
        {
            try
            {
                int readBytes = myStream.EndRead(result);
                if(Socket == null)
                {
                    return;
                }
                if(readBytes <= 0)
                {
                    CloseConnection();
                    return;
                }
                byte[] newBytes = null;
                Array.Resize(ref newBytes, readBytes);
                Buffer.BlockCopy(readBuff, 0, newBytes, 0, readBytes);

                if(Socket == null)
                {
                    return;
                }
                myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnReceiceData, null);
            }
            catch(Exception ex)
            {
                CloseConnection();
                return;
            }
        }
    }
}
