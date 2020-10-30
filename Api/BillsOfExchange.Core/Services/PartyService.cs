using AutoMapper;
using BillsOfExchange.Core.Contracts.BillOfExchange;
using BillsOfExchange.Core.Contracts.Party;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;
using System.Collections.Generic;
using System.Linq;

namespace BillsOfExchange.Core.Services
{
    public class PartyService : BaseService, IPartyService
    {
        public PartyService(
            IMapper mapper,
            IPartyRepository partyRepository,
            IEndorsementRepository endorsementRepository,
            IBillOfExchangeRepository billOfExchangeRepository)
            : base(mapper, partyRepository, endorsementRepository, billOfExchangeRepository)
        { }


        ///<inheritdoc cref="IPartyService"/>
        public IEnumerable<PartyItem> Get(int take, int skip)
        {
            var partiesDB = partyRepository.Get(take, skip);
            var parties = mapper.Map<IEnumerable<PartyItem>>(partiesDB);

            parties = PartyItemValidation(parties);

            return parties;
        }

        private IEnumerable<PartyItem> PartyItemValidation(IEnumerable<PartyItem> parties)
        {
            var partyDuplicitIds = partyRepository.Get(int.MaxValue, 0)
                .Select(x => x.Id)
                .GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key)
                .ToArray();

            string warningMessage;
            foreach (var id in partyDuplicitIds)
            {
                var item = parties.FirstOrDefault(x => x.Id == id);
                if (item == null)
                    continue;
                item.Warnings = AddWarning(item.Warnings, "More than 1 record have this Id", id);
            }

            return parties;
        }


        public PartyItem GetById(int id)
        {
            var partiesDB = partyRepository.Get(int.MaxValue, 0).Where(x=>x.Id == id).ToList();
            var parties = mapper.Map<IEnumerable<PartyItem>>(partiesDB);
            PartyItem party = parties.First();
            if (partiesDB.Count > 1)
            {
                party.Name = null;
                party.Warnings.Add("There is more than one party in storage with the same id");
            }

            return party;
        }


        public IEnumerable<BillOfExchangeItem> GetBillsByDrawerId(int id)
        {
            var billsDB = billOfExchangeRepository.GetByDrawerIds(new[] { id }).FirstOrDefault();
            var bills = mapper.Map<IEnumerable<BillOfExchangeItem>>(billsDB).ToList();

            for (int i = 0; i < bills.Count; i++)
            {
                bills[i] = BillItemFillingAndValidation(bills[i]);
            }

            return bills;
        }


        public IEnumerable<BillOfExchangeItem> GetBillsByBeneficiaryId(int id)
        {
            var ownBillsWithoutEndorsments = GetOwnBillsWithoutEndorsments(id);
            var ownBillsWithEndorsments = GetOwnBillsWithEndorsments(id);

            var bills = ownBillsWithoutEndorsments.Concat(ownBillsWithEndorsments).ToList();

            for (int i = 0; i < bills.Count; i++)
            {
                bills[i] = BillItemFillingAndValidation(bills[i]);
            }

            return bills;
        }

        private IEnumerable<BillOfExchangeItem> GetOwnBillsWithoutEndorsments(int id)
        {
            var billsDB = billOfExchangeRepository.GetByBeneficiaryIds(new[] { id })
                .FirstOrDefault()?.ToList() ?? new List<BillOfExchange>();
            var exceptBillsIds = endorsementRepository
                .GetByBillIds(billsDB.Select(x => x.Id).ToArray())
                .Select(x => x.First().BillId)
                .ToList();

            billsDB = billsDB.Where(x => !exceptBillsIds.Contains(x.Id)).ToList();
            var bills = mapper.Map<IEnumerable<BillOfExchangeItem>>(billsDB).ToList();
            return bills;
        }

        private IEnumerable<BillOfExchangeItem> GetOwnBillsWithEndorsments(int id)
        {
            #region according to problems with data (inside the storage) it is risky to "appropriate" some endorsements

            //var billsCandidates = endorsementRepository.GetByNewBeneficiaryIds(new[] { id }).First()
            //    .Select(x => new { x.Id, x.BillId });

            //var candidatesIds = billsCandidates.Select(y => y.Id).ToList();
            //var wrongCandidatesIds = endorsementRepository.Get(int.MaxValue, 0)
            //    .Where(x => candidatesIds.Contains(x.PreviousEndorsementId ?? 0))
            //    .Select(x => x.PreviousEndorsementId.Value)
            //    .ToArray();
            //var winnerBills = billsCandidates.Where(x => !wrongCandidatesIds.Contains(x.Id));

            #endregion thats why...

            var allBillIds = billOfExchangeRepository.Get(int.MaxValue, 0)
                .Select(x => x.Id)
                .ToArray();

            var beneficiaryBillIds = endorsementRepository.GetByBillIds(allBillIds)
                .Select(x => x.OrderBy(y => y.Id).Last())
                .Where(x => x.NewBeneficiaryId == id)
                .Select(x => x.BillId)
                .ToArray();

            var billsDB = billOfExchangeRepository.GetByIds(beneficiaryBillIds);
            var bills = mapper.Map<IEnumerable<BillOfExchangeItem>>(billsDB).ToList();
            return bills;
        }
    }
}
