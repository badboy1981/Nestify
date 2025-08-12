package generator

// File: generator.go

import (
	"fmt"
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/types"
)

func CreateStructure(node types.Node, root string) error {
	path := filepath.Join(root, node.Name)

	if node.Type == "folder" {
		if err := os.MkdirAll(path, os.ModePerm); err != nil {
			return fmt.Errorf("خطا در ایجاد پوشه %s: %v", path, err)
		}
		for _, child := range node.Children {
			if err := CreateStructure(child, path); err != nil {
				return err
			}
		}
	} else if node.Type == "file" {
		f, err := os.Create(path)
		if err != nil {
			return fmt.Errorf("خطا در ایجاد فایل %s: %v", path, err)
		}
		defer f.Close()
	}

	return nil
}