// Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Common.MongoDBRepository;
using System;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using IdentityServer4.MongoDBDriver.Entities;

namespace IdentityServer4.MongoDBDriver.Repositories
{
    public class PersistedGrantRepository : MongoDBRepository<PersistedGrant>, IPersistedGrantRepository
    {
        private readonly TimeSpan expiredTokensTTL;

        public PersistedGrantRepository(PersistedGrantRepositoryOptions options) : base(options)
        {
            expiredTokensTTL = TimeSpan.FromSeconds(60);
        }

        protected override Action<BsonClassMap<PersistedGrant>> ClassMapInitializer => (classMap) =>
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);
        };

        protected override Action<IMongoCollection<PersistedGrant>> CollectionInitializer => (collection) =>
        {
            collection.Indexes.CreateOne(Builders<PersistedGrant>.IndexKeys.Ascending(x => x.Key), new CreateIndexOptions
            {
                Unique = true
            });

            collection.Indexes.CreateOne(Builders<PersistedGrant>.IndexKeys.Ascending(x => x.SubjectId));

            collection.Indexes.CreateOne(Builders<PersistedGrant>.IndexKeys.Ascending(x => x.Expiration), new CreateIndexOptions
            {
                ExpireAfter = expiredTokensTTL
            });
        };
    }
}
