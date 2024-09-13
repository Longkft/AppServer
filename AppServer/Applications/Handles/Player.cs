using AppServer.Applications.Interfaces;
using AppServer.Applications.Messaging;
using AppServer.Applications.Messaging.Constants;
using AppServer.GameModels;
using AppServer.GameModels.Handlers;
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
        private UserHandler UsersDb { get; set; }
        private User UserInfo { get; set; }

        public Player(WsServer server, IMongoDatabase database) : base(server) 
        {
            SessionId = this.Id.ToString();
            IsDisconnected = false;
            _logger = new GameLogger();
            UsersDb = new UserHandler(database);
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
            OnDisconnect();
            base.OnWsDisconnected();
        }

        public override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            _logger.Print($"Client {SessionId} send message {message}");

            try
            {
                var wsMess = GameHelper.ParseStruct<WsMessage<object>>(message);
                switch (wsMess.Tags)
                {
                    case WsTags.Invalid:
                        break;
                    case WsTags.Login:
                        var loginData = GameHelper.ParseStruct<LoginData>(wsMess.Data.ToString());
                        UserInfo = UsersDb.FindByUserName(loginData.Username);
                        if (UserInfo != null)
                        {
                            var hashPass = GameHelper.HashPassword(loginData.Password);
                            if (hashPass == UserInfo.Password)
                            {
                                //todo in lobby
                                var messInfo = new WsMessage<UserInfo>(WsTags.UserInfo, this.GetUserInfo());
                                this.SendMessage(messInfo);
                                this.PlayerJoinLobby();
                                return;
                            }
                        }
                        var invalidMess = new WsMessage<string>(WsTags.Invalid, "Username or Password is Invalid");
                        this.SendMessage(GameHelper.ParseString(invalidMess));
                        //var user = new User("volong", "123456", "Admin");
                        //var newUser = UsersDb.Create(user);
                        //_logger.Info(newUser.Username);
                        break;
                    case WsTags.Register:
                        var regData = GameHelper.ParseStruct<RegisterData>(wsMess.Data.ToString());
                        if (UserInfo != null)
                        {
                            invalidMess = new WsMessage<string>(WsTags.Invalid, "You are Logined");
                            this.SendMessage(GameHelper.ParseString(invalidMess));
                            return;
                        }
                        var check = UsersDb.FindByUserName(regData.Username);
                        if (check != null)
                        {
                            invalidMess = new WsMessage<string>(WsTags.Invalid, "Username exits");
                            this.SendMessage(GameHelper.ParseString(invalidMess));
                            return;
                        }
                        var newUser = new User(regData.Username, regData.Password, regData.DisplayName);
                        UserInfo = UsersDb.Create(newUser);
                        if (UserInfo != null)
                        {
                            //todo in lobby
                            this.PlayerJoinLobby();
                        }
                        break;
                    case WsTags.RoomInfo:
                        break;
                    case WsTags.UserInfo:
                        break;
                    case WsTags.CreateRoom:
                        var createRoom = GameHelper.ParseStruct<CreateRoomData>(wsMess.Data.ToString());
                        this.OnUSerCreateRoom(createRoom);
                        break;
                    case WsTags.Play:
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

        private void OnUSerCreateRoom(CreateRoomData data)
        {
            var room = ((WsGameServer)Server).RoomManager.CreateRoom(data.Time);
            if (room != null && room.JoinRoom(this))
            {
                var lobby = ((WsGameServer)Server).RoomManager.Lobby;
                lobby.ExitRoom(this);
            }
        }

        private void PlayerJoinLobby()
        {
            var lobby = ((WsGameServer)Server).RoomManager.Lobby;
            lobby.JoinRoom(this);
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
            var lobby = ((WsGameServer)Server).RoomManager.Lobby;
            lobby.ExitRoom(this);
            _logger.Info("Player Disconnected");
        }

        public override string ToString()
        {
            return $"Player(Id: {Id})";
        }

        public UserInfo GetUserInfo()
        {
            if (UserInfo != null)
            {
                return new UserInfo
                {
                    DisplayName = UserInfo.DisplayName,
                    Amout = UserInfo.Amout,
                    Avata = UserInfo.Avata,
                    Level = UserInfo.Level
                };
            }
            return new UserInfo();
        }

        public bool SendMessage<T>(WsMessage<T> message)
        {
            var mes = GameHelper.ParseString(message);
            return this.SendMessage(mes);
        }
    }
}
