using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public class Viewport
    {
        public double Left, Right, Bottom, Top;

        //public Viewport(Point leftBotPoint, Point rightTopPoint)
        //{
        //    LeftBotPoint = leftBotPoint;
        //    RightTopPoint = rightTopPoint;
        //}

        //public Viewport(Point leftBotPoint, double width, double height)
        //    : this(leftBotPoint, new Point { X = leftBotPoint.X + width, Y = leftBotPoint.Y + height })
        //{ }

        public Viewport(double leftBotPointX, double leftBotPointY, double width, double height)
            //: this(new Point { X = leftBotPointX, Y = leftBotPointY }, width, height)
        {
            Left = leftBotPointX;
            Bottom = leftBotPointY;
            Right = Left + width;
            Top = Bottom + height;
        }

        public void ScrollHorizontally(double distanceToTheRight)
        {
            Left += distanceToTheRight;
            Right += distanceToTheRight;
        }

        public void ScrollVertically(double distanceToTheBot)
        {
            Bottom += distanceToTheBot;
            Top += distanceToTheBot;
        }

        public override string ToString()
        {
            StringBuilder sb = new($"Viewport [({Left}, {Bottom}), ({Right}, {Top})]");
            return sb.ToString();
        }
    }
}
