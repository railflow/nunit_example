using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Railflow.NUnit.TestRail.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Example
{

    class LoginTest
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

        [Test]
        public void VerifyLoginPageTitle()
        {
            Assert.AreEqual("Invoice Ninja | Free Source-Available Online Invoicing", driver.Title);
        }

        [Test]
        public void LogInCorrectCredentials()
        {
            new LoginPage(this.driver).LogIn("sergey@railflow.io", "myS3crEt");
            Assert.That(this.driver.Url.EndsWith("dashboard"));
            Assert.AreEqual("Oplavin Sergey", driver.FindElement(By.Id("myAccountButton")).Text);
        }

        [Test]
        public void LogInWrongCredentials()
        {
            LoginPage loginPage = new LoginPage(this.driver);
            loginPage.LogIn("user@railflow.io", "password");
            Assert.AreEqual(LOGIN_PAGE_URL, driver.Url);
            Assert.AreEqual("These credentials do not match our records", loginPage.getErrorArea().Text);
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
