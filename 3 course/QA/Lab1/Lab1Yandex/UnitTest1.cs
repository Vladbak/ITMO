using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace Lab1Yandex
{
    public class UnitTest1
    {
        IWebDriver driver;
        string pathToChrome = @"C:\Users\Corrector\Documents\ITMO\3 course\QA\Lab1\packages\Selenium.WebDriver.ChromeDriver.2.43.0\driver\win32";
        string login = "vladbak.ivanov";
        string password = "testpassword";
        string sendTo = "vladbaksh@gmail.com";

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver(pathToChrome);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(5);
        }

        [SetUp]
        public void logIn()
        {
            driver.Url = "https://mail.yandex.ru/";

            //HeadBanner-ButtonsWrapper
            driver.FindElement(By.CssSelector("div.HeadBanner-ButtonsWrapper a.HeadBanner-Button-Enter")).Click();

            IWebElement loginInput = driver.FindElement(By.CssSelector("input[name='login']"));

            loginInput.SendKeys(login);

            IWebElement passwordInput = driver.FindElement(By.CssSelector("input[name='passwd']"));
            passwordInput.SendKeys(password);

            driver.FindElement(By.CssSelector("div.passport-Domik-Form-Field button[type='submit']")).Click();
        }

        [Test]
        public void checkForNewMail()
        {
            driver.FindElement(By.CssSelector("div.mail-ComposeButton-Wrap span.mail-ComposeButton-Refresh")).Click();
        }

        [Test]
        public void NewMail()
        {
            driver.FindElement(By.CssSelector("div.mail-ComposeButton-Wrap a")).Click();

            IWebElement email = driver.FindElement(By.CssSelector("div.mail-Compose-Field-Input div.js-compose-field.mail-Bubbles"));

            email.SendKeys(sendTo);

            IWebElement text = driver.FindElement(By.CssSelector("div.cke_wysiwyg_div.cke_reset.cke_enable_context_menu.cke_editable.cke_editable_themed.cke_contents_ltr.cke_show_borders"));

            text.SendKeys("Hello!");

            driver.FindElement(By.CssSelector("div.mail-Compose-Field-Actions_left button")).Click();
        }

        [Test]
        public void ToolbarButtons()
        {
            string[] toolbarButtons = { "div.ns-view-toolbar-button-delete",
                                        "div.ns-view-toolbar-button-forward",
                                        "div.ns-view-toolbar-button-spam" };

            for (int i=0; i< toolbarButtons.Length; i++)
            {
                IWebElement button = driver.FindElement(By.CssSelector(toolbarButtons[i]));
                Assert.IsTrue(button.GetAttribute("class").Contains("is-disabled"));
            }

            IWebElement letterCheckBox = driver.FindElement(By.CssSelector("div.ns-view-container-desc.mail-MessagesList.js-messages-list label"));

            letterCheckBox.Click();

            for (int i = 0; i < toolbarButtons.Length; i++)
            {
                IWebElement button = driver.FindElement(By.CssSelector(toolbarButtons[i]));
                Assert.IsFalse(button.GetAttribute("class").Contains("is-disabled"));
            }

            letterCheckBox.Click();

            for (int i = 0; i < toolbarButtons.Length; i++)
            {
                IWebElement button = driver.FindElement(By.CssSelector(toolbarButtons[i]));
                Assert.IsTrue(button.GetAttribute("class").Contains("is-disabled"));
            }
        }

        [Test]
        public void CheckReadState()
        {
            string markAsRead = "div.ns-view-toolbar-button-mark-as-read";
            string markAsUnRead = "div.ns-view-toolbar-button-mark-as-unread";

            IWebElement letterCheckBox = driver.FindElement(By.CssSelector("div.ns-view-container-desc.mail-MessagesList.js-messages-list label"));

            letterCheckBox.Click();

            IWebElement letter = driver.FindElement(By.CssSelector("a.mail-MessageSnippet"));
            IWebElement markAsReadButton = driver.FindElement(By.CssSelector(markAsRead));
            IWebElement markAsUnReadButton = driver.FindElement(By.CssSelector(markAsUnRead));

            if (letter.GetAttribute("class").Contains("is-unread"))
            {
                // if letter is unread
                Assert.IsFalse(markAsReadButton.GetAttribute("class").Contains("is-hidden"));
                Assert.IsTrue(markAsUnReadButton.GetAttribute("class").Contains("is-hidden"));
            }
            else
            {
                //if letter is read
                Assert.IsTrue(markAsReadButton.GetAttribute("class").Contains("is-hidden"));
                Assert.IsFalse(markAsUnReadButton.GetAttribute("class").Contains("is-hidden"));
            }
        }

        [Test]
        public void ChangeReadState()
        {
            string markAsReadString = "div.ns-view-toolbar-button-mark-as-read";
            string markAsUnReadString = "div.ns-view-toolbar-button-mark-as-unread";
            string letterCheckBoxString = "div.ns-view-container-desc.mail-MessagesList.js-messages-list label";
            string letterString = "a.mail-MessageSnippet";

            IWebElement letterCheckBox = driver.FindElement(By.CssSelector(letterCheckBoxString));

            letterCheckBox.Click();

            IWebElement letter = driver.FindElement(By.CssSelector(letterString));

            if (letter.GetAttribute("class").Contains("is-unread"))
            {
                // if letter is unread
                Assert.IsTrue(MarkAsRead(markAsReadString, letterString));
                driver.FindElement(By.CssSelector(letterCheckBoxString)).Click();
                Assert.IsTrue(MarkAsUnread(markAsUnReadString, letterString));
            }
            else
            {
                //if letter is read
                Assert.IsTrue(MarkAsUnread(markAsUnReadString, letterString));
                driver.FindElement(By.CssSelector(letterCheckBoxString)).Click();
                Assert.IsTrue(MarkAsRead(markAsReadString, letterString));
            }
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }

        private bool MarkAsUnread(string markAsUnRead, string letterString)
        {
            driver.FindElement(By.CssSelector(markAsUnRead)).Click();
            Thread.Sleep(3000);
            var letter = driver.FindElement(By.CssSelector(letterString));
            return letter.GetAttribute("class").Contains("is-unread");
        }

        private bool MarkAsRead(string markAsRead, string letterString)
        {
            driver.FindElement(By.CssSelector(markAsRead)).Click();
            Thread.Sleep(3000);
            var letter = driver.FindElement(By.CssSelector(letterString));
            return !letter.GetAttribute("class").Contains("is-unread");
        }
    }
}
