using TechTalk.SpecFlow;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.Configuration;

public sealed class ScreenShotConfiguration
{
    public string GenerateScreenShotFileName(string featureName, string scenarioName, string extension = "jpg")
    {
        var featureSubDir = SanitizeFileName(Truncate(featureName));
        var scenarioSubDir = SanitizeFileName(Truncate(scenarioName));

        var fileName = Path.Combine(featureSubDir.ToString(), scenarioSubDir.ToString(),
            $"{Guid.NewGuid()}.{extension}");
        return fileName;

        ReadOnlySpan<char> SanitizeFileName(ReadOnlySpan<char> name)
        {
            var invalidFileNameChars = Path.GetInvalidFileNameChars();
            Span<char> sanitized = stackalloc char[name.Length];
            var index = 0;

            foreach (var c in name)
            {
                if (!invalidFileNameChars.AsSpan().Contains(c))
                {
                    sanitized[index++] = c == ' ' ? '_' : c;
                }
            }

            return sanitized[..index].ToString();
        }

        ReadOnlySpan<char> Truncate(ReadOnlySpan<char> s, int maxLength = 50) =>
            s.Length <= maxLength ? s : s[..maxLength];
    }

    public string GetScreenShotPath(string screenShotFileName)
    {
        ThrowIfNull(screenShotFileName);

        var screenShotDir = GetScreenShotDir();
        return Path.Combine(screenShotDir, screenShotFileName);
    }

    public string GetAttachmentUrl(string screenShotFileName)
    {
        ThrowIfNull(screenShotFileName);

        var attachmentTemplate = Environment.GetEnvironmentVariable(EnvVariables.AttachmentTemplate) ??
                                 GetScreenShotDir() + "\\{0}";
        return string.Format(attachmentTemplate, screenShotFileName);
    }

    private string GetScreenShotDir() =>
        Environment.GetEnvironmentVariable(EnvVariables.ScreenShotPath) ??
        Path.Combine(Directory.GetCurrentDirectory(), "screenshots");

    public void ClearScreenShotDir(ISpecFlowOutputHelper specFlowOutputHelper)
    {
        var screenshotDir = GetScreenShotDir();
        if (!Directory.Exists(screenshotDir)) return;

        try
        {
            Directory.Delete(screenshotDir, true);
            specFlowOutputHelper.WriteLine($"Screenshots deleted successfully from: {screenshotDir}");
        }
        catch (Exception ex)
        {
            specFlowOutputHelper.WriteLine($"Error deleting screenshots: {ex.Message}");
        }
    }

    private static void ThrowIfNull(string argument)
        => ArgumentNullException.ThrowIfNull(argument: argument);
}