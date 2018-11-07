using System;
using System.Threading;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace Lab1Yandex
{
    public class UnitTest1
    {
        IWebDriver driver;
        string pathToChrome = @"C:\Users\Corrector\Documents\ITMO\3 course\QA\Lab1\packages\Selenium.WebDriver.ChromeDriver.2.43.0\driver\win32";
        string login = "vladbak.ivanov";
        string password = "testpassword";
        string HelloMessage = "Hello!";
        string SimpleText = "SimpleText";
        string BoldText = "Bold text";
        string ItalicText = "Italic Text";
        string UnderlinedText = "Underlined text";
        string LineThroughText = "Line Through Text";
        string Link = "www.yandex.ru";
        string LinkTitle = "Yandex";
        string LinkToImage = "https://avatars.mds.yandex.net/get-bunker/128809/2242b0f7baf7f84a7d0d6cd6020acd311fba9df8/orig";
        string Quote = "To be or not to be";
        string ColorfulText = "Colorful Text";
        string FontText = "Custom font text";

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver(pathToChrome);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
        }

        [SetUp]
        public void logIn()
        {
            driver.Url = "https://mail.yandex.ru/";

            //HeadBanner-ButtonsWrapper
            driver.FindElement(By.CssSelector("div.HeadBanner-ButtonsWrapper a.HeadBanner-Button-Enter")).Click();

            IWebElement loginInput = driver.FindElement(By.CssSelector("input[name='login']"));

            loginInput.SendKeys(login);
            try
            {
                IWebElement passwordInput = driver.FindElement(By.CssSelector("input[name='passwd']"));
                passwordInput.SendKeys(password);
                driver.FindElement(By.CssSelector("div.passport-Domik-Form-Field button[type='submit']")).Click();
            }
            catch (Exception e)
            {
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                driver.FindElement(By.CssSelector("input[type='password']")).SendKeys(password);
                driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            }
        }

        [Test]
        public void checkForNewMail()
        {
            driver.FindElement(By.CssSelector("div.mail-ComposeButton-Wrap span.mail-ComposeButton-Refresh")).Click();
        }

        [Test]
        public void NewMailToMyself()
        {
            //Find button New Mail
            driver.FindElement(By.CssSelector("div.mail-ComposeButton-Wrap a")).Click();

            //Click to Себе option
            driver.FindElement(By.CssSelector("span[data-name='Себе']")).Click();
            //e-mail text
            IWebElement text = driver.FindElement(By.CssSelector("div.cke_wysiwyg_div.cke_reset.cke_enable_context_menu.cke_editable.cke_editable_themed.cke_contents_ltr.cke_show_borders"));

            text.SendKeys(HelloMessage);
            //sending
            driver.FindElement(By.CssSelector("div.mail-Compose-Field-Actions_left button")).Click();
            Thread.Sleep(3000);

            //go to Sended tab
            driver.FindElement(By.CssSelector("a[href='#sent']")).Click();

            //clicking on last sended series of email
            var series = driver.FindElement(By.XPath("//div[@class='mail-Layout-Main js-mail-layout-content']/div[2]/div[5]/div/div/div/div[2]/div/div[1]"));
            series.Click();

            //click on last email in series
            series.FindElement(By.XPath("./div/div[2]/div/div[1]")).Click();

            var actualText = driver.FindElement(By.CssSelector("div.mail-Message-Body-Content div")).GetAttribute("innerText");

            Assert.AreEqual(HelloMessage, actualText);
        }

        [Test]
        public void TestRichTextbox()
        {
            //Find button New Mail
            driver.FindElement(By.CssSelector("div.mail-ComposeButton-Wrap a")).Click();

            //Click to Себе option
            driver.FindElement(By.CssSelector("span[data-name='Себе']")).Click();

            var ToolBarXPath = "//*[@class='mail-Compose-Field-Input mail-Editor-Container']/div/div/span[1]";
            IWebElement RichTextArea = driver.FindElement(By.CssSelector("[role='presentation'] div[role='textbox']"));
            IWebElement ToolBar = driver.FindElement(By.XPath(ToolBarXPath));

            try
            {
                //TestUndoRedoButtons(RichTextArea, ToolBar);

                // TestTextStyleButtons(RichTextArea, ToolBarXPath);

                //TestLinks(RichTextArea, ToolBar);

                //TestImageButton(RichTextArea, ToolBar);

                //TestBlockQuote(RichTextArea, ToolBar);

                //===================эта дичь не работает==============
                TestTextColors(RichTextArea, ToolBar);

                //TestFont(RichTextArea, ToolBar);
                //===================================================

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        private void TestFont(IWebElement RichTextArea, IWebElement ToolBar)
        {
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__mailfont")).Click();
            driver.FindElement(By.CssSelector("a[style='font-family:comic sans ms,sans-serif;']")).Click();
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__mailfontsize")).Click();
            driver.FindElement(By.CssSelector("a[style='font-size:16px;line-height:normal;']")).Click();

            RichTextArea.SendKeys(FontText);
            Assert.IsTrue(RichTextArea.FindElement(By.CssSelector("span[style='font-family:comic sans ms,sans-serif;'] span[style='font-size:16px;']")).GetAttribute("innerText").Contains(FontText));
            RichTextArea.SendKeys(Keys.Enter);
        }

        private void TestTextColors(IWebElement RichTextArea, IWebElement ToolBar)
        {
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__mailtextcolor")).Click();
            Thread.Sleep(1000);
            var a = driver.FindElement(By.CssSelector("[data-value='00FF00']"));

            Actions builder = new Actions(driver);
            builder.MoveToElement(a, 5, 5).Click().Build().Perform();

            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__mailbgcolor")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("[data-value='FFFF00']")).Click();
            Thread.Sleep(1000);

            RichTextArea.Click();
            RichTextArea.SendKeys(ColorfulText);
            Assert.IsTrue(RichTextArea.FindElement(By.CssSelector("span[style='color:#00ff00;'] span[style='background-color:#ffff00;']")).GetAttribute("innerText").Contains(ColorfulText));
            RichTextArea.SendKeys(Keys.Control + "z");
            RichTextArea.SendKeys(Keys.Control + "z");
            RichTextArea.SendKeys(Keys.Control + "z");
        }

        private void TestImageButton(IWebElement RichTextArea, IWebElement ToolBar)
        {
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__addimage")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(".mail-Compose-AddImage-Popup-Wrapper input")).SendKeys(LinkToImage);
            driver.FindElement(By.CssSelector(".mail-Compose-AddImage-Popup-Wrapper button")).Click();
            Thread.Sleep(2000);
            Assert.AreEqual(LinkToImage, RichTextArea.FindElement(By.CssSelector("img")).GetAttribute("src"));
            RichTextArea.SendKeys(Keys.Enter);
        }

        private void TestBlockQuote(IWebElement RichTextArea, IWebElement ToolBar)
        {
            //blockquote
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__blockquote")).Click();
            Thread.Sleep(1000);
            RichTextArea.SendKeys(Quote);
            Thread.Sleep(1000);
            Assert.AreEqual(Quote, RichTextArea.FindElement(By.CssSelector("blockquote div")).GetAttribute("innerText"));
            RichTextArea.SendKeys(Keys.Enter);
        }

        private void TestLinks(IWebElement RichTextArea, IWebElement ToolBar)
        {
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__link")).Click();
            Thread.Sleep(1500);
            var list = driver.FindElements(By.CssSelector(".mail-Compose-Maillink input"));
            list[0].SendKeys(Link);
            list[1].SendKeys(LinkTitle);

            driver.FindElement(By.CssSelector(".mail-Compose-Maillink button")).Click();
            Thread.Sleep(2000);

            Assert.IsTrue(RichTextArea.FindElement(By.CssSelector("a")).GetAttribute("innerText").Contains(LinkTitle));

            RichTextArea.Click();
            RichTextArea.SendKeys(Keys.Left);
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__unlink")).Click();
            RichTextArea.Click();
            RichTextArea.SendKeys(Keys.Enter);
        }

        private void TestUndoRedoButtons(IWebElement RichTextArea, IWebElement ToolBar)
        {
            // UNDO-REDO BUTTON
            RichTextArea.SendKeys(SimpleText);
            Assert.AreEqual(SimpleText, RichTextArea.FindElement(By.CssSelector("div")).GetAttribute("innerText"));

            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__undo")).Click();
            Assert.AreEqual("\r\n", RichTextArea.FindElement(By.CssSelector("div")).GetAttribute("innerText"));

            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__redo")).Click();
            Assert.AreEqual(SimpleText, RichTextArea.FindElement(By.CssSelector("div")).GetAttribute("innerText"));

            RichTextArea.SendKeys(Keys.Enter);
        }

        private void TestTextStyleButtons(IWebElement RichTextArea, string ToolBarXPath)
        {
            var isMenu = false;
            try
            {
                driver
                    .FindElement(By.XPath(ToolBarXPath))
                    .FindElement(By.CssSelector("a.cke_button.cke_button__bold"));
            }
            catch (Exception e)
            {
                isMenu = true;
            }

            if (isMenu)
                TestStyleButtonsMenu(RichTextArea, ToolBarXPath);
            else
                TestStyleButtonsPure(RichTextArea, ToolBarXPath);
        }

        private void TestStyleButtonsMenu(IWebElement RichTextArea, string ToolBarXPath)
        {
            var arrayOfInputs = new string[] { BoldText, ItalicText, UnderlinedText, LineThroughText };
            var arrayOfCssSelectors = new string[] { "strong", "em", "u", "span[style='text-decoration:line-through;']" };
            var arrayOfOptions = new string[] { "bold", "italic", "underline", "strike" };

            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(1000);

                //clicking main menu button
                driver.FindElement(By.XPath("//a[@title='Выделение']")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.CssSelector($"[data-command='{arrayOfOptions[i]}']")).Click();

                RichTextArea.SendKeys(arrayOfInputs[i]);

                Assert
                    .IsTrue(RichTextArea
                        .FindElement(By.CssSelector(arrayOfCssSelectors[i]))
                        .GetAttribute("innerText")
                        .Contains(arrayOfInputs[i])
                    );

                //turning off style option
                driver.FindElement(By.XPath("//a[@title='Выделение']")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.CssSelector($"[data-command='{arrayOfOptions[i]}']")).Click();

                RichTextArea.SendKeys(Keys.Enter);
            }
        }

        private void TestStyleButtonsPure(IWebElement RichTextArea, string ToolBarXPath)
        {
            var ToolBar = driver.FindElement(By.XPath(ToolBarXPath));
            // ================================STYLE BUTTONS=================================
            // BOLD TEXT
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__bold")).Click();
            RichTextArea.SendKeys(BoldText);

            Assert.IsTrue(RichTextArea.FindElement(By.CssSelector("strong")).GetAttribute("innerText").Contains(BoldText));
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__bold")).Click();
            RichTextArea.SendKeys(Keys.Enter);
            //ITALIC TEXT
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__italic")).Click();
            RichTextArea.SendKeys(ItalicText);

            Assert.IsTrue(RichTextArea.FindElement(By.CssSelector("em")).GetAttribute("innerText").Contains(ItalicText));
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__italic")).Click();
            RichTextArea.SendKeys(Keys.Enter);
            //UNDERLINED TEXT
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__underline")).Click();
            RichTextArea.SendKeys(UnderlinedText);

            Assert.IsTrue(RichTextArea.FindElement(By.CssSelector("u")).GetAttribute("innerText").Contains(UnderlinedText));
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__underline")).Click();
            RichTextArea.SendKeys(Keys.Enter);
            //LINE CROSSED TEXT
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__strike")).Click();
            RichTextArea.SendKeys(LineThroughText);

            Assert.IsTrue(RichTextArea.FindElement(By.CssSelector("span[style='text-decoration:line-through;']")).GetAttribute("innerText").Contains(LineThroughText));
            ToolBar.FindElement(By.CssSelector("a.cke_button.cke_button__strike")).Click();
            RichTextArea.SendKeys(Keys.Enter);
        }

        [Test]
        public void ToolbarButtons()
        {
            string[] toolbarButtons = { "div.ns-view-toolbar-button-delete",
                                        "div.ns-view-toolbar-button-forward",
                                        "div.ns-view-toolbar-button-spam" };

            for (int i = 0; i < toolbarButtons.Length; i++)
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
        public void MoveMailForward()
        {
            string forwardButton = "div.ns-view-toolbar-button-forward";
            string forwardedMsg = "Forwarded message";

            IWebElement letterCheckBox = driver.FindElement(By.CssSelector("div.ns-view-container-desc.mail-MessagesList.js-messages-list label"));

            letterCheckBox.Click();

            IWebElement button = driver.FindElement(By.CssSelector(forwardButton));

            Assert.IsFalse(button.GetAttribute("class").Contains("is-disabled"));
            button.Click();
            Thread.Sleep(500);

            driver.FindElement(By.CssSelector("span[data-name='Себе']")).Click();

            //e-mail text
            IWebElement text = driver.FindElement(By.CssSelector("div.cke_wysiwyg_div.cke_reset.cke_enable_context_menu.cke_editable.cke_editable_themed.cke_contents_ltr.cke_show_borders"));

            text.SendKeys(forwardedMsg);
            //sending
            driver.FindElement(By.CssSelector("div.mail-Compose-Field-Actions_left button")).Click();
            Thread.Sleep(3000);

            //go to Sended tab
            driver.FindElement(By.CssSelector("a[href='#sent']")).Click();

            var series = driver.FindElement(By.XPath("//div[@class='mail-Layout-Main js-mail-layout-content']/div[2]/div[5]/div/div/div/div[2]/div/div[1]"));
            series.Click();

            //click on last email in series
            series.FindElement(By.XPath("./div/div[2]/div/div[1]")).Click();

            var list = driver.FindElements(By.CssSelector("div.mail-Message-Body-Content div"));
            var isExpectedMsg = false;
            foreach(var div in list)
            {
                if (div.GetAttribute("innerText").Contains(forwardedMsg))
                {
                    isExpectedMsg = true;
                    break;
                }
            }

            Assert.IsTrue(isExpectedMsg);
        }

        [Test]
        public void DeleteEmail()
        {
            string inbox = "div[class~='ns-view-folders'] a[href='#inbox']";
            string trash = "div[class~='ns-view-folders'] a[href='#trash']";
            string deleteButton = "div.ns-view-toolbar-button-delete";
            string DeletedMsg = "Deleted message";

            //Find button New Mail
            driver.FindElement(By.CssSelector("div.mail-ComposeButton-Wrap a")).Click();

            //Click to Себе option
            driver.FindElement(By.CssSelector("span[data-name='Себе']")).Click();
            //e-mail text
            IWebElement text = driver.FindElement(By.CssSelector("div.cke_wysiwyg_div.cke_reset.cke_enable_context_menu.cke_editable.cke_editable_themed.cke_contents_ltr.cke_show_borders"));

            text.SendKeys(DeletedMsg);
            //sending
            driver.FindElement(By.CssSelector("div.mail-Compose-Field-Actions_left button")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.CssSelector(inbox)).Click();

            IWebElement letterCheckBox = driver.FindElement(By.CssSelector("div.ns-view-container-desc.mail-MessagesList.js-messages-list label"));

            letterCheckBox.Click();

            IWebElement button = driver.FindElement(By.CssSelector(deleteButton));

            Assert.IsFalse(button.GetAttribute("class").Contains("is-disabled"));
            button.Click();
            Thread.Sleep(500);

            driver.FindElement(By.CssSelector(trash)).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//div[@class='ns-view-container-desc mail-MessagesList js-messages-list']/div[1]")).Click();

            var actualText = driver.FindElement(By.CssSelector("div.mail-Message-Body-Content div")).GetAttribute("innerText");

            Assert.AreEqual(DeletedMsg, actualText);
        }

        [Test]
        public void MarkAsSpam()
        {
            string inbox = "div[class~='ns-view-folders'] a[href='#inbox']";
            string spam = "div[class~='ns-view-folders'] a[href='#spam']";
            string spamButton = "div.ns-view-toolbar-button-spam";
            string spamMsg = "spam message";

            //Find button New Mail
            driver.FindElement(By.CssSelector("div.mail-ComposeButton-Wrap a")).Click();

            //Click to Себе option
            driver.FindElement(By.CssSelector("span[data-name='Себе']")).Click();
            //e-mail text
            IWebElement text = driver.FindElement(By.CssSelector("div.cke_wysiwyg_div.cke_reset.cke_enable_context_menu.cke_editable.cke_editable_themed.cke_contents_ltr.cke_show_borders"));

            text.SendKeys(spamMsg);
            //sending
            driver.FindElement(By.CssSelector("div.mail-Compose-Field-Actions_left button")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.CssSelector(inbox)).Click();

            IWebElement letterCheckBox = driver.FindElement(By.CssSelector("div.ns-view-container-desc.mail-MessagesList.js-messages-list label"));

            letterCheckBox.Click();

            IWebElement button = driver.FindElement(By.CssSelector(spamButton));

            Assert.IsFalse(button.GetAttribute("class").Contains("is-disabled"));
            button.Click();
            Thread.Sleep(500);

            driver.FindElement(By.CssSelector(spam)).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//div[@class='ns-view-container-desc mail-MessagesList js-messages-list']/div[1]")).Click();

            var actualText = driver.FindElement(By.CssSelector("div.mail-Message-Body-Content div")).GetAttribute("innerText");

            Assert.AreEqual(spamMsg.ToLower(), actualText.ToLower());
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

        [Test]
        public void ClickOnTabs()
        {
            var list = driver.FindElements(By.CssSelector("[class~='js-invalidate-tab']"));

            for (int i=0; i< list.Count;i++)
            {
                list[i].Click();
                Thread.Sleep(500);

                list = driver.FindElements(By.CssSelector("[class~='js-invalidate-tab']"));
                Thread.Sleep(500);
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
