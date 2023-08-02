using System;

namespace NCalc.BigIntOffset
{
    public class BigIntegerOffsetException : Exception
    {
        public BigIntegerOffsetException(string errMessage) : base(errMessage)
        {
        }
    }
}