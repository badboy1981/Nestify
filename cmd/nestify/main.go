package main

import (
	nestify "github.com/badboy1981/Nestify"
	cli "github.com/badboy1981/Nestify/internal/cli"
)

func main() {
	// ارسال سیستم‌فایل ایمبد شده از ریشه به پکیج cli
	cli.SetTemplatesFS(nestify.RootTemplatesFS)
	cli.RunCli()
}
