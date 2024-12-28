using SupportTicketService.Infrastructure;

namespace SupportTicketService.APIs;

public class CustomerCompaniesService : CustomerCompaniesServiceBase
{
    public CustomerCompaniesService(SupportTicketServiceDbContext context)
        : base(context) { }
}
