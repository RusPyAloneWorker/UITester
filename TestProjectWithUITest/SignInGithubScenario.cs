using Microsoft.Playwright;
using OpenQA.Selenium;
using TestProjectWithUITest.Pages;
using UITester;

namespace TestProjectWithUITest;

public class SignInGithubScenario : ITestScenario
{
    private GithubHomePage GithubHomePage;
    private GithubSignInPage GithubSignInPage;
    private GithubUserHomePage GithubUserHomePage;

	public async Task RunAsync(IPage page)
    {
        Arrange(page);

        await page.GotoAsync("/");

        await GithubHomePage.SignInLink.ClickAsync();
		await GithubSignInPage.LoginInput.FillAsync(_login);
		await GithubSignInPage.PasswordInput.FillAsync(_password);
		await GithubSignInPage.SignInButton.ClickAsync();

        Assert.That(page.Url, Is.EqualTo("https://github.com/"));
        Assert.That(await GithubUserHomePage.UserNameText.InnerHTMLAsync(), Is.EqualTo(_userName));
        Assert.That(await GithubUserHomePage.DashboardText.InnerHTMLAsync(), Is.EqualTo("Dashboard"));
    }

    public void Arrange(IPage page)
    {
        GithubHomePage = new GithubHomePage(page);
		GithubSignInPage = new GithubSignInPage(page);
		GithubUserHomePage = new GithubUserHomePage(page);
	}

    public string ScenarioName { get; } = "Sign In Github";
}
