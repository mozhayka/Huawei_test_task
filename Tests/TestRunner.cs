using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Answers;
using VisibilityChecker;

namespace Tests
{
    internal class TestRunner
    {
        public static async Task TestOnInputFile(string path, VisibilityResult rightAnswer)
        {
            UIMonitor monitor = new();
            monitor.LoadInputFile(path);
            IVisibilityTester vt = new OptimizedVisibilityTester(monitor);
            var ans = await vt.TestVisibilityAsync();
            VisibilityTestAnswers.CompareAnswers(rightAnswer, ans);
        }

        public static async Task TestSimpleOnInputFile(string path, VisibilityResult rightAnswer)
        {
            UIMonitor monitor = new();
            monitor.LoadInputFile(path);
            IVisibilityTester vt = new SimpleVisibilityTester(monitor);// new OptimizedVisibilityTester(monitor);
            var ans = await vt.TestVisibilityAsync();
            VisibilityTestAnswers.CompareAnswers(rightAnswer, ans);
        }
    }
}
