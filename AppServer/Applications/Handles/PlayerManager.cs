using AppServer.Applications.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Applications.Handles
{
    public class PlayerManager : IPlayerManager
    { 
        public ConcurrentDictionary<string, IPlayer> Players {  get; set; }

        public PlayerManager()
        {
            Players = new ConcurrentDictionary<string, IPlayer>();
        }

        public void AddPlayer(IPlayer player)
        {
            if (FindPlayer(player) == null)
            {
                Players.TryAdd(player.SessionId, player);
                Console.WriteLine($"List Players {Players.Count}");
            }
        }

        public void RemovePlayer(string id)
        {
            if (FindPlayer(id) != null)
            {
                Players.TryRemove(id, out var player);
                if (player != null)
                {
                    Console.WriteLine($"Remove {id} success");
                    Console.WriteLine($"List Player {Players.Count}");
                }
            }
        }

        public void RemovePlayer(IPlayer player)
        {
            this.RemovePlayer(player.SessionId);
        }

        public IPlayer FindPlayer(string id)
        {
            return Players.FirstOrDefault(p => p.Value.Equals(id)).Value;
        }

        public IPlayer FindPlayer(IPlayer player)
        {
            return Players.FirstOrDefault(p => p.Value.Equals(player)).Value;
        }

        public List<IPlayer> GetPlayers()
        {
           return Players.Values.ToList();
        }
    }
}
