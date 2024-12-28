using Microsoft.AspNetCore.Mvc;

namespace SupportTicketService.APIs;

[ApiController()]
public class CustomerCompaniesController : CustomerCompaniesControllerBase
{
    public CustomerCompaniesController(ICustomerCompaniesService service)
        : base(service) { }
}
