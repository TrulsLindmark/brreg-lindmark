using Lindmark.Api.Brreg.Models;

namespace Lindmark.Api.Brreg.Services;

public interface ICompanyService
{
    Task<BrregImportReport> ImportCompanies(string outputPath, string inputPath = "firmaer.csv");

    List<Company> GetCompanies();
    
    CompanySummary GetCompanySummary(List<Company> companies);
}