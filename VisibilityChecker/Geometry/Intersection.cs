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
        public static Intersect SegmentsIntersect(double parent_l, double parent_r, double child_l, double child_r)
        {
            if (child_r < parent_l || parent_r < child_l)
                return Intersect.Outside;
            if (parent_l <= child_l && child_r <= parent_r)
                return Intersect.Inside;
            return Intersect.Intersect;
        }
    }
}
