using System;
using System.Collections.Generic;
using System.Linq;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;
using BillsOfExchange.Dto;

namespace BillsOfExchange.Services
{
    public class EndorsementService : IEndorsementService
    {
        private readonly IEndorsementRepository _repository;
        private readonly IBillsOfExchangeService _billsOfExchangeService;

        public EndorsementService(IEndorsementRepository repository, IBillsOfExchangeService billsOfExchangeService)
        {
            _repository = repository;
            _billsOfExchangeService = billsOfExchangeService;
        }

        public IEnumerable<EndorsementDto> GetEndorsements(int billId)
        {
            var billOfExchange = _billsOfExchangeService.GetById(billId);
            if (billOfExchange.DrawerId == billOfExchange.BeneficiaryId)
            {
                throw new ApplicationException("Bill of exchange has same drawer as beneficiary.");
            }

            var endorsements = _repository.GetByBillIds(new[] { billId }).First().ToList();
            var list = new LinkedList<Endorsement>();
            Endorsement currentEndorsement = null;
            while (currentEndorsement == null ||
                   endorsements.Any(x => x.PreviousEndorsementId == currentEndorsement.Id))
            {
                currentEndorsement = currentEndorsement == null
                    ? GetFirstEndorsement(endorsements)
                    : endorsements.FirstOrDefault(x => x.PreviousEndorsementId == currentEndorsement.Id);

                if (list.Any(x => x.Id == currentEndorsement.Id))
                {
                    throw new ApplicationException(
                        "Indosament data inconsistency - circle reference between endorsements");
                }

                if (currentEndorsement.NewBeneficiaryId == billOfExchange.BeneficiaryId)
                {
                    throw new ApplicationException(
                        $"Endorsement has the same new beneficiary party as bill of exchange");
                }

                list.AddLast(currentEndorsement);
            }

            if (list.Count != endorsements.Count)
            {
                throw new ApplicationException("List of endorsement is not linked.");
            }

            var newBeneficiaries = list.Select(x => x.NewBeneficiaryId).ToList();
            if (newBeneficiaries.Count() != newBeneficiaries.Distinct().Count())
            {
                throw new ApplicationException("Indosament data inconsistenci - same new beneficiary id");
            }

            return MapToDto(list);
        }

        private Endorsement GetFirstEndorsement(List<Endorsement> endorsements)
        {
            try
            {
                return endorsements.Single(x => x.PreviousEndorsementId == null);
            }
            catch (InvalidOperationException e)
            {
                throw new ApplicationException("Indosament data inconsistency - two first endorsements");
            }
        }

        private IEnumerable<EndorsementDto> MapToDto(LinkedList<Endorsement> endorsements)
        {
            foreach (var e in endorsements)
            {
                yield return new EndorsementDto
                {
                    Id = e.Id,
                    BillId = e.BillId,
                    NewBeneficiary = e.NewBeneficiaryId,
                    PreviousEndorsementId = e.PreviousEndorsementId
                };
            }
        }
    }
}