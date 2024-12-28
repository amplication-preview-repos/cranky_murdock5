using SupportTicketService.APIs.Dtos;
using SupportTicketService.Infrastructure.Models;

namespace SupportTicketService.APIs.Extensions;

public static class CustomerCompaniesExtensions
{
    public static CustomerCompany ToDto(this CustomerCompanyDbModel model)
    {
        return new CustomerCompany
        {
            CompanyId = model.CompanyId,
            CompanyName = model.CompanyName,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            SupportTickets = model.SupportTickets?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CustomerCompanyDbModel ToModel(
        this CustomerCompanyUpdateInput updateDto,
        CustomerCompanyWhereUniqueInput uniqueId
    )
    {
        var customerCompany = new CustomerCompanyDbModel
        {
            Id = uniqueId.Id,
            CompanyId = updateDto.CompanyId,
            CompanyName = updateDto.CompanyName
        };

        if (updateDto.CreatedAt != null)
        {
            customerCompany.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            customerCompany.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return customerCompany;
    }
}
