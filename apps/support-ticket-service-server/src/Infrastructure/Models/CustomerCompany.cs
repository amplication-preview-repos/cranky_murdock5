using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportTicketService.Infrastructure.Models;

[Table("CustomerCompanies")]
public class CustomerCompanyDbModel
{
    [Range(-999999999, 999999999)]
    public int? CompanyId { get; set; }

    [StringLength(1000)]
    public string? CompanyName { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<SupportTicketDbModel>? SupportTickets { get; set; } =
        new List<SupportTicketDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
