using AutoMapper;
using IdentityServer4.MongoDBDriver.Entities;
using System.Collections.Generic;

namespace IdentityServer4.MongoDBDriver.Mappers
{
    public static class IdentityResourceMappers
    {
        static IdentityResourceMappers()
        {
            Mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<IdentityResourceProfile>();
            }).CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static Models.IdentityResource ToModel(this IdentityResource identityResource)
        {
            return identityResource == null ? null : Mapper.Map<Models.IdentityResource>(identityResource);
        }

        public static List<Models.IdentityResource> ToModel(this IEnumerable<IdentityResource> identityResources)
        {
            return identityResources == null ? null : Mapper.Map<List<Models.IdentityResource>>(identityResources);
        }

        public static IdentityResource ToEntity(this Models.IdentityResource identityResource)
        {
            return identityResource == null ? null : Mapper.Map<IdentityResource>(identityResource);
        }

        public static List<IdentityResource> ToEntity(this IEnumerable<Models.IdentityResource> identityResources)
        {
            return identityResources == null ? null : Mapper.Map<List<IdentityResource>>(identityResources);
        }
    }
}
