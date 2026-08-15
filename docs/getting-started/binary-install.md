Installation Guide Nestify can be installed either as a standalone pre-built binary or built directly from source using the Go toolchain. Select your preferred installation method below to view the detailed setup instructions.

=== "Pre-built Binary (binary.md)"# Pre-built Binary Installation

This guide walks you through downloading and configuring pre-compiled binaries for Nestify across supported operating systems.

## Prerequisites

Before installing, ensure your system meets the minimum requirements:

* **Supported Operating Systems:** Linux (amd64, arm64), macOS (Intel, Apple Silicon), Windows (x86_64)

* **Command Line Utilities:** `curl` or `wget` for automated fetching, and `tar` or `unzip` for archive extraction

* **Permissions:** Administrative or `sudo` access if installing into system-wide executable paths (such as `/usr/local/bin`)

## Automated Installation Script

The fastest method to install Nestify is using the automated installer script, which detects your OS and architecture automatically:

```bash
curl -fsSL https://raw.githubusercontent.com/nestify/nestify/main/install.sh | bash
```

!!! tip "Customizing Destination Directory"
    By default, the automated installer targets `/usr/local/bin`. You can override the destination directory by supplying the `BINDIR` environment variable:

```bash
    curl -fsSL https://raw.githubusercontent.com/nestify/nestify/main/install.sh | BINDIR=$HOME/.local/bin bash
```

## Manual Installation

If you prefer to download and verify binaries manually, follow these steps:

1. Navigate to the official releases page and locate the latest release build.

2. Download the package corresponding to your operating system and CPU architecture.

3. Extract the archive contents:


### For Linux / macOS tarballs

```bash
tar -xzf nestify_Linux_x86_64.tar.gz
```

### For Windows zip archives (PowerShell)

```bash
Expand-Archive -Path nestify_Windows_x86_64.zip -DestinationPath C:\nestify
```

4. Move the executable to a directory listed in your system `PATH`:

```bash
sudo mv nestify /usr/local/bin/
```

5. Verify the executable permissions and confirm successful installation:

```bash
nestify version
```

!!! info "Checksum Verification"
    To ensure package integrity, compare the SHA-256 hash of your downloaded file against the published `checksums.txt` document:

```bash
    sha256sum --check checksums.txt 2>&1 | grep OK
```

## Troubleshooting

### Permission Denied Errors

If executing `nestify` returns `Permission denied` (`EACCES`), grant execution rights explicitly:

```bash
chmod +x /usr/local/bin/nestify
```

### macOS Developer Verification Block

macOS Security may block unverified binaries with an "unidentified developer" prompt. You can strip the quarantine attribute using `xattr`:

```bash
xattr -d com.apple.quarantine /usr/local/bin/nestify
```

### Command Not Found

If running `nestify` results in `command not found`, verify that your destination folder exists within your shell's active `$PATH`:

```bash
echo $PATH
```

If necessary, append the installation directory to your profile file (`~/.bashrc` or `~/.zshrc`):

```bash
export PATH="$PATH:/usr/local/bin"
```

=== "Go / Source (go-install.md)"# Go / Source Installation

This document provides instructions for compiling and installing Nestify directly from source code using the Go toolchain.

## Prerequisites

Ensure your local development setup satisfies the following dependencies:
* **Go Toolchain:** Go 1.16 or higher installed (`go version`)

* **Git:** Installed and available on your system path

* **Environment Configuration:** Ensure `$GOPATH/bin` is exported in your environment `PATH`

| Requirement | Minimum Version | Recommended |
|---|---|---|
| Go Compiler | 1.16.0 | 1.22+ |
| Git | 2.20.0 | Latest |
| Storage | 50 MB | 100 MB |
!!! note "Verifying Go Environment Variables"

Confirm your `GOPATH` and `GOBIN` paths before running the module installation commands:

```bash
go env GOPATH GOBIN
```

## Installation via `go install`

To compile and install the executable directly into your `$GOPATH/bin` directory, run:

```bash

go install github.com/badboy1981/Nestify/cmd/nestify@latest

```

To install a specific tagged release version:

```bash

go install github.com/badboy1981/Nestify/cmd/nestify@v1.0.0

```

## Compiling from Source Repository

If you intend to modify the codebase or contribute to development, build the binary directly from the cloned repository:

1. Clone the project repository:

```bash

git clone https://github.com/badboy1981/Nestify.git

cd Nestify

```

2. Download and verify module dependencies:

```bash

go mod download

go mod verify

```

3. Compile the binary using `go install` or `go build`:

```bash

go install ./cmd/nestify

```

4. Verify the installed binary:

```bash

nestify --version

```

!!! warning "CGO Dependencies Notice"

    Nestify is designed to compile with `CGO_ENABLED=0` by default to produce statically linked binaries. If cross-compiling or utilizing CGO features, ensure your local `gcc` or `clang` cross-compiler tools are properly configured.

## Troubleshooting

### Binary Installed but Command Not Found

When `go install` succeeds but running `nestify` fails, your Go binary directory is not included in your active shell path. Add the `GOBIN` path to your shell configuration:

```bash

# For Zsh (~/.zshrc) or Bash (~/.bashrc)

export PATH="$PATH:$(go env GOPATH)/bin"

```

Reload the configuration in your active session:

```bash

source ~/.zshrc

```

### Module Proxy and Download Errors

If network restrictions impede dependency retrieval during installation, set an explicit public Go proxy:

```bash

export GOPROXY=https://proxy.golang.org,direct

go install github.com/badboy1981/Nestify/cmd/nestify@latest

```

### Clearing Build Caches

If you experience build irregularities or lingering artifact issues after upgrading Go versions, clear the build and module cache:

```bash
go clean -cache -modcache
```
