using System;
using System.Collections.Generic;
using System.Text;

namespace LeapYear
{
    public static class LeapYearExtension
    {
        public static bool IsLeapYear(this DateTime datetime){
            var year = datetime.Year;
            if ((year % 4 == 0 && year % 100 != 0) 
                || (year % 4 == 0 && year % 100 == 0 && year % 400 == 0))
                return true;
            else return false;
        }
    }
}
