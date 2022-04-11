using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TheExchangeApi.Models;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// Add MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);

// get section "ProductDatabase" from appsettings.json
builder.Services.Configure<ProductDatabaseSettings>(
    builder.Configuration.GetSection("ProductDatabase"));

//dependency injection: whenever IProductDatabaseSettings is required, provide instance of ProductDatabaseSettings class 
builder.Services.AddSingleton<IProductDatabaseSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);

//provide IMongoClient with ConnectionString from appsettings.json
builder.Services.AddSingleton<IMongoClient>(singleton =>
    new MongoClient(builder.Configuration.GetValue<string>("ProductDatabase:ConnectionString")));


// CORS configuration, defined a CORS policy to use with attributes for each controlle/method.
builder.Services.AddCors(options =>
{
    options.AddPolicy("myFrontendPolicy",
        policy =>
        {
            policy.WithOrigins(
                "https://exchange-shop.netlify.app",
                "http://localhost:3000")
            .AllowAnyHeader();
            //.AllowAnyMethod();
        }   
    );
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT validation, middleware intercepts and validates received requests
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
     {
         c.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
         c.TokenValidationParameters = new TokenValidationParameters
         {
             ValidAudience = builder.Configuration["Auth0:Audience"],
             ValidIssuer = $"{builder.Configuration["Auth0:Domain"]}"
         };
     });

//authorisation, makes sure JWT has the required scope
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("product:read-write", policy => policy.
//        RequireAuthenticatedUser().
//        RequireClaim("scope", "product:read-write"));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//calling CORS service initialiser
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
