using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Infrastructure.Serialization
{
    public static class JsonOptionsExtensions
    {
        public static IMvcBuilder AddDomainConverters(this IMvcBuilder builder)
        {
            return builder.AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                opts.JsonSerializerOptions.Converters.Add(new FullNameJsonConverter());
                opts.JsonSerializerOptions.Converters.Add(new StudentInfoJsonConverter());
            });
        }
    }
}
