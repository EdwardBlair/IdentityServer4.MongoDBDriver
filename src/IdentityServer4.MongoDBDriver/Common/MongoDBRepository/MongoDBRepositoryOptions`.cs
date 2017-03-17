// Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Common.Interfaces;
using MongoDB.Driver;

namespace Common.MongoDBRepository
{
    public interface IMongoDBRepositoryOptions<out TRepository>
        where TRepository : IRepository
    {
        string ConnectionString { get; }
        IMongoDatabase Database { get; }
        string CollectionName { get; }
    }
}
