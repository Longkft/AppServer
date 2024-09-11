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
    public class UserHandler : IDbHandler<User>
    {
        private readonly IGameDB<User> _userDb;

        public UserHandler(IMongoDatabase database)
        {
            _userDb = new MongoHandler<User>(database);
        }

        public User Find(string id)
        {
            var filter = Builders<User>.Filter.Eq(i => i.Id, id);
            return _userDb.Get(filter);
        }

        public List<User> FindAll()
        {
            return _userDb.GetAll();
        }

        public User FindByUserName(string username)
        {
            var filter = Builders<User>.Filter.Eq(i => i.Username, username);
            return _userDb.Get(filter);
        }

        public User Create(User item)
        {
            var user = _userDb.Create(item);
            return user;
        }

        public User Update(string id, User item)
        {
            var filter = Builders<User>.Filter.Eq(i => i.Id, id);
            var updater = Builders<User>.Update
                .Set(i => i.Password, item.Password)
                .Set(i => i.DisplayName, item.DisplayName)
                .Set(i => i.Amout, item.Amout)
                .Set(i => i.Level, item.Level)
                .Set(i => i.Avata, item.Avata)
                .Set(i => i.UpdateAt, DateTime.Now);
            _userDb.Update(filter, updater);
            return item;
        }

        public void Remove(string id)
        {
            var filter = Builders<User>.Filter.Eq(i => i.Id, id);
            _userDb.Remove(filter);
        }
    }
}
