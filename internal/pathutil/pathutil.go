package pathutil

import (
	"path/filepath"
	"strings"
)

func NormalizeForOS(inputPath string) string {
	if inputPath == "" || inputPath == "." {
		abs, _ := filepath.Abs(".")
		return abs
	}
	return filepath.Clean(inputPath)
}

func ToStandardPath(inputPath string) string {
	standard := filepath.ToSlash(inputPath)
	standard = strings.TrimPrefix(standard, "./")
	return standard
}
