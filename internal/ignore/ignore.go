package ignore

import (
	"bufio"
	"os"
	"path/filepath"
	"strings"
)

type IgnoreMatcher struct {
	patterns []string
}

func ListAvailableTemplates(templatesPath string) ([]string, error) {
	entries, err := os.ReadDir(templatesPath)
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

func NewIgnoreMatcher(rootPath string) (*IgnoreMatcher, error) {
	filePath := filepath.Join(rootPath, ".nestifyignore")
	var patterns []string

	file, err := os.Open(filePath)
	if err == nil {
		defer file.Close()
		scanner := bufio.NewScanner(file)
		for scanner.Scan() {
			line := strings.TrimSpace(scanner.Text())
			// نادیده گرفتن خطوط خالی و کامنت‌ها
			if line != "" && !strings.HasPrefix(line, "#") {
				// حذف اسلش‌های ابتدا و انتها برای مقایسه راحت‌تر
				line = strings.Trim(line, "/")
				patterns = append(patterns, line)
			}
		}
	}

	// همیشه این موارد سیستمی را اضافه کن
	patterns = append(patterns, ".git", "node_modules", ".Test", "Test")

	return &IgnoreMatcher{patterns: patterns}, nil
}

// func (m *IgnoreMatcher) ShouldIgnore(path string, isDir bool) bool {
// 	// مسیر ورودی قبلاً در اسکنر استاندارد شده، فقط چک کن
// 	cleanPath := strings.Trim(path, "/")

//		for _, p := range m.patterns {
//			if cleanPath == p || strings.HasPrefix(cleanPath, p+"/") {
//				return true
//			}
//		}
//		return false
//	}
func (m *IgnoreMatcher) ShouldIgnore(path string, isDir bool) bool {
	for _, pattern := range m.patterns {
		// استفاده از Match برای پشتیبانی از ستاره و الگوها
		matched, _ := filepath.Match(pattern, path)
		if matched {
			return true
		}
		// چک کردن مستقیم برای پوشه‌ها یا نام‌های دقیق
		if path == pattern {
			return true
		}
	}
	return false
}
