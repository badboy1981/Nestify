package cli

import (
	"fmt"
	"os"
)

func RunCli() {
	if len(os.Args) == 1 {
		ShowHelp()
		return
	}

	arg := os.Args[1]

	switch arg {
	case "--help", "-h":
		ShowHelp()
	case "--version", "version":
		ShowVersion()
	case "init":
		runInitCmd()
	case "scan":
		runScanCmd()
	case "analyze":
		runAnalyzeCmd()
	case "ignore-list":
		runIgnoreListCmd()
	case "context":
		runContextCmd()
	case "ignore-use":
		if len(os.Args) < 3 {
			fmt.Println("❌ لطفا نام تمپلیت را وارد کنید. مثال: nestify ignore-use go")
			return
		}
		runIgnoreUseCmd(os.Args[2])
	case "prompt-list":
		runPromptListCmd()
	case "prompt":
		if len(os.Args) < 3 {
			fmt.Println("❌ لطفا نام پرامپت را وارد کنید. مثال: nestify prompt architecture")
			return
		}
		runPromptShowCmd(os.Args[2])
	default:
		fmt.Printf("❌ ساب‌کامند نامعتبر: %s\n", arg)
		fmt.Println("برای راهنمایی بیشتر: nestify --help")
	}
}
