using Microsoft.Playwright;

namespace TestProjectWithUITest.Pages;

public class CalculatorPage: BaseTestPage
{
	public CalculatorPage(IPage page) : base(page)
	{ }

	public ILocator FirstNumberInput => ByXPath("/html/body/div/main/form/div[1]/input");
	public ILocator OperationSelector => ByXPath("/html/body/div/main/form/div[2]/select");
	public ILocator SecondNumberInput => ByXPath("/html/body/div/main/form/div[3]/input");
	public ILocator CalculateButton => ByXPath("/html/body/div/main/form/button");

	public static class Operators
	{
		public static string Plus = "+";
		public static string Minus = "\u2212";
		public static string Multiply = "\u00d7";
		public static string Divide = "\u00f7";
	}
}