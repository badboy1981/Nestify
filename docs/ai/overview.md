# 💬 AI Context & Prompt Engineering

Nestify is built for **local, offline-first AI workflows**.  
It never sends your codebase to the cloud. Instead, it prepares a clean, token-efficient Markdown report that you can paste into any LLM (ChatGPT, Claude, Gemini, and others).

The core idea is simple:

1. Filter noise with `.nestifyignore`
2. Generate a unified context report
3. Optionally inject a focused prompt template
4. Copy the report into your AI tool of choice

---

## 🧠 What the AI Context Report Contains

Running `nestify context` produces `Nestify-Report/ai_context_report.md` with:

| Section | Description |
|---------|-------------|
| **AI Task & Instructions** | Optional header from `-p` (embedded template or custom text) |
| **Project Metrics** | Total size, file count, folder count |
| **Languages Breakdown** | Extension-based language distribution (GitHub-style bars) |
| **Project Directory Tree** | ASCII tree of the scanned structure |

Because build artifacts and dependency folders are filtered out first, the report stays small and useful for LLM token limits.

---

## ⚡ Recommended AI Workflow

```bash
# 1. Reduce noise
nestify ignore-use go

# 2. Generate context with an architecture-focused prompt
nestify context -p architecture -d 2

# 3. Open the report and paste it into your LLM
#    Nestify-Report/ai_context_report.md
```

!!! tip "Run from anywhere"
    Open a terminal inside the project you want to analyze. Nestify works globally from any directory.

---

## 🛠️ Related Commands

| Command | Role |
|---------|------|
| [`nestify context`](../commands/context.md) | Build the unified AI-ready report |
| [`nestify prompt-list`](../commands/prompt.md) | List embedded prompt templates |
| [`nestify prompt`](../commands/prompt.md) | Print the full text of a template |
| [`nestify ignore-use`](../commands/ignore.md) | Apply tech-stack ignore rules before context |

---

## 📋 Prompt Templates at a Glance

Nestify ships with embedded prompts under `templates-prompts/`:

- `default` — general architecture review
- `architecture` — Clean / Onion / Event-Driven checks
- `refactor` — modular layout and coupling issues
- `security` — structural AppSec risks
- `desktop` — desktop app organization patterns

Full descriptions and usage are in [Embedded Prompt Templates](prompt-templates.md).

---

## ✏️ Custom Instructions

You are not limited to built-in templates. Pass any text to `-p`:

```bash
nestify context -p "Review this Go project for hexagonal architecture violations" -d 3
```

If the value matches a template name, that template is injected.  
Otherwise the string is used as a custom instruction block at the top of the report.

---

## 🔒 Privacy

- All scanning and report generation run **locally**
- No telemetry
- No network calls from Nestify itself
- You decide what leaves your machine when you paste the report into an LLM

---

## ➡️ Next Steps

- Browse the [prompt template catalog](prompt-templates.md)
- Read the full [`context` command reference](../commands/context.md)
- Start from the [Quick Start](../getting-started/quickstart.md) if you have not run Nestify yet
