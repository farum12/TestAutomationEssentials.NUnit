using Farum.QA.TestAutomationEssentials.Support;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farum.QA.TestAutomationEssentials.Drivers.PageDrivers
{
    public class LoginPageDriver
    {
        private Element UsernameTextBox => new Element(By.Id("user-name"), _webDriver);
        private Element PasswordTextBox => new Element(By.Id("password"), _webDriver);
        private Element LoginButton => new Element(By.Id("login-button"), _webDriver);


        private readonly WebDriver _webDriver;

        public LoginPageDriver(WebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public LoginPageDriver SetUsername(String username)
        {
            UsernameTextBox.SendKeys(username);
            return this;
        }

        public LoginPageDriver SetPassword(String password)
        {
            PasswordTextBox.SendKeys(password);
            return this;
        }

        public void ClickLogin()
        {
            LoginButton.Click();
        }
    }
}
