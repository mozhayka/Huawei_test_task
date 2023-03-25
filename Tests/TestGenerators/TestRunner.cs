using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Answers;
using VisibilityChecker;

namespace Tests
{
    internal class TestRunner
    {
        public static void TestOnInputFile(string path, VisibilityResult rightAnswer)
        {
            UIMonitor monitor = new();
            monitor.LoadInputFile(path);
            var sw = Stopwatch.StartNew();
            var ans = monitor.TestVisibility();
            sw.Stop();
            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}");
            VisibilityTestAnswers.CompareAnswers(rightAnswer, ans);
        }
    }
}
