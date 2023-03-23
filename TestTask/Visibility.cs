using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    enum Visibility_X
    {
        Visible,
        Partially,
        Invisible,
    }

    enum Visibility_Y
    {
        Visible,
        Partially,
        Invisible,
    }

    enum Visibility
    {
        Visible,
        Partially,
        Invisible,
    }

    static class VisibilityMerger
    {
        static public Visibility MergeVisibility(Visibility_X x, Visibility_Y y)
        {
            if (x == Visibility_X.Visible && y == Visibility_Y.Visible)
                return Visibility.Visible;
            if (x == Visibility_X.Invisible || y == Visibility_Y.Invisible)
                return Visibility.Invisible;
            return Visibility.Partially;
        }
    }

}
