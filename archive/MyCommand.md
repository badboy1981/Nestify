


./nestify init --template config/structure.json --path ./myproject
./nestify scan --path . --tree --folders-only



./nestify init --template config/ExternalMemoryAIGemini.json --path G:/Programming/AiMemory/Gemini
./nestify scan --path G:/Programming/AiMemory/Gemini --tree --folders-only

./nestify init --template config/ExternalMemoryAICopilot.json --path G:/Programming/AiMemory/Copilot
./nestify init --template "templates\projects\go_basic.json" --path "G:\Test"
./nestify init --template templates/projects/go_basic.json --path G:\Test
./nestify scan --path G:/Programming/AiMemory/Copilot/ExternalMemoryAI --tree --folders-only

./nestify scan --path G:/Programming/AiMemory --tree --folders-only

G:/Programming/Customer/Javid/ConsoleStore
./nestify scan --path g:/Programming/Customer/Javid/ConsoleStore --tree --folders-only
./nestify scan --path . --tree --folders-only


./nestify init --template config/templates-ignore.json --path .


G:/Programming/AiMemory/Copilot/ExternalMemoryAI
G:/Programming/AiMemory/Test
G:\Programming\Customer\Javid\ConsoleStore
G:\Test

# ۱. همگام‌سازی پکیج‌ها
go mod tidy

# ۲. اطمینان از وجود پکیج ایگنور
go get github.com/monochromegane/go-gitignore

# ۳. پاک کردن فایل اجرایی قدیمی
rm -f nestify.exe

# ۴. ساخت مجدد با کدهای اصلاح شده
go build -o nestify.exe ./cmd/nestify