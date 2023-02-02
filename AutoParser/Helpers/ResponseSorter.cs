using AutoParser.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;


namespace AutoParser.Helpers
{
    public class ResponseSorter
    {
        public List<string> HtmlConverter(string responseSort,string propName)
        {
            //Make Html converter
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseSort);

            //TODO Make json file for safe classes name
            //var SelectorMake = JsonReader.GetValues().Selector + propName + JsonReader.GetValues().SelectorEnd;
            //Console.WriteLine(SelectorMake.ToString());
            //var HtmlElements = htmlDoc.DocumentNode.SelectNodes(SelectorMake);

            //propName
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
