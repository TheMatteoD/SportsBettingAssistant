using System;
using System.Net.Http;
using static System.Net.WebRequestMethods;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using File = System.IO.File;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SportsBettingAssistantProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userInput;
            Console.WriteLine("Currently you can only receive betting tips for NFL Spreads. Check back for more in the future!");
            Console.WriteLine("Type 'nfl' for this weeks picks");
            userInput = Console.ReadLine().ToLower();


           if (userInput == "nfl")
            {
                NFLBettingAssistant.GetNewData();
                NFLBettingAssistant.FindBestBets();
            }else if(userInput == "dev test")// Doesn't call api
            {
                NFLBettingAssistant.FindBestBets();
            }
        }
    }
}