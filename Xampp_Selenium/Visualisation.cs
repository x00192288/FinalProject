using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Xampp_Selenium
{
    [TestFixture]
    public class VisualisationTest
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
            // Navigate to .NET web application
            driver.Navigate().GoToUrl("https://localhost:7030/");

            // Perform actions and assertions
            IWebElement element = driver.FindElement(By.CssSelector("a.nav-link.text-dark[href='/Visualisation/Histogram']"));
            element.Click();

            IWebElement form = driver.FindElement(By.Id("filterForm"));

            // Find the input elements by their IDs
            IWebElement yearInput = form.FindElement(By.Id("timestampYear"));
            IWebElement monthInput = form.FindElement(By.Id("timestampMonth"));
            IWebElement dayInput = form.FindElement(By.Id("timestampDay"));

            // Enter values into the input fields
            yearInput.SendKeys("2023");
            monthInput.SendKeys("12");
            dayInput.SendKeys("04");

            // Submit the form
            form.Submit();
        }

        [TearDown]
        public void Teardown()
        {
            // Close the browser
            driver?.Quit();
        //    driver?.Close();
        }
    }
}
