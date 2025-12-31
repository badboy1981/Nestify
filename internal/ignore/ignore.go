// internal/ignore/ignore.go
package ignore

import (
	"os"
	"path/filepath"
	"strings"

	"github.com/monochromegane/go-gitignore"
)

// IgnoreMatcher ساختار نگهدارنده مچر
type IgnoreMatcher struct {
	matcher gitignore.IgnoreMatcher
}

// NewIgnoreMatcher فایل .nestifyignore را بارگذاری می‌کند
func NewIgnoreMatcher(rootPath string) (*IgnoreMatcher, error) {
	filePath := filepath.Join(rootPath, ".nestifyignore")

	// بررسی وجود فایل برای جلوگیری از خطای سیستمی
	if _, err := os.Stat(filePath); os.IsNotExist(err) {
		// اگر فایل نباشد، یک مچر خالی برمی‌گردانیم که هیچ فایلی را ایگنور نمی‌کند
		return &IgnoreMatcher{
			matcher: gitignore.NewGitIgnoreFromReader(rootPath, strings.NewReader("")),
		}, nil
	}

	// استفاده از متد نیو برای خواندن فایل
	// آرگومان اول مسیر ریشه پروژه است تا مچر بداند الگوها نسبت به کجا هستند
	matcher, err := gitignore.NewGitIgnore(filePath, rootPath)
	if err != nil {
		return nil, err
	}

	return &IgnoreMatcher{matcher: matcher}, nil
}

// ShouldIgnore بررسی می‌کند که آیا مسیر مورد نظر باید حذف شود یا خیر
func (m *IgnoreMatcher) ShouldIgnore(relPath string, isDir bool) bool {
	// ۱. تبدیل جداکننده‌ها به اسلش (/) برای سازگاری در ویندوز
	relPath = filepath.ToSlash(relPath)

	// ۲. کتابخانه monochromegane از ما می‌پرسد که آیا این مسیر یک دایرکتوری است یا خیر.
	// بهتر است این مقدار را از بدنه اصلی برنامه (جایی که Walk انجام می‌دهید) پاس دهید.
	return m.matcher.Match(relPath, isDir)
}
