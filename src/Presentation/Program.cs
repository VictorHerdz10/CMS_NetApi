using CMS_NetApi.Presentation.Extensions;
using CMS_NetApi.Presentation.Conventions;
using CMS_NetApi.Infrastructure;
using CMS_NetApi.Application;
using CMS_NetApi.Presentation.Profiles;

var builder = WebApplication.CreateBuilder(args);

// 1. Cargar .env si existe
builder.LoadEnv();

// 2. Ensamblar configuración (env primero, luego JSON)
var cfg = builder.Configuration;
builder.Services.AddAutoMapper(typeof(AuthDtoMapper));
builder.Services.AddHttpContextAccessor();

// 3. Registrar capas
builder.Services.AddInfrastructure(cfg);   // JWT, Mongo, repos, servicios
builder.Services.AddApplication(cfg);         // MediatR, AutoMapper, validaciones
builder.Services.AddControllers(options =>{options.Conventions.Add(new ApiRoutePrefixConvention("api"));});
builder.Services.AddEndpointsApiExplorer();
// 4. Middlewares de API (organizados en Extensions)
builder.Services.AddSwagger();
builder.Services.AddCorsPolicy(cfg);
builder.Services.AddJwtAuthentication(cfg);
var app = builder.Build();

// 5. Pipeline HTTP (orden claro y único)
app.UseApiPipeline();
app.Run();