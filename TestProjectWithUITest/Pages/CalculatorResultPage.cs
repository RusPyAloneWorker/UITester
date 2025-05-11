using Microsoft.Playwright;

namespace TestProjectWithUITest.Pages;

public class CalculatorResultPage: BaseTestPage
{
	public CalculatorResultPage(IPage page) : base(page)
	{ }

	public ILocator Result => ByXPath("/html/body/div/main/div/p/strong");
}