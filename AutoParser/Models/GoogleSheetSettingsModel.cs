namespace AutoParser.Models
{
    public class GoogleSheetSettingsModel
    {
        public string? PathToKey { get; set; }
        public string? ApplicationName { get; set; }
        public string? SpreadsheetId { get; set; }
        public string? SheetRange { get; set; }
        public string? ReviewBodyClassname { get; set; }
        public string? DataTimeClassname { get; set; }
        public string? AuthorsClassname { get; set; }
        public string? RankingStarsItemPropName { get; set; }
        public string? Selector { get; set; }
        public string? SelectorEnd { get; set; }
        public WorkUrl? WorkUrl { get; set; }
    }

    public class WorkUrl
    {
        public string? Url0 { get; set; }
        public string? Url1 { get; set; }
        public string? Url2 { get; set; }
        public string? Url3 { get; set; }
        public string? Url4 { get; set; }
        public string? Url5 { get; set; }
        public string? Url6 { get; set; }
        public string? Url7 { get; set; }
        public string? Url8 { get; set; }
        public string? Url9 { get; set; }
        public string? Url10 { get; set; }
    }
}
