using FluentValidation;
using ZadatakV2.Dto.Models;
using ZadatakV2.WebApi.FluentValidation;

namespace ZadatakV2.WebApi.StartupExtensions
{
    public static class ConfigureValidationExtension
    {
        public static void ConfigureValidation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IValidator<AddStudentRequest>, AddStudentRequestValidator>();
        }
    }
}
