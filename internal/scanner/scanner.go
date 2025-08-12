package scanner

// File: scanner.go

import (
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/types"
)

func Scan(path string, foldersOnly bool) ([]types.Node, error) {
	info, err := os.Stat(path)
	if err != nil {
		return nil, err
	}
	if !info.IsDir() {
		return nil, nil
	}

	// ایجاد Node برای پوشه ریشه
	rootNode := types.Node{
		Name: filepath.Base(path),
		Type: "folder",
		Size: info.Size(),
	}

	// اسکن محتوای داخل پوشه
	children, err := scanDir(path, foldersOnly)
	if err != nil {
		return nil, err
	}
	rootNode.Children = children

	return []types.Node{rootNode}, nil
}

func scanDir(path string, foldersOnly bool) ([]types.Node, error) {
	// لیست پوشه‌های نادیده گرفته‌شده
	ignoreFolders := map[string]bool{
		".git":         true,
		"node_modules": true,
	}

	entries, err := os.ReadDir(path)
	if err != nil {
		return nil, err
	}

	var nodes []types.Node

	for _, entry := range entries {
		if foldersOnly && !entry.IsDir() {
			continue // فقط پوشه‌ها رو اسکن کن
		}
		if entry.IsDir() && ignoreFolders[entry.Name()] {
			continue // نادیده گرفتن پوشه‌های مشخص‌شده
		}

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
			children, err := scanDir(fullPath, foldersOnly)
			if err != nil {
				return nil, err
			}
			node.Children = children
		}

		nodes = append(nodes, node)
	}

	return nodes, nil
}
