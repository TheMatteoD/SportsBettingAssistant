using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace SportsBettingAssistantProject
{
    internal class NFLBettingAssistant
    {
        public static void GetNewData()
        {
            /// Generates a new json file at \NFLdata.json
            //Variables 
            var API_KEY = ;
            var SPORT_KEY = "americanfootball_nfl";
            var REGION = "us";
            var MARKETS = "spreads";
            var URL = $"https://api.the-odds-api.com/v4/sports/{SPORT_KEY}/odds/?apiKey={API_KEY}&regions={REGION}&markets={MARKETS}";


            // Call API store response
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(URL);
                var result = client.GetAsync(endpoint).Result.Content.ReadAsStringAsync().Result;
                File.WriteAllText(@"D:\Repos\SportsBettingAssistant\SportsBettingAssistantProject\bin\Debug\netcoreapp3.1\NFLdata.json", result);
            }

        }
        public static void FindBestBets()
        {
            //Store JSON to variable
            var reader = new StreamReader("NFLdata.json");
            var dataString = reader.ReadToEnd().ToString();
            var dataJson = JArray.Parse(dataString);


            //Loop for each game
            foreach (var game in dataJson)
            {
                //store all bookmakers odds
                Console.WriteLine($"{game["home_team"]}");
                //find outliers

                //return selected picks
            }

        }
    }
}
