using AppServer.Applications.Interfaces;
using AppServer.Applications.Messaging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Rooms.Interfaces
{
    public interface IBaseRoom
    {
        public string Id { get; set; }
        public ConcurrentDictionary<string, IPlayer> Players { get; }
        bool JoinRoom(IPlayer player);
        bool ExitRoom(IPlayer player);
        bool ExitRoom(string id);
        IPlayer FindPlayer(string id);
        void SendMessage(string mes);
        void SendMessage<T>(WsMessage<T> message);
        void SendMessage<T>(WsMessage<T> message, string idIgnore);
    }
}
