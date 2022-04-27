using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow.Infrastructure;

namespace Farum.QA.TestAutomationEssentials.Drivers
{
    /// <summary>
    /// Class which represents test configuration for given Test Thread. Additionally serves as variables container.
    /// </summary>
    public class ConfigurationDriver
    {
        // Set of variables which are set from given *.srprofile
        #region VariablesSetOnStartup
        /// <summary>
        /// Variable for setting which WebDriver should be used (CHROME, FIREFOX, SAUCELABS, etc.)
        /// </summary>
        public Support.Enums.BrowserTypes Browser = Support.Enums.BrowserTypes.Chrome;

        /// <summary>
        /// Variable for debug functions e.g. turning on/off various WriteLines.
        /// </summary>
        public Boolean DebugMode = true;

        /// <summary>
        /// Variable for turning on/off screenshot capturing; useful when testing non-browser functionalities.
        /// </summary>
        public Boolean TakeScreenshots = true;

        /// <summary>
        /// Variable for turning on/off screenshot capturing when a scenario is successful; useful when we want to ensure with SS that a scenario was successful.
        /// </summary>
        public Boolean TakeScreenshotsOnSuccess = false;

        #endregion

        /// <summary>
        /// Base URL for application instance.
        /// </summary>
        public String SeleniumBaseUrl => "https://www.saucedemo.com/";

        /// <summary>
        /// Base URL for API requests.
        /// </summary>
        public String APIBaseUrl => "";

        /// <summary>
        /// Base directory for test data.
        /// </summary>
        public String TestDataDirectory => @"C:\Dev\TestData\";

        private readonly ISpecFlowOutputHelper _outputHelper;

        public ConfigurationDriver(ISpecFlowOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            BuildConfiguration();
        }

        /// <summary>
        /// Sets configuration variables. Values are taken form current *.srprofile.
        /// </summary>
        public void BuildConfiguration()
        {
            if (TestContext.Parameters.Get("browser") != null)
            {
                switch (TestContext.Parameters.Get("browser"))
                {
                    //case "IE": Support.Enums.BrowserTypes.InternetExplorer; break;
                    case "CHROME": Browser = Support.Enums.BrowserTypes.Chrome; break;
                    case "SAUCELABS": Browser = Support.Enums.BrowserTypes.SauceLabs; break;
                    //case "FIREFOX": Browser = Support.Enums.BrowserTypes.Firefox; break;
                    case string browser: throw new NotSupportedException($"{browser} is not a supported browser!");
                    default: throw new NotSupportedException("not supported browser: <null>");
                }
            }
            else
            {
                _outputHelper.WriteLine("[BuildConfiguration] Browser variable not defined in given runsettings! Ensure that you've set browser or you've selected runsettings!");
                throw new NotImplementedException();
            }

            if (TestContext.Parameters.Get("debugMode") != null)
            {
                DebugMode = Convert.ToBoolean(TestContext.Parameters.Get("debugMode"));
            }
            else
            {
                _outputHelper.WriteLine("[BuildConfiguration] DebugMode variable not defined in given runsettings. Using default.");
            }

            if (TestContext.Parameters.Get("takeScreenshotOnSuccess") != null)
            {
                TakeScreenshotsOnSuccess = Convert.ToBoolean(TestContext.Parameters.Get("takeScreenshotOnSuccess"));
            }
            else
            {
                _outputHelper.WriteLine("[BuildConfiguration] takeScreenshotOnSuccess variable not defined in given runsettings. Using default.");
            }
        }
    }
}
