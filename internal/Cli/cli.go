package Cli

// File: cli.go

import (
	"flag"
	"fmt"
	"os"

	"github.com/badboy1981/Nestify/internal/analyzer"
)

func RunCli() {
	initCmd := flag.NewFlagSet("init", flag.ExitOnError)
	scanCmd := flag.NewFlagSet("scan", flag.ExitOnError)
	analyzeCmd := flag.NewFlagSet("analyze", flag.ExitOnError)

	initTemplate := initCmd.String("template", "template.json", "فایل JSON معماری پروژه")
	initPath := initCmd.String("path", ".", "مسیر ایجاد ساختار پروژه")

	scanPath := scanCmd.String("path", ".", "مسیر پروژه برای اسکن")
	printTreeFlag := scanCmd.Bool("tree", false, "نمایش ساختار به صورت درختی و ذخیره Markdown")
	foldersOnlyFlag := scanCmd.Bool("folders-only", false, "فقط پوشه‌ها را اسکن کن")

	analyzePath := analyzeCmd.String("path", ".", "مسیر پروژه برای تحلیل اسکلت")

	if len(os.Args) < 2 {
		fmt.Println("❌ لطفا یک ساب‌کامند وارد کنید: init، scan یا analyze")
		return
	}

	switch os.Args[1] {
	case "init":
		initCmd.Parse(os.Args[2:])
		runInit(*initTemplate, *initPath)
	case "scan":
		scanCmd.Parse(os.Args[2:])
		runScan(*scanPath, *printTreeFlag, *foldersOnlyFlag)
	case "analyze":
		analyzeCmd.Parse(os.Args[2:])
		analyzer.RunAnalyze(*analyzePath)
	default:
		fmt.Println("❌ ساب‌کامند نامعتبر. فقط init، scan یا analyze پشتیبانی می‌شود.")
	}
}
