// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Modifications Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Xunit;
using IdentityServer4.MongoDBDriver.Mappers;
using System.Collections.Generic;
using FluentAssertions;
using System;

namespace IdentityServer4.MongoDBDriver.UnitTests.Mappers
{
    public class PersistedGrantMappersTests
    {
        [Fact]
        public void AutomapperConfiguration_IsValid()
        {
            var model = new Models.PersistedGrant();
            var mappedEntity = model.ToEntity();
            var mappedModel = mappedEntity.ToModel();

            var modelList = new List<Models.PersistedGrant>()
            {
                model
            };
            var mappedEntityList = modelList.ToEntity();
            var mappedModelList = mappedEntityList.ToModel();

            Assert.NotNull(mappedEntity);
            Assert.NotNull(mappedModel);
            Assert.NotNull(mappedEntityList);
            Assert.NotNull(mappedModelList);
            PersistedGrantMappers.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void UpdateEntity_should_copy_all_properties_from_new_model_to_existing_entity()
        {
            var newModel = new Models.PersistedGrant()
            {
                ClientId = "ClientId",
                Data = "New Data",
                CreationTime = new DateTime(2017, 01, 01, 0, 0, 0),
                Expiration = new DateTime(2017, 01, 02, 12, 0, 0),
                Key = "Key",
                SubjectId = "SubjectId",
                Type = "Type"
            };

            var existingEntity = new Entities.PersistedGrant()
            {
                ClientId = "ClientId",
                CreationTime = new DateTime(2017, 01, 01, 0, 0, 0),
                Data = "Data",
                Expiration = new DateTime(2017, 01, 02, 0, 0, 0),
                Key = "Key",
                SubjectId = "SubjectId",
                Type = "Type"
            };

            newModel.UpdateEntity(existingEntity);

            var expected = new Entities.PersistedGrant()
            {
                ClientId = "ClientId",
                CreationTime = new DateTime(2017, 01, 01, 0, 0, 0),
                Data = "New Data",
                Expiration = new DateTime(2017, 01, 02, 12, 0, 0),
                Key = "Key",
                SubjectId = "SubjectId",
                Type = "Type"
            };

            existingEntity.ShouldBeEquivalentTo(expected);
        }
    }
}
