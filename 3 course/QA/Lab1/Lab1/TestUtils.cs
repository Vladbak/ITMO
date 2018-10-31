using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class TestUtils
    {
        IWebDriver driver;
        string pathToChrome = @"C:\Users\Corrector\Documents\ITMO\3 course\QA\Lab1\packages\Selenium.WebDriver.ChromeDriver.2.43.0\driver\win32";
        string login = "vladbaksh@gmail.com";
        string password = "testpassword";

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver(pathToChrome);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(5);
        }

        [SetUp]
        public void logIn()
        {
            driver.Url = "https://www.tumblr.com";
            IWebElement signInButton = driver.FindElement(By.CssSelector("button#signup_login_button"));

            if (signInButton != null)
            {
                signInButton.Click();
            }

            IWebElement loginInput = driver.FindElement(By.CssSelector("input#signup_determine_email"));
            

            loginInput.SendKeys(login);
            driver.FindElement(By.CssSelector("span.signup_determine_btn.active")).Click();
            driver.FindElement(By.CssSelector("div.magiclink_password_container.chrome")).Click();
            
            IWebElement passwordInput = driver.FindElement(By.CssSelector("input#signup_password"));
            passwordInput.SendKeys(password);
            driver.FindElement(By.CssSelector("span.signup_login_btn.active")).Click();

        }

        [Test]
        public void newPostElements()
        {
            //string[] postTypes = { "regular", "photo", "quote", "link", "conversation", "audio", "video" };

            List<IWebElement> postButtons = driver.FindElements(By.CssSelector(".new_post_label")).ToList();
            
            postButtons.ForEach((IWebElement element) => {
                
                element.Click();
                driver.FindElement(By.CssSelector(".post_container.new_post_buttons_container"));

                driver.FindElement(By.CssSelector("div.post-form--controls div.control.left")).Click();
            });
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }


    }
}
