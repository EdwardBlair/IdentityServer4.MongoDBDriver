using AutoMapper;
using IdentityServer4.MongoDBDriver.Entities;
using System.Collections.Generic;

namespace IdentityServer4.MongoDBDriver.Mappers
{
    public static class ClientMappers
    {
        static ClientMappers()
        {
            Mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<ClientMapperProfile>();
                cfg.AddProfile<SecretMapperProfile>();
            }).CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static Models.Client ToModel(this Client client)
        {
            return client == null ? null : Mapper.Map<Models.Client>(client);
        }

        public static List<Models.Client> ToModel(this IEnumerable<Client> clients)
        {
            return clients == null ? null : Mapper.Map<List<Models.Client>>(clients);
        }

        public static Client ToEntity(this Models.Client client)
        {
            return client == null ? null : Mapper.Map<Client>(client);
        }

        public static List<Client> ToEntity(this IEnumerable<Models.Client> clients)
        {
            return clients == null ? null : Mapper.Map<List<Client>>(clients);
        }
    }
}
