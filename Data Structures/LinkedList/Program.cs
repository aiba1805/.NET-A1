using System;

namespace TechTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var dlinkedList = new LinkedList<int>();
            //Проверка добавления в конец
            dlinkedList.Add(4);
            dlinkedList.Add(85);
            Console.WriteLine($"Last element - {dlinkedList.Last()}");
            //Проверка добавления в произвольную позицию по индексу
            dlinkedList.Add(5);
            dlinkedList.Add(1);
            dlinkedList.Add(7);
            dlinkedList.AddAt(2,9);
            // Должно быть 4 5 9 1 7
            foreach (var item in dlinkedList)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            // Проверка удаления по индексу
            dlinkedList.RemoveAt(2);
            //Должно быть 4 5 1 7
            foreach (var item in dlinkedList)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            //проверка получения элемента по индексу
            Console.WriteLine($"Element at 3rd index - {dlinkedList.ElementAt(3)}"); // вывести 7
            Console.WriteLine($"Length -  {dlinkedList.Length}"); //Выведет 4
            foreach (var item in dlinkedList)
            { 
                Console.Write($"{item}");
            }
            dlinkedList.Remove();
            Console.WriteLine();
            foreach (var item in dlinkedList)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            Console.WriteLine($"{dlinkedList.Length}"); //Выведет 3
        }
    }
}