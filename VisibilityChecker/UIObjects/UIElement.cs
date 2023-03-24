using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VisibilityChecker
{
    public class UIElement
    {
        private readonly List<UIElement> Subelements = new();
        //public Point LeftBotPoint { get; private set; }
        //public Point RightTopPoint { get; private set; }
        public double Left, Right, Top, Bottom;
        public int Id { get; private set; }

        //public UIElement(Point leftBotPoint, Point rightTopPoint, int id)
        //{
        //    LeftBotPoint = leftBotPoint;
        //    RightTopPoint = rightTopPoint;
        //    Id = id;
        //    Subelements = new List<UIElement>();
        //}

        //public UIElement(Point leftBotPoint, double width, double height, int id)
        //    : this(leftBotPoint, new Point { X = leftBotPoint.X + width, Y = leftBotPoint.Y + height }, id)
        //{ }
        
        public UIElement(double leftBotPointX, double leftBotPointY, double width, double height, int id)
            //: this(new Point { X = leftBotPointX, Y = leftBotPointY }, width, height, id)
        {
            Left = leftBotPointX;
            Bottom = leftBotPointY;
            Right = Left + width;
            Top = Bottom + height;
            Id = id;
        }

        public void AddSubelement(UIElement sub)
        {
            if (!IsInside(sub))
            {
                throw new InvalidOperationException("subelements is not inside parent");
            }
            Subelements.Add(sub);
        }

        public void RemoveSubelement(int place)
        {
            Subelements.RemoveAt(place);
        }

        public List<UIElement> GetSubelements()
        {
            return Subelements;
        }
        public override string ToString()
        {
            StringBuilder sb = new($"[({Left}, {Bottom}), ({Right}, {Top})] id {Id}, subelements: ");
            Subelements.ForEach(p => sb.Append($"{p.Id} "));
            return sb.ToString();
        }

        public bool IsInside(UIElement sub)
        {
            return IsInside(this, sub);
        }

        public static bool IsInside(UIElement parent, UIElement sub)
        {
            var isInsideByX = Intersection.SegmentsIntersect(parent.Left, parent.Right, sub.Left, sub.Right) == Intersect.Inside;
            var isInsideByY = Intersection.SegmentsIntersect(parent.Bottom, parent.Top, sub.Bottom, sub.Top) == Intersect.Inside;
            return isInsideByX && isInsideByY;
        }
    }
}
