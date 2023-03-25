using System.Globalization;
using Tests.Answers;
using VisibilityChecker;

namespace Tests
{
    public class Tests
    {
        const string path = "..\\..\\..\\..\\Tests\\TestInput\\";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestSimple1()
        {
            string name = "TextFile1.txt";
            await TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test1Answer, "Simple");
        }

        [Test]
        public async Task TestOptimized1()
        {
            string name = "TextFile1.txt";
            await TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test1Answer, "Optimized");
        }

        [Test]
        public async Task TestSimple2()
        {
            string name = "TextFile2.txt";
            await TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test2Answer, "Simple");
        }

        [Test]
        public async Task TestOptimized2()
        {
            string name = "TextFile2.txt";
            await TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test2Answer, "Optimized");
        }

        [Test]
        public async Task TestSimpleLarge()
        {
            string name = "Large";
            int n = 100000;
            string newName = LargeFileGenerator.CreateLargeFileIfNotExists(path, name, n);
            await TestRunner.TestOnInputFile(newName, new LargeFileGenerator().GenerateRightAnswer(n), "Simple");
        }

        [Test]
        public async Task TestOptimizedLarge()
        {
            string name = "Large";
            int n = 100000;
            string newName = LargeFileGenerator.CreateLargeFileIfNotExists(path, name, n);
            await TestRunner.TestOnInputFile(newName, new LargeFileGenerator().GenerateRightAnswer(n), "Optimized");
        }
    }
}
