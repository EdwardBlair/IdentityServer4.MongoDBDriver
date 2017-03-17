// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Modifications Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.Extensions.Logging;
using IdentityServer4.MongoDBDriver.Repositories;
using IdentityServer4.MongoDBDriver.Mappers;

namespace IdentityServer4.MongoDBDriver.Stores
{
    public class ResourceStore : IResourceStore
    {
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IIdentityResourceRepository _identityResourceRepository;
        private readonly ILogger<ResourceStore> _logger;

        public ResourceStore(IApiResourceRepository apiResourceRepository, IIdentityResourceRepository identityResourceRepository, ILogger<ResourceStore> logger)
        {
            _apiResourceRepository = apiResourceRepository ?? throw new ArgumentNullException(nameof(apiResourceRepository));
            _identityResourceRepository = identityResourceRepository ?? throw new ArgumentNullException(nameof(identityResourceRepository));
            _logger = logger;
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var api = (await _apiResourceRepository.FindAsync(x => x.Name == name)).FirstOrDefault();

            var model = api.ToModel();

            if (model != null)
            {
                _logger.LogDebug("Found {api} API resource in database", name);
            }
            else
            {
                _logger.LogDebug("Did not find {api} API resource in database", name);
            }

            return model;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var names = scopeNames.ToArray();

            var apis = await _apiResourceRepository.FindAsync(api => api.Scopes.Where(x => names.Contains(x.Name)).Any());

            var model = apis.ToModel();

            _logger.LogDebug("Found {scopes} API scopes in database", model.SelectMany(x => x.Scopes).Select(x => x.Name));

            return model;
            
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var scopes = scopeNames.ToArray();

            var resources = await _identityResourceRepository.FindAsync(x => scopes.Contains(x.Name));

            var model = resources.ToModel();

            _logger.LogDebug("Found {scopes} identity scopes in database", model.Select(x => x.Name));

            return model;
        }

        public async Task<Resources> GetAllResources()
        {
            var identities = await _identityResourceRepository.FindAsync(x => true);

            var apis = await _apiResourceRepository.FindAsync(x => true);

            var result = new Resources(identities.ToModel(), apis.ToModel());

            _logger.LogDebug("Found {scopes} as all scopes in database", result.IdentityResources.Select(x => x.Name).Union(result.ApiResources.SelectMany(x => x.Scopes).Select(x => x.Name)));

            return result;
        }
    }
}
