using AppServer.Rooms.Constants;
using AppServer.Rooms.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Rooms
{
    public class PuzzleRoom : BaseRoom
    {
        private readonly int _time;
        public PuzzleRoom(int time = 180) : base(RoomType.Battle)
        {
            _time = time;
        }
    }
}
