using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Applications.Interfaces
{
    public interface IPlayer
    {
        public string SessionId { get; set; }
        public string Name { get; set; }

        void SetDisconnect(bool value);
        bool SendMessage(string message);
        void OnDisconnect();
    }
}
