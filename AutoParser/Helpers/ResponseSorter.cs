using AutoParser.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;


namespace AutoParser.Helpers
{
    public class ResponseSorter
    {
        public List<string> HtmlConverter(string responseSort,string classname)
        {
            //Make Html converter
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseSort);

            //TODO Make json file for safe classes name
            
            var benefitsElements = htmlDoc.DocumentNode.SelectNodes($"//div[@class='{classname}']");

            List<string> elements = new List<string>();

            if (benefitsElements != null)
            {
                foreach (var element in benefitsElements)
                {
                    elements.Add(element.InnerText);
                }
                return elements;
            }
            return null;
        }
    }
}
