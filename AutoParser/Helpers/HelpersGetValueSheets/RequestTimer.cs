namespace AutoParser.Helpers.HelpersGetValueSheets
{
    public class RequestTimer
    {
        private async Task DelayBasedOnRequestCount(int requestCounter, int maxRequests, int delaySeconds)
        {
            if (requestCounter >= maxRequests)
            {
                Console.WriteLine($"Reached maximum requests ({maxRequests}), waiting for {delaySeconds} seconds...");
                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                Console.WriteLine($"Finished waiting, resuming requests.");
                requestCounter = 0;
            }
        }
    }
}
