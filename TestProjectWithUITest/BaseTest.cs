using Microsoft.Extensions.Configuration;
using UITester;

namespace TestProjectWithUITest
{
    public class BaseTest
    {
        private DirectoryInfo BaseProjectDirectory =>
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!;

        private string RecordsDirectory => Path.Combine(BaseProjectDirectory.Parent!.FullName, _configuration["VideoRecordsProjectDirectoryPath"]);

        private string? chromeExecutable => _configuration["ChromeExecutablePath"]; 

        protected UiTest _uiTest;

        private const string BaseUrl = @"http://localhost:5176/";

        private IConfiguration _configuration;

        private static (int, int) Resolution = (1240, 640);

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder();
            _configuration = builder
                .SetBasePath(BaseProjectDirectory.FullName)
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            _uiTest = UiTestKernelBuilder
                .InitChrome(chromeExecutable)
                .AddVideoRecording(Resolution, RecordsDirectory, BaseUrl)
                .Build();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _uiTest.Dispose();
        }
    }
}
