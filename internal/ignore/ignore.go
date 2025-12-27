// File: internal/ignore/ignore.go
package ignore

import (
	"bufio"
	"os"
	"path/filepath"
	"strings"
)

// IgnoreMatcher holds the rules for ignoring paths
type IgnoreMatcher struct {
	patterns []string
}

// NewIgnoreMatcher creates a matcher from a .nestifyignore file in the given root path
func NewIgnoreMatcher(rootPath string) (*IgnoreMatcher, error) {
	matcher := &IgnoreMatcher{}

	filePath := filepath.Join(rootPath, ".nestifyignore")
	if _, err := os.Stat(filePath); os.IsNotExist(err) {
		// اگر فایل وجود نداشت، هیچ ignoreای اعمال نمی‌شه
		return matcher, nil
	}

	file, err := os.Open(filePath)
	if err != nil {
		return nil, err
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := strings.TrimSpace(scanner.Text())

		// نادیده گرفتن خطوط خالی و کامنت‌ها
		if line == "" || strings.HasPrefix(line, "#") {
			continue
		}

		// اضافه کردن الگو
		matcher.patterns = append(matcher.patterns, line)
	}

	return matcher, scanner.Err()
}

// ShouldIgnore checks if a relative path should be ignored
func (m *IgnoreMatcher) ShouldIgnore(relPath string) bool {
	// اول چک کردن الگوهای پیش‌فرض سخت‌کد شده (حفظ رفتار قبلی)
	if m.isDefaultIgnored(relPath) {
		return true
	}

	// چک کردن الگوهای .nestifyignore
	for _, pattern := range m.patterns {
		if matchPattern(pattern, relPath) {
			return true
		}
	}
	return false
}

// الگوهای پیش‌فرض (مثل قبل)
func (m *IgnoreMatcher) isDefaultIgnored(relPath string) bool {
	base := filepath.Base(relPath)
	switch base {
	case ".git", "node_modules", ".github", "bin", "obj":
		return true
	}
	return false
}

// matchPattern - نسخه اصلاح‌شده و تست‌شده برای پوشه‌ها و فایل‌ها
func matchPattern(pattern, path string) bool {
	pattern = strings.TrimSpace(pattern)
	path = strings.TrimSpace(path)

	if pattern == path {
		return true
	}

	// پوشه‌ها: الگو با / تموم بشه
	if strings.HasSuffix(pattern, "/") {
		pattern = strings.TrimSuffix(pattern, "/")
		return path == pattern || path == pattern+"/" || strings.HasPrefix(path, pattern+"/")
	}

	// فایل‌ها با پسوند
	if strings.HasPrefix(pattern, "*.") {
		ext := pattern[1:]
		return strings.HasSuffix(path, ext)
	}

	// مسیر دقیق یا زیرمسیر
	if strings.Contains(pattern, "/") {
		return path == pattern || strings.HasPrefix(path, pattern+"/")
	}

	// فقط اسم
	return filepath.Base(path) == pattern
}
