package scanner

// بالای فایل، import جدید اضافه کن
import (
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/ignore" // اضافه شد
	"github.com/badboy1981/Nestify/internal/types"
)

// تغییر تابع Scan برای دریافت matcher
func Scan(path string, foldersOnly bool) ([]types.Node, error) {
	info, err := os.Stat(path)
	if err != nil {
		return nil, err
	}
	if !info.IsDir() {
		return nil, nil
	}

	// ساخت ignore matcher از ریشه پروژه
	matcher, err := ignore.NewIgnoreMatcher(path)
	if err != nil {
		return nil, err
	}

	rootNode := types.Node{
		Name: filepath.Base(path),
		Type: "folder",
		Size: info.Size(),
	}

	children, err := scanDir(path, path, matcher, foldersOnly) // path دوم برای ریشه
	if err != nil {
		return nil, err
	}
	rootNode.Children = children

	return []types.Node{rootNode}, nil
}

// تغییر scanDir برای دریافت rootPath و matcher
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

		// مسیر نسبی نسبت به ریشه پروژه
		relPath, err := filepath.Rel(rootPath, fullPath)
		if err != nil {
			relPath = entry.Name()
		}

		// اگر پوشه باشه، به relPath یه / اضافه کن (مثل gitignore)
		if entry.IsDir() {
			// relPath += string(filepath.Separator)
			relPath += "/" // همیشه از / استفاده کن، حتی در ویندوز
		}

		// چک ignore
		if matcher.ShouldIgnore(relPath) {
			continue
		}

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
			children, err := scanDir(fullPath, rootPath, matcher, foldersOnly)
			if err != nil {
				return nil, err
			}
			node.Children = children
		}

		nodes = append(nodes, node)
	}

	return nodes, nil
}
