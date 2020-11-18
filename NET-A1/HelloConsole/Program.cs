using HelloBL;
using System;

namespace HelloConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Hello hello = new Hello();
            Console.WriteLine(hello.GetMessage(args[0]));
        }
    }
}
