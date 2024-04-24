using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
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
    services.AddSwaggerGen();    
    services.AddAutoMapper(typeof(UserProfile));

    services.AddScoped<IUserRepository, UserRepository>();    

    services.AddScoped<IPasswordHasher, PasswordHasher>();
    services.AddScoped<IJwtProvider, JwtProvider>();

    services.AddScoped<IAuthService, AuthService>();

    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
}

//void RegisterRepositories(IServiceCollection services)
//{

//}

//void RegisterServices(IServiceCollection services)
//{

//}



void ConfigureApp(WebApplication app)
{    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();    
}


