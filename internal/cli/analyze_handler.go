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

	fmt.Println("🔍 Analyzing project (applying ignore filters)...")

	normPath := pathutil.NormalizeForOS(targetPath)

	nodes, err := scanner.Scan(normPath, false, *depth)
	if err != nil {
		fmt.Printf("❌ Scan error: %v\n", err)
		return
	}

	if len(nodes) == 0 {
		fmt.Println("⚠️ No files found to analyze.")
		return
	}

	report := analyzer.AnalyzeSkeleton(nodes)

	depthStr := "Unlimited"
	if *depth > 0 {
		depthStr = fmt.Sprintf("%d", *depth)
	}

	// Prepend scan depth information to the analysis report.
	reportWithDepth := fmt.Sprintf("> **Scan Depth:** %s\n\n%s", depthStr, report)

	reportDir := pathutil.NormalizeForOS("Nestify-Report")
	if err := os.MkdirAll(reportDir, 0755); err != nil {
		fmt.Printf("❌ Failed to create report directory: %v\n", err)
		return
	}

	outputPath := filepath.Join(reportDir, "skeleton_report.md")
	err = os.WriteFile(outputPath, []byte(reportWithDepth), 0644)
	if err != nil {
		fmt.Printf("❌ Failed to save analysis report: %v\n", err)
		return
	}

	fmt.Println("✅ Project analysis completed successfully!")
	fmt.Printf("📄 Report saved to: %s\n\n", outputPath)
	fmt.Println(reportWithDepth)
}
