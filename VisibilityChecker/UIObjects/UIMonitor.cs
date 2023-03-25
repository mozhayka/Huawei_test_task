using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public class UIMonitor
    {
        public List<UIElement> RootElements { get; } = new();
        public List<UIElement> AllElements { get; } = new();
        public UIViewport? Viewport { get; private set; }
        private readonly VisibilityResult LastVisibilityResult = new();

        public UIMonitor()
        { }

        public void LoadInputFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            int id = 0;
            foreach (string s in lines.Take(lines.Length - 1))
            {
                ParseUIElement(s, id);
                id++;
            }
            ParseViewport(lines[^1]);
        }

        public void ScrollHorizontally(double distanceToTheRight)
        {
            Viewport?.ScrollHorizontally(distanceToTheRight);
        }

        public void ScrollVertically(double distanceToTheBot)
        {
            Viewport?.ScrollVertically(distanceToTheBot);
        }

        public VisibilityResult TestVisibility()
        {
            if (Viewport == null)
                throw new Exception("Viewport is not initialized, call LoadInputFile()");
            LastVisibilityResult.Clear();
            foreach (var elem in RootElements)
            {
                RecurentVisibilityTest(elem);
            }
            return LastVisibilityResult;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("UI elements");
            AllElements.ForEach(p => sb.AppendLine(p.ToString()));
            sb.AppendLine(Viewport?.ToString());
            return sb.ToString();
        }

        private void ParseUIElement(string s, int id)
        {
            var args = s.Split(' ');
            int fatherUIElement = int.Parse(args[0]);
            double[] coordinates = args.Skip(1).Select(x => double.Parse(x)).ToArray();
            var elem = new UIElement(coordinates[0], coordinates[1], coordinates[2], coordinates[3], id);

            AllElements.Add(elem);
            if (fatherUIElement == -1)
            {
                RootElements.Add(elem);
            }
            else
            {
                AllElements[fatherUIElement].AddSubelement(elem);
            }
        }

        private void ParseViewport(string s)
        {
            var args = s.Split(' ');
            double[] coordinates = args.Select(x => double.Parse(x)).ToArray();
            Viewport = new UIViewport(coordinates[0], coordinates[1], coordinates[2], coordinates[3]);
        }

        private void RecurentVisibilityTest(UIElement elem)
        {
            var visibility = UIRectangle.Intersect(Viewport, elem);
            LastVisibilityResult.Add(elem.Id, visibility);

            if (visibility == RectanglesIntersection.Intersect)
            {
                foreach (var element in elem.Subelements)
                {
                    RecurentVisibilityTest(element);
                }
            }
        }
    }
}
