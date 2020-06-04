using OpenQA.Selenium.Chrome;
using System;

namespace JG.Facebook.HappyBirthday.Core
{
    internal class Robot: Behavior
    {
        internal static void Load()
        {
            //set chrome driver options
            var options = SetDriverOptions();

            using (var driver = new ChromeDriver(options))
            {
                try
                {
                    OpenFacebook(driver);
                    Login(driver);
                    GoToEventPage(driver);
                    SendMessageToFrient(driver);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (!DEBUG_MODE)
                        driver.Close();
                    else
                        Console.ReadKey();
                }
            }
        }

        
    }
}
