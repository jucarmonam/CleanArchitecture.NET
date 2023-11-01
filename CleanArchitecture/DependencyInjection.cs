using WebUI.Filters;

namespace WebUI;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            //Class to handle possible unhandled exceptions
            options.Filters.Add<ApiExceptionFilterAttribute>();
        });
        return services;
    }
}
