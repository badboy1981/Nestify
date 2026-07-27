package cli

import (
	"flag"
	"fmt"
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/analyzer"
	"github.com/badboy1981/Nestify/internal/pathutil"
	"github.com/badboy1981/Nestify/internal/scanner"
)

func runAnalyzeCmd() {
	cmd := flag.NewFlagSet("analyze", flag.ExitOnError)
	path := cmd.String("path", ".", "Project path to analyze")
	depth := cmd.Int("depth", 0, "Maximum directory depth to analyze (0 for unlimited)")
	cmd.IntVar(depth, "d", 0, "Maximum directory depth to analyze (shorthand)")

	cmd.Parse(os.Args[2:])

	targetPath := *path
	if targetPath == "." && len(cmd.Args()) > 0 {
		targetPath = cmd.Args()[0]
	}

	fmt.Println("🔍 در حال آنالیز پروژه (با اعمال فیلترهای ignore)...")

	normPath := pathutil.NormalizeForOS(targetPath)

	nodes, err := scanner.Scan(normPath, false, *depth)
	if err != nil {
		fmt.Printf("❌ خطا در اسکن مسیر: %v\n", err)
		return
	}

	if len(nodes) == 0 {
		fmt.Println("⚠️ هیچ فایلی برای آنالیز پیدا نشد.")
		return
	}

	report := analyzer.AnalyzeSkeleton(nodes)

	depthStr := "Unlimited"
	if *depth > 0 {
		depthStr = fmt.Sprintf("%d", *depth)
	}

	// اضافه کردن میزان عمق اسکن در ابتدای گزارش آنالیز
	reportWithDepth := fmt.Sprintf("> **Scan Depth:** %s\n\n%s", depthStr, report)

	reportDir := pathutil.NormalizeForOS("Nestify-Report")
	if err := os.MkdirAll(reportDir, 0755); err != nil {
		fmt.Printf("❌ خطا در ایجاد پوشه گزارشات: %v\n", err)
		return
	}

	outputPath := filepath.Join(reportDir, "skeleton_report.md")
	err = os.WriteFile(outputPath, []byte(reportWithDepth), 0644)
	if err != nil {
		fmt.Printf("❌ خطا در ذخیره گزارش آنالیز: %v\n", err)
		return
	}

	fmt.Println("✅ آنالیز پروژه با موفقیت انجام شد!")
	fmt.Printf("📄 گزارش خروجی ذخیره شد در: %s\n\n", outputPath)
	fmt.Println(reportWithDepth)
}
