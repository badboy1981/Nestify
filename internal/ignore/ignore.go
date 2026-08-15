package ignore

import (
	"bufio"
	"embed"
	"os"
	"path/filepath"
	"strings"

	"github.com/badboy1981/Nestify/internal/pathutil"
)

// ListAvailableTemplatesFromFS dynamically lists template names from an embedded FS directory.
func ListAvailableTemplatesFromFS(fs embed.FS, templatesDir string) ([]string, error) {
	entries, err := fs.ReadDir(templatesDir)
	if err != nil {
		return nil, err
	}
	var list []string
	for _, e := range entries {
		if !e.IsDir() && strings.HasSuffix(e.Name(), ".txt") {
			list = append(list, strings.TrimSuffix(e.Name(), ".txt"))
		}
	}
	return list, nil
}

type IgnoreMatcher struct {
	patterns []string
}

func NewIgnoreMatcher(targetPath string) (*IgnoreMatcher, error) {
	var patterns []string

	// 1. Load .nestifyignore from the current working directory (where the command is run).
	cwd, err := os.Getwd()
	if err == nil {
		cwdIgnore := filepath.Join(cwd, ".nestifyignore")
		patterns = append(patterns, readIgnoreFile(cwdIgnore)...)
	}

	// 2. If targetPath differs from cwd and has its own .nestifyignore, load that too.
	absTarget, err1 := filepath.Abs(targetPath)
	absCwd, err2 := filepath.Abs(cwd)
	if err1 == nil && err2 == nil && absTarget != absCwd {
		targetIgnore := filepath.Join(targetPath, ".nestifyignore")
		patterns = append(patterns, readIgnoreFile(targetIgnore)...)
	}

	// Built-in system defaults.
	patterns = append(patterns, ".git", "node_modules", ".Test", "Test")

	// Deduplicate patterns.
	uniquePatterns := make([]string, 0, len(patterns))
	seen := make(map[string]bool)
	for _, p := range patterns {
		if !seen[p] {
			seen[p] = true
			uniquePatterns = append(uniquePatterns, p)
		}
	}

	return &IgnoreMatcher{patterns: uniquePatterns}, nil
}

func readIgnoreFile(filePath string) []string {
	var patterns []string
	file, err := os.Open(filePath)
	if err != nil {
		return patterns
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := strings.TrimSpace(scanner.Text())
		if line != "" && !strings.HasPrefix(line, "#") {
			line = strings.Trim(line, "/")
			line = pathutil.ToStandardPath(line)
			patterns = append(patterns, line)
		}
	}
	return patterns
}

func (m *IgnoreMatcher) ShouldIgnore(path string, isDir bool) bool {
	cleanPath := pathutil.ToStandardPath(path)
	baseName := filepath.Base(cleanPath)

	for _, pattern := range m.patterns {
		// Match against file or folder name (e.g. node_modules or *.log).
		matchedBase, _ := filepath.Match(pattern, baseName)
		if matchedBase || baseName == pattern {
			return true
		}

		// Match against full relative path (e.g. build/outputs).
		matchedPath, _ := filepath.Match(pattern, cleanPath)
		if matchedPath || cleanPath == pattern || strings.HasPrefix(cleanPath, pattern+"/") {
			return true
		}
	}
	return false
}
