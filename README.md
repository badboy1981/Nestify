Nestify
Nestify یک ابزار خط فرمان (CLI) است که به توسعه‌دهندگان کمک می‌کند تا ساختار پوشه‌های پروژه‌هایشان را به‌صورت خودکار اسکن، تحلیل و ایجاد کنند. این ابزار با Go نوشته شده و برای پروژه‌های مختلف (مثل وب، بازی‌های Unity، یا بک‌اند) مناسب است. با Nestify می‌توانید ساختار پروژه‌ها را به‌صورت JSON یا Markdown ببینید، اسکلت پروژه را تحلیل کنید، یا با استفاده از قالب‌های آماده، ساختارهای جدید بسازید.
ویژگی‌ها

اسکن پروژه: ساختار پوشه‌ها و فایل‌ها را اسکن کرده و به‌صورت JSON یا Markdown نمایش می‌دهد.
اسکن فقط پوشه‌ها: فقط دایرکتوری‌ها را اسکن کنید تا نقشه کلی پروژه را ببینید.
تحلیل اسکلت پروژه: نقش هر پوشه (مثل هسته کد، تست‌ها، یا منابع) را به‌صورت خودکار تشخیص می‌دهد.
ایجاد ساختار: با استفاده از قالب‌های JSON، ساختار پوشه‌ها و فایل‌ها را در پروژه‌تان ایجاد کنید.

پیش‌نیازها

Go نسخه 1.16 یا بالاتر
نصب پکیج خارجی: github.com/xlab/treeprint

نصب

مخزن را کلون کنید:git clone https://github.com/badboy1981/Nestify.git


به پوشه پروژه بروید:cd Nestify


پروژه را بیلد کنید:go build -o nestify ./cmd/nestify


فایل اجرایی nestify را به PATH اضافه کنید (اختیاری):mv nestify /usr/local/bin/



دستورات CLI
Nestify از سه ساب‌کامند اصلی پشتیبانی می‌کند: init, scan, و analyze.
1. init - ایجاد ساختار پروژه
با استفاده از یک فایل JSON قالب، ساختار پوشه‌ها و فایل‌ها را در مسیر مشخص‌شده ایجاد می‌کند.
استفاده:
./nestify init --template <فایل-قالب> --path <مسیر-پروژه>

مثال:
./nestify init --template config/structure.json --path ./myproject

این دستور ساختار تعریف‌شده در structure.json را در پوشه myproject ایجاد می‌کند.
گزینه‌ها:

--template: مسیر فایل JSON قالب (پیش‌فرض: template.json)
--path: مسیر مقصد برای ایجاد ساختار (پیش‌فرض: .)

2. scan - اسکن ساختار پروژه
پوشه‌ها و فایل‌های پروژه را اسکن کرده و خروجی را به‌صورت JSON ذخیره می‌کند. همچنین می‌تواند ساختار درختی را در ترمینال یا فایل Markdown نمایش دهد.
استفاده:
./nestify scan --path <مسیر-پروژه> [--tree] [--folders-only]

مثال:
./nestify scan --path . --tree --folders-only

این دستور فقط پوشه‌های پروژه فعلی را اسکن کرده، ساختار درختی را در ترمینال نمایش می‌دهد و در scan_output.md ذخیره می‌کند.
گزینه‌ها:

--path: مسیر پروژه برای اسکن (پیش‌فرض: .)
--tree: نمایش ساختار درختی و ذخیره در scan_output.md
--folders-only: فقط پوشه‌ها را اسکن کن

خروجی:

فایل scan_output.json: ساختار کامل به‌صورت JSON
فایل scan_output.md: ساختار درختی (اگر --tree فعال باشد)

3. analyze - تحلیل اسکلت پروژه
نقشه کلی پروژه (فقط پوشه‌ها) را تحلیل کرده و نقش هر پوشه را مشخص می‌کند (مثل "نقطه ورود برنامه" یا "تنظیمات").
استفاده:
./nestify analyze --path <مسیر-پروژه>

مثال:
./nestify analyze --path .

این دستور اسکلت پروژه را تحلیل کرده، گزارش را در ترمینال نمایش می‌دهد و در skeleton_report.md ذخیره می‌کند.
گزینه‌ها:

--path: مسیر پروژه برای تحلیل (پیش‌فرض: .)

خروجی:

فایل skeleton_report.md: گزارش اسکلت پروژه با نقش‌های تخمینی

ساختار پروژه
Nestify
├── .gitattributes
├── .gitignore
├── LICENSE
├── NestifyDiagram.json
├── README.md
├── cmd
│   └── nestify
│       └── main.go
├── config
│   └── structure.json
├── go.mod
├── go.sum
├── internal
│   ├── Cli
│   │   ├── cli.go
│   │   ├── init.go
│   │   └── scan.go
│   ├── analyzer
│   │   └── analyzer.go
│   ├── generator
│   │   └── generator.go
│   ├── scanner
│   │   └── scanner.go
│   ├── treeprinter
│   │   └── treeprinter.go
│   ├── types
│   │   └── type.go
├── nestify
├── scan_output.json
├── scan_output.md
└── skeleton_report.md

مثال فایل قالب (structure.json)
{
  "projectType": "web",
  "language": "go",
  "tags": ["backend", "api"],
  "root": [
    {
      "name": "src",
      "type": "folder",
      "children": [
        {
          "name": "main.go",
          "type": "file"
        }
      ]
    }
  ]
}

توسعه‌دهندگان

بدست آمده توسط: badboy1981

مشارکت
اگر ایده‌ای برای بهبود دارید، لطفاً issue باز کنید یا pull request بفرستید!
لایسنس
این پروژه تحت لایسنس MIT منتشر شده است. جزئیات در فایل LICENSE.