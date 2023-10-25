using Microsoft.Extensions.DependencyInjection;

namespace WebUI;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}
