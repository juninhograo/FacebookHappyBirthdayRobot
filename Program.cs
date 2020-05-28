using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;

namespace JG.Robot.Task
{
    class Program
    {
        private static string[] MESSAGES_LIST = ConfigurationManager.AppSettings["MESSAGES_LIST"].Split('/');
        private static string FACEBOOK_USER = ConfigurationManager.AppSettings["FACEBOOK_USER"];
        private static string FACEBOOK_PASSWORD = ConfigurationManager.AppSettings["FACEBOOK_PASSWORD"];
        private static bool DEBUG_MODE = ConfigurationManager.AppSettings["DEBUG_MODE"].ToUpper() == "TRUE";

        private static ChromeOptions chromeOptions = new ChromeOptions();

        static void Main(string[] args)
        {
            RunMainTask();
        }

        private static void RunMainTask()
        {
            //set chrome driver options
            SetDriverOptions();

            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions))
            {
                try
                {
                    Console.WriteLine("Open Facebook");
                    driver.Navigate().GoToUrl("https://www.facebook.com/");
                    Console.WriteLine("Enter with credentials");
                    var element = By.XPath("//*[@id =\"email\"]");
                    Console.WriteLine("Input username");
                    driver.FindElement(element).SendKeys(FACEBOOK_USER);

                    element = By.XPath("//*[@id =\"pass\"]");
                    Console.WriteLine("Input password");
                    driver.FindElement(element).SendKeys(FACEBOOK_PASSWORD);

                    Console.WriteLine("Click on Login button");
                    driver.FindElementById("u_0_b").Click();
                    Console.WriteLine("Login Successfull");
                    Thread.Sleep(3000);

                    Console.WriteLine("Entering in the Facebook birthday page");
                    driver.Navigate().GoToUrl("https://www.facebook.com/events/birthdays");
                    Thread.Sleep(3000);

                    Console.WriteLine("Find friends without a birthday message");
                    var people = driver.FindElementsByXPath("//*[@class=\"sjgh65i0\"]//*[@class='_1mf _1mj']");

                    if (people.Count == 0)
                        Console.WriteLine("Ops! Any friend was found, let's close the app!");
                    else
                    {
                        Console.WriteLine("Iterate at friend's list");
                        foreach (var person in people)
                        {
                            Console.WriteLine("Get a random message");
                            int index = new Random().Next(MESSAGES_LIST.Length);
                            Console.WriteLine("Send the message");
                            person.SendKeys(MESSAGES_LIST[index]);
                            Thread.Sleep(2500);
                            person.SendKeys(Keys.Return);
                            Thread.Sleep(1000);
                        }
                        Console.WriteLine("Sent messages for all friends, let's close the app!");
                    }
                    Thread.Sleep(10000);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally {
                    if (!DEBUG_MODE)
                        driver.Close();
                    else
                        Console.ReadKey();
                }
            }
        }

        private static void SetDriverOptions()
        {
            if (!DEBUG_MODE)
                chromeOptions.AddArguments("headless");
            chromeOptions.AddUserProfilePreference("intl.accept_languages", "en");
            chromeOptions.AddArgument("no-sandbox");
            chromeOptions.AddArguments("--incognito");
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArguments("--browser.download.folderList=2");
            chromeOptions.AddArgument("--no-proxy-server");
            chromeOptions.AddArgument("--proxy-server='direct://'");
            chromeOptions.AddArgument("--proxy-bypass-list=*");
        }
    }
}
