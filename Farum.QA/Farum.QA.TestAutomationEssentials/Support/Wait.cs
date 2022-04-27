using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Farum.QA.TestAutomationEssentials.Support
{
    /// <summary>
    /// Class intended to contain more complex waiting methods.
    /// </summary>
    public static class Wait
    {
        /// <summary>
        /// Mimics Wait.Until; executes certain task (eg. method) within given timeout and pauses if task has returned false; retries until task returns true or timeout time is exceeded.
        /// DO NOT USE WITH WEBDRIVER. Use in API calls.
        /// </summary>
        /// <param name="task">Task to be executed and retried until it succeeds. Example value: () => SomeMethod(arg1,arg2)</param>
        /// <param name="timeout">Time till method tries to execute given task.</param>
        /// <param name="pause">Time for which thread pauses and retries the task.</param>
        public static bool UntilSuccessOrTimeout(Func<bool> task, TimeSpan timeout, TimeSpan pause)
        {
            if (pause.TotalMilliseconds < 0)
            {
                throw new ArgumentException("pause must be >= 0 miliseconds");
            }
            var stopwatch = Stopwatch.StartNew();
            do
            {
                if (task()) { return true; }
                Thread.Sleep((int)pause.TotalMilliseconds);
            } while (stopwatch.Elapsed < timeout);
            return false;
        }

        public static void UntilPageHasChangedDuringAction(Drivers.WebDriver webDriver, Action task)
        {
            var currentPageTitle = webDriver.Current.Title;
            task();
            webDriver
                .Wait("Page did not change in expected time!", TimeSpan.FromSeconds(12))
                .Until(ExpectedConditions.PageTitleChangedFrom(currentPageTitle));
        }
    }
}
