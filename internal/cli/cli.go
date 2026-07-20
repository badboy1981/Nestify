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
		runInitCmd() // این تابع در فایل init.go تعریف شده است
	case "scan":
		runScanCmd() // این تابع در فایل scan.go تعریف شده است
	case "analyze":
		runAnalyzeCmd() // این تابع در فایل cli.go (پایین‌تر) یا فایل خودش تعریف می‌شود
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
	default:
		fmt.Printf("❌ ساب‌کامند نامعتبر: %s\n", arg)
		fmt.Println("برای راهنمایی بیشتر: nestify --help")
	}
}

// فقط این مورد را اینجا نگه دار چون فایل جداگانه برای آنالیز در پوشه CLI نداری
// func runAnalyzeCmd() {
// 	// اگر بعدا فایل analyze.go را در این پوشه ساختی، این را هم به آنجا منتقل کن
// 	fmt.Println("🔍 در حال آنالیز پروژه...")
// 	// فراخوانی متد اصلی آنالیزور
// }
