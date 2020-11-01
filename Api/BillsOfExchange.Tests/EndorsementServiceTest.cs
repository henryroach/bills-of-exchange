using System;
using BillsOfExchange.DataProvider;
using BillsOfExchange.Services;
using NUnit.Framework;

namespace BillsOfExchange.Tests
{
    public class EndorsementServiceTest
    {
        private EndorsementService _endorsementService;
        private BillOfExchangeRepository _billOfExchangeRepository;
        private BillsOfExchangeService _billsOfExchangeService;
        private EndorsementRepository _endorsementRepository;

        public EndorsementServiceTest()
        {
            _billOfExchangeRepository = new BillOfExchangeRepository();
            _endorsementRepository = new EndorsementRepository();
            _billsOfExchangeService = new BillsOfExchangeService(_billOfExchangeRepository, _endorsementRepository);

            _endorsementService = new EndorsementService(new EndorsementRepository(), _billsOfExchangeService);
        }

        [Test]
        public void GetEndorsementTest()
        {
            var bills = _billOfExchangeRepository.Get(1000, 0);
            foreach (var b in bills)
            {
                _endorsementService.GetEndorsements(b.Id);
            }
        }

        [Test]
        public void GetEndorsementTest_BillNotExists()
        {
            Assert.Throws<RecordNotFoundException>(() => _endorsementService.GetEndorsements(-1));
        }

        [Test]
        public void GetEndorsementTest_CircleReference()
        {
            Assert.Throws<ApplicationException>(() => _endorsementService.GetEndorsements(8));
        }

        [Test]
        public void GetEndorsementTest_SameDrawerBeneficiary()
        {
            var exception = Assert.Throws<ApplicationException>(() => _endorsementService.GetEndorsements(2));
            Assert.AreEqual("Bill of exchange has same drawer as beneficiary.", exception.Message);
        }

        [Test]
        public void GetEndorsementTest_MultiplePreviousNull()
        {
            var exception = Assert.Throws<ApplicationException>(() => _endorsementService.GetEndorsements(10));
            Assert.AreEqual("Indosament data inconsistency - two first endorsements", exception.Message);
        }

        [Test]
        public void GetEndorsementTest_SameBeneficiaryAsNewBeneficiary()
        {
            var exception = Assert.Throws<ApplicationException>(() => _endorsementService.GetEndorsements(4));
            Assert.AreEqual("Endorsement has the same new beneficiary party as bill of exchange", exception.Message);
        }

        [Test]
        public void GetEndorsementTest_SameNewBeneficiary()
        {
            var exception = Assert.Throws<ApplicationException>(() => _endorsementService.GetEndorsements(6));
            Assert.AreEqual("Indosament data inconsistenci - same new beneficiary id", exception.Message);
        }
    }
}