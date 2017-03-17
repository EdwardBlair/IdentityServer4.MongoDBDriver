// Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Common.MongoDBRepository;
using MongoDB.Driver;

namespace IdentityServer4.MongoDBDriver.Repositories
{
    public class ApiResourceRepositoryOptions : IMongoDBRepositoryOptions<ApiResourceRepository>
    {
        public string ConnectionString { get; set; }

        public IMongoDatabase Database { get; set; }

        public string CollectionName { get; set; } = "apiResources";
    }
}
