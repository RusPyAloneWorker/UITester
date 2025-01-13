using Microsoft.Playwright;

namespace UITester;

public class UiTestKernelBuilder
{
	#region Constants
	private const int MaxXResolution = 1920;
	private const int MaxYResolution = 1080;
	#endregion
	
	private IPlaywright _playwright;
	private IBrowser _browser;
	private BrowserNewContextOptions _browserContextOptions;

	private UiTestKernelBuilder()
	{ }
	
	public static UiTestKernelBuilder InitChrome(string? executablePath = null)
	{
		if (executablePath != null && !File.Exists(executablePath))
		{
			throw new FileNotFoundException($"File {executablePath} not found.");
		}
		
		var playwright = Playwright.CreateAsync().GetAwaiter().GetResult();
		var browser = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
		{
			Headless = true,
			// ChromiumSandbox = true,
			ExecutablePath = executablePath
		}).GetAwaiter().GetResult();

		var builder = new UiTestKernelBuilder
		{
			_browser = browser,
			_playwright = playwright
		};

		return builder;
	}

	public UiTestKernelBuilder AddVideoRecording((int, int) resolution, string videoDirPath, string baseUrl)
	{
		if (!Directory.Exists(videoDirPath))
		{
			throw new DirectoryNotFoundException($"Directory {videoDirPath} not found.");
		}
		
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(resolution.Item1);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(resolution.Item2);

		if (resolution.Item1 > MaxXResolution || resolution.Item2 > MaxYResolution)
		{
			throw new ArgumentOutOfRangeException(nameof(resolution), $"Resolution is outside of bound {MaxXResolution}x{MaxYResolution}.");
		}

		_browserContextOptions = new BrowserNewContextOptions()
		{
			RecordVideoDir = videoDirPath,
			RecordVideoSize = new RecordVideoSize { Width = resolution.Item1, Height = resolution.Item2 },
			BaseURL = baseUrl
		};

		return this;
	}

	public UiTest Build()
	{
		var uiTest = new UiTest(_playwright, _browser, _browserContextOptions);

		return uiTest;
	}
}