namespace AutoParser.Helpers
{
    public static class Logger
    {
        public static void WrtieLog(string message)
        {
            string logDirectory = JsonReader.GetValues().logPath;
            string logFileName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
            string logFilePath = Path.Combine(logDirectory, logFileName);

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}:{message}\n_____");
            }
        }
    }
}