using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public class Viewport
    {
        public Point LeftBotPoint { get; private set; }
        public Point RightTopPoint { get; private set; }

        public Viewport(Point leftBotPoint, Point rightTopPoint)
        {
            LeftBotPoint = leftBotPoint;
            RightTopPoint = rightTopPoint;
        }

        public Viewport(Point leftBotPoint, double width, double height)
            : this(leftBotPoint, new Point { X = leftBotPoint.X + width, Y = leftBotPoint.Y + height })
        { }

        public Viewport(double leftBotPointX, double leftBotPointY, double width, double height)
            : this(new Point { X = leftBotPointX, Y = leftBotPointY }, width, height)
        { }

        public void ScrollHorizontally(double distanceToTheRight)
        {
            LeftBotPoint.X += distanceToTheRight;
            RightTopPoint.X += distanceToTheRight;
        }

        public void ScrollVertically(double distanceToTheBot)
        {
            LeftBotPoint.Y += distanceToTheBot;
            RightTopPoint.Y += distanceToTheBot;
        }

        public override string ToString()
        {
            StringBuilder sb = new($"Viewport [{LeftBotPoint}, {RightTopPoint}]");
            return sb.ToString();
        }
    }
}
