package copier

import (
	"fmt"
	"io"
	"os"
	"path/filepath"
	"strings"

	"github.com/badboy1981/Nestify/internal/ignore"
	"github.com/badboy1981/Nestify/internal/pathutil"
)

// Result holds summary statistics of a copy operation.
type Result struct {
	FilesCopied int
	DirsCreated int
}

// Copy walks src (respecting .nestifyignore via IgnoreMatcher) and
// streams every non-ignored file/directory into dst.
// src is expected to be the current working directory of the user.
// dst is the destination path provided via --path.
func Copy(src, dst string) (*Result, error) {
	absSrc, err := filepath.Abs(src)
	if err != nil {
		return nil, fmt.Errorf("resolve source path: %w", err)
	}
	absDst, err := filepath.Abs(dst)
	if err != nil {
		return nil, fmt.Errorf("resolve destination path: %w", err)
	}

	if absSrc == absDst {
		return nil, fmt.Errorf("source and destination are the same path: %s", absSrc)
	}

	// Prevent copying a tree into one of its own subdirectories.
	sep := string(os.PathSeparator)
	if strings.HasPrefix(absDst+sep, absSrc+sep) {
		return nil, fmt.Errorf("destination is inside source; this would cause recursive copy")
	}

	matcher, err := ignore.NewIgnoreMatcher(absSrc)
	if err != nil {
		return nil, fmt.Errorf("load ignore rules: %w", err)
	}

	if err := os.MkdirAll(absDst, 0755); err != nil {
		return nil, fmt.Errorf("create destination root: %w", err)
	}

	result := &Result{}
	err = walkAndCopy(absSrc, absSrc, absDst, matcher, result)
	if err != nil {
		return result, err
	}
	return result, nil
}

func walkAndCopy(currentPath, rootSrc, rootDst string, matcher *ignore.IgnoreMatcher, result *Result) error {
	entries, err := os.ReadDir(currentPath)
	if err != nil {
		return err
	}

	for _, entry := range entries {
		entryName := entry.Name()
		fullSrc := filepath.Join(currentPath, entryName)

		relPath, err := filepath.Rel(rootSrc, fullSrc)
		if err != nil {
			return err
		}
		standardRel := pathutil.ToStandardPath(relPath)

		if matcher != nil {
			if matcher.ShouldIgnore(entryName, entry.IsDir()) || matcher.ShouldIgnore(standardRel, entry.IsDir()) {
				continue
			}
		}

		fullDst := filepath.Join(rootDst, relPath)

		if entry.IsDir() {
			if err := os.MkdirAll(fullDst, 0755); err != nil {
				return fmt.Errorf("create directory %s: %w", fullDst, err)
			}
			result.DirsCreated++
			if err := walkAndCopy(fullSrc, rootSrc, rootDst, matcher, result); err != nil {
				return err
			}
			continue
		}

		if err := copyFile(fullSrc, fullDst); err != nil {
			return fmt.Errorf("copy file %s: %w", standardRel, err)
		}
		result.FilesCopied++
	}

	return nil
}

func copyFile(src, dst string) error {
	in, err := os.Open(src)
	if err != nil {
		return err
	}
	defer in.Close()

	// Ensure parent directory exists (defensive; usually already created).
	if err := os.MkdirAll(filepath.Dir(dst), 0755); err != nil {
		return err
	}

	out, err := os.Create(dst)
	if err != nil {
		return err
	}
	defer out.Close()

	if _, err := io.Copy(out, in); err != nil {
		return err
	}

	// Best-effort preserve permission bits (ignore error on platforms that restrict it).
	if info, err := os.Stat(src); err == nil {
		_ = os.Chmod(dst, info.Mode())
	}

	return nil
}
