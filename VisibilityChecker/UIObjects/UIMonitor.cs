using System.Text;

namespace VisibilityChecker
{
    public class UIMonitor
    {
        public List<UIElement> RootElements { get; } = new();
        public List<UIElement> AllElements { get; } = new();
        public UIViewport? Viewport { get; private set; }
        public VisibilityResult LastVisibilityResult { get; private set; } = new();

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

        public void ScrollVertically(double distanceToTheTop)
        {
            Viewport?.ScrollVertically(distanceToTheTop);
        }

        public VisibilityResult TestVisibility()
        {
            if (Viewport == null)
                throw new Exception("Viewport is not initialized, call LoadInputFile()");
            LastVisibilityResult.Clear();
            foreach (var elem in RootElements)
            {
                RecurentTestVisibility(elem);
            }
            return LastVisibilityResult;
        }

        public void TestVisibilityConcurrent()
        {
            if (Viewport == null)
                throw new Exception("Viewport is not initialized, call LoadInputFile()");
            Parallel.ForEach (
                RootElements,
                elem => RecurentTestVisibilityConcurrent(elem)
            );
        }

        public VisibilityResult RecalculateVisibilityResult()
        {
            LastVisibilityResult.Clear();
            foreach (var elem in RootElements)
            {
                RecurentRecalculateVisibilityResult(elem);
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

        private void RecurentTestVisibility(UIElement elem)
        {
            if (Viewport == null)
                throw new Exception("Viewport was not initialized");
            var intersection = Viewport.Intersect(elem);
            LastVisibilityResult.Add(elem.Id, intersection);
            elem.Visibility = IntersectionVisibilityConverter.FromIntersection(intersection);

            if (intersection == RectanglesIntersection.Intersect)
            {
                foreach (var element in elem.Subelements)
                {
                    RecurentTestVisibility(element);
                }
            }
        }

        private void RecurentTestVisibilityConcurrent(UIElement elem)
        {
            if (Viewport == null)
                throw new Exception("Viewport was not initialized");
            var intersection = Viewport.Intersect(elem);
            elem.Visibility = IntersectionVisibilityConverter.FromIntersection(intersection);

            if (intersection == RectanglesIntersection.Intersect)
            {
                foreach (var element in elem.Subelements)
                {
                    RecurentTestVisibilityConcurrent(element);
                }
            }
        }

        private void RecurentRecalculateVisibilityResult(UIElement elem)
        {
            if (elem.Visibility == null)
                throw new Exception($"UIElement {elem.Id} visibility was not calculated");
            LastVisibilityResult.Add(elem.Id, (Visibility)elem.Visibility);

            if (elem.Visibility == Visibility.Partially)
            {
                foreach (var element in elem.Subelements)
                {
                    RecurentRecalculateVisibilityResult(element);
                }
            }
        }
    }
}
