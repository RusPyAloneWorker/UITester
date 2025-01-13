using Microsoft.Playwright;
using TestProjectWithUITest.Pages;
using UITester;

namespace TestProjectWithUITest;

public class OpenAiScenario : ITestScenario
{
	private GithubHomePage GithubHomePage;

	public async Task RunAsync(IPage page)
	{
		Arrange(page);

		await page.GotoAsync("/");
		await GithubHomePage.ResourcesButton.HoverAsync();
		await GithubHomePage.AiLink.ClickAsync();

		Assert.True(page.Url.Contains("/resources/articles/ai"), "Не перешел по пути sponsors");
	}

	public void Arrange(IPage page)
	{
		GithubHomePage = new GithubHomePage(page);
	}

	public string ScenarioName { get; } = "Open Ai Page";
}