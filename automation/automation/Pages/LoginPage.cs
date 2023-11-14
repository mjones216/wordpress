using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automation.Pages
{
    public class LoginPage
    {
        private readonly string _username = "Username or Email Address";
        private readonly string _password = "Password";
        private readonly string _loginButton = "Log In";
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }

        public async Task<IPage> Login(string baseUrl, string username, string password)
        {
            await _page.GotoAsync(baseUrl);
            await _page.WaitForLoadStateAsync();
            await _page.GetByLabel(_username).FillAsync(username);
            await _page.GetByLabel(_password, new() { Exact = true }).FillAsync(password);
            await _page.GetByRole(AriaRole.Button, new() { Name = _loginButton }).ClickAsync();
  
            return _page;
        }
    }
}
