package ignore

import (
	"bufio"
	"embed"
	"os"
	"path/filepath"
	"strings"
)

// خواندن کاملا پویای لیست تمپلیت‌ها بدون نیاز به تعریف نام آنها در کد
func ListAvailableTemplatesFromFS(fs embed.FS, templatesDir string) ([]string, error) {
	entries, err := fs.ReadDir(templatesDir)
	if err != nil {
		return nil, err
	}
	var list []string
	for _, e := range entries {
		if !e.IsDir() && strings.HasSuffix(e.Name(), ".txt") {
			list = append(list, strings.TrimSuffix(e.Name(), ".txt"))
		}
	}
	return list, nil
}

type IgnoreMatcher struct {
	patterns []string
}

func NewIgnoreMatcher(rootPath string) (*IgnoreMatcher, error) {
	filePath := filepath.Join(rootPath, ".nestifyignore")
	var patterns []string

	file, err := os.Open(filePath)
	if err == nil {
		defer file.Close()
		scanner := bufio.NewScanner(file)
		for scanner.Scan() {
			line := strings.TrimSpace(scanner.Text())
			if line != "" && !strings.HasPrefix(line, "#") {
				line = strings.Trim(line, "/")
				patterns = append(patterns, line)
			}
		}
	}

	patterns = append(patterns, ".git", "node_modules", ".Test", "Test")

	return &IgnoreMatcher{patterns: patterns}, nil
}

func (m *IgnoreMatcher) ShouldIgnore(path string, isDir bool) bool {
	for _, pattern := range m.patterns {
		matched, _ := filepath.Match(pattern, path)
		if matched || path == pattern {
			return true
		}
	}
	return false
}
