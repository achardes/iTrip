using System;
namespace iTrip
{
    public static class Converters
    {
        public static double FromStringToDouble(this string value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch (Exception ex)
            {
                return 0.0;
            }
        }

        public static int FromStringToInt(this string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
