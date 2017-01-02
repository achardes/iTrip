using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Conventions;
using System.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace iTrip
{
    public static class DataContext
    {
        public static string DatabaseName = "iTrip";

        public static IMongoDatabase GetMongoDatabase(string databaseName)
        {
            string MongoDbServer = "mongodb://localhost:27017";

            IMongoClient client = null;

            try
            {
                 client = new MongoClient(MongoDbServer);
                
            }
            catch (Exception ex)
            {
                Console.Write("plop");
            }




            IMongoDatabase database = client.GetDatabase(databaseName);

            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            conventionPack.Add(new IgnoreExtraElementsConvention(true));
            conventionPack.Add(new EnumRepresentationConvention(BsonType.String));

            ConventionRegistry.Register("ConventionPack", conventionPack, x => true);

            return database;
        }

        public static void RecreateCollection<T>(IMongoDatabase database, string collectionName, IEnumerable<T> data)
        {
            if (data.Any())
            {
                string tmpCollectionName = collectionName + "_tmp";
                database.DropCollectionAsync(tmpCollectionName).Wait();
                database.CreateCollectionAsync(tmpCollectionName).Wait();

                IMongoCollection<T> collection = database.GetCollection<T>(tmpCollectionName);

                collection.InsertManyAsync(data).Wait();
                database.DropCollectionAsync(collectionName).Wait();
                database.RenameCollectionAsync(tmpCollectionName, collectionName);
            }
            else
            {
                database.DropCollectionAsync(collectionName).Wait();
            }
        }
    }
}
