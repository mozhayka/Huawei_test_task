using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    internal enum Intersect
    {
        Inside,
        Outside,
        Intersect,
    }

    internal class Intersection
    {
        public static Intersect SegmentsIntersect(double segment1_l, double segment1_r, double segment2_l, double segment2_r)
        {
            if (segment2_r < segment1_l || segment1_r < segment2_l)
                return Intersect.Outside;
            if (segment1_l <= segment2_l && segment2_r <= segment1_r)
                return Intersect.Inside;
            return Intersect.Intersect;
        }
    }
}
