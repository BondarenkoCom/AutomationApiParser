using System.Globalization;

namespace AutoParser.Helpers
{
    public class FigureOutRating
    {
        public string GetStarsRating(string htmlAttribute)
        {
            string extract = htmlAttribute
                .Replace("width:", "")
                .Replace("%", "")
                .Replace(";", "")
                .Replace("px", "")
                .Trim();

            if (double.TryParse(extract, NumberStyles.Float, CultureInfo.InvariantCulture, out double resExtract))
            {
                double result = (resExtract / 100) * 5;
                string formattedResult = result.ToString("F1");
                //Console.WriteLine(formattedResult);
                //Console.WriteLine($"Style width {htmlAttribute}, result after figure = {formattedResult}\n");
                return formattedResult;
            }
            else
            {
                //Console.WriteLine("Invalid input");
                return "Invalid input";
            }
        }
    }
}