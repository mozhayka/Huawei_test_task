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

        public static void TestWithScrolling(string path, VisibilityResult rightAnswer, string TestVisibilityType)
        {
            UIMonitor monitor = new();
            monitor.LoadInputFile(path);
            monitor.ScrollVertically(3);
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

        public static void TestWithScrolling2(string path, VisibilityResult rightAnswer, string TestVisibilityType)
        {
            UIMonitor monitor = new();
            monitor.LoadInputFile(path);
            monitor.ScrollVertically(3);
            monitor.ScrollHorizontally(-3);
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
