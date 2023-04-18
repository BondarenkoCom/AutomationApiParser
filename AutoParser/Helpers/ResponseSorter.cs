using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace AutoParser.Helpers
{
    public class ResponseSorter
    {
        private readonly FigureOutRating _figure = new FigureOutRating();
        private ConvertRating _convertRating = new ConvertRating();

        private HtmlDocument LoadHtmlDocument(string htmlContent)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);
            return htmlDoc;
        }

        public string HtmlConverterForMetaValue(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//meta[@itemprop='{propName}']");

            if (htmlElement != null)
            {
                string contentValue = htmlElement.GetAttributeValue("content", "");

                string firstThreeCharacters = contentValue.Substring(0,3);

                var checkElement = _convertRating.CheckRating(firstThreeCharacters);
                return checkElement ?? $"Element {propName} is null (Empty)";
            }
            else
            {
                string error = "Node not found.";
                return error;

            }
            return null;
        }

        public string HtmlConverter(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");

            var checkElement = _convertRating.CheckRating(htmlElement.InnerText);
            return checkElement ?? $"Element {propName} is null (Empty)";
        }

        public string HtmlConverterUlanUdeZoon(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");

            if (htmlElement != null)
            {
                var innerText = htmlElement.InnerText.Trim();
                var regex = new Regex(@"(\d+,\d+)", RegexOptions.Compiled);
                var match = regex.Match(innerText);

                if (match.Success)
                {
                    var ratingValue = match.Value;
                    return ratingValue;
                }
                else
                {
                    Console.WriteLine($"Error: Unable to find the rating value");
                }
            }

            return $"Element {propName} is null (Empty)";
        }

        public string HtmlConverterForAllDoctorsInHere(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");

            if (htmlElement != null)
            {
                var dataRatingValue = htmlElement.GetAttributeValue("data-rating-value", string.Empty);
                if (!string.IsNullOrEmpty(dataRatingValue))
                {
                    return dataRatingValue;
                }
            }

            return $"Element {propName} is null (Empty)";
        }

        public string HtmlConverterVsevrachizdes(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");

            var checkElement = _convertRating.CheckRating(htmlElement.InnerText);
            return checkElement ?? $"Element {propName} is null (Empty)";
        }

        public string HtmlConverterProDoctorov(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[contains(@class, '{propName}')]");

            if (htmlElement != null)
            {
                string extractedValue = htmlElement.InnerText.Trim();

                var checkElement = _convertRating.CheckRating(extractedValue);
                return checkElement;
            }
            else
            {
                return $"Element {propName} is null (Empty)";
            }
        }

        public string HtmlConverterMedClab(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");

            var checkElement = _convertRating.CheckRating(htmlElement.InnerText, true);
            return checkElement ?? $"Element {propName} is null (Empty)";
        }

        public string HtmlConverterVrachRussia(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var ratingElement = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='vrach_stars_score']/a");

            var checkElement = _convertRating.CheckRating(ratingElement.InnerText);
            return checkElement ?? $"Element {propName} is null (Empty)";
        }


        public string HtmlConverterForAllDoc(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var htmlElement = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");
            string styleAttribute = htmlElement.Attributes["style"].Value;

            var rankResult = _figure.GetStarsRating(styleAttribute);

            var checkElement = _convertRating.CheckRating(rankResult);
            return checkElement ?? $"Element {propName} is null (Empty)";
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

            var rankResult = _figure.GetStarsRating(widthAttribute);

            var checkElement = _convertRating.CheckRating(rankResult);
            return checkElement ?? $"Element {propName} is null (Empty)";
        }

        public string HtmlConverterForDoctorLaser(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var span = htmlDoc.DocumentNode.SelectSingleNode($"//span[@class='{propName}']");
            var rankResult = span?.InnerText;

            var checkElement = _convertRating.CheckRating(rankResult);
            return checkElement ?? $"Element {propName} is null (Empty)";
        }

        public string HtmlConverterForAptekaRu(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var span = htmlDoc.DocumentNode.SelectSingleNode($"//span[@class='{propName}']");

            var rankResult = _figure.GetStarsRating(span.InnerHtml);


            var checkElement = _convertRating.CheckRating(rankResult);
            return checkElement ?? $"Element {propName} is null (Empty)";
        }


        public string HtmlConverterForGastomir(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            HtmlNode ratingNode = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}'][2]");

            HtmlNode ratingValueNode = ratingNode.SelectSingleNode(".//div[@class='value']");
            string ratingValue = ratingValueNode?.InnerText;

            var checkElement = _convertRating.CheckRating(ratingValue);
            return checkElement ?? $"Element {propName} is null (Empty)";
        }

        public List<string> HtmlConverter(string responseSort)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);
            var title = htmlDoc.DocumentNode.SelectSingleNode("//head/title").InnerHtml;

            return null;
        }

        public string HtmlConverterForEApteka(string responseSort, string propName)
        {
            var htmlDoc = LoadHtmlDocument(responseSort);

            var classNode = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");

            if (classNode == null)
            {
                return "empty";
            }

            return classNode.Descendants("li").Count().ToString();
        }

        public string CountActiveStarsAsString(string htmlContent, string propName)
        {
            var htmlDoc = LoadHtmlDocument(htmlContent);

            var classNode = htmlDoc.DocumentNode.SelectSingleNode($"//div[@class='{propName}']");

            if (classNode == null)
            {
                return "0";
            }

            int activeStarsCount = 
                classNode.Descendants("span")                             
                .Count(s => s.Attributes["class"]?.Value.Contains("vigroup-stars-elem active") == true);

            return activeStarsCount.ToString();
        }
    }
}
