package cli

import (
	"embed"
	"fmt"
	"os"
	"path"

	"github.com/badboy1981/Nestify/internal/ignore"
)

var templatesFS embed.FS

func SetTemplatesFS(fs embed.FS) {
	templatesFS = fs
}

func runIgnoreListCmd() {
	// Read available templates directly from the embedded templates-ignore folder.
	list, err := ignore.ListAvailableTemplatesFromFS(templatesFS, "templates-ignore")
	if err != nil {
		fmt.Println("❌ Failed to read ignore templates:", err)
		return
	}

	fmt.Println("🚫 Available ignore templates:")
	for _, name := range list {
		fmt.Printf("  - %s\n", name)
	}
	fmt.Println("\nUsage: nestify ignore-use <name>")
}

func runIgnoreUseCmd(templateName string) {
	// Path of the template inside the embedded templates-ignore folder.
	sourcePath := path.Join("templates-ignore", templateName+".txt")
	destPath := ".nestifyignore"

	data, err := templatesFS.ReadFile(sourcePath)
	if err != nil {
		fmt.Printf("❌ Template '%s' not found.\n", templateName)
		return
	}

	err = os.WriteFile(destPath, data, 0644)
	if err != nil {
		fmt.Println("❌ Failed to create .nestifyignore:", err)
		return
	}

	fmt.Printf("✅ .nestifyignore created using template '%s'.\n", templateName)
}
