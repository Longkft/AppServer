using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppServer.Applications.Handles
{
    public static class GameHelper
    {
        public static string ParseString<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static T ParseStruct<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
