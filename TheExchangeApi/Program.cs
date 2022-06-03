using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using TheExchangeApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using FluentValidation;
using TheExchangeApi.PipelineBehaviours;

var builder = WebApplication.CreateBuilder(args);

RegisterMediatR();

RegisterValidators();

InjectIMongoCollection();

CorsConfig();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
RegisterAndInjectHttpAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

OverrideSwaggerSchemaIdGeneration();

JwtAuthentication();

AddAuthPolicies();

void RegisterAndInjectHttpAccessor()
{
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
}
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
        options.Authority = builder.Configuration.GetValue<string>("Auth0:Authority");
        options.Audience = builder.Configuration.GetValue<string>("Auth0:Audience");
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
    });
}
void OverrideSwaggerSchemaIdGeneration()
{
    builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.FullName));
}
void InjectIMongoCollection()
{
    builder.Services.AddSingleton(singleton =>
    new MongoClient(builder.Configuration.GetValue<string>("ProductDatabase:ConnectionString"))
        .GetDatabase(builder.Configuration.GetValue<string>("ProductDatabase:DatabaseName"))
        .GetCollection<Product>(builder.Configuration.GetValue<string>("ProductDatabase:ProductsCollectionName"))

    );
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
