using System.Globalization;
using Tests.Answers;
using VisibilityChecker;

namespace Tests
{
    public class Tests
    {
        private const string PathToDirectory = "..\\..\\..\\..\\Tests\\TestInput\\";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSimple1()
        {
            string name = "TextFile1.txt";
            TestRunner.TestOnInputFile(PathToDirectory + name, VisibilityTestAnswers.Test1Answer);
        }

        [Test]
        public void TestSimple2()
        {
            string name = "TextFile2.txt";
            TestRunner.TestOnInputFile(PathToDirectory + name, VisibilityTestAnswers.Test2Answer);
        }

        [Test]
        public void TestLarge()
        {
            int n = 100000;
            string fullPath = LargeFileGenerator.CreateLargeFileIfNotExists(PathToDirectory, "Large", n);
            TestRunner.TestOnInputFile(fullPath, new LargeFileGenerator().GenerateRightAnswer(n));
        }
    }
}
