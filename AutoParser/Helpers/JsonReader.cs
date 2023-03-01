using AutoParser.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace AutoParser.Helpers
{
    public static class JsonReader
    {
        public static GoogleSheetSettingsModel? GetValues()
        {

            try
            {

                string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string jsonFilePath = Path.Combine(basePath, "JsonResours", "ParserSettings.json");
             
                if (!File.Exists(jsonFilePath))
                    return null;

                string json = File.ReadAllText(jsonFilePath);

                return JsonConvert.DeserializeObject<GoogleSheetSettingsModel>(json);
            }
            catch (Exception ex)
            {
                Logger.WrtieLog($"Error processing element at path {ex.ToString()}");
                return null;
            }
        }
    }
}
