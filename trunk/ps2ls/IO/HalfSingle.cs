using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ps2ls.IO
{
    public struct HalfSingle
    {
        private Byte msb;
        private Byte lsb;

        public HalfSingle(Byte[] bytes, Int32 offset)
        {
            msb = bytes[offset + 0];
            lsb = bytes[offset + 1];
        }

        public HalfSingle(IEnumerable<Byte> bytes)
        {
            msb = bytes.ElementAt(0);
            lsb = bytes.ElementAt(1);
        }

        //http://www.experts-exchange.com/Programming/Languages/C_Sharp/Q_23967775.html
        public Single ToSingle()
        {
            const int exponentBits = 5;
            const int exponentBitMask = (1 << exponentBits) - 1; //0x1F = 31
            const int mantissaBits = 10;
            //const int mantissaBitMask = (1 << mantissaBits) - 1; //0x3FF = 1023
            const int bias = 16;

            bool signIsNegative = (msb & 0x80) != 0;
            int exponentPart = (msb >> 2) & exponentBitMask;
            int coefficientPart = ((int)(msb & 0x03) << 8) | (int)(lsb);


            // IEEE reserves exponent fields 0 and ~0
            if (exponentPart == 0)
            {
                // Zero is represented by coefficient field set to 0.
                // Positive and negative 0 are possible.
                if (coefficientPart == 0)
                    return (float)((signIsNegative) ? (-0.0) : (0.0));
                else
                {
                    //Denormalized number- does not have an assumed leading 1 before the binary point.
                    //No change.
                }
            }
            else if (exponentPart == exponentBitMask)
            {
                // Special cases for infinity (+,-)
                if (coefficientPart == 0)
                    return (signIsNegative) ? (float.NegativeInfinity) : (float.PositiveInfinity);
                else
                // Special case for Not-A-Number (NaN)
                {
                    const int coefficientMsb = (1 << (mantissaBits - 1)); //most significant bit of the 10-bit coefficient
                    if ((coefficientPart & coefficientMsb) != 0) //most significant bit is set?
                        return float.NaN; // Quiet NaN; non-signalling NaN
                    return float.NaN; //Signalling NaN, rerpresents an error
                }
            }
            else
            {
                //Normalize coefficient with implied leading binary "1"  in the 11'th bit position
                coefficientPart |= (1 << mantissaBits);
            }

            float signFactor = (float)((signIsNegative) ? (-1.0) : (1.0));
            const int elevenBitMask = 1 << 11 - 1;
            float fractionFactor = (float)((float)(coefficientPart) / (float)(elevenBitMask)); //coefficient is a fraction 0.00 to 1.0;
            float exponentialFactor = (float)Math.Pow(2.0, (float)(exponentPart - bias));
            float retval = signFactor * fractionFactor * exponentialFactor;
            return retval;
        }
    }
}
