using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsOfExchange.Utils
{
    public static class PagingUtils
    {
        public static int GetSkipedItemsCount(int itemsPerPage, int pageNumber)
            => (pageNumber - 1) * itemsPerPage;
        
    }
}
