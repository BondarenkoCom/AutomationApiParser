namespace AutoParser.Models
{
    public class GoogleSheetSettingsModel
    {
        public string? PathToKey { get; set; }
        public string? ApplicationName { get; set; }
        public string? SpreadsheetId { get; set; }
        public string? SpreadsheetIdForSecondTable { get; set; }
        public string? SheetRange { get; set; }
        public string? ReviewBodyClassname { get; set; }
        public string? DataTimeClassname { get; set; }
        public string? AuthorsClassname { get; set; }
        public string? RankingStarsItemPropName { get; set; }
        public string? RankingStarsItemPropNameDoctu { get; set; }
        public string? doctuNamesClass { get; set; }
        public string? Selector { get; set; }
        public string? SelectorEnd { get; set; }
        public string[]? WorkUrl { get; set; }
        public string[]? WebSites { get; set; }
    }
}
