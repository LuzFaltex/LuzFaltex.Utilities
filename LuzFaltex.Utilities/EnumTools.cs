using System;
using System.Linq;

namespace LuzFaltex.Utilities
{
    public static class EnumTools
    {
        public static TEnum ParseFromValue<TEnum>(long value)
            where TEnum : struct, Enum
        {
            Array values = Enum.GetValues(typeof(TEnum));
            long bitMask = default;

            foreach (TEnum member in values)
                bitMask |= Convert.ToInt64(member);

            return Enum.TryParse((value & bitMask).ToString(), out TEnum result) ? result : default;
        }

    }
}
