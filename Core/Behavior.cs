using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace JG.Facebook.HappyBirthday.Core
{
    internal abstract class Behavior: Utils
    {
        internal static void OpenFacebook(ChromeDriver driver)
        {
            Console.WriteLine("Open Facebook");
            driver.Navigate().GoToUrl("https://www.facebook.com/");
        }

        internal static void Login(ChromeDriver driver)
        {
            Console.WriteLine("Enter with credentials");
            var element = By.XPath("//*[@id =\"email\"]");
            Console.WriteLine("Input username");
            driver.FindElement(element).SendKeys(FACEBOOK_USER);

            element = By.XPath("//*[@id =\"pass\"]");
            Console.WriteLine("Input password");
            driver.FindElement(element).SendKeys(FACEBOOK_PASSWORD);

            Console.WriteLine("Click on Login button");
            driver.FindElementByCssSelector("button[type=submit]").Click();
            Console.WriteLine("Login Successfull");
            Thread.Sleep(3000);
        }

        internal static void GoToEventPage(ChromeDriver driver)
        {
            Console.WriteLine("Entering in the Facebook birthday page");
            driver.Navigate().GoToUrl("https://www.facebook.com/events/birthdays");
            Thread.Sleep(3000);
        }
        internal static void SendMessageToFrient(ChromeDriver driver)
        {
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

        internal static void LogOut(ChromeDriver driver)
        {
            Thread.Sleep(2500);
            Console.WriteLine("Click on Account Settings");
            var accountButton = driver.FindElement(By.CssSelector("div[aria-label=\"Account\"]"));
            accountButton.Click();
            Thread.Sleep(2500);
            //Click on Log out button
            var logout = driver.FindElement(By.XPath("//span[contains(.,'Log Out')]"));
            logout.Click();
            Console.WriteLine("Log out Successfull");
            Thread.Sleep(3000);
        }
    }
}
