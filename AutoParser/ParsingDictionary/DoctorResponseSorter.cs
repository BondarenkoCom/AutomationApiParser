using AutoParser.Helpers;
using AutoParser.Interfaces;

namespace AutoParser.ParsingDictionary
{
    public class DoctorResponseSorter : IResponseSorter
    {
        public Dictionary<string, Func<string, string, string>> GetResponseSorterMethods()
        {
            return new Dictionary<string, Func<string, string, string>> {
               { "doctu.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemPropNameDoctu) },
               { "all-doc.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForAllDoc(responseSort, JsonReader.GetValues().RankingStarsItemPropNameAllDoc) },
               //{ "spb.callmedic.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemPropNameCallmedic) },
               { "doktorlaser.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForDoctorLaser(responseSort, JsonReader.GetValues().RankingStarsItemPropNameDoktorlaser) },
               { "gastromir.com", (responseSort, propName) => new ResponseSorter().HtmlConverterForGastomir(responseSort, JsonReader.GetValues().RankingStarsItemPropNameGastomir) },
               { "syktyvkar.infodoctor.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemPropNameInfodoctor) },
               { "kleos.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForKleos(responseSort, JsonReader.GetValues().RankingStarsItemPropNameKleos)},
               { "spb.docdoc.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForDocDoc(responseSort, JsonReader.GetValues().RankingStarsItemSpbDocdoc)},
               { "spb.infodoctor.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemSpbInfodoctor)},
               { "krasotaimedicina.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemKrasotaimedicina)},
               { "like.doctor", (responseSort, propName) => new ResponseSorter().HtmlConverterForDoctorLaser(responseSort, JsonReader.GetValues().RankingStarsItemLikeDoctor)},
               { "med-otzyv.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForMetaValue(responseSort, JsonReader.GetValues().RankingStarsItemMedOtzyv)},
               { "vrach-russia.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterVrachRussia(responseSort, JsonReader.GetValues().RankingStarsItemVrachRussia)},
               { "meddoclab.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterMedClab(responseSort, JsonReader.GetValues().RankingStarsItemMeddoClab)},
               { "prodoctorov.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterProDoctorov(responseSort, JsonReader.GetValues().RankingStarsItemProDoctorov)},
               { "spb.vsevrachizdes.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForAllDoctorsInHere(responseSort, JsonReader.GetValues().RankingStarsItemAllDoctorsInHere)},
               { "ulan-ude.zoon.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterUlanUdeZoon(responseSort, JsonReader.GetValues().RankingStarsItemZoonUlanUde)},
               { "spb.zoon.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterUlanUdeZoon(responseSort, JsonReader.GetValues().RankingStarsItemZoonUlanUde)}
            };
        }
    }
}
