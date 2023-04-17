using System.Globalization;
using System.Text.RegularExpressions;

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
                .Replace("<s style=", "")
                .Replace("/s", "")
                .Trim();

            string pattern = @"\d+";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(extract);

            string extractedNumber = match.Value;
            Console.WriteLine(extractedNumber);

            if (double.TryParse(extractedNumber, NumberStyles.Float, CultureInfo.InvariantCulture, out double resExtract))
            {
                double result = (resExtract / 100) * 5;
                string formattedResult = result.ToString("F1");
                return formattedResult;
            }
            else
            {
                return "Invalid input";
            }
        }
    }
}