package scanner

import (
	"os"
	"path/filepath"

	// "strings" <- این خط حذف شد چون دیگر استفاده نمی‌شود

	"github.com/badboy1981/Nestify/internal/ignore"
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

	matcher, err := ignore.NewIgnoreMatcher(path)
	if err != nil {
		return nil, err
	}

	rootNode := types.Node{
		Name: filepath.Base(path),
		Type: "folder",
		Size: info.Size(),
	}

	children, err := scanDir(path, path, matcher, foldersOnly)
	if err != nil {
		return nil, err
	}
	rootNode.Children = children

	return []types.Node{rootNode}, nil
}

func scanDir(currentPath, rootPath string, matcher *ignore.IgnoreMatcher, foldersOnly bool) ([]types.Node, error) {
	entries, err := os.ReadDir(currentPath)
	if err != nil {
		return nil, err
	}

	var nodes []types.Node

	for _, entry := range entries {
		if foldersOnly && !entry.IsDir() {
			continue
		}

		fullPath := filepath.Join(currentPath, entry.Name())
		relPath, err := filepath.Rel(rootPath, fullPath)
		if err != nil {
			relPath = entry.Name()
		}

		// بررسی ایگنور شدن قبل از پردازش (بهینه برای سرعت)
		if matcher.ShouldIgnore(relPath, entry.IsDir()) {
			continue
		}

		info, err := entry.Info()
		if err != nil {
			continue
		}

		node := types.Node{
			Name: entry.Name(),
			Size: info.Size(),
		}

		if entry.IsDir() {
			node.Type = "folder"
			children, err := scanDir(fullPath, rootPath, matcher, foldersOnly)
			if err != nil {
				return nil, err
			}
			node.Children = children
		} else {
			node.Type = "file"
		}

		nodes = append(nodes, node)
	}

	return nodes, nil
}
