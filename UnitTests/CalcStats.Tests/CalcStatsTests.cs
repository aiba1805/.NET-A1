using NUnit.Framework;
using CalcStats;

namespace CalcStats.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(new[] { 0,1,2}, ExpectedResult = 0)]
        [TestCase(new[] { -9, 7, 2 }, ExpectedResult = -9)]
        public int ReturnMinValue(int[] arr)
        {
            return arr.Min();
        }

        [TestCase(new[] { 0, 1, 2 }, ExpectedResult = 2)]
        [TestCase(new[] { -9, 7, 2 }, ExpectedResult = 7)]
        public int ReturnMaxValue(int[] arr)
        {
            return arr.Max();
        }

        [TestCase(new[] { 0, 1, 2 }, ExpectedResult = 1)]
        [TestCase(new[] { -9, 7, 2 }, ExpectedResult = 0)]
        public int ReturnAvgValue(int[] arr)
        {
            return arr.Avg();
        }

        [TestCase(new[] { 0, 1, 2 }, ExpectedResult = 3)]
        [TestCase(new[] { -9, 7, 2, 4 }, ExpectedResult = 4)]
        public int ReturnLengthValue(int[] arr)
        {
            return arr.Count();
        }
    }
}