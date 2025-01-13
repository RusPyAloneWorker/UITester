using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectWithUITest.Pages;


public abstract class BaseTestPage
{
	private IPage Page { get; init; }

    protected BaseTestPage(IPage page)
    {
		Page = page;
	}

	protected ILocator ByXPath(string path) => Page.Locator("xpath=/"+path);
}

