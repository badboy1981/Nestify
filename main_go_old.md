package main

import (
	"encoding/json"
	"fmt"
	"os"
	"path/filepath"
)

func createFolders(structure map[string]interface{}, root string) {
	for name, content := range structure {
		path := filepath.Join(root, name)
		err := os.MkdirAll(path, os.ModePerm)
		if err != nil {
			fmt.Printf("❌ Error creating folder %s: %v\n", path, err)
			continue
		}
		if sub, ok := content.(map[string]interface{}); ok {
			createFolders(sub, path)
		}
	}
}

func main() {
	file, err := os.ReadFile("config/structure.json")
	if err != nil {
		fmt.Println("❌ Failed to read structure.json:", err)
		return
	}

	var structure map[string]interface{}
	err = json.Unmarshal(file, &structure)
	if err != nil {
		fmt.Println("❌ Failed to parse JSON:", err)
		return
	}

	createFolders(structure, ".")
	fmt.Println("✅ Folder structure created successfully.")
}
