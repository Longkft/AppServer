using AppServer.Rooms.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Rooms.Interfaces
{
    public interface IRoomManager
    {
        BaseRoom Lobby { get; set; }
        BaseRoom CreateRoom();
        BaseRoom FindRoom(string id);
        bool RemoveRoom(string id);
    }
}
