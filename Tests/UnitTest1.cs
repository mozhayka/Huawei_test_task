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
            var ans = await TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test1Answer);
            Assert.That(ans, Is.True);
        }

        [Test]
        public async Task Test2()
        {
            string name = "TextFile2.txt";
            var ans = await TestRunner.TestOnInputFile(path + name, VisibilityTestAnswers.Test2Answer);
            Assert.That(ans, Is.True);
        }
    }
}