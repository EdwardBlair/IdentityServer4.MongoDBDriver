using AutoMapper;
using IdentityServer4.MongoDBDriver.Entities;

namespace IdentityServer4.MongoDBDriver.Mappers
{
    public class ScopeMapperProfile : Profile
    {
        public ScopeMapperProfile()
        {
            // entity to model
            CreateMap<Scope, Models.Scope>(MemberList.Destination);

            // model to entity
            CreateMap<Models.Scope, Scope>(MemberList.Source);
        }
    }
}
