using Microsoft.Playwright;
using OpenQA.Selenium;

namespace TestProjectWithUITest.Pages;

public class GithubUserHomePage : BaseTestPage
{
	public GithubUserHomePage(IPage page) : base(page)
	{
	}

	public ILocator DashboardText =>
		ByXPath("/html/body/div[1]/div[1]/header/div/div[1]/div/div[2]/nav/ul/li/a/span");

	public ILocator UserNameText => 
		ByXPath("/html/body/div[1]/div[5]/div/div/aside/div/div/div/div/select-panel/dialog-helper/button/span[1]/span/span[2]");
}