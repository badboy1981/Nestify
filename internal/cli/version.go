// internal/cli/version.go
package cli

import "fmt"

const (
	AppName    = "Nestify"
	Version    = "v1.0.0"
	BuildDate  = "2025-12-31"
	Author     = "badboy1981"
	Repository = "https://github.com/badboy1981/Nestify"
)

func ShowVersion() {
	fmt.Printf("%s %s\n", AppName, Version)
	fmt.Printf("Build Date: %s\n", BuildDate)
	fmt.Printf("Author: %s\n", Author)
	fmt.Printf("Repository: %s\n", Repository)
	fmt.Println("\nA powerful project structure scanner, analyzer, and generator written in Go.")
	fmt.Println("Use 'nestify --help' for usage information.")
}
