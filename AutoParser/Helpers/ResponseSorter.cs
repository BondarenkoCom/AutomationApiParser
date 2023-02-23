using HtmlAgilityPack;
using System.Globalization;

namespace AutoParser.Helpers
{
    public class ResponseSorter
    {
        public string HtmlConverter(string responseSort, string propName)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseSort);

            try
            {
              var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");
              return htmlElement?.InnerText ?? $"Element {propName} is null (Empty)";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string HtmlConverterForAllDoc(string responseSort, string propName)
        {
            var htmlDoc = new HtmlDocument();
            var figure = new FigureOutRating();
            htmlDoc.LoadHtml(responseSort);

            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");
            string styleAttribute = htmlElement.Attributes["style"].Value;

            var rankResult = figure.GetStarsRating(styleAttribute);

            return rankResult.ToString() ?? $"Element {propName} is null (Empty)";
        }

        public List<string> HtmlConverter(string responseSort)
        {
            //TODO 1 make return title
            //TODO 2 make checker "What the site?" for choose json file with data for correct parsing
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseSort);

            var title = htmlDoc.DocumentNode.SelectSingleNode("//head/title").InnerHtml;

            Console.WriteLine(title);
            return null;
        }
    }
}
