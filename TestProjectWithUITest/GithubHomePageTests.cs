namespace TestProjectWithUITest;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class GithubHomePageTests: BaseTest
{
	[TestCase(typeof(OpenGithubSponsorsScenario))]
	//[TestCase(typeof(SignInGithubScenario))
	[TestCase(typeof(OpenAiScenario))]
	public async Task GithubTests(Type testScenario)
	{
		await _uiTest.RunTestAsync(testScenario);
	}
}