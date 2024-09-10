using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDatabase.Mongodb.Interfaces
{
    public interface IGameDB<T> where T : class
    {
        IMongoDatabase GetDatabase();
        T Get(string id);
        List<T> GetAll();
        T Create(T item);
        bool Remove(string id);
        T UpDate(string id, T item);
    }
}
