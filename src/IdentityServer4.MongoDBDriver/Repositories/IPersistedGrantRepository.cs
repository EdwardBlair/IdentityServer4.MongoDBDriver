// Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityServer4.MongoDBDriver.Entities;

namespace IdentityServer4.MongoDBDriver.Repositories
{
    public interface IPersistedGrantRepository
    {
        Task<long> DeleteManyAsync(Expression<Func<PersistedGrant, bool>> predicate);

        Task<IEnumerable<PersistedGrant>> FindAsync(Expression<Func<PersistedGrant, bool>> predicate);

        Task<PersistedGrant> InsertOneAsync(PersistedGrant entity);

        Task<PersistedGrant> UpdateOneAsync(Expression<Func<PersistedGrant, bool>> predicate, PersistedGrant entity);
    }
}
