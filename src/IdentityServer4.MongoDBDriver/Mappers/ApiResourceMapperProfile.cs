using AutoMapper;
using IdentityServer4.MongoDBDriver.Entities;

namespace IdentityServer4.MongoDBDriver.Mappers
{
    public class ApiResourceProfile : Profile
    {
        public ApiResourceProfile()
        {
            // entity to model
            CreateMap<ApiResource, Models.ApiResource>(MemberList.Destination);

            // model to entity
            CreateMap<Models.ApiResource, ApiResource>(MemberList.Source);
        }
    }
}
