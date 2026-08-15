# 📦 Templates & Ignore

Nestify ships with three families of **embedded templates** (via Go’s `embed` package). They require zero external downloads and are available as soon as the binary is installed.

| Family | Location in source | Purpose |
|--------|--------------------|---------|
| **Ignore** | `templates-ignore/*.txt` | Tech-stack noise filters → local `.nestifyignore` |
| **Project** | `templates-projects/*.json` | JSON blueprints for `nestify init` scaffolding |
| **Prompt** | `templates-prompts/*.txt` | LLM instruction blocks for `nestify context -p` |

---

## 🚫 Ignore templates

Apply a preset, then customize the generated `.nestifyignore` file.

```bash
nestify ignore-list
nestify ignore-use go
```

→ Full catalog: [Ignore Templates](ignore-rules.md)

---

## 🏗️ Project scaffolding templates

Bootstrap a physical folder/file tree from an embedded or local JSON blueprint.

```bash
nestify init --template templates-projects/go_standard.json --path ./MyApp
```

→ Details: [Custom Scaffolding](custom-scaffolding.md)

---

## 💬 Prompt templates

Inject architecture, security, or refactor instructions into AI context reports.

```bash
nestify prompt-list
nestify context -p architecture -d 2
```

→ Catalog: [AI Prompt Templates](../ai/prompt-templates.md)

---

## 🔗 Related commands

| Command | Doc |
|---------|-----|
| `ignore-list` / `ignore-use` | [Ignore Management](../commands/ignore.md) |
| `init` | [Project Generation](../commands/init.md) |
| `prompt-list` / `prompt` | [Prompt Management](../commands/prompt.md) |
| `context` | [AI Context](../commands/context.md) |
| `copy` | [Clean Copy](../commands/copy.md) |
