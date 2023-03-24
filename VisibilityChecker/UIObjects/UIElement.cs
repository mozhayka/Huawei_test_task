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
        private readonly List<UIElement> Subelements;
        public Point LeftTopPoint { get; private set; }
        public Point RightBotPoint { get; private set; }
        public int Id { get; private set; }

        public UIElement(Point leftTopPoint, Point rightBotPoint, int id)
        {
            LeftTopPoint = leftTopPoint;
            RightBotPoint = rightBotPoint;
            Id = id;
            Subelements = new List<UIElement>();
        }

        public UIElement(Point leftTopPoint, double width, double height, int id)
            : this(leftTopPoint, new Point { X = leftTopPoint.X + width, Y = leftTopPoint.Y - height }, id)
        { }
        
        public UIElement(double leftTopPointX, double leftTopPointY, double width, double height, int id)
            : this(new Point { X = leftTopPointX, Y = leftTopPointY }, width, height, id)
        { }

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
            StringBuilder sb = new($"[{LeftTopPoint}, {RightBotPoint}] id {Id}, subelements ");
            Subelements.ForEach(p => sb.Append($"{p.Id} "));
            return sb.ToString();
        }

        public bool IsInside(UIElement sub)
        {
            return IsInside(this, sub);
        }

        public static bool IsInside(UIElement parent, UIElement sub)
        {
            return parent.LeftTopPoint >= sub.LeftTopPoint && sub.RightBotPoint >= parent.RightBotPoint;
        }
    }
}
