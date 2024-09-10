using AppServer.Applications.Handles;
using AppServer.Applications.Interfaces;
using NetCoreServer;
using System.Net;

//namespace AppServer
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
            Console.WriteLine("Hello, World!");

            IPlayerManager playerManager = new PlayerManager();

            var wsServer = new WsGameServer(IPAddress.Any, port: 8080, playerManager);
            wsServer.StartServer();

            while (true)
            {
                string? type = Console.ReadLine();
                if (type == "restart")
                {
                    wsServer.RestartServer();
                }

                if (type == "shutdown")
                {
                    wsServer.StopServer();
                    break;
                }
            }
//        }   
//    }
//}
