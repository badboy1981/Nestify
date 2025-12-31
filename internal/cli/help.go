// internal/cli/help.go
package cli

import "fmt"

func ShowHelp() {
	fmt.Println("Nestify - Project Structure Scanner & Generator")
	fmt.Println()
	fmt.Println("Usage:")
	fmt.Println("  nestify <command> [options]")
	fmt.Println()
	fmt.Println("Commands:")
	fmt.Println("  init          Create a new project from a JSON template")
	fmt.Println("  scan          Scan a project and generate structure reports")
	fmt.Println("  analyze       Analyze project skeleton and detect folder roles")
	fmt.Println()
	fmt.Println("Template Management:")
	fmt.Println("  ignore-list   Show available ignore templates (go, nodejs, etc.)")
	fmt.Println("  ignore-use    Apply a specific ignore template to your project")
	fmt.Println()
	fmt.Println("Global Options:")
	fmt.Println("  --help, -h    Show this help message")
	fmt.Println("  --version     Show version information")
	fmt.Println()
	fmt.Println("Examples:")
	fmt.Println("  nestify scan --path ./MyProject --tree")
	fmt.Println("  nestify ignore-list")
	fmt.Println("  nestify ignore-use go")
	fmt.Println("  nestify init --template templates/dotnet.json --path ./NewApp")
	fmt.Println()
}
