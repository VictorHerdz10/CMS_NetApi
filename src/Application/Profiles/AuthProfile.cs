using AutoMapper;
using CMS_NetApi.Application.Models.UserCommand;
using CMS_NetApi.Domain.Entities;
namespace CMS_NetApi.Application.Profiles;


public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<UsuarioRequest, User>().ForMember(d => d.Password, o => o.Ignore());
        CreateMap<User, UsuarioResponse>();
    }
}