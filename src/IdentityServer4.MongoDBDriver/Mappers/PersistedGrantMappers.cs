using AutoMapper;
using IdentityServer4.MongoDBDriver.Entities;
using System.Collections.Generic;

namespace IdentityServer4.MongoDBDriver.Mappers
{
    public static class PersistedGrantMappers
    {
        static PersistedGrantMappers()
        {
            Mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<PersistedGrantProfile>();
            }).CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static Models.PersistedGrant ToModel(this PersistedGrant persistedGrant)
        {
            return persistedGrant == null ? null : Mapper.Map<Models.PersistedGrant>(persistedGrant);
        }

        public static List<Models.PersistedGrant> ToModel(this IEnumerable<PersistedGrant> persistedGrants)
        {
            return persistedGrants == null ? null : Mapper.Map<List<Models.PersistedGrant>>(persistedGrants);
        }

        public static PersistedGrant ToEntity(this Models.PersistedGrant persistedGrant)
        {
            return persistedGrant == null ? null : Mapper.Map<PersistedGrant>(persistedGrant);
        }

        public static List<PersistedGrant> ToEntity(this IEnumerable<Models.PersistedGrant> persistedGrants)
        {
            return persistedGrants == null ? null : Mapper.Map<List<PersistedGrant>>(persistedGrants);
        }

        public static PersistedGrant UpdateEntity(this Models.PersistedGrant persistedGrant, PersistedGrant target)
        {
            return Mapper.Map(persistedGrant, target);
        }
    }
}