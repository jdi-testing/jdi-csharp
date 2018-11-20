using System;
using Epam.JDI.Core.Base;
using JDI_Web.Selenium.Base;
using JDI_Web.Selenium.DriverFactory;
using JDI_Web.Settings;
using OpenQA.Selenium;
using static JDI_Web.Settings.WebSettings;

namespace JDI_Web.Selenium.Elements.Composite
{
    public class WebSite : Application
    {
        public IWebDriver WebDriver => WebSettings.WebDriverFactory.GetDriver(DriverName);
        public string Url => WebDriver.Url;
        public string BaseUrl => new Uri(WebDriver.Url).GetLeftPart(UriPartial.Authority);
        public string Title => WebDriver.Title;
        private static WebCascadeInit CascadeInit => new WebCascadeInit();

        public static void Init(Type siteType, string domain = null, Func<IWebDriver> driver = null)
        {
            if (driver != null)
                UseDriver(driver);
            if (domain != null)
                Domain = domain;
            InitFromProperties();
            CascadeInit.InitStaticPages(siteType, WebSettings.WebDriverFactory.CurrentDriverName);
            CurrentSite = siteType;
        }

        public static T Init<T>(Type siteType, string driverName) where T : Application
        {
            return CascadeInit.InitPages<T>(siteType, driverName);
        }

        public static T Init<T>(Type siteType, DriverTypes driverType = DriverTypes.Chrome) where T : Application
        {
            return Init<T>(siteType, UseDriver(driverType));
        }

        public T Init<T>(string driverName) where T : Application
        {
            DriverName = driverName;
            return Init<T>(GetType(), driverName);
        }

        public T Init<T>(DriverTypes driverType = DriverTypes.Chrome) where T : Application
        {
            DriverName = UseDriver(driverType);
            return Init<T>(DriverName);
        }

        public T InitScope<T>(object scope) where T : Application
        {
            return Init<T>(GetType());
        }

        public static void Open()
        {
            WebSettings.WebDriver.Navigate().GoToUrl(Domain);
        }

        public void OpenUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public void OpenBaseUrl()
        {
            WebDriver.Navigate().GoToUrl(BaseUrl);
        }

        public void Refresh()
        {
            WebDriver.Navigate().Refresh();
        }

        public void Forward()
        {
            WebDriver.Navigate().Forward();
        }

        public void Back()
        {
            WebDriver.Navigate().Back();
        }
    }
}
