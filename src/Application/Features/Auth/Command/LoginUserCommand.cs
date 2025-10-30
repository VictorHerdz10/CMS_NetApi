
using CMS_NetApi.Application.Services;
using CMS_NetApi.Domain.Entities;
using CMS_NetApi.Domain.Interfaces;
using CMS_NetApi.Application.Models.UserCommand;
using FluentValidation;
using MediatR;
using AutoMapper;
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
    IMapper mapper,
    IPasswordHasher passwordHasher) :
    IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<string> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(command.Datos.Email, cancellationToken)
            ?? throw new NotFoundException("El usuario no existe");

        if (!_passwordHasher.Verify(command.Datos.Password, user.Password))
            throw new BadRequestException("La contraseña es incorrecta");

        var userMapped = _mapper.Map<User>(user);
        var token = _jwtService.GenerarToken(userMapped);

        return token;
    }
}