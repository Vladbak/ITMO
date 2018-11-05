using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace Lab3Facebook
{
    [TestClass]
    public class UnitTest1
    {
        public static readonly String Test = "test";

        public static readonly String DeviceName = "vbox86p";
        public static readonly String PackageName = "com.facebook.katana";
        public static readonly String PlatformName = "Android";
        public static readonly Int32 WaitTimeout = 15000;
        public static readonly String MainActivity = "com.facebook.composer.activity.ComposerActivity";

        public static readonly String TapOnProfile = "//*[@resource-id='com.facebook.katana:id/accounts_on_device_container']/android.widget.LinearLayout";
        public static readonly String LoginInput= "//*[@resource-id='com.facebook.katana:id/login_username']";
        public static readonly String PasswordInput= "//*[@resource-id='com.facebook.katana:id/login_password']";
        public static readonly String LoginButton= "//*[@resource-id='com.facebook.katana:id/login_login']";

        public static readonly String RememberPswdButton = "//*[@resource-id='com.facebook.katana:id/dbl_on']";
        public static readonly String FirstCachedAccount= "//*[@resource-id='com.facebook.katana:id/accounts_on_device_container']/*[1]";

        public static readonly String NewPostLink = "//*[@resource-id='android:id/list']/android.view.View[3]";
        public static readonly String NewPostTextArea = "//*[@resource-id='com.facebook.katana:id/composer_status_text']";
        public static readonly String TextStyle = "//*[@resource-id='com.facebook.katana:id/text_style_picker']/*[2]";
        public static readonly String CreateNewPost = "//*[@resource-id='com.facebook.katana:id/primary_named_button']";
        public static readonly String PrevWidget= "//*[@resource-id='com.dch_android.widget_facebook:id/widgetBackground']";
        //public static readonly String SendPostButton= "//*[@resource-id='com.facebook.katana:id/action_buttons_wrapper']";
        //public static readonly String SendPostButton= "//*[@resource-id='com.facebook.katana:id/action_buttons_wrapper']";
        //public static readonly String SendPostButton= "//*[@resource-id='com.facebook.katana:id/action_buttons_wrapper']";
        //public static readonly String SendPostButton= "//*[@resource-id='com.facebook.katana:id/action_buttons_wrapper']";
        //public static readonly String SendPostButton= "//*[@resource-id='com.facebook.katana:id/action_buttons_wrapper']";
        //android:id/list com.facebook.katana:id/composer_status_text com.facebook.katana:id/action_buttons_wrapper
        //        mCurrentFocus=Window{230d61ad u0 com.facebook.katana/com.facebook.composer.act
        //ivity.ComposerActivity}

        public static readonly String Login = "etoktotakoy@mail.ru";
        public static readonly String Password = "rapter00";

        AndroidDriver<AndroidElement> driver;

        public AndroidDriver<AndroidElement> Init(string Activity)
        {
            var desiredCapabilities = new DesiredCapabilities();
            desiredCapabilities.SetCapability("deviceName", DeviceName);
            desiredCapabilities.SetCapability("appPackage", PackageName);
            desiredCapabilities.SetCapability("appActivity", Activity);
            desiredCapabilities.SetCapability("newCommandTimeout", 120);
            var driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), desiredCapabilities);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }

        [TestInitialize]
        public void LogIn()
        {
            driver = Init(MainActivity);
            Thread.Sleep(2000);
            try
            {
                driver.FindElementByXPath(TapOnProfile).Click();
            }
            catch (Exception e)
            {
                driver.FindElementByXPath(LoginInput).SendKeys(Login);
                Thread.Sleep(500);
                driver.FindElementByXPath(LoginInput).Tap(1, 1);
                driver.FindElementByXPath(PasswordInput).SendKeys(Password);
                Thread.Sleep(500);
                driver.FindElementByXPath(LoginButton).Click();
                Thread.Sleep(3000);
                driver.FindElementByXPath(RememberPswdButton).Click();
            }
        }

        [TestMethod]
        public void Idle()
        {

        }

        [TestMethod]
        public void I3dle()
        {

        }

        [TestMethod]
        public void Id1le()
        {

        }

        [TestMethod]
        public void NewPost()
        {
            Thread.Sleep(2000);
            driver.FindElementByXPath(NewPostLink).Click();
            Thread.Sleep(1000);
            driver.FindElementByXPath(NewPostTextArea).SendKeys(Test);
            Thread.Sleep(1000);
            driver.FindElementByXPath(TextStyle).Click();
            Thread.Sleep(1000);
            driver.FindElementByXPath(CreateNewPost).Click();
            Thread.Sleep(1000);
        }

        [TestMethod]
        public void InstallWidget()
        {
            var height = driver.Manage().Window.Size.Height;
            var width = driver.Manage().Window.Size.Width;
            driver.CloseApp();

            try
            {
                driver.FindElementByXPath("//android.appwidget.AppWidgetHostView/android.widget.RelativeLayout");
                driver.Swipe(600, 400, 500, 200, 5000);
                Thread.Sleep(1500);
            }
            catch (Exception e) { }

            driver.Tap(1, width / 2, height / 2, 2000);
            Thread.Sleep(1500);
            driver.Tap(1, driver.FindElementByXPath("//*[@resource-id='com.android.launcher3:id/widget_button']"), 10);
            Thread.Sleep(1500);
            driver.Swipe(width * 3 / 4, height / 2, width / 10, height / 2, 700);
            Thread.Sleep(1500);
            driver.Swipe(width * 3 / 4, height / 2, width / 10, height / 2, 700);
            Thread.Sleep(1500);
            driver.Swipe(width * 3 / 4, height / 2, width / 10, height / 2, 700);
            Thread.Sleep(1500);
            driver.Tap(1, driver.FindElementByXPath("//android.widget.GridLayout/android.widget.LinearLayout[5]"), 3000);
            Thread.Sleep(1500);
            driver.Tap(1, driver.FindElementByXPath("//*[@resource-id='android:id/button1']"), 10);
            Thread.Sleep(600);
            driver.FindElementById("com.dch_android.widget_facebook:id/refresh_button").Click();
            Thread.Sleep(4000);
        }
    }
}
