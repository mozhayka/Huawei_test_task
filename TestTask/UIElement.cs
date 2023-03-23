using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class UIElement
    {
        private readonly List<UIElement> subelements;
        public Point LeftTopPoint { get; set; }
        public Point RightBotPoint { get; set; }

        public UIElement(Point leftTopPoint, Point rightBotPoint)
        {
            LeftTopPoint = leftTopPoint;
            RightBotPoint = rightBotPoint;
            subelements = new List<UIElement>();
        }

        public UIElement(Point leftTopPoint, double width, double height)
            : this(leftTopPoint, new Point { X = leftTopPoint.X + width, Y = leftTopPoint.Y - height })
        { }
        
        public UIElement(double leftTopPointX, double leftTopPointY, double width, double height)
            : this(new Point { X = leftTopPointX, Y = leftTopPointY }, width, height)
        { }

        public void AddSubelement(UIElement sub)
        {
            subelements.Add(sub);
        }

        public void RemoveSubelement(int place)
        {
            subelements.RemoveAt(place);
        }

        public List<UIElement> GetSubelements()
        {
            return subelements;
        }

        public Visibility_X IsVisibleByX(double leftPoint, double rightPoint)
        {
            if (RightBotPoint.X < leftPoint || rightPoint < LeftTopPoint.X)
                return Visibility_X.Invisible;
            if (leftPoint < LeftTopPoint.X  && RightBotPoint.X < rightPoint)
                return Visibility_X.Visible;
            return Visibility_X.Partially;
        }

        public Visibility_X IsVisibleByX(Point topLeft, Point botRight)
        {
            return IsVisibleByX(topLeft.X, botRight.X);
        }

        public Visibility_Y IsVisibleByY(double topPoint, double botPoint)
        {
            if (RightBotPoint.Y > topPoint || botPoint > LeftTopPoint.Y)
                return Visibility_Y.Invisible;
            if (topPoint > LeftTopPoint.Y && RightBotPoint.Y > botPoint)
                return Visibility_Y.Visible;
            return Visibility_Y.Partially;
        }

        public Visibility_Y IsVisibleByY(Point topLeft, Point botRight)
        {
            return IsVisibleByY(topLeft.Y, botRight.Y);
        }

        public Visibility IsVisible(Point topLeft, Point botRight)
        {
            var x_visibility = IsVisibleByX(topLeft, botRight);
            var y_visibility = IsVisibleByY(topLeft, botRight);
            return VisibilityMerger.MergeVisibility(x_visibility, y_visibility);
        }
    }
}
