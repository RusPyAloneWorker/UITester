namespace TestProjectWithUITest;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class UnitTests: BaseTest
{
	// [TestCase(typeof(ClickCalculatorLink))]
	[TestCase(typeof(Calculator2Plus2Result))]
	[TestCase(typeof(Calculator2Minus2Result))]
	public async Task GithubTests(Type testScenario)
	{
		await _uiTest.RunTestAsync(testScenario);
	}
}