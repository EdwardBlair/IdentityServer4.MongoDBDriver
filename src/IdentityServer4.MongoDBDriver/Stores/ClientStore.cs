// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Modifications Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using System.Linq;
using IdentityServer4.MongoDBDriver.Repositories;
using IdentityServer4.MongoDBDriver.Mappers;

namespace IdentityServer4.MongoDBDriver.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientStore> _logger;

        public ClientStore(IClientRepository clientRepository, ILogger<ClientStore> logger)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _logger = logger;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = (await _clientRepository.FindAsync(x => x.ClientId == clientId)).FirstOrDefault();

            var model = client?.ToModel();

            if (model != null)
            {
                _logger.LogDebug("Found {clientId} client in database", clientId);
            }
            else
            {
                _logger.LogDebug("Did not find {clientId} client in database", clientId);
            };

            return model;
        }
    }
}
