using AutoMapper;
using CMS_NetApi.Application.Models.UserCommand;
using CMS_NetApi.Presentation.Dtos.Request;
namespace CMS_NetApi.Presentation.Profiles;

public class AuthDtoMapper : Profile
{
    public AuthDtoMapper()
    {
        CreateMap<RegisterRequestDto, UsuarioRequest>();
        CreateMap<UsuarioRequest, RegisterRequestDto>();

    }
}