package cli

import (
	"path/filepath"
	"strings"
)

func NormalizePath(inputPath string) string {
	if inputPath == "" {
		return ""
	}
	// ۱. ابتدا تمام بک‌اسلش‌های احتمالی که سالم مانده‌اند را به اسلش تبدیل می‌کنیم
	standardPath := strings.ReplaceAll(inputPath, "\\", "/")

	// ۲. استفاده از Clean برای حذف موارد اضافی مثل ./
	standardPath = filepath.Clean(standardPath)

	// ۳. تبدیل به فرمت محلی سیستم‌عامل (در ویندوز اسلش‌ها را به بک‌اسلش برمی‌گرداند تا OS بفهمد)
	return filepath.FromSlash(standardPath)
}
