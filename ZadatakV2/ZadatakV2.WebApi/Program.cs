using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

    services.AddScoped<IPasswordHasher, PasswordHasher>();

    services.AddScoped<IJwtProvider, JwtProvider>();
}

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


