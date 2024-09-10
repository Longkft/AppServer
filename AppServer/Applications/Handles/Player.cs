using AppServer.Applications.Interfaces;
using AppServer.Applications.Messaging;
using AppServer.Applications.Messaging.Constants;
using AppServer.GameModels;
using AppServer.Logging;
using GameDatabase.Mongodb.Handlers;
using GameDatabase.Mongodb.Interfaces;
using MongoDB.Driver;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Applications.Handles
{
    public class Player : WsSession, IPlayer
    {
        public string SessionId { get; set; }
        public string Name { get; set; }

        private bool IsDisconnected { get; set; }

        private readonly IGameLogger _logger;

        private IGameDB<User> UsersDb { get; set; }

        public Player(WsServer server, IMongoDatabase database) : base(server) 
        {
            SessionId = this.Id.ToString();
            IsDisconnected = false;
            _logger = new GameLogger();
            UsersDb = new MongoHandler<User>(database);
        }

        public override void OnWsConnected(HttpRequest request)
        {
            //todo login on player connected
            var url = request.Url;
            _logger.Info("Player Connected");
            IsDisconnected = false;
            base.OnWsConnected(request);
        }
        public override void OnWsDisconnected()
        {
            OnDisconnected();
            base.OnWsDisconnected();
        }

        public override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            _logger.Print($"Client {SessionId} send message {message}");

            try
            {
                var wsMess = GameHelper.ParseStruct<WsMessage<object>>(message);
                switch(wsMess.Tags)
                {
                    case WsTags.Invalid:
                        break;
                    case WsTags.Login:
                        var loginData = GameHelper.ParseStruct<LoginData>(wsMess.Data.ToString());
                        var user = new User("volong", "123456", "Admin");
                        var newUser = UsersDb.Create(user);
                        _logger.Info(newUser.Username);
                        break;
                    case WsTags.Register:
                        break;
                    case WsTags.Lobby:
                        break;
                }
            }
            catch (Exception e)
            {
                _logger.Error("OnWsReceived error", e);
            }

            //((WsGameServer) Server).SendAll($"{this.SessionId} send message {message}");
            //base.OnWsReceived(buffer, offset, size);
        }

        private bool _isConnected;

        public void SetDisconnect(bool value)
        {
            _isConnected = !value;
        }

        public bool SendMessage(string mes)
        {
            return this.SendTextAsync(mes);
        }

        public void OnDisconnect()
        {
            //todo logic handle player disconnected
            _logger.Info("Player Disconnected");
        }

        public override string ToString()
        {
            return $"Player(Id: {Id})";
        }
    }
}
