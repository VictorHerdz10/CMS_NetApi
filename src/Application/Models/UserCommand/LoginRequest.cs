namespace CMS_NetApi.Application.Models.UserCommand;

public record LoginRequest(
    string Email,
    string Password
);