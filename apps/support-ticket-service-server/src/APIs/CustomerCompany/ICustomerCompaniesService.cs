using SupportTicketService.APIs.Common;
using SupportTicketService.APIs.Dtos;

namespace SupportTicketService.APIs;

public interface ICustomerCompaniesService
{
    /// <summary>
    /// Create one Customer Company
    /// </summary>
    public Task<CustomerCompany> CreateCustomerCompany(CustomerCompanyCreateInput customercompany);

    /// <summary>
    /// Delete one Customer Company
    /// </summary>
    public Task DeleteCustomerCompany(CustomerCompanyWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customer Companies
    /// </summary>
    public Task<List<CustomerCompany>> CustomerCompanies(CustomerCompanyFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Customer Company records
    /// </summary>
    public Task<MetadataDto> CustomerCompaniesMeta(CustomerCompanyFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer Company
    /// </summary>
    public Task<CustomerCompany> CustomerCompany(CustomerCompanyWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer Company
    /// </summary>
    public Task UpdateCustomerCompany(
        CustomerCompanyWhereUniqueInput uniqueId,
        CustomerCompanyUpdateInput updateDto
    );

    /// <summary>
    /// Connect multiple Support Tickets records to Customer Company
    /// </summary>
    public Task ConnectSupportTickets(
        CustomerCompanyWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] supportTicketsId
    );

    /// <summary>
    /// Disconnect multiple Support Tickets records from Customer Company
    /// </summary>
    public Task DisconnectSupportTickets(
        CustomerCompanyWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] supportTicketsId
    );

    /// <summary>
    /// Find multiple Support Tickets records for Customer Company
    /// </summary>
    public Task<List<SupportTicket>> FindSupportTickets(
        CustomerCompanyWhereUniqueInput uniqueId,
        SupportTicketFindManyArgs SupportTicketFindManyArgs
    );

    /// <summary>
    /// Update multiple Support Tickets records for Customer Company
    /// </summary>
    public Task UpdateSupportTickets(
        CustomerCompanyWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] supportTicketsId
    );
}
