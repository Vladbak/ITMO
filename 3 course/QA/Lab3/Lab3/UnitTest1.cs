using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace Lab3
{
    [TestClass]
    public class UnitTest1
    {
        public static readonly String HelloMessage = "Hello!";

        public static readonly String DeviceName = "vbox86p";
        public static readonly String PackageName = "com.perm.kate";
        public static readonly String PlatformName = "Android";
        public static readonly Int32 WaitTimeout = 15000;

        public static readonly String MainActivity = "com.perm.kate.MainActivity";
        public static readonly String LoginInput = "//*[@resource-id='com.perm.kate:id/username']";
        public static readonly String PasswordInput = "//*[@resource-id='com.perm.kate:id/password']";
        public static readonly String LoginButton = "//*[@resource-id='com.perm.kate:id/login_btn']";

        public static readonly String MessagesTab = "//*[@resource-id='com.perm.kate:id/action_messages']";
        public static readonly String TextArea = "//*[@resource-id='com.perm.kate:id/et_new_message']";
        public static readonly String SendMessageButton = "//*[@resource-id='com.perm.kate:id/btn_new_message']";
        public static readonly String MyDialog = "//android.view.View/android.widget.ListView/android.widget.LinearLayout/android.widget.ImageView[@content-desc='Profile Владимир Бакшенов']/../android.widget.LinearLayout";

        public static readonly String LikeButton= "//android.widget.ListView/android.widget.LinearLayout/android.widget.LinearLayout[2]/android.widget.LinearLayout/android.widget.LinearLayout[4]";

        public static readonly String Login= "vladbakshen@mail.ru";
        public static readonly String Password= "1259486370Vk";

        AndroidDriver<AndroidElement> driver;

        public AndroidDriver<AndroidElement> Init(string Activity)
        {
            var desiredCapabilities = new DesiredCapabilities();
            desiredCapabilities.SetCapability("deviceName", DeviceName);
            desiredCapabilities.SetCapability("appPackage", PackageName);
            desiredCapabilities.SetCapability("appActivity", Activity);
            desiredCapabilities.SetCapability("newCommandTimeout", 120);
            var driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), desiredCapabilities);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return driver;
        }

        [TestInitialize]
        public void LogIn()
        {
            driver = Init(MainActivity);
            driver.FindElementByXPath(LoginInput).SendKeys(Login);
            driver.FindElementByXPath(PasswordInput).SendKeys(Password);
            driver.FindElementByXPath(LoginButton).Click();
        }

        [TestMethod]
        public void WriteMessage()
        {
            Thread.Sleep(2000);
            driver.FindElementByXPath(MessagesTab).Click();
            Thread.Sleep(2000);
            driver.FindElementByXPath(MyDialog).Click();
            Thread.Sleep(2000);
            driver.FindElementByXPath(TextArea).SendKeys(HelloMessage);
            driver.FindElementByXPath(SendMessageButton).Click();
        }

        [TestMethod]
        public void LikePost()
        {
            Thread.Sleep(2000);
            driver.FindElementByXPath(LikeButton).Click();
        }
    }
}
