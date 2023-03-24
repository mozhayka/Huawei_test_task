using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisibilityChecker;

namespace ConsoleVisibilityChecker
{
    public class ConsoleListener
    {
        public static void Start()
        {
            Console.WriteLine("Print input filename (or press enter to start with TestInput1.txt)");
            var path = Console.ReadLine();
            if (path == "" || path == null)
                path = "..\\..\\..\\..\\Input\\TestInput1.txt";

            Console.WriteLine("Reading input file ...");
            UIMonitor monitor = new(path);
            ConsolePrinter.PrintMonitorElements(monitor);
            IVisibilityTester vt = new OptimizedVisibilityTester(monitor); // new SimpleVisibilityTester(monitor);

            Console.WriteLine("Print commands");
            while (true)
            {
                if (!CommandParser(monitor, vt))
                    break;
            }
            Console.WriteLine("Exited");
        }

        private static bool CommandParser(UIMonitor monitor, IVisibilityTester vt)
        {
            var command = Console.ReadLine();
            if (command == null)
            {
                Console.WriteLine("Unknown command (null), write help to see implemented commands");
                return true;
            }
            var args = command.Split(' ').ToArray();
            switch (args[0])
            {
                case "e":
                    return false;
                case "v":
                    ConsolePrinter.PrintVisibleShort(vt.VisibilityTest());
                    break;
                case "fv":
                    ConsolePrinter.PrintVisibleFull(vt.VisibilityTest(), monitor.AllElements);
                    break;
                case "hor":
                    TryScrollHorizontally(args, vt);
                    break;
                case "ver":
                    TryScrollVertically(args, vt);
                    break;
                case "m":
                    ConsolePrinter.PrintMonitorElements(monitor);
                    break;
                case "help":
                    ConsolePrinter.PrintHelp();
                    break;
                default:
                    Console.WriteLine("Unknown command, write help to see implemented commands");
                    break;
            }
            return true;
        }

        private static void TryScrollHorizontally(string[] args, IVisibilityTester vt)
        {
            try
            {
                var x = double.Parse(args[1]);
                vt.ScrollHorizontally(x);
            }
            catch
            {
                Console.WriteLine("Wrong arguments (use , in 1,5)");
            }
        }

        private static void TryScrollVertically(string[] args, IVisibilityTester vt)
        {
            try
            {
                var x = double.Parse(args[1]);
                vt.ScrollVertically(x);
            }
            catch
            {
                Console.WriteLine("Wrong arguments (use , in 1,5)");
            }
        }
    }
}
