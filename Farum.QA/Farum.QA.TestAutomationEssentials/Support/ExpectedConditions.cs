using OpenQA.Selenium;
using System;

namespace Farum.QA.TestAutomationEssentials.Support
{
    /// <summary>
    /// Class containing methods which should be used in _webDriver.Wait().Until().
    /// </summary>
    public static class ExpectedConditions
    {
        /// <summary>
        /// An expectation for checking that an element is present on the DOM of a
        /// page. Visibility means that the element is not only displayed but also has a height and width that is greater than 0.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The <see cref="IWebElement"/> once it is located.</returns>
        public static Func<IWebDriver, Element> ElementIsVisible(By locator)
        {
            return driver =>
            {
                try
                {
                    return new Element(driver, driver.FindElement(locator));
                }
                catch
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// An expectation for checking an element is visible and enabled such that you
        /// can click it.
        /// </summary>
        /// <param name="locator">The element's locator.</param>
        public static Func<IWebDriver, Element> ElementIsInteractable(By locator)
        {
            return driver =>
            {
                var element = driver.FindElement(locator);
                if (element.Enabled && element.Displayed)
                {
                    return new Element(driver, element);
                }
                return null;
            };
        }

        /// <summary>
        /// An expectation for checking the title of a page.
        /// </summary>
        /// <param name="title">The expected title, which must be an exact match.</param>
        public static Func<IWebDriver, bool> PageTitleEquals(string title)
        {
            return driver =>
            {
                try
                {
                    return driver.Title.Equals(title);
                }
                catch
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// An expectation for checking that the title of a page contains a case-sensitive substring.
        /// </summary>
        /// <param name="title">The fragment of title expected.</param>
        public static Func<IWebDriver, bool> PageTitleContains(string title)
        {
            return driver =>
            {
                try
                {
                    return driver.Title.Contains(title);
                }
                catch
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// An expectation for checking that the title of a page has changed from given title.
        /// </summary>
        /// <param name="currentPageTitle">Current's Page title.</param>
        public static Func<IWebDriver, bool> PageTitleChangedFrom(string currentPageTitle)
        {
            return driver =>
            {
                var titleChanged = false;
                try
                {
                    if (!driver.Title.Equals(currentPageTitle))
                        titleChanged = true;
                }
                catch { }

                return titleChanged;
            };
        }

        /// <summary>
        /// Wait until an element is no longer attached to the DOM.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><see langword="false"/> is the element is still attached to the DOM; otherwise, <see langword="true"/>.</returns>
        public static Func<IWebDriver, bool> StalenessOf(By locator)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator).Enabled;
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            };
        }

        /// <summary>
        /// An expectation for checking if the given text is present in the specified element.
        /// </summary>
        /// <param name="locator">The WebElement's locator</param>
        /// <param name="text">Text to be present in the element</param>
        public static Func<IWebDriver, bool> ElementContainsText(By locator, string text)
        {
            return driver =>
            {
                try
                {
                    if (driver.FindElement(locator).Text.Contains(text))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            };
        }

        public static Func<IWebDriver, bool> TaskIsCompleted(Func<bool> task)
        {
            return d =>
            {
                if (task())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// An expectation for the URL of the current page to contain a specific URL.
        /// </summary>
        /// <param name="fraction">The fraction of the url that the page should be on.</param>
        public static Func<IWebDriver, bool> UrlContains(string fraction)
        {
            return driver =>
            {
                if (driver.Url.Contains(fraction))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }




    }
}
