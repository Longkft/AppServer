using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Applications.Messaging
{
    public enum WsTags
    {
        Invalid,
        Login,
        Register,
        UserInfo,
        RoomInfo,
        LobbyInfo,
        CreateRoom,
        Play
    }
}
