using Microsoft.EntityFrameworkCore;
using SupportTicketService.APIs;
using SupportTicketService.APIs.Common;
using SupportTicketService.APIs.Dtos;
using SupportTicketService.APIs.Errors;
using SupportTicketService.APIs.Extensions;
using SupportTicketService.Infrastructure;
using SupportTicketService.Infrastructure.Models;

namespace SupportTicketService.APIs;

public abstract class SupportTicketsServiceBase : ISupportTicketsService
{
    protected readonly SupportTicketServiceDbContext _context;

    public SupportTicketsServiceBase(SupportTicketServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Support Ticket
    /// </summary>
    public async Task<SupportTicket> CreateSupportTicket(SupportTicketCreateInput createDto)
    {
        var supportTicket = new SupportTicketDbModel
        {
            CreatedAt = createDto.CreatedAt,
            GeneralNotes = createDto.GeneralNotes,
            TicketNumber = createDto.TicketNumber,
            UpdatedAt = createDto.UpdatedAt,
            WorkflowStep = createDto.WorkflowStep
        };

        if (createDto.Id != null)
        {
            supportTicket.Id = createDto.Id;
        }
        if (createDto.CustomerCompany != null)
        {
            supportTicket.CustomerCompany = await _context
                .CustomerCompanies.Where(customerCompany =>
                    createDto.CustomerCompany.Id == customerCompany.Id
                )
                .FirstOrDefaultAsync();
        }

        _context.SupportTickets.Add(supportTicket);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SupportTicketDbModel>(supportTicket.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Support Ticket
    /// </summary>
    public async Task DeleteSupportTicket(SupportTicketWhereUniqueInput uniqueId)
    {
        var supportTicket = await _context.SupportTickets.FindAsync(uniqueId.Id);
        if (supportTicket == null)
        {
            throw new NotFoundException();
        }

        _context.SupportTickets.Remove(supportTicket);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Support Tickets
    /// </summary>
    public async Task<List<SupportTicket>> SupportTickets(SupportTicketFindManyArgs findManyArgs)
    {
        var supportTickets = await _context
            .SupportTickets.Include(x => x.CustomerCompany)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return supportTickets.ConvertAll(supportTicket => supportTicket.ToDto());
    }

    /// <summary>
    /// Meta data about Support Ticket records
    /// </summary>
    public async Task<MetadataDto> SupportTicketsMeta(SupportTicketFindManyArgs findManyArgs)
    {
        var count = await _context.SupportTickets.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Support Ticket
    /// </summary>
    public async Task<SupportTicket> SupportTicket(SupportTicketWhereUniqueInput uniqueId)
    {
        var supportTickets = await this.SupportTickets(
            new SupportTicketFindManyArgs
            {
                Where = new SupportTicketWhereInput { Id = uniqueId.Id }
            }
        );
        var supportTicket = supportTickets.FirstOrDefault();
        if (supportTicket == null)
        {
            throw new NotFoundException();
        }

        return supportTicket;
    }

    /// <summary>
    /// Update one Support Ticket
    /// </summary>
    public async Task UpdateSupportTicket(
        SupportTicketWhereUniqueInput uniqueId,
        SupportTicketUpdateInput updateDto
    )
    {
        var supportTicket = updateDto.ToModel(uniqueId);

        if (updateDto.CustomerCompany != null)
        {
            supportTicket.CustomerCompany = await _context
                .CustomerCompanies.Where(customerCompany =>
                    updateDto.CustomerCompany == customerCompany.Id
                )
                .FirstOrDefaultAsync();
        }

        _context.Entry(supportTicket).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.SupportTickets.Any(e => e.Id == supportTicket.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a CustomerCompany record for Support Ticket
    /// </summary>
    public async Task<CustomerCompany> GetCustomerCompany(SupportTicketWhereUniqueInput uniqueId)
    {
        var supportTicket = await _context
            .SupportTickets.Where(supportTicket => supportTicket.Id == uniqueId.Id)
            .Include(supportTicket => supportTicket.CustomerCompany)
            .FirstOrDefaultAsync();
        if (supportTicket == null)
        {
            throw new NotFoundException();
        }
        return supportTicket.CustomerCompany.ToDto();
    }
}
