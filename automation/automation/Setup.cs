using automation;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace automation;

public class Setup : IDisposable
{
    private readonly IConfiguration _configuration;
    private IBrowser? _browser;

    public Setup()
    {
        var builder = new ConfigurationBuilder();

        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<Setup>(true)
            .AddEnvironmentVariables();

        _configuration = builder.Build();

        EnvironmentDetails = _configuration.GetSection("EnvironmentDetails").Get<EnvironmentDetails>()!;
    }

    public EnvironmentDetails EnvironmentDetails { get; }

    public void Dispose()
    {
        _browser?.CloseAsync();
    }

    public async Task<IPage> InitializePlaywright()
    {
        IPlaywright playwright = await Playwright.CreateAsync();
        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = false });

        return await _browser.NewPageAsync();
    }
}