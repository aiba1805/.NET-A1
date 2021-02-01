using NUnit.Framework;
using System;

namespace LeapYear.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsLeapYear_True()
        {
            var date = new DateTime(1996, 1, 1);
            var isLeapYear =  date.IsLeapYear();
            Assert.IsTrue(isLeapYear);
        }

        [Test]
        public void IsLeapYear_False()
        {
            var date = new DateTime(2001, 1, 1);
            var isLeapYear = date.IsLeapYear();
            Assert.IsFalse(isLeapYear);
        }
    }
}