using FluentAssertions;
using Lindmark.Api.Brreg.Models;

namespace Lindmark.Api.Brreg.Test;

public class UtilsTest
{
    [Fact]
    public void ReturnsSlettet_WhenDeletionDateIsNotNull()
    {
        var content = new BrregResponse
        {
            OrgNr = "123456789", Name = "Company", OrganizationType = new OrganizationType { Code = "AS" },
            DeletionDate = "deleted"
        };
        var result = Utils.GetStatusFromContent(content);
        result.Should().Be(Status.Slettet);
    }

    [Fact]
    public void ReturnsUnderAvvikling_WhenUnderLiquidationIsTrue()
    {
        var content = new BrregResponse
        {
            OrgNr = "123456789", Name = "Company", OrganizationType = new OrganizationType { Code = "AS" },
            UnderLiquidation = true
        };
        var result = Utils.GetStatusFromContent(content);
        result.Should().Be(Status.UnderAvvikling);
    }

    [Fact]
    public void ReturnsUnderAvvikling_WhenForcedLiquidationOrDissolutionIsTrue()
    {
        var content = new BrregResponse
        {
            OrgNr = "123456789", Name = "Company", OrganizationType = new OrganizationType { Code = "AS" },
            ForcedLiquidationOrDissolution = true
        };
        var result = Utils.GetStatusFromContent(content);
        result.Should().Be(Status.UnderAvvikling);
    }

    [Fact]
    public void ReturnsKonkurs_WhenBankruptIsTrue()
    {
        var content = new BrregResponse
        {
            OrgNr = "123456789", Name = "Company", OrganizationType = new OrganizationType { Code = "AS" },
            Bankrupt = true
        };
        var result = Utils.GetStatusFromContent(content);
        result.Should().Be(Status.Konkurs);
    }

    [Fact]
    public void ReturnsAktiv_WhenAllConditionsAreFalseOrNull()
    {
        var content = new BrregResponse
        {
            OrgNr = "123456789", Name = "Company", OrganizationType = new OrganizationType { Code = "AS" },
            DeletionDate = null,
            UnderLiquidation = false,
            ForcedLiquidationOrDissolution = false,
            Bankrupt = false
        };
        var result = Utils.GetStatusFromContent(content);
        result.Should().Be(Status.Aktiv);
    }
}