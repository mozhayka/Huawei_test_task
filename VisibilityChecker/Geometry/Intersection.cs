namespace VisibilityChecker
{
    internal enum SegmentsIntersection
    {
        Inside,
        Outside,
        Intersect,
    }

    internal enum RectanglesIntersection
    {
        Inside,
        Outside,
        Intersect,
    }

    internal class Intersection
    {
        public static SegmentsIntersection IntersectSegments(double parent_l, double parent_r, double child_l, double child_r)
        {
            if (child_r < parent_l || parent_r < child_l)
                return SegmentsIntersection.Outside;
            if (parent_l <= child_l && child_r <= parent_r)
                return SegmentsIntersection.Inside;
            return SegmentsIntersection.Intersect;
        }

        public static RectanglesIntersection MergeIntersections(SegmentsIntersection x, SegmentsIntersection y)
        {
            if (x == SegmentsIntersection.Inside && y == SegmentsIntersection.Inside)
                return RectanglesIntersection.Inside;
            if (x == SegmentsIntersection.Outside || y == SegmentsIntersection.Outside)
                return RectanglesIntersection.Outside;
            return RectanglesIntersection.Intersect;
        }
    }
}
