using Microsoft.Playwright;
using TestProjectWithUITest.Pages;
using UITester;

namespace TestProjectWithUITest;

public class ClickCalculatorLink: ITestScenario
{
	private HomePage _homePage;

	public async Task RunAsync(IPage page)
	{
		Arrange(page);

		await page.GotoAsync("/");
		
		await Task.Delay(TimeSpan.FromSeconds(5));
		
		await _homePage.CalculatorLink.ClickAsync();

		Assert.True(page.Url.Contains("/Calculator"), "Не перешел по пути /сalculator");
	}

	public void Arrange(IPage page)
	{
		_homePage = new HomePage(page);
	}

	public string ScenarioName { get; } = "Click Calculator Link";	
}