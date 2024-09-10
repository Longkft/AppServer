using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Logging
{
    public interface IGameLogger
    {
        void Print(string msg);
        void Info(string info);
        void Warning(string ms, Exception exception);
        void Error(string error, Exception exception);
        void Error(string error);
    }
}
