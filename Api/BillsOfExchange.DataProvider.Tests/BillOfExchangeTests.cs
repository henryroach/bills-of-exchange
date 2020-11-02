using System.Collections.Generic;
using System.Linq;
using BillsOfExchange.DataProvider.Models;
using NUnit.Framework;

namespace BillsOfExchange.DataProvider.Tests
{
    public class BillOfExchangeTests
    {
        [Test]
        public void BillOfExchangeRepository_Get()
        {
            BillOfExchangeRepository sut = new BillOfExchangeRepository();

            BillOfExchange[] result1 = sut.Get(10, 0).ToArray();
            BillOfExchange[] result2 = sut.Get(10, 5).ToArray();

            CollectionAssert.AreEqual(result1.Skip(5).Take(5), result2.Take(5));
        }

        [Test]
        public void BillOfExchangeRepository_GetByIds()
        {
            BillOfExchangeRepository sut = new BillOfExchangeRepository();

            IReadOnlyList<BillOfExchange> result = sut.GetByIds(new int[] { 6, 7, 8 });

            Assert.Multiple(
                () =>
                {
                    Assert.AreEqual(6, result.First().Id);
                    Assert.AreEqual(8, result.Last().Id);
                });
        }

        [Test]
        public void BillOfExchangeRepository_GetByDrawerIds()
        {
            BillOfExchangeRepository sut = new BillOfExchangeRepository();

            IReadOnlyList<IEnumerable<BillOfExchange>> result = sut.GetByDrawerIds(new int[] { 14, 9 });

            Assert.Multiple(
                () =>
                {
                    Assert.AreEqual(1, result.First().Count());
                    Assert.AreEqual(2, result.Last().Count());
                });
        }

        [Test]
        public void BillOfExchangeRepository_GetByBeneficiaryIds()
        {
            BillOfExchangeRepository sut = new BillOfExchangeRepository();

            IReadOnlyList<IEnumerable<BillOfExchange>> result = sut.GetByBeneficiaryIds(new int[] { 13 });
            BillOfExchange bill1 = result.Single().First();
            BillOfExchange bill2 = result.Single().Last();

            Assert.AreEqual(2, bill1.Id);
            Assert.AreEqual(4, bill2.Id);
        }
    }
}