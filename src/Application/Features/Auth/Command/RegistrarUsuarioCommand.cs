
using CMS_NetApi.Application.Services;
using CMS_NetApi.Domain.Entities;
using CMS_NetApi.Domain.Interfaces;
using CMS_NetApi.Application.Models.UserCommand;
using FluentValidation;
using MediatR;
using AutoMapper;
using CMS_NetApi.Application.Exceptions;


namespace CMS_NetApi.Application.Features.Auth.Command;

public record RegistrarUsuarioCommand(UsuarioRequest Datos) : IRequest<UsuarioResponse>;

public class RegistrarUsuarioValidator : AbstractValidator<RegistrarUsuarioCommand>
{
    public RegistrarUsuarioValidator()
    {
        RuleFor(c => c.Datos.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio");

        RuleFor(c => c.Datos.Email)
            .NotEmpty().WithMessage("Email obligatorio")
            .EmailAddress().WithMessage("Formato de email inválido");

        RuleFor(c => c.Datos.Password)
            .NotEmpty().WithMessage("Contraseña obligatoria")
            .MinimumLength(8).WithMessage("LA COntraseña debe de tener no menos de 8 caracteres ");
    }
}

internal sealed class RegistrarUsuarioCommandHandler(
    IUsuarioRepository repo,
    IMapper mapper,
    IPasswordHasher hasher) : IRequestHandler<RegistrarUsuarioCommand, UsuarioResponse>
{
    public async Task<UsuarioResponse> Handle(
        RegistrarUsuarioCommand cmd,
        CancellationToken ct)
    {
        // 1. ¿Existe email?
        if (await repo.GetByEmailAsync(cmd.Datos.Email, ct) is not null)
            throw new ConflictException("Email ya registrado");

        // 2. Crear entidad
        var user = mapper.Map<User>(cmd.Datos);

        // 3. Hash + rol especial
        user.Password = hasher.Hash(cmd.Datos.Password);
        user.TipoUsuario = cmd.Datos.Email switch
        {
            "gsanchez@uci.cu" or "victorhernandezsalcedo4@gmail.com" or "agalfonso@uci.cu"
                => "Admin_Gnl",
            _ => "Sin Asignar"
        };

        // 5. Persistir
        await repo.AddAsync(user, ct);

        // 6. Respuesta
        return mapper.Map<UsuarioResponse>(user);
    }
}