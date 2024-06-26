using Microsoft.AspNetCore.Http.Json;
using Syndicate.API;
using Syndicate.API.Middleware;
using Syndicate.Data;
using Syndicate.Services;
using Syndicate.Services.Features.Identity;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                builder.WithOrigins("https://localhost:3000");
            });
    }
);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});


Options.Register(builder.Services, builder.Configuration);
Database.Register(builder.Services, builder.Configuration);
IdentityModule.Register(builder.Services, builder.Configuration);
Services.Register(builder.Services);

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors(o =>
{
    o.AllowAnyHeader();
    o.AllowAnyMethod();
    o.WithOrigins("https://localhost:3000", "https://localhost:3001");
    o.AllowCredentials();
    o.SetPreflightMaxAge(TimeSpan.FromDays(1));
});

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Endpoints.Register(app);

app.Run();

