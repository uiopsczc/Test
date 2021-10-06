using System;

namespace CsCat
{
    public class UintUtil
    {
        public static bool IsContains(uint value, uint beContainedValue)
        {
            return (value & beContainedValue) == beContainedValue;
        }
    }
}