using AutoMapper;
using IdentityServer4.MongoDBDriver.Entities;

namespace IdentityServer4.MongoDBDriver.Mappers
{
    public class IdentityResourceProfile : Profile
    {
        public IdentityResourceProfile()
        {
            // entity to model
            CreateMap<IdentityResource, Models.IdentityResource>(MemberList.Destination);

            // model to entity
            CreateMap<Models.IdentityResource, IdentityResource>(MemberList.Source);
        }
    }
}
