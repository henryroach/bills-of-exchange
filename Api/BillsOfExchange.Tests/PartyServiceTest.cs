using System.Linq;
using BillsOfExchange.DataProvider;
using BillsOfExchange.Dto;
using BillsOfExchange.Services;
using NUnit.Framework;

namespace BillsOfExchange.Tests
{
    public class PartyServiceTest
    {
        [Test]
        public void GetListTest()
        {
            var partyService = new PartyService(new PartyRepository());
            var result = partyService.GetList(new PagedRequestDto { Take = 20 });

            Assert.AreEqual(20, result.Data.Count());
        }
    }
}