using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class Viewport
    {
        public Point LeftTopPoint { get; private set; }
        public Point RightBotPoint { get; private set; }

        public Viewport(Point leftTopPoint, Point rightBotPoint)
        {
            LeftTopPoint = leftTopPoint;
            RightBotPoint = rightBotPoint;
        }

        public Viewport(Point leftTopPoint, double width, double height)
            : this(leftTopPoint, new Point { X = leftTopPoint.X + width, Y = leftTopPoint.Y - height })
        { }

        public Viewport(double leftTopPointX, double leftTopPointY, double width, double height)
            : this(new Point { X = leftTopPointX, Y = leftTopPointY }, width, height)
        { }

        public void ScrollHorizontally(double distanceToTheRight)
        {
            LeftTopPoint.X += distanceToTheRight;
            RightBotPoint.X += distanceToTheRight;
        }

        public void ScrollVertically(double distanceToTheBot)
        {
            LeftTopPoint.Y -= distanceToTheBot;
            RightBotPoint.Y -= distanceToTheBot;
        }
    }
}
