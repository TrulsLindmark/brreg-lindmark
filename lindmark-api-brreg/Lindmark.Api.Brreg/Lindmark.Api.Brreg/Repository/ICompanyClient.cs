using Lindmark.Api.Brreg.Models;

namespace Lindmark.Api.Brreg.Repository;

public interface ICompanyClient
{
    Task<Company> GetCompany(CompanyInput company, CancellationToken token);
}