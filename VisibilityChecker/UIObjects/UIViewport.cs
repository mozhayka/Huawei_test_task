using System.Text;

namespace VisibilityChecker
{
    public class UIViewport : UIRectangle
    {
        public UIViewport(double leftBotPointX, double leftBotPointY, double width, double height)
            : base(leftBotPointX, leftBotPointX + width, leftBotPointY + height, leftBotPointY)
        { }

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
