using AppServer.GameModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.GameModels
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Avata { get; set; }
        public int Level { get; set; }
        public long Amout { get; set; }

        public User(string username, string password, string displayname)
        {
            Username = username;
            Password = password;
            DisplayName = displayname;
            Avata = "";
            Level = 1;
            Amout = 0;
        }
    }
}
