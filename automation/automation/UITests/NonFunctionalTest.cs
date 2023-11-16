using automation.Pages;
using Bogus;
using FluentAssertions;
using Microsoft.Playwright;
using Xunit;

namespace automation.UITests
{
    public class NonFunctionalTest : IClassFixture<Setup>, IAsyncLifetime
    {
        private readonly Setup _startupService;
        private readonly Faker _faker;
        private string _baseUrl = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _adminUrl = string.Empty;
        private LoginPage _loginPage = null!;
        private Navigation _navigation = null!;
        private NewPage _newPage = null!;
        private IPage _page = null!;

        public NonFunctionalTest(Setup startupService)
        {
            _startupService = startupService;
            _faker = new Faker();
        }

        public Task DisposeAsync()
        {
            _startupService.Dispose();
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            _page = await _startupService.InitializePlaywright();
            _loginPage = new LoginPage(_page);
            _navigation = new Navigation(_page);
            _newPage = new NewPage(_page);
            _baseUrl = _startupService.EnvironmentDetails.BaseUrl;
            _username = _startupService.EnvironmentDetails.Username;
            _password = _startupService.EnvironmentDetails.LoginPassword;
            _adminUrl = _startupService.EnvironmentDetails.AdminUrl;
        }

        [Fact]
        public async Task MeasurePageLoadTime()
        {

            var navigationStart = DateTime.Now;
            await _loginPage.Login(_baseUrl, _username, _password);
            _page.Url.Should().Be(_adminUrl);
            var navigationEnd = DateTime.Now;
            var pageLoadTime = navigationEnd - navigationStart;

            Assert.True(pageLoadTime.TotalSeconds < 1); // Adjust the threshold as needed
        }
    }
}
