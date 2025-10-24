using CMS_NetApi.Domain.Entities;

namespace CMS_NetApi.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<User?> GetByIdAsync(string id, CancellationToken ct = default);
    Task AddAsync(User usuario, CancellationToken ct = default);
    Task UpdateAsync(User usuario, CancellationToken ct = default);
    Task DeleteAsync(string id, CancellationToken ct = default);
}