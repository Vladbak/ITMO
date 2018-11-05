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
        int sleepTime = 600;
        AndroidDriver<AndroidElement> driver;
        public static readonly String Login = "engine9779@gmail.com";
        public static readonly String Password = "testpassword";
        public static readonly String HelloMessage = "Hello!";

        public static readonly String DeviceName = "vbox86p";
        public static readonly String PackageName = "com.perm.kate";
        public static readonly String PlatformName = "Android";
        public static readonly Int32 WaitTimeout = 15000;

        public static readonly String MainActivity = "com.perm.kate.MainActivity";
        public static readonly String LoginInput = "//*[@resource-id='com.perm.kate:id/username']";
        public static readonly String PasswordInput = "//*[@resource-id='com.perm.kate:id/password']";
        public static readonly String LoginButton = "//*[@resource-id='com.perm.kate:id/login_btn']";

        public static readonly String MessagesTab = "//android.widget.TextView[@text='Messages']/..";
        public static readonly String MyDialog = "//android.view.View/android.widget.ListView/android.widget.LinearLayout/android.widget.ImageView[@content-desc='Profile Владимир Бакшенов']/../android.widget.LinearLayout";
        public static readonly String TextArea = "//*[@resource-id='com.perm.kate:id/smile_button']/../android.widget.EditText";
        public static readonly String SendMessageButton = "//*[@resource-id='com.perm.kate:id/btn_new_message']";

        public static readonly String LikeButton = "//*[@resource-id='com.perm.kate:id/likes_view']";

        public static readonly String RepostButton = "//*[@resource-id='com.perm.kate:id/reposts_view']";
        public static readonly String WallButton = "//android.widget.TextView[@text='Wall']/../..";
        public static readonly String GroupButton = "//android.widget.TextView[@text='9Cute']/../..";
        public static readonly String GroupsButton = "//*[@resource-id='com.perm.kate:id/ll_profile_groups_region']";
        public static readonly String ProfileTab = "//*[@resource-id='com.perm.kate:id/action_profile']";
        public static readonly String Prof1ileTab = "//*[@resource-id='com.perm.kate:id/action_profile']";
        public static readonly String HomeButton = "//*[@resource-id='com.perm.kate:id/ll_home_button']";
        public static readonly String MyProfileButton = "//*[@resource-id='com.perm.kate:id/ll_profile_name_region']";
        public static readonly String ProfileWallButton = "//*[@resource-id='com.perm.kate:id/ll_profile_wall_region']";
        public static readonly String RepostOnMyWallButton= "//*[@resource-id='com.perm.kate:id/select_dialog_listview']/android.widget.TextView[1]";
        public static readonly String ShareButton = "//*[@resource-id='android:id/button1']";

        public static readonly String SearchButton = "//*[@content-desc='Search']";
        public static readonly String SearchGroupsButton = "//*[@text='Groups']";
        public static readonly String SearchField = "//*[@resource-id='com.perm.kate:id/tb_search']";
        public static readonly String SendSearchRequestButton = "//*[@resource-id='com.perm.kate:id/btn_search']";
        public static readonly String FirstSearchResult = "//*[@resource-id='com.perm.kate:id/lv_search_list']/*[1]";
        public static readonly String JoinGroupButton = "//*[@resource-id='com.perm.kate:id/fl_button_bg2']";
        public static readonly String YesButton = "//*[@resource-id='android:id/button1']";
        public static readonly String GroupMenuButton = "//*[@resource-id='com.perm.kate:id/fl_button_menu']";
        public static readonly String LeaveGroupOption = "//android.widget.ListView/android.widget.LinearLayout[3]";

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
        public void JoinOrLeaveGroup()
        {
            Thread.Sleep(5000);
            driver.FindElementByXPath(SearchButton).Click();
            Thread.Sleep(4000);
            driver.FindElementByXPath(SearchGroupsButton).Click();
            Thread.Sleep(sleepTime);
            driver.FindElementByXPath(SearchField).SendKeys("google");
            Thread.Sleep(sleepTime);
            driver.FindElementByXPath(SendSearchRequestButton).Click();
            Thread.Sleep(sleepTime);
            driver.FindElementByXPath(FirstSearchResult).Click();
            Thread.Sleep(sleepTime);
            try
            {
                driver.FindElementByXPath(JoinGroupButton).Click();
                Thread.Sleep(sleepTime);
                driver.FindElementByXPath(YesButton).Click();
            }
            catch (Exception e)
            {// if we already joined this group
                driver.FindElementByXPath(GroupMenuButton).Click();
                Thread.Sleep(sleepTime);
                driver.FindElementByXPath(LeaveGroupOption).Click();
                Thread.Sleep(sleepTime);
                driver.FindElementByXPath(YesButton).Click();
            }
            Thread.Sleep(1000);
        }

        [TestMethod]
        public void Repost()
        {
            Thread.Sleep(sleepTime);
            SwipeUntilFound(RepostButton);
            Thread.Sleep(sleepTime);
            driver.FindElementByXPath(RepostOnMyWallButton).Click();
            Thread.Sleep(sleepTime);
            driver.FindElementByXPath(ShareButton).Click();
            Thread.Sleep(sleepTime);
            driver.FindElementByXPath(ProfileTab).Click();
            Thread.Sleep(sleepTime);
            driver.FindElementByXPath(MyProfileButton).Click();
            Thread.Sleep(sleepTime);
            driver.FindElementByXPath(ProfileWallButton).Click();
            Thread.Sleep(2000);
        }

        [TestMethod]
        public void InstallWidget()
        {
            var height = driver.Manage().Window.Size.Height;
            var width = driver.Manage().Window.Size.Width;
            driver.CloseApp();

            Thread.Sleep(1500);
            driver.Tap(1, width / 2, height / 2, 2000);
            Thread.Sleep(1500);
            driver.Tap(1, driver.FindElementByXPath("//*[@resource-id='com.android.launcher3:id/widget_button']"), 10);
            Thread.Sleep(1500);
            driver.Swipe(width * 3 / 4, height / 2, width / 10, height / 2, 1000);
            Thread.Sleep(1500);
            driver.Swipe(width * 3 / 4, height / 2, width / 10, height / 2, 1000);
            Thread.Sleep(1500);
            driver.Tap(1, driver.FindElementByXPath("//*[@resource-id='com.android.launcher3:id/widget_preview'][1]"), 3000);
            Thread.Sleep(1500);
            driver.Tap(1, driver.FindElementByXPath("//*[@resource-id='android:id/button1']"), 10);
        }

        [TestMethod]
        public void WriteMessage()
        {
            Thread.Sleep(2000);
            driver.FindElementByXPath(MessagesTab).Click();
            Thread.Sleep(2000);
            SwipeUntilFound(MyDialog);
            Thread.Sleep(2000);
            driver.FindElementByXPath(TextArea).SendKeys(HelloMessage);
            Thread.Sleep(2000);
            driver.FindElementByXPath(SendMessageButton).Click();
        }

        [TestMethod]
        public void LikePost()
        {
            Thread.Sleep(2000);
            SwipeUntilFound(LikeButton);
        }

        private void SwipeUntilFound(string xpath)
        {
            var height = driver.Manage().Window.Size.Height;
            var width = driver.Manage().Window.Size.Width;
            Thread.Sleep(2000);
            var elementFound = false;
            while (!elementFound)
            {
                try
                {
                    driver.FindElementByXPath(xpath).Click();
                    elementFound = true;
                }
                catch (Exception e)
                {
                    Thread.Sleep(1000);
                    driver.Swipe(500, 500, 500, 100, 500);
                }
            }
        }
    }
}
