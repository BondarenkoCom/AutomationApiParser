using AutoParser.Helpers;
using AutoParser.WebDriver;

Console.WriteLine("Commands:");

ApiWebDriver _apiWebDriver = new ApiWebDriver();

string _url = "https://uteka.ru/lekarstvennye-sredstva/obezbolivayushhie-sredstva/nurofen-ekspress-forte/reviews/";

var request = _apiWebDriver.RunDriverClient(_url);
Console.WriteLine(request.Result);

//ImportInformationToGoogleDocs.PushToGoogleSheets(request.Result);
Console.WriteLine("Is Ready:");
Console.ReadLine();
