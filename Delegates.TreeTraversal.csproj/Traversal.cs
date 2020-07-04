//using System;
//using System.CodeDom;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Delegates.TreeTraversal
//{
//    //public class Traversal<TTree, TValue>

//    public static class Traversal
//    {


//        public static IEnumerable<T> GetBinaryTreeValues<T>(BinaryTree<T> binaryTree) 
//        {
//            return AlgBinary(new List<T>(), binaryTree);
//        }

//        private static List<T> AlgBinary<T>(List<T> newList, BinaryTree<T> binaryTree)
//        {
//            newList.Add(binaryTree.Value);
//            if (binaryTree.Left != null)
//                AlgBinary(newList, binaryTree.Left);
//            if (binaryTree.Right != null)
//                AlgBinary(newList, binaryTree.Right);
//            return newList;

//        }

//        public static IEnumerable<Job> GetEndJobs(Job job)
//        {
//            if (job.Subjobs.Count == 0)
//            {
//                var a = new List<Job>();
//                a.Add(job);
//                return a;

//            }
//            return AlgJobs(new List<Job>(), job.Subjobs);
//        }

//        private static List<Job> AlgJobs(List<Job> jobListNew, List<Job> jobList)
//        {
//            foreach (var job in jobList)
//            {
//                if (job.Subjobs.Count > 0)
//                    AlgJobs(jobListNew, job.Subjobs);
//                else
//                    jobListNew.Add(job);
//            }
//            return jobListNew;

//        }

//        public static List<Product> GetProducts(ProductCategory productCategory) 
//        {
//            var listProducts = new List<Product>();
//            listProducts.AddRange(productCategory.Products);
//            return AlgProd(listProducts, productCategory.Categories);
//        }

//        private static List<Product> AlgProd(List<Product> newListProduct, List<ProductCategory> listCategory)
//        {
//            foreach (var productCategory in listCategory)
//            {
//                if (productCategory.Categories.Count > 0)
//                    AlgProd(newListProduct, productCategory.Categories);
//                else
//                    newListProduct.AddRange(productCategory.Products);
//            }
//            return newListProduct;
//        }

//    }
//}

////Вам нужно написать один алгоритм обхода дерева, который бы принимал в качестве аргументов делегаты, объясняющие алгоритму, как обходить дерево и какие величины выводить.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.TreeTraversal
{
    public static class Traversal
    {
        private static IEnumerable<T> Get<T>(T data, Func<T, bool> predicate, Func<T, IList<T>> childs)
        {
            {
                if (data == null)
                    yield break;

                if (predicate(data))
                    yield return data;

                foreach (var child in childs(data))
                    foreach (var item in Get(child, predicate, childs))
                        yield return item;
            }
        }

        public static IEnumerable<int> GetBinaryTreeValues(BinaryTree<int> data)
        {
            return Get(data,
                       d => true,
                       d => new List<BinaryTree<int>>() { d.Left, d.Right }
                    ).Select(d => d.Value);
        }

        public static IEnumerable<Job> GetEndJobs(Job data)
        {
            return Get(data,
                       d => d.Subjobs.Count == 0,
                       d => d.Subjobs
                    );
        }

        public static IEnumerable<Product> GetProducts(ProductCategory data)
        {
            return Get(data,
                       d => true,
                       d => d.Categories
                    ).SelectMany(d => d.Products);
        }
    }
}
