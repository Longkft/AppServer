using AppServer.GameModels.Interfaces;
using GameDatabase.Mongodb.Handlers;
using GameDatabase.Mongodb.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.GameModels.Handlers
{
    public class RoomHandler : IDbHandler<RoomModel>
    {
        private readonly IGameDB<RoomModel> _roomDb;
        public RoomHandler(IMongoDatabase database)
        {
            _roomDb = new MongoHandler<RoomModel>(database);
        }
        public RoomModel Create(RoomModel item)
        {
            var room = _roomDb.Create(item);
            return room;
        }

        public RoomModel Find(string id)
        {
            var filter = Builders<RoomModel>.Filter.Eq(i => i.RoomId, id);
            return _roomDb.Get(filter);
        }

        public List<RoomModel> FindAll()
        {
            return _roomDb.GetAll();
        }

        public void Remove(string id)
        {
            var filter = Builders<RoomModel>.Filter.Eq(i => i.Id, id);
            _roomDb.Remove(filter);
        }

        public RoomModel Update(string id, RoomModel item)
        {
            var filter = Builders<RoomModel>.Filter.Eq(i => i.Id, id);
            var updater = Builders<RoomModel>.Update
                .Set(i => i.CountPlayer, item.CountPlayer)
                .Set(i => i.UpdateAt, DateTime.Now);
            _roomDb.Update(filter, updater);
            return item;
        }
    }
}
