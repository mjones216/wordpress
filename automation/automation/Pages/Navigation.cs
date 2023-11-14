using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automation.Pages
{
    public class Navigation
    {
        private readonly string _pagesLink = "Pages";
        private readonly string _addNewPage = "Add New";
        private readonly string _loginButton = "Log In";
        private readonly IPage _page;

        public Navigation(IPage page)
        {
            _page = page;
        }

        public async Task<IPage> ClickAddNewPage()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = _pagesLink, Exact = true }).ClickAsync();
            await _page.Locator("#wpbody-content").GetByRole(AriaRole.Link, new() { Name = _addNewPage }).ClickAsync();
            await _page.WaitForLoadStateAsync();
            return _page;
        }
    }
}
