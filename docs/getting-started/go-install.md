# **Nestify Go Installation Guide**

Comprehensive setup, dependency configuration, and deployment instructions for Nestify.

# **Overview**

Nestify is built using Go (Golang) to leverage high-concurrency execution, cross-platform portability, and low resource overhead. This guide walks you through preparing your environment, installing the Go runtime, configuring system path variables, building Nestify from source or binary, and resolving common installation issues.

# **System Prerequisites**

Ensure your host system meets or exceeds the minimum operating hardware and software requirements listed below before proceeding.

| Component | Minimum Requirement | Recommended Specification |
| :---- | :---- | :---- |
| Go Version | Go 1.21.0 or higher | Go 1.22.x or latest stable release |
| Architecture | amd64 (x86\_64) or arm64 (Apple Silicon / AArch64) | amd64 or arm64 |
| Memory (RAM) | 512 MB | 2 GB+ |
| Storage | 250 MB free disk space | 1 GB+ free disk space |

# **Step 1: Installing the Go Runtime Environment**

If Go is not already installed on your system, follow the platform-specific instructions below to install the official Go toolchain.

## **macOS Installation**

You can install Go on macOS using either Homebrew or the official installer package:

* **Option A: Using Homebrew (Recommended)**  
  Execute the following command in your terminal:  
  `brew install go`  
* **Option B: Official Installer Package**  
  Download the appropriate `.pkg` installer from the official Go download portal (go.dev) and run the setup wizard.

## **Linux Installation**

Download and extract the official Go binary tarball into system folders:

* **Download Archive:**  
  `wget https://go.dev/dl/go1.22.5.linux-amd64.tar.gz`  
* **Extract Files:**  
  `sudo tar -C /usr/local -xzf go1.22.5.linux-amd64.tar.gz`  
* **Configure User Path:**  
  Append `export PATH=$PATH:/usr/local/go/bin` to your `~/.bashrc`, `~/.zshrc`, or profile configuration file.

## **Windows Installation**

* Download the MSI installer package (`go1.22.x.windows-amd64.msi`) from the official Go website.  
* Open the downloaded file and follow the GUI installation prompts.  
* The installer automatically registers Go binary paths into your system `PATH` environment variable.

# **Step 2: Path Environment Variable Setup**

To enable global command execution for Go binaries installed via `go install`, append your local `GOPATH` binary folder to your system environment.

## **Shell Path Configuration Matrix**

| Shell / Operating System | Profile Configuration File | Command to Append |  
| Bash (Linux/macOS) | `~/.bashrc` or `~/.bash_profile` | `export PATH=$PATH:$(go env GOPATH)/bin` |  
| Zsh (macOS/Linux) | `~/.zshrc` | `export PATH=$PATH:$(go env GOPATH)/bin` |  
| PowerShell (Windows) | `$PROFILE` | `$env:Path += ";$(go env GOPATH)\bin"` |

Apply profile updates immediately by restarting your shell or running `source ~/.zshrc` (or `source ~/.bashrc`).

## **Verifying Go Installation**

To verify that Go is correctly installed and accessible in your command prompt, execute:

go versionExpected console output should resemble:

go version go1.22.5 darwin/arm64

# **Step 3: Installing Nestify**

You can install Nestify using either the Go binary installer or by compiling directly from source code.

## **Option A: Install via Go CLI (Recommended)**

Run the command below to automatically download, compile, and place the executable into your `GOPATH/bin` directory:

go install github.com/nestify/nestify/cmd/nestify@latest

## **Option B: Building from Source Repository**

For development setups or customized compilation:

* **Clone the Repository:**  
  `git clone https://github.com/nestify/nestify.git`  
* **Navigate to Project Root:**  
  `cd nestify`  
* **Fetch Dependencies:**  
  `go mod download`  
* **Compile Execution Binary:**  
  `go build -o nestify ./cmd/nestify`  
* **Move Binary to Global Executable Path (Optional):**  
  `sudo mv nestify /usr/local/bin/`

# **Step 4: Installation Verification**

Confirm that Nestify is correctly installed and executable:

`nestify --version`To inspect supported command flags, configuration parameters, and subcommands, display the general help menu:

`nestify --help`

# **Troubleshooting & Support**

## **Issue: "command not found: nestify"**

* **Cause:** The binary output path `$(go env GOPATH)/bin` is missing from your system `PATH` environment variable.  
* **Solution:** Re-check Step 2 and ensure your shell profile exports `$(go env GOPATH)/bin`.

## **Issue: Module Dependency / Checksum Mismatch**

* **Cause:** Corrupted local Go module cache.  
* **Solution:** Clear the cache and re-download module dependencies:  
  `go clean -modcache`  
  `go mod tidy`

## **Issue: Permission Denied During Installation**

* **Cause:** Insufficient write permissions for standard system binary directories like `/usr/local/bin`.  
* **Solution:** Execute moving steps with `sudo` or modify target destination to user-owned binary paths such as `~/.local/bin` or `$(go env GOPATH)/bin`.

## **Documentation & Maintenance Contact**

For additional assistance, bug reports, or enterprise support regarding Nestify releases, contact Person or check repository maintenance updates from Date.