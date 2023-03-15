using HtmlAgilityPack;

namespace AutoParser.Helpers
{
    public class ResponseSorter
    {
        private readonly FigureOutRating _figure = new FigureOutRating();

        private HtmlDocument LoadHtmlDocument(string htmlContent)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);
            return htmlDoc;
        }

        public string HtmlConverter(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");
            return htmlElement?.InnerText ?? $"Element {propName} is null (Empty)";
        }

        public string HtmlConverterForAllDoc(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");
            string styleAttribute = htmlElement.Attributes["style"].Value;

            var rankResult = _figure.GetStarsRating(styleAttribute);

            return rankResult.ToString() ?? $"Element {propName} is null (Empty)";
        }

        public string HtmlConverterForKleos(string responseSort, string propName)
        {
            return HtmlConverterForAllDoc(responseSort, propName);
        }

        public string HtmlConverterForDocDoc(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");
            string widthAttribute = htmlElement.Attributes["width"].Value;

            Console.WriteLine($" Width: {widthAttribute}");

            var rankResult = _figure.GetStarsRating(widthAttribute);

            return rankResult.ToString() ?? $"Element {propName} is null (Empty)";
        }

        public string HtmlConverterForDoctorLaser(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var span = htmlDoc.DocumentNode.SelectSingleNode($"//span[@class='{propName}']");
            var rankResult = span?.InnerText;

            return rankResult ?? $"Element {propName} is null (Empty)";
        }

        public string HtmlConverterForGastomir(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            HtmlNode ratingNode = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}'][2]");

            HtmlNode ratingValueNode = ratingNode.SelectSingleNode(".//div[@class='value']");
            string ratingValue = ratingValueNode?.InnerText;

            return ratingValue ?? $"Element {propName} is null (Empty)";
        }

        public List<string> HtmlConverter(string responseSort)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var title = htmlDoc.DocumentNode.SelectSingleNode("//head/title").InnerHtml;

            Console.WriteLine(title);
            return null;
        }
    }
}
