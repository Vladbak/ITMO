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

            Thread.Sleep(3000);

            driver.Url = dashboardUrl;

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
        public void LikePost()
        {
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(".post_container .like")).Click();
        }

        [Test]
        public void Reblog()
        {
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(".post_container a.reblog")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("button.create_post_button")).Click();
            driver.Url = dashboardUrl;
        }

        [Test]
        public void Reply()
        {
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(".post_container .reply")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(".text-input")).SendKeys("Nice!");
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(".post-activity-reply button")).Click();
        }

        [Test]
        public void SendPost()
        {
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(".post_container .share")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//a//*[@alt='tumblrbot']/../../..")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        [Test]
        public void SubscribeToBlog()
        {
            Thread.Sleep(1000);
            var blogTitle = driver.FindElement(By.CssSelector(".recommended_tumblelogs li a")).GetAttribute("data-peepr").Split('\"')[3];

            driver.FindElement(By.CssSelector(".recommended_tumblelogs li a.follow")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("[title='Учетная запись']")).Click();
            driver.FindElement(By.CssSelector(".popover_menu_list li a[href='/following']")).Click();
            //.follower div.info .name a
            var list = driver.FindElements(By.CssSelector(".follower"));
            var subscriptionFound = false;
            string title=string.Empty;
            foreach (var f in list)
            {
                try
                {
                    title = f.FindElement(By.CssSelector("div.info .name a")).GetAttribute("innerText");
                }
                catch (Exception e) { continue; };
                if (title.ToLower().Equals(blogTitle.ToLower()))
                {
                    subscriptionFound = true;
                    break;
                }
            }
            Assert.IsTrue(subscriptionFound);
            //

        }

        [Test]
        public void ClickOnAllTabs()
        {
            var tabsList = driver.FindElements(By.CssSelector(".tab_bar .tab"));
            for(int i=0; i< tabsList.Count; i++)
            {
                tabsList[i].Click();
                Thread.Sleep(3000);
                tabsList = driver.FindElements(By.CssSelector(".tab_bar .tab"));
                Thread.Sleep(1000);
            }
        }

        [Test]
        public void WriteToBot()
        {
            driver.FindElement(By.CssSelector("[title='Сообщения']")).Click();
            driver.FindElement(By.CssSelector(".inbox-compose-toggle")).Click();
            driver.FindElement(By.CssSelector(".inbox-compose-input input")).SendKeys("tumblrbot");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("[data-subview='searchResultView'] .inbox-recipient")).Click();

            //number of messages before new msg from me
            var numberOfMessages = driver.FindElements(By.CssSelector(".conversation-message")).Count;
            driver.FindElement(By.CssSelector(".messaging-conversation-wrapper textarea")).SendKeys(HelloMessage);
            driver.FindElement(By.CssSelector(".messaging-conversation-wrapper button[type='submit']")).Click();

            //number after my new msg
            var newNumberOfMessages = driver.FindElements(By.CssSelector(".conversation-message")).Count;

            Assert.IsTrue(newNumberOfMessages > numberOfMessages);

            //waiting for bot response
            Thread.Sleep(5000);

            numberOfMessages = newNumberOfMessages;
            //number after bot responses 
            newNumberOfMessages = driver.FindElements(By.CssSelector(".conversation-message")).Count;
            Assert.IsTrue(newNumberOfMessages > numberOfMessages);
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
