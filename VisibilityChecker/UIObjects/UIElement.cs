using System.Text;

namespace VisibilityChecker
{
    public class UIElement : UIRectangle
    {
        public List<UIElement> Subelements { get; protected set; } = new();
        public Visibility? Visibility { get; internal set; }
        public int Id { get; protected set; }
        
        public UIElement(double leftBottomPointX, double leftBotPointY, double width, double height, int id)
            : base(leftBottomPointX, leftBottomPointX + width, leftBotPointY + height, leftBotPointY)
        {
            Id = id;
        }

        public override string ToString()
        {
            StringBuilder sb = new($"[({Left}, {Bottom}), ({Right}, {Top})] id {Id}, subelements: ");
            Subelements.ForEach(p => sb.Append($"{p.Id} "));
            return sb.ToString();
        }

        internal void AddSubelement(UIElement sub)
        {
            if (!IsInside(sub))
            {
                throw new InvalidOperationException("subelement is not inside parent");
            }
            Subelements.Add(sub);
        }
    }
}
