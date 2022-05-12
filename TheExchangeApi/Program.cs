using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TheExchangeApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using FluentValidation;
using TheExchangeApi.PipelineBehaviours;

var builder = WebApplication.CreateBuilder(args);

RegisterMediatR();

RegisterValidators();

GetDatabaseName();

InjectDatabaseSettings();

GetConnectionStringForMongoClient();

CorsConfig();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

JwtAuthentication();
AddAuthPolicies();


void RegisterMediatR()
{
    builder.Services.AddMediatR(typeof(Program).Assembly)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
}
void RegisterValidators()
{
    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
}
void GetDatabaseName()
{
    builder.Services.Configure<ProductDatabaseSettings>(
    builder.Configuration.GetSection("ProductDatabase"));
}
void InjectDatabaseSettings()
{
    builder.Services.AddSingleton<IProductDatabaseSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);
}
void GetConnectionStringForMongoClient()
{
    builder.Services.AddSingleton<IMongoClient>(singleton =>
    new MongoClient(builder.Configuration.GetValue<string>("ProductDatabase:ConnectionString")));
}
void CorsConfig()
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("theExchangeShopPolicy",
            policy =>
            {
                policy.WithOrigins(
                    "https://exchange-shop.netlify.app",
                    "http://localhost:3000",
                    "https://exchange-dashboard.netlify.app")
                .AllowAnyHeader();
            }
        );
    });
}
void JwtAuthentication()
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.Authority = "https://the-exchange.eu.auth0.com";
        options.Audience = "https://exchange/api";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });
}
void AddAuthPolicies()
{
    builder.Services.AddAuthorization(options => {
        options.AddPolicy("ReadAccess", policy => policy.RequireClaim("permissions", "read:products"));
        options.AddPolicy("WriteAccess", policy => policy.RequireClaim("permissions", "write:products"));
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
