using AutoParser.Helpers;
using AutoParser.WebDriver;

Console.WriteLine("Commands:");

ApiWebDriver _apiWebDriver = new ApiWebDriver();

var request = _apiWebDriver.RunDriverClient(url: JsonReader.GetValues().WorkUrl);
Console.WriteLine(request.Result);

Console.WriteLine("Is Ready:");
Console.ReadLine();
