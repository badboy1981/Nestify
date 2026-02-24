package cli

import (
	"fmt"
	"io"
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/ignore"
)

// نمایش لیست تمپلیت‌های آماده
func runIgnoreListCmd() {
	templatesPath := filepath.Join("templates", "ignore")
	list, err := ignore.ListAvailableTemplates(templatesPath)
	if err != nil {
		fmt.Println("❌ خطا در خواندن پوشه تمپلیت‌ها:", err)
		return
	}

	fmt.Println("🚫 لیست تمپلیت‌های Ignore آماده:")
	for _, name := range list {
		fmt.Printf("  - %s\n", name)
	}
	fmt.Println("\nاستفاده: nestify ignore-use <name>")
}

// کپی کردن تمپلیت انتخاب شده در فایل پروژه
func runIgnoreUseCmd(templateName string) {
	sourcePath := filepath.Join("templates", "ignore", templateName+".txt")
	destPath := ".nestifyignore"

	// خواندن از تمپلیت و نوشتن در فایل مقصد
	input, err := os.Open(sourcePath)
	if err != nil {
		fmt.Printf("❌ تمپلیت '%s' پیدا نشد.\n", templateName)
		return
	}
	defer input.Close()

	output, err := os.Create(destPath)
	if err != nil {
		fmt.Println("❌ خطا در ایجاد فایل .nestifyignore:", err)
		return
	}
	defer output.Close()

	_, err = io.Copy(output, input)
	if err != nil {
		fmt.Println("❌ خطا در کپی محتوا:", err)
		return
	}

	fmt.Printf("✅ فایل .nestifyignore با استفاده از تمپلیت '%s' ایجاد شد.\n", templateName)
}
