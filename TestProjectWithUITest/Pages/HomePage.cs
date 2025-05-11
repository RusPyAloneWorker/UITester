using Microsoft.Playwright;

namespace TestProjectWithUITest.Pages;

public class HomePage: BaseTestPage
{
	public HomePage(IPage page) : base(page)
	{ }

	public ILocator CalculatorLink => ByXPath("/html/body/header/nav/div/div/ul/li[3]/a");
}