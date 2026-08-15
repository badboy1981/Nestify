package types

// File: type.go

type Node struct {
	Name     string `json:"name"`
	Type     string `json:"type"`              // "folder" or "file"
	Content  string `json:"content,omitempty"` // file content only
	Role     string `json:"role,omitempty"`    // estimated role (for analysis)
	Size     int64  `json:"size,omitempty"`
	Children []Node `json:"children,omitempty"` // nested folders or files
}

type Template struct {
	ProjectType string   `json:"projectType,omitempty"`
	Language    string   `json:"language,omitempty"`
	Tags        []string `json:"tags,omitempty"`
	Root        []Node   `json:"root"`
}
