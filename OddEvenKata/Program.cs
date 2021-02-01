using System;

namespace OddEvenKata
{
    class Program
    {
        static void Main(string[] args)
        {
            var determinationList = new DeterminationList(1, 100);
            var i = 1;
            foreach (var item in determinationList)
            {
                Console.WriteLine($"{i++} - {item} ");
            }
            Console.ReadLine();
        }
    }
}
