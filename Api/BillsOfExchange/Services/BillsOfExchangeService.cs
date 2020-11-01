using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.XPath;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;
using BillsOfExchange.Dto;

namespace BillsOfExchange.Services
{
    public class BillsOfExchangeService : IBillsOfExchangeService
    {
        private readonly IBillOfExchangeRepository _repository;
        private readonly IEndorsementRepository _endorsementRepository;

        public BillsOfExchangeService(
            IBillOfExchangeRepository repository,
            IEndorsementRepository endorsementRepository)
        {
            _repository = repository;
            _endorsementRepository = endorsementRepository;
        }

        public PagedResultDto<BillOfExchangeDto> GetList(PagedRequestDto request)
        {
            if (request == null)
            {
                request = new PagedRequestDto { Skip = 0, Take = 10 };
            }

            var list = _repository.Get(request.Take, request.Skip).ToList();

            var result = new PagedResultDto<BillOfExchangeDto>();
            if (list.Any())
            {
                result.Data = MapToDto(list);
            }

            return result;
        }

        public IEnumerable<BillOfExchangeDto> GetByDrawerId(int drawerId)
        {
            var billOfExchanges = _repository.GetByDrawerIds(new[] { drawerId }).Single();
            return MapToDto(billOfExchanges);
        }

        public IEnumerable<BillOfExchangeDto> GetByBeneficiaryId(int beneficiaryId)
        {
            var billsOfExchange = _repository.GetByBeneficiaryIds(new[] { beneficiaryId }).FirstOrDefault();
            var billsIds = _endorsementRepository.GetByNewBeneficiaryIds(new[] { beneficiaryId }).SelectMany(
                x => x.Where(y => y.PreviousEndorsementId == null).Select(y => y.BillId))
                .Distinct().ToImmutableList();

            return MapToDto((billsOfExchange ?? new List<BillOfExchange>()).Concat(_repository.GetByIds(billsIds)));
        }

        public BillOfExchange GetById(int id)
        {
            try
            {
                var billOfExchange = _repository.GetByIds(new[] { id }).SingleOrDefault();
                if (billOfExchange == null)
                {
                    throw new RecordNotFoundException($"Can't find bill of exchange with id {id}");
                }

                return billOfExchange;
            }
            catch (InvalidOperationException)
            {
                throw new ApplicationException($"Found more than one bill of exchange with id {id}");
            }
        }

        private IEnumerable<BillOfExchangeDto> MapToDto(IEnumerable<BillOfExchange> billsOfExchange)
        {
            foreach (var boe in billsOfExchange)
            {
                yield return new BillOfExchangeDto
                {
                    Id = boe.Id,
                    DrawerId = boe.DrawerId,
                    BeneficiaryId = boe.BeneficiaryId,
                    Amount = boe.Amount
                };
            }
        }
    }
}