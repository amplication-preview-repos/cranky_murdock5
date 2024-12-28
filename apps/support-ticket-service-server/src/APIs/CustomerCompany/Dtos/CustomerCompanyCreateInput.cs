namespace SupportTicketService.APIs.Dtos;

public class CustomerCompanyCreateInput
{
    public int? CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public List<SupportTicket>? SupportTickets { get; set; }

    public DateTime UpdatedAt { get; set; }
}
