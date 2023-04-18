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
               { "apteka.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForAptekaRu(responseSort, JsonReader.GetValues().RankingStarsAptekaRu)},
               //{ "eapteka.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForEApteka(responseSort, JsonReader.GetValues().RankingStarsEApteka)},
               //{ "planetazdorovo.ru", (responseSort, propName) => new ResponseSorter().CountActiveStarsAsString(responseSort, JsonReader.GetValues().RankingStarsPlanetaZdorovo)},
               { "uteka.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemUteka)}
            };
        } 
    }
}
