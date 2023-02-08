using AutoParser.Models;
using Newtonsoft.Json;

namespace AutoParser.Helpers
{
    public static class JsonReader
    {
        public static GoogleSheetSettingsModel? GetValues()
        {
            
            List<string> getJson = new List<string>();

            //TODO Make read non absolute path
            getJson.Add(@"C:\Users\Honor\source\repos\AutomationApiParser\AutoParser\JsonResours\UtekaCase.json");
            
            foreach (var item in getJson)
            {
                string json = File.ReadAllText(item);

                GoogleSheetSettingsModel settings = JsonConvert.DeserializeObject<GoogleSheetSettingsModel>(json);
                return settings;
            }
            return null;
        }
    }
}
