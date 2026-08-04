# 🚫 Ignore Management (`ignore-list` & `ignore-use`)

The **Ignore System** is the cornerstone of Nestify's noise-reduction engine. Before generating architecture reports or sending context to Large Language Models (LLMs), filtering out build artifacts, compiled binaries, dependency directories, and temporary OS files is critical to preserve token efficiency and report clarity.

Nestify provides embedded, zero-configuration ignore templates for 18+ major tech stacks.

---

## 🛠️ Commands Reference

Nestify offers two primary commands to inspect and apply ignore templates:

### 1. List Available Templates (`ignore-list`)
Displays all tech-stack ignore templates built directly into the Nestify binary.

```bash
nestify ignore-list
```

### 2. Apply a Template (`ignore-use`)

Copies the specified embedded template into a local `.nestifyignore` file in your current working directory.

```bash
nestify ignore-use <template-name>
```

!!! info "Cross-Platform & Shell Agnostic"
     Nestify is compiled into a single native binary with zero external dependencies. All CLI commands run identically across **Windows (PowerShell / CMD)**, **macOS**, and **Linux (Bash / Zsh)**. Learn more about [Shell Agnostic Execution](https://www.google.com/search?q=../getting-started/quickstart.md%23cross-platform-execution).

---

## 💡 Usage Workflow & Examples

### Step 1: Discover Embedded Tech Stacks

Run `ignore-list` to see available technology presets (e.g., `go`, `dotnet`, `nodejs`, `python`, `flutter`, `unity`, etc.):

```bash
nestify ignore-list

```

**Output Preview:**

```text
Available embedded ignore templates:
  - angular
  - docker
  - dotnet
  - flutter
  - general
  - go
  - java
  - kotlin
  - nodejs
  - php-laravel
  - python
  - react
  - ruby
  - rust
  - swift
  - terraform
  - unity
  - vue

```

### Step 2: Apply Template to Current Project

Select your tech stack to generate a local `.nestifyignore` file:

```bash
nestify ignore-use go

```

*(Replace `go` with `dotnet`, `nodejs`, `python`, `flutter`, or any supported template name).*

---

## ⚙️ How `.nestifyignore` Works

When any scanning command (`scan`, `analyze`, or `context`) is executed, Nestify looks for a `.nestifyignore` file in the project root. It parses gitignore-style glob patterns to exclude matching paths during directory traversal.

### Example `.nestifyignore` File Content (`dotnet` template):

```gitignore
# Build outputs
bin/
obj/
out/

# Dependencies & IDEs
.vs/
.vscode/
*.user
*.suo

# Test results
TestResults/

```

---

## 📝 Customizing Local Ignore Rules

The created `.nestifyignore` file is a plain text file. You can open it in any text editor and append project-specific custom rules:

```gitignore
# Append custom internal logs or data folders
/data/local_cache/
*.tmp_log
```

??? note "Default Scanning Behavior"
    If no `.nestifyignore` file is present in your target directory, Nestify performs a **full, unfiltered traversal** and includes all files and folders in the generated report. To ignore build artifacts or system clutter, ensure you generate a `.nestifyignore` file using `nestify ignore-use <template>`.