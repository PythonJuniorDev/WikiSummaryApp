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

### HTTP Requests with `HttpClient`

- I used `HttpClient` to send a GET request to the Wikipedia API.
- I explored why `HttpClient` is preferred over `HttpWebRequest` — it’s modern, simpler, and better for async.

### JSON Parsing with `JsonDocument`

- I initially tried `Newtonsoft.Json`, but found `JsonDocument` is built into .NET and more memory-efficient.
- I used `.RootElement.GetProperty("key")` to extract values.

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

> “Parsing JSON should be as simple as `JsonConvert.DeserializeObject()`.”

### ✅ Resolution

- `JsonDocument` doesn't require full deserialization and is better for reading specific properties in large JSON objects.

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
catch (HttpRequestException ex)
{
    Console.WriteLine($"Request failed: {ex.Message}");
}
```
---

## 🔄 Things I Want to Explore Next
- Improve Error Handling: Add more specific messages and custom exceptions.
- Implement Caching: Save previous requests to avoid repeated API calls.
- Convert to GUI: Use Windows Forms or WPF for a better user interface.