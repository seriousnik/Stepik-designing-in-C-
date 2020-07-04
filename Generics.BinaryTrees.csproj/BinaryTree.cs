using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Generics.BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        public T Value { get; set; }
        public T First() => Value;
        public BinaryTree<T> Left { get; set; }
        public BinaryTree<T> Right { get; set; }

        private bool isValueNull;
        public void Add(T parameter)
        {

            if (!isValueNull)
            {
                Value = parameter;
                isValueNull = true;
                return;
            }

            if (parameter.CompareTo(Value) <= 0)
            {
                if (Left == null)
                    Left = new BinaryTree<T>(parameter);
                else
                    Left.Add(parameter);

            }
            else 
            {
                if (Right == null)
                    Right = new BinaryTree<T>(parameter);
                else
                    Right.Add(parameter);
            }

        }

        public BinaryTree(T item)
            {
                Value = item;
                isValueNull = true;
            }

        public BinaryTree() { }
        public IEnumerator<T> GetEnumerator()
        {
            if (!isValueNull) yield break;

            if (Left != null)
            {
                foreach (var v in Left)
                {
                    yield return v;
                }
            }

            yield return Value;

            if (Right != null)
            {
                foreach (var v in Right)
                {
                    yield return v;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class BinaryTree
    {
        public static BinaryTree<T> Create<T>(params T[] parameters) where T : IComparable<T>
        {
            var binaryTree = new BinaryTree<T>();          
            foreach (T e in parameters)
                binaryTree.Add(e);
            return binaryTree;
        }
    }
}

