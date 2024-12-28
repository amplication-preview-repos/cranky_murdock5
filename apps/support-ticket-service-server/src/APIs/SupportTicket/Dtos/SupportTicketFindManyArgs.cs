using Microsoft.AspNetCore.Mvc;
using SupportTicketService.APIs.Common;
using SupportTicketService.Infrastructure.Models;

namespace SupportTicketService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class SupportTicketFindManyArgs : FindManyInput<SupportTicket, SupportTicketWhereInput> { }
