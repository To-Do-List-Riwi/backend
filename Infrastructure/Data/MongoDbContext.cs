using backend.Models;
using MongoDB.Driver;

namespace backend.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IConfiguration configuration) 
        {
            var client = new MongoClient(configuration.GetConnectionString("DbConnection"));
            _database = client.GetDatabase(configuration["MongoDbSettings:Database"]);
        }

        public IMongoCollection<ToDoList> ToDoLists => _database.GetCollection<ToDoList>("ToDoLists");
    }
}