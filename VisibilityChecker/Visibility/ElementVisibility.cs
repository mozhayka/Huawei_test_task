using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    internal class ElementVisibility
    {
        public static Visibility_X IsVisibleByX(UIElement elem, UIViewport window)
        {
            var intersectoin = Intersection.SegmentsIntersect(window.Left, window.Right, elem.Left, elem.Right);
            return intersectoin switch
            {
                Intersect.Inside => Visibility_X.Visible,
                Intersect.Outside => Visibility_X.Invisible,
                Intersect.Intersect => Visibility_X.Partially,
                _ => throw new NotImplementedException("Unknown intersection"),
            };
        }

        public static Visibility_Y IsVisibleByY(UIElement elem, UIViewport window)
        {
            var intersectoin = Intersection.SegmentsIntersect(window.Bottom, window.Top, elem.Bottom, elem.Top);
            return intersectoin switch
            {
                Intersect.Inside => Visibility_Y.Visible,
                Intersect.Outside => Visibility_Y.Invisible,
                Intersect.Intersect => Visibility_Y.Partially,
                _ => throw new NotImplementedException("Unknown intersection"),
            };
        }

        public static Visibility_ IsVisible(UIElement elem, UIViewport window)
        {
            var x_visibility = IsVisibleByX(elem, window);
            var y_visibility = IsVisibleByY(elem, window);
            return VisibilityMerger.MergeVisibility(x_visibility, y_visibility);

        }
    }
}
