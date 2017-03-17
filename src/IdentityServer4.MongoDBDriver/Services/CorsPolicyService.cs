// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Modifications Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.MongoDBDriver.Repositories;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDBDriver.Services
{
    public class CorsPolicyService : ICorsPolicyService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<CorsPolicyService> _logger;

        public CorsPolicyService(IClientRepository clientRepository, ILogger<CorsPolicyService> logger)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _logger = logger;
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            var origins = await _clientRepository.GetAllowedOriginsAsync();

            var isAllowed = origins.Contains(origin, StringComparer.OrdinalIgnoreCase);

            _logger.LogDebug("Origin {origin} is allowed: {originAllowed}", origin, isAllowed);

            return isAllowed;
        }
    }
}
