namespace VisibilityChecker
{
    public enum Visibility
    {
        Visible,
        Partially,
        Invisible,
    }

    internal class IntersectionVisibilityConverter
    {
        public static Visibility FromIntersection(RectanglesIntersection intersection)
        {
            return intersection switch
            {
                RectanglesIntersection.Inside => Visibility.Visible,
                RectanglesIntersection.Outside => Visibility.Invisible,
                _ => Visibility.Partially,
            };
        }

        public static RectanglesIntersection FromVisibility(Visibility intersection)
        {
            return intersection switch
            {
                Visibility.Visible => RectanglesIntersection.Inside,
                Visibility.Invisible => RectanglesIntersection.Outside,
                _ => RectanglesIntersection.Intersect,
            };
        }
    }
}
