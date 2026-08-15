package cli

import (
	"fmt"
	"path"
	"strings"

	"github.com/badboy1981/Nestify/internal/ignore"
)

// List available prompt templates from the embedded templates-prompts folder.
func runPromptListCmd() {
	list, err := ignore.ListAvailableTemplatesFromFS(templatesFS, "templates-prompts")
	if err != nil {
		fmt.Println("❌ Failed to read prompt templates:", err)
		return
	}

	fmt.Println("📋 Available built-in prompt templates:")
	for _, name := range list {
		fmt.Printf("  - %s\n", name)
	}
	fmt.Println("\nUse with the context command:")
	fmt.Println("  nestify context -p <template_name_or_text>")
	fmt.Println("Show prompt text in the terminal:")
	fmt.Println("  nestify prompt <template_name>")
}

// Print the full text of a specific prompt template to the terminal.
func runPromptShowCmd(templateName string) {
	sourcePath := path.Join("templates-prompts", templateName+".txt")

	data, err := templatesFS.ReadFile(sourcePath)
	if err != nil {
		fmt.Printf("❌ Prompt '%s' not found.\n", templateName)
		fmt.Println("To list prompts: nestify prompt-list")
		return
	}

	fmt.Printf("📄 Prompt [%s]:\n", templateName)
	fmt.Println("--------------------------------------------------")
	fmt.Println(string(data))
	fmt.Println("--------------------------------------------------")
}

// Resolve prompt text for injection into the context report.
func getPromptContent(promptInput string) string {
	promptInput = strings.TrimSpace(promptInput)
	if promptInput == "" {
		return ""
	}

	// Treat bare -p / -p default as the default template.
	targetTemplate := promptInput
	if promptInput == "true" || promptInput == "default" {
		targetTemplate = "default"
	}

	// First try to load an embedded template with this name.
	sourcePath := path.Join("templates-prompts", targetTemplate+".txt")
	data, err := templatesFS.ReadFile(sourcePath)
	if err == nil {
		return string(data)
	}

	// If no template matches, treat the input as custom prompt text.
	return promptInput
}
