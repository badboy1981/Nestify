# 💬 Prompt Management (`prompt-list` & `prompt`)

Nestify comes with a set of embedded, pre-built **Prompt Templates** designed for common LLM codebase analysis tasks (e.g., Clean Architecture checks, refactoring guides, security audits, and desktop app code reviews).

The prompt management commands allow you to discover available embedded prompts and inspect their exact text directly from your terminal.

---

## 🛠️ Commands Reference

### 1. List Available Prompts (`prompt-list`)
Displays all prompt templates built directly into the Nestify binary.

```bash
nestify prompt-list

```

### 2. View Prompt Content (`prompt`)

Prints the full instruction text of a specific embedded prompt template to the console.

```bash
nestify prompt <template-name>

```

!!! info "Cross-Platform & Shell Agnostic"
    Nestify is compiled into a single native binary with zero external dependencies. All CLI commands run identically across **Windows (PowerShell / CMD)**, **macOS**, and **Linux (Bash / Zsh)**. Learn more about [Shell Agnostic Execution](https://www.google.com/search?q=../getting-started/quickstart.md%23cross-platform-execution).

---

## 💡 Usage Workflow & Examples

### Step 1: List Built-in Prompt Templates

Run `prompt-list` to view all available prompt presets:

```bash
nestify prompt-list

```

**Output Preview:**

```text
Available embedded prompt templates:
  - architecture
  - default
  - desktop
  - refactor
  - security

```

### Step 2: Inspect a Template's Content

Before injecting a prompt template into `nestify context`, you can preview its exact wording:

```bash
nestify prompt architecture

```

**Output Preview:**

```text
You are an enterprise software architect experienced in Clean Architecture, Onion Architecture, and Event-Driven systems.
Evaluate the directory and module structure of this project.
Verify layer separation (Domain, Application, Infrastructure, Presentation), check for dependency inversion violations, and suggest structural corrections to strictly maintain architectural boundaries.

```

### Step 3: Inject In AI Context Output

Once you know the name of your desired template, pass it to the `context` command using the `-p` flag:

```bash
nestify context -p architecture -d 2

```

---

## 📝 Embedded Prompts Summary

| Template Name | Primary Focus / Use Case |
| --- | --- |
| `architecture` | Evaluates Clean Architecture, Onion Architecture, and layer boundaries. |
| `refactor` | Identifies code smell indicators, monolithic folder coupling, and modularization targets. |
| `security` | Audits folder layouts for sensitive data exposure, config leaks, and unsafe path structures. |
| `desktop` | Analyzes client/desktop application components and UI architecture. |
| `default` | General codebase structure review for high-level AI analysis. |