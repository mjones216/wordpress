using automation.Pages;
using Bogus;
using Microsoft.Playwright;
using Xunit;

namespace automation.UITests
{
    public class FunctionalTest : IClassFixture<Setup>, IAsyncLifetime
    {
        private readonly Setup _startupService;
        private readonly Faker _faker;
        private string _baseUrl = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private LoginPage _loginPage = null!;
        private Navigation _navigation = null!;
        private NewPage _newPage = null!;
        private IPage _page = null!;

        public FunctionalTest(Setup startupService)
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
        }

        [Fact]
        public async Task AbleToAddNewPage()
        {
            var pageTitle = _faker.Random.AlphaNumeric(10);
            var pageBlock = _faker.Random.AlphaNumeric(100);

            await _loginPage.Login(_baseUrl, _username, _password);
            await _navigation.ClickAddNewPage();
            await _newPage.EnterNewPageDetails(pageTitle, pageBlock);
        }
    }
}
