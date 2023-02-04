using AutoParser.Models;
using Newtonsoft.Json;

namespace AutoParser.Helpers
{
    public static class JsonReader
    {
        public static GoogleSheetSettingsModel? GetValues()
        {
            //var getPath = Path.Combine(Environment.CurrentDirectory, "GoogleSheetSettings.json");
            var getPath = @"C:\Users\Honor\source\repos\AutomationApiParser\AutoParser\GoogleSheetSettings.json";
            
            string json = File.ReadAllText(getPath);
            GoogleSheetSettingsModel settings = JsonConvert.DeserializeObject<GoogleSheetSettingsModel>(json);
            return settings;    
        }
    }
}
