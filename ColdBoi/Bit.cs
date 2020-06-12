using System;

namespace ColdBoi
{
    public static class Bit
    {
        public static bool IsSet(byte b, int bitNumber)
        {
            return (b & (1 << bitNumber)) > 0;
        }
        
        public static byte Set(byte b, int bitNumber, bool value)
        {
            return (byte) ((b & ~(1 << bitNumber)) | (Convert.ToByte(value) << bitNumber));
        }
    }
}