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
            if (elem.RightTopPoint.X < window.LeftBotPoint.X || window.RightTopPoint.X < elem.LeftBotPoint.X)
                return Visibility_X.Invisible;
            if (window.LeftBotPoint.X <= elem.RightTopPoint.X && elem.LeftBotPoint.X <= window.RightTopPoint.X)
                return Visibility_X.Visible;
            return Visibility_X.Partially;
        }

        public static Visibility_Y IsVisibleByY(UIElement elem, Viewport window)
        {
            if (elem.RightTopPoint.Y < window.LeftBotPoint.Y || window.RightTopPoint.Y < elem.LeftBotPoint.Y)
                return Visibility_Y.Invisible;
            if (window.LeftBotPoint.Y <= elem.RightTopPoint.Y && elem.LeftBotPoint.Y <= window.RightTopPoint.Y)
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
