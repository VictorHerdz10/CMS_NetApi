using CMS_NetApi.Domain.Entities;

namespace CMS_NetApi.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken ct = default);
    Task<User?> GetUserByIdAsync(string id, CancellationToken ct = default);
    Task AddUserAsync(User usuario, CancellationToken ct = default);
    Task UpdateUserAsync(User usuario, CancellationToken ct = default);
    Task DeleteUserAsync(string id, CancellationToken ct = default);
}