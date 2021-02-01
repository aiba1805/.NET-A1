using System;
using System.Collections.Generic;
using System.Text;

namespace CalcStats
{
    public static class ArrayExtensions
    {
        public static int Min(this int[] arr)
        {
            var min = arr[0];
            foreach (var item in arr)
            {
                if (min > item) min = item;
            }
            return min;
        }

        public static int Max(this int[] arr)
        {
            var max = arr[0];
            foreach (var item in arr)
            {
                if (max < item) max = item;
            }
            return max;
        }

        public static int Avg(this int[] arr)
        {
            var sum = 0;
            foreach (var item in arr)
            {
                sum += item;
            }
    
            return sum / arr.Length;
        }

        public static int Count(this int[] arr)
        {
            var i = 0;
            foreach (var item in arr)
            {
                i++;
            }

            return i;
        }
    }
}
