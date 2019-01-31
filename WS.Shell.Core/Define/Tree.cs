using Newtonsoft.Json;
using System.Collections.Generic;
using WS.Text;

namespace WS.Shell.Define
{
    /// <summary>
    /// 树形结构
    /// </summary>
    public class Tree<D>
    {
        TreeNode<D> Root { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="data"></param>
        public static Tree<D> Init(D data)
        {
            return new Tree<D>
            {
                Root = new TreeNode<D>
                {
                    Data = data
                }
            };
        }

        public Tree<D> Append(D data)
        {
            Root.Children.Add(new TreeNode<D>
            {
                Data = data
            });
            return this;
        }

        /// <summary>
        /// 变成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ""+Root.ToString();
        }
    }

    /// <summary>
    /// 树节点
    /// </summary>
    /// <typeparam name="D"></typeparam>
    public class TreeNode<D>
    {
        public D Data { get; set; }
        public TreeNode<D> Parent { get; set; }
        public List<TreeNode<D>> Children { get; set; }

        //public static TreeNode

        public override string ToString()
        {
            string result = $"{{data: {Data.ToString()}, children: [";

            for(int i =0; i< Children.Count; i++)
            {
                result += $"{{data: {Children[i].ToString()}}}";
                if(i< Children.Count - 1)
                {
                    result += ", ";
                }
            }

            result += "]}";
            return result;
            //return JsonUtil.ToJson(this);
        }
    }
}
