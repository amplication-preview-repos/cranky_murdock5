using Microsoft.AspNetCore.Mvc;
using SupportTicketService.APIs;
using SupportTicketService.APIs.Common;
using SupportTicketService.APIs.Dtos;
using SupportTicketService.APIs.Errors;

namespace SupportTicketService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SupportTicketsControllerBase : ControllerBase
{
    protected readonly ISupportTicketsService _service;

    public SupportTicketsControllerBase(ISupportTicketsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Support Ticket
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<SupportTicket>> CreateSupportTicket(
        SupportTicketCreateInput input
    )
    {
        var supportTicket = await _service.CreateSupportTicket(input);

        return CreatedAtAction(nameof(SupportTicket), new { id = supportTicket.Id }, supportTicket);
    }

    /// <summary>
    /// Delete one Support Ticket
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteSupportTicket(
        [FromRoute()] SupportTicketWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteSupportTicket(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Support Tickets
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<SupportTicket>>> SupportTickets(
        [FromQuery()] SupportTicketFindManyArgs filter
    )
    {
        return Ok(await _service.SupportTickets(filter));
    }

    /// <summary>
    /// Meta data about Support Ticket records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SupportTicketsMeta(
        [FromQuery()] SupportTicketFindManyArgs filter
    )
    {
        return Ok(await _service.SupportTicketsMeta(filter));
    }

    /// <summary>
    /// Get one Support Ticket
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<SupportTicket>> SupportTicket(
        [FromRoute()] SupportTicketWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.SupportTicket(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Support Ticket
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateSupportTicket(
        [FromRoute()] SupportTicketWhereUniqueInput uniqueId,
        [FromQuery()] SupportTicketUpdateInput supportTicketUpdateDto
    )
    {
        try
        {
            await _service.UpdateSupportTicket(uniqueId, supportTicketUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a CustomerCompany record for Support Ticket
    /// </summary>
    [HttpGet("{Id}/customerCompany")]
    public async Task<ActionResult<List<CustomerCompany>>> GetCustomerCompany(
        [FromRoute()] SupportTicketWhereUniqueInput uniqueId
    )
    {
        var customerCompany = await _service.GetCustomerCompany(uniqueId);
        return Ok(customerCompany);
    }
}
