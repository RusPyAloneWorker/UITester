namespace TestProjectWithUITest;

[TestFixture]
// [Parallelizable(ParallelScope.All)]
public class UnitTests: BaseTest
{
	private static IEnumerable<Type> GetTestScenarios()
	{
		for (int i = 0; i < 50; i++)
		{
			yield return typeof(Calculator2Minus2Result);
		}
	}

	[TestCaseSource(nameof(GetTestScenarios))]
	public async Task GithubTests(Type testScenario)
	{
		await _uiTest.RunTestAsync(testScenario);
	}
}