using UnitTesting.Interface;

namespace UnitTesting
{
    public class LeapYear: ILeapYears
    {
        public bool Leap(int year)
        {
            if (year > 0)
            {
                if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            throw new ArgumentException();
        }
    }
}
