namespace AutoParser.Helpers
{
    public class ConvertRating
    {
        public string CheckRating(string htmlElement, bool isForMeddClab = false)
        {
            Console.WriteLine(htmlElement);

            string trimmedElement = htmlElement.Trim();

            // Remove "Рейтинг:" if isForMeddClab is true
            if (isForMeddClab && trimmedElement.Contains("Рейтинг:"))
            {
                trimmedElement = trimmedElement.Replace("Рейтинг:", "").Trim();
            }

            if (trimmedElement.Contains("."))
            {
                string replacedString = trimmedElement.Replace(".", ",");
                return replacedString;
            }
            else
            {
                return trimmedElement;
            }
        }
    }
}
