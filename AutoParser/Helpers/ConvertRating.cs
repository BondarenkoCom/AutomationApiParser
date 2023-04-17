namespace AutoParser.Helpers
{
    public class ConvertRating
    {
        public string CheckRating(string htmlElement, bool isForMeddClab = false)
        {

            string trimmedElement = htmlElement.Trim();

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
