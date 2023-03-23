using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TestTask
{
    internal class UIMonitor
    {
        public List<UIElement> ParentElements { get; }
        public List<UIElement> AllElements { get; }
        public Viewport Window { get; private set; }

        public UIMonitor(string path)
        {
            ParentElements = new();
            AllElements = new();
            ReadInitFile(path);
        }

        private void ReadInitFile(string path)
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

        private void ParseUIElement(string s, int id)
        {
            var args = s.Split(' ');
            int fatherUIElement = int.Parse(args[0]);
            double[] coordinates = args.Skip(1).Select(x => double.Parse(x)).ToArray();
            var elem = new UIElement(coordinates[0], coordinates[1], coordinates[2], coordinates[3], id);

            AllElements.Add(elem);
            if (fatherUIElement == -1)
            {
                ParentElements.Add(elem);
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
            Window = new Viewport(coordinates[0], coordinates[1], coordinates[2], coordinates[3]);
        }

        public void ScrollHorizontally(double distanceToTheRight)
        {
            Window.ScrollHorizontally(distanceToTheRight);
        }
         
        public void ScrollVertically(double distanceToTheBot)
        {
            Window.ScrollVertically(distanceToTheBot);
        }
    }
}
