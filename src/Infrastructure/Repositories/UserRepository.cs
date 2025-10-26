using CMS_NetApi.Domain.Entities;
using CMS_NetApi.Domain.Interfaces;
using CMS_NetApi.Infrastructure.Database.Models;
using CMS_NetApi.Infrastructure.Database.Context;
using MongoDB.Driver;
using MongoDB.Bson;
using AutoMapper;

namespace CMS_NetApi.Infrastructure.Repositories;

public class UserRepository(MongoDbContext context, IMapper mapper) : IUserRepository
{
    private readonly IMongoCollection<UserMongo> _user = context.Usuarios;
    private readonly IMapper _mapper = mapper;
    

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken ct = default)
    {
        var mongo = await _user.Find(x => x.Email == email)
                              .FirstOrDefaultAsync(ct);
        return mongo is null ? null : _mapper.Map<User>(mongo);
    }

    public async Task<User?> GetUserByIdAsync(string id, CancellationToken ct = default)
    {
        var mongo = await _user.Find(x => x.Id == id)
                              .FirstOrDefaultAsync(ct);
        return mongo is null ? null : _mapper.Map<User>(mongo);
    }

    public async Task AddUserAsync(User usuario, CancellationToken ct = default)
    {
        var mongo = _mapper.Map<UserMongo>(usuario);

        if (string.IsNullOrEmpty(mongo.Id))
        mongo.Id = ObjectId.GenerateNewId().ToString();
        await _user.InsertOneAsync(mongo, cancellationToken: ct);
    }

    public async Task UpdateUserAsync(User usuario, CancellationToken ct = default)
    {
        var mongo = _mapper.Map<UserMongo>(usuario);
        await _user.ReplaceOneAsync(x => x.Id == mongo.Id, mongo, cancellationToken: ct);
    }

    public async Task DeleteUserAsync(string id, CancellationToken ct = default)
    {
        await _user.DeleteOneAsync(x => x.Id == id, cancellationToken: ct);
    }
}