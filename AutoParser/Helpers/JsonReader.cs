using AutoParser.Models;
using AutoParser.WebDriver;
using Newtonsoft.Json;

namespace AutoParser.Helpers
{
    public static class JsonReader
    {
        public static GoogleSheetSettingsModel? GetValues()
        {
            //TODO Make read multiply settings both Json
            //TODO problem, dont work  both Jsons
            //TODO maybe use Lists?

            List<string> getJson = new List<string>();

            //TODO Make read non absolute path
            getJson.Add(@"C:\Users\Honor\source\repos\AutomationApiParser\AutoParser\JsonResours\UtekaCase.json");
            
            foreach (var item in getJson)
            {
                string json = File.ReadAllText(item);

                //Console.WriteLine(json);

                GoogleSheetSettingsModel settings = JsonConvert.DeserializeObject<GoogleSheetSettingsModel>(json);
                return settings;
            }
            return null;
        }
    }
}
