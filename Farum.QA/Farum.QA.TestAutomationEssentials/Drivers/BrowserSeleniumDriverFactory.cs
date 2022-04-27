using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Farum.QA.TestAutomationEssentials.Drivers
{
    /// <summary>
    /// Class which returns driver for selected Browser.
    /// </summary>
    public class BrowserSeleniumDriverFactory
    {
        private readonly ConfigurationDriver _configurationDriver;
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public BrowserSeleniumDriverFactory(ConfigurationDriver configurationDriver,
                                            FeatureContext featureContext,
                                            ScenarioContext scenarioContext)
        {
            _configurationDriver = configurationDriver;
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        public IWebDriver GetForBrowser(Enum browserId)
        {
            //string lowerBrowserId = browserId.ToUpper();
            switch (browserId)
            {
                //case "IE": return GetInternetExplorerDriver();
                case Support.Enums.BrowserTypes.Chrome: return GetChromeDriver();
                case Support.Enums.BrowserTypes.SauceLabs: return GetSauceLabsDriver();
                //case "FIREFOX": return GetFirefoxDriver();
                default: throw new NotSupportedException("not supported browser!");
            }
        }

        /*private IWebDriver GetFirefoxDriver()
        {
            return new FirefoxDriver(FirefoxDriverService.CreateDefaultService(_testRunContext.TestDirectory))
            {
                Url = _configurationDriver.SeleniumBaseUrl
            };
        }*/

        private IWebDriver GetChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            return new ChromeDriver(options)
            {
                Url = _configurationDriver.SeleniumBaseUrl
            };
        }

        private IWebDriver GetSauceLabsDriver()
        {
            var tags = new List<string>();
            tags.Add("Farum");

            var sauceOptions = new Dictionary<string, object>
            {
                ["username"] = "SOMEUSERNAMEHERE",
                ["accessKey"] = "ACCESSKEYHERE",
                ["name"] = $"[Test Run] {_featureContext.FeatureInfo.Title} - {_scenarioContext.ScenarioInfo.Title}",
                ["public"] = "team",
                ["screenResolution"] = "1920x1080",
                ["tags"] = tags,
                ["build"] = $"{DateTime.Now.ToString("MM_dd_yyyy")}_Farum_Test_Run"
            };

            var options = new ChromeOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10",
                UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore
            };

            options.AddAdditionalCapability("sauce:options", sauceOptions, true);

            return new RemoteWebDriver(new Uri($"https://{sauceOptions["username"]}:{sauceOptions["accessKey"]}@SOMEDATACENTRE.com"), options.ToCapabilities(), TimeSpan.FromSeconds(40));
        }

        /*private IWebDriver GetInternetExplorerDriver()
        {
            var internetExplorerOptions = new InternetExplorerOptions
            {
                IgnoreZoomLevel = true;
            }
            return new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(_testRunContext.TestDirectory), internetExplorerOptions)
            {
                Url = _configurationDriver.SeleniumBaseUrl
            };
        }*/
    }
}
