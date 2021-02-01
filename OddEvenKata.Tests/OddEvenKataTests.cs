using NUnit.Framework;
using System;

namespace OddEvenKata.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ReturnEven()
        {
            var list = new DeterminationList(1, 100);
            for (int i = 1; i < 100; i++)
            {
                if (i % 2 == 0)
                {
                    Assert.That(list[i] == "Even");
                }
            }
        }


        [Test]
        public void ReturnOdd()
        {
            var list = new DeterminationList(1, 100);
            for (int i = 1; i < 100; i++)
            {
                if (i % 2 != 0 && !i.IsPrime())
                {
                    Assert.That(list[i] == "Odd");
                }
            }
        }

        [Test]
        public void ReturnPrime()
        {
            var list = new DeterminationList(1, 100);
            for (int i = 1; i < 100; i++)
            {
                if (i != 2 && i.IsPrime())
                {
                    Assert.That(list[i] == i);
                }
            }
        }
    }
}