using SupportTicketService.APIs;

namespace SupportTicketService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerCompaniesService, CustomerCompaniesService>();
        services.AddScoped<ISupportTicketsService, SupportTicketsService>();
    }
}
