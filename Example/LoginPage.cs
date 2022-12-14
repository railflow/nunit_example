using OpenQA.Selenium;
using System;

namespace Example
{
    class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void LogIn(String email, String password)
        {
            getEmailInput().SendKeys(email);
            getPasswordInput().SendKeys(password);
            getLoginButton().Click();
        }

        public IWebElement getEmailInput()
        {
            return driver.FindElement(By.Id("email"));
        }

        public IWebElement getPasswordInput()
        {
            return driver.FindElement(By.Id("password"));
        }

        public IWebElement getLoginButton()
        {
            return driver.FindElement(By.Id("loginButton"));
        }

        public IWebElement getErrorArea()
        {
            return driver.FindElement(By.ClassName("alert")).FindElement(By.XPath("//li"));
        }

    }
}
