package generator

import (
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/types"
)

// CreateStructure recursively creates folders and files from a node tree.
func CreateStructure(node types.Node, root string) error {
	path := filepath.Join(root, node.Name)

	if node.Type == "folder" {
		// Create directory.
		if err := os.MkdirAll(path, 0755); err != nil {
			return err
		}
		// Create children.
		for _, child := range node.Children {
			if err := CreateStructure(child, path); err != nil {
				return err
			}
		}
	} else {
		// Create file with content.
		// If node.Content is empty, an empty file is written.
		data := []byte(node.Content)
		if err := os.WriteFile(path, data, 0644); err != nil {
			return err
		}
	}
	return nil
}
