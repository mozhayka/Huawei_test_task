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

        internal bool IsInside(UIRectangle sub)
        {
            return IsInside(this, sub);
        }

        internal static bool IsInside(UIRectangle parent, UIRectangle sub)
        {
            return Intersect(parent, sub) == RectanglesIntersection.Inside;
        }

        internal RectanglesIntersection Intersect(UIRectangle sub)
        {
            return Intersect(this, sub);
        }

        internal static RectanglesIntersection Intersect(UIRectangle parent, UIRectangle sub)
        {
            return Intersection.MergeIntersections(HorizontalIntersect(parent, sub), VerticalIntersect(parent, sub));
        }

        internal static SegmentsIntersection HorizontalIntersect(UIRectangle parent, UIRectangle sub)
        {
            return Intersection.IntersectSegments(parent.Left, parent.Right, sub.Left, sub.Right);
        }

        internal static SegmentsIntersection VerticalIntersect(UIRectangle parent, UIRectangle sub)
        {
            return Intersection.IntersectSegments(parent.Bottom, parent.Top, sub.Bottom, sub.Top);
        }
    }
}
