# 📄 NOTES.md - Detailed Understanding

## 🚀 Project Overview
This project is a simple C# console application that fetches a summary of a user-provided topic from Wikipedia’s public API, processes the response, and generates two files (HTML and json). One file contains a summary of the topic, and the other shows the raw JSON response.


## 📚 Key Concepts Learned
Async Programming
async and await keywords were used to make non-blocking HTTP requests. At first, I didn’t understand why async is necessary. I thought the app could just wait for the response and continue. However, after diving into async programming, I realized that it allows the app to remain responsive during API calls, which is crucial for efficiency in real-world applications.


## HTTP Requests with HttpClient

I learned how to send an HTTP GET request using HttpClient to fetch data from Wikipedia's API. Initially, I wasn't sure why I needed HttpClient instead of simply using HttpWebRequest. But after researching, I found that HttpClient is the more modern and efficient tool for handling HTTP requests in .NET.


## JSON Parsing

The JsonDocument class in .NET is used to parse JSON responses from the Wikipedia API. I was unfamiliar with the JsonDocument class at first, so I tried using Newtonsoft.Json. But JsonDocument turned out to be part of .NET’s native API and is more memory-efficient when handling large JSON objects. I learned how to access nested values by using rootElement.GetProperty("key").


## Error Handling

I added error handling with try-catch blocks to handle potential issues like network failures or invalid JSON responses. Before this, I didn’t consider handling every error that could occur during the HTTP request and response phases. After encountering some errors during testing, I realized it’s crucial to anticipate and handle exceptions.


# 💡 Misconceptions & Resolutions

## Misconception 1
"I thought I could use HttpClient synchronously and simply call .GetAsync() without needing to mark the method as async."

## Resolution
I learned that calling GetAsync() inside an async method is necessary to avoid blocking the thread. This is because async methods allow the program to run other tasks while waiting for the HTTP request to complete.


## Misconception 2
"I thought parsing JSON in C# would be as simple as JsonConvert.DeserializeObject(), but it’s more nuanced."

## Resolution
I discovered JsonDocument works better for accessing specific values from large JSON objects. The JsonDocument class does not require deserialization, which is more efficient when working with structured data like the one returned by Wikipedia’s API.


# 📝 Short Reflections on Challenges Faced
## Reflection 1
"At first, I didn’t understand why I needed to use Path.Combine() instead of just using strings to combine file paths. But then, I realized that Path.Combine() automatically handles platform-specific path separators (like / vs \), which is essential when working on cross-platform projects."

## Reflection 2
"The UseShellExecute = true property was a mystery at first. I didn’t understand why it was necessary to open the generated HTML file in the browser. I learned that this property tells the system to use the default application to open the file, which in this case is the web browser."


# 🔧 Key Code Insights

## Making Async HTTP Calls

HttpClient was used to send an asynchronous GET request to the Wikipedia API, followed by await to ensure the program doesn’t block while waiting for the response.

Example:
using (HttpClient client = new HttpClient())
{
    HttpResponseMessage response = await client.GetAsync(requestUri);
    response.EnsureSuccessStatusCode();
    string jsonResponse = await response.Content.ReadAsStringAsync();
}


## Parsing JSON

JsonDocument is used to parse the JSON data. I learned that the GetProperty() method can be used to extract specific fields from a JSON object. Using this allowed me to avoid deserializing the entire JSON object into a class.

Example:

using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
{
    var summary = doc.RootElement.GetProperty("extract").GetString();
}


## HTML File Creation

File.WriteAllText() was used to create HTML files. The HTML content was dynamically built from the API response, with simple string interpolation to insert values such as the topic title and summary.

Example:

string htmlContent = $"<html><head><title>{title}</title></head><body><h1>{title}</h1><p>{summary}</p></body></html>";
File.WriteAllText(filePath, htmlContent);


## Handling Exceptions

I added try-catch blocks to handle potential network issues, invalid JSON, or other unforeseen exceptions.

Example:

try
{
    var response = await client.GetAsync(requestUri);
    response.EnsureSuccessStatusCode();
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Request failed: {ex.Message}");
}


## 🔄 Things I Want to Explore Next
Improve Error Handling: I could make error messages more specific and create custom exceptions for better clarity.

Implement Caching: I want to implement a caching mechanism that saves previous requests, so users don’t have to wait for repeated calls to the same topic.

Convert to GUI: I could convert this app into a GUI-based application using Windows Forms or WPF for better user experience.