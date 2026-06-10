
using AegisAuth.API.Extensions;
using AegisAuth.API.Services;
using AegisAuth.Application;
using AegisAuth.Application.Behaviors;
using AegisAuth.Application.Common.Interfaces;
using AegisAuth.Infrastructure;
using AegisAuth.Persistence;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace AegisAuth.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddApiServices();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(jwtOptions =>
        {
            jwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidAudiences = builder.Configuration.GetSection("Api:ValidAudiences").Get<string[]>(),
                ValidIssuers = builder.Configuration.GetSection("Api:ValidIssuers").Get<string[]>(),
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };

        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireApiReadScope", policy => policy.RequireClaim("scope", "api.read"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
