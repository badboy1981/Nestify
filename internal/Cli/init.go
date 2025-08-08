package cli

import (
	"encoding/json"
	"flag"
	"fmt"
	"os"

	"github.com/badboy1981/Nestify/internal/generator"
	"github.com/badboy1981/Nestify/internal/types"
)

func Run(args []string) {
	fs := flag.NewFlagSet("init", flag.ExitOnError)
	pathFlag := fs.String("path", ".", "Output path for folder structure")
	jsonFile := fs.String("template", "config/structure.json", "Template JSON file")
	fs.Parse(args)

	data, err := os.ReadFile(*jsonFile)
	if err != nil {
		fmt.Println("❌ Failed to read template file:", err)
		return
	}

	var template types.Template
	if err := json.Unmarshal(data, &template); err != nil {
		fmt.Println("X Failed to parse JSON: ", err)
		return
	}

	rootNode := template.Root

	if err := generator.CreateStructure(rootNode, *pathFlag); err != nil {
		fmt.Println("X Error creating structure:", err)
		return
	}

	fmt.Println("✅ Folder structure created at", *pathFlag)
}
