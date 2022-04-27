using FluentAssertions;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using WebDriver = Farum.QA.TestAutomationEssentials.Drivers.WebDriver;

namespace Farum.QA.TestAutomationEssentials.StepDefinitions
{
    [Binding]
    public class FT_0001_LoginSteps
    {
        private readonly WebDriver _webDriver;
        private readonly Drivers.PageDrivers.LoginPageDriver _loginPageDriver;

        public FT_0001_LoginSteps(WebDriver webDriver,
                                  Drivers.PageDrivers.LoginPageDriver loginPageDriver)
        {
            _webDriver = webDriver;
            _loginPageDriver = loginPageDriver;
        }

        [Given(@"User is on Swag Labs login page")]
        public void GivenUserIsOnSwagLabsLoginPage()
        {
            _webDriver
                .Current.Manage().Window.Maximize();
        }

        [Given(@"User sets ""(.*)"" in Username")]
        public void GivenUserSetsInUsername(string username)
        {
            _loginPageDriver
                .SetUsername(username);
        }

        [Given(@"User sets ""(.*)"" in Password")]
        public void GivenUserSetsInPassword(string password)
        {
            _loginPageDriver
                .SetPassword(password);
        }

        [When(@"User clicks Login button")]
        public void WhenUserClicksLoginButton()
        {
            _loginPageDriver
                .ClickLogin();
        }

        [Then(@"Products should be visible")]
        public void ThenProductsShouldBeVisible()
        {
            _webDriver
                .Wait("Products Page wasn't displayed in expected time!")
                .Until(Support.ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='title']")))
                .Text.Should().Contain("PRODUCTS");
        }
    }
}
