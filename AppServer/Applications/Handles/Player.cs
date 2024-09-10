using AppServer.Applications.Interfaces;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Applications.Handles
{
    public class Player : WsSession, IPlayer
    {
        public string SessionId { get; set; }
        public string Name { get; set; }

        private bool IsDisconnected { get; set; }

        public Player(WsServer server) : base(server) 
        {
            SessionId = this.Id.ToString();
            IsDisconnected = false;
        }

        public override void OnWsConnected(HttpRequest request)
        {
            //todo login on player connected
            Console.WriteLine("Player Connected");
            IsDisconnected = false;
        }
        public override void OnWsDisconnected()
        {
            OnDisconnected();
            base.OnWsDisconnected();
        }

        public override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            Console.WriteLine($"Client {SessionId} send message {message}");
            ((WsGameServer) Server).SendAll($"{this.SessionId} send message {message}");
            //base.OnWsReceived(buffer, offset, size);
        }

        private bool _isConnected;

        public void SetDisconnect(bool value)
        {
            _isConnected = !value;
        }

        public bool SendMessage(string mes)
        {
            return this.SendTextAsync(mes);
        }

        public void OnDisconnect()
        {
            //todo logic handle player disconnected
            Console.WriteLine("Player Disconnected");
        }

        public void SetDisconect(bool value)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Player(Id: {Id})";
        }
    }
}
