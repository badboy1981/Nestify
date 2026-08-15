package cli

import (
	"flag"
	"fmt"
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/copier"
	"github.com/badboy1981/Nestify/internal/pathutil"
)

func runCopyCmd() {
	cmd := flag.NewFlagSet("copy", flag.ExitOnError)
	path := cmd.String("path", "", "Destination path for the clean project copy")

	cmd.Parse(os.Args[2:])

	if *path == "" || *path == "." {
		fmt.Println("❌ Please specify a destination path with --path.")
		fmt.Println("Example: nestify copy --path ../clean-project")
		return
	}

	src, err := os.Getwd()
	if err != nil {
		fmt.Printf("❌ Failed to resolve current directory: %v\n", err)
		return
	}

	dst := pathutil.NormalizeForOS(*path)

	fmt.Printf("📂 Source : %s\n", src)
	fmt.Printf("📂 Target : %s\n", dst)
	fmt.Println("⏳ Copying (respecting .nestifyignore)...")

	result, err := copier.Copy(src, dst)
	if err != nil {
		fmt.Printf("❌ Copy error: %v\n", err)
		return
	}

	absDst, _ := filepath.Abs(dst)
	fmt.Println("\n✅ Clean project copied successfully!")
	fmt.Printf("   📁 Dirs  : %d\n", result.DirsCreated)
	fmt.Printf("   📄 Files : %d\n", result.FilesCopied)
	fmt.Printf("   📍 Path  : %s\n", absDst)
}
