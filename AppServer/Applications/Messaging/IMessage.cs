using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Applications.Messaging
{
    public interface IMessage<T>
    {
        public WsTags Tags { get; set; }
        public T Data { get; set; }
        
    }
}
