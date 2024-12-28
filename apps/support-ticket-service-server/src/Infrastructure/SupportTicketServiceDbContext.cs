using Microsoft.EntityFrameworkCore;
using SupportTicketService.Infrastructure.Models;

namespace SupportTicketService.Infrastructure;

public class SupportTicketServiceDbContext : DbContext
{
    public SupportTicketServiceDbContext(DbContextOptions<SupportTicketServiceDbContext> options)
        : base(options) { }

    public DbSet<CustomerCompanyDbModel> CustomerCompanies { get; set; }

    public DbSet<SupportTicketDbModel> SupportTickets { get; set; }
}
