using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensiveLibraries
{
    namespace DataStructure
    {
        public class BinaryTree<TItem> where TItem : IComparable<TItem>
        {
            public TItem Node { get; set; }
            private uint nodeCount;
            public uint NodeCount
            {
                get { return this.nodeCount; }
            }
            public BinaryTree<TItem> Left { get; set; }
            public BinaryTree<TItem> Right { get; set; }
            public BinaryTree(TItem _node)
            {
                this.Node = _node;
                this.Left = null;
                this.Right = null;
                this.nodeCount = 1;
            }
            public void Insert(TItem newItem)
            {
                this.nodeCount++;
                TItem currentNode = this.Node;
                if (currentNode.CompareTo(newItem) > 0)
                {
                    if (this.Left == null)
                    {
                        this.Left = new BinaryTree<TItem>(newItem);
                    }
                    else
                    {
                        this.Left.Insert(newItem);
                    }
                }
                else
                {
                    if (this.Right == null)
                    {
                        this.Right = new BinaryTree<TItem>(newItem);
                    }
                    else
                    {
                        this.Right.Insert(newItem);
                    }
                }
            }
            public string TraverseToString()
            {
                string res = "";
                if (this.Left != null)
                {
                    res = this.Left.TraverseToString();
                }
                res += $"{this.Node.ToString()} ";
                if (this.Right != null)
                {
                    res += this.Right.TraverseToString();
                }
                return res;
            }
            public TItem[] Traverse()
            {
                TItem[] res = null;
                if (this.Left != null)
                {
                    res = this.Left.Traverse();
                }
                if (res == null)
                {
                    res = new TItem[1] { this.Node };
                }
                else
                {
                    TItem[] tmp = new TItem[res.Length + 1];
                    res.CopyTo(tmp, 0);
                    tmp[res.Length] = this.Node;
                    res = tmp;
                }
                if (this.Right != null)
                {
                    TItem[] tmp = this.Right.Traverse();
                    TItem[] cntr = new TItem[res.Length + tmp.Length];
                    res.CopyTo(cntr, 0);
                    tmp.CopyTo(cntr, res.Length);
                    res = cntr;
                }
                return res;
            }
            public bool HasNode(TItem queryNode)
            {
                if (queryNode == null) return false;
                if (queryNode.Equals(this.Node)) return true;
                if ((queryNode.CompareTo(this.Node) < 0) && (this.Left != null))
                {
                    return this.Left.HasNode(queryNode);
                }
                else if ((queryNode.CompareTo(this.Node) >= 0) && (this.Right != null))
                {
                    return this.Right.HasNode(queryNode);
                }
                return false;
            }
            public BinaryTree<TItem> Remove(TItem targetNode) //应当捕捉KeyNotFoundException
            {
                if (!this.HasNode(targetNode))
                {
                    throw new KeyNotFoundException($"{targetNode.ToString()} not found in the tree.");
                }
                TItem[] arrItems = this.Traverse();
                uint preCount = (uint)arrItems.Length;
                uint lower = 0, upper = preCount - 1;
                uint searchIndex = (lower + upper) / 2;
                while ((!arrItems[searchIndex].Equals(targetNode)) && (lower < upper))
                {
                    if (targetNode.CompareTo(arrItems[searchIndex]) < 0)
                    {
                        upper = searchIndex;
                        searchIndex = (lower + upper) / 2;
                    }
                    else
                    {
                        lower = searchIndex + 1;
                        searchIndex = (lower + upper) / 2;
                    }
                }
                if (preCount > 1)
                {
                    for (uint i = searchIndex; i < preCount - 1; i++) arrItems[i] = arrItems[i + 1];
                    BinaryTree<TItem> res = new BinaryTree<TItem>(arrItems[(preCount - 2) / 2]);
                    res.nodeCount = preCount - 1;
                    if ((preCount - 2) / 2 > 0) res.Left = binaryInsert(ref arrItems, 0, (preCount - 2) / 2 - 1);
                    if ((preCount - 2) / 2 < preCount - 2) res.Right = binaryInsert(ref arrItems, (preCount - 2) / 2 + 1, preCount - 2);
                    return res;
                }
                else
                {
                    return null;
                }
            }
            private static BinaryTree<TItem> binaryInsert(ref TItem[] itemList, uint lower, uint upper)
            {
                uint currentMid = (lower + upper) / 2;
                BinaryTree<TItem> res = new BinaryTree<TItem>(itemList[currentMid]);
                res.nodeCount = upper - lower + 1;
                if (lower >= upper) return res;
                if (lower < currentMid) res.Left = binaryInsert(ref itemList, lower, currentMid - 1);
                if (currentMid < upper) res.Right = binaryInsert(ref itemList, currentMid + 1, upper);
                return res;

            }

        }
    }
}
