using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;


namespace Lab3Gmail
{
    [TestClass]
    public class UnitTest1
    {
        public static readonly String HelloMessage = "Hello!";

        public static readonly String DeviceName = "vbox86p";
        public static readonly String PackageName = "com.facebook.katana";
        public static readonly String PlatformName = "Android";
        public static readonly Int32 WaitTimeout = 15000;
        public static readonly String MainActivity = "com.facebook.composer.activity.ComposerActivity";

        public static readonly String TapOnProfile = "//*[@resource-id='com.facebook.katana:id/accounts_on_device_container']/android.widget.LinearLayout";
        public static readonly String LoginInput= "//*[@resource-id='com.facebook.katana:id/login_username']";
        public static readonly String PasswordInput= "//*[@resource-id='com.facebook.katana:id/login_password']";
        public static readonly String LoginButton= "//*[@resource-id='com.facebook.katana:id/login_login']";

        //com.facebook.katana:id/login_username com.facebook.katana:id/login_login
        public static readonly String NewFBPost= "//*[@resource-id='android:id/list']/android.view.View[2]";
        public static readonly String NewPostTextArea= "//*[@resource-id='com.facebook.katana:id/composer_status_text']";
        public static readonly String SendPostButton= "//*[@resource-id='com.facebook.katana:id/action_buttons_wrapper']";
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
            Thread.Sleep(10000);
            try
            {
                driver.FindElementByXPath(TapOnProfile).Click();
            }
            catch (Exception e)
            {
                driver.FindElementByXPath(LoginInput).SendKeys(Login);
                driver.FindElementByXPath(LoginInput).Tap(1, 1);
                driver.FindElementByXPath(PasswordInput).SendKeys(Password);
                driver.FindElementByXPath(LoginButton).Click();
                driver.Tap(1, 1050, 1900, 1);
            }
        }

        [TestMethod]
        public void NewPost()
        {
            Thread.Sleep(2000);
            driver.FindElementByXPath(NewFBPost).Click();
            Thread.Sleep(2000);
            driver.FindElementByXPath(NewPostTextArea).SendKeys(HelloMessage);
            driver.FindElementByXPath(SendPostButton).Click();
        }
    }
}
