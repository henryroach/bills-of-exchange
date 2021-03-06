﻿using System.Collections.Generic;
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
        public PagedResultDto<BillOfExchangeDto> Get([FromQuery] PagedRequestDto request) =>
            _billsOfExchangeService.GetList(request);

        [HttpGet("getById/{id}")]
        public BillOfExchangeDetailDto GetById([FromRoute] int id) =>
            _billsOfExchangeService.GetById(id);

        [HttpGet("getByDrawerId")]
        public IEnumerable<BillOfExchangeDto> GetByDrawerId(int drawerId) =>
            _billsOfExchangeService.GetByDrawerId(drawerId);

        [HttpGet("getByBeneficiaryId")]
        public IEnumerable<BillOfExchangeDto> GetByBeneficiaryId(int beneficiaryId) =>
            _billsOfExchangeService.GetByBeneficiaryId(beneficiaryId);

        [HttpGet("getEndorsement/{billId}")]
        public IEnumerable<EndorsementDto> GetEndorsements(int billId) => _endorsementService.GetEndorsements(billId);
    }
}