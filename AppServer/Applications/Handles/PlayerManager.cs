﻿using AppServer.Applications.Interfaces;
using AppServer.Logging;
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

        private readonly IGameLogger _logger;

        public PlayerManager(IGameLogger logger)
        {
            _logger = logger;
            Players = new ConcurrentDictionary<string, IPlayer>();
        }

        public void AddPlayer(IPlayer player)
        {
            if (FindPlayer(player) == null)
            {
                Players.TryAdd(player.SessionId, player);
                //Players.AddOrUpdate(player.SessionId, player, (key, oldValue) => player);
                _logger.Info($"List Player {Players.Count}");
            }
        }

        public void RemovePlayer(string id)
        {
            if (FindPlayer(id) != null)
            {
                Players.TryRemove(id, out var player);
                if (player != null)
                {
                    _logger.Info($"Remove {id} success");
                    _logger.Info($"List Player {Players.Count}");
                }
            }
        }

        public bool PlayerExists(string sessionId)
        {
            return Players.ContainsKey(sessionId);
        }

        public void RemovePlayer(IPlayer player)
        {
            this.RemovePlayer(player.SessionId);
        }

        public IPlayer FindPlayer(string id)
        {
            return Players.FirstOrDefault(p => p.Value.SessionId == id).Value;
        }

        public IPlayer FindPlayer(IPlayer player)
        {
            return Players.Values.FirstOrDefault(p => p.SessionId == player.SessionId);
        }

        public List<IPlayer> GetPlayers()
        {
           return Players.Values.ToList();
        }
    }
}
