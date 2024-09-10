using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDatabase.Mongodb.Handlers
{
    public class MongoDb
    {
        private readonly IMongoClient _client;
        private IMongoDatabase _database => _client.GetDatabase("GameOnline");

        public MongoDb()
        {
            try
            {
                var setting = MongoClientSettings.FromConnectionString("mongodb://localhost:27017/");
                _client = new MongoClient(setting);

                // Thêm kiểm tra để đảm bảo _database không null
                if (_database == null)
                {
                    Console.WriteLine("Failed to get database.");
                } 
                else
                {
                    Console.WriteLine("success to get database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing MongoDB: {ex.Message}");
                // Hoặc log lỗi bằng cách khác tùy theo nhu cầu của bạn
            }
        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }

        // Thêm phương thức để kiểm tra kết nối
        public bool IsConnected()
        {
            try
            {
                _client.ListDatabaseNames();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
