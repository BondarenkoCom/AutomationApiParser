using AutoParser.Models;
using AutoParser.WebDriver;
using Newtonsoft.Json;

namespace AutoParser.Helpers
{
    public static class JsonReader
    {
        
        public static GoogleSheetSettingsModel? GetValues()
        {            
            string[] getJson = new string[] {
            @"C:\Users\Honor\source\repos\AutomationApiParser\AutoParser\JsonResours\UtekaCase.json",
            @"C:\Users\Honor\source\repos\AutomationApiParser\AutoParser\JsonResours\DoctuCase.json",
            };

            foreach (var item in getJson)
            {
                string json = File.ReadAllText(item);

                Console.WriteLine(json);

                GoogleSheetSettingsModel settings = JsonConvert.DeserializeObject<GoogleSheetSettingsModel>(json);

                return settings;
                //TODO Make read multiply settings both Json
            }
            return null;
        }
    }
}
