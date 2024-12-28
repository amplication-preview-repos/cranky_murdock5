using SupportTicketService.Infrastructure;

namespace SupportTicketService.APIs;

public class SupportTicketsService : SupportTicketsServiceBase
{
    public SupportTicketsService(SupportTicketServiceDbContext context)
        : base(context) { }
}
