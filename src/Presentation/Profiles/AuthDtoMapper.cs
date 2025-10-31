using AutoMapper;
using CMS_NetApi.Application.Models.UserCommand;
using CMS_NetApi.Presentation.Dtos.Request;
using CMS_NetApi.Presentation.Dtos.Responses;

namespace CMS_NetApi.Presentation.Profiles;

public class AuthDtoMapper : Profile
{
    public AuthDtoMapper()
    {
        // Mapeos de Register
        CreateMap<RegisterRequestDto, UsuarioRequest>();
        
        // Mapeos de Login - CORREGIDOS
        CreateMap<LoginRequestDto, LoginRequest>();
        
        // Mapeos de Authenticate
        CreateMap<UsuarioResponse, AuthenticateResponseDto>();
        
        // Los mapeos inversos solo si son necesarios
        // CreateMap<UsuarioRequest, RegisterRequestDto>(); // Solo si necesitas este mapeo inverso
        // CreateMap<LoginRequest, LoginRequestDto>(); // Solo si necesitas este mapeo inverso
        // CreateMap<AuthenticateResponseDto, UsuarioResponse>(); // Solo si necesitas este mapeo inverso
    }
}