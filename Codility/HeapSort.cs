using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public static class HeapSort
    {
        public static void Sort<T>(List<T> input, IComparer<T> comparer)
        {
            BTree<T> tree = new BTree<T>(input, comparer);
            input = tree.Sort(); 
        }

        private class BTree<T>
        {
            List<List<TreeNode<T>>> _levels = new List<List<TreeNode<T>>>(); 

            public IComparer<T> Comparer {get; private set;}

            public int Levels { get; private set; }

            public TreeNode<T> Root { get; private set; }


            public BTree(List<T> input, IComparer<T> comparer)
            {
                this.Comparer = comparer;
                this.CreateFromList(input);
            }

            public List<T> Sort()
            {
                while (this.MaxHeapify()) { }

                List<T> output = new List<T>(); 

                while (this._levels.Count > 1)
                {
                    T newRoot = this.GetAndRemoveLastChild();
                    output.Add(this.Root.Value); 

                    this.Root.Value = newRoot;
                    this.Root.BubbleDown(this.Comparer); 
                }

                output.Add(this.Root.Value); 

                return output; 
            }


            private bool MaxHeapify()
            {
                bool output = false;
                for (int n = _levels.Count - 1; n >= 0; n--)
                {
                    var level = _levels[n];
                    for (int i = level.Count - 1; i >= 0; i--)
                    {
                        if (level[i].BubbleUp(this.Comparer))
                        {
                            level[i].BubbleUp(this.Comparer); 
                            output = true;
                        }
                    }
                }

                return output; 
            }

            private void CreateFromList(List<T> input)
            {
                int level = 0; 
                int index = 0; 
                
                while(index < input.Count)
                {
                    if (level == 0)
                    {
                        this.Root = new TreeNode<T> { Value = input[index++] };
                        _levels.Add(new List<TreeNode<T>>());
                        _levels[level].Add(this.Root);
                        level++;
                    }

                    else
                    {
                        if (_levels.Count < level + 1)
                            _levels.Add(new List<TreeNode<T>>()); 

                        foreach (var node in _levels[level - 1])
                        {
                            node.AddLeftChild(input[index++]);
                            _levels[level].Add(node.LeftChild); 
                            if (index == input.Count)
                                break;

                            node.AddRightChild(input[index++]);
                            _levels[level].Add(node.RightChild); 
                            if (index == input.Count)
                                break;
                        }

                        level++; 
                    }
                }
            }

            private T RemoveLastChild()
            {
                var rootValue = this.Root.Value;

                this.Root.Value = this.GetAndRemoveLastChild();
                this.Root.BubbleDown(this.Comparer); 

                return rootValue;
            }

            private T GetAndRemoveLastChild()
            {
                T output = default(T); 

                if (_levels.Count > 0)
                {
                    var list = _levels[_levels.Count - 1];
                    if (list.Count > 0)
                    {
                        var node = list[list.Count - 1];
                        list.RemoveAt(list.Count - 1);
                        output = node.Value;
                        node.Remove(); 
                    }

                    if (list.Count == 0)
                        _levels.RemoveAt(_levels.Count - 1); 
                }

                return output; 
            }
        }

        private class TreeNode<T>
        {
            public T Value;
            public TreeNode<T> Parent; 
            public TreeNode<T> LeftChild;
            public TreeNode<T> RightChild;

            public bool HasChildren
            {
                get { return LeftChild != null || RightChild != null; }
            }

            public TreeNode<T> NodeToTheLeft
            {
                get{
                    if (this.Parent != null)
                    {
                        if (this.Parent.LeftChild != this)
                            return this.Parent.LeftChild; 
                        else{
                            var parentLeft = this.Parent.NodeToTheLeft;
                            if (parentLeft != null)
                                return parentLeft.RightChild;
                        }
                    }

                    return null;
                }
            }

            public bool BubbleUp(IComparer<T> comparer)
            {
                bool output = false; 
                if (this.Parent != null)
                {
                    if (comparer.Compare(this.Value, this.Parent.Value) > 0)
                    {
                        this.SwapWithParent();
                        output = true; 
                        this.Parent.BubbleUp(comparer);
                    }
                }

                return output; 
            }

            public void BubbleDown(IComparer<T> comparer)
            {
                if (this.HasChildren)
                {
                    bool compareWithLeft = false; 

                    if (this.LeftChild != null && this.RightChild != null)
                    {
                        if (comparer.Compare(this.LeftChild.Value, this.RightChild.Value) > 0)
                            compareWithLeft = true; 
                    }
                    else
                    {
                        compareWithLeft = this.LeftChild != null; 
                    }

                    if (compareWithLeft)
                    {
                        if (comparer.Compare(this.LeftChild.Value, this.Value) > 0)
                        {
                            this.LeftChild.SwapWithParent(); 
                            this.LeftChild.BubbleDown(comparer); 
                        }
                    }
                    else
                    {
                        if (comparer.Compare(this.RightChild.Value, this.Value) > 0)
                        {
                            this.RightChild.SwapWithParent(); 
                            this.RightChild.BubbleDown(comparer); 
                        }
                    }
                }
            }

            public void SwapWithParent()
            {
                if (this.Parent != null)
                {
                    T temp = this.Parent.Value;
                    this.Parent.Value = this.Value; 
                    this.Value = temp; 
                }
            }

            public void Remove()
            {
                if (this.Parent != null)
                {
                    if (this.Parent.LeftChild == this)
                        this.Parent.LeftChild = null;

                    if (this.Parent.RightChild == this)
                        this.Parent.RightChild = null;
                }
            }

            public void AddLeftChild(T value)
            {
                var newNode = new TreeNode<T> { Value = value };
                this.LeftChild = newNode;
                newNode.Parent = this;
            }

            public void AddRightChild(T value)
            {
                var newNode = new TreeNode<T> { Value = value };
                this.RightChild = newNode;
                newNode.Parent = this;
            }

            public override string ToString()
            {
                return this.Value.ToString();
            }
        }
    }
}
