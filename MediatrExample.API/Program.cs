using FluentValidation;
using MediatR;
using MediatrExample.API.CustomExtensions;
using MediatrExample.API.Logging;
using MediatrExample.CQRS.User.GetAllUser;
using MediatrExample.Data.Context;
using MediatrExample.Service.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using StackExchange.Redis;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.

var basicHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes("elastic:DkIedPPSCb"));

MyLogger.ConfigureSeriLog(Configuration);
builder.Host.UseSerilog();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTTokenOptions:SecretKey"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1), //Skew default 5 minute
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true
        };
        #region JWT Error Handler for Auth
        //opt.Events = new JwtBearerEvents
        //{
        //    OnChallenge = async (context) =>
        //    {
        //        context.HandleResponse();
        //        if (context.AuthenticateFailure != null)
        //        {
        //            context.Response.StatusCode = 401;
        //            var unAuthResponse = GenericResponse<string>.Error(401, "Unauthorized");
        //            await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(unAuthResponse));
        //        }
        //    },
        //    OnAuthenticationFailed = async (context) =>
        //    {
        //        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
        //        {
        //            context.Response.Headers.Add("Token-Expired", "true");
        //        }
        //    }
        //};
        #endregion
    });

builder.Services.AddControllers();

var multiplexer = RedisUtility.ConnectRedis(Configuration);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

builder.Services.AddMediatR(Assembly.GetAssembly(typeof(GetAllUserRequest)));
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMyServiceLifeCycles(Configuration);
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
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                Type = SecuritySchemeType.OAuth2,
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (Configuration["Environment:EnvironmentName"] == "dev")
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

app.MapGet("/", () =>
{
    using (var scope = app.Services.CreateAsyncScope())
    {
        var service = scope.ServiceProvider;
        var logger = service.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Example Log Date: {0}", DateTime.Now);
    }
    return "Server is Running...";
});

#region Auto Run Pending Migration
using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }
}
#endregion

app.Run();
