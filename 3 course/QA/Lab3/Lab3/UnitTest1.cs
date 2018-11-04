using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Interfaces;


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
        public static readonly String LoginInput = "//android.widget.EditText[1]";
        public static readonly String PasswordInput = "//android.widget.EditText[2]";
        public static readonly String LoginButton = "//android.widget.Button[1]";

        public static readonly String MessagesTab = "//android.widget.TextView[@text='Messages']/..";
        public static readonly String MyDialog = "//android.view.View/android.widget.ListView/android.widget.LinearLayout/android.widget.ImageView[@content-desc='Profile Даниэль Гхази']/../android.widget.LinearLayout";
        public static readonly String TextArea = "//android.widget.ListView/../android.widget.LinearLayout/android.widget.EditText";
        public static readonly String SendMessageButton = "//android.widget.ListView/../android.widget.LinearLayout/android.widget.ImageButton[@content-desc='Send']";

        public static readonly String LikeButton = "//android.widget.ListView/android.widget.LinearLayout/android.widget.LinearLayout[2]/android.widget.LinearLayout/android.widget.LinearLayout[4]";

        public static readonly String Login = "vladbakshen@mail.ru";
        public static readonly String Password = "1259486370Vk";

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
            Thread.Sleep(5000);
            driver.FindElementByXPath(LoginInput).SendKeys(Login);
            driver.FindElementByXPath(PasswordInput).SendKeys(Password);
            driver.FindElementByXPath(LoginButton).Click();
        }

        [TestMethod]
        public void InstallWidget()
        {
            //driver = Init(MainActivity);
            var height = driver.Manage().Window.Size.Height;
            var width = driver.Manage().Window.Size.Width;
            driver.CloseApp();
            //menu
            driver.Tap(1, width / 2, (int)(height * 0.9), 1);
            Thread.Sleep(500);
            //apps
            driver.Tap(1, width / 8, (int)(height * 0.05), 1);
            Thread.Sleep(500);
            //widget
            driver.Tap(1, width / 3, (int)(height * 0.05), 1);
            Thread.Sleep(500);

            driver.Swipe(width * 3 / 4, height / 2, width / 10, height / 2, 1000);
            Thread.Sleep(500);
            driver.Swipe(width * 3 / 4, height / 2, width / 10, height / 2, 1000);
            Thread.Sleep(1000);

            driver.Tap(1, width * 3 / 4, height / 4, 3000);
        }

        [TestMethod]
        public void WriteMessage()
        {
            //driver = Init(MainActivity);
            Thread.Sleep(2000);
            driver.FindElementByXPath(MessagesTab).Click();
            Thread.Sleep(2000);
            driver.FindElementByXPath(MyDialog).Click();
            Thread.Sleep(2000);
            driver.FindElementByXPath(TextArea).SendKeys(HelloMessage);
            Thread.Sleep(2000);
            driver.FindElementByXPath(SendMessageButton).Click();
        }

        [TestMethod]
        public void LikePost()
        {
            //driver = Init(MainActivity);
            Thread.Sleep(2000);
            driver.FindElementByXPath(LikeButton).Click();
        }
    }
}
