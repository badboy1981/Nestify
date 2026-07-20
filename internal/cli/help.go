// internal/cli/help.go
package cli

import "fmt"

func ShowHelp() {
	fmt.Println("Nestify - Fast, Lightweight Project Structure Scanner & AI Context Generator")
	fmt.Println()
	fmt.Println("Usage:")
	fmt.Println("  nestify <command> [options]")
	fmt.Println()
	fmt.Println("Commands:")
	fmt.Println("  context       Generate unified AI-ready context report (Metrics + Languages + Tree)")
	fmt.Println("  analyze       Analyze project metrics and language distributions")
	fmt.Println("  scan          Scan directory structure and save JSON/Markdown reports")
	fmt.Println("  init          Generate physical folder/file structure from JSON template")
	fmt.Println()
	fmt.Println("Ignore Template Management:")
	fmt.Println("  ignore-list   List available embedded ignore templates (go, dotnet, nodejs, etc.)")
	fmt.Println("  ignore-use    Apply an embedded ignore template to current project (.nestifyignore)")
	fmt.Println()
	fmt.Println("Global Options:")
	fmt.Println("  --help, -h    Show this help message")
	fmt.Println("  --version     Show version information")
	fmt.Println()
	fmt.Println("Examples:")
	fmt.Println("  # Step 1: Clean build noise")
	fmt.Println("  nestify ignore-use go")
	fmt.Println()
	fmt.Println("  # Step 2: Generate AI Prompt context")
	fmt.Println("  nestify context")
	fmt.Println()
	fmt.Println("  # Other operations")
	fmt.Println("  nestify scan --path ./MyProject --tree")
	fmt.Println("  nestify scan --path ./FamousRepo --folders-only")
	fmt.Println("  nestify init --template Nestify-Report/Blueprint.json --path ./NewApp")
	fmt.Println()
}
