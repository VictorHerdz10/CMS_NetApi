using AutoMapper;
using CMS_NetApi.Domain.Entities;
using CMS_NetApi.Infrastructure.Database.Models;

namespace CMS_NetApi.Infrastructure.Profiles;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        // Domain → Mongo
        CreateMap<User, UserMongo>();

        // Mongo → Domain (si necesitas leer)
        CreateMap<UserMongo, User>();
    }
}