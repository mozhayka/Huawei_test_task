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
            //if (!File.Exists(path + name))
            InputFileGenerator.GenerateLargeFile(path, name);

            await TestRunner.TestOnInputFile(path + name, InputFileGenerator.RightAnswer());
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
            //if (!File.Exists(path + name))
            InputFileGenerator.GenerateLargeFile(path, name);

            await TestRunner.TestSimpleOnInputFile(path + name, InputFileGenerator.RightAnswer());
        }
    }
}