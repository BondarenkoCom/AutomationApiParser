using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParser.Models
{
    public class GoogleSheetSettingsModel
    {
        public string? PathToKey { get; set; }
        public string? ApplicationName { get; set; }
        public string? SpreadsheetId { get; set; }
        public string? SheetRange { get; set; }
    }
}
