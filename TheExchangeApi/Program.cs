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

RegisterIMongoCollection();

CorsConfig();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
RegisterHttpAccessor();
builder.Services.AddEndpointsApiExplorer();

RegisterSwagger();

JwtAuthentication();

AddAuthPolicies();

RegisterSession();

void RegisterHttpAccessor()
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
        options.AddPolicy("WriteAccess", policy => policy.RequireClaim("permissions", "write:products"));
    });
}
void RegisterSwagger()
{
    builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.FullName));
}
void RegisterIMongoCollection()
{
    var client = new MongoClient(builder.Configuration.GetValue<string>("ProductDatabase:ConnectionString"));
    var database = client.GetDatabase(builder.Configuration.GetValue<string>("ProductDatabase:DatabaseName"));
    var productCollection = database.GetCollection<Product>(builder.Configuration.GetValue<string>("ProductDatabase:ProductsCollectionName"));
    var cartsCollection = database.GetCollection<ShoppingCart>(builder.Configuration.GetValue<string>("ProductDatabase:CartsCollectionName"));

    builder.Services.AddSingleton(productCollection);
    builder.Services.AddSingleton(cartsCollection);
}
void RegisterSession()
{
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(10);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
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
app.UseSession();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
