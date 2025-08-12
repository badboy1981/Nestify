package treeprinter

import (
	"github.com/badboy1981/Nestify/internal/types"
	"github.com/xlab/treeprint"
)

func PrintTree(node *types.Node) {
	tree := treeprint.New()
	addNode(tree, node)
	println(tree.String())
}

// تابع جدید برای برگرداندن رشته درخت
func GetTreeString(node *types.Node) string {
	tree := treeprint.New()
	addNode(tree, node)
	return tree.String()
}

func addNode(branch treeprint.Tree, node *types.Node) {
	newBranch := branch.AddBranch(node.Name)
	for _, child := range node.Children {
		addNode(newBranch, &child)
	}
}
