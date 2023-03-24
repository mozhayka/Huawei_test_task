using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VisibilityChecker
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public override string ToString()
        {
            return $"Point ({X}, {Y})";
        }

        public static bool operator >=(Point LeftTop, Point RightBot)
        {
            return LeftTop.X <= RightBot.X && LeftTop.Y >= RightBot.Y;
        }

        public static bool operator <=(Point RightBot, Point LeftTop)
        {
            return LeftTop.X <= RightBot.X && LeftTop.Y >= RightBot.Y;
        }
    }
}
