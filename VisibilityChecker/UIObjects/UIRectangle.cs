using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public class UIRectangle
    {
        public double Left { get; protected set; }
        public double Right { get; protected set; }
        public double Top { get; protected set; }
        public double Bottom { get; protected set; }

        public UIRectangle(double left, double right, double top, double bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public bool IsInside(UIRectangle sub)
        {
            return IsInside(this, sub);
        }

        public static bool IsInside(UIRectangle parent, UIRectangle sub)
        {
            var isInsideByX = Intersection.SegmentsIntersect(parent.Left, parent.Right, sub.Left, sub.Right) == Intersect.Inside;
            var isInsideByY = Intersection.SegmentsIntersect(parent.Bottom, parent.Top, sub.Bottom, sub.Top) == Intersect.Inside;
            return isInsideByX && isInsideByY;
        }
    }
}
