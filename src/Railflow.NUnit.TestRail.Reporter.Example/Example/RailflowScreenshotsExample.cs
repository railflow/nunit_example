using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using Railflow.NUnit.TestRail.Reporter;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Example
{
    public class RailflowScreenshotsExample
    {
        private ChromeDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Will run driver in headless-mode (without UI)
            var options = new ChromeOptions();
            options.AddArguments("--headless");

            // Setup chrome driver matching browser on current machine
            // NOTE: This is a must for CI tests (where browser version isn't known upfront)
            new DriverManager().SetUpDriver(new ChromeConfig(), "MatchingBrowser");

            // Instantiate driver and navigate to specific URL
            driver = new ChromeDriver(options)
            {
                Url = "https://duckduckgo.com"
            };

            // Wait the page to load
            Thread.Sleep(2000);
        }

        [Test]
        public void ScreenshotExample1()
        {
            // Take multiple screenshots and associate with current test
            RailflowScreenshot.Take(driver);
            RailflowScreenshot.Take(driver);
            RailflowScreenshot.Take(driver);
        }

        [Test]
        public void ScreenshotExample2()
        {
            var screenshot = driver.GetScreenshot();

            // Associate existing screenshot with current test
            RailflowScreenshot.AddExisting(screenshot);
        }

        [TearDown]
        public void TearDown()
        {
            // Take screenshot only if the last test failed
            RailflowScreenshot.TakeIfLastTestFailed(driver);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Quit and dispose
            driver.Quit();
        }
    }
}
