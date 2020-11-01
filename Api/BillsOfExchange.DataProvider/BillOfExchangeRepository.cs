using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac.Extras.DynamicProxy;
using BillsOfExchange.DataProvider.Extensions;
using BillsOfExchange.DataProvider.Models;
using Newtonsoft.Json;

namespace BillsOfExchange.DataProvider
{
    [Intercept(typeof(LogInterceptor))]
    public class BillOfExchangeRepository: IBillOfExchangeRepository
    {
        private static readonly Lazy<BillOfExchange[]> _billsOfExchange = new Lazy<BillOfExchange[]>(() =>
        {
            string json = File.ReadAllText("Data/BillsOfExchange.json");
            return JsonConvert.DeserializeObject<IEnumerable<BillOfExchange>>(json).OrderBy(item => item.Id).ToArray();
        });

        public IEnumerable<BillOfExchange> Get(int take, int skip)
            => _billsOfExchange.Value.Skip(skip).Take(take);

        public IReadOnlyList<BillOfExchange> GetByIds(IReadOnlyList<int> ids)
            => _billsOfExchange.Value.Where(item => ids.Contains(item.Id)).SortBySequence(ids, item => item.Id)
                .ToArray();

        public IReadOnlyList<IEnumerable<BillOfExchange>> GetByDrawerIds(IReadOnlyList<int> drawerIds)
            => _billsOfExchange.Value.Where(item => drawerIds.Contains(item.DrawerId)).ToLookup(item => item.DrawerId)
                .SortBySequence(drawerIds, lookup => lookup.Key).ToArray();

        public IReadOnlyList<IEnumerable<BillOfExchange>> GetByBeneficiaryIds(IReadOnlyList<int> beneficiaryIds)
            => _billsOfExchange.Value.Where(item => beneficiaryIds.Contains(item.BeneficiaryId)).ToLookup(item => item.BeneficiaryId)
                .SortBySequence(beneficiaryIds, lookup => lookup.Key).ToArray();
    }
}