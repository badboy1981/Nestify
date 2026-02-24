# Nestify

**Nestify** is a powerful, lightweight command-line (CLI) tool written in Go that helps developers **scan**, **analyze**, and **create** project folder structures effortlessly.

It works with any type of project: backend (Go, .NET, Node.js), frontend (React, Vue, Angular), mobile (Flutter, MAUI), games (Unity), Python projects, and more.

## Key Features

- **Smart Project Scanning**  
  Generates complete folder/file structure as JSON or readable Markdown.

- **Folders-Only Mode** (`--folders-only`)  
  Shows a clean high-level architecture view without file clutter.

- **`.nestifyignore` Support**  
  Works just like `.gitignore` — exclude unwanted items (e.g., `bin/`, `obj/`, `.vs/`, `node_modules/`, `TestResults/`) for perfectly clean output.

- **Skeleton Analysis** (`analyze`)  
  Automatically identifies the role of each folder (entry point, core code, tests, assets, configuration, etc.).

- **Project Generation from Templates** (`init`)  
  Instantly create folder and file structures from JSON templates.

- **Beautiful Tree View**  
  Displays project structure in an easy-to-read tree format in the terminal.

## Prerequisites

- Go 1.16 or higher
- External package: `github.com/xlab/treeprint`

## Installation

```bash
git clone https://github.com/badboy1981/Nestify.git
cd Nestify
go build -o nestify ./cmd/nestify

# Optional: add to your system PATH
sudo mv nestify /usr/local/bin/   # Linux/macOS
# On Windows, copy to a folder in your PATH
```

Now you can run `nestify` from anywhere.

## Commands

### 1. Scan a Project (`scan`)

```bash
nestify scan --path <project-path> [options]
```

**Useful options:**

- `--tree` → Pretty tree view in terminal + Markdown output (`scan_output.md`)
- `--folders-only` → Show only directories (great for architecture overviews)
- JSON output is always saved to `scan_output.json`

**Example:**
```bash
nestify scan --path ./MyProject --tree --folders-only
```

### 2. Analyze Project Skeleton (`analyze`)

```bash
nestify analyze --path <project-path>
```

Prints an estimated role report for each folder and saves it to `skeleton_report.md`.

### 3. Create Project from Template (`init`)

```bash
nestify init --template <template-json-file> --path <target-path>
```

**Example using a predefined template:**
```bash
nestify init --template templates/dotnet-maui.json --path ./MyNewApp
```

### Reusing Structure from an Existing Project

One of Nestify's most practical features is the ability to **capture the folder structure of any existing project** and reuse it as a template.

**How to do it:**

1. Scan the existing project (use `--folders-only` and a good `.nestifyignore` for the cleanest result):

   ```bash
   nestify scan --path ./ExistingProject --folders-only
   ```

   This creates a clean `scan_output.json`.

2. (Optional) Edit `scan_output.json` to add, remove, or tweak folders/files.

3. Create a new project with the exact same structure:

   ```bash
   nestify init --template ./ExistingProject/scan_output.json --path ./MyNewProject
   ```

**Result:** A fresh project with identical folder layout — ideal for standardizing microservices, modules, or team templates.

> **Pro tip:** Collect your favorite project structures as JSON files in a `templates/` folder for quick reuse!

## Example Output

Here’s a real-world example of a clean scan (`--tree --folders-only`) of a .NET MAUI project with a proper `.nestifyignore`:

```
.
└── ExternalMemoryAI
    ├── ExternalMemoryAI.sln
    ├── docs
    │   ├── Architecture.md
    │   ├── PRD.md
    │   ├── PRDBase.md
    │   └── Roadmap.md
    ├── models
    │   ├── LLaMA
    │   └── MiniLM
    ├── src
    │   ├── AiMemory.Application
    │   │   └── Agents
    │   │       ├── Export
    │   │       ├── Filtering
    │   │       ├── Indexing
    │   │       ├── Ingestion
    │   │       ├── Retrieval
    │   │       └── Versioning
    │   ├── AiMemory.Core
    │   │   ├── Entities
    │   │   ├── Enums
    │   │   ├── Interfaces
    │   │   └── ValueObjects
    │   ├── AiMemory.Infrastructure
    │   │   ├── Persistence
    │   │   └── VectorStores
    │   └── AiMemory.UI
    │       ├── Components
    │       ├── Resources
    │       └── wwwroot
    └── tests
        ├── AiMemory.Application.Tests
        ├── AiMemory.Core.Tests
        ├── AiMemory.Infrastructure.Tests
        ├── AiMemory.Tests.Unit
        ├── AiMemory.UI.Tests
        └── Integration.Tests
```

The corresponding `scan_output.json` contains a structured array ready to be used as a template.

## `.nestifyignore` Example

Place this file in the root of the project you scan to hide noise.

**Example for .NET / MAUI projects:**

```gitignore
# Build & IDE artifacts
bin/
obj/
.vs/
.vscode/
packages/

# Test results
TestResults/
*.trx

# EF Core generated migrations
**/Migrations/*Designer.cs
**/Migrations/*ModelSnapshot.cs

# MAUI platform details
**/Platforms/

# Placeholder files
**/Class1.cs
**/UnitTest1.cs

# Nestify outputs (avoid loops)
scan_output.*
docs/scan_output.*
```

## Contributing

Contributions are welcome!

- Open issues for bugs or ideas
- Submit pull requests
- Add new ready-to-use templates to the `templates/` folder

## License

Nestify is released under the **MIT License** — free to use, modify, and distribute.

---

**Built with ❤️ by [badboy1981](https://github.com/badboy1981)**

> Nestify — Because your project structure deserves clarity, beauty, and order. 🚀