using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Applications.Interfaces
{
    public interface IPlayerManager
    {
        ConcurrentDictionary<string, IPlayer> Players { get; }

        void AddPlayer(IPlayer player);
        void RemovePlayer(string id);
        bool PlayerExists(string id);
        void RemovePlayer(IPlayer player);
        IPlayer FindPlayer(string id);
        IPlayer FindPlayer(IPlayer player);
        List<IPlayer> GetPlayers();
    }
}
