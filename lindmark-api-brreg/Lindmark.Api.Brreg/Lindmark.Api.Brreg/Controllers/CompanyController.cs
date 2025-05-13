using Lindmark.Api.Brreg.Models;
using Lindmark.Api.Brreg.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lindmark.Api.Brreg.Controllers;

[ApiController]
public class CompanyController(ICompanyService companyService, ILogger<CompanyController> logger) : ControllerBase
{
    // TODO(tl): Add documentation for the API

    [HttpGet]
    [Route("import")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BrregImportReport), 200)]
    public async Task<ActionResult<BrregImportReport>> ImportCompanies()
    {
        try
        {
            var report = await companyService.ImportCompanies("firmaer_output.csv");
            return Ok(report);
        }
        catch (Exception e)
        {
            logger.LogError("Failed import: {Message}", e.Message);
            return StatusCode(500);
        }
    }
    
    [HttpGet]
    [Route("api/companies/summary")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CompanySummary), 200)]
    public ActionResult<CompanySummary> GetCompaniesSummary()
    {
        try
        {
            var companies = companyService.GetCompanies();
            var companySummary = companyService.GetCompanySummary(companies);
            return Ok(companySummary);
        }
        catch (Exception e)
        {
            logger.LogError("Failed to get summary with message {Message}", e.Message);
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("api/companies")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Company>), 200)]
    public ActionResult<List<Company>> GetCompanies()
    {
        try
        {
            var companies = companyService.GetCompanies();
            return Ok(companies);
        }
        catch (Exception e)
        {
            logger.LogError("Failed to get companies message {Message}", e.Message);
            return StatusCode(500);
        }
    }
}