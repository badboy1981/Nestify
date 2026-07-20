package cli

import (
	"embed"
	"fmt"
	"os"
	"path"

	"github.com/badboy1981/Nestify/internal/ignore"
)

var templatesFS embed.FS

func SetTemplatesFS(fs embed.FS) {
	templatesFS = fs
}

func runIgnoreListCmd() {
	// خواندن مستقیم از پوشه templates-ignore
	list, err := ignore.ListAvailableTemplatesFromFS(templatesFS, "templates-ignore")
	if err != nil {
		fmt.Println("❌ خطا در خواندن تمپلیت‌ها:", err)
		return
	}

	fmt.Println("🚫 لیست تمپلیت‌های Ignore آماده:")
	for _, name := range list {
		fmt.Printf("  - %s\n", name)
	}
	fmt.Println("\nاستفاده: nestify ignore-use <name>")
}

func runIgnoreUseCmd(templateName string) {
	// آدرس فایل در پوشه templates-ignore
	sourcePath := path.Join("templates-ignore", templateName+".txt")
	destPath := ".nestifyignore"

	data, err := templatesFS.ReadFile(sourcePath)
	if err != nil {
		fmt.Printf("❌ تمپلیت '%s' پیدا نشد.\n", templateName)
		return
	}

	err = os.WriteFile(destPath, data, 0644)
	if err != nil {
		fmt.Println("❌ خطا در ایجاد فایل .nestifyignore:", err)
		return
	}

	fmt.Printf("✅ فایل .nestifyignore با استفاده از تمپلیت '%s' ایجاد شد.\n", templateName)
}
