using Microsoft.Playwright;
using OpenQA.Selenium;

namespace UITester;

/// <summary>
/// Тестироващик сценариев.
/// </summary>
public interface ITestScenario
{
    Task RunAsync(IPage page);
    
    string ScenarioName { get; }
}