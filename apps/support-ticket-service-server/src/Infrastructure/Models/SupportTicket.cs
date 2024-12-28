using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupportTicketService.Core.Enums;

namespace SupportTicketService.Infrastructure.Models;

[Table("SupportTickets")]
public class SupportTicketDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerCompanyId { get; set; }

    [ForeignKey(nameof(CustomerCompanyId))]
    public CustomerCompanyDbModel? CustomerCompany { get; set; } = null;

    [StringLength(1000)]
    public string? GeneralNotes { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Range(-999999999, 999999999)]
    public int? TicketNumber { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public WorkflowStepEnum? WorkflowStep { get; set; }
}
