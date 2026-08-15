# 🚀 Welcome to Nestify Documentation

**Nestify** is a fast, lightweight, cross-platform CLI written in Go.  
It helps you **scan**, **analyze**, **contextualize for AI**, **scaffold**, and **clean-copy** project structures — entirely offline, as a single native binary.

No Node.js. No Python runtime. No telemetry. Your code stays on your machine.

---

## ✨ What you can do

| Goal | Command | Start here |
|------|---------|------------|
| Install the tool | `go install …` | [Installation](getting-started/installation.md) |
| First useful results in minutes | `ignore-use` → `scan` → `context` | [Quick Start](getting-started/quickstart.md) |
| Explore all CLI commands | `nestify --help` | [Commands Overview](commands/overview.md) |
| AI-ready Markdown reports | `nestify context -p …` | [AI Context](ai/overview.md) |
| Ignore & project templates | `ignore-use` / `init` | [Templates](template/overview.md) |
| How the code is organized | — | [Architecture](architecture.md) |

---

## ⚡ 30-second path

```bash
# Install (once)
go install github.com/badboy1981/Nestify/cmd/nestify@latest

# Inside any project
nestify ignore-use go
nestify scan --tree -d 2
nestify context -p architecture -d 2
```

Reports are written under `Nestify-Report/` in the current directory.

---

## 🧭 Documentation map

- **Getting Started** — install and quick start  
- **CLI Commands** — `scan`, `ignore`, `analyze`, `context`, `prompt`, `init`, `copy`  
- **AI & Prompt Engineering** — context reports and embedded prompts  
- **Template & Ignore** — ignore presets, scaffolding JSON, custom templates  
- **Architecture** — internal packages and request flow  

---

## 🔒 Design principles

- **Offline-first** — scanning and report generation never call the network  
- **Single binary** — templates embedded with Go `embed`  
- **Noise-aware** — `.nestifyignore` keeps metrics and AI context token-efficient  
- **Global CLI** — run from any project folder after install  

---

## ➡️ Next step

New here? Start with **[Installation](getting-started/installation.md)**, then **[Quick Start](getting-started/quickstart.md)**.
