package scanner

import (
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/ignore"
	"github.com/badboy1981/Nestify/internal/types"
)

func Scan(path string, foldersOnly bool) ([]types.Node, error) {
	// ۱. حتما مسیر را مطلق و استاندارد کن
	absPath, _ := filepath.Abs(path)
	absPath = filepath.ToSlash(absPath)

	info, err := os.Stat(absPath)
	if err != nil {
		return nil, err
	}

	// ۲. ماتچر را با مسیر مطلق بساز
	matcher, err := ignore.NewIgnoreMatcher(absPath)
	if err != nil {
		return nil, err
	}

	rootNode := types.Node{
		Name: filepath.Base(absPath),
		Type: "folder",
		Size: info.Size(),
	}

	// ۳. بسیار مهم: هر دو ورودی باید absPath باشند
	children, err := scanDir(absPath, absPath, matcher, foldersOnly)
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
		relPath, _ := filepath.Rel(rootPath, fullPath)

		// --- این بخش باید حتماً اضافه شود ---
		standardRelPath := filepath.ToSlash(relPath)

		// بررسی ایگنور با دقت بالا (ارسال وضعیت پوشه بودن)
		if matcher.ShouldIgnore(standardRelPath, entry.IsDir()) {
			continue
		}

		info, _ := entry.Info()
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
