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
    public class PartyRepository : IPartyRepository
    {
        private static readonly Lazy<Party[]> _parties = new Lazy<Party[]>(
            () =>
            {
                string json = File.ReadAllText("Data/Parties.json");
                return JsonConvert.DeserializeObject<IEnumerable<Party>>(json).OrderBy(item => item.Id).ToArray();
            });

        public IEnumerable<Party> Get(int take, int skip)
            => _parties.Value.Skip(skip).Take(take);

        public int Count() => _parties.Value.Length;

        public IReadOnlyList<Party> GetByIds(IReadOnlyList<int> ids)
            => _parties.Value.Where(item => ids.Contains(item.Id)).SortBySequence(ids, item => item.Id)
                .ToArray();
    }
}