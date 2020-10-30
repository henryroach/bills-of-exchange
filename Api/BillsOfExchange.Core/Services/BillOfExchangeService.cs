using AutoMapper;
using BillsOfExchange.Core.Contracts.BillOfExchange;
using BillsOfExchange.Core.Contracts.Endorsment;
using BillsOfExchange.DataProvider;
using System.Collections.Generic;
using System.Linq;

namespace BillsOfExchange.Core.Services
{
    public class BillOfExchangeService : BaseService, IBillOfExchangeService
    {
        public BillOfExchangeService(
            IMapper mapper,
            IPartyRepository partyRepository,
            IEndorsementRepository endorsementRepository,
            IBillOfExchangeRepository billOfExchangeRepository)
            :base(mapper, partyRepository, endorsementRepository, billOfExchangeRepository)
        { }


        ///<inheritdoc cref="IBillOfExchangeService"/>
        public IEnumerable<BillOfExchangeItem> Get(int take, int skip)
        {
            var billDB = billOfExchangeRepository.Get(take, skip);
            var bills = mapper.Map<IEnumerable<BillOfExchangeItem>>(billDB).ToList();

            for (int i = 0; i < bills.Count; i++)
            {
                bills[i] = BillItemFillingAndValidation(bills[i]);
            }

            return bills;
        }


        ///<inheritdoc cref="IBillOfExchangeService"/>
        public BillOfExchangeDetail GetById(int id)
        {
            var billDB = billOfExchangeRepository.GetByIds(new[] { id }).First();
            var bill = mapper.Map<BillOfExchangeDetail>(billDB);

            bill = BillItemFillingAndValidation(bill);

            bill.CurrentBeneficiaryId = endorsementRepository.GetByBillIds(new[] { id })
                .FirstOrDefault()
                .OrderBy(x => x.PreviousEndorsementId)
                .Select(x => x.NewBeneficiaryId)
                .LastOrDefault();

            string warningMessage = TryGetPartyName(bill.CurrentBeneficiaryId, out string currentBeneficiaryName);
            bill.CurrentBeneficiaryName = currentBeneficiaryName;
            bill.Warnings = AddWarning(bill.Warnings, warningMessage, bill.Id);

            return bill;
        }


        ///<inheritdoc cref="IBillOfExchangeService"/>
        public IEnumerable<EndorsementItem> GetEndorsementsByBillId(int id)
        {
            var endorsementsDB = endorsementRepository.GetByBillIds(new[] { id }).First(); //.OrderBy(x => x.Id)
            var endorsementsMapped = mapper.Map<IEnumerable<EndorsementItem>>(endorsementsDB);
            
            int firstBeneficiaryId = billOfExchangeRepository.Get(1, 0).First().BeneficiaryId;

            var endorsements = EndorsementItemsFillingAndValidation(endorsementsMapped, firstBeneficiaryId);

            return endorsements;
        }

        private LinkedList<EndorsementItem> EndorsementItemsFillingAndValidation(
            IEnumerable<EndorsementItem> endorsementsEnum, int firstBeneficiaryId)
        {
            if (endorsementsEnum == null || endorsementsEnum.Count() == 0)
            {
                return new LinkedList<EndorsementItem>();
            }

            var endorsements = new LinkedList<EndorsementItem>(endorsementsEnum);
            var current = endorsements.Last;

            List<int> usedEndorsementIds = new List<int>() { current.Value.Id };
            string warningMessage = null;

            while (current.Previous != null)
            {
                // Check data problem 5
                if (current.Value.NewBeneficiaryId == current.Previous.Value.NewBeneficiaryId)
                {
                    warningMessage = "Previous endorsement have the same beneficiary";
                    current.Value.Warnings = AddWarning(current.Value.Warnings, warningMessage, current.Value.Id);
                }
                // Check data problem 3
                if (current.Value.PreviousEndorsementId == null)
                {
                    warningMessage = "Reference to previous endorsement can't be empty";
                    current.Value.Warnings = AddWarning(current.Value.Warnings, warningMessage, current.Value.Id);
                }
                // Check data problem 1
                else if (usedEndorsementIds.Contains(current.Value.PreviousEndorsementId.Value))
                {
                    warningMessage = "Circle reference on endorsement with this id detected";
                    current.Value.Warnings = AddWarning(current.Value.Warnings, warningMessage, current.Value.PreviousEndorsementId.Value);
                }

                // Set NewBeneficiaryName for current endorsement and check data problems
                warningMessage = TryGetPartyName(current.Value.NewBeneficiaryId, out string currentName);
                current.Value.NewBeneficiaryName = currentName;
                current.Value.Warnings = AddWarning(current.Value.Warnings, warningMessage, current.Value.Id);

                usedEndorsementIds.Add(current.Value.Id);
                current = current.Previous;
            }

            // Check data problem 4
            if (current.Value.NewBeneficiaryId == firstBeneficiaryId)
            {
                current.Value.Warnings =
                    AddWarning(current.Value.Warnings, "First endorsement's beneficiary equals beneficiary from bill", current.Value.Id);
            }

            // Set NewBeneficiaryName for first endorsement and check data problems
            warningMessage = TryGetPartyName(current.Value.NewBeneficiaryId, out string resultName);
            current.Value.NewBeneficiaryName = resultName;
            current.Value.Warnings = AddWarning(current.Value.Warnings, warningMessage, current.Value.Id);

            return endorsements;
        }

    }
}
