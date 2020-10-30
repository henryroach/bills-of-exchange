using AutoMapper;
using BillsOfExchange.Core.Contracts.BillOfExchange;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillsOfExchange.Core.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper mapper;
        protected readonly IPartyRepository partyRepository;
        protected readonly IEndorsementRepository endorsementRepository;
        protected readonly IBillOfExchangeRepository billOfExchangeRepository;

        public BaseService(
            IMapper mapper,
            IPartyRepository partyRepository,
            IEndorsementRepository endorsementRepository,
            IBillOfExchangeRepository billOfExchangeRepository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.partyRepository = partyRepository ?? throw new ArgumentNullException(nameof(partyRepository));
            this.endorsementRepository = endorsementRepository ?? throw new ArgumentNullException(nameof(endorsementRepository));
            this.billOfExchangeRepository = billOfExchangeRepository ?? throw new ArgumentNullException(nameof(billOfExchangeRepository));
        }

        protected IList<string> AddWarning(IList<string> warnings, string warningMessage, int itemId = 0)
        {
            warnings = warnings ?? new List<string>();

            if (string.IsNullOrEmpty(warningMessage))
                return warnings;

            if (itemId > 0)
                warnings.Add($"Id({itemId}): {warningMessage}");
            else
                warnings.Add(warningMessage);

            return warnings;
        }

        protected TBill BillItemFillingAndValidation<TBill>(TBill bill) where TBill : BillOfExchangeItem
        {
            string warningMessage = TryGetPartyName(bill.DrawerId, out string resultDrawerName);
            bill.DrawerName = resultDrawerName;
            bill.Warnings = AddWarning(bill.Warnings, warningMessage, bill.Id);

            warningMessage = TryGetPartyName(bill.BeneficiaryId, out string resultBeneficiaryName);
            bill.BeneficiaryName = resultBeneficiaryName;
            bill.Warnings = AddWarning(bill.Warnings, warningMessage, bill.Id);

            // Check data problem 2
            if (bill.DrawerId == bill.BeneficiaryId)
            {
                string equalsMessage = "Drawer and Beneficiary are equals";
                bill.Warnings = AddWarning(bill.Warnings, equalsMessage, bill.Id);
            }

            return bill;
        }

        protected string TryGetPartyName(int partyId, out string partyNameField)
        {
            Party party = null;
            partyNameField = null;

            try
            {
                party = partyRepository.GetByIds(new[] { partyId }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return $"The party with id='{partyId}' is duplicit";
            }

            if (party == null)
            {
                return $"The party with id='{partyId}' haven't found";
            }

            partyNameField = party.Name;
            return null;
        }

    }
}
