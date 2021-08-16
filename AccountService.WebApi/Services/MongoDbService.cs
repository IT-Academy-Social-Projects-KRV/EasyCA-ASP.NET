﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AccountService.WebApi.Services
{
    public class MongoDbService : IMongoDbService
    {
        public MongoClient Client { get; }
        public MongoDbService(IConfiguration configuration)
        {
            if (Client == null) Client = new MongoClient(configuration.GetConnectionString("MongoDb"));
        }
    }
}
