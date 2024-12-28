using Microsoft.EntityFrameworkCore;
using SupportTicketService.APIs;
using SupportTicketService.APIs.Common;
using SupportTicketService.APIs.Dtos;
using SupportTicketService.APIs.Errors;
using SupportTicketService.APIs.Extensions;
using SupportTicketService.Infrastructure;
using SupportTicketService.Infrastructure.Models;

namespace SupportTicketService.APIs;

public abstract class CustomerCompaniesServiceBase : ICustomerCompaniesService
{
    protected readonly SupportTicketServiceDbContext _context;

    public CustomerCompaniesServiceBase(SupportTicketServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Customer Company
    /// </summary>
    public async Task<CustomerCompany> CreateCustomerCompany(CustomerCompanyCreateInput createDto)
    {
        var customerCompany = new CustomerCompanyDbModel
        {
            CompanyId = createDto.CompanyId,
            CompanyName = createDto.CompanyName,
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            customerCompany.Id = createDto.Id;
        }
        if (createDto.SupportTickets != null)
        {
            customerCompany.SupportTickets = await _context
                .SupportTickets.Where(supportTicket =>
                    createDto.SupportTickets.Select(t => t.Id).Contains(supportTicket.Id)
                )
                .ToListAsync();
        }

        _context.CustomerCompanies.Add(customerCompany);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CustomerCompanyDbModel>(customerCompany.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Customer Company
    /// </summary>
    public async Task DeleteCustomerCompany(CustomerCompanyWhereUniqueInput uniqueId)
    {
        var customerCompany = await _context.CustomerCompanies.FindAsync(uniqueId.Id);
        if (customerCompany == null)
        {
            throw new NotFoundException();
        }

        _context.CustomerCompanies.Remove(customerCompany);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Customer Companies
    /// </summary>
    public async Task<List<CustomerCompany>> CustomerCompanies(
        CustomerCompanyFindManyArgs findManyArgs
    )
    {
        var customerCompanies = await _context
            .CustomerCompanies.Include(x => x.SupportTickets)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return customerCompanies.ConvertAll(customerCompany => customerCompany.ToDto());
    }

    /// <summary>
    /// Meta data about Customer Company records
    /// </summary>
    public async Task<MetadataDto> CustomerCompaniesMeta(CustomerCompanyFindManyArgs findManyArgs)
    {
        var count = await _context.CustomerCompanies.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Customer Company
    /// </summary>
    public async Task<CustomerCompany> CustomerCompany(CustomerCompanyWhereUniqueInput uniqueId)
    {
        var customerCompanies = await this.CustomerCompanies(
            new CustomerCompanyFindManyArgs
            {
                Where = new CustomerCompanyWhereInput { Id = uniqueId.Id }
            }
        );
        var customerCompany = customerCompanies.FirstOrDefault();
        if (customerCompany == null)
        {
            throw new NotFoundException();
        }

        return customerCompany;
    }

    /// <summary>
    /// Update one Customer Company
    /// </summary>
    public async Task UpdateCustomerCompany(
        CustomerCompanyWhereUniqueInput uniqueId,
        CustomerCompanyUpdateInput updateDto
    )
    {
        var customerCompany = updateDto.ToModel(uniqueId);

        if (updateDto.SupportTickets != null)
        {
            customerCompany.SupportTickets = await _context
                .SupportTickets.Where(supportTicket =>
                    updateDto.SupportTickets.Select(t => t).Contains(supportTicket.Id)
                )
                .ToListAsync();
        }

        _context.Entry(customerCompany).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.CustomerCompanies.Any(e => e.Id == customerCompany.Id))
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
    /// Connect multiple Support Tickets records to Customer Company
    /// </summary>
    public async Task ConnectSupportTickets(
        CustomerCompanyWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .CustomerCompanies.Include(x => x.SupportTickets)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .SupportTickets.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.SupportTickets);

        foreach (var child in childrenToConnect)
        {
            parent.SupportTickets.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Support Tickets records from Customer Company
    /// </summary>
    public async Task DisconnectSupportTickets(
        CustomerCompanyWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .CustomerCompanies.Include(x => x.SupportTickets)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .SupportTickets.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.SupportTickets?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Support Tickets records for Customer Company
    /// </summary>
    public async Task<List<SupportTicket>> FindSupportTickets(
        CustomerCompanyWhereUniqueInput uniqueId,
        SupportTicketFindManyArgs customerCompanyFindManyArgs
    )
    {
        var supportTickets = await _context
            .SupportTickets.Where(m => m.CustomerCompanyId == uniqueId.Id)
            .ApplyWhere(customerCompanyFindManyArgs.Where)
            .ApplySkip(customerCompanyFindManyArgs.Skip)
            .ApplyTake(customerCompanyFindManyArgs.Take)
            .ApplyOrderBy(customerCompanyFindManyArgs.SortBy)
            .ToListAsync();

        return supportTickets.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Support Tickets records for Customer Company
    /// </summary>
    public async Task UpdateSupportTickets(
        CustomerCompanyWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] childrenIds
    )
    {
        var customerCompany = await _context
            .CustomerCompanies.Include(t => t.SupportTickets)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customerCompany == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .SupportTickets.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customerCompany.SupportTickets = children;
        await _context.SaveChangesAsync();
    }
}
