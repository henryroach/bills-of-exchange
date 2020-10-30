using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public class DataCorruptedException : Exception
    {
        public DataCorruptedException(string message) : base(message)
        { }
    }
}
