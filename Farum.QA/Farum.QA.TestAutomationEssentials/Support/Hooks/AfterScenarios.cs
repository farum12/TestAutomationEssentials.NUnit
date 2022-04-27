using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
namespace Farum.QA.TestAutomationEssentials.Support.Hooks
{
    /// <summary>
    /// Class which contains methods, which will be executed after each scenario.
    /// </summary>
    [Binding]
    public class AfterScenarios
    {
        private readonly Drivers.WebDriver _webDriver;
        private readonly ScenarioContext _scenarioContext;
        private readonly Drivers.ConfigurationDriver _configurationDriver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        public AfterScenarios(Drivers.WebDriver webDriver,
                              ScenarioContext scenarioContext,
                              Drivers.ConfigurationDriver configurationDriver,
                              ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _webDriver = webDriver;
            _scenarioContext = scenarioContext;
            _configurationDriver = configurationDriver;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        /// <summary>
        /// Executes logic when scenario was failed. Currently captures scenario and saves it.
        /// </summary>
        [AfterScenario(Order = 100)]
        public void OnError()
        {
            if (_scenarioContext.TestError != null)
            {
                if (_configurationDriver.TakeScreenshots)
                {
                    var filePath = _webDriver.GetScreenshot(_scenarioContext.ScenarioInfo.Title);
                    _specFlowOutputHelper.AddAttachment(filePath);
                    // Uncomment for Allure support
                    //AllureLifecycle.Instance.AddAttachment(filePath, "_scenarioContext.ScenarioInfo.Title");
                }

                _specFlowOutputHelper.WriteLine("Something has happened... error was catched.");
                _specFlowOutputHelper.WriteLine($"Current URL: {_webDriver.Current.Url}");
            }
        }

        /// <summary>
        /// Executes logic when scenario was passed. Currently captures scenario and saves it.
        /// </summary>
        [AfterScenario(Order = 100)]
        public void OnPass()
        {
            if (_scenarioContext.TestError == null)
            {
                if (_configurationDriver.TakeScreenshots && _configurationDriver.TakeScreenshotsOnSuccess)
                {
                    var filePath = _webDriver.GetScreenshot(_scenarioContext.ScenarioInfo.Title);
                    _specFlowOutputHelper.AddAttachment(filePath);
                    // Uncomment for Allure support
                    //AllureLifecycle.Instance.AddAttachment(filePath, "_scenarioContext.ScenarioInfo.Title");
                }

                _specFlowOutputHelper.WriteLine("Scenario successful. Closing the WebDriver");
                _specFlowOutputHelper.WriteLine($"Current URL: {_webDriver.Current.Url}");
            }
        }

        /// <summary>
        /// Executes logic when Scenario was ran on Sauce Labs, to mark either failure or pass.
        /// </summary>
        [AfterScenario(Order = 101)]
        public void ExecuteSauceLabsLogic()
        {
            if (_configurationDriver.Browser == Enums.BrowserTypes.SauceLabs)
            {
                ((IJavaScriptExecutor)_webDriver.Current).ExecuteScript("sauce:job-result=" + (_scenarioContext.TestError == null ? "passed" : "failed"));
            }
        }

    }
}
