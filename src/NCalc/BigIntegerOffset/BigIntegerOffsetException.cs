using System;

namespace NCalc.BigIntegerOffset
{
    public class BigIntegerOffsetException : Exception
    {
        public BigIntegerOffsetException(string errMessage) : base(errMessage)
        {
        }
    }
}