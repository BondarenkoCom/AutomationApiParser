using HtmlAgilityPack;

namespace AutoParser.Helpers
{
    public class ResponseSorter
    {
        public List<string> HtmlConverter(string responseSort,string propName)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseSort);

                var HtmlElements = htmlDoc.DocumentNode.SelectNodes($"//div[@class='{propName}']");

                List<string> elements = new List<string>();

                if (HtmlElements != null)
                {
                    foreach (var element in HtmlElements)
                    {
                        elements.Add(element.InnerText);
                    }
                    return elements;
                }
                else
                {
                    List<string> elementsMessage = new List<string>();
                    elementsMessage.Add($"Element {propName} is null(Empty)");
                    return elementsMessage;
                }
                return null;           
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
