using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using System.Linq;
using static Epam.JDI.Core.Settings.JDISettings;
using static JDI_UIWebTests.UIObjects.TestSite;

namespace JDI_ImageToWebElement.Tests
{
    
    public class GetElementByImage
    {
        private string _imagesPath = Path.GetDirectoryName(typeof(GetElementByImage).Assembly.Location) + "\\Images\\";
        [Test]
        public void GetCalculateButtonLocators()
        {
            Logger.Info("Try to get calculate button locators");
            var locators = 
                ImagePattnerSerach.GetElementLocatorsByElementImage(MetalsColorsPage.WebDriver, _imagesPath + "calculate.png");

            foreach(var locator in locators)
            {
                Logger.Info("Css locator " + locator.CssLocator);
                Logger.Info("xPath locator " + locator.XPathLocator);
                foreach(var item in locator.ElementAttributesLocators)
                {
                    Logger.Info("Element attribute locator key: " + item.Key + " value " + item.Value);
                }
                var element = MetalsColorsPage.WebDriver.FindElement(By.CssSelector(locator.CssLocator));
                Assert.IsTrue(MetalsColorsPage.CalculateButton.WebElement.Text == element.Text);

                Assert.IsTrue(MetalsColorsPage.CalculateButton.WebElement.Text == MetalsColorsPage.WebDriver.FindElement(By.XPath(locator.XPathLocator)).Text);

            }

            Assert.IsTrue(MetalsColorsPage.CalculateButton.WebElement.GetAttribute("Id") == locators[0].WebElement.GetAttribute("Id"));

            
            
        }

        [Test]
        public void GetCheckBoxButtonLocators()
        {
            Logger.Info("Try to get calculate button locators");
            var locators =
                ImagePattnerSerach.GetElementLocatorsByElementImage(MetalsColorsPage.WebDriver, _imagesPath + "checkbox.png");

            foreach (var locator in locators)
            {
                Logger.Info("Css locator " + locator.CssLocator);
                Logger.Info("xPath locator " + locator.XPathLocator);
                foreach (var item in locator.ElementAttributesLocators)
                {
                    Logger.Info("Element attribute locator key: " + item.Key + " value " + item.Value);
                }

                if(locator.WebElement.Text == "Water")
                {
                    var element = MetalsColorsPage.WebDriver.FindElement(By.CssSelector(locator.CssLocator));
                    Assert.IsTrue(MetalsColorsPage.CbWater.WebElement.Text == element.Text);

                    Assert.IsTrue(MetalsColorsPage.CbWater.WebElement.Text == MetalsColorsPage.WebDriver.FindElement(By.XPath(locator.XPathLocator)).Text);
                }
            }

            //var linq = locators.Where(x=>x.WebElement.GetAttribute("NaM"))
            Assert.IsTrue(MetalsColorsPage.CbWater.WebElement.Text == locators.Where(x=>x.WebElement.Text == "Water").Select(x=>x.WebElement.Text).First());
        }
    }
}
