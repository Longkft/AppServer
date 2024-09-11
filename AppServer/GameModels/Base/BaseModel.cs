using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.GameModels.Base
{
    public class BaseModel
    {
        [BsonId]
        public string Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public BaseModel() 
        {
            CreateAt = DateTime.Now;
        }
    }
}
