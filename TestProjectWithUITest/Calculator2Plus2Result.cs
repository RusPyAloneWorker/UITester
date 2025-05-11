using Microsoft.Playwright;
using TestProjectWithUITest.Pages;
using UITester;

namespace TestProjectWithUITest;

public class Calculator2Plus2Result: ITestScenario
{
	private CalculatorPage CalculatorPage;
	private HomePage HomePage;
	private CalculatorResultPage CalculatorResultPage;
	
	public async Task RunAsync(IPage page)
	{
		Arrange(page);
		var correctNumber = 4;

		await page.GotoAsync("/");

		await Task.Delay(TimeSpan.FromSeconds(5));
		
		await HomePage.CalculatorLink.ClickAsync();

		await CalculatorPage.FirstNumberInput.FillAsync("2");
		await CalculatorPage.OperationSelector.SelectOptionAsync(CalculatorPage.Operators.Plus);
		await CalculatorPage.SecondNumberInput.FillAsync("2");
		
		await Task.Delay(TimeSpan.FromSeconds(5));
		
		await CalculatorPage.CalculateButton.ClickAsync();
		
		await Task.Delay(TimeSpan.FromSeconds(5));

		int.TryParse(await CalculatorResultPage.Result.TextContentAsync(), out var number);
		
		Assert.That(number, Is.EqualTo(correctNumber));
	}
	
	public void Arrange(IPage page)
	{
		HomePage = new HomePage(page);
		CalculatorPage = new CalculatorPage(page);
		CalculatorResultPage = new CalculatorResultPage(page);
	}


	public string ScenarioName { get; } = "Calculate 2+2";
}