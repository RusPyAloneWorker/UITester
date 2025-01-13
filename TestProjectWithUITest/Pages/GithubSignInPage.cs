using Microsoft.Playwright;
using OpenQA.Selenium;

namespace TestProjectWithUITest.Pages;

public class GithubSignInPage : BaseTestPage
{
	public GithubSignInPage(IPage page) : base(page)
	{}

	public ILocator LoginInput => ByXPath("/*[@id=\"login_field\"]");
	
	public ILocator PasswordInput => ByXPath("/*[@id=\"password\"]");

	public ILocator SignInButton =>
		ByXPath("/html/body/div[1]/div[3]/main/div/div[4]/form/div/input[13]");
}