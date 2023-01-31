using AutoParser.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;


namespace AutoParser.Helpers
{
    public class ResponseSorter
    {
        public string HtmlConverter(string responseSort)
        {
            //Make Html converter
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseSort);

            //TODO Make json file for safe classes name
            //TODO make to combine authorsText + benefitsText

            string benefitsClassname = "review__content ui-text ui-text_size_l ui-text_type_high ui-text_responsive";
            string dataTimeClassname = "review__date ui-text ui-text_size_m ui-text_type_high ui-text_responsive";

            //Make new helper for search nodes by class/id/etc
            var benefitsElements = htmlDoc.DocumentNode.SelectNodes($"//div[@class='{benefitsClassname}']");
            var dataTimeElements = htmlDoc.DocumentNode.SelectNodes($"//div[@class='{dataTimeClassname}']");

            if (benefitsElements != null)
            {
               //Fix problem with copyies first element
                foreach (var elementData in dataTimeElements)
                {
                    foreach (var elementBenefits in benefitsElements)
                    {
                      Console.WriteLine($"text - {elementBenefits.InnerText} Data - {elementData}\n---");
                      ImportInformationToGoogleDocs.PushToGoogleSheets(elementData.InnerText ,elementBenefits.InnerText);
                    }
                }
            }
            return null;
        }
    }
}
