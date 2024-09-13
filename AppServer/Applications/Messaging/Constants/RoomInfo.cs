using AppServer.Rooms.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Applications.Messaging.Constants
{
    public struct RoomInfo
    {
        public List<UserInfo> Players {  get; set; }
        public RoomType RoomType { get; set; }
    }
}
