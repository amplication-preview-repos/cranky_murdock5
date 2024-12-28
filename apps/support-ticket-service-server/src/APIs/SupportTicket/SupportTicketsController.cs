using Microsoft.AspNetCore.Mvc;

namespace SupportTicketService.APIs;

[ApiController()]
public class SupportTicketsController : SupportTicketsControllerBase
{
    public SupportTicketsController(ISupportTicketsService service)
        : base(service) { }
}
