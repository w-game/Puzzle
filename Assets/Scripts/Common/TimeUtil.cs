using System;

namespace Common
{
    public class TimeUtil
    {
        public static int Timestamp => (int)((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
    }
}