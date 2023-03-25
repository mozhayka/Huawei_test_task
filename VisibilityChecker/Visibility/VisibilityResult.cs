using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public class VisibilityResult
    {
        public List<int> VisibleIds { get; set; } = new();
        public List<int> PartiallyIds { get; set; } = new();
        public List<int> InvisibleIds { get; set; } = new();

        public VisibilityResult()
        {
        }

        internal void Clear()
        {
            VisibleIds.Clear();
            PartiallyIds.Clear();
            InvisibleIds.Clear();
        }

        internal void Add(int Id, RectanglesIntersection intersection)
        {
            switch (intersection)
            {
                case RectanglesIntersection.Inside:
                    VisibleIds.Add(Id);
                    break;
                case RectanglesIntersection.Intersect:
                    PartiallyIds.Add(Id);
                    break;
                case RectanglesIntersection.Outside:
                    InvisibleIds.Add(Id);
                    break;
            }
        }
    }
}
