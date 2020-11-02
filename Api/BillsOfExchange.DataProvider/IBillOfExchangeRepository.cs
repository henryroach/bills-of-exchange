using System.Collections.Generic;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.DataProvider
{
    public interface IBillOfExchangeRepository
    {
        int Count();

        IEnumerable<BillOfExchange> Get(int take, int skip);

        IReadOnlyList<BillOfExchange> GetByIds(IReadOnlyList<int> ids);

        IReadOnlyList<IEnumerable<BillOfExchange>> GetByDrawerIds(IReadOnlyList<int> drawerIds);

        IReadOnlyList<IEnumerable<BillOfExchange>> GetByBeneficiaryIds(IReadOnlyList<int> beneficiaryIds);
    }
}