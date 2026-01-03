package types

// File: type.go

type Node struct {
	Name     string `json:"name"`
	Type     string `json:"type"`              // "folder" or "file"
	Content  string `json:"content,omitempty"` // فقط برای فایل‌ها
	Role     string `json:"role,omitempty"`    // نقش تخمینی (برای تحلیل)
	Size     int64  `json:"size,omitempty"`
	Children []Node `json:"children,omitempty"` // زیرپوشه‌ها یا فایل‌ها
}

type Template struct {
	ProjectType string   `json:"projectType,omitempty"`
	Language    string   `json:"language,omitempty"`
	Tags        []string `json:"tags,omitempty"`
	Root        []Node   `json:"root"`
}
