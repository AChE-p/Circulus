using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saveTest
{
    class TestDouble : SavingForm
    {
        public double d;
        public const int byteLen = 8;
        public override byte[] ConvertToByte(int byteLen)
        {
            return BitConverter.GetBytes(d);
        }
        public override void ConvertFromByte(byte[] data)
        {
            d = BitConverter.ToDouble(data, 0);
        }
    }
}
