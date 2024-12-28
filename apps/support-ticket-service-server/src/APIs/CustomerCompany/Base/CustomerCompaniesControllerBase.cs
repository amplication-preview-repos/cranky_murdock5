using Microsoft.AspNetCore.Mvc;
using SupportTicketService.APIs;
using SupportTicketService.APIs.Common;
using SupportTicketService.APIs.Dtos;
using SupportTicketService.APIs.Errors;

namespace SupportTicketService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomerCompaniesControllerBase : ControllerBase
{
    protected readonly ICustomerCompaniesService _service;

    public CustomerCompaniesControllerBase(ICustomerCompaniesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer Company
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<CustomerCompany>> CreateCustomerCompany(
        CustomerCompanyCreateInput input
    )
    {
        var customerCompany = await _service.CreateCustomerCompany(input);

        return CreatedAtAction(
            nameof(CustomerCompany),
            new { id = customerCompany.Id },
            customerCompany
        );
    }

    /// <summary>
    /// Delete one Customer Company
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCustomerCompany(
        [FromRoute()] CustomerCompanyWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteCustomerCompany(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customer Companies
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<CustomerCompany>>> CustomerCompanies(
        [FromQuery()] CustomerCompanyFindManyArgs filter
    )
    {
        return Ok(await _service.CustomerCompanies(filter));
    }

    /// <summary>
    /// Meta data about Customer Company records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomerCompaniesMeta(
        [FromQuery()] CustomerCompanyFindManyArgs filter
    )
    {
        return Ok(await _service.CustomerCompaniesMeta(filter));
    }

    /// <summary>
    /// Get one Customer Company
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<CustomerCompany>> CustomerCompany(
        [FromRoute()] CustomerCompanyWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.CustomerCompany(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer Company
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCustomerCompany(
        [FromRoute()] CustomerCompanyWhereUniqueInput uniqueId,
        [FromQuery()] CustomerCompanyUpdateInput customerCompanyUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomerCompany(uniqueId, customerCompanyUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Support Tickets records to Customer Company
    /// </summary>
    [HttpPost("{Id}/supportTickets")]
    public async Task<ActionResult> ConnectSupportTickets(
        [FromRoute()] CustomerCompanyWhereUniqueInput uniqueId,
        [FromQuery()] SupportTicketWhereUniqueInput[] supportTicketsId
    )
    {
        try
        {
            await _service.ConnectSupportTickets(uniqueId, supportTicketsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Support Tickets records from Customer Company
    /// </summary>
    [HttpDelete("{Id}/supportTickets")]
    public async Task<ActionResult> DisconnectSupportTickets(
        [FromRoute()] CustomerCompanyWhereUniqueInput uniqueId,
        [FromBody()] SupportTicketWhereUniqueInput[] supportTicketsId
    )
    {
        try
        {
            await _service.DisconnectSupportTickets(uniqueId, supportTicketsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Support Tickets records for Customer Company
    /// </summary>
    [HttpGet("{Id}/supportTickets")]
    public async Task<ActionResult<List<SupportTicket>>> FindSupportTickets(
        [FromRoute()] CustomerCompanyWhereUniqueInput uniqueId,
        [FromQuery()] SupportTicketFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindSupportTickets(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Support Tickets records for Customer Company
    /// </summary>
    [HttpPatch("{Id}/supportTickets")]
    public async Task<ActionResult> UpdateSupportTickets(
        [FromRoute()] CustomerCompanyWhereUniqueInput uniqueId,
        [FromBody()] SupportTicketWhereUniqueInput[] supportTicketsId
    )
    {
        try
        {
            await _service.UpdateSupportTickets(uniqueId, supportTicketsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
