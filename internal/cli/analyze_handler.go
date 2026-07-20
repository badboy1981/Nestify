package cli

import (
	"fmt"
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/analyzer"
	"github.com/badboy1981/Nestify/internal/pathutil"
	"github.com/badboy1981/Nestify/internal/scanner"
)

func runAnalyzeCmd() {
	targetPath := "."
	if len(os.Args) > 2 {
		targetPath = os.Args[2]
	}

	fmt.Println("🔍 در حال آنالیز پروژه (با اعمال فیلترهای ignore)...")

	normPath := pathutil.NormalizeForOS(targetPath)

	// ۱. اسکن کامل پروژه
	// خود scanner.Scan به صورت داخلی IgnoreMatcher را اجرا کرده
	// و با پاس دادن false، فایل‌ها را هم جهت محاسبه درصد زبان‌ها اسکن می‌کند.
	nodes, err := scanner.Scan(normPath, false)
	if err != nil {
		fmt.Printf("❌ خطا در اسکن مسیر: %v\n", err)
		return
	}

	if len(nodes) == 0 {
		fmt.Println("⚠️ هیچ فایلی برای آنالیز پیدا نشد.")
		return
	}

	// ۲. تحلیل آمار و زبان‌های پروژه
	report := analyzer.AnalyzeSkeleton(nodes)

	// ۳. ساخت پوشه گزارشات و ذخیره فایل
	reportDir := pathutil.NormalizeForOS("Nestify-Report")
	if err := os.MkdirAll(reportDir, 0755); err != nil {
		fmt.Printf("❌ خطا در ایجاد پوشه گزارشات: %v\n", err)
		return
	}

	outputPath := filepath.Join(reportDir, "skeleton_report.md")
	err = os.WriteFile(outputPath, []byte(report), 0644)
	if err != nil {
		fmt.Printf("❌ خطا در ذخیره گزارش آنالیز: %v\n", err)
		return
	}

	// ۴. نمایش موفقیت
	fmt.Println("✅ آنالیز پروژه با موفقیت انجام شد!")
	fmt.Printf("📄 گزارش خروجی ذخیره شد در: %s\n\n", outputPath)
	fmt.Println(report)
}
