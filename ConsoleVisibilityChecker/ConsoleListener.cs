﻿using System;
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
                path = "C:\\Users\\mozha\\HOMEWORK\\8sem\\Huawei\\TestTask\\Input\\TestInput1.txt";

            Console.WriteLine("Reading input file ...");
            UIMonitor monitor = new(path);
            ConsolePrinter.PrintMonitorElements(monitor);
            IVisibilityTester vt = new SimpleVisibilityTester(monitor);

            Console.WriteLine("Print commands (implemented commands: " +
                "visibility (v) / full visibility (fv) / " +
                "scroll horizontally x (hor x) / scroll vertically x (ver x) / exit (e))");
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
                    vt.ScrollHorizontally(double.Parse(args[1]));
                    break;
                case "ver":
                    vt.ScrollVertically(double.Parse(args[1]));
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
            return true;
        }
    }
}