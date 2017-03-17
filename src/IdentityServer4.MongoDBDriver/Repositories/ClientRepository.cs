// Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Common.MongoDBRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;
using IdentityServer4.MongoDBDriver.Entities;

namespace IdentityServer4.MongoDBDriver.Repositories
{
    public class ClientRepository : MongoDBRepository<Client>, IClientRepository
    {
        public ClientRepository(ClientRepositoryOptions options) : base(options)
        {
        }

        public async Task<IEnumerable<string>> GetAllowedOriginsAsync()
        {
            var results = new List<string>();

            using (var cursor = await _collection.AsQueryable().SelectMany(x => x.AllowedCorsOrigins).Distinct().ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    results.AddRange(cursor.Current);
                }
            }

            return results;
        }

        protected override Action<BsonClassMap<Client>> ClassMapInitializer => (classMap) =>
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);
        };

        protected override Action<IMongoCollection<Client>> CollectionInitializer => (collection) =>
        {
            collection.Indexes.CreateOne(Builders<Client>.IndexKeys.Ascending(x => x.ClientId), new CreateIndexOptions
            {
                Unique = true
            });
        };
    }
}
