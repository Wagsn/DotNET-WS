using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WS.NET.Extensions
{
    /// <summary>
    /// 点扩展
    /// </summary>
    public static class PointExtensions
    {
        public static double Distance(this Point point1, Point point2, Func<int, int, int, int, double> distance)
        {
            return distance(point1.X, point1.Y, point2.X, point2.Y);
        }
        public static double Distance(this Point point1, Point point2)
        {
            return Distance(point1, point2, (p1x, p1y, p2x, p2y) => Math.Sqrt((p1x - p2x) * (p1x - p2x) + (p1y - p2y) * (p1y - p2y)));
        }
    }
}
