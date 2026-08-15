package cli

import (
	"encoding/json"
	"flag"
	"fmt"
	"os"

	"github.com/badboy1981/Nestify/internal/generator"
	"github.com/badboy1981/Nestify/internal/pathutil"
	"github.com/badboy1981/Nestify/internal/types"
)

func runInitCmd() {
	cmd := flag.NewFlagSet("init", flag.ExitOnError)
	template := cmd.String("template", "template.json", "JSON project structure template file")
	path := cmd.String("path", ".", "Destination path for the generated structure")

	cmd.Parse(os.Args[2:])

	cleanTemplatePath := pathutil.NormalizeForOS(*template)
	cleanDestPath := pathutil.NormalizeForOS(*path)

	runInit(cleanTemplatePath, cleanDestPath)
}

func runInit(templateFile string, path string) {
	// Read the template file.
	data, err := os.ReadFile(templateFile)
	if err != nil {
		fmt.Printf("❌ Failed to read template file at: %s\n", templateFile)
		return
	}

	var template types.Template
	if err := json.Unmarshal(data, &template); err != nil {
		fmt.Println("❌ Failed to parse JSON:", err)
		return
	}

	// Create the destination root directory before generating files.
	if err := os.MkdirAll(path, 0755); err != nil {
		fmt.Printf("❌ Failed to access or create destination path: %s\n", path)
		return
	}

	for _, rootNode := range template.Root {
		// Pass the absolute path to the generator.
		err = generator.CreateStructure(rootNode, path)
		if err != nil {
			fmt.Printf("❌ Failed to create structure: %v\n", err)
			return
		}
	}

	fmt.Printf("✅ Project created successfully at:\n   %s\n", path)
}
