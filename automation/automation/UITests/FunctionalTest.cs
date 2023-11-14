using automation.Pages;
using Bogus;
using Microsoft.Playwright;
using Xunit;
using FluentAssertions;

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
        public async Task AbleToAddAndEditNewPage()
        {
            var pageTitle = _faker.Random.Words(10);
            var pageBlock = _faker.Random.Words(100);
            var pageTitleUpdated = _faker.Random.Words(10);
            var pageBlockUpdated = _faker.Random.Words(100);

            await _loginPage.Login(_baseUrl, _username, _password);
            await _navigation.ClickAddNewPage();
            await Assertions.Expect(_newPage.PublishButton()).ToBeDisabledAsync();
            await _newPage.EnterPageDetails(pageTitle, pageBlock);
            await Assertions.Expect(_newPage.ViewPageNotice()).ToBeVisibleAsync();
            await Assertions.Expect(_page.GetByText(pageTitle + " is now live.")).ToBeVisibleAsync();
            var pageAddress = await _newPage.PageAddress()!.InputValueAsync();
            await _newPage.ClickViewPageButton();
            _page.Url.Should().Be(pageAddress);
            await Assertions.Expect(_newPage.PublishedPageTitle(pageTitle)).ToBeVisibleAsync();
            await Assertions.Expect(_page.GetByText(pageBlock)).ToBeVisibleAsync();
            await _navigation.ClickEditPage();
            await _newPage.UpdatePageDetails(pageTitleUpdated, pageBlockUpdated);
            await Assertions.Expect(_newPage.ViewPageNotice()).ToBeVisibleAsync();
            await _newPage.ClickViewPageLink();
            await Assertions.Expect(_newPage.PublishedPageTitle(pageTitle)).Not.ToBeVisibleAsync();
            await Assertions.Expect(_newPage.PublishedPageTitle(pageTitleUpdated)).ToBeVisibleAsync();
            await Assertions.Expect(_page.GetByText(pageBlock)).ToBeVisibleAsync();
            await Assertions.Expect(_page.GetByText(pageBlockUpdated)).ToBeVisibleAsync();
        }
    }
}
