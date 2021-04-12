using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SmartHome.Data.Infrastructure.Abstractions;
using SmartHome.Data.Infrastructure.Abstractions.Models;
using SmartHome.Data.Infrastructure.MongoDB.Configuration;
using SmartHome.Data.Infrastructure.MongoDB.Extentions;
using SmartHome.Data.Infrastructure.MongoDB.Models;
using System.Threading.Tasks;

namespace SmartHome.Data.Infrastructure.MongoDB.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly IMongoCollection<SensorDataDb> _sensorsCollection;

        public DataRepository(IOptionsMonitor<MongoDbConfiguration> options)
        {
            var config = options.CurrentValue;

            var client = new MongoClient(config.ConnectionString);
            var db = client.GetDatabase(config.Database);

            _sensorsCollection = db.GetCollection<SensorDataDb>("Sensors");
        }

        public async Task SaveSensorDataAsync(SensorDataRequest sensorDataRequest)
        {
            try
            {
                await _sensorsCollection.InsertOneAsync(sensorDataRequest.ToSensorDataDb());
            }
            catch { /* Do nothing */ }
        }
    }
}
