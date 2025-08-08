package main

import (
	"fmt"
	"os"

	cli "github.com/badboy1981/Nestify/internal/Cli"
)

func main() {
	if len(os.Args) < 2 {
		fmt.Println("❌ Please provide a command (e.g., init)")
		return
	}

	command := os.Args[1]
	args := os.Args[2:]

	switch command {
	case "init":
		cli.Run(args)
	default:
		fmt.Printf("❌ Unknown command '%s'\n", command)
	}
}
