using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VisibilityChecker
{
    public class UIElement : UIRectangle
    {
        public List<UIElement> Subelements { get; protected set; } = new();
        
        public int Id { get; protected set; }
        
        public UIElement(double leftBotPointX, double leftBotPointY, double width, double height, int id)
            : base(leftBotPointX, leftBotPointX + width, leftBotPointY + height, leftBotPointY)
        {
            Id = id;
        }

        public void AddSubelement(UIElement sub)
        {
            if (!IsInside(sub))
            {
                throw new InvalidOperationException("subelement is not inside parent");
            }
            Subelements.Add(sub);
        }

        public override string ToString()
        {
            StringBuilder sb = new($"[({Left}, {Bottom}), ({Right}, {Top})] id {Id}, subelements: ");
            Subelements.ForEach(p => sb.Append($"{p.Id} "));
            return sb.ToString();
        }
    }
}
