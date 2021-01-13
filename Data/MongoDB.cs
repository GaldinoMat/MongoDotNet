using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using ApiMongoDB.Data.Collections;

namespace ApiMongoDB.Data
{
    public class MongoDB
    {
        // Variable used to access referenced database
        public IMongoDatabase DB { get; }

        // Receives an interface IConfiguration
        // Creates and stores reference from MongoDB database
        public MongoDB(IConfiguration configuration)
        {
            try
            {
                // Creates settings based on determined connection string
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                
                // Instantiates a mongo client object based on settings
                var client = new MongoClient(settings);
                
                // Gets the database from client and sets on public database
                DB = client.GetDatabase(configuration["DatabaseName"]);
                MapClasses();
            }
            catch (Exception ex)
            {
                throw new MongoException("It was not possible to connect to MongoDB", ex);
            }
        }

        private void MapClasses()
        {
            // Creates and registers a convention to work on camelCase
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            // Checks if there is any mapped class from determined type, if not, it creates and maps it
            if (!BsonClassMap.IsClassMapRegistered(typeof(Infected)))
            {
                BsonClassMap.RegisterClassMap<Infected>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}