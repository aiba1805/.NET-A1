using System;
using System.Linq;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var inputString = Console.ReadLine();
                if (string.IsNullOrEmpty(inputString))
                {
                    throw new ArgumentNullException("Input string can not be empty!");
                }
                inputString.Split(" ").Select(c => c.First()).ToList().ForEach(c => { Console.WriteLine(c); });
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}