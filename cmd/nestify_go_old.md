package main

import (
	"fmt"
	"os"

	"github.com/badboy1981/Nestify/internal/initcmd"
)

func main() {
	if len(os.Args) < 2 {
		fmt.Println("❌ Please provide a command (e.g., init)")
		return
	}

	command := os.Args[1]
	args := os.Args[2:] // pass remaining args to subcommand

	switch command {
	case "init":
		initcmd.Run(args)
	default:
		fmt.Printf("❌ Unknown command '%s'\n", command)
	}
}
