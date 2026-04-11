using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PokerApi.Data;
using PokerApi.Dtos;
using PokerApi.Hubs;
using PokerApi.Models;
using PokerApi.Services;
using PokerApi.Services;
using Scalar.AspNetCore;
using TexasHolDemPokerApi.Dtos;
using TexasHolDemPokerApi.Models;
using TexasHolDemPokerApi.Security;
using TexasHolDemPokerApi.Services;
using TexasHolDemPokerApi.Services.Interface;
using TexasHolDemPokerApi.utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Initialize db context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Initialize automapper for model to dto
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Room, RoomDto>();
    cfg.CreateMap<RoomDto, Room>();
    cfg.CreateMap<LoginDto, Login>();
    cfg.CreateMap<Login, LoginDto>();
    cfg.CreateMap<Player, PlayerDto>();
});

//Initialize interface to service
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRoomPlayerService, RoomPlayerService>();
builder.Services.AddSingleton<TokenGenerator>();


builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        //Received bearer token from cookie instead of header
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                // Token depuis cookie (existant)
                ctx.Token = ctx.Request.Cookies["token"];

                // Fallback : token depuis query string pour SignalR
                if (string.IsNullOrEmpty(ctx.Token))
                {
                    var accessToken = ctx.Request.Query["access_token"];
                    var path = ctx.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        ctx.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey("NeedToHaveLongPassphraseForSecurity"u8.ToArray()),
            ValidIssuer = "localhost",
            ValidAudience = "localhost",
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true
        };
    }
 );

builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

// 1. Ajouter le service CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:5173") // ← port de ton app React (Vite)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // indispensable pour les cookies;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(options =>
    {
        options.AddPreferredSecuritySchemes(JwtBearerDefaults.AuthenticationScheme)
        .AddHttpAuthentication(JwtBearerDefaults.AuthenticationScheme, auth =>
        {
            auth.Token = "test.tezt.etzg";
        }).EnablePersistentAuthentication();
    });
    app.MapOpenApi();
}

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.MapHub<RoomHub>("/hubs/rooms");

app.Run();