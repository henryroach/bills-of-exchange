using System.Collections.Generic;
using BillsOfExchange.DataProvider.Models;
using NUnit.Framework;
using System.Linq;

namespace BillsOfExchange.DataProvider.Tests
{
    public class PartyRepositoryTests
    {
        [Test]
        public void PartyRepository_Get()
        {
            PartyRepository sut = new PartyRepository();

            Party[] result1 = sut.Get(10, 0).ToArray();
            Party[] result2 = sut.Get(10, 5).ToArray();

            CollectionAssert.AreEqual(result1.Skip(5).Take(5), result2.Take(5));
        }

        [Test]
        public void PartyRepository_GetByIds()
        {
            PartyRepository sut = new PartyRepository();

            IReadOnlyList<Party> result = sut.GetByIds(new int[] { 8, 15 });

            Assert.Multiple(
                () =>
                {
                    Assert.AreEqual(8, result.First().Id);
                    Assert.AreEqual(15, result.Last().Id);
                });
        }
    }
}