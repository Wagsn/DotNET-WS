using System.Linq;

namespace WS.Core
{
    public static class Util
    {
        /// <summary>
        /// 获取两个字符串的相似度
        /// 原理：本次所用到的相似度计算公式是 相似度=Kq*q/(Kq*q+Kr*r+Ks*s) (Kq > 0 , Kr>=0,Ka>=0) 其中，q是字符串1和字符串2中都存在的单词的总数，s是字符串1中存在，字符串2中不存在的单词总数，r是字符串2中存在，字符串1中不存在的单词总数.Kq,Kr和ka分别是q,r,s的权重，根据实际的计算情况，我们设Kq=2，Kr=Ks=1.
        /// 来源：http://www.cnblogs.com/lcq529/archive/2018/03/21/8618287.html
        /// </summary>
        /// <param name=”sourceString”>第一个字符串</param>
        /// <param name=”str”>第二个字符串</param>
        /// <returns>相似度为小数</returns>
        public static decimal GetSimilarityWith(this string sourceString, string str)
        {
            decimal Kq = 2;
            decimal Kr = 1;
            decimal Ks = 1;

            char[] ss = sourceString.ToCharArray();
            char[] st = str.ToCharArray();

            //获取交集数量
            int q = ss.Intersect(st).Count();
            int s = ss.Length - q;
            int r = st.Length - q;

            return Kq * q / (Kq * q + Kr * r + Ks * s);
        }
    }
}