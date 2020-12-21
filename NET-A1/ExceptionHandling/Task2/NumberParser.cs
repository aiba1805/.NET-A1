using System;
using System.Linq;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string value)
        {
            try
            {
                if (value is null) throw new ArgumentNullException("Value can not be null!");
                value = value.Trim();
                if (string.IsNullOrEmpty(value)) throw new FormatException("Wrong format");
                var positive = value.First() != '-';
                var first = value.First();
                if (!(first >= '0' && first <= '9')) value = value.Remove(0,1).ToString();
                long number = 0;
                long degree = 1;
                foreach(var item in value.Reverse())
                {
                    if (!(item >= '0' && item <= '9')) throw new FormatException("Wrong format");
                    number += (GetInt(item) * degree);
                    degree*=10;
                }
                if (!positive) number *= -1;
                if (number > int.MaxValue || number < int.MinValue) throw new OverflowException();
                return (int)number;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (OverflowException ex)
            {
                throw ex;
            }
        }

        private int GetInt(char c)
        {
            return c - '0';
        }
    }
}