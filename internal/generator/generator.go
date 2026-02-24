package generator

import (
	"os"
	"path/filepath"

	"github.com/badboy1981/Nestify/internal/types"
)

// CreateStructure به صورت بازگشتی پوشه‌ها و فایل‌ها را ایجاد می‌کند
func CreateStructure(node types.Node, root string) error {
	path := filepath.Join(root, node.Name)

	if node.Type == "folder" {
		// ایجاد پوشه
		if err := os.MkdirAll(path, 0755); err != nil {
			return err
		}
		// ایجاد زیرمجموعه‌ها
		for _, child := range node.Children {
			if err := CreateStructure(child, path); err != nil {
				return err
			}
		}
	} else {
		// ایجاد فایل با محتوا
		// اگر node.Content خالی باشد، یک فایل خالی ساخته می‌شود
		data := []byte(node.Content)
		if err := os.WriteFile(path, data, 0644); err != nil {
			return err
		}
	}
	return nil
}
