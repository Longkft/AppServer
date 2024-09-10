using AppServer.Applications.Handles;
using AppServer.Applications.Interfaces;
using AppServer.GameModels;
using AppServer.Logging;
using GameDatabase.Mongodb.Handlers;
using NetCoreServer;
using System.Net;

namespace AppServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            IGameLogger logger = new GameLogger();

            var mongoDb = new MongoDb();
            //var mongoHandle = new MongoHandler<User>(mongoDb.GetDatabase());

            IPlayerManager playerManager = new PlayerManager(logger);
            var wsServer = new WsGameServer(IPAddress.Any, port: 8080, playerManager, logger, mongoDb);
            wsServer.StartServer();
            logger.Print("Game Server Started");
            while (true)
            {
                string? type = Console.ReadLine();
                if (type == "restart")
                {
                    logger.Print("Game Server Restart");
                    wsServer.RestartServer();
                }

                if (type == "shutdown")
                {
                    logger.Print("Game Server Stop");
                    wsServer.StopServer();
                    break;
                }
            }
        }
    }
}
