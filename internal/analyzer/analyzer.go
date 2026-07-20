package analyzer

import (
	"fmt"
	"path/filepath"
	"sort"
	"strings"

	"github.com/badboy1981/Nestify/internal/types"
)

type LanguageStat struct {
	Name       string
	Extension  string
	Count      int
	TotalBytes int64
	Percentage float64
}

// AnalyzeSkeleton تحلیل جامع پروژه شامل تفکیک زبان‌ها به سبک گیت‌هاب
func AnalyzeSkeleton(nodes []types.Node) string {
	extMap := make(map[string]*LanguageStat)
	var totalFiles int
	var totalFolders int
	var totalBytes int64

	// پیمایش کامل گره‌ها برای استخراج آمار
	var processNode func(node types.Node)
	processNode = func(node types.Node) {
		if node.Type == "folder" {
			totalFolders++
			for _, child := range node.Children {
				processNode(child)
			}
		} else {
			totalFiles++
			totalBytes += node.Size

			ext := strings.ToLower(filepath.Ext(node.Name))
			if ext == "" {
				ext = "other"
			}

			langName := detectLanguageName(ext, node.Name)
			if stat, exists := extMap[langName]; exists {
				stat.Count++
				stat.TotalBytes += node.Size
			} else {
				extMap[langName] = &LanguageStat{
					Name:       langName,
					Extension:  ext,
					Count:      1,
					TotalBytes: node.Size,
				}
			}
		}
	}

	for _, node := range nodes {
		processNode(node)
	}

	// تبدیل مپ به اسلایس و مرتب‌سازی بر اساس حجم
	var stats []LanguageStat
	for _, stat := range extMap {
		if totalBytes > 0 {
			stat.Percentage = (float64(stat.TotalBytes) / float64(totalBytes)) * 100
		}
		stats = append(stats, *stat)
	}

	sort.Slice(stats, func(i, j int) bool {
		return stats[i].TotalBytes > stats[j].TotalBytes
	})

	// ساخت گزارش مارک‌داون
	var sb strings.Builder
	sb.WriteString("# 🧠 Nestify Project Analysis Report\n\n")

	// ۱. آمارهای کلی پروژه
	sb.WriteString("## 📊 Project Metrics\n")
	sb.WriteString(fmt.Sprintf("- **Total Size:** %.2f KB\n", float64(totalBytes)/1024))
	sb.WriteString(fmt.Sprintf("- **Total Files:** %d\n", totalFiles))
	sb.WriteString(fmt.Sprintf("- **Total Folders:** %d\n\n", totalFolders))

	// ۲. تفکیک زبان‌ها به سبک گیت‌هاب
	sb.WriteString("## 🌐 Languages Breakdown\n")
	for _, stat := range stats {
		progressBar := makeProgressBar(stat.Percentage)
		sb.WriteString(fmt.Sprintf("- **%-12s** `%s` %6.1f%% (%d files, %.1f KB)\n",
			stat.Name, progressBar, stat.Percentage, stat.Count, float64(stat.TotalBytes)/1024))
	}

	// ۳. بخش آماده‌سازی برای AI
	sb.WriteString("\n---\n")
	sb.WriteString("### 🤖 Prompt-Ready Summary for AI Analysis\n")
	sb.WriteString("```json\n{\n")
	sb.WriteString(fmt.Sprintf("  \"total_files\": %d,\n", totalFiles))
	sb.WriteString(fmt.Sprintf("  \"total_size_bytes\": %d,\n", totalBytes))
	sb.WriteString("  \"top_languages\": [\n")
	for i, stat := range stats {
		comma := ","
		if i == len(stats)-1 {
			comma = ""
		}
		sb.WriteString(fmt.Sprintf("    {\"language\": \"%s\", \"percentage\": %.1f}%s\n", stat.Name, stat.Percentage, comma))
	}
	sb.WriteString("  ]\n}\n```\n")

	return sb.String()
}

func makeProgressBar(percent float64) string {
	totalBlocks := 10
	filled := int((percent / 100.0) * float64(totalBlocks))
	if filled == 0 && percent > 0 {
		filled = 1
	}
	return strings.Repeat("█", filled) + strings.Repeat("░", totalBlocks-filled)
}

func detectLanguageName(ext, filename string) string {
	switch ext {
	case ".go":
		return "Go"
	case ".cs":
		return "C#"
	case ".js":
		return "JavaScript"
	case ".ts":
		return "TypeScript"
	case ".py":
		return "Python"
	case ".json":
		return "JSON"
	case ".md":
		return "Markdown"
	case ".txt":
		return "Text"
	case ".html":
		return "HTML"
	case ".css":
		return "CSS"
	default:
		if filename == "Dockerfile" || filename == "Makefile" {
			return filename
		}
		return "Other"
	}
}
