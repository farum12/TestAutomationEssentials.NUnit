using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechTalk.SpecFlow.Infrastructure;

namespace Farum.QA.TestAutomationEssentials.Drivers
{
    /// <summary>
    /// Class handling interactions with Web Browser for given test thread.
    /// </summary>
    public class WebDriver : IDisposable
    {
        private readonly ConfigurationDriver _configurationDriver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        private readonly BrowserSeleniumDriverFactory _browserSeleniumDriverFactory;
        private readonly Lazy<IWebDriver> _currentWebDriverLazy;
        private readonly TimeSpan _waitDuration = TimeSpan.FromSeconds(30);
        private bool _isDisposed;

        public WebDriver(ConfigurationDriver configurationDriver,
                         ISpecFlowOutputHelper specFlowOutputHelper,
                         BrowserSeleniumDriverFactory browserSeleniumDriverFactory)
        {
            _browserSeleniumDriverFactory = browserSeleniumDriverFactory;
            _configurationDriver = configurationDriver;
            _specFlowOutputHelper = specFlowOutputHelper;
            _currentWebDriverLazy = new Lazy<IWebDriver>(GetWebDriver);
        }

        public IWebDriver Current => _currentWebDriverLazy.Value;

        /// <summary>
        /// Method wich returns default WebDriverWait with default wait duration.
        /// </summary>
        public WebDriverWait Wait()
        {
            // IF WE WANT TO USE RETRY FROM NUNIT - CUSTOM WEBDRIVERWAIT WILL BE NEEDED
            return new WebDriverWait(Current, _waitDuration);
        }

        /// <summary>
        /// Method wich returns default WebDriverWait with given wait duration.
        /// </summary>
        /// <param name="waitDuration">Time for which WebDriver should wait until further conditions are met.</param>
        public WebDriverWait Wait(TimeSpan waitDuration)
        {
            // IF WE WANT TO USE RETRY FROM NUNIT - CUSTOM WEBDRIVERWAIT WILL BE NEEDED
            return new WebDriverWait(Current, waitDuration);
        }

        /// <summary>
        /// Method wich returns WebDriverWait with default wait duration. Outputs timeoutMessage when timeout has occured.
        /// </summary>
        /// <param name="timeoutMessage">Message, which will be displayed when WebDriverWait hits timeout.</param>
        public WebDriverWait Wait(String timeoutMessage)
        {
            // IF WE WANT TO USE RETRY FROM NUNIT - CUSTOM WEBDRIVERWAIT WILL BE NEEDED
            var wait = new WebDriverWait(Current, _waitDuration);
            wait.Message = timeoutMessage;
            return wait;
        }

        /// <summary>
        /// Method wich returns WebDriverWait with given wait duration. Outputs timeoutMessage when timeout has occured.
        /// </summary>
        /// <param name="timeoutMessage">Message, which will be displayed when WebDriverWait hits timeout.</param>
        /// <param name="waitDuration">Time for which WebDriver should wait until further conditions are met.</param>
        public WebDriverWait Wait(String timeoutMessage, TimeSpan waitDuration)
        {
            // IF WE WANT TO USE RETRY FROM NUNIT - CUSTOM WEBDRIVERWAIT WILL BE NEEDED
            var wait = new WebDriverWait(Current, waitDuration);
            wait.Message = timeoutMessage;
            return wait;
        }

        private IWebDriver GetWebDriver()
        {
            return _browserSeleniumDriverFactory.GetForBrowser(_configurationDriver.Browser);
        }

        /// <summary>
        /// Captures screenshot of currently opened Page, which is handled by WebDriver.
        /// </summary>
        /// <param name="screenshotPrefix">Prefix which will be set on captured screenshot.</param>
        /// <returns>Path to captured screenshot.</returns>
        public String GetScreenshot(String screenshotPrefix)
        {
            try
            {
                if (!Directory.Exists($"{TestContext.CurrentContext.WorkDirectory}\\Screenshots\\"))
                {
                    if (_configurationDriver.DebugMode)
                    {
                        _specFlowOutputHelper.WriteLine("Screenshot path missing. Creating one...");
                    }
                    Directory.CreateDirectory($"{TestContext.CurrentContext.WorkDirectory}\\Screenshots");
                }
                var screenshotPath = $"{TestContext.CurrentContext.WorkDirectory}\\Screenshots\\";



                // Replace illegal characters
                var screenshotTitle = screenshotPrefix.Replace(" ", "_").Replace("\\", "_").Replace("/", "_") + "_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm");
                ((ITakesScreenshot)Current).GetScreenshot().SaveAsFile($"{screenshotPath}{screenshotTitle}.png", ScreenshotImageFormat.Png);
                _specFlowOutputHelper.WriteLine($"Captured screenshot with title: {screenshotTitle}");
                return $"{screenshotPath}{screenshotTitle}.png";
            }
            catch (Exception e)
            {
                _specFlowOutputHelper.WriteLine("[GetScreenshot] Unable to generate screenshot. Error message: ");
                _specFlowOutputHelper.WriteLine(e.Message);
                return null;
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentWebDriverLazy.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}
