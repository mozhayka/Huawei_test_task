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
        public void Test1_Simple()
        {
            string name = "TextFile1.txt";
            TestRunner.TestOnInputFile(PathToDirectory + name, VisibilityTestAnswers.Test1Answer, "Simple");
        }

        [Test]
        public void Test1_Concurrent()
        {
            string name = "TextFile1.txt";
            TestRunner.TestOnInputFile(PathToDirectory + name, VisibilityTestAnswers.Test1Answer, "Concurrent");
        }

        [Test]
        public void Test2_Simple()
        {
            string name = "TextFile2.txt";
            TestRunner.TestOnInputFile(PathToDirectory + name, VisibilityTestAnswers.Test2Answer, "Simple");
        }

        [Test]
        public void Test2_Concurrent()
        {
            string name = "TextFile2.txt";
            TestRunner.TestOnInputFile(PathToDirectory + name, VisibilityTestAnswers.Test2Answer, "Concurrent");
        }

        [Test]
        public void TestWithScrolling1_Simple()
        {
            string name = "TextFile2.txt";
            TestRunner.TestWithScrolling(PathToDirectory + name, VisibilityTestAnswers.TestWithScrolling1Answer, "Simple");
        }

        [Test]
        public void TestWithScrolling1_Concurrent()
        {
            string name = "TextFile2.txt";
            TestRunner.TestWithScrolling(PathToDirectory + name, VisibilityTestAnswers.TestWithScrolling1Answer, "Concurrent");
        }

        [Test]
        public void TestWithScrolling2_Simple()
        {
            string name = "TextFile2.txt";
            TestRunner.TestWithScrolling2(PathToDirectory + name, VisibilityTestAnswers.TestWithScrolling2Answer, "Simple");
        }

        [Test]
        public void TestWithScrolling2_Concurrent()
        {
            string name = "TextFile2.txt";
            TestRunner.TestWithScrolling2(PathToDirectory + name, VisibilityTestAnswers.TestWithScrolling2Answer, "Concurrent");
        }

        [Test]
        public void TestLarge_Simple()
        {
            int n = 10000;
            string fullPath = LargeFileGenerator.CreateLargeFileIfNotExists(PathToDirectory, "Large", n);
            TestRunner.TestOnInputFile(fullPath, LargeFileGenerator.GenerateRightAnswer(n), "Simple");
        }

        [Test]
        public void TestLarge_Concurrent()
        {
            int n = 10000;
            string fullPath = LargeFileGenerator.CreateLargeFileIfNotExists(PathToDirectory, "Large", n);
            TestRunner.TestOnInputFile(fullPath, LargeFileGenerator.GenerateRightAnswer(n), "Concurrent");
        }
    }
}
