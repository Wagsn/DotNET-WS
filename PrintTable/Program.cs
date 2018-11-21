using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintTable
{
    class Program
    {
        static void Main(string[] args)
        {
            // 自适应二维表的创建
            // 表行数
            int tbRowCount = 0;
            // 表列数
            int tbColCount = 0;
            // 表列宽
            int tbColWidth = 0;
            // 表行高  暂时一字符高度
            int tbRowHeight = 0;
            // 显示宽度，包含制表符
            int width = tbColCount * tbColWidth + (tbColCount + 1) * 2;
            // 显示高度，包含制表符
            int height = tbRowCount * tbRowHeight + (tbRowCount + 1) * 2;
            int area = width * height;
            // 等高等宽表格
            Console.WriteLine("等高等宽表格");
            Console.WriteLine(Int2DMatToTable(new int[][]
            {
                new int[]{1521541, 1651, 4511 },
                new int[]{42452, 7552, 82542 },
                new int[]{5432752, 1827, 95475277 }
            }, 3, 3));

            int[][] intMat2D = new int[][]
            {
                new int[]{15221, 1851, 155 },
                new int[]{422, 722, 825525 },
                new int[]{27, 52, 45425515 }
            };
            string[][] strMat2D = new string[][]
            {
                new string[]{"tojson", "tojson [objName]", "将对象转换成JSON字符串"},
                new string[]{"test", "test [express]", "测试表达式"},
                new string[]{"now", "now [dateformat]", "显示当前时间"}
            };
            object[][] mat2D = IntToObject(intMat2D);
            // 等高变宽表格
            Console.WriteLine("等高变宽表格");
            Console.WriteLine(Mat2DToTable(strMat2D));

            Console.Read();
        }

        /// <summary>
        /// 二维矩阵转换成表格
        /// </summary>
        /// <param name="mat2D"></param>
        /// <returns></returns>
        static string Mat2DToTable(object[][] mat2D)
        {
            string result = "";
            int i =0, j=0;
            int[] widths = ColMaxWidthOfMat2D(mat2D);

            result += TableLine(0, widths) + "\r\n";  //打印表头线  
            //打印表格内容  
            for (i = 0; i < mat2D.Length; i++)
            {
                if (i > 0) result += TableLine(1, widths) + "\r\n";  //打印表中线  
                result += "│";  //行首  
                for (j = 0; j < mat2D[i].Length; j++)
                {
                    string str = mat2D[i][j].ToString();
                    result += NormalStr(str, widths[j]);
                    if (j < mat2D[i].Length - 1) result += "│";  //表中竖线  
                }
                result += "│" + "\r\n";  //行尾  
            }
            result += TableLine(2, widths);  //打印表尾线  
            return result;
        }

        /// <summary>
        /// 打印表格、无用方法
        /// </summary>
        static void PrintTable()
        {
            const int ROWS = 3;
            const int COLS = 3;
            int[,] arr = new int[ROWS, COLS] {
                {0,1,2},
                {3,4,5},
                {6,7,8}
            };

            for (int i = 0, r = 0; i < (ROWS * 2 + 1); ++i)
            {
                if (i == 0)
                {
                    Console.Write("┌");
                    for (int j = 0; j < COLS; ++j)
                    {
                        if (j < COLS - 1)
                            Console.Write("─┬");
                        else
                            Console.Write("─┐");
                    }
                }
                else if (i == ROWS * 2)
                {
                    Console.Write("└");

                    for (int j = 0; j < COLS; ++j)
                    {
                        if (j < COLS - 1)
                            Console.Write("─┴");
                        else
                            Console.Write("─┘");
                    }
                }
                else if (i % 2 == 0)
                {
                    Console.Write("├");
                    for (int j = 0; j < COLS; ++j)
                    {
                        if (j < COLS - 1)
                            Console.Write("─┼");
                        else
                            Console.Write("─┤");
                    }
                }
                else
                {
                    Console.Write("│");
                    for (int j = 0; j < COLS; ++j)
                    {
                        Console.Write(" {0}│", arr[r, j]);
                    }
                    ++r;
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// 打印等宽表格线
        /// location 表格线的位置， 0 表头线，1 表中线，2 表尾线
        /// colCount 表格的列数 
        /// colWidth 表格的列宽  
        /// </summary>
        /// <param name="location">表格线的位置， 0 表头线，1 表中线，2 表尾线</param>
        /// <param name="colCount">表格的列数</param>
        /// <param name="colWidth">表格的列宽</param>
        /// <returns></returns>
        static string TableLine(int location, int colCount, int colWidth)
        {
            string result = "";
            string[] lineHead = { "┌", "├", "└" };  // 行首
            string[] lineMid1 = { "─", "─", "─" };  // 字中
            string[] lineMid2 = { "┬", "┼", "┴" };  // 字间
            string[] lineTail = { "┐", "┤", "┘" };  // 行尾
            result += lineHead[location]; //行首  
            for (int i = 0; i < colCount; i++)
            {
                for (int j = 0; j < colWidth; j++)
                {
                    result += lineMid1[location];
                }
                if (i < colCount - 1) result += lineMid2[location];
            }
            result += lineTail[location];//行尾  
            return result;
        }

        /// <summary>
        /// 生成表格线字符串
        /// </summary>
        /// <param name="location">表头0|表中1|表尾2</param>
        /// <param name="colWidths"></param>
        /// <returns>表格线字符串</returns>
        static string TableLine(int location, int[] colWidths)
        {
            string result = "";
            string[] lineHead = { "┌", "├", "└" };  // 行首
            string[] lineMid1 = { "─", "─", "─" };  // 格中
            string[] lineMid2 = { "┬", "┼", "┴" };  // 格间
            string[] lineTail = { "┐", "┤", "┘" };  // 行尾
            result += lineHead[location]; //行首
            for (int i = 0; i < colWidths.Length; i++)
            {
                for (int j = 0; j < colWidths[i]; j++)
                {
                    result += lineMid1[location];
                }
                if (i < colWidths.Length - 1) result += lineMid2[location];
            }
            result += lineTail[location]; //行尾
            return result;
        }

        /// <summary>
        /// 获取二维矩阵的最大列宽组（取每列最宽）
        /// </summary>
        /// <param name="oss"></param>
        /// <returns></returns>
        static int[] ColMaxWidthOfMat2D(object[][] oss)
        {
            // 列宽一维矩阵
            int[] widths = new int[oss.Count()==0?0:oss[0].Count()];
            for(int i=0; i<oss.Count(); i++)
            {
                object[] os = oss[i];
                for(int j= 0; j < os.Count(); j++)
                {
                    int tmp = DisplayWidth(os[j].ToString());
                    widths[j] = widths[j] > tmp ? widths[j] : tmp;
                }
            }
            return widths;
        }
        
        /// <summary>
        /// 获取指定二维数组中的最大显示宽度
        /// </summary>
        /// <param name="intMat2D">二维数组名</param>
        /// <param name="rowCount">行数</param>
        /// <param name="colCount">列数</param>
        /// <returns></returns>
        static int MaxWidthOfIntMat2D(int[][] intMat2D, int rowCount, int colCount)
        {
            int width = 0;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    int c = 1;
                    int temp = intMat2D[i][j];
                    while (temp >= 10)
                    {
                        temp /= 10;
                        c++;
                    }
                    width = width < c ? c : width;
                }
            }
            return width;
        }

        /// <summary>
        /// 获取二维对象矩阵的每格的宽高规格矩阵
        /// int[0][j]:宽数据  int[1][i]: 高数据 --表示i行j列的宽高数据
        /// </summary>
        /// <param name="oss"></param>
        /// <returns></returns>
        static int[][] CeilMaxOfMat2D(object[][] oss)
        {
            return null;
        }

        /// <summary>
        /// 计算显示长度，考虑制表符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static int DisplayWidth(string str)
        {
            int lengthCount = 0;
            var splits = str.ToCharArray();
            for (int i = 0; i < splits.Length; i++)
            {
                if (splits[i] == '\t')
                {
                    lengthCount += 8 - lengthCount % 8;
                }
                else
                {
                    lengthCount += Encoding.GetEncoding("GBK").GetByteCount(splits[i].ToString());
                }
            }
            return lengthCount;
        }

        /// <summary>
        /// 将整形二维矩阵转换成对象二维矩阵
        /// </summary>
        /// <param name="intMat2D">整形二维矩阵</param>
        /// <returns>对象二维矩阵</returns>
        static object[][] IntToObject(int[][] intMat2D)
        {
            object[][] oss = new object[intMat2D.Length][];
            for (int i = 0; i < intMat2D.Length; i++)
            {
                oss[i] = new object[intMat2D[i].Length];
                for (int j = 0; j < intMat2D[i].Length; j++)
                {
                    oss[i][j] = intMat2D[i][j];
                }
            }
            return oss;
        }

        /// <summary>
        /// 功能：将二维数组打印成表格样式  
        /// 参数：intMat2D 二维数组名，rowCount 行数，colCount 列数
        /// </summary>
        /// <param name="intMat2D">二维数组名</param>
        /// <param name="rowCount">行数</param>
        /// <param name="colCount">列数</param>
        /// <returns></returns>
        static string Int2DMatToTable(int[][] intMat2D, int rowCount, int colCount)
        {
            //string tablines = "┌┬┐├┼┤└┴┘─│" ;
            string result = "";
            int i, j, colWidth;

            colWidth = MaxWidthOfIntMat2D(intMat2D, rowCount, colCount);  //获取所有数据中的最大宽度  
            result += TableLine(0, colCount, colWidth) + "\r\n";  //打印表头线  

            //打印表格内容  
            for (i = 0; i < rowCount; i++)
            {
                if (i > 0) result += TableLine(1, colCount, colWidth) + "\r\n";  //打印表中线  
                result += "│";  //行首  
                for (j = 0; j < colCount; j++)
                {
                    int num = intMat2D[i][j];
                    result += NormalStr(string.Concat(num), colWidth);
                    if (j < colCount - 1) result += "│";  //表中竖线  
                }
                result += "│" + "\r\n";  //行尾  
            }
            result += TableLine(2, colCount, colWidth);  //打印表尾  
            return result;
        }

        /// <summary>
        /// 归一化字符串为指定长度，字符串比长度短右填充空格，比长度长则截断
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string NormalStr(string str, int length)
        {
            int strLen = str == null ? 0 : DisplayWidth(str);
            if (strLen > length)
            {
                return str.Substring(0, length);
            }
            else if (strLen == length)
            {
                return str;
            }
            else
            {
                return str + SpaceOf(length - strLen);
            }
        }
        
        /// <summary>
        /// 指定长度的空格字符串
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        static string SpaceOf (int num)
        {
            string result = "";
            for(int i=0; i< num; i++)
            {
                result += " ";
            }
            return result;
        }

        ///// <summary>
        ///// 比较表达式，大于等于小于依次返回三个同类型结果
        ///// </summary>
        ///// <typeparam name="TResult"></typeparam>
        ///// <param name="left"></param>
        ///// <param name="right"></param>
        ///// <param name="great"></param>
        ///// <param name="equal"></param>
        ///// <param name="less"></param>
        ///// <returns></returns>
        //static TResult CompareExpression<TResult>(int left, int right, TResult great, TResult equal, TResult less)
        //{
        //    if (left > right)
        //    {
        //        return great;
        //    }
        //    else if (left == right)
        //    {
        //        return equal;
        //    }
        //    else
        //    {
        //        return less;
        //    }
        //}
    }
}
