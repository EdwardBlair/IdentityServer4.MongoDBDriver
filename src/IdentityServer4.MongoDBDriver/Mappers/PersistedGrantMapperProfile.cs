using AutoMapper;
using IdentityServer4.MongoDBDriver.Entities;

namespace IdentityServer4.MongoDBDriver.Mappers
{
    public class PersistedGrantProfile : Profile
    {
        public PersistedGrantProfile()
        {
            // entity to model
            CreateMap<PersistedGrant, Models.PersistedGrant>(MemberList.Destination);

            // model to entity
            CreateMap<Models.PersistedGrant, PersistedGrant>(MemberList.Source);
        }
    }
}
