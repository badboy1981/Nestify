package cli

import (
	"encoding/json"
	"flag"
	"fmt"
	"os"

	"github.com/badboy1981/Nestify/internal/generator"
	"github.com/badboy1981/Nestify/internal/scanner"
	"github.com/badboy1981/Nestify/internal/treeprinter"
	"github.com/badboy1981/Nestify/internal/types"
)

func RunCli() {
	initCmd := flag.NewFlagSet("init", flag.ExitOnError)
	scanCmd := flag.NewFlagSet("scan", flag.ExitOnError)

	initTemplate := initCmd.String("template", "template.json", "فایل JSON معماری پروژه")
	initPath := initCmd.String("path", ".", "مسیر ایجاد ساختار پروژه")

	scanPath := scanCmd.String("path", ".", "مسیر پروژه برای اسکن")
	printTreeFlag := scanCmd.Bool("tree", false, "نمایش ساختار به صورت درختی")

	if len(os.Args) < 2 {
		fmt.Println("❌ لطفا یک ساب‌کامند وارد کنید: init یا scan")
		return
	}

	switch os.Args[1] {
	case "init":
		initCmd.Parse(os.Args[2:])
		runInit(*initTemplate, *initPath)
	case "scan":
		scanCmd.Parse(os.Args[2:])
		runScan(*scanPath, *printTreeFlag)
	default:
		fmt.Println("❌ ساب‌کامند نامعتبر. فقط init یا scan پشتیبانی می‌شود.")
	}
}

func runInit(templateFile string, path string) {
	data, err := os.ReadFile(templateFile)
	if err != nil {
		fmt.Println("❌ خطا در خواندن فایل قالب:", err)
		return
	}

	var template types.Template
	if err := json.Unmarshal(data, &template); err != nil {
		fmt.Println("❌ خطا در پارس JSON:", err)
		return
	}

	for _, rootNode := range template.Root {
		err = generator.CreateStructure(rootNode, path)
		if err != nil {
			fmt.Println("❌ خطا در ایجاد ساختار:", err)
			return
		}
	}

	fmt.Println("✅ ساختار پروژه با موفقیت ایجاد شد.")
}

func runScan(path string, printTree bool) {
	rootNodes, err := scanner.Scan(path)
	if err != nil {
		fmt.Println("❌ خطا در اسکن مسیر:", err)
		return
	}

	outputFile := "scan_output.json"
	file, err := os.Create(outputFile)
	if err != nil {
		fmt.Println("❌ خطا در ایجاد فایل خروجی:", err)
		return
	}
	defer file.Close()

	encoder := json.NewEncoder(file)
	encoder.SetIndent("", "  ")
	if err := encoder.Encode(rootNodes); err != nil {
		fmt.Println("❌ خطا در نوشتن JSON:", err)
		return
	}

	fmt.Println("✅ خروجی اسکن در", outputFile, "ذخیره شد.")

	if printTree {
		for _, root := range rootNodes {
			treeprinter.PrintTree(&root)
		}
	}
}
