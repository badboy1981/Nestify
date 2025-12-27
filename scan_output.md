```
.
└── ExternalMemoryAI
    ├── ExternalMemoryAI.sln
    ├── docs
    │   ├── Architecture.md
    │   ├── PRD.md
    │   ├── PRDBase.md
    │   ├── Roadmap.md
    │   ├── scan_output.json
    │   └── scan_output.md
    ├── full_setup.bat
    ├── models
    │   ├── LLaMA
    │   └── MiniLM
    ├── src
    │   ├── AiMemory.Application
    │   │   ├── Agents
    │   │   │   ├── Export
    │   │   │   ├── Filtering
    │   │   │   ├── Indexing
    │   │   │   ├── Ingestion
    │   │   │   ├── Retrieval
    │   │   │   └── Versioning
    │   │   ├── AiMemory.Application.csproj
    │   │   ├── Class1.cs
    │   │   ├── Commands
    │   │   │   └── CommandPalette
    │   │   └── Services
    │   │       ├── ITopicService.cs
    │   │       └── TopicService.cs
    │   ├── AiMemory.Core
    │   │   ├── AiMemory.Core.csproj
    │   │   ├── Class1.cs
    │   │   ├── Entities
    │   │   │   ├── Conversation.cs
    │   │   │   ├── Message.cs
    │   │   │   └── TopicNode.cs
    │   │   ├── Enums
    │   │   │   └── MessageRole.cs
    │   │   ├── Interfaces
    │   │   │   ├── IAI
    │   │   │   ├── IPersistence
    │   │   │   ├── ITopicRepository.cs
    │   │   │   └── IVectorStore
    │   │   └── ValueObjects
    │   ├── AiMemory.Infrastructure
    │   │   ├── AI-Inference
    │   │   ├── AiMemory.Infrastructure.csproj
    │   │   ├── AiMemory.db
    │   │   ├── Class1.cs
    │   │   ├── DependencyInjection.cs
    │   │   ├── Persistence
    │   │   │   ├── Migration
    │   │   │   │   ├── Scripts
    │   │   │   │   └── Services
    │   │   │   ├── MongoDB
    │   │   │   └── SQLite
    │   │   │       ├── AppDbContext.cs
    │   │   │       ├── Migrations
    │   │   │       │   ├── 20251227152823_InitialCreate.Designer.cs
    │   │   │       │   ├── 20251227152823_InitialCreate.cs
    │   │   │       │   └── AppDbContextModelSnapshot.cs
    │   │   │       └── SQLiteTopicRepository.cs
    │   │   └── VectorStores
    │   │       ├── FAISS
    │   │       ├── Qdrant
    │   │       └── SqliteVSS
    │   └── AiMemory.UI
    │       ├── AiMemory.UI.csproj
    │       ├── App.xaml
    │       ├── App.xaml.cs
    │       ├── Components
    │       │   ├── ChatViewer
    │       │   ├── CommandPalette
    │       │   ├── Layout
    │       │   │   ├── MainLayout.razor
    │       │   │   ├── MainLayout.razor.css
    │       │   │   ├── NavMenu.razor
    │       │   │   └── NavMenu.razor.css
    │       │   ├── Pages
    │       │   │   ├── Counter.razor
    │       │   │   ├── Home.razor
    │       │   │   ├── NotFound.razor
    │       │   │   └── Weather.razor
    │       │   ├── Routes.razor
    │       │   ├── Shared
    │       │   │   └── CommandPalette.razor
    │       │   ├── TopicTree
    │       │   └── _Imports.razor
    │       ├── MainPage.xaml
    │       ├── MainPage.xaml.cs
    │       ├── MauiProgram.cs
    │       ├── Pages
    │       ├── Platforms
    │       │   ├── Android
    │       │   │   ├── AndroidManifest.xml
    │       │   │   ├── MainActivity.cs
    │       │   │   ├── MainApplication.cs
    │       │   │   └── Resources
    │       │   │       └── values
    │       │   │           └── colors.xml
    │       │   ├── MacCatalyst
    │       │   │   ├── AppDelegate.cs
    │       │   │   ├── Entitlements.plist
    │       │   │   ├── Info.plist
    │       │   │   └── Program.cs
    │       │   ├── Windows
    │       │   │   ├── App.xaml
    │       │   │   ├── App.xaml.cs
    │       │   │   ├── Package.appxmanifest
    │       │   │   └── app.manifest
    │       │   └── iOS
    │       │       ├── AppDelegate.cs
    │       │       ├── Info.plist
    │       │       ├── Program.cs
    │       │       └── Resources
    │       │           └── PrivacyInfo.xcprivacy
    │       ├── Properties
    │       │   └── launchSettings.json
    │       ├── Resources
    │       │   ├── AppIcon
    │       │   │   ├── appicon.svg
    │       │   │   └── appiconfg.svg
    │       │   ├── Fonts
    │       │   │   └── OpenSans-Regular.ttf
    │       │   ├── Images
    │       │   │   └── dotnet_bot.svg
    │       │   ├── Raw
    │       │   │   └── AboutAssets.txt
    │       │   └── Splash
    │       │       └── splash.svg
    │       ├── Shared
    │       ├── appsettings.json
    │       └── wwwroot
    │           ├── app.css
    │           ├── index.html
    │           └── lib
    │               └── bootstrap
    │                   └── dist
    │                       └── css
    │                           ├── bootstrap.min.css
    │                           └── bootstrap.min.css.map
    └── tests
        ├── AiMemory.Application.Tests
        │   ├── AiMemory.Application.Tests.csproj
        │   ├── TestResults
        │   └── UnitTest1.cs
        ├── AiMemory.Core.Tests
        │   ├── AiMemory.Core.Tests.csproj
        │   ├── TestResults
        │   └── UnitTest1.cs
        ├── AiMemory.Infrastructure.Tests
        │   ├── AiMemory.Infrastructure.Tests.csproj
        │   ├── TestResults
        │   └── UnitTest1.cs
        ├── AiMemory.Tests.Unit
        │   ├── AiMemory.Tests.Unit.csproj
        │   ├── TopicServiceTests.cs
        │   └── UnitTest1.cs
        ├── AiMemory.UI.Tests
        │   ├── AiMemory.UI.Tests.csproj
        │   ├── TestResults
        │   └── UnitTest1.cs
        └── Integration.Tests
            ├── Integration.Tests.csproj
            ├── TestResults
            └── UnitTest1.cs

```
