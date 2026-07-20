package nestify

import "embed"

//go:embed templates-ignore templates-projects
var RootTemplatesFS embed.FS
