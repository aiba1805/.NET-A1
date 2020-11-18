using System;

namespace HelloBL
{
    public class Hello
    {
        public string GetMessage(string name)
        {
            if (string.IsNullOrEmpty(name)) return "Please, provide name";
            return $"{DateTime.Now} Hello, {name}";
        }
    }
}
