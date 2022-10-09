using System;

namespace Task1_Library
{
    public class Library
    {
        public static string OutPutLogic(string input)
        {
            var time = DateTime.Now.ToString();
            return time + " ,Hello, " + input;
        }
    }
}
