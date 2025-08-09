package treeprinter

import (
	"fmt"

	"github.com/badboy1981/Nestify/internal/types"
	"github.com/xlab/treeprint"
)

// PrintTree ساختار درختی رو به صورت زیبا روی کنسول چاپ می‌کنه
func PrintTree(root *types.Node) {
	tree := treeprint.New()
	buildTree(tree, root)
	fmt.Println(tree.String())
}

func buildTree(branch treeprint.Tree, node *types.Node) {
	if len(node.Children) > 0 {
		sub := branch.AddBranch(node.Name)
		for i := range node.Children {
			buildTree(sub, &node.Children[i])
		}
	} else {
		branch.AddNode(node.Name)
	}
}
