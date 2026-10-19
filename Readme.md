# 🛡️ Cybersecurity Awareness Bot (Part 2 - WPF GUI & Advanced Logic)

A desktop-based interactive cybersecurity assistant built in **C# .NET 8.0 WPF (Windows Presentation Foundation)**. This application expands upon the original console bot by introducing a rich graphical user interface, audio voice greetings, dynamic sentiment detection, randomized responses, session memory, and fluid conversation flow[cite: 13, 15, 17, 19].

---

## ✨ Key Features & Rubric Alignment

1. **GUI & Aesthetic Design (`10 Marks`):**
   - Built using a custom **Oxblood (`#5C061C`) and Gold (`#E5C158`)** visual theme with clean element spacing and a custom layout.
   - Automatically renders the framed ASCII art banner directly inside the interactive chat window[cite: 13].
   - Integrated startup audio playback (`VoiceGreeting.wav`) on application launch[cite: 13].

2. **Keyword Recognition (`15 Marks`):**
   - Automatically detects security-related keywords such as `phishing`, `password`, `malware`, `ransomware`, `social engineering`, and `privacy`[cite: 17].

3. **Randomized Responses (`10 Marks`):**
   - Leverages a C# `Dictionary` and `List` structure paired with `Random` selection so that repeated queries on the same topic display fresh, informative advice[cite: 11, 17].

4. **Sentiment Detection (`10 Marks`):**
   - Detects user emotions and tones (e.g., `"worried"`, `"curious"`, `"frustrated"`) and adapts bot responses empathetically to provide encouragement or simplified explanations[cite: 15].

5. **Memory & Recall (`10 Marks`):**
   - Remembers the user's name captured during initialization and tracks user interests (e.g., remembering when a user expresses interest in a specific topic like privacy) to personalize follow-up advice[cite: 19].

6. **Conversation Flow (`10 Marks`):**
   - Supports natural follow-up prompts like `"tell me more"`, `"explain more"`, or `"another tip"` without resetting the active session context[cite: 19].

---

## 📂 Project Architecture & Components

- **`MainWindow.xaml / .xaml.cs`**: Handles the graphical interface layout, user input bindings, button events (`Send`, `Clear`, `QuickTopics`), and chat history rendering.
- **`AudioPlayer.cs`**: Static utility class managing asynchronous sound playback for the `VoiceGreeting.wav` file.
- **`UIHelper.cs`**: Encapsulates data validation rules (e.g., name validation and formatting) and ASCII banner strings.
- **`Assets/`**: Houses the multimedia files (`VoiceGreeting.wav`).

---

## 🚀 Installation & Execution Instructions

1. **Prerequisites:**
   - Ensure you have **Visual Studio 2022** installed with the **.NET Desktop Development** workload enabled (targeting **.NET 8.0**).
2. **Open the Project:**
   - Clone or download this repository, then open the `CybersecurityAwarenessBotPart2.sln` solution file in Visual Studio.
3. **Verify Build Actions:**
   - Ensure `MainWindow.xaml.cs`, `AudioPlayer.cs`, and `UIHelper.cs` have their **Build Action** set to **Compile** in the Properties window.
   - Ensure `VoiceGreeting.wav` inside the `Assets` folder has its **Build Action** set to **Content**.
4. **Run the Application:**
   - Click **Build -> Rebuild Solution**, then press **`F5`** to run the app.

---

## 📦 GitHub Release & Version Control
- **Commit History:** Maintained with over six descriptive, meaningful commit messages tracking incremental development[cite: 11].
- **Releases & Tags:** Tagged with structured version releases (`v1.0.0` for Console implementation, `v2.0.0` for WPF GUI integration).