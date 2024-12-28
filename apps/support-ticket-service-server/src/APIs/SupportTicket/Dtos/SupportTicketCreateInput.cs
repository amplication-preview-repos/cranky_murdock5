using SupportTicketService.Core.Enums;

namespace SupportTicketService.APIs.Dtos;

public class SupportTicketCreateInput
{
    public DateTime CreatedAt { get; set; }

    public CustomerCompany? CustomerCompany { get; set; }

    public string? GeneralNotes { get; set; }

    public string? Id { get; set; }

    public int? TicketNumber { get; set; }

    public DateTime UpdatedAt { get; set; }

    public WorkflowStepEnum? WorkflowStep { get; set; }
}
