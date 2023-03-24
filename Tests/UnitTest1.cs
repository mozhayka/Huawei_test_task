using VisibilityChecker;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            UIMonitor monitor = new("..\\..\\..\\..\\Tests\\TestInput\\TextFile1.txt");
            IVisibilityTester vt = new OptimizedVisibilityTester(monitor);
            var ans = vt.VisibilityTest();
            Assert.Pass();
        }
    }
}