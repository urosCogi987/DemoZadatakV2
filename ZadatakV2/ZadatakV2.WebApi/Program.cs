using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ZadatakV2.Domain.Repositories;
using ZadatakV2.Persistance.Repositories;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Service.Services;
using ZadatakV2.WebApi;
using ZadatakV2.WebApi.MappingProfiles;
using ZadatakV2.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);
WebApplication app = builder.Build();
ConfigureApp(app);
app.Run();

void ConfigureServices(IServiceCollection services)
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
            };
        });

    services.AddAuthorization();

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    
    ConfigureSwagger(services);

    services.AddAutoMapper(typeof(UserProfile));

    services.AddScoped<IUserRepository, UserRepository>();    

    services.AddScoped<IPasswordHasher, PasswordHasher>();
    services.AddScoped<IJwtProvider, JwtProvider>();

    services.AddScoped<IAuthService, AuthService>();

    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
}

void ConfigureSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "JWTToken_Auth_API",
            Version = "v1"
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
        });
    });
}

void ConfigureApp(WebApplication app)
{    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zadatak");
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();    
}


