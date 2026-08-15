# 🚫 Ignore Templates (`.nestifyignore`)

Ignore templates are plain-text rule lists embedded under `templates-ignore/`.  
`nestify ignore-use <name>` copies one of them to a **local** `.nestifyignore` in your current directory.

That file is then used by `scan`, `analyze`, `context`, and `copy` to skip build noise and dependency trees.

---

## 🛠️ Commands

```bash
# List every embedded ignore template
nestify ignore-list

# Apply a template (creates/overwrites ./.nestifyignore)
nestify ignore-use go
```

CLI details: [Ignore Management](../commands/ignore.md).

---

## 📚 Available templates

Names match the embedded `templates-ignore/*.txt` files (without the extension):

| Template | Typical use |
|----------|-------------|
| `general` | Generic OS / editor / VCS clutter |
| `go` | Go modules, binaries, coverage |
| `nodejs` | `node_modules`, npm/yarn caches |
| `python` | `__pycache__`, venv, dist |
| `dotnet` | `bin/`, `obj/`, Visual Studio |
| `java` | Maven/Gradle build outputs |
| `kotlin` | Kotlin / Android build noise |
| `react` | React / front-end build folders |
| `vue` | Vue CLI / Vite outputs |
| `angular` | Angular CLI caches and dist |
| `flutter` | Flutter / Dart build artifacts |
| `unity` | Unity Library, Temp, Builds |
| `rust` | `target/` and Cargo artifacts |
| `ruby` | Bundler / Rails temp paths |
| `swift` | Xcode / SPM derived data |
| `php-laravel` | Laravel vendor and caches |
| `docker` | Docker-related local clutter |
| `terraform` | Terraform state and providers |

Run `nestify ignore-list` on your installed binary for the authoritative list.

---

## ⚙️ How matching works

When a scan-style command runs, Nestify builds an `IgnoreMatcher` that:

1. Reads `.nestifyignore` from the **current working directory**
2. If `--path` points elsewhere, also reads that path’s `.nestifyignore` when present
3. Adds built-in defaults already used by the matcher (including `.git` and `node_modules`)

Patterns are matched against **base names** and **relative paths** (similar to simple gitignore-style rules).

---

## 📝 Customize after apply

`.nestifyignore` is a normal text file. After `ignore-use`, edit it freely:

```gitignore
# Extra project-specific rules
/data/local_cache/
*.tmp_log
Nestify-Report/
```

Many templates already include commented optional blocks (for example Nestify self-ignore). Uncomment lines by removing the leading `#`.

---

## 🔄 Typical workflow

```bash
# Inside your project
nestify ignore-use nodejs
# Edit .nestifyignore if needed
nestify scan --tree -d 2
nestify context -p architecture -d 2
# Optional: clean physical copy
nestify copy --path ../clean-app
```

!!! warning "No ignore file"
    If there is no `.nestifyignore`, traversal is largely unfiltered (aside from matcher defaults). Always apply a template before serious AI context or copy work.

---

## ➕ Add a custom ignore template to the binary

1. Create `templates-ignore/my-stack.txt` with one pattern per line.
2. Reinstall:

```bash
go install ./cmd/nestify
```

3. Verify:

```bash
nestify ignore-list
nestify ignore-use my-stack
```

No Go code changes are required; templates are discovered dynamically from the embedded FS.

---

## ➡️ Related

- [Templates overview](index.md)
- [Custom Scaffolding](custom-scaffolding.md)
- [Ignore commands](../commands/ignore.md)
- [Clean Copy](../commands/copy.md)
