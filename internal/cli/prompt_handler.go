package cli

import (
	"fmt"
	"path"
	"strings"

	"github.com/badboy1981/Nestify/internal/ignore"
)

// لیست کردن تمپلیت‌های پرامپت موجود از پوشه templates-prompts
func runPromptListCmd() {
	list, err := ignore.ListAvailableTemplatesFromFS(templatesFS, "templates-prompts")
	if err != nil {
		fmt.Println("❌ خطا در خواندن تمپلیت‌های پرامپت:", err)
		return
	}

	fmt.Println("📋 لیست پرامپت‌های آماده پیش‌فرض:")
	for _, name := range list {
		fmt.Printf("  - %s\n", name)
	}
	fmt.Println("\nاستفاده با دستور context:")
	fmt.Println("  nestify context -p <template_name_or_text>")
	fmt.Println("نمایش متن پرامپت در ترمینال:")
	fmt.Println("  nestify prompt <template_name>")
}

// نمایش متن یک پرامپت خاص در ترمینال
func runPromptShowCmd(templateName string) {
	sourcePath := path.Join("templates-prompts", templateName+".txt")

	data, err := templatesFS.ReadFile(sourcePath)
	if err != nil {
		fmt.Printf("❌ پرامپت '%s' پیدا نشد.\n", templateName)
		fmt.Println("برای دیدن لیست پرامپت‌ها: nestify prompt-list")
		return
	}

	fmt.Printf("📄 پرامپت [%s]:\n", templateName)
	fmt.Println("--------------------------------------------------")
	fmt.Println(string(data))
	fmt.Println("--------------------------------------------------")
}

// تابع کمکی برای خواندن متن پرامپت (جهت استفاده در context_handler)
func getPromptContent(promptInput string) string {
	promptInput = strings.TrimSpace(promptInput)
	if promptInput == "" {
		return ""
	}

	// اگر کاربر فقط -p زد یا -p default نوشت
	targetTemplate := promptInput
	if promptInput == "true" || promptInput == "default" {
		targetTemplate = "default"
	}

	// ابتدا بررسی می‌کنیم آیا تمپلیتی با این نام وجود دارد
	sourcePath := path.Join("templates-prompts", targetTemplate+".txt")
	data, err := templatesFS.ReadFile(sourcePath)
	if err == nil {
		return string(data)
	}

	// اگر تمپلیتی پیدا نشد، متن ورودی کاربر را به عنوان پرامپت اختصاصی در نظر می‌گیریم
	return promptInput
}
