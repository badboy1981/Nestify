package Cli

// File: scan.go

import (
	"encoding/json"
	"fmt"
	"os"

	"github.com/badboy1981/Nestify/internal/scanner"
	"github.com/badboy1981/Nestify/internal/treeprinter"
)

func runScan(path string, printTree bool, foldersOnly bool) {
	rootNodes, err := scanner.Scan(path, foldersOnly)
	if err != nil {
		fmt.Println("❌ Failed to scan path:", err)
		return
	}

	outputFile := "scan_output.json"
	file, err := os.Create(outputFile)
	if err != nil {
		fmt.Println("❌ Failed to create output file:", err)
		return
	}
	defer file.Close()

	encoder := json.NewEncoder(file)
	encoder.SetIndent("", "  ")
	if err := encoder.Encode(rootNodes); err != nil {
		fmt.Println("❌ Failed to write JSON:", err)
		return
	}

	fmt.Println("✅ The scan output has been saved in", outputFile)

	if printTree {
		// ساخت رشته کامل درخت برای همه Root ها
		treeStr := ""
		for _, root := range rootNodes {
			treeStr += treeprinter.GetTreeString(&root) + "\n"
		}

		// چاپ در ترمینال
		fmt.Print(treeStr)

		// ذخیره در فایل Markdown
		mdOutput := "```\n" + treeStr + "```\n"
		err = os.WriteFile("scan_output.md", []byte(mdOutput), 0644)
		if err != nil {
			fmt.Println("❌ Failed to save Markdown:", err)
		} else {
			fmt.Println("✅ Markdown output saved in scan_output.md.")
		}
	}
}
