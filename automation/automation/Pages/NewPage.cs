using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automation.Pages
{
    public class NewPage
    {
        private readonly string _title = "Add title";
        private readonly string _block = "Empty block; start writing or type forward slash to choose a block";
        private readonly string _publishButton = "Publish";
        private readonly IPage _page;

        public NewPage(IPage page)
        {
            _page = page;
        }

        public async Task<IPage> EnterNewPageDetails(string title, string block)
        {
            await _page.FrameLocator("iframe[name=\"editor-canvas\"]").GetByLabel(_title).FillAsync(title);
            await _page.FrameLocator("iframe[name=\"editor-canvas\"]").GetByLabel("Add default block").ClickAsync();
            await _page.FrameLocator("iframe[name=\"editor-canvas\"]").GetByLabel(_block).FillAsync(block);
            await _page.GetByRole(AriaRole.Button, new() { Name = _publishButton, Exact = true }).ClickAsync();
            await _page.GetByLabel("Editor publish").GetByRole(AriaRole.Button, new() { Name = _publishButton, Exact = true }).ClickAsync();
            await _page.WaitForLoadStateAsync();
            return _page;
        }
    }
}
