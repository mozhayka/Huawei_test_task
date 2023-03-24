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

        //[Test]
        //public async Task Test1()
        //{
        //    string name = "TextFile1.txt";
        //    await TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test1Answer);
        //}

        //[Test]
        //public async Task Test2()
        //{
        //    string name = "TextFile2.txt";
        //    await TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test2Answer);
        //}

        [Test]
        public async Task TestLarge()
        {
            string name = "Large";
            int n = 1000000;
            string newName = CreateLargeFileIfNotExists(name, n);
            await TestRunner.TestOnInputFile(newName); 
                //new InputFileGenerator().GenerateRightAnswer(n));
        }

        //[Test]
        //public async Task TestSimple1()
        //{
        //    string name = "TextFile1.txt";
        //    await TestRunner.TestSimpleOnInputFile(path + name, VisibilityTestAnswers.Test1Answer);
        //}

        //[Test]
        //public async Task TestSimple2()
        //{
        //    string name = "TextFile2.txt";
        //    await TestRunner.TestSimpleOnInputFile(path + name, VisibilityTestAnswers.Test2Answer);
        //}

        [Test]
        public async Task TestSimpleLarge()
        {
            string name = "Large";
            int n = 1000000;
            string newName = CreateLargeFileIfNotExists(name, n);
            await TestRunner.TestSimpleOnInputFile(newName); 
                //new InputFileGenerator().GenerateRightAnswer(n));
        }

        private static string CreateLargeFileIfNotExists(string name, int n)
        {
            string newName = $"{name}_{n}.txt";
            string newPath = Path.Combine(path, newName);
            if (!File.Exists(newPath))
                new InputFileGenerator().GenerateLargeFile(newPath, n);
            return path + newName;
        }
    }
}