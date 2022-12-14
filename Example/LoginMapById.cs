using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Railflow.NUnit.TestRail.Reporter;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Example
{

    class LoginMapByIdTest
    {
        private static readonly String LOGIN_PAGE_URL = "https://test.railflow.io/login";

        private IWebDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), "MatchingBrowser");
        }

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");                     
            driver = new ChromeDriver(options)
            {
                Url = LOGIN_PAGE_URL
            };
        }

        [Railflow(TestRailIds = new[] { 1825, 1826 })]
        [Test]
        public void LogInCorrectCredentials()
        {
            new LoginPage(this.driver).LogIn("sergey@railflow.io", "myS3crEt");
            Assert.That(this.driver.Url.EndsWith("dashboard"));
            Assert.AreEqual("Oplavin Sergey", driver.FindElement(By.Id("myAccountButton")).Text);
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                CurrentTest.TakeScreenshotIfLastTestFailed((WebDriver)driver);
                driver.Quit();
            }
        }


    }
}
