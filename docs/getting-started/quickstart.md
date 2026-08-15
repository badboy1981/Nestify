# ⚡ Quick Start

Nestify is installed and ready. Follow these short steps to get your first useful results in under two minutes.

!!! tip "Run from anywhere"
    Nestify works globally. Open a terminal inside **any project folder** and run the commands below.  
    You can also target another path with `--path ./some/folder`.

---

## ✅ 1. Confirm the Installation

```bash
nestify --version
```

You should see the version information. You can also explore all available commands:

```bash
nestify --help
```

---

## 🧹 2. Remove Project Noise

Before scanning, apply an ignore template so build artifacts and dependency folders are filtered out.

```bash
nestify ignore-list
```

Then apply the template that matches your project:

```bash
nestify ignore-use go
```

Replace `go` with any other available template (`nodejs`, `python`, `dotnet`, `flutter`, …).

This creates a local `.nestifyignore` file in the current directory.

---

## 🌳 3. See the Folder Structure

Generate a clean, readable tree of your project:

```bash
nestify scan --tree -d 2
```

!!! tip "Useful `scan` parameters"
    - `--tree` → produces a human-readable Markdown tree  
    - `-d 2` → limits scan depth to 2 levels  
    - `--folders-only` → shows only folders (no files)

The report is saved inside the `Nestify-Report/` folder.

---

## 🧠 4. Generate an AI-Ready Context Report

Create a unified Markdown report that combines metrics, language distribution, and the project tree — perfect for pasting into an LLM:

```bash
nestify context -p architecture -d 2
```

- `-p architecture` injects the built-in architecture prompt template  
- You can also pass custom text: `-p "Review this structure for clean architecture"`

The output is saved as `Nestify-Report/ai_context_report.md`.

---

## 📊 5. Optional — Project Metrics

Get a quick overview of language distribution and project size:

```bash
nestify analyze
```

---

## 📁 Where Are the Reports?

All generated files are automatically placed in a `Nestify-Report/` directory in your current working folder:

- JSON structure reports
- Markdown tree reports
- `ai_context_report.md`
- `skeleton_report.md`

---

## ➡️ Next Step

Explore the full command reference:

→ [CLI Commands Overview](../commands/overview.md)
