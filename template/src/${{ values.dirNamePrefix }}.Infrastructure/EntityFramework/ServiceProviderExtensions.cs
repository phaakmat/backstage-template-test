using Microsoft.Extensions.DependencyInjection;

namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;

public static class ServiceProviderExtensions
{
    public static IServiceProvider EnsureEntityFrameworkDbCreated(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<IDbContext>().EnsureCreated();
        }

        return serviceProvider;
    }
}