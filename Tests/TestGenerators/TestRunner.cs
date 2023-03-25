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
        public static void TestOnInputFile(string path, VisibilityResult rightAnswer, string TestVisibilityType)
        {
            UIMonitor monitor = new();
            monitor.LoadInputFile(path);
            VisibilityResult ans;
            switch (TestVisibilityType) 
            {
                case "Simple":
                    ans = monitor.TestVisibility();
                    break;
                case "Concurrent":
                    monitor.TestVisibilityConcurrent();
                    ans = monitor.RecalculateVisibilityResult();
                    break;
                default:
                    ans = monitor.RecalculateVisibilityResult();
                    break;
            };
            VisibilityTestAnswers.CompareAnswers(rightAnswer, ans);
        }
    }
}
