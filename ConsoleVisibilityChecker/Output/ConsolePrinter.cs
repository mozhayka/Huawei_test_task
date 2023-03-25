using System.Text;
using VisibilityChecker;

namespace ConsoleVisibilityChecker
{
    internal class ConsolePrinter
    {
        public static void PrintVisibleInShortForm(VisibilityResult ans)
        {
            StringBuilder sb = new();
            sb.AppendLine("Visible UI elements: ");
            foreach (var elem in ans.VisibleIds)
            {
                sb.Append($"{elem} ");
            }
            sb.AppendLine();
            sb.AppendLine("Partially visible UI elements: ");
            foreach (var elem in ans.PartiallyIds)
            {
                sb.Append($"{elem} ");
            }
            sb.AppendLine();
            sb.AppendLine("Invisible UI elements: ");
            foreach (var elem in ans.InvisibleIds)
            {
                sb.Append($"{elem} ");
            }
            sb.AppendLine();
            Console.WriteLine(sb.ToString());
        }

        public static void PrintVisibleWithAllSubelements(VisibilityResult ans, List<UIElement> allElements)
        {
            StringBuilder sb = new();
            sb.AppendLine("Visible UI elements: ");
            foreach (var elem in ans.VisibleIds)
            {
                PrintUIElementWithSubelements(allElements[elem], sb);
            }
            sb.AppendLine();
            sb.AppendLine("Partially visible UI elements: ");
            foreach (var elem in ans.PartiallyIds)
            {
                sb.Append($"{elem} ");
            }
            sb.AppendLine();
            Console.WriteLine(sb.ToString());
        }

        public static void PrintMonitorElements(UIMonitor monitor)
        {
            Console.WriteLine(monitor.ToString());
        }

        public static void PrintHelp()
        {
            StringBuilder sb = new();
            sb.AppendLine("Implemented commands:");
            sb.AppendLine("v - visibility");
            sb.AppendLine("fv - full visibility");
            sb.AppendLine("hor x - scroll horizontally by x to the right");
            sb.AppendLine("ver x - scroll vertically by x to the top");
            sb.AppendLine("h - help");
            sb.AppendLine("e - exit");

            Console.WriteLine(sb.ToString());
        }        
        
        private static void PrintUIElementWithSubelements(UIElement elem, StringBuilder sb)
        {
            sb.Append($"{elem.Id} (");
            foreach (var element in elem.Subelements)
            {
                PrintUIElementWithSubelements(element, sb);
            }
            sb.Append(") ");
        }
    }
}
