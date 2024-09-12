using AppServer.Applications.Interfaces;
using AppServer.Logging;
using AppServer.Rooms.Interfaces;
using GameDatabase.Mongodb.Handlers;
using NetCoreServer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace AppServer.Applications.Handles
{
    public class WsGameServer : WsServer, IWsGameServer
    {
        private int _port;
        public readonly IPlayerManager _playerManager;

        private readonly IGameLogger _logger;

        private readonly MongoDb _mongodb;
        public readonly IRoomManager RoomManager;

        public WsGameServer(IPAddress address, int port, IPlayerManager playerManager, IGameLogger logger, MongoDb mongodb, IRoomManager roomManager) : base(address, port)
        {
            _port = port;
            _playerManager = playerManager;
            _logger = logger;
            _mongodb = mongodb;
            RoomManager = roomManager;
        }

        protected override TcpSession CreateSession()
        {
            //todo handle new session
            var session = base.CreateSession();
            _logger.Info("New Session Connected");
            var player = new Player(this, _mongodb.GetDatabase());
            _logger.Info($"Session ID player: {session.Id}");
            _logger.Info($" ID player: {player.SessionId}");
            _playerManager.AddPlayer(player);
            return player;
        }

        protected override void OnDisconnected(TcpSession session)
        {
            _logger.Print($" ID player OnDisconnected: {session.Id}");
            //if (_playerManager.PlayerExists(session.Id.ToString()))
            //{
            //    _playerManager.RemovePlayer(session.Id.ToString());
            //    _logger.Info($"Session Disconnected");
            //}
            //else
            //{
            //    _logger.Error($"Warning: Attempted to remove non-existent player with sessionId: {session.Id}");
            //}
            var player = _playerManager.FindPlayer(session.Id.ToString());
            if (player != null)
            {
                _logger.Info($"Session player Disconnected");
                _playerManager.RemovePlayer(player);
            } else
            {
                _logger.Error($"Warning: Attempted to remove non-existent player with sessionId: {session.Id}");
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
                _logger.Print($"Server Ws started at {_port}");
                return;
            }
        }

        protected override void OnError(SocketError error)
        {
            _logger.Error($"Server Ws error");
            base.OnError(error);
        }

        public void StopServer()
        {
            //todo logic before stop
            this.Stop();
        }
    }
}
