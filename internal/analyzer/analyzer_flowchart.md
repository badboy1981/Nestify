Proceeding with diagram generation...

# Generate Mermaid Diagram From Code

This command analyzes the coding files you have selected, understands each component, its flow, connections, and relationships, and then maps everything into a comprehensive Mermaid diagram.

Use this to quickly visualize the structure and logic of your code, making it easier to understand, document, and share.

---

📄 Analyzing: `internal/analyzer/analyzer.go`

📊 Generating: **Flowchart**



Fetching Mermaid documentation for flowchart.md
Completed with input: {
  "file": "flowchart.md"
}

```mermaid
flowchart TD
    A["Start AnalyzeSkeleton"] --> B["Initialize extMap and totals"]
    B --> C["Iterate over root nodes"]
    C --> D{"node.Type == 'folder'?"}
    D -->|Yes| E["Increment totalFolders"]
    E --> F["Iterate node.Children"]
    F --> G["processNode(child)"]
    G --> D
    D -->|No| H["Increment totalFiles and totalBytes"]
    H --> I["Normalize extension"]
    I --> J["Call detectLanguageName(ext, node.Name)"]
    J --> K{"lang exists in extMap?"}
    K -->|Yes| L["Increment language stat count and bytes"]
    K -->|No| M["Create new LanguageStat entry"]
    L --> N["Continue node loop"]
    M --> N
    N --> C
    C --> O["Build stats slice from extMap"]
    O --> P{"totalBytes > 0?"}
    P -->|Yes| Q["Calculate each stat percentage"]
    P -->|No| R["Leave percentages at zero"]
    Q --> S["Sort stats by TotalBytes descending"]
    R --> S
    S --> T["Render report text and JSON summary"]
    T --> U["Return analysis report string"]
    J -.-> V["detectLanguageName: map extension to language"]
    T -.-> W["makeProgressBar: render percent bar per language"]
```

Open Diagram in Editor