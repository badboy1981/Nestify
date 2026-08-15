# 📋 Clean Project Copy (`nestify copy`)

The `copy` command creates a **clean physical copy** of the current project into a new destination path.  
It walks the source directory, respects `.nestifyignore` rules, and streams every non-ignored file and folder to the target — including text and binary files.

Unlike `init` (which scaffolds an **empty** structure from JSON), `copy` transfers **real file contents**.

---

## 🛠️ Usage & Options

```bash
nestify copy --path <destination>
```

### Flags & Options

| Flag | Short | Description | Default |
| --- | --- | --- | --- |
| `--path` |  | Destination directory for the clean copy. **Required.** | — |

!!! info "Source is always the current directory"
    Nestify runs from the folder you are in. Open a terminal inside the project you want to clone, then point `--path` at the destination. There is no separate source flag.

!!! info "Cross-Platform & Shell Agnostic"
    Nestify is compiled into a single native binary with zero external dependencies. All CLI commands run identically across **Windows (PowerShell / CMD)**, **macOS**, and **Linux (Bash / Zsh)**.

---

## 💡 Recommended Workflow

1. Open a terminal **inside** the source project.
2. Apply an ignore template (and customize `.nestifyignore` if needed):

```bash
nestify ignore-list
nestify ignore-use go
```

3. Optionally inspect what will remain after filtering:

```bash
nestify scan --tree -d 2
```

4. Copy the clean project to a new location:

```bash
nestify copy --path ../clean-project
```

---

## 💡 Examples

### 1. Basic clean copy

```bash
# Inside the project you like
nestify ignore-use nodejs
nestify copy --path ../my-clean-app
```

### 2. After customizing ignore rules

```bash
nestify ignore-use go
# Edit .nestifyignore to add extra patterns if needed
nestify copy --path ./sandbox/clean-clone
```

---

## ⚙️ How Copy Works

1. **Source** = current working directory.
2. **Destination** = value of `--path` (must not be empty or `.`).
3. **Filter** = `IgnoreMatcher` loaded from `.nestifyignore` in the source (plus the same built-in defaults used by `scan`).
4. **Transfer** = recursive walk; directories are created with `MkdirAll`, files are streamed with `io.Copy` (text and binary).
5. **Safety checks**:
   - Source and destination must not be the same path.
   - Destination must not sit inside the source tree (avoids recursive copy).

---

## 📊 Output

On success the CLI prints a short summary:

```text
📂 Source : /path/to/source
📂 Target : /path/to/destination
⏳ Copying (respecting .nestifyignore)...

✅ Clean project copied successfully!
   📁 Dirs  : 12
   📄 Files : 48
   📍 Path  : /path/to/destination
```

---

## 🔄 Difference from `init`

| | `nestify copy` | `nestify init` |
|---|---|---|
| **Input** | Live project on disk | JSON blueprint |
| **Output** | Real files + folders (content included) | Empty scaffold structure |
| **Filter** | `.nestifyignore` | N/A (template defines structure) |
| **Typical use** | Clean clone of an existing repo | Bootstrap from template / scan JSON |

---

## ⚠️ Notes

- `--path` is **required**. Running `nestify copy` without it shows an error and a usage example.
- Filtering decisions come only from `.nestifyignore` (and the matcher defaults already used elsewhere in Nestify). The command does not invent extra ignore rules.
- Existing files in the destination may be overwritten when paths collide; choose an empty or new target folder when possible.
