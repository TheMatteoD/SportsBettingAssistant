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
            //NFLBettingAssistant.GetNewData();
            NFLBettingAssistant.FindBestBets();
        }
    }
}