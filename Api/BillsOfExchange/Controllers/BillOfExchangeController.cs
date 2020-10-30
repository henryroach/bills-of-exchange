using BillsOfExchange.Core.Contracts.BillOfExchange;
using BillsOfExchange.Core.Contracts.Endorsment;
using BillsOfExchange.Core.Services;
using BillsOfExchange.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillsOfExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillOfExchangeController : ControllerBase
    {
        private readonly IBillOfExchangeService billOfExchangeService;

        public BillOfExchangeController(IBillOfExchangeService billOfExchangeService)
        {
            this.billOfExchangeService = billOfExchangeService ?? throw new ArgumentNullException(nameof(billOfExchangeService));
        }

        [EnableCors]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("GetItems")]
        public List<BillOfExchangeItem> GetItems(int pageSize = 10, int pageNumber = 1)
        {
            int skip = PagingUtils.GetSkipedItemsCount(pageSize, pageNumber);
            var result = billOfExchangeService.Get(pageSize, skip).ToList();

            return result;
        }

        [EnableCors]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("GetById/{id}")]
        public BillOfExchangeDetail GetById(int id)
        {
            var result = billOfExchangeService.GetById(id);

            return result;
        }

        [EnableCors]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("GetEndorsementsByBillId/{id}")]
        public IEnumerable<EndorsementItem> GetEndorsementsByBillId(int id)
        {
            var result = billOfExchangeService.GetEndorsementsByBillId(id);
            return result;
        }
    }
}
