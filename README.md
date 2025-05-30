# 📰 WikiSummaryApp
WikiSummaryApp

---

## 🔍 What This Project Does ✅

- Asks the user for a topic.
- Fetches the summary from Wikipedia's public API.
- Parses JSON response safely.
- Saves the raw JSON response as a `.json` file (for inspection)
- Generates a styled HTML file containing the title, summary, and an optional image.
- Opens the HTML file in the default browser.

---

## 📌 Why I Built This

Built as a personal learning project with deep analysis of every step: understanding exactly how every line works.
I deliberately worked on concepts that felt outside my comfort zone: an opportunity to strengthen my weak spots by diving into the unknown. 
I was looking to explore code that could help me build something beyond my current skills. I used ChatGPT not just to generate code, but to **challenge myself to understand every decision**, line by line.

---

## 🧠 How I Worked With ChatGPT

I use ChatGPT as a tool. I don't blindly accept what is presented to me — staying critical keeps me sharp.
I documented every step and question I asked, and my understanding of the answers.

You can read a summary of key insights in [`NOTES.md`](./NOTES.md).

---

## 🎯 What This Project Shows

This project is part of my learning journey as a junior developer. I created it to:

- Practice working with HTTP requests and JSON in C#
- Learn to parse API responses using `System.Text.Json`
- Understand async/await patterns and file handling
- Explore how to create and open HTML files from C#

I used **AI tools (like ChatGPT)** and **online documentation** to help me understand unfamiliar concepts. I asked many "beginner" questions along the way, and wrote **extensive internal comments** as notes to myself to reinforce my understanding.

---

## 📂 Output

The app creates two files:

- A summary HTML page with title, description, and optional image (✅ opens automatically)
- A raw JSON view, formatted safely inside a `<pre>` tag (saved only)

These are saved in an `output/` folder. Only the summary file is opened automatically in the default browser.

---

## 👨‍💻 Learning Focus

  Some areas I deep-dived into:
- Asynchronous programming with async/await
- The using statement — especially for HttpClient and resource cleanup (e.g., sockets, file handles)
- Why JsonDocument.Parse() returns a disposable type
- Making async HTTP requests in C#
- Handling HTTP errors with HttpResponseMessage.EnsureSuccessStatusCode()
- Safe JSON parsing using TryGetProperty()
- Working with JSON in .NET
- Handling exceptions and errors properly
- Formatting and saving data as HTML
- Encoding raw JSON for HTML display using WebUtility.HtmlEncode() (to prevent injection)
- Cross-platform file path handling with Path.Combine() (instead of hardcoding separators)
- Using file and directory operations (System.IO)
- Automatically opening files with the default program using System.Diagnostics.ProcessStartInfo and UseShellExecute = true

---

## 💡 What I Learned in general

Throughout this project, I focused on understanding:
- The structure of a simple app (e.g. file handling, logic separation)
- How to validate and trust — but also question — code generated by AI

More details in [`NOTES.md`](./NOTES.md)

---

## 💡 How I Learn

This code reflects how I learn:

- I use AI (like ChatGPT) and online resources to guide my exploration.
- I dive deep into parts I don’t fully understand — like how `HttpClient` works under the hood or what `JsonDocument` really does.
- I add detailed comments (in my personal copy) to reinforce what I learn — especially for parts experienced developers might skip over.
- I constantly ask “why” — not just “how.”

---

## 🛠️ Tech Stack

- C# 9 / .NET 6
- Wikipedia REST API
- Async/await (asynchronous programming model)
- JSON processing (`JsonDocument`)
- Basic HTML generation
- File I/O with `System.IO`
- Console application

---

## 🙌 Inspired By ✅

- [Wikipedia REST API docs](https://en.wikipedia.org/api/rest_v1/)
- .NET documentation
- ChatGPT explanations, deep dives, and debugging help
- Stack Overflow
- Markdown cheat sheets

---

## 💡 Future Ideas

- Add language selection (fetch summaries in different languages)
- Cache results locally

---

## 🚀 How to Run

```bash
git clone https://github.com/PythonJuniorDev/WikiSummaryApp.git
cd WikiSummaryApp
dotnet build
dotnet run
```