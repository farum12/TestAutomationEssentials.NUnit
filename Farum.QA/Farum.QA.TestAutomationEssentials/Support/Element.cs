using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using WebDriver = Farum.QA.TestAutomationEssentials.Drivers.WebDriver;
namespace Farum.QA.TestAutomationEssentials.Support
{
    public class Element : IWebElement
    {
        public IWebElement Elem;
        public readonly By Locator;
        private readonly IWebDriver _webDriver;

        public string TagName => Elem.TagName;

        public string Text => Elem.Text;

        public bool Enabled => Elem.Enabled;

        public bool Selected => Elem.Selected;

        public Point Location => Elem.Location;

        public Size Size => Elem.Size;

        public bool Displayed => Elem.Displayed;

        /// <summary>
        /// Returns an Element object, which represents IWebElement. This constructor uses default Wait Time, as set in ConfigurationDriver.
        /// </summary>
        /// <param name="locator">Locator for the Element</param>
        /// <param name="webDriver">WebDriver which should search for given locator.</param>
        /// <param name="expectedContidion">Optional argument for Support.ExpectedConditions method, which will be called before element is found. null - verify if element is visible on DOM.</param>
        public Element(By locator, WebDriver webDriver, Func<IWebDriver, IWebElement> expectedContidion = null)
        {
            webDriver
                .Wait($"Cannot find element located by {locator} at \n {webDriver.Current.Url}")
                .Until(expectedContidion ?? ExpectedConditions.ElementIsVisible(locator));

            Elem = webDriver.Current.FindElement(locator);
            Locator = locator;
            _webDriver = webDriver.Current;
        }

        /// <summary>
        /// Returns an Element object, which represents IWebElement. Serves as conversion from IWebElement to Element.
        /// </summary>
        /// <param name="element">IWebElement to convert onto Element</param>
        public Element(IWebDriver driver, IWebElement element)
        {
            Elem = element;
            _webDriver = driver;
        }

        /// <summary>
        /// Scrolls View Port to given Element.
        /// </summary>
        public Element ScrollIntoView()
        {
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", Elem);
            return this;
        }




        public void Clear()
        {
            Elem.Clear();
        }

        public void Click()
        {
            Elem.Click();
        }

        public IWebElement FindElement(By by)
        {
            return Elem.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Elem.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            return Elem.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return Elem.GetCssValue(propertyName);
        }

        public string GetDomAttribute(string attributeName)
        {
            return Elem.GetDomAttribute(attributeName);
        }

        public string GetDomProperty(string propertyName)
        {
            return Elem.GetDomProperty(propertyName);
        }

        public string GetProperty(string propertyName)
        {
            return Elem.GetProperty(propertyName);
        }

        public ISearchContext GetShadowRoot()
        {
            throw new NotImplementedException();
        }

        public void SendKeys(string text)
        {
            Elem.SendKeys(text);
        }

        public void Submit()
        {
            throw new NotImplementedException();
        }

    }
}
