using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text;
using ZadatakV2.Domain.Repositories;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Repositories;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Service.Services;
using ZadatakV2.WebApi;
using ZadatakV2.WebApi.MappingProfiles;
using ZadatakV2.WebApi.Middlewares;
using ZadatakV2.WebApi.Services;


[assembly: RootNamespace("ZadatakV2.Shared.Resources")]

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);
WebApplication app = builder.Build();
ConfigureApp(app);
app.Run();

void ConfigureServices(IServiceCollection services)
{
    
    ConfigureLocalization(services);

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
    
    services.AddFluentValidationAutoValidation();
    services.AddFluentValidationClientsideAdapters();    
    services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

    services.AddEndpointsApiExplorer();
    
    ConfigureSwagger(services);

    services.AddExceptionHandler<AppExceptionHandler>();

    services.AddAutoMapper(typeof(UserProfile));

    services.AddScoped<IUserRepository, UserRepository>();    
    services.AddScoped<IStudentRepository, StudentRepository>();
    services.AddScoped<ISubjectRepository, SubjectRepository>();
    services.AddScoped<IGradeRepository, GradeRepository>();
    services.AddScoped<IStudentExamRepository, StudentExamRepository>();

    services.AddScoped<IPasswordHasher, PasswordHasher>();
    services.AddScoped<IJwtProvider, JwtProvider>();

    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IStudentService, StudentService>();
    services.AddScoped<ISubjectService, SubjectService>();
    services.AddScoped<IGradeService, GradeService>();
    services.AddScoped<IStudentExamService, StudentExamService>();

    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
}

void ConfigureLocalization(IServiceCollection services)
{
    services.AddLocalization();
    var defaultCulture = "en-US";

    var supportedCultures = new[]
    {
        new CultureInfo(defaultCulture),
        new CultureInfo("sr-Latn-RS")
    };

    services.Configure<RequestLocalizationOptions>(options => 
    {
        options.DefaultRequestCulture = new RequestCulture(defaultCulture);
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
        //options.ApplyCurrentCultureToResponseHeaders = true;
        options.RequestCultureProviders = new List<IRequestCultureProvider>()
        {
            new AcceptLanguageHeaderRequestCultureProvider()
        };
    });
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
    var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
    app.UseRequestLocalization(localizeOptions.Value);  

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zadatak");
        });
    }

    app.UseExceptionHandler(_ => { });

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();    

    app.MapControllers();    
}


