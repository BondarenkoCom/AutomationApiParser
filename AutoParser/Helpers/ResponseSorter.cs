using HtmlAgilityPack;

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

            try
            {
                var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");
                string styleAttribute = htmlElement.Attributes["style"].Value;

                var rankResult = figure.GetStarsRating(styleAttribute);

                return rankResult.ToString() ?? $"Element {propName} is null (Empty)";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string HtmlConverterForKleos(string responseSort, string propName)
        {
            var htmlDoc = new HtmlDocument();
            var figure = new FigureOutRating();
            htmlDoc.LoadHtml(responseSort);

            try
            {
                var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");
                string styleAttribute = htmlElement.Attributes["style"].Value;

                var rankResult = figure.GetStarsRating(styleAttribute);

                return rankResult.ToString() ?? $"Element {propName} is null (Empty)";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string HtmlConverterForDocDoc(string responseSort, string propName)
        {
            var htmlDoc = new HtmlDocument();
            var figure = new FigureOutRating();
            htmlDoc.LoadHtml(responseSort);

            try
            {
                var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");
                //Fix bug, write wrong value --r1ktdgvx-0:80px;--r1ktdgvx-1:16px
                string styleAttribute = htmlElement.Attributes["style"].Value;
                string heightAttribute = htmlElement.Attributes["height"].Value;
                string widthAttribute = htmlElement.Attributes["width"].Value;

                Console.WriteLine($"Height: {heightAttribute}, Width: {widthAttribute}");


                //string[] cssValues = new string[] { heightAttribute, widthAttribute };
                //TODO make type value for method figure GetStarsRating
                //string styleAttributeFigure = string.Join("; ", cssValues);

                var rankResult = figure.GetStarsRating(widthAttribute);

                return rankResult.ToString() ?? $"Element {propName} is null (Empty)";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string HtmlConverterForDoctorLaser(string responseSort, string propName)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseSort);

            try
            {
                var span = htmlDoc.DocumentNode.SelectSingleNode($"//span[@class='{propName}']");
                var rankResult = span.InnerText;

                return rankResult.ToString() ?? $"Element {propName} is null (Empty)";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string HtmlConverterForGastomir(string responseSort, string propName)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseSort);

            try
            {
                HtmlNode ratingNode = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}'][2]");

                // Get the value from the "value" div element within the "rating" div element
                HtmlNode ratingValueNode = ratingNode.SelectSingleNode(".//div[@class='value']");
                string ratingValue = ratingValueNode.InnerText;

                return ratingValue.ToString() ?? $"Element {propName} is null (Empty)";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<string> HtmlConverter(string responseSort)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseSort);

            var title = htmlDoc.DocumentNode.SelectSingleNode("//head/title").InnerHtml;

            Console.WriteLine(title);
            return null;
        }
    }
}
