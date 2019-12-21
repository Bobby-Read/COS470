using NUnit.Framework;
using Assignment2;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("z", ExpectedResult = 26)]
        [TestCase("abc", ExpectedResult = 6)]
        [TestCase("aAa", ExpectedResult = 3)]
        [TestCase("   ", ExpectedResult = 0)]
        [TestCase("23958", ExpectedResult = 0)]
        [TestCase("8.,  ", ExpectedResult = 0)]
        [TestCase("1.,4aaa", ExpectedResult = 3)]
        [TestCase("word", ExpectedResult = 60)]

        public int TestWordScore(string input)
        {
            return Assignment2.DollarAddresses.WordScore(input);
        }
    }
}