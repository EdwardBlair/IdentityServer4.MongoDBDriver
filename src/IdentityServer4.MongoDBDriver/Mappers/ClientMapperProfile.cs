using AutoMapper;
using IdentityServer4.MongoDBDriver.Entities;
using System.Linq;

namespace IdentityServer4.MongoDBDriver.Mappers
{
    public class ClientMapperProfile : Profile
    {
        public ClientMapperProfile()
        {
            // entity to model
            CreateMap<Client, Models.Client>(MemberList.Destination)
                .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new System.Security.Claims.Claim(x.Type, x.Value))));

            // model to entity
            CreateMap<Models.Client, Client>(MemberList.Source)
                .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new Claim { Type = x.Type, Value = x.Value })));
        }
    }
}
