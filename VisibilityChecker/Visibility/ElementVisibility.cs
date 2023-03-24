using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    internal class ElementVisibility
    {
        public static Visibility_X IsVisibleByX(UIElement elem, Viewport window)
        {
            var segment1_l = window.LeftBotPoint.X;
            var segment1_r = window.RightTopPoint.X;
            var segment2_l = elem.LeftBotPoint.X;
            var segment2_r = elem.RightTopPoint.X;
            var intersectoin = Intersection.SegmentsIntersect(segment1_l, segment1_r, segment2_l, segment2_r);
            return intersectoin switch
            {
                Intersect.Inside => Visibility_X.Visible,
                Intersect.Outside => Visibility_X.Invisible,
                Intersect.Intersect => Visibility_X.Partially,
                _ => throw new NotImplementedException("Unknown intersection"),
            };
        }

        public static Visibility_Y IsVisibleByY(UIElement elem, Viewport window)
        {
            var segment1_l = window.LeftBotPoint.Y;
            var segment1_r = window.RightTopPoint.Y;
            var segment2_l = elem.LeftBotPoint.Y;
            var segment2_r = elem.RightTopPoint.Y;
            var intersectoin = Intersection.SegmentsIntersect(segment1_l, segment1_r, segment2_l, segment2_r);
            return intersectoin switch
            {
                Intersect.Inside => Visibility_Y.Visible,
                Intersect.Outside => Visibility_Y.Invisible,
                Intersect.Intersect => Visibility_Y.Partially,
                _ => throw new NotImplementedException("Unknown intersection"),
            };
        }

        public static Visibility_ IsVisible(UIElement elem, Viewport window)
        {
            var x_visibility = IsVisibleByX(elem, window);
            var y_visibility = IsVisibleByY(elem, window);
            return VisibilityMerger.MergeVisibility(x_visibility, y_visibility);

        }
    }
}
