using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using System.Threading;

namespace JG.Facebook.HappyBirthday.Core
{
    internal abstract class Utils
    {
        internal static readonly string[] MESSAGES_LIST = ConfigurationManager.AppSettings["MESSAGES_LIST"].Split('/');
        internal static readonly string FACEBOOK_USER = ConfigurationManager.AppSettings["FACEBOOK_USER"];
        internal static readonly string FACEBOOK_PASSWORD = ConfigurationManager.AppSettings["FACEBOOK_PASSWORD"];
        internal static readonly bool DEBUG_MODE = ConfigurationManager.AppSettings["DEBUG_MODE"].ToUpper() == "TRUE";

        internal static void ActionButton(ChromeDriver driver, string buttonName, int waitTime)
        {
            try
            {
                Thread.Sleep(waitTime);
                var playButton = driver.FindElementByCssSelector(buttonName);
                if (playButton != null)
                    playButton.Click();
            }
            catch (System.Exception)
            {
                Console.WriteLine($"Small error! The button {buttonName} is not there!");
            }
        }

        internal static ChromeOptions SetDriverOptions()
        {
            var options = new ChromeOptions();
            if (!DEBUG_MODE)
                options.AddArguments("headless");
            options.AddUserProfilePreference("intl.accept_languages", "en");
            options.AddArgument("no-sandbox");
            options.AddArguments("--incognito");
            options.AddArgument("--start-maximized");
            options.AddArguments("--browser.download.folderList=2");
            options.AddArgument("--no-proxy-server");
            options.AddArgument("--proxy-server='direct://'");
            options.AddArgument("--proxy-bypass-list=*");
            options.AddArgument("--start-maximized");
            return options;
        }
    }
}
