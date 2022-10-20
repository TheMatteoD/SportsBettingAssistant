using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Text;

namespace SportsBettingAssistantProject
{
    internal class NFLBettingAssistant
    {
        public static void GetNewData()
        {
            /// Generates a new json file at \NFLdata.json
            /// 
            //Gets API KEY from gitignore
            var reader = new StreamReader("apikeys.json");
            var keyString = reader.ReadToEnd().ToString();
            var keyJson = JObject.Parse(keyString);
            var TheOddsAPIKey = keyJson["the_odds_api"];
            reader.Close();


            //Variables 
            var API_KEY = TheOddsAPIKey;
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
                // get game details
                var homeTeam = game["home_team"];
                var awayTeam = game["away_team"];

                var bookmakers = new List<string>();
                string spreadTeam = "";
                var spreads = new List<float>();


                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine($"{homeTeam} vs. {awayTeam}");


                //store all bookmakers names and spreads
                foreach (var bookmaker in game["bookmakers"])
                {
                    bookmakers.Add((string)bookmaker["key"]);
                    spreadTeam = ((string)bookmaker["markets"][0]["outcomes"][0]["name"]);
                    spreads.Add((float)bookmaker["markets"][0]["outcomes"][0]["point"]);
                }
                
                
                //find most common spread from all books
                float? targetSpread =
                spreads
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
                .Select(x => (float?)x.Key)
                .FirstOrDefault();

                if (targetSpread >= 0)
                {
                    Console.WriteLine($"The anticipated spread is: {spreadTeam} at +{targetSpread}");
                }
                else
                {
                    Console.WriteLine($"The anticipated spread is: {spreadTeam} at {targetSpread}");
                }

                //return hedged betting options

                //foreach ()
                
                //what output will look like.
                // vvvvvvvvvvvvvvvvvv
                //Console.WriteLine($"At *insert bookmakers website* you can bet the *team name* at *spread* hedging your bet by *difference of anticipated spread and this bookmakers spread* points");

            }

        }
    }
}
