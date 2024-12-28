namespace SupportTicketService.APIs.Dtos;

public class CustomerCompanyWhereInput
{
    public int? CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public List<string>? SupportTickets { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
