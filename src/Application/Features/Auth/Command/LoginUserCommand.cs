
using CMS_NetApi.Application.Services;
using CMS_NetApi.Domain.Entities;
using CMS_NetApi.Domain.Interfaces;
using CMS_NetApi.Application.Models.UserCommand;
using FluentValidation;
using MediatR;
using CMS_NetApi.Application.Exceptions;


namespace CMS_NetApi.Application.Features.Auth.Command;

public record LoginUserCommand(LoginRequest Datos) : IRequest<string>;

public class LoginValidator : AbstractValidator<LoginUserCommand>
{
    public LoginValidator()
    {
        RuleFor(c => c.Datos.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("Formato de email inválido");

        RuleFor(c => c.Datos.Password)
            .NotEmpty().WithMessage("Contraseña obligatoria");
    }
}

internal sealed class LoginUserCommandHandler(
    IUserRepository userRepository,
    IJwtService jwtService,
    IPasswordHasher passwordHasher) :
    IRequestHandler<LoginUserCommand, string>
{

    public async Task<string> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmailAsync(command.Datos.Email, cancellationToken)
            ?? throw new NotFoundException("El usuario no existe");

        if (!passwordHasher.Verify(command.Datos.Password, user.Password))
            throw new BadRequestException("La contraseña es incorrecta");

        var token = jwtService.GenerarToken(user);

        return token;
    }
}