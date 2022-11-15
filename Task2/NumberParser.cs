using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            ValidateInput(stringValue);
            int intResult = 0;
            switch (stringValue[0])
            {
                case '-':
                    {
                        foreach (var stringChar in stringValue.Remove(0, 1))
                        {
                            intResult = ConvertStringToInt(stringChar, intResult);
                        }

                        intResult *= -1;
                        break;
                    }
                case '+':
                    {
                        foreach (var stringChar in stringValue.Remove(0, 1))
                        {
                            intResult = ConvertStringToInt(stringChar, intResult);
                        }

                        break;
                    }
                default:
                    foreach (var stringChar in stringValue)
                    {
                        intResult = ConvertStringToInt(stringChar, intResult);
                    }

                    break;
            }
            return intResult;
        }

        public void ValidateInput(string stringValue)
        {
            if (stringValue == null)
                throw new ArgumentNullException();
            if (stringValue == "" || char.IsWhiteSpace(stringValue[0]))
                throw new FormatException();

            char[] numbers = { '-', ' ', '+', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < stringValue.Length; i++)
            {
                if (!numbers.Contains(stringValue[i]) || (stringValue[i] == '-' && i != 0) || (stringValue[i] == '+' && i != 0))
                    throw new FormatException();
            }

            if ((stringValue.Length == 10 && stringValue[0] != '-' && !Regex.IsMatch(stringValue, "^([1 - 2][0 - 1][0 - 4][0 - 7][0 - 4][0 - 8][0 - 3][0 - 6][0 - 4][0 - 7])$")) || (stringValue.Length == 11 && !Regex.IsMatch(stringValue, "^(-[1 - 2][0 - 1][0 - 4][0 - 7][0 - 4][0 - 8][0 - 3][0 - 6][0 - 4][0 - 8])$")) || stringValue.Length > 11)
            {
                throw new OverflowException();
            }
        }

        public int ConvertStringToInt(char inputChar, int result)
        {
            if (!(inputChar == 0) && !char.IsWhiteSpace(inputChar))
            {
                result = result * 10 + (inputChar - 48);
            }
            return result;
        }
    }
}