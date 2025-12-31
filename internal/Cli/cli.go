// internal/cli/cli.go
package cli

import (
	"flag"
	"fmt"
	"os"

	"github.com/badboy1981/Nestify/internal/analyzer"
)

func RunCli() {
	// چک کردن global flags اول
	if len(os.Args) == 1 {
		ShowHelp()
		return
	}

	arg := os.Args[1]

	if arg == "--help" || arg == "-h" {
		ShowHelp()
		return
	}

	if arg == "--version" || arg == "version" {
		ShowVersion()
		return
	}

	if len(os.Args) < 2 {
		fmt.Println("❌ لطفا یک ساب‌کامند وارد کنید: init، scan یا analyze")
		fmt.Println("برای راهنمایی بیشتر: nestify --help")
		return
	}

	switch arg {
	case "init":
		runInitCmd()
	case "scan":
		runScanCmd()
	case "analyze":
		runAnalyzeCmd()
	default:
		fmt.Printf("❌ ساب‌کامند نامعتبر: %s\n", arg)
		fmt.Println("ساب‌کامندهای معتبر: init، scan، analyze")
		fmt.Println("برای راهنمایی بیشتر: nestify --help")
	}
}

// زیرکامندها رو جدا کردیم تا ماژولار بشه (می‌تونی بعداً به فایل جدا منتقل کنی)
func runInitCmd() {
	cmd := flag.NewFlagSet("init", flag.ExitOnError)
	template := cmd.String("template", "template.json", "فایل JSON معماری پروژه")
	path := cmd.String("path", ".", "مسیر ایجاد ساختار پروژه")

	cmd.Parse(os.Args[2:])

	runInit(*template, *path)
}

func runScanCmd() {
	cmd := flag.NewFlagSet("scan", flag.ExitOnError)
	path := cmd.String("path", ".", "مسیر پروژه برای اسکن")
	tree := cmd.Bool("tree", false, "نمایش ساختار به صورت درختی و ذخیره Markdown")
	foldersOnly := cmd.Bool("folders-only", false, "فقط پوشه‌ها را اسکن کن")

	cmd.Parse(os.Args[2:])

	runScan(*path, *tree, *foldersOnly)
}

func runAnalyzeCmd() {
	cmd := flag.NewFlagSet("analyze", flag.ExitOnError)
	path := cmd.String("path", ".", "مسیر پروژه برای تحلیل اسکلت")

	cmd.Parse(os.Args[2:])

	analyzer.RunAnalyze(*path)
}
