using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace automation.UITests
{
    public class FunctionalTest : IClassFixture<Setup>, IAsyncLifetime
    {
        private readonly Setup _startupService;
        private string _baseUrl = string.Empty;
        private string _password = string.Empty;
        private IPage _page = null!;

        public FunctionalTest(Setup startupService)
        {
            _startupService = startupService;
        }

        public Task DisposeAsync()
        {
            _startupService.Dispose();
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            _page = await _startupService.InitializePlaywright();
            _baseUrl = _startupService.EnvironmentDetails.BaseUrl;
            _password = _startupService.EnvironmentDetails.LoginPassword;
        }

        [Fact]
        public async Task UserIsAbleToClickLogin()
        {
            await _page.GotoAsync(_baseUrl);
        }
    }
}
