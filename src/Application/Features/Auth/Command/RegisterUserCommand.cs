
using CMS_NetApi.Application.Services;
using CMS_NetApi.Domain.Entities;
using CMS_NetApi.Domain.Interfaces;
using CMS_NetApi.Application.Models.UserCommand;
using FluentValidation;
using MediatR;
using AutoMapper;
using CMS_NetApi.Application.Exceptions;


namespace CMS_NetApi.Application.Features.Auth.Command;

public record RegisterUserCommand(UsuarioRequest Datos) : IRequest<string>;

public class RegistrarUsuarioValidator : AbstractValidator<RegisterUserCommand>
{
    public RegistrarUsuarioValidator()
    {
         RuleFor(c => c.Datos.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(c => c.Datos.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("Formato de email inválido")
            .MaximumLength(150).WithMessage("El email no puede exceder 150 caracteres");

        RuleFor(c => c.Datos.Password)
            .NotEmpty().WithMessage("Contraseña obligatoria")
            .MinimumLength(8).WithMessage("LA COntraseña debe de tener no menos de 8 caracteres ");
    }
}

internal sealed class RegisterUserCommandHandler(
    IUserRepository repo,
    IMapper mapper,
    IPasswordHasher hasher) : IRequestHandler<RegisterUserCommand, string>
{
    public async Task<string> Handle(
        RegisterUserCommand cmd,
        CancellationToken ct)
    {
        // 1. ¿Existe email?
        if (await repo.GetUserByEmailAsync(cmd.Datos.Email, ct) is not null)
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
        await repo.AddUserAsync(user, ct);

        // 6. Respuesta
        return "Usuario registrado exitosamente";
    }
}