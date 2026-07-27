package scanner

import (
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/ignore"
	"github.com/badboy1981/Nestify/internal/pathutil"
	"github.com/badboy1981/Nestify/internal/types"
)

// Scan اسکن پروژه را با امکان تعیین حداکثر عمق انجام می‌دهد
func Scan(path string, foldersOnly bool, maxDepth int) ([]types.Node, error) {
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

	// شروع اسکن از عمق ۱
	children, err := scanDir(standardRoot, standardRoot, matcher, foldersOnly, 1, maxDepth)
	if err != nil {
		return nil, err
	}
	rootNode.Children = children

	return []types.Node{rootNode}, nil
}

func scanDir(currentPath, rootPath string, matcher *ignore.IgnoreMatcher, foldersOnly bool, currentDepth, maxDepth int) ([]types.Node, error) {
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
		standardRel := pathutil.ToStandardPath(relPath)

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
			// اگر سقف عمق تعیین نشده باشد (0 یا کمتر) یا هنوز به سقف عمق نرسیده باشیم، وارد زیرپوشه می‌شویم
			if maxDepth <= 0 || currentDepth < maxDepth {
				children, err := scanDir(fullPath, rootPath, matcher, foldersOnly, currentDepth+1, maxDepth)
				if err != nil {
					return nil, err
				}
				node.Children = children
			}
		} else {
			node.Type = "file"
		}
		nodes = append(nodes, node)
	}
	return nodes, nil
}
