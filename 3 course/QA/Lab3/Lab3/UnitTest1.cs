using System;
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

        public static readonly String OptionalButton1Id = "android:id/button1";
        public static readonly String MainActivity = "com.perm.kate.MainActivity";
        public static readonly String GotItButton = "//*[@resource-id='com.google.android.gm:id/welcome_tour_got_it]";
        public static readonly String AddNewEmail = "//*[@resource-id='com.google.android.gm:id/setup_addresses_add_another']";
        public static readonly String OtherEmailsButton = "//*[@resource-id='com.google.android.gm:id/providers_list']/android.widget.LinearLayout[5]/android.widget.LinearLayout";
        public static readonly String NewEmailTextArea = "//android.widget.EditText";
        public static readonly String LoginInput = "//*[@resource-id='com.facebook.katana:id/login_username']";
        public static readonly String PasswordInput = "//*[@resource-id='com.facebook.katana:id/login_password']";
        public static readonly String GotItButton = "//*[@resource-id='com.google.android.gm:id/welcome_tour_got_it']";
        public static readonly String GotItButton = "//*[@resource-id='com.google.android.gm:id/welcome_tour_got_it']";
        public static readonly String GotItButton = "//*[@resource-id='com.google.android.gm:id/welcome_tour_got_it']";
        public static readonly String NoGooglePlayButton = "//android.widget.Button[@resource-id='android:id/button1']";

        
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
            driver.FindElementByXPath(LoginName).SendKeys(Login);
            driver.FindElementByXPath(PasswordName).SendKeys(Password);
            driver.FindElementByXPath(SignInName).Click();
        }

        [TestMethod]
        public void WriteMessage()
        {
            driver.FindElementByXPath(Messages).Click();
            driver.FindElementByXPath(MyDialog).Click();
            driver.FindElementByXPath(TextArea).SendKeys(HelloMessage);
            driver.FindElementByXPath(SendButton).Click();
        }

    }
}
