using AutoParser.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;


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
            return null;
        }
    }
}
