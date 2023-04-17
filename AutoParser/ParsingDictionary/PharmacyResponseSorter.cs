using AutoParser.Helpers;
using AutoParser.Interfaces;

namespace AutoParser.ParsingDictionary
{
    public class PharmacyResponseSorter : IResponseSorter
    {
        public Dictionary<string, Func<string, string, string>> GetResponseSorterMethods()
        {
            return new Dictionary<string, Func<string, string, string>>
            {
               { "apteka.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForAptekaRu(responseSort, JsonReader.GetValues().RankingStarsAptekaRu)}
               { "eapteka.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemPropNameDoctu) },
               { "planetazdorovo.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemPropNameDoctu) },
               { "uteka.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemPropNameDoctu) }
            };
        } 
    }
}
