using System.Globalization;
using VisibilityChecker;

namespace ConsoleVisibilityChecker
{
    public class ConsoleRunner
    {
        public static void Start()
        {
            Console.WriteLine("Print input filename (or press enter to start with TestInput1.txt)");
            var path = Console.ReadLine();
            if (path == "" || path == null)
                path = "..\\..\\..\\..\\Input\\TestInput1.txt";

            Console.WriteLine("Reading input file ...");
            UIMonitor monitor = new();
            monitor.LoadInputFile(path);
            ConsolePrinter.PrintMonitorElements(monitor);

            Console.WriteLine("Print commands");
            CommandParser(monitor);

            Console.WriteLine("Exited");
        }

        private static void CommandParser(UIMonitor monitor)
        {
            while (true)
            {
                var command = Console.ReadLine();
                if (command == null)
                {
                    Console.WriteLine("Unknown command (null), write help to see implemented commands");
                    continue;
                }
                var args = command.Split(' ').ToArray();
                switch (args[0])
                {
                    case "e":
                        return;
                    case "v":
                        ConsolePrinter.PrintVisibleInShortForm(monitor.TestVisibility());
                        break;
                    case "fv":
                        ConsolePrinter.PrintVisibleWithAllSubelements(monitor.TestVisibility(), monitor.AllElements);
                        break;
                    case "hor":
                        TryScroll(args, monitor);
                        break;
                    case "ver":
                        TryScroll(args, monitor);
                        break;
                    case "m":
                        ConsolePrinter.PrintMonitorElements(monitor);
                        break;
                    case "help":
                        ConsolePrinter.PrintHelp();
                        break;
                    default:
                        Console.WriteLine("Unknown command, type help to see implemented commands");
                        break;
                }
            }
        }

        private static void TryScroll(string[] args, UIMonitor monitor)
        {
            try
            {
                var x = double.Parse(args[1]);
                switch (args[0])
                {
                    case "hor":
                        monitor.ScrollHorizontally(x);
                        break;
                    case "ver":
                        monitor.ScrollVertically(x);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
