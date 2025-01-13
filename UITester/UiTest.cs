using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace UITester;

public class UiTest: IDisposable
{
	#region Constants
	private const int PauseBeforeVideoEndsSeconds = 2;
	#endregion

	private readonly IPlaywright _playwright;
	private readonly IBrowser _browser;
	private readonly BrowserNewContextOptions _browserNewContextOptions;

	public UiTest(IPlaywright playwright, IBrowser browser, BrowserNewContextOptions browserNewContextOptions)
	{
		_playwright = playwright;
		_browser = browser;
		_browserNewContextOptions = browserNewContextOptions;
	}

	public async Task RunTestAsync(ITestScenario testScenario)
	{
		var context = await _browser.NewContextAsync(_browserNewContextOptions);
		var page = await context.NewPageAsync();
		var videoTitle = testScenario.ScenarioName;

		string? videoPath = await page.Video?.PathAsync();

		try
		{
			await testScenario.RunAsync(page);
		}
		catch (AssertionException assertionException)
		{
			videoTitle = MakeErrorVideoTitle(assertionException.Message, testScenario.ScenarioName);
			throw;
		}
		catch (PlaywrightException driverException)
		{
			videoTitle = MakeErrorVideoTitle(driverException.Message, testScenario.ScenarioName);
			throw new ApplicationException(
				$"The test failed: {driverException.Message}. See video: "
				, driverException);
		}
		catch (ApplicationException applicationException)
		{
			videoTitle = MakeErrorVideoTitle(applicationException.Message, testScenario.ScenarioName);
			throw new ApplicationException($"Video Record has failed: {applicationException.Message}", applicationException);
		}
		catch (Exception exception)
		{
			videoTitle = MakeErrorVideoTitle(exception.Message, testScenario.ScenarioName);
			throw new ApplicationException($"Something went wrong: {exception.Message}", exception);
		}
		finally
		{
			await Task.Delay(TimeSpan.FromSeconds(PauseBeforeVideoEndsSeconds));
			await context.CloseAsync();
			await context.DisposeAsync();

			RenameVideoRecord(videoPath, videoTitle);
		}
	}

	public async Task RunTestAsync(Type testScenarioType)
	{
		if (testScenarioType.IsAssignableFrom(typeof(ITestScenario)))
		{
			throw new ArgumentException("This type is not test scenario.");
		}

		var testScenario = Activator.CreateInstance(testScenarioType)!;

		await RunTestAsync((ITestScenario)testScenario);
	}

	private string MakeErrorVideoTitle(string exceptionMessage, string scenarioName)
	{
		var errorMessage = exceptionMessage
			.Replace("\n", String.Empty)
			.Replace("\t", String.Empty)
			.Replace("\r", String.Empty)
			.Replace("\\", String.Empty)
			.Replace("|", String.Empty)
			.Replace("/", String.Empty)
			.Replace("?", String.Empty)
			.Replace(":", "")
			.Replace("  ", " ")
			.Replace("\"", "'")
			.Replace("<", "(")
			.Replace(">", ")");

		return $"[Error {errorMessage}] {scenarioName}"; 
	}

	private void RenameVideoRecord(string filePath, string newTitle)
	{
		var fileExtension = new FileInfo(filePath).Extension;
		var path = Path.Combine(_browserNewContextOptions.RecordVideoDir, newTitle + fileExtension);

		if (File.Exists(path))
		{
			File.Delete(path);
		}

		File.Move(filePath, path);
	}

	public async ValueTask DisposeAsync()
	{
		await _browser.DisposeAsync();
		_playwright.Dispose();
	}

	public void Dispose()
	{
		if (_browser is IDisposable browserDisposable)
			browserDisposable.Dispose();
		else
			_ = _browser.DisposeAsync().AsTask();
		
		_playwright.Dispose();
	}
}