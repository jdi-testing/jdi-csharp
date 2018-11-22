using System;
using Epam.JDI.Core.Logging;
using Epam.JDI.Core.Reporting;
using Epam.JDI.Core.Settings;
using JDI_Web.Selenium.Base;
using Timer = JDI_Commons.Timer;
using static Epam.JDI.Core.ExceptionUtils;

namespace JDI_Web.Selenium.Elements.WebActions
{
    public class ActionScenarios
    {
        public static Action<WebBaseElement, string, Action<WebBaseElement>, LogLevels> ActionScenario =
            (element, actionName, action, level) =>
            {
                element.LogAction(actionName, level);
                var timer = new Timer();
                new Timer(JDISettings.Timeouts.CurrentTimeoutSec).Wait(() =>
                {
                    action(element);
                    return true;
                });
                JDISettings.Logger.Info(actionName + " done");
                PerformanceStatistic.AddStatistic(timer.TimePassed.TotalMilliseconds);
            };

        public static Func<WebBaseElement, string, Func<WebBaseElement, string>, LogLevels, object> ResultScenario =
            (element, actionName, getResultString, level) =>
            {
                element.LogAction(actionName);
                var timer = new Timer();
                var result = getResultString(element);
                var timePassed = timer.TimePassed.TotalMilliseconds;
                PerformanceStatistic.AddStatistic(timer.TimePassed.TotalMilliseconds);
                JDISettings.ToLog($"Get result '{result}' in {timePassed / 1000:F} seconds", level);
                return result;
            };
    }
}
