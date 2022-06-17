using FluentValidation;
using MediatR;
using MediatrExample.API.CustomExtensions;
using MediatrExample.CQRS.User.GetAllUser;
using MediatrExample.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTTokenOptions:SecretKey"])),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero //Skew default 300ms
        };
    });

builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetAssembly(typeof(GetAllUserRequest)));
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMyServiceLifeCycles();
builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(GetAllUserRequestValidator)));
builder.Services.ConfigureMyOptionModels(Configuration);

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(Configuration.GetConnectionString("NpgSQLConnection"), npsqlOpt =>
    {
        npsqlOpt.CommandTimeout(150);
        npsqlOpt.MigrationsAssembly("MediatrExample.Data");
    });
});
// Above Code for "timestamp with time zone' literal cannot be generated for Local DateTime: a UTC DateTime is required" error solution
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddCors(options => { options.AddPolicy("AllowOrigin", policy => policy.AllowAnyOrigin()); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Mediatr Example",
        Description = "CQRS with Mediatr, FluentValidation, EF Core, ELK + SeriLog",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger Config
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Mediatr Example v1");
    });
}

app.UseMyCustomMiddleware();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Server is running...");

app.Run();
