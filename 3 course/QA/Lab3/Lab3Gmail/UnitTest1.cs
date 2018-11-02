using System;
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
        public static readonly String PackageName = "com.perm.kate";
        public static readonly String PlatformName = "Android";
        public static readonly Int32 WaitTimeout = 15000;
        public static readonly String OptionalButton1Id = "android:id/button1";
        public static readonly String MainActivity = "com.perm.kate.MainActivity";
        public static readonly String LoginName = "//android.widget.LinearLayout/android.widget.EditText[1]";
        public static readonly String PasswordName = "//android.widget.LinearLayout/android.widget.EditText[2]";
        public static readonly String SignInName = "//android.widget.LinearLayout/android.widget.FrameLayout[1]/android.widget.Button";
        public static readonly String Messages = "//android.view.View/android.widget.LinearLayout/android.widget.FrameLayout[2]/android.widget.LinearLayout/android.widget.FrameLayout";
        public static readonly String MyDialog = "//android.widget.ListView/android.widget.LinearLayout/android.widget.ImageView[@content-desc='Profile Владимир Бакшенов']/../android.widget.LinearLayout";
        public static readonly String TextArea = "//android.widget.EditText";
        public static readonly String SendButton = "//android.widget.ImageButton[@content-desc='Send']";
        public static readonly String Passw1ordName = "//EditText[@text='E-mail or phone']";
        public static readonly String Passwo1rdName = "//EditText[@text='E-mail or phone']";
        public static readonly String Pa1sswordName = "//EditText[@text='E-mail or phone']";
        public static readonly String Pass1wo1rdName = "//EditText[@text='E-mail or phone']";
        public static readonly String Pa1ss11wordName = "//EditText[@text='E-mail or phone']";
        public static readonly String Pass111wor1dName = "//EditText[@text='E-mail or phone']";
        public static readonly String Pass11wor1dName = "//EditText[@text='E-mail or phone']";
        public static readonly String Pa1s11swordName = "//EditText[@text='E-mail or phone']";


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
