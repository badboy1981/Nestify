# ⚙️ Installation

**Nestify** is a fast, lightweight CLI tool compiled into a single native binary.  
It has **zero external runtime dependencies** — once installed, it runs completely offline.  
All ignore templates, project templates, and prompt templates are embedded directly into the binary via Go’s `embed` package.

You only need **Go** installed on your system (for the recommended method).

---

## 📋 Prerequisites

| Requirement | Notes |
|-------------|-------|
| **Go** | Required for Method 1. Project is built with Go 1.24 (compatible with Go 1.16+). |
| **Git** | Required only if you prefer to clone the repository. |
| **Supported platforms** | Windows, macOS, Linux (amd64 / arm64) |

!!! info "Zero External Runtimes"
    After installation Nestify is a standalone executable. No Node.js, Python, virtual environments, or extra daemons are needed.

---

## 🚀 Method 1 — Install with Go (Recommended)

This is the most common and familiar way for developers who already have Go installed.

### Option A: One-line install (fastest)

```bash
go install github.com/badboy1981/Nestify/cmd/nestify@latest
```

### Option B: Clone and install (useful for development)

```bash
git clone https://github.com/badboy1981/Nestify.git
cd Nestify
go install ./cmd/nestify
```

Both commands compile the binary (including all embedded templates) and place it in your Go binary directory (`$(go env GOPATH)/bin`).

---

## 📦 Method 2 — Pre-built Binary (GitHub Releases)

If you prefer not to use the Go toolchain, download a ready-made binary from the official releases page:

1. Go to the [Releases](https://github.com/badboy1981/Nestify/releases) page.
2. Download the archive that matches your operating system and architecture.
3. Extract the archive.
4. Move the `nestify` (or `nestify.exe`) executable to a directory that is already in your `PATH`  
   (examples: `/usr/local/bin`, `~/.local/bin`, or any custom folder you prefer).
5. Make sure the file is executable (Linux / macOS):

```bash
chmod +x nestify
```

---

## ✅ Verify the Installation

Run the version command from any directory:

```bash
nestify --version
```

or

```bash
nestify version
```

**Expected output:**

```
Nestify v1.0.0
Build Date: 2025-12-31
Author: badboy1981
Repository: https://github.com/badboy1981/Nestify

A powerful project structure scanner, analyzer, and generator written in Go.
Use 'nestify --help' for usage information.
```

If you see this output, Nestify is ready to use.

---

## 🔧 Troubleshooting PATH Issues

If the terminal says `command not found` or `'nestify' is not recognized`, the directory containing the binary is missing from your `PATH`.

### Default Go binary locations

| Operating System | Default location |
|------------------|------------------|
| Windows | `%USERPROFILE%\go\bin` |
| macOS / Linux | `$HOME/go/bin` |

You can always check the exact path with:

```bash
go env GOPATH
```

### Windows (PowerShell)

```powershell
[Environment]::SetEnvironmentVariable("Path", $env:Path + ";$env:USERPROFILE\go\bin", "User")
```

Restart the terminal after running the command.

### macOS / Linux (Zsh or Bash)

```bash
# For Zsh (default on modern macOS)
echo 'export PATH=$PATH:$(go env GOPATH)/bin' >> ~/.zshrc
source ~/.zshrc

# For Bash
echo 'export PATH=$PATH:$(go env GOPATH)/bin' >> ~/.bashrc
source ~/.bashrc
```

After updating the PATH, run `nestify --version` again to confirm.

---

## ➡️ Next Step

Once Nestify is installed, continue to the [Quick Start](quickstart.md) guide to run your first commands.
