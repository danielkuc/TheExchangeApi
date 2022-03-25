using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TheExchangeApi.Areas.Admin.Models;
using TheExchangeApi.Areas.Admin.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// get section "ProductStoreDatabaseSettings" from appsettings.json
builder.Services.Configure<ProductStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(ProductStoreDatabaseSettings)));

//dependency injection: whenever IProductStoreDatabaseSettings is required, provide instance of ProductStoreDatabaseSettings class 
builder.Services.AddSingleton<IProductStoreDatabaseSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<ProductStoreDatabaseSettings>>().Value);

//provide IMongoClient with ConnectionString from appsettings.json
builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("ProductStoreDatabaseSettings:ConnectionString")));

// tie interface with it's implementation
builder.Services.AddScoped<IProductService, ProductService>();

// CORS configuration, defined a CORS policy to use with attributes for each controlle/method.
builder.Services.AddCors(options =>
{
    options.AddPolicy("myFrontendPolicy",
        builder =>
        {
            builder.WithOrigins("https://exchange-shop.netlify.app", "http://localhost:3000");
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
         c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
         {
             ValidAudience = builder.Configuration["Auth0:Audience"],
             ValidIssuer = $"{builder.Configuration["Auth0:Domain"]}"
         };
     });

//authorisation, makes sure JWT has the required scope
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("todo:read-write", p => p.
        RequireAuthenticatedUser().
        RequireClaim("scope", "todo:read-write"));
});

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
app.UseAuthorization();

app.MapControllers();


app.Run();
