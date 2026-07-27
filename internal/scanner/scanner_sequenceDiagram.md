Proceeding with diagram generation...

# Generate Mermaid Diagram From Code

This command analyzes the coding files you have selected, understands each component, its flow, connections, and relationships, and then maps everything into a comprehensive Mermaid diagram.

Use this to quickly visualize the structure and logic of your code, making it easier to understand, document, and share.

---

📄 Analyzing: `internal/scanner/scanner.go`

📊 Generating: **Sequence Diagram**



Fetching Mermaid documentation for sequenceDiagram.md
Completed with input: {
  "file": "sequenceDiagram.md"
}

```mermaid
sequenceDiagram
    participant User
    participant Scanner
    participant PathUtil
    participant Ignore
    participant FileSystem

    User->>Scanner: Scan(path, foldersOnly)
    activate Scanner
    activate PathUtil
    activate Ignore
    activate FileSystem

    Scanner->>PathUtil: NormalizeForOS(path)
    PathUtil-->>Scanner: osPath

    Scanner->>FileSystem: Stat(osPath)
    alt os.Stat fails
        FileSystem-->>Scanner: err
        Scanner-->>User: return error
    else os.Stat succeeds
        FileSystem-->>Scanner: info
        Scanner->>PathUtil: ToStandardPath(osPath)
        PathUtil-->>Scanner: standardRoot

        Scanner->>Ignore: NewIgnoreMatcher(standardRoot)
        alt ignore initialization fails
            Ignore-->>Scanner: err
            Scanner-->>User: return error
        else ignore initialization succeeds
            Ignore-->>Scanner: matcher

            Scanner->>Scanner: scanDir(standardRoot, standardRoot, matcher, foldersOnly)
            Note over Scanner: Recursive directory traversal begins

            Scanner->>FileSystem: ReadDir(currentPath)
            FileSystem-->>Scanner: entries list

            loop For each directory entry
                Scanner->>Scanner: check foldersOnly and entry.IsDir()
                alt skip non-folder entry
                    Scanner-->>Scanner: continue
                else process entry
                    Scanner->>PathUtil: ToStandardPath(relPath)
                    PathUtil-->>Scanner: standardRel

                    opt matcher is present
                        Scanner->>Ignore: ShouldIgnore(entryName, isDir)
                        Ignore-->>Scanner: ignoreDecision1
                        alt ignored by entry name
                            Scanner-->>Scanner: continue
                        else not ignored by entry name
                            Scanner->>Ignore: ShouldIgnore(standardRel, isDir)
                            Ignore-->>Scanner: ignoreDecision2
                            alt ignored by relative path
                                Scanner-->>Scanner: continue
                            else not ignored
                                Scanner->>FileSystem: entry.Info()
                                FileSystem-->>Scanner: entry info

                                alt directory entry
                                    Scanner->>Scanner: scanDir(fullPath, rootPath, matcher, foldersOnly)
                                    Scanner-->>Scanner: children nodes
                                else file entry
                                    Scanner-->>Scanner: create file node
                                end

                                Scanner-->>Scanner: append node to nodes
                            end
                        end
                    end
                end
            end

            Scanner-->>User: return root node list
        end
    end

    deactivate FileSystem
    deactivate Ignore
    deactivate PathUtil
    deactivate Scanner
```

Open Diagram in Editor