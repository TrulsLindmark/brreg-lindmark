using Lindmark.Api.Brreg.Models;

namespace Lindmark.Api.Brreg;

public static class Utils
{
    public static Status GetStatusFromContent(BrregResponse content)
    {
        if (content.DeletionDate != null)
        {
            return Status.Slettet;
        }

        if (content.UnderLiquidation == true || content.ForcedLiquidationOrDissolution == true)
        {
            return Status.UnderAvvikling;
        }

        if (content.Bankrupt == true)
        {
            return Status.Konkurs;
        }

        return Status.Aktiv;
    }
}