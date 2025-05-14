# 📄 NOTES.md – Detailed Understanding

## 🚀 Project Overview

This project is a simple C# console application that:

- Asks the user for a topic.
- Fetches a summary from Wikipedia’s public API.
- Parses the JSON response.
- Generates two output files:
  - A styled HTML file with a summary.
  - A `.json` file with the raw response.
- Opens the HTML file in the default browser.

---

## 📚 Key Concepts Learned

### Async Programming

- `async` and `await` were used to make non-blocking HTTP requests.
- I initially thought the app could just wait for the response.
- I learned that async allows the app to remain responsive and efficient, especially in real-world scenarios.
- **Without** async/await, the UI freezes until the web request is complete — the user can’t move the window, click anything else, or cancel.

### HTTP Requests with `HttpClient`

- I used `HttpClient` to send a GET request to the Wikipedia API.
- I explored why `HttpClient` is preferred over `HttpWebRequest` — it’s modern, simpler, and better for async.
- HttpClient, all the lower-level work like creating the request, handling the response stream, and reading from it is abstracted away. 
- It wraps all those details under the hood and makes your code much cleaner, especially with async/await

### JSON Parsing with `JsonDocument`

- I learned how to safely access data in a JSON response using JsonDocument, part of .NET's built-in System.Text.Json namespace.
- Instead of deserializing the entire JSON into C# objects, I accessed only the data I needed directly from the JSON structure.
- This avoids full deserialization and gives me more control when handling structured API data.
- It also reduces unnecessary memory use — you're only storing what you actually need.
- I used .RootElement.GetProperty("key") and TryGetProperty() to reduce the chance of errors if fields are missing or the structure changes.
- The top-level JSON object typically includes two key fields: "title" and "extract", which — in the case of the WikiSummaryApp — contain the article’s title and summary.

### Error Handling

- I added `try-catch` blocks to handle:
  - Network issues
  - JSON parsing errors
  - Unexpected exceptions

---

## ❌ Misconceptions & ✅ Resolutions

### ❌ Misconception 1

> “I thought I could use `HttpClient.GetAsync()` without marking the method `async`.”

### ✅ Resolution

- You must use `await` in an `async` method to avoid blocking the thread.
- Async allows the program to handle other tasks while waiting for I/O.

---

### ❌ Misconception 2

> “I thought JSON parsing in .NET would be fully automatic and straightforward.”

### ✅ Resolution

- I learned that while .NET provides tools like JsonDocument, safely accessing properties often requires checking whether fields exist (e.g., using TryGetProperty()), especially when working with dynamic or unpredictable API responses.
- This gives more control but also means I need to write more precise and defensive code — checking if a field exists before reading it — to prevent runtime errors.

---

## 📝 Reflections on Challenges

### 💡 Path Handling

> I didn’t understand why I needed `Path.Combine()`.

- `Path.Combine()` handles platform-specific separators (`/` vs `\`) — essential for cross-platform apps.

### 💡 Opening Files

> I didn’t know why `UseShellExecute = true` was necessary.

- It tells the system to open the file with the default application (e.g., browser for `.html`).

---

## 🔧 Key Code Insights

### Making Async HTTP Calls

```csharp

Sending the HTTP request

using (HttpClient client = new HttpClient())
{
    HttpResponseMessage response = await client.GetAsync(requestUri);
    response.EnsureSuccessStatusCode();
    string jsonResponse = await response.Content.ReadAsStringAsync();
}

Parsing JSON

using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
{
    string summary = doc.RootElement.GetProperty("extract").GetString();
}

HTML File Creation

string htmlContent = $"<html><head><title>{title}</title></head><body><h1>{title}</h1><p>{summary}</p></body></html>";
File.WriteAllText(filePath, htmlContent);

Handling Exceptions

try
{
    var response = await client.GetAsync(requestUri);
    response.EnsureSuccessStatusCode();
}
catch (HttpRequestException e)
{
    Console.WriteLine($"Request failed: {e.Message}");
}
```
---

## 🔄 Things I Want to Explore Next
- Improve Error Handling: Add more specific messages and custom exceptions.
- Implement Caching: Save previous requests to avoid repeated API calls.
- Convert to GUI: Use Windows Forms or WPF for a better user interface.
(WPF = Windows Presentation Foundation: UI framework by Microsoft used to build rich desktop applications on Windows.)