package scanner

import (
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/ignore"
	"github.com/badboy1981/Nestify/internal/pathutil" // فقط این را برای آدرس‌ها استفاده کن
	"github.com/badboy1981/Nestify/internal/types"
)

func Scan(path string, foldersOnly bool) ([]types.Node, error) {
	osPath := pathutil.NormalizeForOS(path)
	info, err := os.Stat(osPath)
	if err != nil {
		return nil, err
	}

	standardRoot := pathutil.ToStandardPath(osPath)
	matcher, err := ignore.NewIgnoreMatcher(standardRoot)
	if err != nil {
		return nil, err
	}

	rootNode := types.Node{
		Name: filepath.Base(standardRoot),
		Type: "folder",
		Size: info.Size(),
	}

	children, err := scanDir(standardRoot, standardRoot, matcher, foldersOnly)
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

		entryName := entry.Name()
		fullPath := filepath.Join(currentPath, entryName)

		relPath, _ := filepath.Rel(rootPath, fullPath)
		standardRel := pathutil.ToStandardPath(relPath) // اصلاح استفاده از پکیج جدید

		if matcher != nil {
			if matcher.ShouldIgnore(entryName, entry.IsDir()) || matcher.ShouldIgnore(standardRel, entry.IsDir()) {
				continue
			}
		}

		info, _ := entry.Info()
		node := types.Node{
			Name: entryName,
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
