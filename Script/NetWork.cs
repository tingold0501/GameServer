using System;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    class NetWork
    {
        public TcpListener ServerSocket;
        public static NetWork instance = new NetWork();
        public static Client[] Clients = new Client[100];

        public void ServerStart()
        {
            for(int i = 1; i < 100; i++)
            {
                Clients[i] = new Client();
            }
            ServerSocket = new TcpListener(IPAddress.Any, 5012);
            ServerSocket.Start();
            ServerSocket.BeginAcceptTcpClient(OnClientConnect ,null);
            Console.WriteLine("Server has successfully started.");
        }

        void OnClientConnect(IAsyncResult result)
        {
            TcpClient client = ServerSocket.EndAcceptTcpClient(result);
            client.NoDelay = false;
            ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);

        }
    }
}
