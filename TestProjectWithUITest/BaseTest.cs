using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITester;

namespace TestProjectWithUITest
{
    public class BaseTest
    {
        private DirectoryInfo BaseProjectDirectory =>
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!;

        private string RecordsDirectory => Path.Combine(BaseProjectDirectory.Parent!.FullName, _configuration["VideoRecordsProjectDirectoryPath"]);

        private string? chromeExecutable => _configuration["ChromeExecutablePath"]; // @"C:\Users\almetov.mr\AppData\Local\ms-playwright\chromium-1148\chrome-win\chrome.exe";

        protected UiTest _uiTest;

        private const string BaseUrl = @"https://github.com/";

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
