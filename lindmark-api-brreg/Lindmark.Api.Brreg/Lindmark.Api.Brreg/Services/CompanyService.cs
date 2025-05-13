using System.Collections.Concurrent;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Lindmark.Api.Brreg.Models;
using Lindmark.Api.Brreg.Repository;

namespace Lindmark.Api.Brreg.Services;

public class CompanyService(ICompanyClient companyClient) : ICompanyService
{
    public async Task<BrregImportReport> ImportCompanies(string? outputPath, string inputPath = "firmaer.csv")
    {

        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };

        List<CompanyInput> companiesInput = [];
        using (var reader = new StreamReader(inputPath))
        using (var csv = new CsvReader(reader, config))
        {
            companiesInput = csv.GetRecords<CompanyInput>().ToList();
        }

        // Remove by duplicate orgnr. Might be better to count and show in the report?
        companiesInput = companiesInput.GroupBy(x => x.OrgNr).Select(x => x.First()).ToList();

        var companies = new ConcurrentBag<Company>();

        await Parallel.ForEachAsync(companiesInput, async (company, token) =>
        {
            var result = await companyClient.GetCompany(company, token);
            companies.Add(result);
        });

        if (outputPath != null)
        {
            using (var writer = new StreamWriter(outputPath))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteHeader<Company>();
                await csv.NextRecordAsync();
                await csv.WriteRecordsAsync(companies.ToList());
            }
        }
        
        var report = new BrregImportReport
        {
            Total = companies.Count,
            StatusAktiv = companies.Count(x => x.Status == Status.Aktiv),
            StatusFeil = companies.Count(x => x.Status == Status.Feil),
            StatusUnderAvvikling = companies.Count(x => x.Status == Status.UnderAvvikling),
            StatusKonkurs = companies.Count(x => x.Status == Status.Konkurs),
            StatusSlettet = companies.Count(x => x.Status == Status.Slettet),
        };

        return report;
    }
    
    public CompanySummary GetCompanySummary(List<Company> companies)
    {
        var total = companies.Count;
        var statusCount = new CompanyStatusCount
        {
            Aktiv = companies.Count(c => c.Status == Status.Aktiv),
            UnderAvvikling = companies.Count(c => c.Status == Status.UnderAvvikling),
            Konkurs = companies.Count(c => c.Status == Status.Konkurs),
            Slettet = companies.Count(c => c.Status == Status.Slettet),
            Feil = companies.Count(c => c.Status == Status.Feil),
        };

        var employeeDistribution = new CompanyEmployeeDistribution
        {
            ZeroOrNull = companies.Count(c => c.NumberOfEmployees is 0 or null),
            OneToNine = companies.Count(c => c.NumberOfEmployees is >= 1 and <= 9),
            TenToFortyNine = companies.Count(c => c.NumberOfEmployees is >= 10 and <= 49),
            MoreThanFifty = companies.Count(c => c.NumberOfEmployees >= 50),
        };


        var orgTypeGroups = companies
            .GroupBy(x => x.OrganizationTypeCode ?? "Unknown")
            .Select(x => new OrganizationTypeDistribution
            {
                Code = x.Key,
                Percentage = Math.Round((double)x.Count() / total * 100, 2)
            })
            .ToList();

        var organizationTypeDistribution = new CompanyOrganizationTypeDistribution
        {
            OrganizationTypeDistribution = orgTypeGroups
        };

        return new CompanySummary
        {
            StatusCount = statusCount,
            EmployeeDistribution = employeeDistribution,
            OrganizationTypeDistribution = organizationTypeDistribution
        };
    }
    
    public List<Company> GetCompanies()
    {
        const string inputPath = "firmaer_output.csv";
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };

        List<Company> companyOutput;
        using (var reader = new StreamReader(inputPath))
        using (var csv = new CsvReader(reader, config))
        {
            // Setting empty string as null
            // See https://stackoverflow.com/a/60158486
            csv.Context.TypeConverterOptionsCache
                .GetOptions<string>()
                .NullValues.Add("");

            companyOutput = csv.GetRecords<Company>().ToList();
        }
        return companyOutput;
    }
}