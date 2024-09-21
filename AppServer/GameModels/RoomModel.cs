using AppServer.Applications.Handles;
using AppServer.GameModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using XAct.Users;

namespace AppServer.GameModels
{
    public class RoomModel : BaseModel
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public int CountPlayer { get; set; }

        public RoomModel(string roomName)
        {
            RoomId = Guid.NewGuid().ToString();
            RoomName = roomName;
        }
    }
}
