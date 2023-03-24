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
        public async Task Test1()
        {
            string name = "TextFile1.txt";
            await TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test1Answer);
        }

        [Test]
        public async Task Test2()
        {
            string name = "TextFile2.txt";
            await TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test2Answer);
        }

        [Test]
        public async Task TestLarge()
        {
            string name = "Large.txt";
            int n = 100000;
            CreateLargeFileIfNotExists(name, n);
            await TestRunner.TestOnInputFile(path + name, 
                new InputFileGenerator().GenerateRightAnswer(n));
        }

        [Test]
        public async Task TestSimple1()
        {
            string name = "TextFile1.txt";
            await TestRunner.TestSimpleOnInputFile(path + name, VisibilityTestAnswers.Test1Answer);
        }

        [Test]
        public async Task TestSimple2()
        {
            string name = "TextFile2.txt";
            await TestRunner.TestSimpleOnInputFile(path + name, VisibilityTestAnswers.Test2Answer);
        }

        [Test]
        public async Task TestSimpleLarge()
        {
            string name = "Large.txt";
            int n = 100000;
            CreateLargeFileIfNotExists(name, n);
            await TestRunner.TestSimpleOnInputFile(path + name, 
                new InputFileGenerator().GenerateRightAnswer(n));
        }

        private string CreateLargeFileIfNotExists(string name, int n)
        {
            string newName = $"{name}_{n}.txt";
            if (!File.Exists(path + newName))
                new InputFileGenerator().GenerateLargeFile(path, newName, n);
            return newName;
        }
    }
}