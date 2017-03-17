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
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly IPersistedGrantRepository _persistedGrantRepository;
        private readonly ILogger _logger;

        public PersistedGrantStore(IPersistedGrantRepository persistedGrantRepository, ILogger<PersistedGrantStore> logger)
        {
            _persistedGrantRepository = persistedGrantRepository ?? throw new ArgumentNullException(nameof(persistedGrantRepository));
            _logger = logger;
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            var persistedGrants = (await _persistedGrantRepository.FindAsync(x => x.SubjectId == subjectId)).ToList();

            var model = persistedGrants.ToModel();

            _logger.LogDebug("{persistedGrantCount} persisted grants found for {subjectId}", model.Count, subjectId);

            return model;
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            var persistedGrant = (await _persistedGrantRepository.FindAsync(x => x.Key == key)).FirstOrDefault();

            var model = persistedGrant.ToModel();

            _logger.LogDebug("{persistedGrantKey} found in database: {persistedGrantKeyFound}", key, model != null);

            return model;
        }

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            _logger.LogDebug("removing persisted grants from database for subject {subjectId}, clientId {clientId}", subjectId, clientId);

            try
            {
                var deletedCount = await _persistedGrantRepository.DeleteManyAsync(x => x.SubjectId == subjectId && x.ClientId == clientId);

                _logger.LogDebug("removing persisted grants from database for subject {subjectId}, clientId {clientId}: removed {persistedGrantCount} grants", subjectId, clientId, deletedCount);

            }
            catch (Exception ex)
            {
                _logger.LogDebug("removing persisted grants from database for subject {subjectId}, clientId {clientId}: {error}", subjectId, clientId, ex.Message);
            }
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            _logger.LogDebug("removing persisted grants from database for subject {subjectId}, clientId {clientId}, grantType {persistedGrantType}", subjectId, clientId, type);

            try
            {
                var deletedCount = await _persistedGrantRepository.DeleteManyAsync(x => x.SubjectId == subjectId && x.ClientId == clientId && x.Type == type);

                _logger.LogDebug("removing persisted grants from database for subject {subjectId}, clientId {clientId}, grantType {persistedGrantType}: removed {persistedGrantCount} grants", subjectId, clientId, type, deletedCount);

            } catch (Exception ex)
            {
                _logger.LogDebug("removing persisted grants from database for subject {subjectId}, clientId {clientId}, grantType {persistedGrantType}: {error}", subjectId, clientId, type, ex.Message);
            }
        }

        public async Task RemoveAsync(string key)
        {
            var persistedGrant = (await _persistedGrantRepository.FindAsync(x => x.Key == key)).FirstOrDefault();
            if (persistedGrant != null)
            {
                _logger.LogDebug("removing {persistedGrantKey} persisted grant from database", key);

                try
                {
                    await _persistedGrantRepository.DeleteManyAsync(x => x.Key == key);
                } catch (Exception ex)
                {
                    _logger.LogInformation("exception removing {persistedGrantKey} persisted grant from database: {error}", key, ex.Message);
                }
            }
            else
            {
                _logger.LogDebug("no {persistedGrantKey} persisted grant found in database", key);
            }
        }

        public async Task StoreAsync(PersistedGrant token)
        {
            try
            {
                var existing = (await _persistedGrantRepository.FindAsync(x => x.Key == token.Key)).FirstOrDefault();
                if (existing == null)
                {
                    _logger.LogDebug("{persistedGrantKey} not found in database", token.Key);

                    var entity = token.ToEntity();

                    await _persistedGrantRepository.InsertOneAsync(entity);
                }
                else
                {
                    _logger.LogDebug("{persistedGrantKey} found in database", token.Key);
                    var entity = token.UpdateEntity(existing);
                    await _persistedGrantRepository.UpdateOneAsync(x => x.Key == token.Key, entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("exception updating {persistedGrantKey} persisted grant in database: {error}", token.Key, ex.Message);
            }
        }
    }
}
