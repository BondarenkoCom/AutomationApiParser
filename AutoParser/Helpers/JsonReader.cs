using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoParser.Models;
using Newtonsoft.Json;

namespace AutoParser.Helpers
{
    public static class JsonReader
    {
        public static GoogleSheetSettingsModel? GetValues()
        {
            var getPath = Path.Combine(Environment.CurrentDirectory, "GoogleSheetSettings.json");
            Console.WriteLine(getPath);

            string json = File.ReadAllText(getPath);
            GoogleSheetSettingsModel settings = JsonConvert.DeserializeObject<GoogleSheetSettingsModel>(json);
            return settings;    
        }
    }
}
