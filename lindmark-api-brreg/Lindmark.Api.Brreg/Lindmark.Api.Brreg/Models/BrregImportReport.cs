namespace Lindmark.Api.Brreg.Models;

public class BrregImportReport
{
    public int Total { get; set; }
    public int StatusAktiv { get; set; }
    public int StatusUnderAvvikling { get; set; }
    public int StatusKonkurs { get; set; }
    public int StatusSlettet { get; set; }
    public int StatusFeil { get; set; }
}