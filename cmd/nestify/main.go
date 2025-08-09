package main

import (
	"encoding/json"
	"flag"
	"fmt"
	"os"

	initcmd "github.com/badboy1981/Nestify/internal/Cli"
	"github.com/badboy1981/Nestify/internal/scanner"
	"github.com/badboy1981/Nestify/internal/treeprinter"
	"github.com/badboy1981/Nestify/internal/types"
)

func main() {
	if len(os.Args) < 2 {
		fmt.Println("❌ Please provide a command (e.g., init, scan)")
		return
	}

	command := os.Args[1]
	args := os.Args[2:]

	switch command {
	case "init":
		initcmd.Run(args)

	case "scan":
		fs := flag.NewFlagSet("scan", flag.ExitOnError)
		pathFlag := fs.String("path", ".", "Path to scan")
		outputFlag := fs.String("output", "structure.json", "Output JSON file")
		printTreeFlag := fs.Bool("print-tree", false, "Print tree structure to console")
		fs.Parse(args)

		rootNodes, err := scanner.ScanDirectory(*pathFlag)
		if err != nil {
			fmt.Println("❌ Error scanning directory:", err)
			return
		}

		data, err := json.MarshalIndent(rootNodes, "", "  ")
		if err != nil {
			fmt.Println("❌ Error encoding JSON:", err)
			return
		}

		if err := os.WriteFile(*outputFlag, data, 0644); err != nil {
			fmt.Println("❌ Error writing file:", err)
			return
		}

		fmt.Printf("✅ Scan complete! Output saved to %s\n", *outputFlag)

		if *printTreeFlag {
			root := &types.Node{
				Name:     "root",
				Type:     "folder",
				Children: rootNodes,
			}
			treeprinter.PrintTree(root)
		}

	default:
		fmt.Printf("❌ Unknown command '%s'\n", command)
	}
}
