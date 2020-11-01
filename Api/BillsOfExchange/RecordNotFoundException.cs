using System;

namespace BillsOfExchange
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException()
        {

        }

        public RecordNotFoundException(string message) : base(message)
        {

        }
    }
}