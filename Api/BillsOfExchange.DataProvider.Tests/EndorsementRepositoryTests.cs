using System.Collections.Generic;
using System.Linq;
using BillsOfExchange.DataProvider.Models;
using NUnit.Framework;

namespace BillsOfExchange.DataProvider.Tests
{
    public class EndorsementRepositoryTests
    {
        [Test]
        public void EndorsementRepository_Get()
        {
            EndorsementRepository sut = new EndorsementRepository();

            Endorsement[] result1 = sut.Get(10, 0).ToArray();
            Endorsement[] result2 = sut.Get(10, 5).ToArray();

            CollectionAssert.AreEqual(result1.Skip(5).Take(5), result2.Take(5));
        }

        [Test]
        public void EndorsementRepository_GetByIds()
        {
            EndorsementRepository sut = new EndorsementRepository();

            IReadOnlyList<Endorsement> result = sut.GetByIds(new int[] { 1, 3 });

            Assert.Multiple(
                () =>
                {
                    Assert.AreEqual(1, result.First().Id);
                    Assert.AreEqual(3, result.Last().Id);
                });
        }

        [Test]
        public void EndorsementRepository_GetByBillIds()
        {
            EndorsementRepository sut = new EndorsementRepository();

            IReadOnlyList<IEnumerable<Endorsement>> result = sut.GetByBillIds(new int[] { 1, 3 });

            Assert.Multiple(
                () =>
                {
                    Assert.AreEqual(4, result.First().Count());
                    Assert.AreEqual(9, result.Last().Count());
                });
        }

        [Test]
        public void EndorsementRepository_GetByNewBeneficiaryIds()
        {
            EndorsementRepository sut = new EndorsementRepository();

            IReadOnlyList<IEnumerable<Endorsement>> result = sut.GetByNewBeneficiaryIds(new int[] { 15 });

            Assert.AreEqual(3, result.First().Count());
        }
    }
}