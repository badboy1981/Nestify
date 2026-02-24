package cli

import (
	"encoding/json"
	"flag"
	"fmt"
	"os"

	"github.com/badboy1981/Nestify/internal/generator"
	"github.com/badboy1981/Nestify/internal/pathutil"
	"github.com/badboy1981/Nestify/internal/types"
)

// بخش تغییر یافته در تابع runInitCmd
func runInitCmd() {
	cmd := flag.NewFlagSet("init", flag.ExitOnError)
	template := cmd.String("template", "template.json", "فایل JSON معماری پروژه")
	path := cmd.String("path", ".", "مسیر ایجاد ساختار پروژه")

	cmd.Parse(os.Args[2:])

	// اصلاح شده: استفاده از پکیج pathutil
	cleanTemplatePath := pathutil.NormalizeForOS(*template)
	cleanDestPath := pathutil.NormalizeForOS(*path)

	runInit(cleanTemplatePath, cleanDestPath)
}

func runInit(templateFile string, path string) {
	// خواندن فایل تمپلیت
	data, err := os.ReadFile(templateFile)
	if err != nil {
		fmt.Printf("❌ خطا در خواندن فایل تمپلیت در مسیر: %s\n", templateFile)
		return
	}

	var template types.Template
	if err := json.Unmarshal(data, &template); err != nil {
		fmt.Println("❌ خطا در تحلیل JSON:", err)
		return
	}

	// ایجاد پوشه ریشه پروژه (مثلاً G:\Test) قبل از ساخت فایل‌ها
	if err := os.MkdirAll(path, 0755); err != nil {
		fmt.Printf("❌ خطا در دسترسی یا ایجاد مسیر مقصد: %s\n", path)
		return
	}

	for _, rootNode := range template.Root {
		// ارسال مسیر مطلق به ژنراتور
		err = generator.CreateStructure(rootNode, path)
		if err != nil {
			fmt.Printf("❌ خطا در ایجاد ساختار: %v\n", err)
			return
		}
	}

	fmt.Printf("✅ پروژه با موفقیت در مسیر زیر ساخته شد:\n   %s\n", path)
}
