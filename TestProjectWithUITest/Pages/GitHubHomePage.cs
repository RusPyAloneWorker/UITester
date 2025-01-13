using Microsoft.Playwright;

namespace TestProjectWithUITest.Pages;

public class GithubHomePage : BaseTestPage
{

	public GithubHomePage(IPage page) : base(page)
	{}

	public ILocator SignInLink =>
		ByXPath("/html/body/div[1]/div[3]/header/div/div[2]/div/div/div/a");
	
	public ILocator OpenSourceButton =>
		ByXPath("/html/body/div[1]/div[3]/header/div/div[2]/div/nav/ul/li[4]/button");

	public ILocator ResourcesButton =>
		ByXPath("/html/body/div[1]/div[3]/header/div/div[2]/div/nav/ul/li[3]/button");

	public ILocator AiLink =>
		ByXPath("/html/body/div[1]/div[3]/header/div/div[2]/div/nav/ul/li[3]/div/div[1]/div/ul/li[1]/a"); 

	public ILocator GitHubSponsorsLink =>
		ByXPath("/html/body/div[1]/div[3]/header/div/div[2]/div/nav/ul/li[4]/div/div/div[1]/ul/li/a");


}