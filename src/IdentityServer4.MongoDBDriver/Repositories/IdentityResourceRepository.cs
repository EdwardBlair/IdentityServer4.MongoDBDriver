// Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Common.MongoDBRepository;
using System;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using IdentityServer4.MongoDBDriver.Entities;

namespace IdentityServer4.MongoDBDriver.Repositories
{
    public class IdentityResourceRepository : MongoDBRepository<IdentityResource>, IIdentityResourceRepository
    {
        public IdentityResourceRepository(IdentityResourceRepositoryOptions options) : base(options)
        {
        }

        protected override Action<BsonClassMap<IdentityResource>> ClassMapInitializer => (classMap) =>
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);
        };

        protected override Action<IMongoCollection<IdentityResource>> CollectionInitializer => (collection) =>
        {
            collection.Indexes.CreateOne(Builders<IdentityResource>.IndexKeys.Ascending(x => x.Name), new CreateIndexOptions
            {
                Unique = true
            });
        };
    }
}