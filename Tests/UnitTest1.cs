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
        public void TestSimple1()
        {
            string name = "TextFile1.txt";
            TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test1Answer);
        }

        [Test]
        public void TestSimple2()
        {
            string name = "TextFile2.txt";
            TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test2Answer);
        }

        [Test]
        public void TestLarge()
        {
            string name = "Large";
            int n = 100000;
            string newName = LargeFileGenerator.CreateLargeFileIfNotExists(path, name, n);
            TestRunner.TestOnInputFile(newName, new LargeFileGenerator().GenerateRightAnswer(n));
        }
    }
}
