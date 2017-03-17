// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Modifications Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.MongoDBDriver.Repositories;
using IdentityServer4.MongoDBDriver.Services;
using IdentityServer4.MongoDBDriver.Stores;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerMongoDBBuilderExtensions
    {
        public static IIdentityServerBuilder AddConfigurationStore(
            this IIdentityServerBuilder builder,
            string connectionString)
        {
            return builder.AddConfigurationStore(
                config => { config.ConnectionString = connectionString; },
                config => { config.ConnectionString = connectionString; },
                config => { config.ConnectionString = connectionString; });
        }

        public static IIdentityServerBuilder AddConfigurationStore(
            this IIdentityServerBuilder builder,
            Action<ClientRepositoryOptions> clientRepositorySetupAction,
            Action<ApiResourceRepositoryOptions> apiResourceRepositorySetupAction,
            Action<IdentityResourceRepositoryOptions> identityResourceRepositorySetupAction)
        {
            builder.Services.Configure(clientRepositorySetupAction);
            builder.Services.Configure(apiResourceRepositorySetupAction);
            builder.Services.Configure(identityResourceRepositorySetupAction);

            builder.AddConfigurationStore();

            return builder;
        }

        private static IIdentityServerBuilder AddConfigurationStore(
            this IIdentityServerBuilder builder)
        {
            //var configStoreBuilder = builder.AddConfigurationStoreBuilder()
            //configStoreBuilder.AddRequiredPlatformServices()
            builder.Services.AddOptions();
            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<ClientRepositoryOptions>>().Value);
            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<ApiResourceRepositoryOptions>>().Value);
            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<IdentityResourceRepositoryOptions>>().Value);

            //configStoreBuilder.AddCoreServices()
            builder.Services.TryAddSingleton<IClientRepository, ClientRepository>();
            builder.Services.TryAddSingleton<IApiResourceRepository, ApiResourceRepository>();
            builder.Services.TryAddSingleton<IIdentityResourceRepository, IdentityResourceRepository>();

            builder.Services.AddTransient<IClientStore, ClientStore>();
            builder.Services.AddTransient<IResourceStore, ResourceStore>();
            builder.Services.AddTransient<ICorsPolicyService, CorsPolicyService>();

            return builder;
        }

        // Operational Store
        public static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder,
            string connectionString)
        {
            builder.AddOperationalStore(config => { config.ConnectionString = connectionString; });
            return builder;
        }

        public static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder,
            Action<PersistedGrantRepositoryOptions> persistedGrantRepositorySetupAction)
        {
            builder.Services.Configure(persistedGrantRepositorySetupAction);

            builder.AddOperationalStore();

            return builder;
        }

        private static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder)
        {
            //var opStoreBuilder = builder.AddOperationalStoreBuilder()
            //opStoreBuilder.AddRequiredPlatformServices()
            builder.Services.AddOptions();
            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<PersistedGrantRepositoryOptions>>().Value);

            //opStoreBuilder.AddCoreServices()
            builder.Services.TryAddSingleton<IPersistedGrantRepository, PersistedGrantRepository>();

            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

            return builder;
        }
    }
}