using System;
using System.Collections.Generic;
using System.Linq;
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
            //TODO make correct относительный path
            string json = File.ReadAllText(@"C:\Users\Honor\source\repos\AutomationApiParser\AutoParser\GoogleSheetSettings.json");
            GoogleSheetSettingsModel settings = JsonConvert.DeserializeObject<GoogleSheetSettingsModel>(json);
            return settings;    
        }
    }
}
