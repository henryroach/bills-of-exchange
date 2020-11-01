using System.Collections.Generic;
using BillsOfExchange.DataProvider.Models;
using BillsOfExchange.Dto;
using BillsOfExchange.Services;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillsOfExchangeController : ControllerBase
    {
        private readonly IBillsOfExchangeService _billsOfExchangeService;
        private readonly IEndorsementService _endorsementService;

        public BillsOfExchangeController(
            IBillsOfExchangeService billsOfExchangeService,
            IEndorsementService endorsementService)
        {
            _billsOfExchangeService = billsOfExchangeService;
            _endorsementService = endorsementService;
        }

        [HttpGet("get")]
        public PagedResultDto<BillOfExchangeDto> Get(PagedRequestDto request) =>
            _billsOfExchangeService.GetList(request);

        [HttpGet("getByDrawerId")]
        public IEnumerable<BillOfExchangeDto> GetByDrawerId(int drawerId) =>
            _billsOfExchangeService.GetByDrawerId(drawerId);

        [HttpGet("getByBeneficiaryId")]
        public IEnumerable<BillOfExchangeDto> GetByBeneficiaryId(int beneficiaryId) =>
            _billsOfExchangeService.GetByBeneficiaryId(beneficiaryId);

        [HttpGet("getEndorsement")]
        public LinkedList<Endorsement> GetEndorsements(int billId) => _endorsementService.GetEndorsements(billId);
    }
}