package analyzer

// File: analyzer.go

import (
	"fmt"
	"os"
	"strings"

	"github.com/badboy1981/Nestify/internal/scanner"
	"github.com/badboy1981/Nestify/internal/types"
)

func RunAnalyze(path string) {
	nodes, err := scanner.Scan(path, true)
	if err != nil {
		fmt.Println("❌ خطا در اسکن مسیر:", err)
		return
	}
	report := AnalyzeSkeleton(nodes)
	fmt.Println(report)
	err = os.WriteFile("skeleton_report.md", []byte(report), 0644)
	if err != nil {
		fmt.Println("❌ خطا در ذخیره گزارش:", err)
	} else {
		fmt.Println("✅ گزارش اسکلت در skeleton_report.md ذخیره شد.")
	}
}

func AnalyzeSkeleton(nodes []types.Node) string {
	report := "اسکلت پروژه:\n"
	for _, node := range nodes {
		assignRole(&node)
		report += fmt.Sprintf("- %s: %s\n", node.Name, node.Role)
		for _, child := range node.Children {
			if child.Type == "folder" {
				report += fmt.Sprintf("  - %s: %s\n", child.Name, child.Role)
			}
		}
	}
	return report
}

func assignRole(node *types.Node) {
	nameLower := strings.ToLower(node.Name)
	switch {
	case strings.Contains(nameLower, "cmd"):
		node.Role = "نقطه ورود برنامه"
	case strings.Contains(nameLower, "config"):
		node.Role = "تنظیمات و templateها"
	case strings.Contains(nameLower, "internal"):
		node.Role = "ماژول‌های داخلی"
	case strings.Contains(nameLower, "src") || strings.Contains(nameLower, "main"):
		node.Role = "هسته اصلی کد"
	case strings.Contains(nameLower, "test"):
		node.Role = "تست‌ها"
	case strings.Contains(nameLower, "assets") || strings.Contains(nameLower, "static"):
		node.Role = "منابع استاتیک"
	case node.Type == "folder" && len(node.Children) > 0:
		node.Role = "پوشه نگهدارنده"
	default:
		node.Role = "نامشخص"
	}
	for i := range node.Children {
		assignRole(&node.Children[i])
	}
}
