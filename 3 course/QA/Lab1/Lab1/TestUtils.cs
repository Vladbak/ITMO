using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab1
{
    class TestUtils
    {
        IWebDriver driver;
        string pathToChrome = @"C:\Users\Corrector\Documents\ITMO\3 course\QA\Lab1\packages\Selenium.WebDriver.ChromeDriver.2.43.0\driver\win32";
        string login = "vladbaksh@gmail.com";
        string password = "testpassword";
        string dashboardUrl = "https://www.tumblr.com/dashboard";
        string inboxUrl = "https://www.tumblr.com/inbox";
        string tumblrUrl= "https://www.tumblr.com";
        string HelloMessage = "Hello!";

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(pathToChrome);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(5);
        }

        [SetUp]
        public void LogIn()
        {
            driver.Url = tumblrUrl;
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

            Thread.Sleep(2000);
            Assert.AreEqual(driver.Url, dashboardUrl);
        }

        [Test]
        public void NewPostsAreas()
        {
            List<IWebElement> postButtons = driver.FindElements(By.CssSelector(".new_post_label")).ToList();
            
            postButtons.ForEach((IWebElement element) => {

                Assert.IsFalse(driver.FindElement(By.CssSelector("#new_post_buttons")).GetAttribute("class").Contains("is_persistent"));

                element.Click();
                Thread.Sleep(2000);
                Assert.IsTrue(driver.FindElement(By.CssSelector("#new_post_buttons")).GetAttribute("class").Contains("is_persistent"));

                driver.FindElement(By.CssSelector(".post-form--controls button.tx-button")).Click();
                Thread.Sleep(2000);
                Assert.IsFalse(driver.FindElement(By.CssSelector("#new_post_buttons")).GetAttribute("class").Contains("is_persistent"));
            });
        }

        [Test]
        public void CreateNewPost()
        {
            driver.FindElement(By.CssSelector(".new_post_label")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.CssSelector("[data-js-richtexteditor]")).SendKeys(HelloMessage);

            driver.FindElement(By.CssSelector("button.button-area.create_post_button")).Click();

            var postList = driver.FindElements(By.CssSelector("li.post_container div.post_body")).ToList();

            bool wasPostCreated = false;
            foreach(var post in postList)
            {
                if (post.GetAttribute("innerText").Contains(HelloMessage))
                {
                    wasPostCreated = true;
                    break;
                }
            }
            Assert.IsTrue(wasPostCreated);
        }

        [Test]
        public void GoToInboxAndBack()
        {
            Assert.AreEqual(driver.Url, dashboardUrl);
            driver.FindElement(By.CssSelector("[title='Входящие']")).Click();
            Thread.Sleep(2000);
            Assert.AreEqual(driver.Url, inboxUrl);
            driver.FindElement(By.CssSelector("[title='Лента']")).Click();
            Thread.Sleep(2000);
            Assert.AreEqual(driver.Url, dashboardUrl);
        }

        [Test]
        public void WriteToBot()
        {
            driver.FindElement(By.CssSelector("[title='Сообщения']")).Click();
            driver.FindElement(By.CssSelector(".inbox-main [href='/conversation/new/tumblrbot']")).Click();

            //number of messages before new msg from me
            var numberOfMessages = driver.FindElements(By.CssSelector(".conversation-message")).Count;
            driver.FindElement(By.CssSelector(".messaging-conversation-wrapper textarea")).SendKeys(HelloMessage);
            driver.FindElement(By.CssSelector(".messaging-conversation-wrapper button[type='submit']")).Click();

            //number after my new msg
            var newNumberOfMessages = driver.FindElements(By.CssSelector(".conversation-message")).Count;

            Assert.AreEqual(newNumberOfMessages - numberOfMessages, 1);

            //waiting for bot response
            Thread.Sleep(5000);

            numberOfMessages = newNumberOfMessages;
            //number after bot responses 
            newNumberOfMessages = driver.FindElements(By.CssSelector(".conversation-message")).Count;

            Assert.AreEqual(newNumberOfMessages - numberOfMessages, 1);
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
