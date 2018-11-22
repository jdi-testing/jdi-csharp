using System;
using JDI_Commons;
using Epam.JDI.Core.Logging;
using Epam.JDI.Core.Settings;
using JDI_Web.Selenium.Base;
using JDI_Web.Selenium.Elements.Base;
using static Epam.JDI.Core.ExceptionUtils;

namespace JDI_Web.Selenium.Elements.WebActions
{
    public class ActionInvoker
    {
        private readonly WebBaseElement _element;
        public static ActionScenarios ActionScenrios = new ActionScenarios();

        public ActionInvoker(WebBaseElement element)
        {
            JDISettings.NewTest();
            _element = element;
        }
        
        public TResult DoJActionResult<TResult>(string actionName, Func<WebBaseElement, TResult> action,
            Func<TResult, string> logResult = null, LogLevels level = LogLevels.Info)
        {
            return ActionWithException(() =>
            {
                ProcessDemoMode();
                return (TResult) ActionScenarios.ResultScenario(_element, actionName,
                    element =>
                    {
                        var result =
                            ActionWithException(() => new Timer(JDISettings.Timeouts.CurrentTimeoutSec)
                                    .GetResultByCondition(() => action.Invoke(element), res => true),
                                ex => $"Do action {actionName} failed. Can't got result. Reason: {ex}");
                        if (result == null)
                            throw JDISettings.Exception($"Do action {actionName} failed. Can't got result");
                        return logResult == null
                            ? result.ToString()
                            : logResult.Invoke(result);
                    },
                    level);
            }, ex => $"Failed to do '{actionName}' action. Reason: {ex}");
        }
        
        public void DoJAction(string actionName, Action<WebBaseElement> action, LogLevels level = LogLevels.Info)
        {
            TimerExtensions.ForceDone(() => {
                ProcessDemoMode();
                ActionScenarios.ActionScenario(_element, actionName, action, level);
            });
        }

        public void ProcessDemoMode()
        {
            if (!JDISettings.IsDemoMode) return;
            if (_element is WebElement element)
                element.Highlight(JDISettings.HighlightSettings);
        }
    }
}
