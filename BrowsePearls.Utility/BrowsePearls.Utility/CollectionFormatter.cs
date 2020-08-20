using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace BrowsePearls.Utility.Formatting
{
    public class CollectionFormatter
    {
        //一维数组
        public static string Format<TItem>(IEnumerable<TItem> collection)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            foreach (TItem item in collection)
            {
                sb.Append(item.ToString());
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(" }");
            return sb.ToString();
        }

        // 二维数组 （多维数组的的内存模型是所有元素在一行连续内存地址上）
        public static string Format<TItem>(TItem[,] array)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append(Environment.NewLine);
            for (int i = 0; i <= array.GetUpperBound(0); ++i)
            {
                sb.Append("  ");
                sb.Append("{ ");
                for (int j = 0; j <= array.GetUpperBound(1); ++j)
                {
                    sb.Append(array[i, j]);
                    sb.Append(", ");
                }
                sb.Remove(sb.Length - 2, 2);
                sb.Append(" }");
                sb.Append(Environment.NewLine);
            }
            sb.Append("}");
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        // 锯齿数组 (锯齿数组的维度始终是1)
        public static string Format<TItem>(TItem[][] array)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append(Environment.NewLine);
            for (int i = 0; i < array.Length; ++i)
            {
                sb.Append("  ");
                sb.Append(Format(array[i]));
                sb.Append(Environment.NewLine);
            }
            sb.Append("}");
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }
    }
}
