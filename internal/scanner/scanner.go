package scanner

import (
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/types"
)

func ScanDirectory(rootPath string) ([]types.Node, error) {
	var result []types.Node

	entries, err := os.ReadDir(rootPath)
	if err != nil {
		return nil, err
	}

	for _, entry := range entries {
		node := types.Node{
			Name: entry.Name(),
		}

		if entry.IsDir() {
			node.Type = "folder"
			children, err := ScanDirectory(filepath.Join(rootPath, entry.Name()))
			if err != nil {
				return nil, err
			}
			node.Children = children
		} else {
			node.Type = "file"
			info, _ := entry.Info()
			node.Size = info.Size() // اگر Size توی struct هست
		}

		result = append(result, node)
	}

	return result, nil
}
