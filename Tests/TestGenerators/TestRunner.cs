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
        public static async Task TestOnInputFile(string path, VisibilityResult rightAnswer, string VisibilityTester)
        {
            UIMonitor monitor = new();
            monitor.LoadInputFile(path);
            IVisibilityTester vt = VisibilityTester switch
            {
                "Simple" => new SimpleVisibilityTester(monitor),
                "Optimized" => new OptimizedVisibilityTester(monitor),
                _ => new SimpleVisibilityTester(monitor),
            };
            var sw = Stopwatch.StartNew();
            var ans = await vt.TestVisibilityAsync();
            sw.Stop();
            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}");
            VisibilityTestAnswers.CompareAnswers(rightAnswer, ans);
        }
    }
}
