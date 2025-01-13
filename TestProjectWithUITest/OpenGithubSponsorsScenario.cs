using Microsoft.Playwright;
using TestProjectWithUITest.Pages;
using UITester;

namespace TestProjectWithUITest;

public class OpenGithubSponsorsScenario: ITestScenario
{
    public OpenGithubSponsorsScenario()
    { }

    private GithubHomePage GithubHomePage { get; set; }
	
	public async Task RunAsync(IPage page)
	{
		Arrange(page);

		await page.GotoAsync("/");
		await GithubHomePage.OpenSourceButton.HoverAsync();
		await GithubHomePage.GitHubSponsorsLink.ClickAsync();
		
		Assert.Contains("sponsors", page.Url.Split('/'), "Не перешел по пути sponsors");
	}

	public void Arrange(IPage page)
	{
		GithubHomePage = new GithubHomePage(page);
	}

	public string ScenarioName { get; } = "Open GutHub Sponsors Page";
}