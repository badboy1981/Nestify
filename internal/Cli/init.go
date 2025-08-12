package Cli

// File: init.go

import (
	"encoding/json"
	"fmt"
	"os"

	"github.com/badboy1981/Nestify/internal/generator"
	"github.com/badboy1981/Nestify/internal/types"
)

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