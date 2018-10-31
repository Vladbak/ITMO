using System;
using OpenQA.Selenium.Appium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System.Threading;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Remote;

namespace Lab3
{
    [TestClass]
    public class UnitTest1
    {
        public static readonly String DeviceName = "vbox86p";
        public static readonly String PackageName = "com.twitter.android";
        public static readonly String PlatformName = "Android";
        public static readonly Int32 WaitTimeout = 15000;
        public static readonly String OptionalButton1Id = "android:id/button1";
        public static readonly String MainActivity = "com.twitter.app.main.MainActivity";

        public AndroidDriver<AndroidElement> Init(string Activity)
        {
            var desiredCapabilities = new DesiredCapabilities();
            desiredCapabilities.SetCapability("deviceName", DeviceName);
            desiredCapabilities.SetCapability("appPackage", PackageName);
            desiredCapabilities.SetCapability("appActivity", Activity);
            desiredCapabilities.SetCapability("newCommandTimeout", 120);
            var driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), desiredCapabilities);
            Assert.IsNotNull(driver.Context);
            try
            {
                Thread.Sleep(WaitTimeout);
                driver.FindElementById(OptionalButton1Id).Tap(1, 1);
                Thread.Sleep(WaitTimeout);
            }
            catch (NoSuchElementException) { }
            return driver;
        }

        [TestMethod]
        public void TestMethod1()
        {
            var driver = Init(MainActivity);
        }
    }
}
