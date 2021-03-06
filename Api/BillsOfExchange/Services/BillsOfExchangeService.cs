using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;
using BillsOfExchange.Dto;

namespace BillsOfExchange.Services
{
    public class BillsOfExchangeService : IBillsOfExchangeService
    {
        private readonly IBillOfExchangeRepository _repository;
        private readonly IEndorsementRepository _endorsementRepository;
        private readonly PartyService _partyService;

        public BillsOfExchangeService(
            IBillOfExchangeRepository repository,
            IEndorsementRepository endorsementRepository,
            PartyService partyService)
        {
            _repository = repository;
            _endorsementRepository = endorsementRepository;
            _partyService = partyService;
        }

        public PagedResultDto<BillOfExchangeDto> GetList(PagedRequestDto request)
        {
            if (request == null)
            {
                request = new PagedRequestDto { Skip = 0, Take = 10 };
            }

            var count = _repository.Count();
            if (count == 0)
            {
                return new PagedResultDto<BillOfExchangeDto>();
            }

            var list = _repository.Get(request.Take, request.Skip).ToList();

            return new PagedResultDto<BillOfExchangeDto>
            {
                Count = count,
                Data = MapToDto(list)
            };
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

        public BillOfExchangeDetailDto GetById(int id)
        {
            try
            {
                var billOfExchange = _repository.GetByIds(new[] { id }).SingleOrDefault();
                if (billOfExchange == null)
                {
                    throw new RecordNotFoundException($"Can't find bill of exchange with id {id}");
                }

                var parties = _partyService.GetByIds(new[] { billOfExchange.DrawerId, billOfExchange.BeneficiaryId })
                    .ToList();

                return new BillOfExchangeDetailDto
                {
                    Id = billOfExchange.Id,
                    DrawerId = billOfExchange.DrawerId,
                    BeneficiaryId = billOfExchange.BeneficiaryId,
                    Amount = billOfExchange.Amount,
                    Drawer = parties.First(),
                    FirstBeneficiary = parties.Last()
                };
            }
            catch (InvalidOperationException)
            {
                throw new ApplicationException($"Found more than one bill of exchange with id {id}");
            }
        }

        private static IEnumerable<BillOfExchangeDto> MapToDto(IEnumerable<BillOfExchange> billsOfExchange)
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