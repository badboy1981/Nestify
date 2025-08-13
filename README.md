# Nestify

Nestify is a command-line interface (CLI) tool that helps developers scan, analyze, and create project folder structures. Written in Go, it is designed for various project types (e.g., web, Unity games, or backend). With Nestify, you can visualize project structures as JSON or Markdown, analyze the project skeleton, or create new structures using predefined templates.

## Features
- **Project Scanning**: Scans folders and files, outputting the structure as JSON or Markdown.
- **Folders-Only Scanning**: Scan only directories to get a high-level project overview.
- **Skeleton Analysis**: Automatically detects the role of each folder (e.g., core code, tests, or resources).
- **Structure Creation**: Creates folder/file structures based on JSON templates.

## Prerequisites
- Go version 1.16 or higher
- External package: `github.com/xlab/treeprint`

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/badboy1981/Nestify.git
   ```
2. Navigate to the project directory:
   ```bash
   cd Nestify
   ```
3. Build the project:
   ```bash
   go build -o nestify ./cmd/nestify
   ```
4. Optionally, move the `nestify` executable to your PATH:
   ```bash
   mv nestify /usr/local/bin/
   ```

## CLI Commands
Nestify supports three main subcommands: `init`, `scan`, and `analyze`.

### 1. `init` - Create Project Structure
Creates a folder/file structure in the specified path using a JSON template.

**Usage:**
```bash
./nestify init --template <template-file> --path <project-path>
```

**Example:**
```bash
./nestify init --template config/structure.json --path ./myproject
```
This command creates the structure defined in `structure.json` in the `myproject` folder.

**Options:**
- `--template`: Path to the JSON template file (default: `template.json`)
- `--path`: Target path for creating the structure (default: `.`)

### 2. `scan` - Scan Project Structure
Scans the project’s folders and files, saving the structure as JSON. Optionally displays a tree view in the terminal or as Markdown.

**Usage:**
```bash
./nestify scan --path <project-path> [--tree] [--folders-only]
```

**Example:**
```bash
./nestify scan --path . --tree --folders-only
```
This command scans only the folders in the current project, displays a tree structure in the terminal, and saves it to `scan_output.md`.

**Options:**
- `--path`: Path to the project to scan (default: `.`)
- `--tree`: Display tree structure and save to `scan_output.md`
- `--folders-only`: Scan only folders

**Output:**
- `scan_output.json`: Full structure as JSON
- `scan_output.md`: Tree structure (if `--tree` is enabled)

### 3. `analyze` - Analyze Project Skeleton
Analyzes the project’s folder structure and identifies the role of each folder (e.g., "entry point" or "configuration").

**Usage:**
```bash
./nestify analyze --path <project-path>
```

**Example:**
```bash
./nestify analyze --path .
```
This command analyzes the project skeleton, displays the report in the terminal, and saves it to `skeleton_report.md`.

**Options:**
- `--path`: Path to the project to analyze (default: `.`)

**Output:**
- `skeleton_report.md`: Skeleton report with estimated folder roles

## Project Structure
```
Nestify
├── .gitattributes
├── .gitignore
├── LICENSE
├── NestifyDiagram.json
├── README.md
├── cmd
│   └── nestify
│       └── main.go
├── config
│   └── structure.json
├── go.mod
├── go.sum
├── internal
│   ├── Cli
│   │   ├── cli.go
│   │   ├── init.go
│   │   └── scan.go
│   ├── analyzer
│   │   └── analyzer.go
│   ├── generator
│   │   └── generator.go
│   ├── scanner
│   │   └── scanner.go
│   ├── treeprinter
│   │   └── treeprinter.go
│   ├── types
│   │   └── type.go
├── nestify
├── scan_output.json
├── scan_output.md
└── skeleton_report.md
```

## Example Template File (structure.json)
```json
{
  "projectType": "web",
  "language": "go",
  "tags": ["backend", "api"],
  "root": [
    {
      "name": "src",
      "type": "folder",
      "children": [
        {
          "name": "main.go",
          "type": "file"
        }
      ]
    }
  ]
}
```

## Developer
- Developed by: [badboy1981](https://github.com/badboy1981)

## Contributing
If you have ideas for improvements, please open an issue or submit a pull request!

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.