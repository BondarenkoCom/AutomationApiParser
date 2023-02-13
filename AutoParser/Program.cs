using AutoParser.Helpers;

Console.WriteLine("Start parsing");

var readGoogleSheets = new ReadGoogleSheets();
await readGoogleSheets.GetDataFromGoogleSheets();

Console.ReadLine();
