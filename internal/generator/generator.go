package generator

import (
	"fmt"
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/types"
	// "golang.org/x/text/cases"
)

func CreateStructure(node types.Node, root string) error {
	cleanRoot := filepath.Clean(root)
	path := filepath.Join(cleanRoot, node.Name)

	switch node.Type {
	case "folder":
		if err := os.MkdirAll(path, os.ModePerm); err != nil {
			return err
		}
		for _, child := range node.Children {
			if err := CreateStructure(child, path); err != nil {
				return err
			}
		}
	case "file":
		if err := os.WriteFile(path, []byte(node.Content), 0644); err != nil {
			return err
		}
	default:
		// return fmt.Errorf("unknown node type: %s", node.Type, node.Name)
		return fmt.Errorf("unknown node type: '%s' in node '%s'", node.Type, node.Name)
	}

	// if node.Type == "folder" {
	// 	if err := os.MkdirAll(path, os.ModePerm); err != nil {
	// 		return err
	// 	}
	// 	for _, child := range node.Children {
	// 		if err := CreateStructure(child, path); err != nil {
	// 			return err
	// 		}
	// 	}
	// } else if node.Type == "file" {
	// 	if err := os.WriteFile(path, []byte(node.Content), 0644); err != nil {
	// 		return err
	// 	}
	// } else {
	// 	// return errors.New("unknown node type: " + node.Type)
	// 	return fmt.Errorf("unknown node type: '%s' in node '%s'", node.Type, node.Name)
	// }

	return nil
}
