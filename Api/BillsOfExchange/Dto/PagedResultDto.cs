using System.Collections.Generic;

namespace BillsOfExchange.Dto
{
    public class PagedResultDto<T>
    {
        public int Count { get; set; }

        public IEnumerable<T> Data { get; set; }

        public PagedResultDto()
        {
            Data = new List<T>();
        }
    }
}