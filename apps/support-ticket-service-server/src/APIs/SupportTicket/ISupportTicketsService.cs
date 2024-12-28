using SupportTicketService.APIs.Common;
using SupportTicketService.APIs.Dtos;

namespace SupportTicketService.APIs;

public interface ISupportTicketsService
{
    /// <summary>
    /// Create one Support Ticket
    /// </summary>
    public Task<SupportTicket> CreateSupportTicket(SupportTicketCreateInput supportticket);

    /// <summary>
    /// Delete one Support Ticket
    /// </summary>
    public Task DeleteSupportTicket(SupportTicketWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Support Tickets
    /// </summary>
    public Task<List<SupportTicket>> SupportTickets(SupportTicketFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Support Ticket records
    /// </summary>
    public Task<MetadataDto> SupportTicketsMeta(SupportTicketFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Support Ticket
    /// </summary>
    public Task<SupportTicket> SupportTicket(SupportTicketWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Support Ticket
    /// </summary>
    public Task UpdateSupportTicket(
        SupportTicketWhereUniqueInput uniqueId,
        SupportTicketUpdateInput updateDto
    );

    /// <summary>
    /// Get a CustomerCompany record for Support Ticket
    /// </summary>
    public Task<CustomerCompany> GetCustomerCompany(SupportTicketWhereUniqueInput uniqueId);
}
