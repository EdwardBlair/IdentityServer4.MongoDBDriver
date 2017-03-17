// Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Interfaces;
using IdentityServer4.MongoDBDriver.Entities;

namespace IdentityServer4.MongoDBDriver.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<IEnumerable<string>> GetAllowedOriginsAsync();
    }
}
