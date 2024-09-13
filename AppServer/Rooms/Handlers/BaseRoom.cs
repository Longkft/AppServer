using AppServer.Applications.Handles;
using AppServer.Applications.Interfaces;
using AppServer.Applications.Messaging;
using AppServer.Applications.Messaging.Constants;
using AppServer.Rooms.Constants;
using AppServer.Rooms.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Rooms.Handlers
{
    public class BaseRoom : IBaseRoom
    {
        public string Id { get ; set ; }

        public ConcurrentDictionary<string, IPlayer> Players {  get; set ; }
        public RoomType RoomType { get ; set ; }

        public BaseRoom(RoomType type) 
        {
            RoomType = type ;
            Id = GameHelper.RandomString(10);
            Players = new ConcurrentDictionary<string, IPlayer>();
        }

        

        public virtual IPlayer FindPlayer(string id)
        {
            return Players.FirstOrDefault(p => p.Key.Equals(id)).Value;
        }

        public virtual bool JoinRoom(IPlayer player)
        {
            if (FindPlayer(player.SessionId) == null)
            {
               if(Players.TryAdd(player.SessionId, player))
                {
                    this.RoomInfo();
                    return true;
                }
            }
            return false;
        }

        private void RoomInfo()
        {
            var lobby = new RoomInfo
            {
                RoomType=RoomType,
                Players = Players.Values.Select(p => p.GetUserInfo()).ToList()
            };
            var mess = new WsMessage<RoomInfo>(WsTags.RoomInfo, lobby);
            this.SendMessage(mess);
        }

        public void SendMessage(string mes)
        {
            lock (Players) 
            {
                foreach (var player in Players.Values) 
                { 
                    player.SendMessage(mes);
                }
            }
        }

        public void SendMessage<T>(WsMessage<T> message)
        {
            lock (Players)
            {
                foreach (var player in Players.Values)
                {
                    player.SendMessage(message);
                }
            }
        }

        public void SendMessage<T>(WsMessage<T> message, string idIgnore)
        {
            lock (Players)
            {
                foreach (var player in Players.Values.Where(p => p.SessionId != idIgnore))
                {
                    player.SendMessage(message);
                }
            }
        }

        public virtual bool ExitRoom(IPlayer player)
        {
            return this.ExitRoom(player.SessionId);
        }

        public virtual bool ExitRoom(string id)
        {
            var player = FindPlayer(id);
            if (player != null)
            {
                Players.TryRemove(player.SessionId, out player);
                this.RoomInfo();
                return true;
            }
            return false;
        }
    }
}
