using System.Globalization;

namespace AutoParser.Helpers
{
    public class FigureOutRating
    {
        public string GetStarsRating(string HtmlAttribute)
        {

            string att = HtmlAttribute;
            //string extract = att.Replace("width:", "")
            //    .Replace("%", "")
            //    .Replace(";", "").Trim().ToString();
            string extract = att.Replace("width:", "")
                   .Replace("%", "")
                   .Replace(";", "")
                   .Replace("px", "").Trim().ToString();


            try
            {
                double resExtract = double.Parse(extract, CultureInfo.InvariantCulture);

                double result = (resExtract / 100) * 5;
                double doubleValue;

                string formattedResult = result.ToString("F1");
                Console.WriteLine(formattedResult);
                Console.WriteLine($"style width {HtmlAttribute} , result after figure = {formattedResult}\n");

                return formattedResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
            return null;
        }
    }
}