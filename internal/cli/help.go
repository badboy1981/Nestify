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
	fmt.Println("Common Flags:")
	fmt.Println("  --depth, -d   Limit directory traversal depth (e.g. -d 2)")
	fmt.Println("  --tree        Generate ASCII tree Markdown report (scan command)")
	fmt.Println("  --folders-only Include directories only")
	fmt.Println("  --help, -h    Show this help message")
	fmt.Println("  --version     Show version information")
	fmt.Println()
	fmt.Println("Examples:")
	fmt.Println("  nestify scan -d 2 --tree")
	fmt.Println("  nestify context -d 1")
	fmt.Println("  nestify analyze --path ./MyProject -d 3")
	fmt.Println()
}
