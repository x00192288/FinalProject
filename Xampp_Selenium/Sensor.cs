using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Xampp_Selenium
{
    [TestFixture]
    public class SensorTest
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {

            // Initialize the browser driver
            InitializeChromeDriver();


        }

        private void InitializeChromeDriver()
        {
            string pathToChromeDriver = @"/Users/adamgeraghty/Downloads/chromedriver-mac-x64/chromedriver";
            Environment.SetEnvironmentVariable("webdriver.chrome.driver", pathToChromeDriver);
            driver = new ChromeDriver();
        }

        [Test]
        public void TestMyApp()
        {
            // Navigate to your .NET web application
            driver.Navigate().GoToUrl("https://localhost:7030/");

            // Perform actions and assertions
            IWebElement element = driver.FindElement(By.CssSelector("a.nav-link.text-dark[href='/Temperature/Details']"));
            element.Click();
            IWebElement button = driver.FindElement(By.CssSelector("button#moreInfoBtn.btn.btn-primary"));

            button.Click();
            //  IWebElement element = driver.FindElement(By.Id("yourElementId"));
            //  Assert.IsNotNull(element);

            // Add more test steps as needed
        }

        [TearDown]
        public void Teardown()
        {
            // Close the browser
            driver?.Quit();
            driver?.Close();

        }
    }
}
