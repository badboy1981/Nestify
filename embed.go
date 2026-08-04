package nestify

import "embed"

//go:embed templates-ignore templates-projects templates-prompts
var RootTemplatesFS embed.FS
