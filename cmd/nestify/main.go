package main

import (
	nestify "github.com/badboy1981/Nestify"
	cli "github.com/badboy1981/Nestify/internal/cli"
)

func main() {
	// Pass the embedded root template filesystem to the cli package.
	cli.SetTemplatesFS(nestify.RootTemplatesFS)
	cli.RunCli()
}
