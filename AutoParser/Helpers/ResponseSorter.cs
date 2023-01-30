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
            //var parseModel = JsonConvert.DeserializeObject<ReviewFromUteka>(responseSort);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseSort);

            //var titleNode = htmlDoc.DocumentNode.SelectSingleNode("//title");
            //string titleText = titleNode.InnerText;
            //var json = JsonConvert.SerializeObject(new { title = titleText });

            //TODO Make json file for safe classes name
            //TODO make to combine authorsText + benefitsText
            var benefitsText = htmlDoc.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "") == 
            "review__content ui-text ui-text_size_l ui-text_type_high ui-text_responsive");

            var authorsText = htmlDoc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("itemprop", "") == "author");

            //var combineTexts = (benefitsText , authorsText);

            //var divNode = htmlDoc.DocumentNode.SelectSingleNode("//title");
            //string divText = divNode.InnerText;
            //var json = JsonConvert.SerializeObject(new { div = divText });
            //List<string> combineTexts = new List<string>();
            //combineTexts.Add(benefitsText.ToString());
            //combineTexts.Add(authorsText.ToString());

            foreach (var text in authorsText)
            {
                return text.InnerHtml;
                //return text;
            }

            //foreach (var benefits in benefitsText)
            //{
            //    return benefits.InnerHtml;
            //}
            
            return null;
            //TODO Make array data with name customer,rating product,data and text review
            //< div class="review__content ui-text ui-text_size_l ui-text_type_high ui-text_responsive">
            //<strong> Достоинства: </strong><span> Быстро снимает боль</span></div>

            //< div class="review__content ui-text ui-text_size_l ui-text_type_high ui-text_responsive">
            //<strong> Достоинства: </strong><span> Лучшее средства от головной боли</span></div>

            //return json;
            //return responseSort;
        }
    }
}
