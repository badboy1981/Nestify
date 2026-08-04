# 🧠 Analyze Command (`nestify analyze`)

The `analyze` command evaluates the overall structure of your project directory. It strips out build artifacts using `.nestifyignore`, calculates source-code language distribution based on real files, and aggregates directory metrics (file counts, folder counts, and total size).

It also outputs a compact, prompt-ready JSON summary intended for immediate copy-pasting into Large Language Model (LLM) workflows.

---

## 🛠️ Usage & Options

```bash
nestify analyze [options]

```

### Flags & Options

| Flag | Short | Description | Default |
| --- | --- | --- | --- |
| `--path` | `-p` | Target directory path to analyze. | `.` (Current Directory) |
| `--depth` | `-d` | Limits directory analysis depth (`0` for unlimited). | `0` |

!!! info "Cross-Platform & Shell Agnostic"
     Nestify is compiled into a single native binary with zero external dependencies. All CLI commands run identically across **Windows (PowerShell / CMD)**, **macOS**, and **Linux (Bash / Zsh)**. Learn more about [Shell Agnostic Execution](https://www.google.com/search?q=../getting-started/quickstart.md%23cross-platform-execution).

---

## 💡 Examples & Common Use Cases

### 1. Analyze Current Working Directory

Evaluates the current project layout and saves the report to `Nestify-Report/skeleton_report.md`.

```bash
nestify analyze

```

### 2. Limit Traversal Depth

Restricts analysis and language breakdown to top-level project directories up to depth level 2.

```bash
nestify analyze -d 2

```

### 3. Analyze External Project Path

Targets any external repository path without navigating away from your working directory.

```bash
nestify analyze --path ./projects/MyDotNetApp -d 3

```

---

## 📄 Output Overview (`skeleton_report.md`)

When `nestify analyze` finishes running, it creates a structured report inside the `Nestify-Report/` folder containing project metrics, visual language progress bars, and a prompt-ready JSON summary.

### Real Output Example:

> **Scan Depth:** Unlimited

# 🧠 Nestify Project Analysis Report

## 📊 Project Metrics

* **Total Size:** 141.97 KB
* **Total Files:** 62
* **Total Folders:** 20

## 🌐 Languages Breakdown

* Markdown     `███░░░░░░░`   38.0% (16 files, 53.9 KB)
* Text         `██░░░░░░░░`   29.7% (23 files, 42.2 KB)
* Go           `█░░░░░░░░░`   19.3% (18 files, 27.3 KB)
* Other        `█░░░░░░░░░`    9.4% (2 files, 13.3 KB)
* JSON         `█░░░░░░░░░`    3.6% (3 files, 5.2 KB)

---

### 🤖 Prompt-Ready Summary for AI Analysis

```json
{
  "total_files": 62,
  "total_size_bytes": 145381,
  "top_languages": [
    {"language": "Markdown", "percentage": 38.0},
    {"language": "Text", "percentage": 29.7},
    {"language": "Go", "percentage": 19.3},
    {"language": "Other", "percentage": 9.4},
    {"language": "JSON", "percentage": 3.6}
  ]
}

```

---

## ⚙️ Language Breakdown Logic

Nestify scans file extensions throughout your project while adhering strictly to `.nestifyignore`. By suppressing heavy third-party dependencies (`node_modules/`, `vendor/`, `bin/`, `obj/`), the language statistics accurately reflect your custom codebase composition rather than generated or external code.