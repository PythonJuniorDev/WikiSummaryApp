using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    // Main entry point of the program 
    // async Task allows using 'await' and means the method returns a Task instead of void)
    static async Task Main()
    
    {
        // Ask the user to enter a topic
        Console.Write("Enter a topic: ");
        string? input = Console.ReadLine();

        // Stop if the input is empty or just spaces
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Topic cannot be empty.");
            return;
        }

        // Prepare the topic for the URL by replacing spaces with underscores
        string topic = input;
        string urlTopic = topic.Replace(' ', '_');

        // Create the Wikipedia summary API URL
        string url = $"https://en.wikipedia.org/api/rest_v1/page/summary/{urlTopic}";

        try
        {
            // Create a new object to make the web request
            using HttpClient client = new HttpClient();

            // Send the web request and wait for the response
            HttpResponseMessage response = await client.GetAsync(url);

            // Throw an error if the response was not successful
            response.EnsureSuccessStatusCode();

            // Read the response body as a string
            string json = await response.Content.ReadAsStringAsync();

            // Parse the string into a JSON object
            using JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            // Get the title and summary text from the JSON document
            string title = root.GetProperty("title").GetString() ?? "No title";
            string summary = root.GetProperty("extract").GetString() ?? "No summary";

            // Try to get an image URL if one is available
            string? imageUrl = null;
            if (root.TryGetProperty("thumbnail", out JsonElement thumbnail) &&
                thumbnail.TryGetProperty("source", out JsonElement source))
            {
                imageUrl = source.GetString(); // Set imageUrl if found
            }

            // Show the title and summary in the console
            Console.WriteLine($"\nTitle: {title}");
            Console.WriteLine($"Summary: {summary}");

            // Make a safe file name based on the topic
            string fileFriendlyName = topic.Replace(' ', '_').ToLower();

            // Make sure the "output" folder exists
            string outputDir = "output";
            Directory.CreateDirectory(outputDir);

            // Set the file path for the summary HTML file
            string summaryFilePath = Path.Combine(outputDir, $"{fileFriendlyName}_summary.html");

            // Create a simple HTML page with the summary and optional image
            string summaryHtml =
            $@"<html>
            <head>
                <title>{title}</title>
            </head>
            <body>
                <h1>{title}</h1>
                <p>{summary}</p>
                {(imageUrl != null ? $"    <img src=\"{imageUrl}\" alt=\"{title}\" style=\"max-width:300px;\" />" : "")}
            </body>
            </html>";

            // Save the summary HTML to a file
            await File.WriteAllTextAsync(summaryFilePath, summaryHtml);
            Console.WriteLine($"Summary saved to: {summaryFilePath}");

            // Open the summary in the web browser
            OpenInBrowser(summaryFilePath);

            // Create and save an HTML file with the full raw JSON (for debugging or learning)
            string rawHtmlPath = Path.Combine(outputDir, $"{fileFriendlyName}_raw.html");
            string rawHtml =
            $@"<html>
            <head>
                <title>Raw JSON for {title}</title>
            </head>
            <body>
                <pre>{System.Net.WebUtility.HtmlEncode(json)}</pre>
            </body>
            </html>";

            await File.WriteAllTextAsync(rawHtmlPath, rawHtml);
            Console.WriteLine($"Raw JSON saved to: {rawHtmlPath}");
        } // client is automatically cleaned up
        catch (HttpRequestException e)
        {
            // Show an error message if the web request fails
            Console.WriteLine("Error fetching data: " + e.Message );
        }
    }

    // This method opens a file in the default web browser
    static void OpenInBrowser(string filePath)
    {
        string fullPath = Path.GetFullPath(filePath);
        try
        {
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = fullPath,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }
        catch (Exception ex)
        {
            // Show a message if the browser could not be opened
            Console.WriteLine("Could not open file in browser: " + ex.Message);
        }
    }
}
