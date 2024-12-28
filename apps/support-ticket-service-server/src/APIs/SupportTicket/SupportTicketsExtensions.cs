using SupportTicketService.APIs.Dtos;
using SupportTicketService.Infrastructure.Models;

namespace SupportTicketService.APIs.Extensions;

public static class SupportTicketsExtensions
{
    public static SupportTicket ToDto(this SupportTicketDbModel model)
    {
        return new SupportTicket
        {
            CreatedAt = model.CreatedAt,
            CustomerCompany = model.CustomerCompanyId,
            GeneralNotes = model.GeneralNotes,
            Id = model.Id,
            TicketNumber = model.TicketNumber,
            UpdatedAt = model.UpdatedAt,
            WorkflowStep = model.WorkflowStep,
        };
    }

    public static SupportTicketDbModel ToModel(
        this SupportTicketUpdateInput updateDto,
        SupportTicketWhereUniqueInput uniqueId
    )
    {
        var supportTicket = new SupportTicketDbModel
        {
            Id = uniqueId.Id,
            GeneralNotes = updateDto.GeneralNotes,
            TicketNumber = updateDto.TicketNumber,
            WorkflowStep = updateDto.WorkflowStep
        };

        if (updateDto.CreatedAt != null)
        {
            supportTicket.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.CustomerCompany != null)
        {
            supportTicket.CustomerCompanyId = updateDto.CustomerCompany;
        }
        if (updateDto.UpdatedAt != null)
        {
            supportTicket.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return supportTicket;
    }
}
