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
            if (elem.RightBotPoint.X < window.LeftTopPoint.X || window.RightBotPoint.X < elem.LeftTopPoint.X)
                return Visibility_X.Invisible;
            if (window.LeftTopPoint.X < elem.LeftTopPoint.X && elem.RightBotPoint.X < window.RightBotPoint.X)
                return Visibility_X.Visible;
            return Visibility_X.Partially;
        }

        public static Visibility_Y IsVisibleByY(UIElement elem, Viewport window)
        {
            if (elem.RightBotPoint.Y > window.LeftTopPoint.Y || window.RightBotPoint.Y > elem.LeftTopPoint.Y)
                return Visibility_Y.Invisible;
            if (window.LeftTopPoint.Y > elem.LeftTopPoint.Y && elem.RightBotPoint.Y > window.RightBotPoint.Y)
                return Visibility_Y.Visible;
            return Visibility_Y.Partially;
        }

        public static Visibility_ IsVisible(UIElement elem, Viewport window)
        {
            var x_visibility = IsVisibleByX(elem, window);
            var y_visibility = IsVisibleByY(elem, window);
            return VisibilityMerger.MergeVisibility(x_visibility, y_visibility);

        }
    }
}
