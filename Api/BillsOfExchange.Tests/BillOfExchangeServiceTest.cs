using System.Linq;
using BillsOfExchange.DataProvider;
using BillsOfExchange.Dto;
using BillsOfExchange.Services;
using NUnit.Framework;

namespace BillsOfExchange.Tests
{
    public class BillOfExchangeServiceTest
    {
        private BillsOfExchangeService _billsOfExchangeService;

        public BillOfExchangeServiceTest()
        {
            _billsOfExchangeService = new BillsOfExchangeService(new BillOfExchangeRepository(), new EndorsementRepository());
        }

        [Test]
        public void GetListTest()
        {
            var result = _billsOfExchangeService.GetList(new PagedRequestDto { Take = 20 });
            Assert.AreEqual(20, result.Data.Count());
        }

        [Test]
        public void GetListTest_Null()
        {
            var result = _billsOfExchangeService.GetList(null);
            Assert.AreEqual(10, result.Data.Count());
        }

        [Test]
        public void GetByDrawerIdTest()
        {
            var result = _billsOfExchangeService.GetByDrawerId(2);
            Assert.Equals(2, result.Count());
        }

        [Test]
        public void GetByBeneficiaryIdTest()
        {
            var result = _billsOfExchangeService.GetByBeneficiaryId(9);

        }
    }
}