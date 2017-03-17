using AutoMapper;
using IdentityServer4.MongoDBDriver.Entities;

namespace IdentityServer4.MongoDBDriver.Mappers
{
    public class SecretMapperProfile : Profile
    {
        public SecretMapperProfile()
        {
            // entity to model
            CreateMap<Secret, Models.Secret>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src != null));

            // model to entity
            CreateMap<Models.Secret, Secret>(MemberList.Source);
        }
    }
}
