﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JDI_Commons;
using OpenQA.Selenium;
using static Epam.JDI.Core.ExceptionUtils;

namespace JDI_Web.Selenium.DriverFactory
{
    public static class WebDriverByUtils
    {
        public static Func<string, By> GetByFunc(this By by)
        {
            return ByTypes.FirstOrDefault(el => by.ToString().Contains(el.Key)).Value;
        }
        private static string GetBadLocatorMsg(this string byLocator, params object[] args)
        {
            return "Bad locator template '" + byLocator + "'. Args: " + args.Select(el => el.ToString()).Print(", ", "'{0}'") + ".";
        }
        public static By FillByTemplate(this By by, params object[] args)
        {
            var byLocator = by.GetByLocator();
            if (!byLocator.Contains("{0}"))
                throw new Exception(GetBadLocatorMsg(byLocator, args));
            var locator = byLocator;
            byLocator = ActionWithException(
                () => string.Format(locator, args),
                ex => GetBadLocatorMsg(locator, args));
            return by.GetByFunc()(byLocator);
        }
        
        public static By CorrectXPath(this By byValue)
        {
            return byValue.ToString().Contains("By.xpath: //")
                    ? byValue.GetByFunc()(new Regex("//").Replace(byValue.GetByLocator(), "./", 1))
                    : byValue;
        }

        public static bool ContainsRoot(this By by)
        {
            return by != null && by.ToString().Contains(": *root*");
        }
        public static By TrimRoot(this By by)
        {
            var byLocator = by.GetByLocator().Replace("*root*", " ").Trim();
            return GetByFunc(by)(byLocator);
        }

        public static By CopyBy(By by)
        {
            var byLocator = GetByLocator(by);
            return GetByFunc(by)(byLocator);
        }
        
        /*public static string GetByName(By by)
        {
            Matcher m = Pattern.compile("By\\.(?<locator>.*):.*").matcher(by.ToString());
            if (m.find())
                return m.group("locator");
            throw new RuntimeException("Can't get By name for: " + by);
        }*/
        public static string GetByLocator(this By by)
        {
            var byAsString = by.ToString();
            var index = byAsString.IndexOf(": ") + 2;
            return byAsString.Substring(index);
        }
        public static string Print(this By by)
        {
            if (by.ToString().Contains("CssSelector"))
                return "css=" + by.GetByLocator();
            if (by.ToString().Contains("XPath"))
                return "xpath=" + by.GetByLocator();
            if (by.ToString().Contains("ClassName"))
                return "class=" + by.GetByLocator();
            if (by.ToString().Contains("Id"))
                return "id=" + by.GetByLocator();
            if (by.ToString().Contains("LinkText") || by.ToString().Contains("PartialLinkText"))
                return "link=" + by.GetByLocator();
            if (by.ToString().Contains("TagName"))
                return "tag=" + by.GetByLocator();
            return "name=" + by.GetByLocator();
        }

        private static readonly Dictionary<string, Func<string, By>> ByTypes = new Dictionary<string, Func<string, By>>
        {
            {"CssSelector", By.CssSelector},
            {"ClassName", By.ClassName},
            {"Id", By.Id},
            {"LinkText", By.LinkText},
            {"Name", By.Name},
            {"PartialLinkText", By.PartialLinkText},
            {"TagName", By.TagName},
            {"XPath", By.XPath}
        };
    }
}
