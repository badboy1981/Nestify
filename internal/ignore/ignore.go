package ignore

import (
	"os"
	"path/filepath"
	"strings"

	"github.com/monochromegane/go-gitignore"
)

type IgnoreMatcher struct {
	matcher gitignore.IgnoreMatcher
}

// تابع برای لیست کردن تمپلیت‌های موجود در پوشه templates/ignore
func ListAvailableTemplates(templatesPath string) ([]string, error) {
	entries, err := os.ReadDir(templatesPath)
	if err != nil {
		return nil, err
	}
	var list []string
	for _, e := range entries {
		// فقط فایل‌های .txt را به عنوان تمپلیت در نظر می‌گیریم
		if !e.IsDir() && strings.HasSuffix(e.Name(), ".txt") {
			list = append(list, strings.TrimSuffix(e.Name(), ".txt"))
		}
	}
	return list, nil
}

func NewIgnoreMatcher(rootPath string) (*IgnoreMatcher, error) {
	filePath := filepath.Join(rootPath, ".nestifyignore")

	if _, err := os.Stat(filePath); os.IsNotExist(err) {
		return &IgnoreMatcher{
			matcher: gitignore.NewGitIgnoreFromReader(rootPath, strings.NewReader("")),
		}, nil
	}

	matcher, err := gitignore.NewGitIgnore(filePath, rootPath)
	if err != nil {
		return nil, err
	}

	return &IgnoreMatcher{matcher: matcher}, nil
}

// اضافه شدن آرگومان isDir برای تشخیص الگوهای پوشه (مثل /node_modules/)
func (m *IgnoreMatcher) ShouldIgnore(relPath string, isDir bool) bool {
	relPath = filepath.ToSlash(relPath)
	return m.matcher.Match(relPath, isDir)
}
