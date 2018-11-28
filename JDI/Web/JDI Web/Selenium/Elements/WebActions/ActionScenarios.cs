using System;
using Epam.JDI.Core.Logging;
using Epam.JDI.Core.Reporting;
using JDI_Web.Selenium.Base;
using Timer = JDI_Commons.Timer;
using static Epam.JDI.Core.ExceptionUtils;
using static Epam.JDI.Core.Settings.JDISettings;

namespace JDI_Web.Selenium.Elements.WebActions
{
    public class ActionScenarios
    {
        public static string DoLog = "New";

        public static Action<WebBaseElement, string, Action<WebBaseElement>, LogLevels> ActionScenario =
            (element, actionName, action, level) =>
            {
                if (DoLog.Equals("New"))
                {
                    DoLog = "Before";
                    element.LogAction(actionName, level);
                    DoLog = "After";
                }

                var timer = new Timer();
                new Timer(Timeouts.CurrentTimeoutSec).Wait(() =>
                {
                    action(element);
                    return true;
                });
                if (DoLog.Equals("After"))
                {
                    Logger.Info(actionName + " done");
                    DoLog = "New";
                }
                PerformanceStatistic.AddStatistic(timer.TimePassed.TotalMilliseconds);
            };

        public static Func<WebBaseElement, string, Func<WebBaseElement, string>, LogLevels, object> ResultScenario =
            (element, actionName, getResultString, level) =>
            {
                if (DoLog.Equals("New"))
                {
                    DoLog = "Before";
                    element.LogAction(actionName, level);
                    DoLog = "After";
                }
                var timer = new Timer();
                var result = getResultString(element);
                var timePassed = timer.TimePassed.TotalMilliseconds;
                PerformanceStatistic.AddStatistic(timer.TimePassed.TotalMilliseconds);
                if (DoLog.Equals("After"))
                {
                    ToLog($"Get result '{result}' in {timePassed / 1000:F} seconds", level);
                    DoLog = "New";
                }
                return result;
            };
    }
}
