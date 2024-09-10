using AppServer.Applications.Interfaces;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Applications.Handles
{
    public class WsGameServer : WsServer, IWsGameServer
    {
        private int _port;
        public readonly IPlayerManager _playerManager;

        public WsGameServer(IPAddress address, int port, IPlayerManager playerManager) : base(address, port)
        {
            _port = port;
            _playerManager = playerManager;
        }

        protected override TcpSession CreateSession()
        {
            //todo handle new session
            Console.WriteLine("New Session Connected");
            var player =  new Player(this);
            _playerManager.AddPlayer(player);
            return base.CreateSession();
        }

        protected override void OnDisconnected(TcpSession session)
        {
            Console.WriteLine($"Session Disconnected");
            var player = _playerManager.FindPlayer(session.Id.ToString());
            Console.WriteLine($"{_playerManager.FindPlayer(session.Id.ToString())}");
            Console.WriteLine($"player: {_playerManager}");
            Console.WriteLine($"Session ID: {session.Id.ToString()}");
            Console.WriteLine($"aaaaaaa111111");
            if (player != null)
            {
                Console.WriteLine($"aaaaaaa");
                _playerManager.RemovePlayer(player);
            }
            base.OnDisconnected(session);
        }

        public void SendAll(string mes)
        {
            this.MulticastText(mes);
        }

        public void RestartServer()
        {
            //todo logic before restart
            this.Restart();
        }

        public void StartServer()
        {
            //todo logic before start
            if (this.Start())
            {
                Console.WriteLine($"Server Ws started at {_port}");
                return;
            }
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Server Ws error");
            base.OnError(error);
        }

        public void StopServer()
        {
            //todo logic before stop
            this.Stop();
        }
    }
}
