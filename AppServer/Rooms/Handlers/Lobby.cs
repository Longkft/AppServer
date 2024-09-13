using AppServer.Applications.Interfaces;
using AppServer.Rooms.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Rooms.Handlers
{
    public class Lobby : BaseRoom
    {
        public Lobby(RoomType type) : base(type)
        {
            
        }

        public override bool JoinRoom(IPlayer player)
        {
            return base.JoinRoom(player);
        }
    }
}
