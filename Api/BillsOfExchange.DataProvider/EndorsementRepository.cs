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
    public class EndorsementRepository : IEndorsementRepository
    {
        private static readonly Lazy<Endorsement[]> _endorsements = new Lazy<Endorsement[]>(
            () =>
            {
                string json = File.ReadAllText("Data/Endorsements.json");
                return JsonConvert.DeserializeObject<IEnumerable<Endorsement>>(json).ToArray();
            });

        public IEnumerable<Endorsement> Get(int take, int skip)
            => _endorsements.Value.Skip(skip).Take(take);

        public IReadOnlyList<Endorsement> GetByIds(IReadOnlyList<int> ids)
            => _endorsements.Value.Where(item => ids.Contains(item.Id)).SortBySequence(ids, item => item.Id)
                .ToArray();

        public IReadOnlyList<IEnumerable<Endorsement>> GetByBillIds(IReadOnlyList<int> billIds)
            => _endorsements.Value.Where(item => billIds.Contains(item.BillId)).ToLookup(item => item.BillId)
                .SortBySequence(billIds, lookup => lookup.Key).ToArray();

        public IReadOnlyList<IEnumerable<Endorsement>> GetByNewBeneficiaryIds(IReadOnlyList<int> beneficiaryIds)
            => _endorsements.Value.Where(item => beneficiaryIds.Contains(item.NewBeneficiaryId))
                .ToLookup(item => item.NewBeneficiaryId)
                .SortBySequence(beneficiaryIds, lookup => lookup.Key).ToArray();
    }
}