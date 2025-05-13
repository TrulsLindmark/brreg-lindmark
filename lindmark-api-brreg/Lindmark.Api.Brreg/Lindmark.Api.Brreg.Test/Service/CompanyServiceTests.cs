using FluentAssertions;
using Lindmark.Api.Brreg.Models;
using Lindmark.Api.Brreg.Repository;
using Lindmark.Api.Brreg.Services;
using Moq;

namespace Lindmark.Api.Brreg.Test;

public class CompanyServiceTests
{
    private readonly Mock<ICompanyClient> _companyClientMock;
    private readonly CompanyService _companyService;

    public CompanyServiceTests()
    {
        _companyClientMock = new Mock<ICompanyClient>();
        _companyService = new CompanyService(_companyClientMock.Object);
    }
    

    // TODO(tl): Add more tests for the CompanyService class, especially for the ImportCompanies method.
    [Fact]
    public async Task ImportCompanies_ShouldHandleDuplicateOrgNumbers()
    {
        var csvContent = """
                         OrgNr;FirmaNavn
                         Company A;111111111
                         Company A;111111111
                         """;

        var inputPath = TestUtils.CreateTempCsvFile(csvContent);

        try
        {
            _companyClientMock.Setup(x => x.GetCompany(It.IsAny<CompanyInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Company
                {
                    OrgNr = "111111111",
                    Name = "Company A",
                    Status = Status.Aktiv
                });

            var report = await _companyService.ImportCompanies(null, inputPath);

            report.Total.Should().Be(1);
            report.StatusAktiv.Should().Be(1);
        }
        finally
        {
            File.Delete(inputPath);
        }
    }


    [Fact]
    public void GetCompanySummary_HandlesNullOrganizationTypeCode()
    {
        var companies = new List<Company>
        {
            new Company
            {
                OrgNr = "111111111",
                Name = "Company A",
                Status = Status.Aktiv,
                NumberOfEmployees = 10,
                OrganizationTypeCode = null
            }
        };
        

        var result = _companyService.GetCompanySummary(companies);

        result.StatusCount.Aktiv.Should().Be(1);
        result.EmployeeDistribution.TenToFortyNine.Should().Be(1);
        result.OrganizationTypeDistribution.OrganizationTypeDistribution.First().Code.Should().Be("Unknown");
    }
}