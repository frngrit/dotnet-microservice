using System;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration config)
        {
            var connectionString = config.GetValue<string>("DatabaseSettings:ConnectionString");
            var databaseName = config.GetValue<string>("DatabaseSettings:DatabaseName");
            var collectionName = config.GetValue<string>("DatabaseSettings:CollectionName");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            Products = database.GetCollection<Product>(collectionName);
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}

