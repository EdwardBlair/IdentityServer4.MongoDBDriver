// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Modifications Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Xunit;
using IdentityServer4.MongoDBDriver.Mappers;
using System.Collections.Generic;

namespace IdentityServer4.MongoDBDriver.UnitTests.Mappers
{
    public class ClientMappersTests
    {
        [Fact]
        public void ClientAutomapperConfigurationIsValid()
        {
            var model = new Models.Client();
            var mappedEntity = model.ToEntity();
            var mappedModel = mappedEntity.ToModel();

            var modelList = new List<Models.Client>()
            {
                model
            };
            var mappedEntityList = modelList.ToEntity();
            var mappedModelList = mappedEntityList.ToModel();

            Assert.NotNull(mappedEntity);
            Assert.NotNull(mappedModel);
            Assert.NotNull(mappedEntityList);
            Assert.NotNull(mappedModelList);
            ClientMappers.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
