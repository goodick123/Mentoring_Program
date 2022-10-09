using System;

namespace Task1_Lib
{
    public class Lib
    {
        public static string OutPutLogic(string input)
        {
            var time = DateTime.Now.ToString();
            return time + ", Hello, " + input;
        }
    }
}
