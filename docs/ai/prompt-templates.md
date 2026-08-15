# 📋 Embedded Prompt Templates

Nestify includes a set of **prompt engineering templates** embedded in the binary via Go’s `embed` package (`templates-prompts/`).  
They are injected into the top of `ai_context_report.md` when you use `nestify context -p <name>`.

You can also inspect any template in the terminal without generating a full report.

---

## 🧭 Discover & Inspect

```bash
# List all embedded prompt templates
nestify prompt-list

# Print the full instruction text of one template
nestify prompt architecture
```

---

## 📚 Template Catalog

Content below matches the embedded files in `templates-prompts/`.

### `default`

**Use when:** you want a general architectural review.

```text
You are an expert software architect and senior code reviewer.
Analyze the provided project directory structure and metrics report.
Provide a high-level architectural evaluation, highlight potential structural smells or organizational issues, and offer practical suggestions for improvement.
```

```bash
nestify context -p default
```

---

### `architecture`

**Use when:** checking Clean Architecture, Onion Architecture, or Event-Driven layering.

```text
You are an enterprise software architect experienced in Clean Architecture, Onion Architecture, and Event-Driven systems.
Evaluate the directory and module structure of this project.
Verify layer separation (Domain, Application, Infrastructure, Presentation), check for dependency inversion violations, and suggest structural corrections to strictly maintain architectural boundaries.
```

```bash
nestify context -p architecture -d 2
```

---

### `refactor`

**Use when:** looking for coupling, misplaced modules, or a cleaner modular layout.

```text
You are a senior software engineer specializing in code refactoring, clean code, and performance optimization.
Review the attached codebase tree and file distribution metrics.
Identify tightly coupled structures, misplaced modules, or overly complex directory layouts, and suggest a cleaner, refactored layout following modular design principles.
```

```bash
nestify context -p refactor
```

---

### `security`

**Use when:** reviewing structural exposure risks (configs, secrets layout, sensitive paths).

```text
You are a cybersecurity expert and Application Security (AppSec) auditor.
Review the file structure and organizational components of this project.
Identify exposure risks (e.g., exposed configs, unhandled sensitive file extensions, or improper secrets segregation) and provide actionable security recommendations.
```

```bash
nestify context -p security -d 3
```

---

### `desktop`

**Use when:** reviewing desktop / cross-platform application organization.

```text
You are a desktop application specialist (Windows / Linux / macOS cross-platform systems).
Analyze the project tree and code organization.
Check UI/Logic separation, multi-threading or async operational patterns, local resource/asset management, and platform-specific deployment readiness.
```

```bash
nestify context -p desktop
```

---

## ✏️ Custom Prompt Text

If `-p` does not match a template name, Nestify treats the value as **custom instruction text**:

```bash
nestify context -p "Focus on API boundaries and module ownership in this service" -d 2
```

If the value matches an embedded template name, that template is loaded.  
Otherwise the string is used as-is at the top of the report.

---

## 🔄 How Injection Works

1. You run `nestify context -p <template-or-text>`.
2. Nestify resolves the prompt (embedded file or custom string).
3. If a prompt is present, the report starts with:

```markdown
# 💬 AI Task & Instructions

<prompt text here>

---
```

4. Then metrics, language breakdown, and the directory tree follow.

Details of flags and output path: [Context command](../commands/context.md).

---

## ➕ Adding Your Own Templates

Templates are loaded dynamically from `templates-prompts/*.txt`. No code change is required to register a new name.

1. Add a file, for example `templates-prompts/my-audit.txt`, with the instruction text.
2. Reinstall so the file is embedded into the binary:

```bash
go install ./cmd/nestify
```

3. Confirm it appears:

```bash
nestify prompt-list
nestify prompt my-audit
nestify context -p my-audit
```

---

## ➡️ Related Pages

- [AI Context overview](overview.md)
- [Context command](../commands/context.md)
- [Prompt management commands](../commands/prompt.md)
