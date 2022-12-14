using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Railflow.NUnit.TestRail.Reporter;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Example
{
    [Railflow(
        Title = "Login with Railflow Attribute",
        CasePriority = "Critical",
        CaseType = "Railflow",
        CaseFields = new[] { "Required text field = Railflow on class level", "estimate = 10s" },
        ResultFields = new[] { "Custom field =  Result for Railflow on class level", "version = 2.0" },
        SmartAssignment = new[] { "user1@company.net", "user2@company.net" }
        )]
    class LoginWithRailflowOnClass
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
            Assert.AreEqual("Sergey Oplavin", driver.FindElement(By.Id("myAccountButton")).Text);
        }

        [Test]
        public void LogInWrongCredentials()
        {
            LoginPage loginPage = new LoginPage(this.driver);
            loginPage.LogIn("user@railflow.io", "password");
            Assert.AreEqual(LOGIN_PAGE_URL, driver.Url);
            Assert.AreEqual("These credentials do not match our records", loginPage.getErrorArea().Text);
        }

        [Test]
        public void LogInWithAdminCredentials()
        {
            LoginPage loginPage = new LoginPage(this.driver);
            loginPage.LogIn("admin@youcompany.io", "password");
            Assert.AreEqual(LOGIN_PAGE_URL, driver.Url);
            Assert.AreEqual("Admin admin", driver.FindElement(By.Id("myAccountButton")).Text);
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                // take screenshot for failing tests
                CurrentTest.TakeScreenshotIfLastTestFailed((WebDriver)driver);
                driver.Quit();
            }
        }
    }
}
