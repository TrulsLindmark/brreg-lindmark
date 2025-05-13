namespace Lindmark.Api.Brreg.Test;

public static class TestUtils
{
    public static string CreateTempCsvFile(string content)
    {
        var tempPath = Path.GetTempFileName();
        File.WriteAllText(tempPath, content);
        return tempPath;
    }
}