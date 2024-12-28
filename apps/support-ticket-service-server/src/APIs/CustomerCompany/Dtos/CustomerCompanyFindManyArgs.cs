using Microsoft.AspNetCore.Mvc;
using SupportTicketService.APIs.Common;
using SupportTicketService.Infrastructure.Models;

namespace SupportTicketService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerCompanyFindManyArgs
    : FindManyInput<CustomerCompany, CustomerCompanyWhereInput> { }
