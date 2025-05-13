using Lindmark.Api.Brreg.Models;

namespace Lindmark.Api.Brreg.Repository;

public class CompanyClient(HttpClient httpClient, ILogger<CompanyClient> logger) : ICompanyClient
{
    public async Task<Company> GetCompany(CompanyInput company, CancellationToken token)
    {
        try
        {
            var response = await httpClient.GetAsync($"enheter/{company.OrgNr}", token);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<BrregResponse>(token);

                if (content == null)
                    return new Company
                    {
                        OrgNr = company.OrgNr,
                        Name = company.Name,
                        Status = Status.Feil
                    };
                
                var status = Utils.GetStatusFromContent(content);

                return new Company
                {
                    OrgNr = content.OrgNr,
                    Name = content.Name,
                    Status = status,
                    NumberOfEmployees = content.NumberOfEmployees,
                    OrganizationTypeCode = content.OrganizationType.Code,
                    IndustryCode = content.IndustryCode?.Code
                };
            }
            // From brreg documentation it says that 410 is "Fjernet av juridiske årsaker"
            // with the field slettedato set.
            if ((int)response.StatusCode == 410)
            {
                var content = await response.Content.ReadFromJsonAsync<BrregDeletedResponse>(token);

                return new Company
                {
                    OrgNr = content.OrgNr,
                    // "410 - Fjernet av juridiske årsaker" does not include name
                    Name = company.Name,
                    Status = Status.Slettet
                };
            }

            return new Company
            {
                OrgNr = company.OrgNr,
                Name = company.Name,
                Status = Status.Feil
            };
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to fetch orgnr: {Org}", company.OrgNr);

            return new Company
            {
                OrgNr = company.OrgNr,
                Name = company.Name,
                Status = Status.Feil
            };
        }
    }
}