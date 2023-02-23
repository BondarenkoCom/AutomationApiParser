using AutoParser.Models;
using Newtonsoft.Json;

namespace AutoParser.Helpers
{
    public static class JsonReader
    {
        public static GoogleSheetSettingsModel? GetValues()
        {

            string jsonFilePath = @"C:\Users\Honor\source\repos\AutomationApiParser\AutoParser\JsonResours\ParserSettings.json";

            if (!File.Exists(jsonFilePath))
                return null;

            string json = File.ReadAllText(jsonFilePath);
            
            return JsonConvert.DeserializeObject<GoogleSheetSettingsModel>(json);
        }
    }
}
