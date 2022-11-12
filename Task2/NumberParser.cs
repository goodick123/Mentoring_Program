using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue == null)
                throw new ArgumentNullException();
            if (stringValue=="" || char.IsWhiteSpace(stringValue[0]))
                throw new FormatException();

            char[] numbers = { '-',' ','+', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < stringValue.Length; i++)
            {
                if (!numbers.Contains(stringValue[i]) || (stringValue[i]=='-' && i!=0) || (stringValue[i] == '+' && i != 0))
                    throw new FormatException();
            }

            if ((stringValue.Length == 10 && stringValue[0]!= '-' && !Regex.IsMatch(stringValue, "^([1 - 2][0 - 1][0 - 4][0 - 7][0 - 4][0 - 8][0 - 3][0 - 6][0 - 4][0 - 7])$")) || (stringValue.Length == 11 && !Regex.IsMatch(stringValue, "^(-[1 - 2][0 - 1][0 - 4][0 - 7][0 - 4][0 - 8][0 - 3][0 - 6][0 - 4][0 - 8])$")) || stringValue.Length > 11)
            {
                throw new OverflowException();
            }

            int num = 0;
            int n = stringValue.Length;
            var counter = 0;
            if (stringValue[0] == '-')
            {
                for (int i = 1; i < n; i++)
                    if ((stringValue[i] == 0 && counter == 0) || char.IsWhiteSpace(stringValue[i]))
                        continue;
                    else
                    {
                        num = num * 10 + (stringValue[i] - 48);
                        counter++;
                    }
                num *= -1;
            }
            else if (stringValue[0] == '+')
            {
                for (int i = 1; i < n; i++)
                    if ((stringValue[i] == 0 && counter == 0) || char.IsWhiteSpace(stringValue[i]))
                        continue;
                    else
                    {
                        num = num * 10 + (stringValue[i] - 48);
                        counter++;
                    }
            }
            else
            {
                for (int i = 0; i < n; i++)
                    if ((stringValue[i] == 0 && counter == 0) || char.IsWhiteSpace(stringValue[i]))
                        continue;
                    else
                    {
                        num = num * 10 + (stringValue[i] - 48);
                        counter++;
                    }
            }
            return num;
        }
    }
}