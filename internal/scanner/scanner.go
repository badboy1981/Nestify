package scanner

import (
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/types"
)

func Scan(path string) ([]types.Node, error) {
	info, err := os.Stat(path)
	if err != nil {
		return nil, err
	}
	if !info.IsDir() {
		return nil, nil
	}
	return scanDir(path)
}

func scanDir(path string) ([]types.Node, error) {
	entries, err := os.ReadDir(path)
	if err != nil {
		return nil, err
	}

	var nodes []types.Node

	for _, entry := range entries {
		fullPath := filepath.Join(path, entry.Name())
		info, err := os.Stat(fullPath)
		if err != nil {
			return nil, err
		}

		node := types.Node{
			Name: entry.Name(),
			Type: func() string {
				if entry.IsDir() {
					return "folder"
				}
				return "file"
			}(),
			Size: info.Size(),
		}

		if entry.IsDir() {
			children, err := scanDir(fullPath)
			if err != nil {
				return nil, err
			}
			node.Children = children
		}

		nodes = append(nodes, node)
	}

	return nodes, nil
}
