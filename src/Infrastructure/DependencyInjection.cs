using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CMS_NetApi.Infrastructure.Extensions;
using CMS_NetApi.Domain.Interfaces;
using CMS_NetApi.Infrastructure.Repositories;
using CMS_NetApi.Infrastructure.Database.Context;
using MongoDB.Driver;
using System.Reflection;

namespace CMS_NetApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        // 1. Settings

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // 2. MongoClient
        services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(cfg.GetValueOrEnv("BookStoreDatabase:ConnectionString", "MONGODB_CONNECTION_STRING")));

        // 3. MongoDbContext con fábrica
        var connStr = cfg.GetValueOrEnv("BookStoreDatabase:ConnectionString", "MONGODB_CONNECTION_STRING");
        var dbName = cfg.GetValueOrEnv("BookStoreDatabase:DatabaseName", "MONGODB_DATABASE_NAME");

        services.AddScoped(sp =>
            new MongoDbContext(connStr, dbName));

        // 4. Servicios y repos
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}